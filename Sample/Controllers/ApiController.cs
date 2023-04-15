using Microsoft.AspNetCore.Mvc;
using SourceMaps;
using SourceMaps.StackTraces;

namespace Sample.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    private readonly SourceMapCollection _sourceMapCollection;

    public ApiController(SourceMapCollection sourceMapCollection)
    {
        _sourceMapCollection = sourceMapCollection;
    }

    [HttpPost("/api/retrace")]
    public async Task<string> ReTrace()
    {
        var stacktrace = await new StreamReader(Request.Body).ReadToEndAsync();
        return StackTraceParser.ReTrace(_sourceMapCollection, stacktrace, $"{Request.Scheme}://{Request.Host}{Request.PathBase}/");
    }
}