using System.Text.RegularExpressions;

namespace SourceMaps.StackTraces
{
    public static class StackTraceParser
    {
        public static string ReTrace(SourceMapCollection sourceMaps, string stacktrace, string sourceRoot = null)
        {
            var trace = Parse(stacktrace);

            for (var i = 0; i < trace.Frames.Count; i++)
            {
                var frame = trace.Frames[i];

                frame.File = frame.File.Replace(sourceRoot, "");

                var sourceMap = sourceMaps.GetSourceMapFor(frame.File)
                                ?? sourceMaps.GetSourceMapFor(frame.File.Substring(0, frame.File.IndexOf('?')));

                var originalPosition = sourceMap?.OriginalPositionFor(new SourcePosition(frame.LineNumber.Value - 1, frame.ColumnNumber.Value - 1));
                if (originalPosition == null)
                    continue;

                frame.File = originalPosition?.OriginalFileName;
                frame.Method = originalPosition?.OriginalName;
                frame.LineNumber = originalPosition?.OriginalSourcePosition.LineNumber;
                frame.ColumnNumber = originalPosition?.OriginalSourcePosition.ColumnNumber;
            }

            return trace.ToString();
        }

        public static StackTrace Parse(string stacktrace)
        {
            var lines = stacktrace.Split('\n');

            var result = new StackTrace();
            foreach (var line in lines)
            {
                StackFrame frame;
                var success =
                    TryParseChrome(line, out frame) ||
                    TryParseWinJs(line, out frame) ||
                    TryParseGecko(line, out frame);

                if (success)
                    result.Append(frame);
            }

            return result;
        }

        private static readonly Regex ChromeRe = new Regex(@"^\s*at (.*?) ?\(((?:file|https?|blob|chrome-extension|native|eval|webpack|<anonymous>|\/).*?)(?::(\d+))?(?::(\d+))?\)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex ChromeEvalRe = new Regex(@"\((\S*)(?::(\d+))(?::(\d+))\)", RegexOptions.Compiled);
        internal static bool TryParseChrome(string line, out StackFrame frame)
        {
            frame = null;

            var match = ChromeRe.Match(line);
            if (!match.Success)
                return false;

            var isNative = match.Groups[2].Value?.IndexOf("native") == 0;
            var isEval = match.Groups[2].Value?.IndexOf("eval") == 0;

            frame = new StackFrame();
            frame.File = !isNative ? match.Groups[2].Value : null;
            frame.Method = match.Groups[1]?.Value ?? "<unknown>";
            frame.Arguments = match.Groups[2].Value;
            frame.LineNumber = int.Parse(match.Groups[3].Value);
            frame.ColumnNumber = int.Parse(match.Groups[4].Value);

            var submatch = ChromeEvalRe.Match(match.Groups[2].Value);
            if (submatch.Success)
            {
                frame.File = !isNative ? submatch.Groups[1].Value : null;
                frame.Arguments = submatch.Groups[1].Value;
                frame.LineNumber = int.Parse(submatch.Groups[2].Value);
                frame.ColumnNumber = int.Parse(submatch.Groups[3].Value);
            }

            return true;
        }

        private static readonly Regex WinJsRe = new Regex(@"^\s*at (?:((?:\[object object\])?.+) )?\(?((?:file|ms-appx|https?|webpack|blob):.*?):(\d+)(?::(\d+))?\)?\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        internal static bool TryParseWinJs(string line, out StackFrame frame)
        {
            frame = null;

            var match = WinJsRe.Match(line);
            if (!match.Success)
                return false;

            frame = new StackFrame
            {
                File = match.Groups[2].Value,
                Method = match.Groups[1]?.Value ?? "<unknown>",
                Arguments = "",
                LineNumber = int.Parse(match.Groups[3].Value),
                ColumnNumber = int.Parse(match.Groups[4].Value)
            };

            return true;
        }

        private static readonly Regex GeckoRe = new Regex(@"^\s*(.*?)(?:\((.*?)\))?(?:^|@)((?:file|https?|blob|chrome|webpack|resource|\[native).*?|[^@]*bundle)(?::(\d+))?(?::(\d+))?\s*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex GeckoEvalRe = new Regex(@"(\S+) line (\d+)(?: > eval line \d+)* > eval", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        internal static bool TryParseGecko(string line, out StackFrame frame)
        {
            frame = null;

            var match = GeckoRe.Match(line);
            if (!match.Success)
                return false;

            var isEval = match.Groups[3].Value.IndexOf("> eval") > -1;

            frame = new StackFrame
            {
                File = match.Groups[3].Value,
                Method = match.Groups[1]?.Value ?? "<unknown>",
                Arguments = match.Groups[2]?.Value,
                LineNumber = int.Parse(match.Groups[4].Value),
                ColumnNumber = int.Parse(match.Groups[5].Value),
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
    }
}
