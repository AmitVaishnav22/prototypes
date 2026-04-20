using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Pipelines;
using System.Text;

namespace core{
    public static class RespDecoder{

        static byte[] SubArray(byte[] data,int offset)
        {
            int len=data.Length-offset;
            byte[] result=new byte[len];
            Array.Copy(data,offset,result,0,len);
            return result;
        }
        static (int length,int delta) ReadLength(byte[] data)
        {
            int pos=0;
            int length=0;
            for (; pos < data.Length; pos++)
            {
                byte b = data[pos];
                if (!(b>='0' && b <= '9'))
                {
                    return (length,pos+2);
                }
                length=length*10+(b-'0');
            }
            return (length,pos+2);
        }
        static (string value,int delta) ReadSimpleString(byte[] data)
        {
            int pos=1;
            while (data[pos] != '\r')
            {
                pos++;
            }
            string result = Encoding.UTF8.GetString(data, 1, pos - 1);
            return (result, pos + 2);
        }

        static (string value,int delta) ReadError(byte[] data)
        {
            return ReadSimpleString(data);
        }

        static (long value , int delta) ReadInt64(byte[] data)
        {
            int pos=1;
            long value=0;
            while (data[pos] != '\r')
            {
                value=value*10+(data[pos]-'0');
                pos++;
            }
            return (value,pos+2);
        }

        static (string value, int delta) ReadBulkString(byte[] data)
        {
            int pos=1;
            var (len,delta)=ReadLength(SubArray(data,pos));
            pos+=delta;
            string result=Encoding.UTF8.GetString(data,pos,len);
            return (result,pos+len+2);
        }

        static (object[] value,int delta) ReadArray(byte[] data)
        {
            int pos=1;
            var (count,delta)=ReadLength(SubArray(data,pos));
            pos+=delta;
            var list=new List<object>();
            for (int i = 0; i < count; i++)
            {
                var (elem,d,err)=DecodeOne(SubArray(data,pos));
                if (err != null)
                {
                    throw err;
                }
                list.Add(elem);
                pos+=d;
            }
            return (list.ToArray(),pos);
        }
        static(object value,int delta,Exception error) DecodeOne(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return (null, 0, new ArgumentException("Data cannot be null or empty."));
            }
            switch ((char)data[0])
            {
                case '+':
                    var(s,d1)=ReadSimpleString(data);
                    return (s,d1,null);
                case '-':
                    var(e,d2)=ReadError(data);
                    return (e,d2,null);
                case ':':
                    var(i,d3)=ReadInt64(data);
                    return (i,d3,null);
                case '$':
                    var(b,d4)=ReadBulkString(data);
                    return (b,d4,null);
                case '*':
                    var(a,d5)=ReadArray(data);
                    return (a,d5,null);
            }
            return (null, 0, new ArgumentException("Invalid RESP type."));
        }
        public static (object value,Exception error) Decode(byte[] data)
        {
            if (data == null || data.Length == 0)
            {
                return (null, new ArgumentException("Data cannot be null or empty."));
            }
            var (value,_,err)=DecodeOne(data);
            return (value,err);
        }
    }
}