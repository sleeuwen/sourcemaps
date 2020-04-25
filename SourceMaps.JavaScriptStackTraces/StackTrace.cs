using System.Collections.Generic;
using System.Linq;

namespace SourceMaps.StackTraces
{
    public class StackTrace
    {
        public List<StackFrame> Frames { get; set; } = new List<StackFrame>();

        internal void Append(StackFrame frame)
            => Frames.Add(frame);

        public override string ToString()
        {
            return string.Join('\n', Frames.Select(frame => frame.ToString()));
        }
    }
}
