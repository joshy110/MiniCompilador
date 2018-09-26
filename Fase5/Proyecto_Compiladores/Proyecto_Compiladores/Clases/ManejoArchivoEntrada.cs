using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_Compiladores.Clases
{
    public class ManejoArchivoEntrada
    {
        public String  CargaArchivo(String ruta, String txt)
        {
            StreamReader lector = new StreamReader(ruta);
            txt = lector.ReadToEnd();

            //Se crear un arreglo que contendra las líneas del archivo de texto
            string[] lineas = File.ReadAllLines(ruta);
            txt = String.Join("\n", lineas);
            lector.Close();
            return txt;
        }
    }
}
