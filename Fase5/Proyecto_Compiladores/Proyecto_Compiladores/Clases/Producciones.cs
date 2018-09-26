using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    public class Producciones
    {
        public int Produccion_PR;
        public int Longitud_PR;
        public int Siguiente_PR;
        public List<int> Elementos;
        public string NoTerminal_PR;

        /// <summary>
        /// Constructor de la clase que inicializa los valores
        /// </summary>
        public Producciones(int produ, int longi, int next, string noTerminal)
        {
            Produccion_PR = produ;
            Longitud_PR = longi;
            Siguiente_PR = next;
            Elementos = new List<int>();
            NoTerminal_PR = noTerminal;
        }

    }
}
