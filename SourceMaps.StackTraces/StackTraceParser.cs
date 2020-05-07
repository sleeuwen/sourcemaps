using System;
using System.Text.RegularExpressions;

namespace SourceMaps.StackTraces
{
    public static class StackTraceParser
    {
        public static string ReTrace(SourceMap sourceMap, string stacktrace, string? sourceRoot = null)
        {
            var collection = new SourceMapCollection();
            collection.Register(sourceMap);
            return ReTrace(collection, stacktrace, sourceRoot);
        }

        public static string ReTrace(SourceMapCollection sourceMaps, string stacktrace, string? sourceRoot = null)
        {
            var trace = Parse(stacktrace);

            foreach (var frame in trace.Frames)
            {
                if (!string.IsNullOrEmpty(sourceRoot))
                    frame.File = frame.File?.Replace(sourceRoot, "");

                var sourceMap = sourceMaps.GetSourceMapFor(frame.File);
                if (sourceMap == null && frame.File?.IndexOf('?') >= 0) // Allow querystring versioning missing from the registered filename
                    sourceMap = sourceMaps.GetSourceMapFor(frame.File.Substring(0, frame.File.IndexOf('?')));

                if (frame.LineNumber == null || frame.ColumnNumber == null)
                    continue;

                var originalPosition = sourceMap?.OriginalPositionFor(frame.LineNumber.Value - 1, frame.ColumnNumber.Value - 1);
                if (originalPosition == null)
                    continue;

                frame.File = originalPosition?.OriginalFileName;
                frame.Method = originalPosition?.OriginalName;
                frame.LineNumber = originalPosition?.OriginalLineNumber + 1;
                frame.ColumnNumber = originalPosition?.OriginalColumnNumber + 1;
            }

            return trace.ToString();
        }

        public static StackTrace Parse(string stacktrace)
        {
            var lines = stacktrace.Split('\n');

            var result = new StackTrace();
            foreach (var line in lines)
            {
                StackFrame? frame;
                var success =
                    TryParseChrome(line, out frame) ||
                    TryParseWinJs(line, out frame) ||
                    TryParseGecko(line, out frame) ||
                    TryParseNode(line, out frame) ||
                    TryParseJsc(line, out frame);

                if (success)
                    result.Append(frame!);
            }

            return result;
        }

