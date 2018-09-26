using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorScanner
{
    public class ParteConjunto
    {
        public bool Rango { get; private set; }
        public int Inicio { get; private set; }
        //En caso no sea un rango, el fin tendra un valor nulo
        public int? Fin { get; private set; }

        public ParteConjunto(bool rango, int inicio, int? fin)
        {
            Rango = rango;
            Inicio = inicio;
            Fin = fin;
        }

        public override string ToString()
        {
            if (Rango)
            {
                return "(Token>=" + Inicio.ToString() + " && Token<=" + Fin.ToString() + ")";
            }
            else
            {
                return "(Token=="+Inicio.ToString()+")";
            }
        }
    }
}
