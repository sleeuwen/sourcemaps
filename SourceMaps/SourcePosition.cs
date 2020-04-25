using System;

namespace SourceMaps
{
    public struct SourcePosition : IComparable<SourcePosition>, IEquatable<SourcePosition>
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

        public bool Equals(SourcePosition other)
        {
            return LineNumber == other.LineNumber && ColumnNumber == other.ColumnNumber;
        }

        public override bool Equals(object obj)
        {
            return obj is SourcePosition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LineNumber, ColumnNumber);
        }
    }
}
