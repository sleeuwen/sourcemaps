namespace SourceMaps.StackTraces.Tests
{
    internal static class Helper
    {
        public static readonly JavaScriptError Node12 = new JavaScriptError
        {
            Message = "Just an Exception",
            Name = "Error",
            Stack =
                "Error: Just an Exception\n" +
                "    at promiseMe (/home/xyz/hack/asyncnode.js:11:9)\n" +
                "    at async main (/home/xyz/hack/asyncnode.js:15:13)",
        };

        public static readonly JavaScriptError NodeAnonym = new JavaScriptError
        {
            Message = "",
            Name = "Error",
            Stack = @"Error
            at Spect.get (C:\\projects\\spect\\src\\index.js:161:26)
            at Object.get (C:\\projects\\spect\\src\\index.js:43:36)
            at <anonymous>
            at (anonymous function).then (C:\\projects\\spect\\src\\index.js:165:33)
            at process.runNextTicks [as _tickCallback] (internal/process/task_queues.js:52:5)
            at C:\\projects\\spect\\node_modules\\esm\\esm.js:1:34535
            at C:\\projects\\spect\\node_modules\\esm\\esm.js:1:34176
            at process.<anonymous> (C:\\projects\\spect\\node_modules\\esm\\esm.js:1:34506)
            at Function.<anonymous> (C:\\projects\\spect\\node_modules\\esm\\esm.js:1:296856)
            at Function.<anonymous> (C:\\projects\\spect\\node_modules\\esm\\esm.js:1:296555)",
        };

        public static readonly JavaScriptError NodeSpace = new JavaScriptError
        {
            Message = "",
            Name = "Error",
            Stack = @"Error
            at Spect.get (C:\\project files\\spect\\src\\index.js:161:26)
            at Object.get (C:\\project files\\spect\\src\\index.js:43:36)
            at <anonymous>
            at (anonymous function).then (C:\\project files\\spect\\src\\index.js:165:33)
            at process.runNextTicks [as _tickCallback] (internal/process/task_queues.js:52:5)
            at C:\\project files\\spect\\node_modules\\esm\\esm.js:1:34535
            at C:\\project files\\spect\\node_modules\\esm\\esm.js:1:34176
            at process.<anonymous> (C:\\project files\\spect\\node_modules\\esm\\esm.js:1:34506)
            at Function.<anonymous> (C:\\project files\\spect\\node_modules\\esm\\esm.js:1:296856)
            at Function.<anonymous> (C:\\project files\\spect\\node_modules\\esm\\esm.js:1:296555)",
        };

        public static readonly JavaScriptError Opera25 = new JavaScriptError
        {
            Message = "Cannot read property \"undef\" of null",
            Name = "TypeError",
            Stack =
                "TypeError: Cannot read property \"undef\" of null\n" +
                "    at http://path/to/file.js:47:22\n" +
                "    at foo (http://path/to/file.js:52:15)\n" +
                "    at bar (http://path/to/file.js:108:168)",
        };

        public static readonly JavaScriptError Chrome15 = new JavaScriptError
        {
            Arguments = new string[] { "undef" },
            Message = "Object #<Object> has no method \"undef\"",
            Stack = "TypeError: Object #<Object> has no method \"undef\"\n" +
                    "    at bar (http://path/to/file.js:13:17)\n" +
                    "    at bar (http://path/to/file.js:16:5)\n" +
                    "    at foo (http://path/to/file.js:20:5)\n" +
                    "    at http://path/to/file.js:24:4",
        };

        public static readonly JavaScriptError Chrome36 = new JavaScriptError
        {
            Message = "Default error",
            Name = "Error",
            Stack =
                "Error: Default error\n" +
                "    at dumpExceptionError (http://localhost:8080/file.js:41:27)\n" +
                "    at HTMLButtonElement.onclick (http://localhost:8080/file.js:107:146)\n" +
                "    at I.e.fn.(anonymous function) [as index] (http://localhost:8080/file.js:10:3651)",
        };

        public static readonly JavaScriptError Chrome76 = new JavaScriptError
        {
            Message = "BEEP BEEP",
            Name = "Error",
            Stack =
                "Error: BEEP BEEP\n" +
                "    at bar (<anonymous>:8:9)\n" +
                "    at async foo (<anonymous>:2:3)",
        };

        // can be generated when Webpack is built with { devtool: eval }
        public static readonly JavaScriptError ChromeXxWebpack = new JavaScriptError
        {
            Message = "Cannot read property \"error\" of undefined",
            Name = "TypeError",
            Stack =
                "TypeError: Cannot read property \"error\" of undefined\n" +
                "   at TESTTESTTEST.eval(webpack:///./src/components/test/test.jsx?:295:108)\n" +
                "   at TESTTESTTEST.render(webpack:///./src/components/test/test.jsx?:272:32)\n" +
                "   at TESTTESTTEST.tryRender(webpack:///./~/react-transform-catch-errors/lib/index.js?:34:31)\n" +
                "   at TESTTESTTEST.proxiedMethod(webpack:///./~/react-proxy/modules/createPrototypeProxy.js?:44:30)",
        };

        public static readonly JavaScriptError Firefox3 = new JavaScriptError
        {
            FileName = "http://127.0.0.1:8000/js/stacktrace.js",
            LineNumber = 44,
            Message = "this.undef is not a function",
            Name = "TypeError",
            Stack =
                "()@http://127.0.0.1:8000/js/stacktrace.js:44\n" +
                "(null)@http://127.0.0.1:8000/js/stacktrace.js:31\n" +
                "printStackTrace()@http://127.0.0.1:8000/js/stacktrace.js:18\n" +
                "bar(1)@http://127.0.0.1:8000/js/file.js:13\n" +
                "bar(2)@http://127.0.0.1:8000/js/file.js:16\n" +
                "foo()@http://127.0.0.1:8000/js/file.js:20\n" +
                "@http://127.0.0.1:8000/js/file.js:24\n",
        };

        public static readonly JavaScriptError Firefox7 = new JavaScriptError
        {
            FileName = "file:///G:/js/stacktrace.js",
            LineNumber = 44,
            Stack =
                "()@file:///G:/js/stacktrace.js:44\n" +
                "(null)@file:///G:/js/stacktrace.js:31\n" +
                "printStackTrace()@file:///G:/js/stacktrace.js:18\n" +
                "bar(1)@file:///G:/js/file.js:13\n" +
                "bar(2)@file:///G:/js/file.js:16\n" +
                "foo()@file:///G:/js/file.js:20\n" +
                "@file:///G:/js/file.js:24\n",
        };

        public static readonly JavaScriptError Firefox14 = new JavaScriptError
        {
            Message = "x is null",
            Stack =
                "@http://path/to/file.js:48\n" +
                "dumpException3@http://path/to/file.js:52\n" +
                "onclick@http://path/to/file.js:1\n",
            FileName = "http://path/to/file.js",
            LineNumber = 48,
        };

        public static readonly JavaScriptError Firefox31 = new JavaScriptError
        {
            Message = "Default error",
            Name = "Error",
            Stack =
                "foo@http://path/to/file.js:41:13\n" +
                "bar@http://path/to/file.js:1:1\n" +
                ".plugin/e.fn[c]/<@http://path/to/file.js:1:1\n",
            FileName = "http://path/to/file.js",
            LineNumber = 41,
            ColumnNumber = 12,
        };

        public static readonly JavaScriptError Firefox43Eval = new JavaScriptError
        {
            ColumnNumber = 30,
            FileName = "http://localhost:8080/file.js line 25 > eval line 2 > eval",
            LineNumber = 1,
            Message = "message string",
            Stack =
                "baz@http://localhost:8080/file.js line 26 > eval line 2 > eval:1:30\n" +
                "foo@http://localhost:8080/file.js line 26 > eval:2:96\n" +
                "@http://localhost:8080/file.js line 26 > eval:4:18\n" +
                "speak@http://localhost:8080/file.js:26:17\n" +
                "@http://localhost:8080/file.js:33:9",
        };

        // Internal errors sometimes thrown by Firefox
        // More here: https://developer.mozilla.org/en-US/docs/Mozilla/Errors
        //
        // Note that such errors are instanceof "Exception", not "Error"
        public static readonly JavaScriptError Firefox44NsException = new JavaScriptError
        {
            Message = "",
            Name = "NS_ERROR_FAILURE",
            Stack =
                "[2]</Bar.prototype._baz/</<@http://path/to/file.js:703:28\n" +
                "App.prototype.foo@file:///path/to/file.js:15:2\n" +
                "bar@file:///path/to/file.js:20:3\n" +
                "@file:///path/to/index.html:23:1\n", // inside <script> tag
            FileName = "http://path/to/file.js",
            ColumnNumber = 0,
            LineNumber = 703,
            Result = 2147500037,
        };

        public static readonly JavaScriptError Firefox50ResourceUrl = new JavaScriptError
        {
            Stack =
                "render@resource://path/data/content/bundle.js:5529:16\n" +
                "dispatchEvent@resource://path/data/content/vendor.bundle.js:18:23028\n" +
                "wrapped@resource://path/data/content/bundle.js:7270:25",
            FileName = "resource://path/data/content/bundle.js",
            LineNumber = 5529,
            ColumnNumber = 16,
            Message = "this.props.raw[this.state.dataSource].rows is undefined",
            Name = "TypeError",
        };

        public static readonly JavaScriptError Safari6 = new JavaScriptError
        {
            Message = "\"null\" is not an object (evaluating \"x.undef\")",
            Stack =
                "@http://path/to/file.js:48\n" +
                "dumpException3@http://path/to/file.js:52\n" +
                "onclick@http://path/to/file.js:82\n" +
                "[native code]",
            Line = 48,
            SourceUrl = "http://path/to/file.js",
        };

        public static readonly JavaScriptError Safari7 = new JavaScriptError
        {
            Message = "\"null\" is not an object (evaluating \"x.undef\")",
            Name = "TypeError",
            Stack =
                "http://path/to/file.js:48:22\n" +
                "foo@http://path/to/file.js:52:15\n" +
                "bar@http://path/to/file.js:108:107",
            Line = 47,
            SourceUrl = "http://path/to/file.js",
        };

        public static readonly JavaScriptError Safari8 = new JavaScriptError
        {
            Message = "null is not an object (evaluating \"x.undef\")",
            Name = "TypeError",
            Stack =
                "http://path/to/file.js:47:22\n" +
                "foo@http://path/to/file.js:52:15\n" +
                "bar@http://path/to/file.js:108:23",
            Line = 47,
            Column = 22,
            SourceUrl = "http://path/to/file.js",
        };

        public static readonly JavaScriptError Safari8Eval = new JavaScriptError
        {
            Message = "Can\"t find variable: getExceptionProps",
            Name = "ReferenceError",
            Stack =
                "eval code\n" +
                "eval@[native code]\n" +
                "foo@http://path/to/file.js:58:21\n" +
                "bar@http://path/to/file.js:109:91",
            Line = 1,
            Column = 18,
        };

        public static readonly JavaScriptError Ie10 = new JavaScriptError
        {
            Message = "Unable to get property \"undef\" of undefined or null reference",
            Stack =
                "TypeError: Unable to get property \"undef\" of undefined or null reference\n" +
                "   at Anonymous function (http://path/to/file.js:48:13)\n" +
                "   at foo (http://path/to/file.js:46:9)\n" +
                "   at bar (http://path/to/file.js:82:1)",
            Description =
                "Unable to get property \"undef\" of undefined or null reference",
            Number = -2146823281,
        };

        public static readonly JavaScriptError Ie11 = new JavaScriptError
        {
            Message = "Unable to get property \"undef\" of undefined or null reference",
            Name = "TypeError",
            Stack =
                "TypeError: Unable to get property \"undef\" of undefined or null reference\n" +
                "   at Anonymous function (http://path/to/file.js:47:21)\n" +
                "   at foo (http://path/to/file.js:45:13)\n" +
                "   at bar (http://path/to/file.js:108:1)",
            Description =
                "Unable to get property \"undef\" of undefined or null reference",
            Number = -2146823281,
        };

        public static readonly JavaScriptError Ie11Eval = new JavaScriptError
        {
            Message = "\"getExceptionProps\" is undefined",
            Name = "ReferenceError",
            Stack =
                "ReferenceError: \"getExceptionProps\" is undefined\n" +
                "   at eval code (eval code:1:1)\n" +
                "   at foo (http://path/to/file.js:58:17)\n" +
                "   at bar (http://path/to/file.js:109:1)",
            Description = "\"getExceptionProps\" is undefined",
            Number = -2146823279,
        };

        public static readonly JavaScriptError Chrome48Blob = new JavaScriptError
        {
            Message = "Error: test",
            Name = "Error",
            Stack =
                "Error: test\n" +
                "    at Error (native)\n" +
                "    at s (blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379:31:29146)\n" +
                "    at Object.d [as add] (blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379:31:30039)\n" +
                "    at blob:http%3A//localhost%3A8080/d4eefe0f-361a-4682-b217-76587d9f712a:15:10978\n" +
                "    at blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379:1:6911\n" +
                "    at n.fire (blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379:7:3019)\n" +
                "    at n.handle (blob:http%3A//localhost%3A8080/abfc40e9-4742-44ed-9dcd-af8f99a29379:7:2863)",
        };

        public static readonly JavaScriptError Chrome48Eval = new JavaScriptError
        {
            Message = "message string",
            Name = "Error",
            Stack =
                "Error: message string\n" +
                "at baz (eval at foo (eval at speak (http://localhost:8080/file.js:21:17)), <anonymous>:1:30)\n" +
                "at foo (eval at speak (http://localhost:8080/file.js:21:17), <anonymous>:2:96)\n" +
                "at eval (eval at speak (http://localhost:8080/file.js:21:17), <anonymous>:4:18)\n" +
                "at Object.speak (http://localhost:8080/file.js:21:17)\n" +
                "at http://localhost:8080/file.js:31:13\n",
        };

        public static readonly JavaScriptError Phantomjs119 = new JavaScriptError
        {
            Stack =
                "Error: foo\n" +
                "    at file:///path/to/file.js:878\n" +
                "    at foo (http://path/to/file.js:4283)\n" +
                "    at http://path/to/file.js:4287",
        };

        public static readonly JavaScriptError AndroidReactNative = new JavaScriptError
        {
            Message = "Error: test",
            Name = "Error",
            Stack =
                "Error: test\n" +
                "at render(/home/username/sample-workspace/sampleapp.collect.react/src/components/GpsMonitorScene.js:78:24)\n" +
                "at _renderValidatedComponentWithoutOwnerOrContext(/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactCompositeComponent.js:1050:29)\n" +
                "at _renderValidatedComponent(/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactCompositeComponent.js:1075:15)\n" +
                "at renderedElement(/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactCompositeComponent.js:484:29)\n" +
                "at _currentElement(/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactCompositeComponent.js:346:40)\n" +
                "at child(/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactReconciler.js:68:25)\n" +
                "at children(/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/shared/stack/reconciler/ReactMultiChild.js:264:10)\n" +
                "at this(/home/username/sample-workspace/sampleapp.collect.react/node_modules/react-native/Libraries/Renderer/src/renderers/native/ReactNativeBaseComponent.js:74:41)\n",
        };

        public static readonly JavaScriptError AndroidReactNativeProd = new JavaScriptError
        {
            Message = "Error: test",
            Name = "Error",
            Stack =
                "value@index.android.bundle:12:1917\n" +
                "onPress@index.android.bundle:12:2336\n" +
                "touchableHandlePress@index.android.bundle:258:1497\n" +
                "[native code]\n" +
                "_performSideEffectsForTransition@index.android.bundle:252:8508\n" +
                "[native code]\n" +
                "_receiveSignal@index.android.bundle:252:7291\n" +
                "[native code]\n" +
                "touchableHandleResponderRelease@index.android.bundle:252:4735\n" +
                "[native code]\n" +
                "u@index.android.bundle:79:142\n" +
                "invokeGuardedCallback@index.android.bundle:79:459\n" +
                "invokeGuardedCallbackAndCatchFirstError@index.android.bundle:79:580\n" +
                "c@index.android.bundle:95:365\n" +
                "a@index.android.bundle:95:567\n" +
                "v@index.android.bundle:146:501\n" +
                "g@index.android.bundle:146:604\n" +
                "forEach@[native code]\n" +
                "i@index.android.bundle:149:80\n" +
                "processEventQueue@index.android.bundle:146:1432\n" +
                "s@index.android.bundle:157:88\n" +
                "handleTopLevel@index.android.bundle:157:174\n" +
                "index.android.bundle:156:572\n" +
                "a@index.android.bundle:93:276\n" +
                "c@index.android.bundle:93:60\n" +
                "perform@index.android.bundle:177:596\n" +
                "batchedUpdates@index.android.bundle:188:464\n" +
                "i@index.android.bundle:176:358\n" +
                "i@index.android.bundle:93:90\n" +
                "u@index.android.bundle:93:150\n" +
                "_receiveRootNodeIDEvent@index.android.bundle:156:544\n" +
                "receiveTouches@index.android.bundle:156:918\n" +
                "value@index.android.bundle:29:3016\n" +
                "index.android.bundle:29:955\n" +
                "value@index.android.bundle:29:2417\n" +
                "value@index.android.bundle:29:927\n" +
                "[native code]",
        };

        public static readonly JavaScriptError IosReactNative1 = new JavaScriptError
        {
            Message = "Error: from issue #11",
            Stack = @"
            _exampleFunction@/home/test/project/App.js:125:13
            _depRunCallbacks@/home/test/project/node_modules/dep/index.js:77:45
            tryCallTwo@/home/test/project/node_modules/react-native/node_modules/promise/lib/core.js:45:5
            doResolve@/home/test/project/node_modules/react-native/node_modules/promise/lib/core.js:200:13
            ",
        };

        public static readonly JavaScriptError IosReactNative2 = new JavaScriptError
        {
            Message = "Error: from issue https://github.com/facebook/react-native/issues/24382#issuecomment-489404970",
            Stack = "s@33.js:1:531\n" +
                    "b@1959.js:1:1469\n" +
                    "onSocketClose@2932.js:1:727\n" +
                    "value@81.js:1:1505\n" +
                    "102.js:1:2956\n" +
                    "value@89.js:1:1247\n" +
                    "value@42.js:1:3311\n" +
                    "42.js:1:822\n" +
                    "value@42.js:1:2565\n" +
                    "value@42.js:1:794\n" +
                    "value@[native code]",
        };

        internal class JavaScriptError
        {
            public string Message { get; set; }
            public string Name { get; set; }

            public string Stack { get; set; }

            // Microsoft
            public string Description { get; set; }
            public long Number { get; set; }

            // Mozilla
            public string FileName { get; set; }
            public int LineNumber { get; set; }
            public int ColumnNumber { get; set; }

            public long Result { get; set; }

            // Chrome
            public string[] Arguments { get; set; }

            // Safari
            public string SourceUrl { get; set; }
            public int Line { get; set; }
            public int Column { get; set; }
        }
    }
}
