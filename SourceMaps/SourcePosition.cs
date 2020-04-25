using System;

namespace SourceMaps
{
    public struct SourcePosition : IComparable<SourcePosition>
    {
        public int LineNumber;
        public int ColumnNumber;

        public SourcePosition(int lineNumber, int columnNumber)
        {
            this.LineNumber = lineNumber;
            this.ColumnNumber = columnNumber;
        }

        public int CompareTo(SourcePosition other)
        {
            var lineNumberComparison = LineNumber.CompareTo(other.LineNumber);
            if (lineNumberComparison != 0) return lineNumberComparison;
            return ColumnNumber.CompareTo(other.ColumnNumber);
        }
    }
}
