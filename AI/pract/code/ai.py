import os
from openai import OpenAI
from dotenv import load_dotenv
from tools import TOOLS, run_tool
from utils.util import log      
from types import SimpleNamespace

load_dotenv()

MAX_STEPS = 5
client = OpenAI(
    base_url=os.getenv("BASE_URL"),
    api_key=os.getenv("API_KEY")
)
messages = []
messages.append(
    {
        "role":"system",
        "content":"Act like an polite agent"
    }
)

def build_tool_calls(tool_calls):
    """
    Builds a list of tool calls in the format expected by the OpenAI API.
    """
    payload = []
    for tool_call in tool_calls:
        payload.append({
            "id": tool_call.id,
            "type": "function",
            "function": {
                "name": tool_call.function.name,
                "arguments": tool_call.function.arguments
            }
        })
    return payload

def stream_chat(history):
    stream=client.chat.completions.create(
        model=os.getenv("MODEL"),
        messages=history,
        tools=TOOLS,
        stream=True
    )
    full_response=""
    calls={}
    label_printed=False
    try:
        for chunk in stream:
            if not chunk.choices:
                continue
            delta=chunk.choices[0].delta
            if delta.content:
                if not label_printed:
                    print("Assistant:",end="",flush=True)
                    label_printed=True
                print(delta.content,end="",flush=True)
                full_response+=delta.content
            for tool_chunk in delta.tool_calls or []:
                obj=calls.setdefault(tool_chunk.index,SimpleNamespace(id=tool_chunk.id,function=SimpleNamespace(name="",arguments="")))
                if tool_chunk.id:
                    obj.id=tool_chunk.id
                if tool_chunk.function and tool_chunk.function.name:
                    obj.function.name+=tool_chunk.function.name
                if tool_chunk.function and tool_chunk.function.arguments:
                    obj.function.arguments+=tool_chunk.function.arguments
    except KeyboardInterrupt as e:
        log(f"Stopped by you: {e}")
        calls.clear()
    finally:
        stream.close()
    if label_printed:
        print()
    return full_response,list(calls.values())
        
while True:
    user_input=input("\n You:")
    if user_input.lower().strip() in ["exit","quit","bye","goodbye"]:
        break 
    messages.append(
        {
            "role":"user",
            "content":user_input
        } 
    )
    
    for step in range(MAX_STEPS):
        full_response,tool_calls=stream_chat(messages)
        if not tool_calls:
            #messages.append({"role":"assistant","content":full_response})
            break
        log(f"Step {step+1}: model requested {len(tool_calls)} tool calls")
        messages.append({
            "role":"assistant",
            "content":full_response,
            "tool_calls":build_tool_calls(tool_calls)
        })
        # response=client.chat.completions.create(
        #         model=os.getenv("MODEL"),
        #         messages=messages,
        #         tools=TOOLS
        #     )
        # msg = response.choices[0].message
        # if not msg.tool_calls:
        #     break
        # log(f"Step {step+1}: model requested  {len(msg.tool_calls)} tool calls")
        # messages.append(msg)
        for tool_call in tool_calls:
            result=run_tool(tool_call)
            messages.append({
                "role":"tool",
                "tool_call_id":tool_call.id,
                "content":str(result)
            })
    else:
        messages.append({
            "role":"assistant",
            "content":"I have reached the maximum number of steps and cannot provide a response."
        })