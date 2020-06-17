using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Xunit;

namespace SourceMaps.Tests
{
    public class SourceMapParserTests
    {
        [Fact]
        public void ApplyMappingSegment_CorrectlyUpdatesState()
        {
            var state = new MappingParserState { GeneratedLineNumber = 1, GeneratedColumnNumber = 2, SourcesListIndex = 3, OriginalLineNumber = 4, OriginalColumnNumber = 5, NamesListIndex = 6 };

            var segmentFields = new List<int> { 4 }; // Only column number
            SourceMapParser.ApplyMappingSegment(segmentFields, ref state);

            Assert.Equal(1, state.GeneratedLineNumber);
            Assert.Equal(6, state.GeneratedColumnNumber);
            Assert.Equal(3, state.SourcesListIndex);
            Assert.Equal(4, state.OriginalLineNumber);
            Assert.Equal(5, state.OriginalColumnNumber);
            Assert.Equal(6, state.NamesListIndex);

            segmentFields = new List<int> { 1, 5, 4, 3 };
            SourceMapParser.ApplyMappingSegment(segmentFields, ref state);

            Assert.Equal(1, state.GeneratedLineNumber);
            Assert.Equal(7, state.GeneratedColumnNumber);
            Assert.Equal(8, state.SourcesListIndex);
            Assert.Equal(8, state.OriginalLineNumber);
            Assert.Equal(8, state.OriginalColumnNumber);
            Assert.Equal(6, state.NamesListIndex);

            segmentFields = new List<int> { 1, 1, 1, 1, 3 };
            SourceMapParser.ApplyMappingSegment(segmentFields, ref state);

            Assert.Equal(1, state.GeneratedLineNumber);
            Assert.Equal(8, state.GeneratedColumnNumber);
            Assert.Equal(9, state.SourcesListIndex);
            Assert.Equal(9, state.OriginalLineNumber);
            Assert.Equal(9, state.OriginalColumnNumber);
            Assert.Equal(9, state.NamesListIndex);
        }

        [Fact]
        public void ParseMappings_CorrectlyParsesSingleMapping()
        {
            var result = SourceMapParser.ParseMappings(Helper.TestMapWithSourcesContent.MappingsString, Helper.TestMapWithSourcesContent.Names, Helper.TestMapWithSourcesContent.Sources);

            Assert.Equal(13, result.Count);
        }

        [Fact]
        public void Parse_ParsesSuccessfully()
        {
            var result = SourceMapParser.Parse(JsonSerializer.Serialize(Helper.TestMap));
            Assert.NotNull(result);
        }

        [Fact]
        public void Parse_SourceMap_ContainsOriginalSources()
        {
            SourceMap map;

            map = SourceMapParser.Parse(JsonSerializer.Serialize(Helper.TestMap));
            Assert.Equal("/the/root/one.js", map.Sources[0]);
            Assert.Equal("/the/root/two.js", map.Sources[1]);
            Assert.Equal(2, map.Sources.Count);

            map = SourceMapParser.Parse(JsonSerializer.Serialize(Helper.TestMapNoSourceRoot));
            Assert.Equal("one.js", map.Sources[0]);
            Assert.Equal("two.js", map.Sources[1]);
            Assert.Equal(2, map.Sources.Count);

            map = SourceMapParser.Parse(JsonSerializer.Serialize(Helper.TestMapEmptySourceRoot));
            Assert.Equal("one.js", map.Sources[0]);
            Assert.Equal("two.js", map.Sources[1]);
            Assert.Equal(2, map.Sources.Count);
        }

        // [Fact]
        public void Parse_MapsMappingTokensBackExactly()
        {
            var map = SourceMapParser.Parse(JsonSerializer.Serialize(Helper.TestMap));

            Helper.AssertMapping(1, 1, "/the/root/one.js", 1, 1, null, map);
            Helper.AssertMapping(1, 5, "/the/root/one.js", 1, 5, null, map);
            Helper.AssertMapping(1, 9, "/the/root/one.js", 1, 11, null, map);
            Helper.AssertMapping(1, 18, "/the/root/one.js", 1, 21, "bar", map);
            Helper.AssertMapping(1, 21, "/the/root/one.js", 2, 3, null, map);
            Helper.AssertMapping(1, 28, "/the/root/one.js", 2, 10, "baz", map);
            Helper.AssertMapping(1, 32, "/the/root/one.js", 2, 14, "bar", map);
            Helper.AssertMapping(2, 1, "/the/root/two.js", 1, 1, null, map);
            Helper.AssertMapping(2, 5, "/the/root/two.js", 1, 5, null, map);
            Helper.AssertMapping(2, 9, "/the/root/two.js", 1, 11, null, map);
            Helper.AssertMapping(2, 18, "/the/root/two.js", 1, 21, "n", map);
            Helper.AssertMapping(2, 21, "/the/root/two.js", 2, 3, null, map);
            Helper.AssertMapping(2, 28, "/the/root/two.js", 2, 10, "n", map);
        }

        // [Fact]
        public void Parse_MappingsAndEndOfLines()
        {
            var map = new SourceMap
            {
                Mappings = new List<SourceMapMappingEntry>
                {
                    new SourceMapMappingEntry(1, 1, 1, 1, null, "bar.js"),
                    new SourceMapMappingEntry(2, 2, 2, 2, null, "bar.js"),
                    new SourceMapMappingEntry(1, 1, 1, 1, null, "baz.js"),
                }
            };

            Helper.AssertMapping(1, 1, "bar.js", 2, 1, null, map);
        }

        [Fact]
        public void Parse_SourcesContentContainsOriginalSources()
        {
            var map = SourceMapParser.Parse(JsonSerializer.Serialize(Helper.TestMapWithSourcesContent));

            Assert.Equal(2, map.SourcesContent.Count);
            Assert.Equal(" ONE.foo = function (bar) {\n   return baz(bar);\n };", map.SourcesContent[0]);
            Assert.Equal(" TWO.inc = function (n) {\n   return n + 1;\n };", map.SourcesContent[1]);
        }

        [Fact]
        public void Parse_OriginalPositionFor()
        {
            var map = new SourceMap
            {
                Mappings = new List<SourceMapMappingEntry>
                {
                    new SourceMapMappingEntry(2, 2, 1, 1, null,
                        "foo/bar/bang.coffee"),
                }
            };

            var pos = map.OriginalPositionFor(3, 2).Value;
            Assert.Equal("foo/bar/bang.coffee", pos.OriginalFileName);
            Assert.Equal(1, pos.OriginalLineNumber);
            Assert.Equal(1, pos.OriginalColumnNumber);
        }

        [Theory]
        [InlineData("files/atlassian.sourcemap")]
        [InlineData("files/babel.sourcemap")]
        [InlineData("files/sass.sourcemap")]
        public void Parse_CanParseGeneratedSourcemaps(string path)
        {
            var map = SourceMapParser.Parse(File.ReadAllText(path));
            Assert.NotNull(map);
        }
    }
}
