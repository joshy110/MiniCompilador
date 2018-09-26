using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    class Nodo_Tabla
    {
        public List<Manejadora> Lista_Nodos = new List<Manejadora>();
        public int Fila;
        public int Columna;

        /// <summary>
        /// Constructor de la Clase que inicializa el Nodo a utilizar
        /// </summary>
        /// <param name="n"></param>
        /// <param name="c"></param>
        /// <param name="fil"></param>
        /// <param name="col"></param>
        public Nodo_Tabla(int n, char c, int fil, int col)
        {
            Manejadora nuevo_Manejador = new Manejadora(n, c);
            Lista_Nodos.Add(nuevo_Manejador);
            Fila = fil;
            Columna = col;
        }
    }
}
