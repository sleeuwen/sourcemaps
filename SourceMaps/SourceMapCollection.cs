using System.Collections.Generic;

namespace SourceMaps
{
    public class SourceMapCollection
    {
        private Dictionary<string, SourceMap> _sourceMaps = new Dictionary<string, SourceMap>();

        public void Register(SourceMap sourceMap)
        {
            _sourceMaps.Add(sourceMap.File, sourceMap);
        }

        public void Register(string name, SourceMap sourceMap)
        {
            _sourceMaps.Add(name, sourceMap);
        }

        public void ParseAndRegister(string sourceMapContent)
        {
            var sourceMap = SourceMapParser.Parse(sourceMapContent);
            _sourceMaps.Add(sourceMap.File, sourceMap);
        }

        public void ParseAndRegister(string name, string sourceMapContent)
        {
            _sourceMaps.Add(name, SourceMapParser.Parse(sourceMapContent));
        }

        public SourceMap GetSourceMapFor(string filename)
        {
            if (_sourceMaps.TryGetValue(filename, out var sourceMap))
                return sourceMap;

            return null;
        }
    }
}
