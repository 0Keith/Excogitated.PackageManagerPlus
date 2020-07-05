using Excogitated.Common;
using Excogitated.PackageManagerPlus.Models;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excogitated.PackageManagerPlus.Api
{
    public class NugetApi
    {
        public static async Task<List<NugetQueryData>> GetPackages(string query)
        {
            var packages = new List<NugetQueryData>();
            var flurl = "https://azuresearch-usnc.nuget.org/query"
                 .SetQueryParams(new { q = query, take = 0, });
            var json = await flurl.GetStringAsync();
            var expected = Jsonizer.Deserialize<NugetQueryResponse>(json).totalHits;
            const int take = 100;
            var relevance = expected.ToDouble();
            for (var skip = 0; skip < expected; skip += take)
            {
                json = await flurl.SetQueryParams(new { q = query, skip, take, })
                    .GetStringAsync();
                var response = Jsonizer.Deserialize<NugetQueryResponse>(json);
                foreach (var d in response.data)
                    packages.Add(new NugetQueryData
                    {
                        Id = d.id,
                        Relevance = (relevance-- / expected).ToPercentage(),
                        TotalDownloads = d.totalDownloads,
                        Version = d.version,
                        Verified = d.verified,
                        Authors = string.Join(", ", d.authors),
                        Description = d.description,
                    });
            }
            if (packages.Count != expected)
                throw new Exception($"packages.Count:{packages.Count} != expected:{expected}");
            return packages;
        }
    }
}
