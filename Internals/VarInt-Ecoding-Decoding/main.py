

# Online Python compiler (interpreter) to run Python online. 
# Write Python 3 code in this online editor and run it. 

# encoding 
def encode(num): 
    if num==0:
        return [0]
    arr=[] 
    curr=0 
    i=0 
    while num: 
        setBit=(num&1) 
        if setBit: 
            curr|=(1<<(i%7)) 
        num>>=1 
        i+=1 
        if i%7==0: 
            temp=curr 
            if num: 
                temp|=(1<<7) 
            arr.append(temp) 
            curr=0 
    if curr: 
        arr.append(curr) 
    return arr 
# decoding 

def decode(arr): 
    ans=0 
    i=0 
    for el in arr: 
        num=calx(el) 
        ans|=(num<<7*i) 
        i+=1 
    return ans 

def calx(el): 
    curr=0 
    for i in range(7): 
        if issetbit(el,i): 
            curr|=(1<<i) 
    return curr 

def issetbit(el,i): 
    return (el&(1<<i)) 
# 100100100 

# delta encoding 

def deltaEncode(timeseries): 
    diff=[] 
    n=len(timeseries) 
    diff.append(timeseries[0]) 
    for i in range(1,n): 
        diff.append((timeseries[i]-timeseries[i-1])) 
    return diff 

def deltaDecode(encodeTimeSeries,timeseriesEnc): 
    originalRawTimeSerieData=[] 
    prev=0 
    for temp in encodeTimeSeries: 
        num=decode(temp) 
        prev+=num 
        originalRawTimeSerieData.append(prev) 
    #print("original back",originalRawTimeSerieData) 
    return originalRawTimeSerieData 

if __name__=="__main__": 
    # num=int(292) 
    # arr=encode(num) 
    # num=decode(arr) 
    # print("num",num) 
    timeseries=[1000, 1005, 1010, 1015] 
    timeseriesEnc=deltaEncode(timeseries) 
    print("delta encoded",timeseriesEnc)
    networkEncValue=0 
    networkEncKey=[] 
    for el in timeseriesEnc: 
        print(encode(el)) 
        networkEncValue+=len(encode(el)) 
        networkEncKey.append(encode(el)) 
    #print("net",networkEncKey) 
    original=deltaDecode(networkEncKey,timeseriesEnc) 
    print("original",original) 
    # print(networkEncValue) # no of compressed bits