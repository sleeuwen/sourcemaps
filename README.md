# SourceMaps

.NET Standard 2.0 library for parsing and using SourceMaps

## Packages
### SourceMaps [![NuGet Version](http://img.shields.io/nuget/v/SourceMaps.svg?style=flat)](https://www.nuget.org/packages/SourceMaps/)
SourceMaps library that handles parsing of the sourcemap and has methods for getting the original position,
name and file for a generated position.

### SourceMaps.StackTraces [![NuGet Version](http://img.shields.io/nuget/v/SourceMaps.StackTraces.svg?style=flat)](https://www.nuget.org/packages/SourceMaps.StackTraces/)
SourceMaps.StackTraces is an additional package that can be used to parse JavaScript stack traces
and map them with the source map to the original files.

## Usage
### SourceMaps
To parse a sourcemap

```csharp
using SourceMaps;

var sourceMap = SourceMapParser.Parse(sourceMapAsString);
```

To get the original mapping for a given generated position, use

```csharp
var mapping = sourceMap.OriginalPositionFor(generatedLineNumber: 1, generatedColumnNumber: 1);

mapping.OriginalName; // original token name
mapping.OriginalFileName; // original file name
mapping.OriginalSourcePosition.LineNumber; // original line number
mapping.OriginalSourcePosition.ColumnNumber; // original column number
```

### SourceMaps.StackTraces
To get the original stack trace using the source maps

```csharp
StackTraceParser.ReTrace(
    sourceMap,
    stackTrace: @"ReferenceError: ""getExceptionProps"" is undefined
    at eval code (eval code:1:1)
    at foo (http://path/to/file.js:58:17)
    at bar (http://path/to/file.js:109:1)",
    sourceRoot: "https://localhost:5001/js/");
```
