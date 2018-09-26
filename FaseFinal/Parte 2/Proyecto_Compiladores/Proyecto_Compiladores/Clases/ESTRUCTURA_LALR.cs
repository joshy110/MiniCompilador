using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    class ESTRUCTURA_LALR
    {
        //Listas NO temporales
        public List<List<int>> LookA = new List<List<int>>(); 
        public List<List<int>> Produ = new List<List<int>>();
        public List<string> NT_ = new List<string>();

        //Listas TEMPORALES
        public List<List<int>> Produ_Nueva_Tempo = new List<List<int>>();
        public List<List<int>> LA_Nuevo_Tempo = new List<List<int>>();
        public List<string> NT_Nuevo_Tempo = new List<string>();
        public List<string> GoTo = new List<string>();
        public List<int> N_Estado = new List<int>();

        //NÚMERO PARA IR VIENDO ESTADOS
        public int Numero;
    }
}
