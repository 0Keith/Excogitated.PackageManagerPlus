using Excogitated.Common;

namespace Excogitated.PackageManagerPlus.Api
{
    public class NugetQueryData
    {
        public string Id { get; set; }
        public Displayable<double> Relevance { get; set; }
        public int TotalDownloads { get; set; }
        public string Version { get; set; }
        public bool Verified { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
    }
}