        private static readonly Regex ChromeRe = new Regex(@"^\s*at (.*?) ?\(((?:file|https?|blob|chrome-extension|native|eval|webpack|<anonymous>|\/).*?)(?::(\d+))?(?::(\d+))?\)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex ChromeEvalRe = new Regex(@"\((\S*)(?::(\d+))(?::(\d+))\)", RegexOptions.Compiled);
        internal static bool TryParseChrome(string line, out StackFrame? frame)
        {
            frame = null;

            var match = ChromeRe.Match(line);
            if (!match.Success)
                return false;

            var isNative = match.Groups[2].Value?.IndexOf("native", StringComparison.Ordinal) == 0;
            var isEval = match.Groups[2].Value?.IndexOf("eval", StringComparison.Ordinal) == 0;

            frame = new StackFrame
            {
                File = !isNative ? match.Groups[2].Value : null,
                Method = !string.IsNullOrEmpty(match.Groups[1]?.Value) ? match.Groups[1].Value : null,
                Arguments = isNative ? new[] { match.Groups[2].Value } : Array.Empty<string>(),
                LineNumber = !string.IsNullOrEmpty(match.Groups[3].Value) ? int.Parse(match.Groups[3].Value) : (int?) null,
                ColumnNumber = !string.IsNullOrEmpty(match.Groups[4].Value) ? int.Parse(match.Groups[4].Value) : (int?) null
            };

            var submatch = ChromeEvalRe.Match(match.Groups[2].Value);
            if (isEval && submatch.Success)
            {
                frame.File = !isNative ? submatch.Groups[1].Value : null;
                frame.LineNumber = !string.IsNullOrEmpty(submatch.Groups[2].Value) ? int.Parse(submatch.Groups[2].Value) : (int?)null;
                frame.ColumnNumber = !string.IsNullOrEmpty(submatch.Groups[3].Value) ? int.Parse(submatch.Groups[3].Value) : (int?)null;
            }

            return true;
        }

        private static readonly Regex WinJsRe = new Regex(@"^\s*at (?:((?:\[object object\])?.+) )?\(?((?:file|ms-appx|https?|webpack|blob):.*?):(\d+)(?::(\d+))?\)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        internal static bool TryParseWinJs(string line, out StackFrame? frame)
        {
            frame = null;

            var match = WinJsRe.Match(line);
            if (!match.Success)
                return false;

            frame = new StackFrame
            {
                File = match.Groups[2].Value,
                Method =  !string.IsNullOrEmpty(match.Groups[1]?.Value) ? match.Groups[1].Value : null,
                Arguments = Array.Empty<string>(),
                LineNumber = int.Parse(match.Groups[3].Value),
                ColumnNumber = !string.IsNullOrEmpty(match.Groups[4].Value) ? int.Parse(match.Groups[4].Value) : (int?)null,
            };

            return true;
        }

        private static readonly Regex GeckoRe = new Regex(@"^\s*(.*?)(?:\((.*?)\))?(?:^|@)((?:file|https?|blob|chrome|webpack|resource|\[native|/).*?|[^@]*bundle)(?::(\d+))?(?::(\d+))?\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex GeckoEvalRe = new Regex(@"(?:^\s*(?:.*?)(?:\((?:.*?)\))?@|^)(\S+) line (\d+)(?: > eval line \d+)* > eval", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        internal static bool TryParseGecko(string line, out StackFrame? frame)
        {
            frame = null;

            var match = GeckoRe.Match(line);
            if (!match.Success)
                return false;

            var isEval = match.Groups[3].Value.IndexOf("> eval", StringComparison.Ordinal) > -1;

            frame = new StackFrame
            {
                File = match.Groups[3].Value,
                Method =  !string.IsNullOrEmpty(match.Groups[1]?.Value) ? match.Groups[1].Value : null,
                Arguments = !string.IsNullOrEmpty(match.Groups[2].Value) ? match.Groups[2].Value.Split(',') : Array.Empty<string>(),
                LineNumber = !string.IsNullOrEmpty(match.Groups[4].Value) ? int.Parse(match.Groups[4].Value) : (int?)null,
                ColumnNumber = !string.IsNullOrEmpty(match.Groups[5].Value) ? int.Parse(match.Groups[5].Value) : (int?)null,
            };

            var submatch = GeckoEvalRe.Match(line);
            if (isEval && submatch.Success)
            {
                frame.File = submatch.Groups[1].Value;
                frame.LineNumber = int.Parse(submatch.Groups[2].Value);
                frame.ColumnNumber = null;
            }

            return true;
        }

        private static readonly Regex NodeRe = new Regex(@"^\s*at (?:((?:\[object object\])?[^\\/]+(?: \[as \S+\])?) )?\(?(.*?):(\d+)(?::(\d+))?\)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        internal static bool TryParseNode(string line, out StackFrame? frame)
        {
            frame = null;

            var match = NodeRe.Match(line);
            if (!match.Success)
                return false;

            frame = new StackFrame
            {
                File = match.Groups[2].Value,
                Method = !string.IsNullOrEmpty(match.Groups[1]?.Value) ? match.Groups[1].Value : null,
                Arguments = Array.Empty<string>(),
                LineNumber = int.Parse(match.Groups[3].Value),
                ColumnNumber = !string.IsNullOrEmpty(match.Groups[4].Value) ? int.Parse(match.Groups[4].Value) : (int?) null
            };

            return true;
        }

        private static readonly Regex JscRe = new Regex(@"^\s*(?:([^@]*)(?:\((.*?)\))?@)?(\S.*?):(\d+)(?::(\d+))?\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        internal static bool TryParseJsc(string line, out StackFrame? frame)
        {
            frame = null;

            var match = JscRe.Match(line);
            if (!match.Success)
                return false;

            frame = new StackFrame
            {
                File = match.Groups[3].Value,
                Method = !string.IsNullOrEmpty(match.Groups[1].Value) ? match.Groups[1].Value : null,
                Arguments = Array.Empty<string>(),
                LineNumber = int.Parse(match.Groups[4].Value),
                ColumnNumber = !string.IsNullOrEmpty(match.Groups[5].Value) ? int.Parse(match.Groups[5].Value) : (int?) null
            };

            return true;
        }
    }
}
