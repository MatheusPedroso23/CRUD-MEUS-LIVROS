using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmLivrosCadastro : Form
    {
        int id;
        bool excluir = false;
        Livro livro = new Livro();
        public FrmLivrosCadastro(int id, bool excluir = false)
        {
            InitializeComponent();
            this.id = id;
            this.excluir = excluir;

            if(this.id > 0 )
            {
                livro.GetLivro(this.id);

                lblId.Text = livro.ID.ToString();
                txtIsbn.Text = livro.Isbn;
                txtTitulo.Text = livro.Titulo;
                txtAutor.Text = livro.Autores;
                txtPrecoUnitario.Text = livro.Unitario.ToString("N2");
                txtSaldoInicial.Text = livro.Saldo_inicial.ToString();
                txtEstoqueMinimo.Text = livro.Estoque_minimo.ToString();
                if(livro.Ativo == 'S')
                    chkAtivo.Checked = true;
            }

            if (this.excluir)
            {
                TravarControles();
                btnSalvar.Visible = false;
                btnExcluir.Visible = true;
            }

        }

        private void TravarControles()
        {
            txtIsbn.Enabled = false;
            txtTitulo.Enabled = false;
            txtAutor.Enabled = false;
            txtPrecoUnitario.Enabled = false;
            txtSaldoInicial.Enabled = false;
            txtEstoqueMinimo.Enabled = false;
            chkAtivo.Enabled = false;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarForm())
            {
                livro.Isbn = txtIsbn.Text;
                livro.Titulo = txtTitulo.Text;
                livro.Autores = txtAutor.Text;
                livro.Unitario = Convert.ToDecimal("0" + txtPrecoUnitario.Text);
                livro.Saldo_inicial = Convert.ToInt32("0" + txtSaldoInicial.Text);
                livro.Estoque_minimo = Convert.ToInt32("0" + txtEstoqueMinimo.Text);
                if (chkAtivo.Checked == true)
                    livro.Ativo = 'S';
                else
                    livro.Ativo = 'N';

                livro.SalvarLivro();
                this.Close();
            }
        }

        private bool ValidarForm()
        {
            if(txtIsbn.Text == "")
            {
                MessageBox.Show("Preencha os campos", Program.sistema);
                txtIsbn.Focus();
                return false;
            }
            else if (txtTitulo.Text == "")
            {
                MessageBox.Show("Informe o título do livro", Program.sistema);
                txtTitulo.Focus();
                return false;
            }
            else if (txtAutor.Text == "")
            {
                MessageBox.Show("Informe o Autor do livro", Program.sistema);
                txtAutor.Focus();
                return false;
            }
            else if (txtTitulo.Text == "")
            {
                MessageBox.Show("Informe o título do livro", Program.sistema);
                txtTitulo.Focus();  
                return false;
            }
            else if (Convert.ToDecimal("0" + txtPrecoUnitario.Text) == 0)
            {
                MessageBox.Show("Informe o preço do livro", Program.sistema);
                txtPrecoUnitario.Focus();
                return false;
            }
            else
                return true;
            
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            livro.Excluir();
            this.Close();
        }
    }
}
