using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    public class Token
    {
        public int Numero_Token;
        public String Simbolo_Token;
        public int Prescedencia_Token;
        public String Asociatividad_Token;

        /// <summary>
        /// Consturctor de la clase que maneja los Tokens
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="simbolo"></param>
        /// <param name="prescedencia"></param>
        /// <param name="asociatividad"></param>
        public Token(int numero, string simbolo, int prescedencia, string asociatividad)
        {
            Numero_Token = numero;
            Simbolo_Token = simbolo;
            Prescedencia_Token = prescedencia;
            Asociatividad_Token = asociatividad;
        }
    }
}
