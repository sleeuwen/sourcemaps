using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SourceMaps
{
    public sealed class SourceMap
    {
        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("file")]
        public string? File { get; set; }

        [JsonPropertyName("sourceRoot")]
        public string? SourceRoot { get; set; }

        [JsonPropertyName("sources")]
        public List<string> Sources { get; set; } = new List<string>();

        [JsonPropertyName("sourcesContent")]
        public List<string>? SourcesContent { get; set; }

        [JsonPropertyName("names")]
        public List<string> Names { get; set; } = new List<string>();

        [JsonPropertyName("mappings")]
        public string MappingsString { get; set; } = "";

        [JsonIgnore]
        public List<SourceMapMappingEntry>? Mappings { get; set; }

        /// <summary>
        /// Returns the original source, line and column information for the generated
        /// source's line and column positions provided.
        /// </summary>
        /// <param name="generatedLineNumber">The one-based line number in the generated source.</param>
        /// <param name="generatedColumnNumber">The zero-based column number in the generated source</param>
        /// <returns>A struct with the original mappings, or null if no mapping exit for the given line and column number.</returns>
        public SourceMapMappingEntry? OriginalPositionFor(int generatedLineNumber, int generatedColumnNumber)
        {
            if (Mappings == null)
                return null;

            var searchEntry = new SourceMapMappingEntry(generatedLineNumber - 1, generatedColumnNumber, null, null, null, null);
            var index = Mappings.BinarySearch(searchEntry, MappingEntryComparer);

            if (index < 0)
            {
                if (~index - 1 >= 0)
                {
                    index = ~index - 1;
                }
            }

            return index >= 0 ? Mappings[index] : (SourceMapMappingEntry?)null;
        }

        private static readonly IComparer<SourceMapMappingEntry> MappingEntryComparer = Comparer<SourceMapMappingEntry>.Create((a, b) =>
        {
            var lineNumberCompare = a.GeneratedLineNumber.CompareTo(b.GeneratedLineNumber);
            if (lineNumberCompare != 0)
                return lineNumberCompare;

            return a.GeneratedColumnNumber.CompareTo(b.GeneratedColumnNumber);
        });
    }
}
