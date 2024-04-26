using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmLivrosRelatorio : Form
    {
        DataTable dt = new DataTable();
        string subTitulo;
        public FrmLivrosRelatorio(DataTable dt, string subTitulo)
        {
            InitializeComponent();
            this.dt = dt;
            this.subTitulo = subTitulo;
        }

        private void FrmLivrosRelatorio_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt));
            if (subTitulo != "")
            {
                this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("subTitulo", "Filtrado por: " + subTitulo));
                this.reportViewer1.RefreshReport();
            }
            else {
                this.reportViewer1.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("subTitulo", "Filtrado por: Todos "));
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
