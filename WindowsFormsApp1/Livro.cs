﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Livro
    {
        public int ID   { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autores { get; set; }
        public decimal Unitario { get; set; }
        public int Estoque_minimo { get; set; }
        public int Saldo_inicial { get; set; }
        public DateTime Data_saldo_inicial { get; set; }
        public int Saldo_atual { get; set; }
        public char Ativo { get; set; }

        public void GetLivro(int id)
        {
            var sql = "SELECT * FROM livros WHERE id=" + id;
            try
            {
                using(var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using(var cmd = new MySqlCommand(sql, cn))
                    {
                        using(var dr = cmd.ExecuteReader())
                        {
                            if(dr.HasRows)
                            {
                                if(dr.Read())
                                {
                                    this.ID = Convert.ToInt32(dr["id"]);
                                    this.Isbn = dr["isbn"].ToString();
                                    this.Titulo = dr["titulo"].ToString();
                                    this.Autores = dr["autores"].ToString();
                                    this.Unitario = Convert.ToDecimal(dr["unitario"]);
                                    this.Estoque_minimo = Convert.ToInt32(dr["estoque_minimo"]);
                                    this.Saldo_inicial = Convert.ToInt32(dr["saldo_inicial"]);
                                    this.Saldo_atual = Convert.ToInt32(dr["saldo_atual"]);
                                    this.Ativo = Convert.ToChar(dr["ativo"]);

                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }

        public void SalvarLivro()
        {
            var sql = "";

            if (this.ID == 0)
                sql = "INSERT INTO livros (isbn, titulo, autores, unitario, estoque_minimo, saldo_inicial, saldo_atual, ativo) VALUES (@isbn, @titulo, @autores, @unitario, @estoque_minimo, @saldo_inicial, @saldo_atual, @ativo)";
            else
                sql = "UPDATE livros SET isbn=@isbn, titulo=@titulo, autores=@autores, unitario=@unitario, estoque_minimo=@estoque_minimo, saldo_inicial=@saldo_inicial, saldo_atual=@saldo_atual, ativo=@ativo WHERE id=" + this.ID;


            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using(var cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@isbn", this.Isbn);
                        cmd.Parameters.AddWithValue("@titulo", this.Titulo);
                        cmd.Parameters.AddWithValue("@autores", this.Autores);
                        cmd.Parameters.AddWithValue("@unitario", this.Unitario);
                        cmd.Parameters.AddWithValue("@estoque_minimo", this.Estoque_minimo);
                        cmd.Parameters.AddWithValue("@saldo_inicial", this.Saldo_inicial);
                        cmd.Parameters.AddWithValue("@saldo_atual", this.Saldo_atual);
                        cmd.Parameters.AddWithValue("@ativo", this.Ativo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message );
            }

        }

        public void Excluir()
        {
            var sql = "DELETE FROM livros WHERE id=" + this.ID;
            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var cmd = new MySqlCommand(sql, cn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public static DataTable GetLivros(string procurar = "") 
        { 
            var dt = new DataTable();

            var sql = "SELECT id, isbn, titulo, autores, unitario, saldo_atual FROM livros.livros";

            if (procurar != "")
                sql += " WHERE titulo LIKE '%" + procurar + "%' OR autores LIKE '%" + procurar + "%'";

            try
            {
                using (var cn = new MySqlConnection(Conn.strConn))
                {
                    cn.Open();
                    using (var da = new MySqlDataAdapter(sql, cn))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }

    }
}
