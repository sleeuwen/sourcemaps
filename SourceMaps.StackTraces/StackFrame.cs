namespace SourceMaps.StackTraces
{
    public class StackFrame
    {
        public string File { get; set; }
        public string Method { get; set; }
        public string[] Arguments { get; set; }
        public int? LineNumber { get; set; }
        public int? ColumnNumber { get; set; }

        public override string ToString()
        {
            var pos = $"{File}:{LineNumber}:{ColumnNumber}";

            if (string.IsNullOrEmpty(Method))
                return pos;
            return $"{Method} ({pos})";
        }
    }
}
