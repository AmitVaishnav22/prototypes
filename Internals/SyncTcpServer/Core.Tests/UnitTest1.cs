using System;
using System.Collections.Generic;
using Xunit;
using core;

namespace Core.Tests
{
    public class RespTests
    {
        [Fact]
        public void TestSimpleStringDecode()
        {
            var cases = new Dictionary<string, string>
            {
                { "+OK\r\n", "OK" }
            };

            foreach (var kv in cases)
            {
                var (value, _) = RespDecoder.Decode(System.Text.Encoding.UTF8.GetBytes(kv.Key));
                Assert.Equal(kv.Value, value);
            }
        }

        [Fact]
        public void TestError()
        {
            var cases = new Dictionary<string, string>
            {
                { "-Error message\r\n", "Error message" }
            };

            foreach (var kv in cases)
            {
                var (value, _) = RespDecoder.Decode(System.Text.Encoding.UTF8.GetBytes(kv.Key));
                Assert.Equal(kv.Value, value);
            }
        }

        [Fact]
        public void TestInt64()
        {
            var cases = new Dictionary<string, long>
            {
                { ":0\r\n", 0 },
                { ":1000\r\n", 1000 }
            };

            foreach (var kv in cases)
            {
                var (value, _) = RespDecoder.Decode(System.Text.Encoding.UTF8.GetBytes(kv.Key));
                Assert.Equal(kv.Value, value);
            }
        }

        [Fact]
        public void TestBulkStringDecode()
        {
            var cases = new Dictionary<string, string>
            {
                { "$5\r\nhello\r\n", "hello" },
                { "$0\r\n\r\n", "" }
            };

            foreach (var kv in cases)
            {
                var (value, _) = RespDecoder.Decode(System.Text.Encoding.UTF8.GetBytes(kv.Key));
                Assert.Equal(kv.Value, value);
            }
        }

        [Fact]
        public void TestArrayDecode()
        {
            var cases = new Dictionary<string, object[]>
            {
                { "*0\r\n", Array.Empty<object>() },

                { "*2\r\n$5\r\nhello\r\n$5\r\nworld\r\n",
                    new object[] { "hello", "world" } },

                { "*3\r\n:1\r\n:2\r\n:3\r\n",
                    new object[] { 1L, 2L, 3L } },

                { "*5\r\n:1\r\n:2\r\n:3\r\n:4\r\n$5\r\nhello\r\n",
                    new object[] { 1L, 2L, 3L, 4L, "hello" } },

                { "*2\r\n*3\r\n:1\r\n:2\r\n:3\r\n*2\r\n+Hello\r\n-World\r\n",
                    new object[]
                    {
                        new object[] { 1L, 2L, 3L },
                        new object[] { "Hello", "World" }
                    }
                }
            };

            foreach (var kv in cases)
            {
                var (value, _) = RespDecoder.Decode(System.Text.Encoding.UTF8.GetBytes(kv.Key));

                var array = (object[])value;
                var expected = kv.Value;

                Assert.Equal(expected.Length, array.Length);

                for (int i = 0; i < array.Length; i++)
                {
                    Assert.Equal(expected[i]?.ToString(), array[i]?.ToString());
                }
            }
        }
    }
}