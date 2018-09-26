using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    public class No_Terminales
    {
        public int Numero_NT;
        public String No_Terminal_NT;
        public List<int> First_NT;
        public int Producion_NT;

        /// <summary>
        /// Constructo de la clase que inicializa los valores a utilizar
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="noTerminal"></param>
        /// <param name="produccion"></param>
        public No_Terminales(int numero, string noTerminal, int produccion)
        {
            this.Numero_NT = numero;
            this.No_Terminal_NT = noTerminal;
            this.Producion_NT = produccion;
            First_NT = new List<int>();
        }
    }
}
