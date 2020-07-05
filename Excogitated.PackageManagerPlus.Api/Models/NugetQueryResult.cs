using System;

namespace Excogitated.PackageManagerPlus.Models
{
    public class NugetQueryResponse
    {
        public class @contextData
        {
            public Uri @vocab { get; set; } // "http://schema.nuget.org/schema#"
            public Uri @base { get; set; } // "https://api.nuget.org/v3/registration5-semver1/"
        }
        public @contextData @context { get; set; }
        public int totalHits { get; set; } // 16091
        public class DataInfo
        {
            //public Uri @id { get; set; } // "https://api.nuget.org/v3/registration5-semver1/xunit.runner.visualstudio/index.json"
            public string @type { get; set; } // "Package"
            public Uri registration { get; set; } // "https://api.nuget.org/v3/registration5-semver1/xunit.runner.visualstudio/index.json"
            public string id { get; set; } // "xunit.runner.visualstudio"
            public string version { get; set; } // "2.4.2"
            public string description { get; set; } // "Visual Studio 2017 15.9+ Test Explorer runner for the xUnit.net framework. Capable of running xUnit.net v1.9.2 and v2.0+ tests. Supports .NET 2.0 or later, .NET Core 2.1 or later, and Universal Windows 10.0.16299 or later."
            public string summary { get; set; } // ""
            public string title { get; set; } // "xUnit.net [Runner: Visual Studio]"
            public Uri iconUrl { get; set; } // "https://api.nuget.org/v3-flatcontainer/xunit.runner.visualstudio/2.4.2/icon"
            public Uri licenseUrl { get; set; } // "https://www.nuget.org/packages/xunit.runner.visualstudio/2.4.2/license"
            public Uri projectUrl { get; set; } // "https://github.com/xunit/visualstudio.xunit"
            public string[] authors { get; set; }
            public int totalDownloads { get; set; } // 72855531
            public bool verified { get; set; } // true
            public class PackageTypesData
            {
                public string name { get; set; } // "Dependency"
            }
            public PackageTypesData[] packageTypes { get; set; }
            public class VersionsData
            {
                public string version { get; set; } // "2.0.0"
                public int downloads { get; set; } // 390724
                public Uri @id { get; set; } // "https://api.nuget.org/v3/registration5-semver1/xunit.runner.visualstudio/2.0.0.json"
            }
            public VersionsData[] versions { get; set; }
        }
        public DataInfo[] data { get; set; }
    }

}
