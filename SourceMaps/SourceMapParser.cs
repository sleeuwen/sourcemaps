using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SourceMaps
{
    public static class SourceMapParser
    {
        /// <summary>
        /// Parses a stream of sourcemap data into a SourceMap
        /// </summary>
        /// <param name="stream">The stream containing sourcemap data</param>
        /// <returns>The decoded SourceMap</returns>
        /// <exception cref="InvalidSourceMapException">when the stream did not contain a valid sourcemap</exception>
        public static async Task<SourceMap> ParseAsync(Stream stream)
        {
            var sourceMap = await JsonSerializer.DeserializeAsync<SourceMap>(stream);
            if (sourceMap.Version != 3)
                throw new InvalidSourceMapException("Unsupported source map version");

            if (!string.IsNullOrEmpty(sourceMap.SourceRoot))
                sourceMap.Sources = sourceMap.Sources.Select(source => Path.Combine(sourceMap.SourceRoot, source)).ToList();

            try
            {
                sourceMap.Mappings = ParseMappings(sourceMap.MappingsString, sourceMap.Names, sourceMap.Sources);
                return sourceMap;
            }
            catch (Exception ex)
            {
                throw new InvalidSourceMapException("Unable to parse sourcemap", ex);
            }
        }

        /// <summary>
        /// Parses a string of sourcemap data into a SourceMap
        /// </summary>
        /// <param name="sourceMapString">The string containing sourcemap data</param>
        /// <returns>The decoded SourceMap</returns>
        /// <exception cref="InvalidSourceMapException">when the string did not contain a valid sourcemap</exception>
        public static SourceMap Parse(string sourceMapString)
        {
            var sourceMap = JsonSerializer.Deserialize<SourceMap>(sourceMapString);
            if (sourceMap.Version != 3)
                throw new ArgumentException("Unknown source map version");

            if (!string.IsNullOrEmpty(sourceMap.SourceRoot))
                sourceMap.Sources = sourceMap.Sources.Select(source => Path.Combine(sourceMap.SourceRoot, source)).ToList();

            try
            {
                sourceMap.Mappings = ParseMappings(sourceMap.MappingsString, sourceMap.Names, sourceMap.Sources);
                return sourceMap;
            }
            catch (Exception ex)
            {
                throw new InvalidSourceMapException("Unable to parse sourcemap", ex);
            }
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
                throw new ArgumentOutOfRangeException(nameof(segmentFields), $"Expected 1, 4 or 5 decoded segments, got {segmentFields.Count}");

            state.SegmentCount = segmentFields.Count;
            state.GeneratedColumnNumber += segmentFields[0];

            if (state.ContainsSourceLocation)
            {
                state.SourcesListIndex += segmentFields[1];
                state.OriginalLineNumber += segmentFields[2];
                state.OriginalColumnNumber += segmentFields[3];
            }

            if (state.ContainsOriginalName)
            {
                state.NamesListIndex += segmentFields[4];
            }
        }
    }

    internal struct MappingParserState
    {
        public int GeneratedLineNumber;
        public int GeneratedColumnNumber;
        public int SourcesListIndex;
        public int OriginalLineNumber;
        public int OriginalColumnNumber;
        public int NamesListIndex;

        internal int SegmentCount;

        internal bool ContainsSourceLocation => SegmentCount > 1;
        internal bool ContainsOriginalName => SegmentCount >= 5;

        public SourceMapMappingEntry GetCurrentSourceMapMappingEntry(List<string> names, List<string> sources)
            => new SourceMapMappingEntry(
                GeneratedLineNumber,
                GeneratedColumnNumber,
                ContainsSourceLocation ? OriginalLineNumber : (int?)null,
                ContainsSourceLocation ? OriginalColumnNumber : (int?)null,
                ContainsOriginalName ? names[NamesListIndex] : null,
                ContainsSourceLocation ? sources[SourcesListIndex] : null);
    }
}
