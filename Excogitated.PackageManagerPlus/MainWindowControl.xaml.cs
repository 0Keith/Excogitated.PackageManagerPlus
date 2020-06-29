using Excogitated.Common;
using Excogitated.PackageManagerPlus.Models;
using Flurl;
using Flurl.Http;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Excogitated.PackageManagerPlus
{
    /// <summary>
    /// Interaction logic for MainWindowControl.
    /// </summary>
    public partial class MainWindowControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowControl"/> class.
        /// </summary>
        public MainWindowControl()
        {
            InitializeComponent();
            QueryBox.KeyUp += QueryBox_KeyUp;
            QueryButton.Click += QueryButton_Click;
        }

        private void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            ExecuteQuery();
        }

        private void QueryBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                ExecuteQuery();
        }

        private async void ExecuteQuery()
        {
            try
            {
                var query = QueryBox.Text?.Trim();
                var results = await System.Threading.Tasks.Task.Run(async () =>
                {
                    var flurl = "https://azuresearch-usnc.nuget.org/query".SetQueryParams(new
                    {
                        q = query,
                        take = 0,
                    });
                    var json = await flurl.GetStringAsync();
                    var response = Jsonizer.Deserialize<NugetQueryResponse>(json);
                    json = await flurl.SetQueryParams(new
                    {
                        q = query,
                        take = response.totalHits,
                    }).GetStringAsync();
                    response = Jsonizer.Deserialize<NugetQueryResponse>(json);
                    return response.data.Select(d => new QueryGridRow
                    {
                        Id = d.id,
                        TotalDownloads = d.totalDownloads,
                        Version = d.version,
                        Verified = d.verified,
                        Authors = string.Join(", ", d.authors),
                        Description = d.description,
                    }).OrderByDescending(r => r.TotalDownloads).ToList();
                });
                QueryGrid.ItemsSource = results;
                if (QueryGrid.Items.SortDescriptions.Count == 0)
                {
                    QueryGrid.Items.IsLiveSorting = true;
                    QueryGrid.Items.SortDescriptions.Add(new SortDescription
                    {
                        Direction = ListSortDirection.Descending,
                        PropertyName = nameof(QueryGridRow.TotalDownloads),
                    });
                    var column = QueryGrid.Columns
                        .Where(c => c.Header is string h && h == nameof(QueryGridRow.TotalDownloads))
                        .First();
                    column.SortDirection = ListSortDirection.Descending;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }

    internal class QueryGridRow
    {
        public string Id { get; set; }
        public int TotalDownloads { get; set; }
        public string Version { get; set; }
        public bool Verified { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
    }
}