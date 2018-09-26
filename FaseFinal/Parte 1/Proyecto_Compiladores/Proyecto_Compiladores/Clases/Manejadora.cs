using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    /// <summary>
    /// Clase que es como un Nodo la cual sirve para manejar los valores que se van a agregar a la tabla
    /// </summary>
    class Manejadora
    {
        //Sería el nodo, producción, por la cual se van a poner las reducciones o desplazamientos
        public int Numero_Nodo;
        //Sería el caracter por el cual van los dezplazamientos
        public char Caracter;

        /// <summary>
        /// Constructor de la clase que inicializa los valores
        /// </summary>
        /// <param name="n"></param>
        /// <param name="c"></param>
        public Manejadora(int n, char c)
        {
            Numero_Nodo = n;
            Caracter = c;
        }
    }
}
