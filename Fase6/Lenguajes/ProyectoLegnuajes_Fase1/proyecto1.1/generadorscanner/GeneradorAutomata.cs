using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorScanner
{
    public class GeneradorAutomata
    {
        public Dictionary<int, Token> Tokens { get; private set; }
        private Dictionary<int, int> TokensAceptacion; //hoja, token
        public Dictionary<string, List<ParteConjunto>> Conjuntos { get; private set; }
        private Dictionary<int, string> Hojas;
        public Dictionary<int, List<int>> Follow { get; private set; }
        public List<string[]> Funciones { get; private set; }

        /// <summary>
        /// Constructor de la clase Generador Automata, inicializa todas las variables necesarias
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="conjuntos"></param>
        public GeneradorAutomata(Dictionary<int, Token> tokens, Dictionary<string, List<ParteConjunto>> conjuntos)
        {
            Tokens = tokens;
            if (tokens.Count == 0)
            {
                throw new Exception("No existe tokens para analizar.");
            }
            Hojas = new Dictionary<int, string>();
            Follow = new Dictionary<int, List<int>>();
            Conjuntos = conjuntos;
            Funciones = new List<string[]>();
            CaracteresUtilizados = new List<string>();
            TokensAceptacion = new Dictionary<int, int>();
        }

        /// <summary>
        /// Inicio del analisis que genera el autómata
        /// </summary>
        public void Start()
        {
            Tokens.ElementAt(0).Value.TokenCompleto = "(" + Tokens.ElementAt(0).Value.TokenCompleto;
            for (int i = 0; i < Tokens.Count - 1; i++)
            {
                Tokens.ElementAt(i).Value.TokenCompleto += "#|";
            }
            Tokens.ElementAt(Tokens.Count - 1).Value.TokenCompleto += "#)";


            List<int> firstF = new List<int>();
            List<int> lastF = new List<int>();
            bool nullable = false;
            string cadena = "";
            LeerCaracter();
            ExpresionRegular(ref firstF, ref lastF, ref nullable, ref cadena);
            for (int i = 1; i <= Follow.Count; i++)
            {
                Follow[i] = Follow[i].Distinct().ToList();
            }

            CaracteresUtilizados = Hojas.Values.ToList().Distinct().ToList();

            CreacionTabla(firstF);
        }

        #region Calculo de Follow
        private int preanalisis;
        private int Contador = 0;
        private int NumeroToken = 0;

        /// <summary>
        /// Valida las expresiones regulares pertenecientes a un token
        /// </summary>
        private void ExpresionRegular(ref List<int> firstBef, ref List<int> lastBef, ref bool nullableBef, ref string before)
        {
            List<int> firstI = new List<int>();
            List<int> lastI = new List<int>();
            string inter = "";
            EliminarEspacios();
            bool nullableInt = false;

            switch ((char)preanalisis)
            {
                case '(':
                    {
                        LeerCaracter();//"("
                        ExpresionRegular(ref firstI, ref lastI, ref nullableInt, ref inter);
                        EliminarEspacios();
                        inter = "("+inter+")";
                        LeerCaracter();//")"
                    }
                    break;
                case '\'':
                    {

                        LeerCaracter();//"'"
                        inter += "'";
                        Hojas.Add(Hojas.Count + 1, "'" + ((char)preanalisis).ToString() + "'");
                        Follow.Add(Hojas.Count, new List<int>());
                        firstI.Add(Hojas.Count);
                        lastI.Add(Hojas.Count);
                        inter += (char)preanalisis;
                        LeerCaracter();
                        LeerCaracter();//"'"
                        inter += "'";
                    }
                    break;
                case '"':
                    {
                        LeerCaracter();//'"'
                        inter += "\"";
                        Hojas.Add(Hojas.Count + 1, "'" + ((char)preanalisis).ToString() + "'");
                        Follow.Add(Hojas.Count, new List<int>());
                        firstI.Add(Hojas.Count);
                        lastI.Add(Hojas.Count);
                        inter += (char)preanalisis;
                        LeerCaracter();
                        LeerCaracter();//'"'
                        inter += "\"";
                    }
                    break;
                case '#':
                    {
                        Hojas.Add(Hojas.Count + 1, ((char)preanalisis).ToString());
                        Follow.Add(Hojas.Count, new List<int>());
                        firstI.Add(Hojas.Count);
                        lastI.Add(Hojas.Count);
                        inter += (char)preanalisis;
                        LeerCaracter();
                    }
                    break;
                default:
                    {
                        string id = "";
                        id = VerificarIdentificador();
                        inter += id + " ";
                        Hojas.Add(Hojas.Count + 1, id);
                        Follow.Add(Hojas.Count, new List<int>());
                        firstI.Add(Hojas.Count);
                        lastI.Add(Hojas.Count);
                    }
                    break;
            }

            OperadorRegular(ref firstI, ref lastI, ref nullableInt, ref inter);

            if (firstBef.Count != 0)
            {
                //Se calcula el follow de la concatenación
                for (int i = 0; i < lastBef.Count; i++)
                {
                    Follow[lastBef[i]].AddRange(firstI);
                }

                //Se modifica el Last según las condiciones de una concatenación
                if (nullableInt)
                {
                    lastBef.AddRange(lastI);
                }
                else
                {
                    lastBef = lastI;
                }

                //Se modifica el First según las condiciones de una concatenación
                if (nullableBef)
                {
                    firstBef.AddRange(firstI);
                }

                nullableBef = nullableBef && nullableInt;
                
                CrearFuncion(before, ".", inter, firstBef, lastBef, nullableBef);

                before += inter;
            }
            else
            {
                nullableBef = nullableInt;
                firstBef = firstI;
                lastBef = lastI;
                before += inter;
            }

            ExpresionExtra(ref firstBef, ref lastBef, ref nullableBef, ref before);
        }

        /// <summary>
        /// Verifica si la expresión regular de un token tiene concatenada otra 
        /// regular a si misma, la cual puede estar vacia.
        /// </summary>
        private void ExpresionExtra(ref List<int> firstBef, ref List<int> lastBef, ref bool nullableBef, ref string before)
        {
            List<int> firstAft = new List<int>();
            List<int> lastAft = new List<int>();
            string after = "";
            bool nullableAft = false;

            EliminarEspacios();
            if (preanalisis == '|')
            {
                LeerCaracter();
                ExpresionRegular(ref firstAft, ref lastAft, ref nullableAft, ref after);

                nullableBef = nullableBef || nullableAft;

                firstBef.AddRange(firstAft);
                lastBef.AddRange(lastAft);

                CrearFuncion(before, "|", after, firstBef, lastBef, nullableBef);

                before += "|" + after;
            }
            else
            {
                if (preanalisis == '#' || preanalisis == '\'' || preanalisis == '"' || preanalisis == '(' || Char.IsLetter((char)preanalisis) || preanalisis == '_')
                {
                    ExpresionRegular(ref firstBef, ref lastBef, ref nullableBef, ref before);
                }
            }
        }

        /// <summary>
        /// Verifica si una parte de la expresión regular tiene un operador logico de cerradura.
        /// </summary>
        private void OperadorRegular(ref List<int> first, ref List<int> last, ref bool nullable, ref string cadena)
        {
            EliminarEspacios();
            if ((preanalisis == '?' || preanalisis == '*' || preanalisis == '+'))
            {
                switch (preanalisis)
                {
                    case '?':
                        {
                            nullable = true;
                            break;
                        }
                    case '*': //Su follow es L(c1) =>F(c1)
                        {
                            for (int i = 0; i < last.Count; i++)
                            {
                                Follow[last[i]].AddRange(first);
                            }
                            nullable = true;
                            break;
                        }
                    case '+':
                        {
                            for (int i = 0; i < last.Count; i++)
                            {
                                Follow[last[i]].AddRange(first);
                            }
                            break;
                        }
                }

                CrearFuncion(cadena, ((char)preanalisis).ToString(), "", first, last, nullable);

                cadena += (char)preanalisis;
                LeerCaracter();
                OperadorRegular(ref first, ref last, ref nullable, ref cadena);
            }
        }

        /// <summary>
        /// Toma los datos de un nodo de la recursividad y almacena los datos del respectivo nodo en una lista para su próxima visualización
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="op"></param>
        /// <param name="c2"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="nullable"></param>
        private void CrearFuncion(string c1, string op, string c2, List<int> first, List<int> last, bool nullable)
        {
            string[] funcion = new string[6];
            funcion[0] = c1;
            funcion[1] = op;
            funcion[2] = c2;

            foreach(int f in first)
            {
                funcion[3] += f.ToString() + ",";
            }

            foreach (int l in last)
            {
                funcion[4] += l.ToString() + ",";
            }
            
            funcion[5] = nullable ? "N" : "NN";

            Funciones.Add(funcion);
        }

        /// <summary>
        /// Va leyendo caracteres hasta que dejen de haber espacios, enters o tabulaciones
        /// </summary>
        private void EliminarEspacios()
        {
            while (preanalisis == '\r' || preanalisis == '\n' || preanalisis == '\t' || preanalisis == ' ')
            {
                LeerCaracter();
            }
        }

        /// <summary>
        /// Lee el sigueinte caracter, verificando si se tienen caracteres estancados
        /// </summary>
        private void LeerCaracter()
        {
            if (Contador < Tokens.ElementAt(NumeroToken).Value.TokenCompleto.Length)
            {
                preanalisis = Tokens.ElementAt(NumeroToken).Value.TokenCompleto[Contador];
                Contador++;
            }
            else
            {
                TokensAceptacion.Add(Hojas.Count(), Tokens.ElementAt(NumeroToken).Key);
                NumeroToken++;
                if (NumeroToken<Tokens.Count)
                {
                    Contador = 0;
                    LeerCaracter();
                }
                else
                {
                    preanalisis = 0;
                }
            }
        }

        /// <summary>
        /// Verifica que el identificador del conjunto sea solo de letras, digitos y que no sea una palabra reservada
        /// </summary>
        /// <returns></returns>
        private string VerificarIdentificador()
        {
            EliminarEspacios();
            string cadena = "";
            cadena += (char)preanalisis;
            Letra_();

            while (Char.IsLetterOrDigit((char)preanalisis) || preanalisis == '_')
            {
                cadena += (char)preanalisis;
                LeerCaracter();
            }

            return cadena;
        }

        /// <summary>
        /// Se verifica si el caracter de preanalisis es una letra o un '_'
        /// </summary>
        private void Letra_()
        {
            if (Char.IsLetter((char)preanalisis) || preanalisis == '_')
            {
                LeerCaracter();
            }
            else
            {
                throw new Exception("Error de sintaxis, se esperaba una letra o _");
            }
        }
        #endregion

        #region Tabla de Estados
        private List<string> CaracteresUtilizados;
        private Queue<int> EstadosPendientes;
        public Dictionary<int, Estado> Estados { get; private set; }

        private void CreacionTabla(List<int> first)
        {
            Estados = new Dictionary<int, Estado>();
            CaracteresUtilizados.RemoveAll(CaracterUtilizado => CaracterUtilizado == "#");
            EstadosPendientes = new Queue<int>();
            int cont = 1;
            EstadosPendientes.Enqueue(cont);
            Estados.Add(cont, new Estado(first, BuscarTokenAceptacion(first)));
            cont++;

            while (EstadosPendientes.Count != 0)
            {
                Estado estadoActual = Estados[EstadosPendientes.Dequeue()];
                for (int i = 0; i < CaracteresUtilizados.Count; i++)
                {
                    List<int> hojasDelEstadoNuevo = CalcularHojasDeEstado(estadoActual.Hojas, CaracteresUtilizados[i]);
                    if (hojasDelEstadoNuevo.Count != 0)
                    {
                        Estado estadoNuevo = new Estado(hojasDelEstadoNuevo, BuscarTokenAceptacion(hojasDelEstadoNuevo));
                        Dictionary<int, Estado> estadoEvaluar = Estados.Where(es => es.Value.CompareTo(estadoNuevo) == 0).ToDictionary(es => es.Key, es => es.Value);

                        if (estadoEvaluar.Count == 0)
                        {
                            EstadosPendientes.Enqueue(cont);
                            estadoActual.AñadirFuncion(CaracteresUtilizados[i], cont);
                            Estados.Add(cont, estadoNuevo);
                            cont++;
                        }
                        else
                        {
                            estadoActual.AñadirFuncion(CaracteresUtilizados[i], estadoEvaluar.ElementAt(0).Key);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Revisa si el estado creado tiene un token de aceptación
        /// </summary>
        /// <param name="hojasEstado"></param>
        /// <returns></returns>
        private int? BuscarTokenAceptacion(List<int> hojasEstado)
        {
            int? estado = null;
            int i = 0;
            while (i < hojasEstado.Count && estado == null)
            {
                if (TokensAceptacion.ContainsKey(hojasEstado[i]))
                {
                    estado = TokensAceptacion[hojasEstado[i]];
                }
                i++;
            }

            return estado;
        }

        private List<int> CalcularHojasDeEstado(List<int> hojasUsadas, String caracter)
        {
            List<int> hojasConcordantes = new List<int>();
            for (int i = 0; i < hojasUsadas.Count; i++)
            {
                if (Hojas[hojasUsadas[i]].Equals(caracter))
                {
                    hojasConcordantes.Add(hojasUsadas[i]);
                }
            }

            List<int> hojasResultantes = new List<int>();
            for (int i = 0; i < hojasConcordantes.Count; i++)
            {
                hojasResultantes.AddRange(Follow[hojasConcordantes[i]]);
            }

            return hojasResultantes;
        }
        #endregion
    }
}