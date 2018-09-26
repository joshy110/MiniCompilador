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
            archivoEntrada = new StreamReader(path.ToLower());
        }

        public int LeerSiguiente()
        {
            int caracter = archivoEntrada.Read();
            if(caracter >= 65 && caracter <= 90)
            {
                caracter = caracter + 32;
            }
            return caracter;
        }

        public void Cerrar()
        {
            archivoEntrada.Close();
        }
    }
}
