using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Scanner
{
    public class ManejoArchivoEntrada
    {
        public int cantidadLeida;
        protected StreamReader archivoEntrada;

        public ManejoArchivoEntrada(string path)
        {
            archivoEntrada = new StreamReader(path);
        }

        public int LeerSiguiente()
        {
            cantidadLeida++;
            int caracter = archivoEntrada.Read();
            if (caracter == '\n')
            {

            }
            return caracter;
        }

        public void Cerrar()
        {
            archivoEntrada.Close();
        }
    }
}
