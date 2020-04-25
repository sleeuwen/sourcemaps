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

        public void Register(string filepath, SourceMap sourceMap)
        {
            _sourceMaps.Add(filepath, sourceMap);
        }

        public void ParseAndRegister(string filepath, string sourceMapContent)
        {
            _sourceMaps.Add(filepath, SourceMapParser.Parse(sourceMapContent));
        }

        public SourceMap GetSourceMapFor(string filename)
        {
            if (_sourceMaps.TryGetValue(filename, out var sourceMap))
                return sourceMap;

            return null;
        }
    }
}
