using System;
using System.Windows;
using Microsoft.Reporting.WinForms;

namespace Report_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool _isReportViewerLoaded;

        public MainWindow()
        {
            InitializeComponent();
            _reportViewer.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {

                var type = this.GetType();
                this._reportViewer.LocalReport.LoadReportDefinition(type.Assembly.GetManifestResourceStream($"{type.Namespace}.MainReport.rdlc"));
                this._reportViewer.LocalReport.LoadSubreportDefinition("content", type.Assembly.GetManifestResourceStream($"{type.Namespace}.SubReport.rdlc"));
                this._reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubReportHandler);

                _reportViewer.RefreshReport(); //it calls SubReportHandler
                _isReportViewerLoaded = true;
            }
        }

        /// <summary>
        /// Handler for SubReport
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SubReportHandler(object sender, SubreportProcessingEventArgs e)
        {

            var dto = new UserDTO[] {
                new UserDTO() { Name="Bill Gates", Login="bill.gates", Email="bill.gates@outlook.com"  },
                new UserDTO() { Name="Larry Page", Login="larry.page", Email="larry.page@gmail.com"  },
                new UserDTO() { Name="Steve Jobs", Login="steve.jobs", Email="steve.jobs@icloud.com"  },
                new UserDTO() { Name="Steve Ballmer", Login="steve.ballmer", Email="steve.ballmer@microsoft.com"  },
                new UserDTO() { Name="Mark Zuckerberg", Login="mark.zuckerberg", Email="mark.zuckerberg@facebook.com"  },
                new UserDTO() { Name="Richard Stallman", Login="richard.stallman", Email="richard.stallman@imfree.com"  },
                new UserDTO() { Name="Sergey Brin", Login="sergey.brin", Email="sergey.brin@google.com"  },
                new UserDTO() { Name="Linus Torvalds", Login="linus.torvalds", Email="linus.torvalds@linux.com"  },
                new UserDTO() { Name="Gabe Newell", Login="gabe.newell", Email="gabe.newell@valve.com"  }
            };

            ReportDataSource ds = new ReportDataSource("Report_Test", dto);

            if (e.DataSources.Count > 0)
            {
                e.DataSources.RemoveAt(0);
            }

            e.DataSources.Add(ds);
        }
    }
}
