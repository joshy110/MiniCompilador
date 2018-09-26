using System;
using System.Collections.Generic;

namespace GeneradorScanner
{
    public class Analizador
    {
        #region Atributos
        private ManejoArchivoEntrada ArchivoEntrada;
        public  Dictionary<int, Token> TokensFinales { get; private set; }
        private Dictionary<int, string> Reservadas;
        public Dictionary<string, List<ParteConjunto>> Conjuntos { get; private set; }
        private List<int> NumerosUtilizados;
        private Stack<char> CaracteresEstancados;
        private int contadorTokensConjuntos = 0;
        private int linea = 1;
        private int columna = 0;
        protected int preanalisis;
        protected string palabraCompleta = "";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor de la clase Analizador, inicia el sistema
        /// </summary>
        /// <param name="path"></param>
        public Analizador(string path)
        {
            ArchivoEntrada = new ManejoArchivoEntrada(path);
            Reservadas = new Dictionary<int, string>();
            Conjuntos = new Dictionary<string, List<ParteConjunto>>();
            NumerosUtilizados = new List<int>();
            TokensFinales = new Dictionary<int, Token>();
            CaracteresEstancados = new Stack<char>();
            Reservadas.Add(7000, "tokens");
            Reservadas.Add(7001, "token");
            Reservadas.Add(7002, "end");
            Reservadas.Add(7003, "chr");
            preanalisis = ArchivoEntrada.LeerSiguiente();
            Start();
        }
        #endregion

        #region No Terminales
        /// <summary>
        /// Inicio de la gramatica
        /// </summary>
        public void Start()
        {
            try
            {
                EliminarEspacios();
                PalabraReservadaOlbigatoria(Reservadas, 7000);
                VerificarEspacioVacio();
                Interno(); //Conjuntos y Tokens
                EliminarEspacios();
                PalabraReservadaOlbigatoria(Reservadas, 7002);
                if (!(VerificarEspacioNoObligatorio() || preanalisis == 65535))
                {
                    throw new Exception("Error de sintaxis, se esperaba la palabra end. Esta debe ser el final de archivo o tener un enter, espacio o tabulación luego de la misma.");
                }
                
                if (contadorTokensConjuntos == 0)
                {
                    throw new Exception("Error de sintaxis, El archivo debe contener al menos un token o conjunto para ser valido.");
                }

                ArchivoEntrada.Cerrar();
            }
            catch (Exception ex)
            {
                columna -= palabraCompleta.Length;
                ArchivoEntrada.Cerrar();
                throw new Exception(ex.Message + "\nEl sistema fallo en la linea " +  linea + " y columna " + columna);
            }
        }

