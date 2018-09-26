using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    class NodoTablaSimbolos
    {
        /*Variables Globales a usar*/
        public string Nombre;
        public string QueTipo;
        public string EsMetodo;
        public List<int> Cant_Parametros;
        public List<string> VariableValor;
        public List<string> VariableNombre;
        public List<string> VariableGPS;
        public List<string> VariableTipo;
        public List<int> Tamanon;
        public List<NodoTablaSimbolos> LosHijos;

        /// <summary>
        /// Constructor de la tabla del nodo de la tabla de símbolos
        /// </summary>
        public NodoTablaSimbolos()
        {
            LosHijos = new List<NodoTablaSimbolos>();
            VariableValor = new List<string>();
            VariableNombre = new List<string>();
            VariableGPS = new List<string>();
            VariableTipo = new List<string>();
            Tamanon = new List<int>();
            Cant_Parametros = new List<int>();
        }
    }
}
