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
            this.Produccion_PR = produ;
            this.Longitud_PR = longi;
            this.Siguiente_PR = next;
            this.Elementos = new List<int>();
            this.NoTerminal_PR = noTerminal;
        }

    }
}
