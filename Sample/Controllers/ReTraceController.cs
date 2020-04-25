using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SourceMaps;
using SourceMaps.StackTraces;

namespace Sample.Controllers
{
    public class ReTraceController : Controller
    {
        private readonly SourceMapCollection _sourceMapCollection;

        public ReTraceController(SourceMapCollection sourceMapCollection)
        {
            _sourceMapCollection = sourceMapCollection;
        }

        [HttpPost]
        public async Task<string> ReTrace()
        {
            var stacktrace = await new StreamReader(Request.Body).ReadToEndAsync();
            var retraced = StackTraceParser.ReTrace(_sourceMapCollection, stacktrace, "https://localhost:5001/dist/");
            return retraced;
        }
    }
}
