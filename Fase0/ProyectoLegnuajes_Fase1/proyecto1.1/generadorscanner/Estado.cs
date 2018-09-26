using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorScanner
{
    public class Estado : IComparable<Estado>
    {
        public Dictionary<string, int> EstadosSiguientes { get; private set; }
        public int? TokenAceptacion { get; private set; }
        public List<int> Hojas { get; private set; }

        public Estado(List<int> hojas, int? tokenAceptacion)
        {
            Hojas = hojas.Distinct().ToList();
            Hojas.Sort();
            EstadosSiguientes = new Dictionary<string, int>();
            TokenAceptacion = tokenAceptacion;
        }

        /// <summary>
        /// Se añade al estado una nueva arista de salida
        /// </summary>
        /// <param name="caracter"></param>
        /// <param name="estadoSiguiente"></param>
        public void AñadirFuncion(string caracter, int estadoSiguiente)
        {
            EstadosSiguientes.Add(caracter, estadoSiguiente);
        }

        public int CompareTo(Estado other)
        {
            return Hojas.SequenceEqual(other.Hojas) ? 0 : -1;
        }
    }
}
