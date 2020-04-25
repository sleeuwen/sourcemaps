using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SourceMaps
{
    public static class SourceMapParser
    {
        public static SourceMap Parse(string sourceMapString)
        {
            var sourceMap = JsonSerializer.Deserialize<SourceMap>(sourceMapString);
            sourceMap.ParsedMappings = ParseMappings(sourceMap.Mappings, sourceMap.Names, sourceMap.Sources);
            return sourceMap;
        }

        internal static List<SourceMapMappingEntry> ParseMappings(string mappings, List<string> names, List<string> sources)
        {
            var result = new List<SourceMapMappingEntry>();
            var state = new MappingParserState();

            var lines = mappings.Split(';');
            for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
            {
                state.GeneratedLineNumber = lineNumber;
                state.GeneratedColumnNumber = 0;

                var segments = lines[lineNumber].Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var segment in segments)
                {
                    ApplyMappingSegment(Base64Vlq.Decode(segment), ref state);
                    result.Add(state.GetCurrentSourceMapMappingEntry(names, sources));
                }
            }

            return result;
        }

        internal static void ApplyMappingSegment(List<int> segmentFields, ref MappingParserState state)
        {
            if (segmentFields == null)
            {
                throw new ArgumentNullException(nameof(segmentFields));
            }

            if (segmentFields.Count == 0 || segmentFields.Count == 2 || segmentFields.Count == 3)
            {
                throw new ArgumentOutOfRangeException(nameof(segmentFields));
            }

            state.GeneratedColumnNumber += segmentFields[0];

            if (segmentFields.Count > 1)
            {
                state.SourcesListIndex += segmentFields[1];
                state.OriginalSourceStartingLineNumber += segmentFields[2];
                state.OriginalSourceStartingColumnNumber += segmentFields[3];
            }

            if (segmentFields.Count >= 5)
            {
                state.NamesListIndex += segmentFields[4];
            }
        }
    }

    internal struct MappingParserState
    {
        public int GeneratedLineNumber;
        public int GeneratedColumnNumber;
        public int? SourcesListIndex;
        public int? OriginalSourceStartingLineNumber;
        public int? OriginalSourceStartingColumnNumber;
        public int? NamesListIndex;

        public MappingParserState(
            int generatedLineNumber,
            int generatedColumnNumber,
            int sourcesListIndex,
            int originalSourceStartingLineNumber,
            int originalSourceStartingColumnNumber,
            int namesListIndex)
        {
            this.GeneratedLineNumber = generatedLineNumber;
            this.GeneratedColumnNumber = generatedColumnNumber;
            this.SourcesListIndex = sourcesListIndex;
            this.OriginalSourceStartingLineNumber = originalSourceStartingLineNumber;
            this.OriginalSourceStartingColumnNumber = originalSourceStartingColumnNumber;
            this.NamesListIndex = namesListIndex;
        }

        public SourceMapMappingEntry GetCurrentSourceMapMappingEntry(List<string> names, List<string> sources)
            => new SourceMapMappingEntry(
                new SourcePosition(GeneratedLineNumber, GeneratedColumnNumber),
                new SourcePosition(OriginalSourceStartingLineNumber ?? 0, OriginalSourceStartingColumnNumber ?? 0),
                NamesListIndex.HasValue ? names[NamesListIndex.Value] : null,
                SourcesListIndex.HasValue ? sources[SourcesListIndex.Value] : null);
    }
}
