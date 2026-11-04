import os

START_SEQ = "\033[94m"
END_SEQ = "\033[0m"
def log(message):
    if os.getenv("DEBUG", "False").lower() == "true":
        print(f"{START_SEQ}{message}{END_SEQ}")
        
