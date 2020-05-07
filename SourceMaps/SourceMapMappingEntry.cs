using System;

namespace SourceMaps
{
    public readonly struct SourceMapMappingEntry : IEquatable<SourceMapMappingEntry>
    {
        public SourceMapMappingEntry(int generatedLineNumber, int generatedColumnNumber, int? originalLineNumber, int? originalColumnNumber, string originalName, string originalFileName)
        {
            this.GeneratedLineNumber = generatedLineNumber;
            this.GeneratedColumnNumber = generatedColumnNumber;
            this.OriginalLineNumber = originalLineNumber;
            this.OriginalColumnNumber = originalColumnNumber;
            this.OriginalName = originalName;
            this.OriginalFileName = originalFileName;
        }

        /// <summary>
        /// Zero-based generated line number
        /// </summary>
        public int GeneratedLineNumber { get; }

        /// <summary>
        /// Zero-based generated column number
        /// </summary>
        public int GeneratedColumnNumber { get; }

        /// <summary>
        /// Zero-based original line number
        /// </summary>
        public int? OriginalLineNumber { get; }

        /// <summary>
        /// Zero-based original column number
        /// </summary>
        public int? OriginalColumnNumber { get; }

        /// <summary>
        /// Original element name
        /// </summary>
        public string OriginalName { get; }

        /// <summary>
        /// Original file name
        /// </summary>
        public string OriginalFileName { get; }

        public static bool operator ==(SourceMapMappingEntry left, SourceMapMappingEntry right)
            => Equals(left, right);

        public static bool operator !=(SourceMapMappingEntry left, SourceMapMappingEntry right)
            => !Equals(left, right);

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
