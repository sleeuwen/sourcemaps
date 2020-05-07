using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SourceMaps;
using SourceMaps.StackTraces;

namespace Sample.Controllers
{
    public class ApiController : Controller
    {
        private readonly SourceMapCollection _sourceMaps;

        public ApiController(SourceMapCollection sourceMaps)
        {
            _sourceMaps = sourceMaps;
        }

        [HttpPost("/api/retrace")]
        public async Task<string> ReTrace()
        {
            var stacktrace = await new StreamReader(Request.Body).ReadToEndAsync();
            return StackTraceParser.ReTrace(_sourceMaps, stacktrace, $"{Request.Scheme}://{Request.Host}{Request.PathBase}/");
        }
    }
}
