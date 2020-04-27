using System;

namespace SourceMaps
{
    public struct SourceMapMappingEntry : IEquatable<SourceMapMappingEntry>
    {
        public readonly int GeneratedLineNumber;
        public readonly int GeneratedColumnNumber;
        public readonly int? OriginalLineNumber;
        public readonly int? OriginalColumnNumber;
        public readonly string OriginalName;
        public readonly string OriginalFileName;

        public SourceMapMappingEntry(int generatedLineNumber, int generatedColumnNumber, int? originalLineNumber, int? originalColumnNumber, string originalName, string originalFileName)
        {
            this.GeneratedLineNumber = generatedLineNumber;
            this.GeneratedColumnNumber = generatedColumnNumber;
            this.OriginalLineNumber = originalLineNumber;
            this.OriginalColumnNumber = originalColumnNumber;
            this.OriginalName = originalName;
            this.OriginalFileName = originalFileName;
        }

        public bool Equals(SourceMapMappingEntry other)
        {
            return GeneratedLineNumber == other.GeneratedLineNumber &&
                   GeneratedColumnNumber == other.GeneratedColumnNumber &&
                   OriginalLineNumber == other.OriginalLineNumber &&
                   OriginalColumnNumber == other.OriginalColumnNumber &&
                   OriginalName == other.OriginalName &&
                   OriginalFileName == other.OriginalFileName;
        }

        public override bool Equals(object obj)
        {
            return obj is SourceMapMappingEntry other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = GeneratedLineNumber;
                hashCode = (hashCode * 397) ^ GeneratedColumnNumber;
                hashCode = (hashCode * 397) ^ OriginalLineNumber.GetHashCode();
                hashCode = (hashCode * 397) ^ OriginalColumnNumber.GetHashCode();
                hashCode = (hashCode * 397) ^ (OriginalName != null ? OriginalName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OriginalFileName != null ? OriginalFileName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
