from datetime import datetime
import requests
import json
from utils.util import log


def get_current_time():
    return datetime.now().strftime("%Y-%m-%d %H:%M:%S")

def get_city_weather(city):
    url=f"https://wttr.in/{city}?format=3"
    response=requests.get(url)
    response.raise_for_status()
    return response.text

def get_user_city():
    url="https://ipinfo.io/json"
    response=requests.get(url)
    response.raise_for_status()
    data=response.json()
    return data.get("city","Unknown")
 
TOOLS=[
    {
        "type":"function",
        "function":{
            "name":"get_current_time",
            "description":"Returns the current time in YYYY-MM-DD HH:MM:SS format",
            "parameters":{"type":"object","properties":{}},
        },
    },
    {
        "type":"function",
        "function":{
            "name":"get_city_weather",
            "description":"Returns the current weather for a given city",
            "parameters":{
                "type":"object",
                "properties":{
                    "city":{"type":"string","description":"The name of the city to get the weather for"}
                },
                "required":["city"]
            }
        }
    },
    {
        "type":"function",
        "function":{
            "name":"get_user_city",
            "description":"Returns the city of the user based on their IP address",
            "parameters":{"type":"object","properties":{}},
        }
    }
]   

def run_tool(tool_call):
    func_name=tool_call.function.name
    try:
        func_args=json.loads(tool_call.function.arguments or "{}")
        log(f"Executing tool {func_name} with arguments: {func_args}")
        fn = FUNCTION_NAME_TO_FUNCTION_MAPPING.get(func_name)
        res = fn(**func_args)
    except Exception as e:
        res = f"Error executing tool {func_name}: {str(e)}"
    log(f"Tool {func_name} returned: {res}")
    return res

FUNCTION_NAME_TO_FUNCTION_MAPPING = {
    "get_current_time": get_current_time,
    "get_city_weather": get_city_weather,
    "get_user_city": get_user_city
}