using System.Collections.Generic;
using SourceMaps.StackTraces;
using Xunit;

namespace SourceMaps.JavaScriptStackTraces.Tests
{
    public class StackTraceParserTests
    {
        [Theory]
        [MemberData(nameof(StackTraceData))]
        public void ParseTests(string stacktrace, List<StackFrame> expected)
        {
            var actual = StackTraceParser.Parse(stacktrace).Frames;

            Assert.Equal(expected.Count, actual.Count);
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].File, actual[i].File);
                Assert.Equal(expected[i].Method, actual[i].Method);
                Assert.Equal(expected[i].Arguments, actual[i].Arguments);
                Assert.Equal(expected[i].LineNumber, actual[i].LineNumber);
                Assert.Equal(expected[i].ColumnNumber, actual[i].ColumnNumber);
            }
        }

        public static IEnumerable<object[]> StackTraceData()
        {
            yield return new object[]
            {
                @"foo@https://localhost:5001/js/one.js:1:95
bar@https://localhost:5001/js/two.js:1:45
baz@https://localhost:5001/js/three.js:1:524",
                new List<StackFrame>()
                {
                    new StackFrame { File = "https://localhost:5001/js/one.js", Method = "foo", Arguments = "", LineNumber = 1, ColumnNumber = 95 },
                    new StackFrame { File = "https://localhost:5001/js/two.js", Method = "bar", Arguments = "", LineNumber = 1, ColumnNumber = 45 },
                    new StackFrame { File = "https://localhost:5001/js/three.js", Method = "baz", Arguments = "", LineNumber = 1, ColumnNumber = 524 },
                }
            };

            yield return new object[]
            {
                @"   at Element.prototype.trigger (https://localhost:5001/js/one.js:1:4280)
   at r.setDate (https://localhost:5001/js/two.js:1:7448)
   at Anonymous function (https://localhost:5001/js/three.js:1:9766)",
                new List<StackFrame>
                {
                    new StackFrame { File = "https://localhost:5001/js/one.js", Method = "Element.prototype.trigger", Arguments = "", LineNumber = 1, ColumnNumber = 4280 },
                    new StackFrame { File = "https://localhost:5001/js/two.js", Method = "r.setDate", Arguments = "", LineNumber = 1, ColumnNumber = 7448 },
                    new StackFrame { File = "https://localhost:5001/js/three.js", Method = "", Arguments = "", LineNumber = 1, ColumnNumber = 9766 },
                },
            };
        }
    }
}
