using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SourceMaps
{
    public static class SourceMapParser
    {
        public static SourceMap Parse(string sourceMapString)
        {
            var sourceMap = JsonSerializer.Deserialize<SourceMap>(sourceMapString);
            if (sourceMap.Version != 3)
                throw new ArgumentException($"Unknown source map version: {sourceMap.Version}");

            if (!string.IsNullOrEmpty(sourceMap.SourceRoot))
                sourceMap.Sources = sourceMap.Sources.Select(source => Path.Combine(sourceMap.SourceRoot, source)).ToList();

            sourceMap.Mappings = ParseMappings(sourceMap.MappingsString, sourceMap.Names, sourceMap.Sources);
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

                var segments = lines[lineNumber].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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
            _ = segmentFields ?? throw new ArgumentNullException(nameof(segmentFields));

            if (segmentFields.Count != 1 && segmentFields.Count != 4 && segmentFields.Count != 5)
                throw new ArgumentOutOfRangeException(nameof(segmentFields), $"Expected 1, 4 or 5 fields, got {segmentFields.Count}");

            state.GeneratedColumnNumber += segmentFields[0];

            if (segmentFields.Count > 1)
            {
                state.SourcesListIndex = (state.SourcesListIndex ?? 0) + segmentFields[1];
                state.OriginalLineNumber = (state.OriginalLineNumber ?? 0) + segmentFields[2];
                state.OriginalColumnNumber = (state.OriginalColumnNumber ?? 0) + segmentFields[3];
            }

            if (segmentFields.Count >= 5)
            {
                state.NamesListIndex = (state.NamesListIndex ?? 0) + segmentFields[4];
            }
        }
    }

    internal struct MappingParserState
    {
        public int GeneratedLineNumber;
        public int GeneratedColumnNumber;
        public int? SourcesListIndex;
        public int? OriginalLineNumber;
        public int? OriginalColumnNumber;
        public int? NamesListIndex;

        public MappingParserState(
            int generatedLineNumber,
            int generatedColumnNumber,
            int sourcesListIndex,
            int originalLineNumber,
            int originalColumnNumber,
            int namesListIndex)
        {
            this.GeneratedLineNumber = generatedLineNumber;
            this.GeneratedColumnNumber = generatedColumnNumber;
            this.OriginalLineNumber = originalLineNumber;
            this.OriginalColumnNumber = originalColumnNumber;
            this.SourcesListIndex = sourcesListIndex;
            this.NamesListIndex = namesListIndex;
        }

        public SourceMapMappingEntry GetCurrentSourceMapMappingEntry(List<string> names, List<string> sources)
            => new SourceMapMappingEntry(
                GeneratedLineNumber,
                GeneratedColumnNumber,
                OriginalLineNumber,
                OriginalColumnNumber,
                NamesListIndex.HasValue ? names[NamesListIndex.Value] : null,
                SourcesListIndex.HasValue ? sources[SourcesListIndex.Value] : null);
    }
}