        /// <summary>
        /// Verifica las posibilidades si viene un token o un conjunto
        /// </summary>
        private void Interno()
        {
            EliminarEspacios();
            palabraCompleta = VerificarIdentificador();
            if (palabraCompleta.ToLower().Equals(Reservadas[7001]))
            {
                palabraCompleta = "";
                VerificarEspacioVacio();
                EliminarEspacios();
                int numeroToken = Nums();
                String Expresion = "(";

                //Se verifica que el número del token no se haya declarado antes como token o palabra reservada
                if (NumerosUtilizados.Contains(numeroToken))
                {
                    throw new Exception("Error de sitaxis, El número " + numeroToken.ToString() + " ya fue utilizado como token o palabra reservada anteriormente.");
                }
                
                NumerosUtilizados.Add(numeroToken);

                EliminarEspacios();
                Coincidir('=');
                EliminarEspacios();
                ExpresionRegular(ref Expresion);
                EliminarEspacios();
                Expresion += ")";
                Token nToken = new Token(Expresion, numeroToken);
                TokensFinales.Add(numeroToken, nToken);

                PalabrasReservadas(numeroToken);
                EliminarEspacios();
                Coincidir(';');
                contadorTokensConjuntos++;
                Interno();
            }
            else
            {
                //Se verifica que el nombre del conjunto no sea una palabra reservada
                if (Reservadas.ContainsValue(palabraCompleta.ToLower()))
                {
                    if (!Reservadas[7002].Equals(palabraCompleta.ToLower()))
                    {
                        throw new Exception("Error de sintaxis, la palabra " + palabraCompleta + " no puede ser utilizada como nombre de conjunto.");
                    }
                    else
                    {
                        RegresarPalabra(palabraCompleta);
                    } 
                }
                else
                {
                    string id = palabraCompleta;
                    //Se verifica que el conjutno no haya sido declarado anteriormente
                    if (Conjuntos.ContainsKey(id.ToLower()))
                    {
                        throw new Exception("Error de sintaxis, el conjunto: " + id + " ya fue declarado con anterioridad.");
                    }
                    palabraCompleta = "";
                    EliminarEspacios();
                    Conjuntos.Add(id.ToLower(), new List<ParteConjunto>());
                    Coincidir('{');
                    Datos(id);
                    EliminarEspacios();
                    Coincidir('}');
                    contadorTokensConjuntos++;
                    Interno();
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
        /// Verifica que los datos de un conjunto sean coreectos
        /// </summary>
        private void Datos(string id)
        {
            EliminarEspacios();
            int inicial, final;
            char x = (char)preanalisis;
            switch ((char)preanalisis)
            {
                case '\'':
                    {
                        LeerCaracter();
                        inicial = preanalisis;
                        LeerCaracter();
                        Coincidir('\'');
                        final = Rangos();
                        DatosExtra(id);
                    }
                    break;
                case '"':
                    {
                        LeerCaracter();
                        inicial = preanalisis;
                        LeerCaracter();
                        Coincidir('"');
                        final = Rangos();
                        DatosExtra(id);
                    }
                    break;
                default:
                    {
                        PalabraReservadaOlbigatoria(Reservadas, 7003);
                        EliminarEspacios();
                        Coincidir('(');
                        EliminarEspacios();
                        inicial = VerificarNumeroAscii();
                        EliminarEspacios();
                        Coincidir(')');
                        final = Rangos();
                        DatosExtra(id);
                    }
                    break;
            }

            //Se crea la nueva parte del conjunto que se logró reconocer
            ParteConjunto newPart;
            if (final != -1)
            {
                if (final <= inicial)
                {
                    throw new Exception("Error de sitaxis, el rango de caracteres indicado no es válida.");
                }

                newPart = new ParteConjunto(true, inicial, final);
            }
            else
            {
                newPart = new ParteConjunto(false, inicial, null);
            }

            Conjuntos[id.ToLower()].Add(newPart);
        }

        /// <summary>
        /// Verifica si los datos de caracteres tienen rangos
        /// </summary>
        private int Rangos()
        {
            int numero;
            EliminarEspacios();
            if (preanalisis == '.')
            {
                LeerCaracter();
                Coincidir('.');
                EliminarEspacios();
                switch((char) preanalisis)
                {
                    case '\'':
                        {
                            LeerCaracter();
                            numero = preanalisis;
                            LeerCaracter();
                            Coincidir('\'');
                        }
                        break;
                    case '"':
                        {
                            LeerCaracter();
                            numero = preanalisis;
                            LeerCaracter();
                            Coincidir('"');
                        }
                        break;
                    default:
                        {
                            PalabraReservadaOlbigatoria(Reservadas, 7003);
                            EliminarEspacios();
                            Coincidir('(');
                            EliminarEspacios();
                            numero = VerificarNumeroAscii();
                            EliminarEspacios();
                            Coincidir(')');
                        }
                        break;
                }
            }
            else
            {
                numero = -1;
            }

            return numero;
        }

        /// <summary>
        /// Verifica si a los datos de un conjunto se le va a ejecutar una unión con más datos.
        /// </summary>
        private void DatosExtra(string id)
        {
            EliminarEspacios();
            if (preanalisis == '+')
            {
                LeerCaracter();
                Datos(id);
            }
        }

        /// <summary>
        /// Verifica una secuencia de números
        /// </summary>
        private int Nums()
        {
            string numero = "" + (char)preanalisis;
            Num();
            while (Char.IsDigit((char)preanalisis))
            {
                numero += (char)preanalisis;
                Num();
            }

            return Convert.ToInt32(numero);
        }

        /// <summary>
        /// Valida las expresiones regulares pertenecientes a un token
        /// </summary>
        private void ExpresionRegular(ref string expresion)
        {
            EliminarEspacios();
            switch ((char)preanalisis)
            {
                case '(':
                    {
                        expresion += "(";
                        LeerCaracter();
                        ExpresionRegular(ref expresion);
                        EliminarEspacios();
                        Coincidir(')');
                        expresion += ")";
                        OperadorRegular(ref expresion);
                        ExpresionExtra(ref expresion);
                    }
                    break;
                case '\'':
                    {
                        
                        LeerCaracter();
                        expresion += "\'" + (char)preanalisis + "\'";
                        LeerCaracter();
                        Coincidir('\'');
                        OperadorRegular(ref expresion);
                        ExpresionExtra(ref expresion);
                    }
                    break;
                case '"':
                    {
                        LeerCaracter();
                        expresion += "\'" + (char)preanalisis + "\'";
                        LeerCaracter();
                        Coincidir('"');
                        OperadorRegular(ref expresion);
                        ExpresionExtra(ref expresion);
                    }
                    break;
                default:
                    {
                        string id = "";
                        try
                        {
                            id = VerificarIdentificador();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Error de sintaxis, error al declarar la expresión regular.");
                        }
                        
                        if (Reservadas.ContainsValue(id.ToLower()))
                        {
                            throw new Exception("Error de sintaxis, la palabra " + id + " no puede ser utilizada en una expresión regular para referise a un conjunto, ya que esta es una palabra reservada.");
                        }

                        if (!Conjuntos.ContainsKey(id.ToLower()))
                        {
                            throw new Exception("Error de sintaxis, el conjunto " + id + " no ha sido declarado anteriormente");
                        }

                        expresion += id + " ";

                        OperadorRegular(ref expresion);
                        ExpresionExtra(ref expresion);
                    }
                    break;
            }
        }

        /// <summary>
        /// Verifica si la expresión regular de un token tiene concatenada otra 
        /// regular a si misma, la cual puede estar vacia.
        /// </summary>
        private void ExpresionExtra(ref string expresion)
        {
            EliminarEspacios();
            if (preanalisis == '|')
            {
                LeerCaracter();
                expresion += "|";
                ExpresionRegular(ref expresion);
            }
            else
            {
                if (preanalisis == '\'' || preanalisis == '"' || preanalisis == '(' || Char.IsLetter((char)preanalisis) || preanalisis == '_')
                {
                    ExpresionRegular(ref expresion);
                }
            }
        }

        /// <summary>
        /// Verifica si una parte de la expresión regular tiene un operador logico de cerradura.
        /// </summary>
        private void OperadorRegular(ref string expresion)
        {
            EliminarEspacios();
            if ((preanalisis == '?' || preanalisis == '*' || preanalisis == '+'))
            {
                expresion += (char)preanalisis;
                LeerCaracter();
                OperadorRegular(ref expresion);
            }
        }

        /// <summary>
        /// Verifica si vienen palabras reservadas al final de un token
        /// </summary>
        private void PalabrasReservadas(int numToken)
        {
            EliminarEspacios();
            if (preanalisis == '{')
            {
                LeerCaracter();
                PalabraReservada(numToken);
                EliminarEspacios();
                Coincidir('}');
            }
        }

        /// <summary>
        /// Verifica cuando debe venir una palabra reservada y su estructura.
        /// </summary>
        private void PalabraReservada(int numToken)
        {
            EliminarEspacios();
            int numPalabra = Nums();

            //Se verifica que el número del token no se haya declarado antes como token o palabra reservada
            if (NumerosUtilizados.Contains(numPalabra))
            {
                throw new Exception("Error de sitaxis, El número " + numPalabra.ToString() + " ya fue utilizado como token o palabra reservada anteriormente.");
            }

            EliminarEspacios();
            Coincidir('=');
            string palabra;
            EliminarEspacios();
            switch ((char)preanalisis)
            {
                case '\'':
                    {
                        LeerCaracter();
                        palabra = Palabra('\'');
                        Coincidir('\'');
                    }
                    break;
                case '"':
                    {
                        LeerCaracter();
                        palabra = Palabra('"');
                        Coincidir('"');
                    }
                    break;
                default:
                    {
                        throw new Exception("Error de sintaxis, se esparaba la definición de una palabra reservada.");
                    }
            }

            NumerosUtilizados.Add(numPalabra);
            TokensFinales[numToken].addPalabraReservada(numPalabra, palabra);
            PalabraExtra(numToken);
        }

        /// <summary>
        /// Verifica si viene una nueva palabra reservada
        /// </summary>
        private void PalabraExtra(int numToken)
        {
            EliminarEspacios();
            if (Char.IsDigit((char)preanalisis))
            {
                PalabraReservada(numToken);
            }
        }
        
        /// <summary>
        /// Lee y verifica una palabra reservada
        /// </summary>
        private string Palabra(char tipoComilla)
        {
            string cadena = "";
            while (preanalisis != tipoComilla && preanalisis != -1)
            {
                cadena += (char)preanalisis;
                LeerCaracter();
            }

            if (cadena.Length== 0)
            {
                throw new Exception("Error de sitaxis, se esperaba la declaración de una palabra reservada.");
            }

            return cadena;
        }
        #endregion

        #region Terminales
        /// <summary>
        /// Verifica si el caracter encontrado es el esperado.
        /// </summary>
        /// <param name="t"></param>
        private void Coincidir(char t)
        {
            if (preanalisis==t)
            {
                LeerCaracter();
            }
            else
            {
                throw new Exception("Error de sintaxis, se esperaba el caracter " + t);
            }
        }

        /// <summary>
        /// Se verifica si el caracter de preanalisis es un digito, de ser así se lee el siguiente digito
        /// </summary>
        private void Num()
        {
            if (Char.IsDigit((char)preanalisis))
            {
                LeerCaracter();
            }
            else
            {
                throw new Exception("Error de sintaxis, se esperaba un número");
            }
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

        #region Otros
        /// <summary>
        /// Verifica si se coloco una palabra reservada obligatoria
        /// </summary>
        /// <param name="diccionarioBase"></param>
        /// <param name="llave"></param>
        private void PalabraReservadaOlbigatoria(Dictionary<int, string> diccionarioBase, int llave)
        {
            palabraCompleta = VerificarIdentificador();
            if (!diccionarioBase[llave].Equals(palabraCompleta.ToLower()))
            {
                throw new Exception("Error de sintaxis, se esperaba la palabra reservada: " + diccionarioBase[llave]);
            }

            palabraCompleta = "";
        }

        /// <summary>
        /// Lee el sigueinte caracter, verificando si se tienen caracteres estancados
        /// </summary>
        private void LeerCaracter()
        {
            preanalisis = (CaracteresEstancados.Count == 0) ? ArchivoEntrada.LeerSiguiente() : CaracteresEstancados.Pop();
            if (preanalisis == '\n')
            {
                linea++;
                columna = -1;
            }
            else
            {
                columna++;
            }
        }

        /// <summary>
        /// Verifica si viene un espacio, enter o tabulación obligatorio
        /// </summary>
        private void VerificarEspacioVacio()
        {
            if (preanalisis == '\r' || preanalisis == '\n' || preanalisis == '\t' || preanalisis == ' ')
            {
                LeerCaracter();
            }
            else
            {
                throw new Exception("Error de sintaxis, se esperaba un espacio vacío, enter o tabulación.");
            }
        }

        /// <summary>
        /// Verifica si vino un espacio que se esperaba.
        /// </summary>
        /// <returns></returns>
        private bool VerificarEspacioNoObligatorio()
        {
            return (preanalisis == '\r' || preanalisis == '\n' || preanalisis == '\t' || preanalisis == ' ');
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
        /// Alamacena una secuencia de caracteres que ya se leyo 
        /// en la cola de caracteres estancados
        /// </summary>
        /// <param name="cadena"></param>
        private void RegresarPalabra(string cadena)
        {
            CaracteresEstancados.Push((char)preanalisis);
            for (int i = cadena.Length - 1; i >= 0; i--)
            {
                columna--;

                CaracteresEstancados.Push(cadena[i]);
            }

            LeerCaracter();

            palabraCompleta = "";
        }

        /// <summary>
        /// Verifica si la siguiente secuencia de digitos esta entre 0 y 255
        /// </summary>
        private int VerificarNumeroAscii()
        {
            int numero = Nums();
            if (!(numero >= 0 && numero <= 255))
            {
                throw new Exception("Error de sintaxis, el valor entre la palabra chr() \n debe de ser mayor o igual a 0 y menor o igual a 255");
            }

            return numero;
        }
        #endregion
    }
}