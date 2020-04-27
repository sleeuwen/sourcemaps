using System;

namespace SourceMaps
{
    public struct SourceMapMappingEntry : IEquatable<SourceMapMappingEntry>
    {
        public readonly SourcePosition GeneratedSourcePosition;
        public readonly SourcePosition OriginalSourcePosition;
        public readonly string OriginalName;
        public readonly string OriginalFileName;

        public SourceMapMappingEntry(SourcePosition generatedSourcePosition, SourcePosition originalSourcePosition, string originalName, string originalFileName)
        {
            this.GeneratedSourcePosition = generatedSourcePosition;
            this.OriginalSourcePosition = originalSourcePosition;
            this.OriginalName = originalName;
            this.OriginalFileName = originalFileName;
        }

        public bool Equals(SourceMapMappingEntry other)
        {
            return GeneratedSourcePosition.Equals(other.GeneratedSourcePosition) && OriginalSourcePosition.Equals(other.OriginalSourcePosition) && OriginalName == other.OriginalName && OriginalFileName == other.OriginalFileName;
        }

        public override bool Equals(object obj)
        {
            return obj is SourceMapMappingEntry other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = GeneratedSourcePosition.GetHashCode();
                hashCode = (hashCode * 397) ^ OriginalSourcePosition.GetHashCode();
                hashCode = (hashCode * 397) ^ (OriginalName != null ? OriginalName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OriginalFileName != null ? OriginalFileName.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
