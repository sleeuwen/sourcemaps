using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace SourceMaps.StackTraces.Tests
{
    public class StackTraceParserTests
    {
        [Theory]
        [MemberData(nameof(StackTraceData))]
        public void ParseTests(string stacktrace, List<StackFrame> expected)
        {
            var actual = StackTraceParser.Parse(stacktrace).Frames;

            actual.Should().HaveSameCount(expected,
                "because parsed stackframes should be the same as actual stackframes");
            for (var i = 0; i < expected.Count; i++)
            {
                actual[i].File.Should().Be(expected[i].File, "because parsed file should be the same as expected");
                actual[i].Method.Should()
                    .Be(expected[i].Method, "because parsed method should be the same as expected");
                actual[i].Arguments.Should().Equal(expected[i].Arguments,
                    "because parsed arguments should be the same as expected");
                actual[i].LineNumber.Should().Be(expected[i].LineNumber,
                    "because parsed line number should be the same as expected");
                actual[i].ColumnNumber.Should().Be(expected[i].ColumnNumber,
                    "because parsed column number should be the same as expected");
            }
        }

        public static IEnumerable<object[]> StackTraceData()
        {
            // Firefox
            yield return new object[]
            {
                @"window.increment@https://localhost:5001/dist/site.js:1:95
throwErr@https://localhost:5001/dist/site.js:1:45
@https://localhost:5001/dist/site.js:1:524",
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "https://localhost:5001/dist/site.js", Method = "window.increment",
                        Arguments = Array.Empty<string>(), LineNumber = 1, ColumnNumber = 95
                    },
                    new StackFrame
                    {
                        File = "https://localhost:5001/dist/site.js", Method = "throwErr",
                        Arguments = Array.Empty<string>(), LineNumber = 1, ColumnNumber = 45
                    },
                    new StackFrame
                    {
                        File = "https://localhost:5001/dist/site.js", Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1, ColumnNumber = 524
                    },
                }
            };

            // Chromium
            yield return new object[]
            {
                @"TypeError: window.undefinedFunctionCall is not a function
    at window.increment (https://localhost:5001/dist/site.js:1:95)
    at throwErr (https://localhost:5001/dist/site.js:1:45)
    at https://localhost:5001/dist/site.js:1:524",
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "https://localhost:5001/dist/site.js", Method = "window.increment",
                        Arguments = Array.Empty<string>(), LineNumber = 1, ColumnNumber = 95
                    },
                    new StackFrame
                    {
                        File = "https://localhost:5001/dist/site.js", Method = "throwErr",
                        Arguments = Array.Empty<string>(), LineNumber = 1, ColumnNumber = 45
                    },
                    new StackFrame
                    {
                        File = "https://localhost:5001/dist/site.js", Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1, ColumnNumber = 524
                    },
                },
            };

            yield return new object[]
            {
                Helper.NodeSpace.Stack,

                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "C:\\\\project files\\\\spect\\\\src\\\\index.js",
                        Method = "Spect.get",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 161,
                        ColumnNumber = 26,
                    },
                    new StackFrame
                    {
                        File = "C:\\\\project files\\\\spect\\\\src\\\\index.js",
                        Method = "Object.get",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 43,
                        ColumnNumber = 36,
                    },
                    new StackFrame
                    {
                        File = "C:\\\\project files\\\\spect\\\\src\\\\index.js",
                        Method = "(anonymous function).then",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 165,
                        ColumnNumber = 33,
                    },
                    new StackFrame
                    {
                        File = "internal/process/task_queues.js",
                        Method = "process.runNextTicks [as _tickCallback]",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 52,
                        ColumnNumber = 5,
                    },
                    new StackFrame
                    {
                        File = "C:\\\\project files\\\\spect\\\\node_modules\\\\esm\\\\esm.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 34535,
                    },
                    new StackFrame
                    {
                        File = "C:\\\\project files\\\\spect\\\\node_modules\\\\esm\\\\esm.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 34176,
                    },
                    new StackFrame
                    {
                        File = "C:\\\\project files\\\\spect\\\\node_modules\\\\esm\\\\esm.js",
                        Method = "process.<anonymous>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 34506,
                    },
                    new StackFrame
                    {
                        File = "C:\\\\project files\\\\spect\\\\node_modules\\\\esm\\\\esm.js",
                        Method = "Function.<anonymous>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 296856,
                    },
                    new StackFrame
                    {
                        File = "C:\\\\project files\\\\spect\\\\node_modules\\\\esm\\\\esm.js",
                        Method = "Function.<anonymous>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 296555,
                    },
                },
            };
            yield return new object[]
            {
                Helper.IosReactNative1.Stack,

                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "/home/test/project/App.js",
                        Method = "_exampleFunction",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 125,
                        ColumnNumber = 13,
                    },
                    new StackFrame
                    {
                        File = "/home/test/project/node_modules/dep/index.js",
                        Method = "_depRunCallbacks",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 77,
                        ColumnNumber = 45,
                    },
                    new StackFrame
                    {
                        File = "/home/test/project/node_modules/react-native/node_modules/promise/lib/core.js",
                        Method = "tryCallTwo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 45,
                        ColumnNumber = 5,
                    },
                    new StackFrame
                    {
                        File = "/home/test/project/node_modules/react-native/node_modules/promise/lib/core.js",
                        Method = "doResolve",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 200,
                        ColumnNumber = 13,
                    },
                },
            };

            yield return new object[]
            {
                Helper.IosReactNative2.Stack,

                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "33.js",
                        Method = "s",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 531,
                    },
                    new StackFrame
                    {
                        File = "1959.js",
                        Method = "b",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 1469,
                    },
                    new StackFrame
                    {
                        File = "2932.js",
                        Method = "onSocketClose",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 727,
                    },
                    new StackFrame
                    {
                        File = "81.js",
                        Method = "value",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 1505,
                    },
                    new StackFrame
                    {
                        File = "102.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 2956,
                    },
                    new StackFrame
                    {
                        File = "89.js",
                        Method = "value",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 1247,
                    },
                    new StackFrame
                    {
                        File = "42.js",
                        Method = "value",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 3311,
                    },
                    new StackFrame
                    {
                        File = "42.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 822,
                    },
                    new StackFrame
                    {
                        File = "42.js",
                        Method = "value",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 2565,
                    },
                    new StackFrame
                    {
                        File = "42.js",
                        Method = "value",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 794,
                    },
                    new StackFrame
                    {
                        File = "[native code]",
                        Method = "value",
                        Arguments = Array.Empty<string>(),
                        LineNumber = null,
                        ColumnNumber = null,
                    },
                },
            };

            yield return new object[]
            {
                "global code@stack_traces/test:83:55",
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "stack_traces/test",
                        Method = "global code",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 83,
                        ColumnNumber = 55,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Safari6.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 48,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "dumpException3",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 52,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "onclick",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 82,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "[native code]",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = null,
                        ColumnNumber = null,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Safari7.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 48,
                        ColumnNumber = 22,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 52,
                        ColumnNumber = 15,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 108,
                        ColumnNumber = 107,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Safari8.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 47,
                        ColumnNumber = 22,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 52,
                        ColumnNumber = 15,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 108,
                        ColumnNumber = 23,
                    },
                },
            };

            yield return new object[]
            {
                // TODO: Take into account the line and column properties on the error object and use them for the first stack trace.
                Helper.Safari8Eval.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "[native code]",
                        Method = "eval",
                        Arguments = Array.Empty<string>(),
                        LineNumber = null,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 58,
                        ColumnNumber = 21,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 109,
                        ColumnNumber = 91,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Firefox3.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://127.0.0.1:8000/js/stacktrace.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 44,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://127.0.0.1:8000/js/stacktrace.js",
                        Method = "<unknown>",
                        Arguments = new string[] { "null" },
                        LineNumber = 31,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://127.0.0.1:8000/js/stacktrace.js",
                        Method = "printStackTrace",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 18,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://127.0.0.1:8000/js/file.js",
                        Method = "bar",
                        Arguments = new string[] { "1" },
                        LineNumber = 13,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://127.0.0.1:8000/js/file.js",
                        Method = "bar",
                        Arguments = new string[] { "2" },
                        LineNumber = 16,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://127.0.0.1:8000/js/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 20,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://127.0.0.1:8000/js/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 24,
                        ColumnNumber = null,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Firefox7.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "file:///G:/js/stacktrace.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 44,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "file:///G:/js/stacktrace.js",
                        Method = "<unknown>",
                        Arguments = new string[] { "null" },
                        LineNumber = 31,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "file:///G:/js/stacktrace.js",
                        Method = "printStackTrace",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 18,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "file:///G:/js/file.js",
                        Method = "bar",
                        Arguments = new string[] { "1" },
                        LineNumber = 13,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "file:///G:/js/file.js",
                        Method = "bar",
                        Arguments = new string[] { "2" },
                        LineNumber = 16,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "file:///G:/js/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 20,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "file:///G:/js/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 24,
                        ColumnNumber = null,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Firefox14.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 48,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "dumpException3",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 52,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "onclick",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = null,
                    },
                },
            };
            yield return new object[]
            {
                Helper.Firefox31.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 41,
                        ColumnNumber = 13,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 1,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = ".plugin/e.fn[c]/<",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 1,
                    },
                },
            };
            yield return new object[]
            {
                Helper.Firefox44NsException.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "[2]</Bar.prototype._baz/</<",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 703,
                        ColumnNumber = 28,
                    },
                    new StackFrame
                    {
                        File = "file:///path/to/file.js",
                        Method = "App.prototype.foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 15,
                        ColumnNumber = 2,
                    },
                    new StackFrame
                    {
                        File = "file:///path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 20,
                        ColumnNumber = 3,
                    },
                    new StackFrame
                    {
                        File = "file:///path/to/index.html",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 23,
                        ColumnNumber = 1,
                    },
                },
            };
            yield return new object[]
            {
                "error\n at Array.forEach (native)",
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = null,
                        Method = "Array.forEach",
                        Arguments = new string[] { "native" },
                        LineNumber = null,
                        ColumnNumber = null,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Chrome15.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 13,
                        ColumnNumber = 17,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 16,
                        ColumnNumber = 5,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 20,
                        ColumnNumber = 5,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 24,
                        ColumnNumber = 4,
                    },
                },
            };
            yield return new object[]
            {
                Helper.Chrome36.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "dumpExceptionError",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 41,
                        ColumnNumber = 27,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "HTMLButtonElement.onclick",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 107,
                        ColumnNumber = 146,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "I.e.fn.(anonymous function) [as index]",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 10,
                        ColumnNumber = 3651,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Chrome76.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "<anonymous>",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 8,
                        ColumnNumber = 9,
                    },
                    new StackFrame
                    {
                        File = "<anonymous>",
                        Method = "async foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 2,
                        ColumnNumber = 3,
                    },
                },
            };
            yield return new object[]
            {
                Helper.ChromeXxWebpack.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "webpack:///./src/components/test/test.jsx?",
                        Method = "TESTTESTTEST.eval",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 295,
                        ColumnNumber = 108,
                    },
                    new StackFrame
                    {
                        File = "webpack:///./src/components/test/test.jsx?",
                        Method = "TESTTESTTEST.render",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 272,
                        ColumnNumber = 32,
                    },
                    new StackFrame
                    {
                        File = "webpack:///./~/react-transform-catch-errors/lib/index.js?",
                        Method = "TESTTESTTEST.tryRender",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 34,
                        ColumnNumber = 31,
                    },
                    new StackFrame
                    {
                        File = "webpack:///./~/react-proxy/modules/createPrototypeProxy.js?",
                        Method = "TESTTESTTEST.proxiedMethod",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 44,
                        ColumnNumber = 30,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Chrome48Eval.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "baz",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 21,
                        ColumnNumber = 17,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 21,
                        ColumnNumber = 17,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "eval",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 21,
                        ColumnNumber = 17,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "Object.speak",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 21,
                        ColumnNumber = 17,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 31,
                        ColumnNumber = 13,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Chrome48Blob.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = null,
                        Method = "Error",
                        Arguments = Array.Empty<string>(),
                        LineNumber = null,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379",
                        Method = "s",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 31,
                        ColumnNumber = 29146,
                    },
                    new StackFrame
                    {
                        File = "blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379",
                        Method = "Object.d [as add]",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 31,
                        ColumnNumber = 30039,
                    },
                    new StackFrame
                    {
                        File = "blob:http%3A//localhost%3A8080/d4eefe0f-361a-4682-b217-76587d9f712a",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 15,
                        ColumnNumber = 10978,
                    },
                    new StackFrame
                    {
                        File = "blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 6911,
                    },
                    new StackFrame
                    {
                        File = "blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379",
                        Method = "n.fire",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 7,
                        ColumnNumber = 3019,
                    },
                    new StackFrame
                    {
                        File = "blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379",
                        Method = "n.handle",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 7,
                        ColumnNumber = 2863,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Ie10.Stack,
                new List<StackFrame>
                {
                    // TODO: func should be normalized
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "Anonymous function",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 48,
                        ColumnNumber = 13,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 46,
                        ColumnNumber = 9,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 82,
                        ColumnNumber = 1,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Ie11.Stack,
                new List<StackFrame>
                {
                    // TODO: func should be normalized
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "Anonymous function",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 47,
                        ColumnNumber = 21,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 45,
                        ColumnNumber = 13,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 108,
                        ColumnNumber = 1,
                    },
                },
            };
            yield return new object[]
            {
                Helper.Ie11Eval.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "eval code",
                        Method = "eval code",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 1,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 58,
                        ColumnNumber = 17,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 109,
                        ColumnNumber = 1,
                    },
                },
            };
            yield return new object[]
            {
                Helper.Opera25.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 47,
                        ColumnNumber = 22,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 52,
                        ColumnNumber = 15,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "bar",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 108,
                        ColumnNumber = 168,
                    },
                },
            };
            yield return new object[]
            {
                Helper.Phantomjs119.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "file:///path/to/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 878,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 4283,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://path/to/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 4287,
                        ColumnNumber = null,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Firefox50ResourceUrl.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "resource://path/data/content/bundle.js",
                        Method = "render",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 5529,
                        ColumnNumber = 16,
                    },
                },
            };
            yield return new object[]
            {
                Helper.Firefox43Eval.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "baz",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 26,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "foo",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 26,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 26,
                        ColumnNumber = null,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "speak",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 26,
                        ColumnNumber = 17,
                    },
                    new StackFrame
                    {
                        File = "http://localhost:8080/file.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 33,
                        ColumnNumber = 9,
                    },
                },
            };
            yield return new object[]
            {
                Helper.AndroidReactNative.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File =
                            "/home/username/sample-workspace/sampleapp.collect.react/src/components/GpsMonitorScene.js",
                        Method = "render",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 78,
                        ColumnNumber = 24,
                    },
                    new StackFrame
                    {
                        File =
                            "/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactCompositeComponent.js",
                        Method = "_renderValidatedComponentWithoutOwnerOrContext",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1050,
                        ColumnNumber = 29,
                    },
                    new StackFrame
                    {
                        File =
                            "/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactCompositeComponent.js",
                        Method = "_renderValidatedComponent",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1075,
                        ColumnNumber = 15,
                    },
                    new StackFrame
                    {
                        File =
                            "/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactCompositeComponent.js",
                        Method = "renderedElement", Arguments = Array.Empty<string>(), LineNumber = 484,
                        ColumnNumber = 29
                    },
                    new StackFrame
                    {
                        File =
                            "/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactCompositeComponent.js",
                        Method = "_currentElement", Arguments = Array.Empty<string>(), LineNumber = 346,
                        ColumnNumber = 40
                    },
                    new StackFrame
                    {
                        File =
                            "/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactReconciler.js",
                        Method = "child", Arguments = Array.Empty<string>(), LineNumber = 68, ColumnNumber = 25
                    },
                    new StackFrame
                    {
                        File =
                            "/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactMultiChild.js",
                        Method = "children", Arguments = Array.Empty<string>(), LineNumber = 264, ColumnNumber = 10
                    },
                    new StackFrame
                    {
                        File =
                            "/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/native/ReactNativeBaseComponent.js",
                        Method = "this",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 74,
                        ColumnNumber = 41,
                    },
                },
            };
            yield return new object[]
            {
                Helper.AndroidReactNativeProd.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "index.android.bundle",
                        Method = "value",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 12,
                        ColumnNumber = 1917,
                    },
                    new StackFrame
                    {
                        File = "index.android.bundle",
                        Method = "value",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 29,
                        ColumnNumber = 927,
                    },
                    new StackFrame
                    {
                        File = "[native code]",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = null,
                        ColumnNumber = null,
                    },
                },
            };

            yield return new object[]
            {
                Helper.Node12.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "/home/xyz/hack/asyncnode.js",
                        Method = "promiseMe",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 11,
                        ColumnNumber = 9,
                    },
                    new StackFrame
                    {
                        File = "/home/xyz/hack/asyncnode.js",
                        Method = "async main",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 15,
                        ColumnNumber = 13,
                    },
                },
            };

            yield return new object[]
            {
                Helper.NodeAnonym.Stack,
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "C:\\\\projects\\\\spect\\\\src\\\\index.js",
                        Method = "Spect.get",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 161,
                        ColumnNumber = 26,
                    },
                    new StackFrame
                    {
                        File = "C:\\projects\\spect\\src\\index.js", Method = "Object.get",
                        Arguments = Array.Empty<string>(), LineNumber = 43, ColumnNumber = 36
                    },
                    new StackFrame
                    {
                        File = null, Method = "<anonymous>",
                        Arguments = Array.Empty<string>(), LineNumber = null, ColumnNumber = null
                    },
                    new StackFrame
                    {
                        File = "C:\\\\projects\\\\spect\\\\src\\\\index.js",
                        Method = "(anonymous function).then",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 165,
                        ColumnNumber = 33,
                    },
                    new StackFrame
                    {
                        File = "internal/process/task_queues.js", Method = "process.runNextTicks [as _tickCallback]",
                        Arguments = Array.Empty<string>(), LineNumber = 52, ColumnNumber = 5
                    },
                    new StackFrame
                    {
                        File = "C:\\\\projects\\\\spect\\\\node_modules\\\\esm\\\\esm.js",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 34535,
                    },
                    new StackFrame
                    {
                        File = "C:\\projects\\spect\\node_modules\\esm\\esm.js", Method = "<unknown>",
                        Arguments = Array.Empty<string>(), LineNumber = 1, ColumnNumber = 34176
                    },
                    new StackFrame
                    {
                        File = "C:\\\\projects\\\\spect\\\\node_modules\\\\esm\\\\esm.js",
                        Method = "process.<anonymous>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 34506,
                    },
                    new StackFrame
                    {
                        File = "C:\\projects\\spect\\node_modules\\esm\\esm.js", Method = "Function.<anonymous>",
                        Arguments = Array.Empty<string>(), LineNumber = 1, ColumnNumber = 296856
                    },
                    new StackFrame
                    {
                        File = "C:\\projects\\spect\\node_modules\\esm\\esm.js", Method = "Function.<anonymous>",
                        Arguments = Array.Empty<string>(), LineNumber = 1, ColumnNumber = 296555
                    },
                },
            };

            yield return new object[]
            {
                @"x
                at new <anonymous> (http://www.example.com/test.js:2:1
                at <anonymous>:1:2",
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "http://www.example.com/test.js",
                        Method = "new <anonymous>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 2,
                        ColumnNumber = 1,
                    },
                    new StackFrame
                    {
                        File = "<anonymous>",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 2,
                    },
                },
            };

            yield return new object[]
            {
                @"ReferenceError: test is not defined
                at repl:1:2
                at REPLServer.self.eval (repl.js:110:21)
                at Interface.<anonymous> (repl.js:239:12)
                at Interface.EventEmitter.emit (events.js:95:17)
                at emitKey (readline.js:1095:12)",
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "repl",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 2,
                    },
                    new StackFrame
                    {
                        File = "repl.js",
                        Method = "REPLServer.self.eval",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 110,
                        ColumnNumber = 21,
                    },
                    new StackFrame
                    {
                        File = "repl.js",
                        Method = "Interface.<anonymous>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 239,
                        ColumnNumber = 12,
                    },
                    new StackFrame
                    {
                        File = "events.js",
                        Method = "Interface.EventEmitter.emit",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 95,
                        ColumnNumber = 17,
                    },
                    new StackFrame
                    {
                        File = "readline.js",
                        Method = "emitKey",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1095,
                        ColumnNumber = 12,
                    },
                },
            };

            yield return new object[]
            {
                @"ReferenceError: breakDown is not defined
                    at null._onTimeout (repl:1:25)
                    at Timer.listOnTimeout [as ontimeout] (timers.js:110:15)",
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "repl",
                        Method = "null._onTimeout",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 25,
                    },
                    new StackFrame
                    {
                        File = "timers.js",
                        Method = "Timer.listOnTimeout [as ontimeout]",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 110,
                        ColumnNumber = 15,
                    },
                },
            };

            yield return new object[]
            {
                @"ReferenceError: test is not defined
                at repl:1:1
                at REPLServer.defaultEval (repl.js:154:27)
                at bound (domain.js:254:14)
                at REPLServer.runBound [as eval] (domain.js:267:12)
                at REPLServer.<anonymous> (repl.js:308:12)
                at emitOne (events.js:77:13)
                at REPLServer.emit (events.js:169:7)
                at REPLServer.Interface._onLine (readline.js:210:10)
                at REPLServer.Interface._line (readline.js:549:8)
                at REPLServer.Interface._ttyWrite (readline.js:826:14)",
                new List<StackFrame>
                {
                    new StackFrame
                    {
                        File = "repl",
                        Method = "<unknown>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 1,
                        ColumnNumber = 1,
                    },
                    new StackFrame
                    {
                        File = "repl.js",
                        Method = "REPLServer.defaultEval",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 154,
                        ColumnNumber = 27,
                    },
                    new StackFrame
                    {
                        File = "domain.js",
                        Method = "bound",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 254,
                        ColumnNumber = 14,
                    },
                    new StackFrame
                    {
                        File = "domain.js",
                        Method = "REPLServer.runBound [as eval]",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 267,
                        ColumnNumber = 12,
                    },
                    new StackFrame
                    {
                        File = "repl.js",
                        Method = "REPLServer.<anonymous>",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 308,
                        ColumnNumber = 12,
                    },
                    new StackFrame
                    {
                        File = "events.js",
                        Method = "emitOne",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 77,
                        ColumnNumber = 13,
                    },
                    new StackFrame
                    {
                        File = "events.js",
                        Method = "REPLServer.emit",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 169,
                        ColumnNumber = 7,
                    },
                    new StackFrame
                    {
                        File = "readline.js",
                        Method = "REPLServer.Interface._onLine",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 210,
                        ColumnNumber = 10,
                    },
                    new StackFrame
                    {
                        File = "readline.js",
                        Method = "REPLServer.Interface._line",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 549,
                        ColumnNumber = 8,
                    },
                    new StackFrame
                    {
                        File = "readline.js",
                        Method = "REPLServer.Interface._ttyWrite",
                        Arguments = Array.Empty<string>(),
                        LineNumber = 826,
                        ColumnNumber = 14,
                    },
                },
            };
        }
    }
}
