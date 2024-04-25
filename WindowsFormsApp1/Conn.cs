using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    static class Conn
    {

        static private string servidor = "localhost";
        static private string bancoDados = "livros";
        static private string usuario = "root";
        static private string senha = " ";

        static public string strConn = $"server={servidor};User Id={usuario};database={bancoDados}; password={senha}";

    }
}
