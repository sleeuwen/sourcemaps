using System.Collections.Generic;

namespace SourceMaps.Tests
{
    public static class Helper
    {
        public static readonly string TestGeneratedCode =
            " ONE.foo=function(a){return baz(a);};\n TWO.inc=function(a){return a+1;};";

        public static readonly SourceMap TestMap = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { "bar", "baz", "n" },
            Sources = new List<string> { "one.js", "two.js" },
            SourceRoot = "/the/root",
            Mappings = "CAAC,IAAI,IAAM,SAAUA,GAClB,OAAOC,IAAID;CCDb,IAAI,IAAM,SAAUE,GAClB,OAAOA",
        };

        public static readonly SourceMap TestMapNoSourceRoot = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { "bar", "baz", "n" },
            Sources = new List<string> { "one.js", "two.js" },
            Mappings =
                "CAAC,IAAI,IAAM,SAAUA,GAClB,OAAOC,IAAID;CCDb,IAAI,IAAM,SAAUE,GAClB,OAAOA"
        };

        public static readonly SourceMap TestMapEmptySourceRoot = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { "bar", "baz", "n" },
            Sources = new List<string> { "one.js", "two.js" },
            SourceRoot = "",
            Mappings =
                "CAAC,IAAI,IAAM,SAAUA,GAClB,OAAOC,IAAID;CCDb,IAAI,IAAM,SAAUE,GAClB,OAAOA"
        };

        public static readonly SourceMap TestMapSingleSource = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { "bar", "baz" },
            Sources = new List<string> { "one.js" },
            SourceRoot = "",
            Mappings = "CAAC,IAAI,IAAM,SAAUA,GAClB,OAAOC,IAAID"
        };

        public static readonly SourceMap TestMapEmptyMappings = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { },
            Sources = new List<string> { "one.js", "two.js" },
            SourcesContent = new List<string> { " ONE.foo = 1;", " TWO.inc = 2;" },
            SourceRoot = "",
            Mappings = ""
        };

        public static readonly SourceMap TestMapEmptyMappingsRelativeSources = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { },
            Sources = new List<string> { "./one.js", "./two.js" },
            SourcesContent = new List<string> { " ONE.foo = 1;", " TWO.inc = 2;" },
            SourceRoot = "/the/root",
            Mappings = ""
        };

        public static readonly SourceMap TestMapEmptyMappingsRelativeSources_generatedExpected = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { },
            Sources = new List<string> { "one.js", "two.js" },
            SourcesContent = new List<string> { " ONE.foo = 1;", " TWO.inc = 2;" },
            SourceRoot = "/the/root",
            Mappings = ""
        };

        public static readonly SourceMap TestMapMultiSourcesMappingRefersSingleSourceOnly = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { "bar", "baz" },
            Sources = new List<string> { "one.js", "withoutMappings.js" },
            SourceRoot = "",
            Mappings = "CAAC,IAAI,IAAM,SAAUA,GAClB,OAAOC,IAAID"
        };


        public static readonly SourceMap TestMapWithSourcesContent = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { "bar", "baz", "n" },
            Sources = new List<string> { "one.js", "two.js" },
            SourcesContent = new List<string>
            {
                " ONE.foo = function (bar) {\n   return baz(bar);\n };",
                " TWO.inc = function (n) {\n   return n + 1;\n };"
            },
            SourceRoot = "/the/root",
            Mappings =
                "CAAC,IAAI,IAAM,SAAUA,GAClB,OAAOC,IAAID;CCDb,IAAI,IAAM,SAAUE,GAClB,OAAOA"
        };

        public static readonly SourceMap TestMapRelativeSources = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { "bar", "baz", "n" },
            Sources = new List<string> { "./one.js", "./two.js" },
            SourcesContent = new List<string>
            {
                " ONE.foo = function (bar) {\n   return baz(bar);\n };",
                " TWO.inc = function (n) {\n   return n + 1;\n };"
            },
            SourceRoot = "/the/root",
            Mappings =
                "CAAC,IAAI,IAAM,SAAUA,GAClB,OAAOC,IAAID;CCDb,IAAI,IAAM,SAAUE,GAClB,OAAOA"
        };

        public static readonly SourceMap EmptyMap = new SourceMap
        {
            Version = 3,
            File = "min.js",
            Names = new List<string> { },
            Sources = new List<string> { },
            Mappings = ""
        };

        public static readonly SourceMap MapWithSourcelessMapping = new SourceMap
        {
            Version = 3,
            File = "example.js",
            Names = new List<string> { },
            Sources = new List<string> { "example.js" },
            Mappings = "AAgCA,C"
        };
    }
}
