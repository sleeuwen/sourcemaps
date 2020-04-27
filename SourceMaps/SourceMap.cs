using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SourceMaps
{
    public class SourceMap
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
        public string Mappings { get; set; }

        [JsonIgnore]
        public List<SourceMapMappingEntry> ParsedMappings { get; set; }

        public SourceMapMappingEntry? OriginalPositionFor(int generatedLineNumber, int generatedColumnNumber)
        {
            if (ParsedMappings == null)
                return null;

            var generatedSourcePosition = new SourcePosition(generatedLineNumber, generatedColumnNumber);

            var index = ParsedMappings.BinarySearch(
                new SourceMapMappingEntry(generatedSourcePosition, default, null, null),
                Comparer<SourceMapMappingEntry>.Create((a, b) => a.GeneratedSourcePosition.CompareTo(b.GeneratedSourcePosition)));

            if (index < 0)
            {
                if (~index - 1 >= 0 &&
                    ParsedMappings[~index - 1].GeneratedSourcePosition.Equals(generatedSourcePosition))
                {
                    index = ~index - 1;
                }
            }

            return index >= 0 ? ParsedMappings[index] : (SourceMapMappingEntry?)null;
        }
    }
}
