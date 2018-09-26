using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GeneradorScanner
{
    public class ManejoArchivoSalida
    {
        StreamWriter Escritor;

        public ManejoArchivoSalida(string path)
        {
            Escritor = new StreamWriter(path);
        }

        public void EscribirLinea(string dato)
        {
            Escritor.WriteLine(dato);
        }

        public void EscribirContiguo(string dato)
        {
            Escritor.Write(dato);
        }

        public void CerrarArchivo()
        {
            Escritor.Close();
        }
    }
}
