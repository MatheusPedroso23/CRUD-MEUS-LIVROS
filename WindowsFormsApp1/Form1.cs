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
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();
            Inicializar();
        }



        private void Inicializar()
        {
            dt = Livro.GetLivros();
            dgvLivros.DataSource = dt;
            ConfigurarGradeLivros();

        }

        private void ConfigurarGradeLivros() 

        {
            dgvLivros.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgvLivros.DefaultCellStyle.Font = new Font("Arial", 9);
            dgvLivros.RowHeadersWidth = 25;

            dgvLivros.Columns["id"].HeaderText = "ID";
            dgvLivros.Columns["id"].Visible = false;

            dgvLivros.Columns["isbn"].HeaderText = "ISBN";
            dgvLivros.Columns["isbn"].Width = 130;
            dgvLivros.Columns["isbn"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLivros.Columns["isbn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvLivros.Columns["titulo"].HeaderText = "Título do livro";
            dgvLivros.Columns["titulo"].Width = 450;
            dgvLivros.Columns["titulo"].DefaultCellStyle.Padding = new Padding(5,0,0,0);

            dgvLivros.Columns["autores"].HeaderText = "Autores do livro";
            dgvLivros.Columns["autores"].Width = 500;
            dgvLivros.Columns["autores"].DefaultCellStyle.Padding = new Padding(5, 0, 0, 0);

            dgvLivros.Columns["unitario"].HeaderText = "Unitario";
            dgvLivros.Columns["unitario"].Width= 90;
            dgvLivros.Columns["unitario"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLivros.Columns["unitario"].DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;
            dgvLivros.Columns["unitario"].DefaultCellStyle.Padding = new Padding(0, 0, 12, 0);
            dgvLivros.Columns["unitario"].DefaultCellStyle.Format = "N2";

            dgvLivros.Columns["saldo_atual"].HeaderText = "Saldo";
            dgvLivros.Columns["saldo_atual"].Width = 70;
            dgvLivros.Columns["saldo_atual"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLivros.Columns["saldo_atual"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvLivros.Columns["saldo_atual"].DefaultCellStyle.Format = "N0";

            dgvLivros.Sort(dgvLivros.Columns["titulo"], ListSortDirection.Ascending);





        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dgvLivros.Rows[dgvLivros.CurrentCell.RowIndex].Cells["id"].Value);

            using(var frm = new FrmLivrosCadastro(id))
            {
                frm.ShowDialog();
                dgvLivros.DataSource = Livro.GetLivros();
                ConfigurarGradeLivros();
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            

            using (var frm = new FrmLivrosCadastro(0))
            {
                frm.ShowDialog();
                dgvLivros.DataSource = Livro.GetLivros();
                ConfigurarGradeLivros();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dt = Livro.GetLivros(txtProcurar.Text);
            dgvLivros.DataSource = dt;
            ConfigurarGradeLivros();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(dgvLivros.Rows[dgvLivros.CurrentCell.RowIndex].Cells["id"].Value);

            using (var frm = new FrmLivrosCadastro(id, true))
            {
                frm.ShowDialog();
                dgvLivros.DataSource = Livro.GetLivros();
                ConfigurarGradeLivros();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            var dt = GerarDadosRelatorio();
            using(var frm = new FrmLivrosRelatorio(dt, txtProcurar.Text))
            {
                frm.ShowDialog();
            }
        }

        private DataTable GerarDadosRelatorio()
        {
            var dt = new DataTable();
            dt.Columns.Add("isbn");
            dt.Columns.Add("titulo");
            dt.Columns.Add("autores");
            dt.Columns.Add("unitario", typeof(decimal));

            foreach (DataGridViewRow row in dgvLivros.Rows)
            {
                 string isbn = row.Cells["isbn"].Value.ToString();
                 string titulo = row.Cells["titulo"].Value.ToString();
                 string autores = row.Cells["autores"].Value.ToString();
                 decimal unitario = Convert.ToDecimal(row.Cells["unitario"].Value);

                dt.Rows.Add(isbn, titulo, autores, unitario);
            }

            return dt;
        }


        private void dgvLivros_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
