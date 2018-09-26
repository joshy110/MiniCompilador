using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    public class KeyWords
    {
        public int Numero_Token;
        public String Simbolo_Key;

        /// <summary>
        /// Consturctor de la clase que maneja los Tokens
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="simbolo"></param>
        /// <param name="prescedencia"></param>
        /// <param name="asociatividad"></param>
        public KeyWords(int numero, string simbolo)
        {
            Numero_Token = numero;
            Simbolo_Key = simbolo;
        }
    }
}
