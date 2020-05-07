using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SourceMaps
{
    public sealed class SourceMap
    {
        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("file")]
        public string File { get; set; }

        [JsonPropertyName("sourceRoot")]
        public string SourceRoot { get; set; }

        [JsonPropertyName("sources")]
        public List<string> Sources { get; set; }

        [JsonPropertyName("sourcesContent")]
        public List<string> SourcesContent { get; set; }

        [JsonPropertyName("names")]
        public List<string> Names { get; set; }

        [JsonPropertyName("mappings")]
        public string MappingsString { get; set; }

        [JsonIgnore]
        public List<SourceMapMappingEntry> Mappings { get; set; }

        /// <summary>
        /// Returns the original source, line and column information for the generated
        /// source's line and column positions provided.
        /// </summary>
        /// <param name="generatedLineNumber">The zero-based line number in the generated source, 1-based.</param>
        /// <param name="generatedColumnNumber">The zero-based column number in the generated source, 0-based</param>
        /// <returns>A struct with the original mappings, or null if no mapping exit for the given line and column number.</returns>
        public SourceMapMappingEntry? OriginalPositionFor(int generatedLineNumber, int generatedColumnNumber)
        {
            if (Mappings == null)
                return null;

            var index = Mappings.BinarySearch(
                new SourceMapMappingEntry(generatedLineNumber, generatedColumnNumber, null, null, null, null),
                Comparer<SourceMapMappingEntry>.Create((a, b) =>
                {
                    var lineNumberComparison = a.GeneratedLineNumber.CompareTo(b.GeneratedLineNumber);
                    if (lineNumberComparison != 0) return lineNumberComparison;
                    return a.GeneratedColumnNumber.CompareTo(b.GeneratedColumnNumber);
                }));

            if (index < 0)
            {
                if (~index - 1 >= 0 &&
                    Mappings[~index - 1].GeneratedLineNumber == generatedLineNumber &&
                    Mappings[~index - 1].GeneratedColumnNumber == generatedColumnNumber)
                {
                    index = ~index - 1;
                }
            }

            return index >= 0 ? Mappings[index] : (SourceMapMappingEntry?)null;
        }
    }
}
