# SourceMaps

.NET Standard 2.0 library for parsing and using SourceMaps

## Packages
### SourceMaps
SourceMaps library that handles parsing of the sourcemap and has methods for getting the original position,
name and file for a generated position.

### SourceMaps.StackTraces
SourceMaps.StackTraces is an additional package that can be used to parse JavaScript stack traces
and map them with the source map to the original files.

## Usage
### SourceMaps
To parse a sourcemap located at `./app.js.map`

```csharp
using SourceMaps
// ...

var map = File.ReadAllText("./app.js.map");
var sourceMap = SourceMapParser.Parse(map);
```

To get the original mapping for a generated position, use

```csharp
var mapping = sourceMap.OriginalPositionFor(/* generatedLineNumber */ 1, /* generatedColumnNumber */ 1);

mapping.OriginalName; // original token name
mapping.OriginalFileName; // original file name
mapping.OriginalSourcePosition.LineNumber; // original line number
mapping.OriginalSourcePosition.ColumnNumber; // original column number
```

### SourceMaps.StackTraces
To get the original stack trace using the source maps

```csharp
StackTraceParser.ReTrace(sourceMap, /* JavaScript StackTrace */ "...", /* optional source root */ "https://localhost:5001/js/")
```
