using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scann
{
    public class ManejoArchivoEntrada
    {
        protected StreamReader archivoEntrada;

        public ManejoArchivoEntrada(string path)
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
