using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_Compiladores.Clases
{
    public class ArchivoEntrada
    {
        protected StreamReader archivoEntrada;

        public ArchivoEntrada(string path)
        {
            archivoEntrada = new StreamReader(path);
        }

        public int LeerSiguiente()
        {
            int caracter = archivoEntrada.Read();
            return caracter;
        }

        public void Cerrar()
        {
            archivoEntrada.Close();
        }
    }
}
