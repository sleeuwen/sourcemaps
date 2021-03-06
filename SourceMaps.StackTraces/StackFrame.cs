using System;

namespace SourceMaps.StackTraces
{
    public class StackFrame
    {
        public string? File { get; set; }
        public string? Method { get; set; }
        public string[] Arguments { get; set; } = Array.Empty<string>();
        public int? LineNumber { get; set; }
        public int? ColumnNumber { get; set; }

        public override string ToString()
        {
            var pos = $"{File}:{LineNumber}:{ColumnNumber}";

            if (string.IsNullOrEmpty(Method))
                return $"  at {pos}";
            return $"  at {Method}{(Arguments.Length > 0 ? $"({string.Join(", ", Arguments)})" : "")} ({pos})";
        }
    }
}
