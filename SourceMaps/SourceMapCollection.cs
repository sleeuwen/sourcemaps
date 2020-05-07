using System;
using System.Collections.Generic;

namespace SourceMaps
{
    /// <summary>
    /// Collection of multiple sourcemaps.
    /// </summary>
    public class SourceMapCollection
    {
        private Dictionary<string, SourceMap> _sourceMaps = new Dictionary<string, SourceMap>();

        /// <summary>
        /// Register the given sourcemap in the collection using the SourceMap's File as name.
        /// </summary>
        /// <param name="sourceMap">A <see cref="SourceMap" /></param>
        public void Register(SourceMap sourceMap)
        {
            if (string.IsNullOrEmpty(sourceMap.File))
                throw new ArgumentException("sourceMap has no associated filename.");

            _sourceMaps.Add(sourceMap.File!, sourceMap);
        }

        /// <summary>
        /// Register the given sourcemap in the collection using the given name.
        /// </summary>
        /// <param name="name">The name to register the SourceMap as</param>
        /// <param name="sourceMap">A <see cref="SourceMap" /></param>
        public void Register(string name, SourceMap sourceMap)
        {
            _sourceMaps.Add(name, sourceMap);
        }

        /// <summary>
        /// Parse and register the given sourcemap in the collection using the SourceMap's File as name.
        /// </summary>
        /// <param name="sourceMapContent">A string containing an encoded SourceMap.</param>
        public void ParseAndRegister(string sourceMapContent)
        {
            var sourceMap = SourceMapParser.Parse(sourceMapContent);

            if (string.IsNullOrEmpty(sourceMap.File))
                throw new ArgumentException("sourceMap has no associated filename.");

            _sourceMaps.Add(sourceMap.File!, sourceMap);
        }

        /// <summary>
        /// Parse and register the given sourcemap in the collection using the given name.
        /// </summary>
        /// <param name="name">The name to register the SourceMap as</param>
        /// <param name="sourceMapContent">A string containing an encoded SourceMap.</param>
        public void ParseAndRegister(string name, string sourceMapContent)
        {
            _sourceMaps.Add(name, SourceMapParser.Parse(sourceMapContent));
        }

        /// <summary>
        /// Get the <see cref="SourceMap" /> that corresponds to the given filename.
        /// </summary>
        /// <param name="filename">The filename to get the SourceMap for.</param>
        /// <returns>The SourceMap, or null if none were found.</returns>
        public SourceMap? GetSourceMapFor(string? filename)
        {
            if (filename != null && _sourceMaps.TryGetValue(filename, out var sourceMap))
                return sourceMap;

            return null;
        }
    }
}
