using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorScanner
{
    public class Token
    {
        protected int NumToken { get; private set; }
        public string TokenCompleto { get; set; }
        public Dictionary<int, string> PalabrasReservadas { get; private set; }

        public Token(string tokenCompleto, int numToken)
        {
            NumToken = numToken;
            TokenCompleto = tokenCompleto;
            PalabrasReservadas = new Dictionary<int, string>();
        }

        public void addPalabraReservada(int numPal, string palabra)
        {
            PalabrasReservadas.Add(numPal, palabra);
        }
    }
}
