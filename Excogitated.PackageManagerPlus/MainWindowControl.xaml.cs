using Excogitated.Common;
using Excogitated.PackageManagerPlus.Api;
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
                var results = await System.Threading.Tasks.Task.Run(() => NugetApi.GetPackages(query));
                QueryGrid.ItemsSource = results;
                if (QueryGrid.Items.SortDescriptions.Count == 0)
                {
                    QueryGrid.Items.IsLiveSorting = true;
                    QueryGrid.Items.SortDescriptions.Add(new SortDescription
                    {
                        Direction = ListSortDirection.Descending,
                        PropertyName = nameof(NugetQueryData.TotalDownloads),
                    });
                    var column = QueryGrid.Columns
                        .Where(c => c.Header is string h && h == nameof(NugetQueryData.TotalDownloads))
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

}