using System.Collections.Generic;
using Xunit;

namespace SourceMaps.Tests
{
    public class SourceMapParserTests
    {
        [Fact]
        public void ApplyMappingSegment_CorrectlyUpdatesState()
        {
            var state = new MappingParserState(1, 2, 3, 4, 5, 6);

            var segmentFields = new List<int> { 4 }; // Only column number
            SourceMapParser.ApplyMappingSegment(segmentFields, ref state);

            Assert.Equal(1, state.GeneratedLineNumber);
            Assert.Equal(6, state.GeneratedColumnNumber);
            Assert.Equal(3, state.SourcesListIndex);
            Assert.Equal(4, state.OriginalSourceStartingLineNumber);
            Assert.Equal(5, state.OriginalSourceStartingColumnNumber);
            Assert.Equal(6, state.NamesListIndex);

            segmentFields = new List<int> { 1, 5, 4, 3 };
            SourceMapParser.ApplyMappingSegment(segmentFields, ref state);

            Assert.Equal(1, state.GeneratedLineNumber);
            Assert.Equal(7, state.GeneratedColumnNumber);
            Assert.Equal(8, state.SourcesListIndex);
            Assert.Equal(8, state.OriginalSourceStartingLineNumber);
            Assert.Equal(8, state.OriginalSourceStartingColumnNumber);
            Assert.Equal(6, state.NamesListIndex);

            segmentFields = new List<int> { 1, 1, 1, 1, 3 };
            SourceMapParser.ApplyMappingSegment(segmentFields, ref state);

            Assert.Equal(1, state.GeneratedLineNumber);
            Assert.Equal(8, state.GeneratedColumnNumber);
            Assert.Equal(9, state.SourcesListIndex);
            Assert.Equal(9, state.OriginalSourceStartingLineNumber);
            Assert.Equal(9, state.OriginalSourceStartingColumnNumber);
            Assert.Equal(9, state.NamesListIndex);
        }

        [Fact]
        public void ParseMappings_CorrectlyParsesSingleMapping()
        {
            var result = SourceMapParser.ParseMappings(Helper.TestMapWithSourcesContent.Mappings, Helper.TestMapWithSourcesContent.Names, Helper.TestMapWithSourcesContent.Sources);

            Assert.Equal(13, result.Count);
        }
    }
}
