using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    public class Analizador
    {
        #region Variables Globales 

        String ArregloCompleto;
        public String MsjoError, Nombre_Compi;

        bool Caracter, Comillas, Apostrofe, Conjunto, Vino_Produ_Start;
        bool Viene_Check, Vino_Token, Aceptar_Token, Vino_PIPE, HayValor, Acc_Token, Vino_L, Vino_R, Vino_Algo_Antes_Llave, Vino_Producciones_Titulo;
        bool Existen_Producciones, Vino_TO, Vino_Comment, YaInicio, LogroCerrar, def_coment, Vino_Comentario_ESP;

        char[] ArregloCompleto_Chars;
        int x, y; //Pos_Token;
        int PosError, PosActual, Defi_Nueva, Fin_CHR, HAY_Parentesis, OtraProduccion, Cont_Token, Presedencia_Token, No_NT, Cont_Produ, Pos_NT;
        public int Vino_Start;
        int[] rango;

        public List<string> Lista_Units;
        public List<string> Lista_Conjuntos;
        public List<Token> Lista_Tokens;
        public List<No_Terminales> Lista_NT;
        List<string> Lista_Alias_Coments;
        public List<string> Lista_KeyWords;
        public List<Producciones> Lista_Producciones;
        List<string> Elementos;
        public List<bool> Lista_Check;
        public List<string> Lista_Estructura_Conjuntos;
        public List<string> Lista_Estructura_Tokens;
        public List<string> Lista_Inalcanzables;

        #endregion

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Analizador()
        {
            MsjoError = "";
            PosError = 0;
            PosActual = 0;
            x = 0;
            y = 0;
            rango = new int[2];
            rango[0] = rango[1] = -1;
            Viene_Check = false;
            Vino_Token = false;
            Vino_Comentario_ESP = false;
            Vino_Producciones_Titulo = false;
            Lista_Units = new List<string>();
            Lista_Conjuntos = new List<string>();
            Lista_Tokens = new List<Token>();
            Lista_KeyWords = new List<string>();
            Lista_Producciones = new List<Producciones>();
            Lista_Alias_Coments = new List<string>();
            Lista_NT = new List<No_Terminales>();
            Elementos = new List<string>();
            Lista_Estructura_Conjuntos = new List<string>();
            Lista_Estructura_Tokens = new List<string>();
            Lista_Check = new List<bool>();
            Lista_Inalcanzables = new List<string>();
            Vino_Produ_Start = false;
        }

        /// <summary>
        /// Función que inicializa el análisis
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool InicioAnalisi(String texto)
        {
            ArregloCompleto = texto;
            ArregloCompleto_Chars = texto.ToCharArray();
            x = ArregloCompleto.Length;
            y = ArregloCompleto_Chars.Length;
            if (Analiza_Titulo(texto)) //Compiler Joshy
            {
                if (PosActual < texto.Length)
                {
                    if (Existe_Unidades(texto.Substring(PosActual)))  //Son las UNITS
                    {
                        if (PosActual < texto.Length)
                        {
                            int unidad = Encontro_Unidades(texto.Substring(PosActual)); //Va revisar la cantidad de unidades existentes
                            if (unidad == 0)
                            {
                                return Manejo_Error("No se puede nombrar una unidad con una palabra reservada.", PosActual + PosError);
                            }
                            if (unidad == 1)
                            {
                                if (!Analizar_Unidades(texto.Substring(PosActual)))
                                {
                                    return false;
                                }
                            }
                            if (unidad == 2)
                            {
                                return Manejo_Error("Se esperaba una unidad.", PosActual + PosError);
                            }
                            if (unidad == 3)
                            {
                                return Manejo_Error("Se debe de definir al menos una unidad.", PosActual + PosError);
                            }
                        }
                        else
                        {
                            return Manejo_Error("La definición del compilador está incompleta", texto.Length - 1);
                        }
                    }
                    if (Existe_Conjuntos(texto.Substring(PosActual))) //Los conjutnos pueden o no venir son los SETS D
                    {
                        if (Analiza_Conjuntos(texto.Substring(PosActual)))
                        {
                            Vino_Producciones_Titulo = false;
                            return true; //Se debe de agregar aca lo del end                            
                        }
                        else
                        {
                            return false;
                        }
                    }
                    if (PosActual < texto.Length)
                    {
                        if (Analiza_Tokens(texto.Substring(PosActual))) //Son los TOKENS
                        {
                            Vino_Producciones_Titulo = false;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return Manejo_Error("La definición del compilador está incompleta", texto.Length - 1);
                    }
                }
                else
                {
                    return Manejo_Error("La definición del compilador está incompleta", texto.Length - 1);
                }
            }
            return false;
        }

        /// <summary>
        /// Función que devuelve si logro analizar el título del archivo de manera correcta
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private bool Analiza_Titulo(String texto)
        {
            int nTempo = OmitirEspacios(texto, 0); //Contador que me sirve para saber en donde voy 

            if (Char.IsLetter(texto[nTempo]))
            {
                String tempoPalabra = Obtener_Palabra(texto, nTempo);
                if (tempoPalabra.ToLower() == "compiler")
                {
                    nTempo = OmitirEspacios(texto, nTempo + 8);

                    if (Char.IsLetter(texto[nTempo])) //Se obtiene el nombre que se le dio al compilador
                    {
                        Nombre_Compi = Obtener_Palabra(texto, nTempo);
                        int punto = Encontro_Punto_Final(texto, nTempo + Nombre_Compi.Length);

                        if (punto == -1)
                        {
                            return Manejo_Error("Se esperaba un punto", PosActual + nTempo + Nombre_Compi.Length);
                        }
                        else
                        {
                            PosActual = punto + 1;
                            return true;
                        }
                    }
                    else
                    {
                        return Manejo_Error("Se esperaba el nombre del compilador", nTempo + Nombre_Compi.Length);
                    }
                }
                else
                {
                    return Manejo_Error("Se esperaba el nombre del compilador", nTempo + 1 + tempoPalabra.Length);
                }
            }
            else
            {
                return Manejo_Error("Se esperaba el nombre del compilador", nTempo);
            }
        }

        /// <summary>
        /// Función que determina si encontro un punto
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private int Encontro_Punto_Final(String texto, int pos)
        {
            for (int i = pos; i < texto.Length; i++)
            {
                if (texto[i] == '.')
                {
                    return i;
                }
                else
                {
                    if (!Char.IsWhiteSpace(texto[i]))
                    {
                        return -1;
                    }
                }
            }
            return -1;
        }

        #region Units

        /// <summary>
        /// Función que analiza si exite la palabra "units" o no
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private bool Existe_Unidades(String texto)
        {
            int nTempo = OmitirEspacios(texto, 0);

            if (Char.IsLetter(texto[nTempo]))
            {
                string palabra = Obtener_Palabra(texto, nTempo);

                if (palabra.ToLower() == "units")
                {
                    PosActual += palabra.Length + nTempo;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Función que valida si encontro una unidad valida
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private int Encontro_Unidades(String texto)
        {
            int nTempo = OmitirEspacios(texto, 0);
            PosError = 0;
            if (Char.IsLetter(texto[nTempo]))
            {
                string palabra = Obtener_Palabra(texto, nTempo);
                if (Es_Reservada(palabra)) //Si se encuentra en las reservadas encontrara error ya que una unidad no puede ser una palabra reservada
                {
                    PosError = nTempo;
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                if (texto[nTempo] == '.')
                {
                    PosError = nTempo;
                    return 3;
                }
                else
                {
                    return 2;
                }
            }
        }

        /// <summary>
        /// Función que analiza si viene una unidad válida
        /// </summary>
        /// <returns></returns>
        private bool Analizar_Unidades(String texto)
        {
            int nTempo = OmitirEspacios(texto, 0);
            int terminoUnidades = texto.IndexOf('.');
            if (terminoUnidades == -1)
            {
                return Manejo_Error("Se esperaba punto al final de las unidades", PosActual + nTempo);
            }
            string[] unidadesTempo = texto.Substring(nTempo, terminoUnidades - nTempo).Split(',');
            int posunidad = 0;
            for (int i = 0; i < unidadesTempo.Length; i++)
            {
                string nombreU = unidadesTempo[i].TrimStart(); //Me tira el string actual 
                nombreU = nombreU.TrimEnd();
                if (!EsPalabra(nombreU) || Es_Reservada(nombreU) || nombreU.IndexOf(' ') != -1)
                {
                    return Manejo_Error("La unidad no esta definida correctamente. Esto se puede deber a: \n 1. "
                        + nombreU + " pertenece a las palabras reservada. \n 2. No viene punto final", PosActual + posunidad + nTempo + nombreU.Length);
                }
                Lista_Units.Add(nombreU); //Se agrega a la lista
                posunidad += unidadesTempo[i].Length + 1;
            }
            PosActual += terminoUnidades + 1;
            return true;
        }

        /// <summary>
        /// Función que valida si es una palabra o no
        /// </summary>
        /// <param name="palabra"></param>
        /// <returns></returns>
        private bool EsPalabra(String palabra)
        {
            if (palabra == "")
            {
                return false;
            }
            else
            {
                if (Char.IsLetter(palabra[0]))
                {
                    for (int i = 0; i < palabra.Length; i++)
                    {
                        if (!Char.IsLetterOrDigit(palabra[i]) || palabra[i] == '_')
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region Sets

        /// <summary>
        /// Función que verifica que exista la palabra sets la cual inicializa los conjuntos
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private bool Existe_Conjuntos(String texto)
        {
            int nTempo = OmitirEspacios(texto, 0);

            if (Char.IsLetter(texto[nTempo]))
            {
                string palabra = Obtener_Palabra(texto, nTempo);

                if (palabra.ToLower() == "sets")
                {
                    PosActual += palabra.Length + nTempo;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return Manejo_Error("Se esperaba Sets", PosActual + nTempo);
            }
        }

        /// <summary>
        /// Función que analiza si existe la palabra sets
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private bool Analiza_Conjuntos(String texto)
        {
            int nTempo = OmitirEspacios(texto, 0);
            if (nTempo == -1)
            {
                return Manejo_Error("La definición del compilador esta incompleta", PosActual);
            }
            else
            {
                PosActual += nTempo;
                if (Char.IsLetter(texto[nTempo]))
                {
                    string palabra = Obtener_Palabra(texto, nTempo);
                    if (Es_Reservada(palabra))
                    {
                        if (palabra.ToLower() == "tokens")
                        {
                            PosActual += nTempo + palabra.Length;
                            Cont_Token = 1;
                            Presedencia_Token = 1;
                            return Validar_Tokens(texto.Substring(nTempo + 6));
                        }
                        else
                        {
                            return Manejo_Error("La palabra " + palabra + "esta reservada", nTempo);
                        }
                    }
                    else
                    {
                        texto = texto.Substring(nTempo + palabra.Length);
                        if (Lista_Conjuntos.Contains(palabra))
                        {
                            return Manejo_Error("El conjunto ya se definio previamente", PosActual);
                        }
                        PosActual += palabra.Length;
                        nTempo = OmitirEspacios(texto, 0);
                        PosActual += nTempo;
                        if (texto[nTempo] == '=')
                        {
                            nTempo++;
                            nTempo = OmitirEspacios(texto, nTempo);
                            switch (texto[nTempo])
                            {
                                case '\'':
                                    {
                                        Caracter = Comillas = Apostrofe = false;
                                        rango[0] = rango[1] = -1;
                                        int final = Obtener_Final_Conjunto(texto);
                                        Lista_Conjuntos.Add(palabra.ToLower());
                                        if (Valida_Conjunto(texto, nTempo, final))
                                        {
                                            PosActual += Defi_Nueva + 1;
                                            Lista_Estructura_Conjuntos.Add(texto.Substring(nTempo, final - 3));
                                            return Analiza_Conjuntos(texto.Substring(final + 1));
                                        }
                                        return false;
                                    }
                                case '\"':
                                    {
                                        Caracter = Comillas = Apostrofe = false;
                                        rango[0] = rango[1] = -1;
                                        int final = Obtener_Final_Conjunto(texto);
                                        Lista_Conjuntos.Add(palabra.ToLower());
                                        if (Valida_Conjunto(texto, nTempo, final))
                                        {
                                            PosActual += Defi_Nueva + 1;
                                            Lista_Estructura_Conjuntos.Add(texto.Substring(nTempo, final - 3));
                                            return Analiza_Conjuntos(texto.Substring(final + 1));
                                        }
                                        return false;
                                    }
                                case 'c':
                                    {
                                        Caracter = Comillas = Apostrofe = false;
                                        rango[0] = rango[1] = -1;
                                        int final = Obtener_Final_Conjunto(texto);
                                        Lista_Conjuntos.Add(palabra.ToLower());
                                        if (Valida_Conjunto(texto, nTempo, final))
                                        {
                                            PosActual += Defi_Nueva + 1;
                                            Lista_Estructura_Conjuntos.Add(texto.Substring(nTempo, final - 3));
                                            return Analiza_Conjuntos(texto.Substring(final + 1));
                                        }
                                        return false;
                                    }
                                case 'C':
                                    {
                                        Caracter = Comillas = Apostrofe = false;
                                        rango[0] = rango[1] = -1;
                                        int final = Obtener_Final_Conjunto(texto);
                                        Lista_Conjuntos.Add(palabra.ToLower());
                                        if (Valida_Conjunto(texto, nTempo, final))
                                        {
                                            PosActual += Defi_Nueva + 1;
                                            Lista_Estructura_Conjuntos.Add(texto.Substring(nTempo, final - 3));
                                            return Analiza_Conjuntos(texto.Substring(final + 1));
                                        }
                                        return false;
                                    }
                                default:
                                    return Manejo_Error("El conjunto ingresado no es valido.", PosActual);
                            }
                        }
                        else
                        {
                            return Manejo_Error("Se esperaba un signo igual.", PosActual);
                        }

                    }
                }
                else
                {
                    return Manejo_Error("Se debe ingresar un Set", PosActual + nTempo);
                }
            }
        }

        /// <summary>
        /// Función que obtiene el final del conjunto
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private int Obtener_Final_Conjunto(String texto)
        {
            int cont_Puntos = 0;
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == '.')
                {
                    cont_Puntos++;
                    string y = texto[i + 1].ToString();
                    if (texto[i + 1] == '.' || texto[i + 1] == '\'' || texto[i + 1] == '\"' || texto[i + 1] == '+' || texto[i + 1] == 'c' || texto[i + 1] == 'C')
                    {
                        cont_Puntos++;
                        if (cont_Puntos == 2)
                        {
                            cont_Puntos = 0;
                        }
                    }
                    else
                    {
                        if (!Char.IsNumber(texto[i - 1]) || texto[i] == '\'')
                        {
                            string x = texto[i].ToString();
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Función que realiza la validación de los conjuntos
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos_chr_actual"></param>
        /// <param name="pos_chr_final"></param>
        /// <returns></returns>
        private bool Valida_Conjunto(String texto, int pos_chr_actual, int pos_chr_final)
        {
            if (texto != "")
            {
                if (pos_chr_actual <= pos_chr_final)
                {
                    pos_chr_actual = OmitirEspacios(texto, pos_chr_actual);
                    switch (texto[pos_chr_actual])
                    {
                        case '\'':
                            {
                                if ((pos_chr_actual + 1) < texto.Length)
                                {
                                    if (texto[pos_chr_actual + 2] == '\'')
                                    {
                                        if (rango[0] == -1 && rango[1] == -1)
                                        {
                                            rango[0] = texto[pos_chr_actual + 1];
                                            Apostrofe = Conjunto = Caracter = true; //Hay conjunto válido.
                                            return Valida_Conjunto(texto, pos_chr_actual + 3, pos_chr_final);
                                        }
                                        else
                                        {
                                            if (Apostrofe)
                                            {
                                                if (rango[1] == -1)
                                                {
                                                    rango[1] = texto[pos_chr_actual + 1];
                                                }
                                                if (rango[1] >= rango[0])
                                                {
                                                    Caracter = Comillas = Apostrofe = Conjunto = false;
                                                    rango[0] = rango[1] = -1;
                                                    return Valida_Conjunto(texto, pos_chr_actual + 3, pos_chr_final);
                                                }
                                                else
                                                {
                                                    return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                                }
                                            }
                                            else
                                            {
                                                return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return Manejo_Error("Se esperaba una comilla simple. \n Por ello el conjunto se definio de forma incorrecta", PosActual + pos_chr_actual);
                                    }
                                }
                                else
                                {
                                    return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                }
                            }
                        case '\"':
                            {
                                if ((pos_chr_actual + 1) < texto.Length)
                                {
                                    if (texto[pos_chr_actual + 2] == '\"')
                                    {
                                        if (rango[0] == -1 && rango[1] == -1)
                                        {
                                            rango[0] = texto[pos_chr_actual + 1];
                                            Apostrofe = Conjunto = Caracter = true; //Hay conjunto válido.
                                            return Valida_Conjunto(texto, pos_chr_actual + 3, pos_chr_final);
                                        }
                                        else
                                        {
                                            if (Apostrofe)
                                            {
                                                if (rango[1] == -1)
                                                {
                                                    rango[1] = texto[pos_chr_actual + 1];
                                                }
                                                if (rango[1] >= rango[0])
                                                {
                                                    Caracter = Comillas = Apostrofe = Conjunto = false;
                                                    rango[0] = rango[1] = -1;
                                                    return Valida_Conjunto(texto, pos_chr_actual + 3, pos_chr_final);
                                                }
                                                else
                                                {
                                                    return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                                }
                                            }
                                            else
                                            {
                                                return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return Manejo_Error("Se esperaba una comilla simple. \n Por ello el conjunto se definio de forma incorrecta", PosActual + pos_chr_actual);
                                    }
                                }
                                else
                                {
                                    return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                }
                            }
                        case '.':
                            {
                                if ((pos_chr_actual + 1) < texto.Length)
                                {
                                    if (texto[pos_chr_actual + 1] == '.')
                                    {
                                        if (rango[0] != -1)
                                        {
                                            return Valida_Conjunto(texto, pos_chr_actual + 2, pos_chr_final);
                                        }
                                        else
                                        {
                                            return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                        }
                                    }
                                    else
                                    {
                                        if (rango[1] == -1)
                                        {
                                            if (!Validar_FinToken_Punto(texto, pos_chr_actual))
                                            {
                                                return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                            }
                                            else
                                            {
                                                if (texto[pos_chr_actual + 1] == '\'' || texto[pos_chr_actual + 1] == '\"')
                                                {

                                                    return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                                }
                                                Defi_Nueva = pos_chr_actual;
                                                rango[0] = rango[1] = -1;
                                                Comillas = Apostrofe = Caracter = Conjunto = false;
                                                return true;
                                            }
                                        }
                                        else
                                        {
                                            return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                        }
                                    }
                                }
                                else
                                {
                                    return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                }
                            }

                        case '+':
                            {
                                if (rango[1] == -1)
                                {
                                    Apostrofe = Comillas = Caracter = false;
                                    rango[0] = -1;
                                    return Valida_Conjunto(texto, pos_chr_actual + 1, pos_chr_final);
                                }
                                else
                                {
                                    return Manejo_Error("No se definio un conjunto de forma correcta.", pos_chr_actual + PosActual);
                                }
                            }
                        case 'c':
                            {
                                int chr_Num = Obtiene_CHR(texto, pos_chr_actual);
                                if (chr_Num != -1)
                                {
                                    if (rango[0] == -1 && rango[1] == -1)
                                    {
                                        Caracter = Conjunto = true;
                                        rango[0] = chr_Num;
                                        char tempo = (char)chr_Num;
                                        return Valida_Conjunto(texto, Fin_CHR + 1, pos_chr_final);
                                    }
                                    else
                                    {
                                        if (Caracter)
                                        {
                                            rango[1] = chr_Num;
                                            if (rango[1] >= rango[0])
                                            {
                                                if (rango[1] > 255)
                                                {
                                                    return Manejo_Error("El rango ingresado no es valido.", PosActual + pos_chr_actual);
                                                }
                                                rango[0] = rango[1] = -1;
                                                Caracter = Comillas = Apostrofe = Conjunto = false;
                                                return Valida_Conjunto(texto, Fin_CHR + 1, pos_chr_final);
                                            }
                                            else
                                            {
                                                return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                            }
                                        }
                                        else
                                        {
                                            return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                        }
                                    }
                                }
                                else
                                {
                                    return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                }

                            }
                        case 'C':
                            {
                                int chr_Num = Obtiene_CHR(texto, pos_chr_actual);
                                if (chr_Num != -1)
                                {
                                    if (rango[0] == -1 && rango[1] == -1)
                                    {
                                        Caracter = Conjunto = true;
                                        rango[0] = chr_Num;
                                        char tempo = (char)chr_Num;
                                        return Valida_Conjunto(texto, Fin_CHR + 1, pos_chr_final);
                                    }
                                    else
                                    {
                                        if (Caracter)
                                        {
                                            rango[1] = chr_Num;
                                            if (rango[1] >= rango[0])
                                            {
                                                rango[0] = rango[1] = -1;
                                                Caracter = Comillas = Apostrofe = Conjunto = false;
                                                return Valida_Conjunto(texto, Fin_CHR + 1, pos_chr_final);
                                            }
                                            else
                                            {
                                                return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                            }
                                        }
                                        else
                                        {
                                            return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                        }
                                    }
                                }
                                else
                                {
                                    return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                                }

                            }
                        default:
                            return Manejo_Error("Se esperaba un signo igual", PosActual);
                    }
                }
                else
                {
                    if (!Conjunto && rango[1] == -1)
                    {
                        Defi_Nueva = PosActual;
                        rango[0] = -1;
                        Comillas = Apostrofe = Caracter = Conjunto = false;
                        return true;
                    }
                    else
                    {
                        return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
                    }
                }
            }
            else
            {
                return Manejo_Error("No se definio un conjunto de forma correcta.", PosActual + pos_chr_actual);
            }
        }

        /// <summary>
        /// Función que obtiene el lenght del chr 
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos_actual"></param>
        /// <returns></returns>
        public int Obtiene_CHR(String texto, int pos_actual)
        {
            if ((pos_actual + 2) < texto.Length)
            {
                string x = texto[pos_actual].ToString();
                string chr = texto[pos_actual].ToString() + texto[pos_actual + 1].ToString() + texto[pos_actual + 2].ToString();
                if (chr.ToLower() == "chr")
                {
                    for (int i = pos_actual + 3; i < texto.Length; i++)
                    {
                        if (texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                        {
                            if (texto[i] == '(')
                            {
                                int patito = Fin_Parentesis_CHR(texto, i + 1);
                                if (patito != -1)
                                {
                                    string numeroCHR = texto.Substring(i + 1, patito - i - 1);
                                    if (Es_Numero(numeroCHR))
                                    {
                                        numeroCHR = Quita_Espacios_Tontos(numeroCHR);
                                        Fin_CHR = patito;
                                        return int.Parse(numeroCHR);
                                    }

                                }
                                return -1;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Función que me dice el número donde encuentra al paréntesis de cierre
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int Fin_Parentesis_CHR(String texto, int pos)
        {
            for (int i = pos; i < texto.Length; i++)
            {
                if (texto[i].Equals(')') && Char.IsNumber(texto[i - 1]))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Función que verifica si es número
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public bool Es_Numero(string texto)
        {
            int pos = -1;
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                {
                    pos = i; //Encontro la pos
                    break;
                }
            }
            string patito = "";
            int n_temp = -1;
            if (pos != -1)
            {
                for (int i = pos; i < texto.Length; i++)
                {
                    if (Char.IsDigit(texto[i]))
                    {
                        patito += texto[pos];
                    }
                    else
                    {
                        n_temp = i;
                    }
                }
            }
            else
            {
                return false;
            }

            if (n_temp != -1)
            {
                for (int i = n_temp; i < texto.Length; i++)
                {
                    if (texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Función que quita espacios inncesesarios
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public string Quita_Espacios_Tontos(string texto)
        {
            int i = 0;
            string tempo = "";
            while (i < texto.Length)
            {
                if (texto[i] != ' ' && texto[i] != '\t' && texto[i] != '\n')
                {
                    tempo += texto[i];
                }
                i++;
            }
            return tempo;
        }

        /// <summary>
        /// Función que valida si se termino de leer el conjunto
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool Validar_FinToken_Punto(string texto, int pos)
        {
            string patita = texto[pos].ToString();
            for (int i = pos; i < texto.Length; i++)
            {
                if (texto[i] != ' ' && texto[i] != '\t' && texto[i] != '\n')
                {
                    if (texto[i] == '\'' || texto[i] == '\"' || texto[i] == '.')
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        #endregion

        #region Tokens

        /// <summary>
        /// Función que analiza si existen tokens
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private bool Analiza_Tokens(String texto)
        {
            int nTempo = OmitirEspacios(texto, 0);
            string sss = texto[nTempo].ToString();
            if (Char.IsLetter(texto[nTempo]))
            {
                string palabra = Obtener_Palabra(texto, nTempo);
                if (palabra.ToLower() == "tokens")
                {
                    PosActual += nTempo + palabra.Length;
                    Cont_Token = 1;
                    Presedencia_Token = 1;
                    return Validar_Tokens(texto.Substring(nTempo + 6));
                }
                else
                {
                    return Manejo_Error("Se esperaba la palabra Tokens", PosActual + nTempo + palabra.Length);
                }
            }
            else
            {
                return Manejo_Error("Se esperaba la palabra Tokens", PosActual + nTempo);
            }
        }

        /// <summary>
        /// Función que valida el analisis de tokens
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private bool Validar_Tokens(string texto)
        {
            int nTempo = OmitirEspacios(texto, 0);
            if (nTempo == -1)
            {
                return Manejo_Error("La definición del compilador es incompleta.", PosActual);
            }
            else
            {
                PosActual += nTempo;
                if (texto[nTempo] == '\'' || texto[nTempo] == '\"')
                {
                    if (Validar_Operador(texto.Substring(nTempo), 0))
                    {
                        Aceptar_Token = true;
                        Vino_R = false;
                        Vino_L = false;
                        Presedencia_Token++;
                        //Posible contador de presecendia
                        return Validar_Tokens(texto.Substring(Defi_Nueva + 1 + nTempo));
                    }
                    return false;
                }
                else if (Char.IsLetter(texto[nTempo]))
                {
                    string palabra = Obtener_Palabra(texto, nTempo);
                    if (palabra.ToLower() == "keywords")
                    {
                        if (Viene_Check)
                        {
                            PosActual += palabra.Length;
                            if (Validar_KeyWord(texto.Substring(nTempo + palabra.Length), 0))
                            {
                                return Validar_Tokens(texto.Substring(texto.IndexOf('.') + 1));
                            }
                        }
                        else
                        {
                            return Manejo_Error("No se es posible definir una keyword sin que exista la palabra Check.", PosActual);
                        }
                    }
                    else if (palabra.ToLower() == "comments")
                    {
                        Vino_TO = Vino_Comment = YaInicio = Vino_Comentario_ESP = false;
                        PosActual += nTempo;
                        if (Validar_Comentarios(texto.Substring(nTempo), 0))
                        {
                            return Validar_Tokens(texto.Substring(texto.IndexOf('.') + 1));
                        }

                    }
                    else if (palabra.ToLower() == "productions")
                    {
                        PosActual += palabra.Length;
                        Existen_Producciones = false;
                        Vino_Producciones_Titulo = true;
                        No_NT = 2;
                        Cont_Produ = 1;
                        //PosActual = texto.IndexOf(palabra) + 11;
                        if (Analiza_Producctions(texto.Substring(nTempo + palabra.Length)))
                        {
                            Se_Definio_Start(); //Se arregla la producción de start
                            return true; //Aca iria el Fin archivo
                        }
                        return false;
                    }
                    else if (Es_Reservada(palabra))
                    {
                        return Manejo_Error("La palabra: " + palabra + " pertenece a las reservadas.", nTempo);
                    }
                    else
                    {
                        texto = texto.Substring(nTempo + palabra.Length);
                        if (SeDefinio_Token(palabra))
                        {
                            return Manejo_Error("El token ya se definio previamente", PosActual);
                        }
                        PosActual += palabra.Length;
                        nTempo = OmitirEspacios(texto, 0);
                        PosActual += nTempo;
                        switch (texto[nTempo])
                        {
                            case '=':
                                {
                                    Vino_PIPE = HayValor = false;
                                    //Lista_Tokens.Add(palabra);
                                    Token nuevoToken = new Token(Cont_Token, palabra.ToLower(), 0, "ES_T");
                                    Lista_Tokens.Add(nuevoToken);
                                    Lista_Check.Add(false);
                                    if (Verificar_Toknes(texto.Substring(nTempo + 1), 0)) /*TERMINAR MÁS TARDE*/
                                    {
                                        PosActual += Defi_Nueva + 1 + nTempo;
                                        Cont_Token++;
                                        Lista_Estructura_Tokens.Add(texto.Substring(nTempo + 1, Defi_Nueva));
                                        return Validar_Tokens(texto.Substring(Defi_Nueva + 2 + nTempo));
                                    }
                                    return false;
                                }
                            default:
                                return Manejo_Error("El token esta mal definido porque se esperaba un signo igual", PosActual - palabra.Length);
                        }
                    }
                }
                else if (!Aceptar_Token)
                {
                    return Manejo_Error("Se debe de ingresar un Token.", PosActual + nTempo);
                }
                else if (Vino_Producciones_Titulo == false)
                {
                    return Manejo_Error("Se debe de definir el nombre de 'PRODUCTIONS'.", PosActual);
                }
                return false;
            }
        }

        /// <summary>
        /// Función que valida si la palabra analizada ya se encuentra entre los tokens
        /// </summary>
        /// <param name="palabra"></param>
        /// <returns></returns>
        public bool SeDefinio_Token(string palabra)
        {
            for (int i = 0; i < Lista_Tokens.Count(); i++)
            {
                if (Lista_Tokens[i].Simbolo_Token.ToLower().Equals(palabra.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Función que valida que los tokens esten correctos
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Verificar_Toknes(string texto, int pos)
        {
            HAY_Parentesis = 0;
            Acc_Token = false;
            if (Validar_Expression_Token(pos, texto, pos))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Función que valida la expresión que contiene el token
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Validar_Expression_Token(int posinic, string texto, int posfin)
        {
            if (texto != "")
            {
                if (posinic < texto.Length)
                {
                    posinic = OmitirEspacios(texto, posinic);
                    switch (texto[posinic])
                    {
                        case '\'':
                            {
                                if ((posinic + 2) < texto.Length)
                                {
                                    if ((texto[posinic + 2] == '\'' && texto[posinic + 1] != '\'') || texto[posinic + 1] == '\'')
                                    {
                                        HayValor = true;
                                        PIPE();
                                        Defi_Nueva = posinic + 2;
                                        if (Vino_PIPE)
                                        {
                                            Vino_PIPE = false;
                                        }
                                        return Validar_Expression_Token(posinic + 3, texto, posfin);
                                    }
                                }
                                return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                            }
                        case '\"':
                            {
                                if ((posinic + 2) < texto.Length)
                                {
                                    string tontito = texto[posinic + 2].ToString();
                                    string tontito2 = texto[posinic + 1].ToString();
                                    if (texto[posinic + 2] == '\"' && texto[posinic + 1] != '\"')
                                    {
                                        HayValor = true;
                                        PIPE();
                                        Defi_Nueva = posinic + 2;
                                        if (Vino_PIPE)
                                        {
                                            Vino_PIPE = false;
                                        }
                                        return Validar_Expression_Token(posinic + 3, texto, posfin);
                                    }
                                }
                                return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                            }
                        case '(':
                            {
                                HAY_Parentesis++; //Quiere decir que la expresión dle token contiene parentesis
                                string lineaTempo = texto; //Me va servir solo cuando este debugeando
                                if (texto.Length >= (posinic + 1))
                                {
                                    if (texto[posinic + 1] != ')')
                                    {
                                        bool tempo = false;
                                        for (int i = posinic + 1; i < texto.Length; i++)
                                        {
                                            if (texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                                            {
                                                switch (texto[i])
                                                {
                                                    case '\'':
                                                        {
                                                            tempo = true;
                                                            break;
                                                        }
                                                    case '\"':
                                                        {
                                                            tempo = true;
                                                            break;
                                                        }
                                                    case '(':
                                                        {
                                                            tempo = true;
                                                            break;
                                                        }
                                                    case '[':
                                                        {
                                                            tempo = true;
                                                            break;
                                                        }
                                                }
                                                if (!tempo)
                                                {
                                                    if (Char.IsLetter(texto[i]))
                                                    {
                                                        tempo = true;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        if (!tempo)
                                        {
                                            return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                                        }
                                        else
                                        {
                                            return Validar_Expression_Token(posinic + 1, texto, posfin);
                                        }
                                    }
                                }
                                return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);

                            }
                        case ')':
                            {
                                HAY_Parentesis--;
                                if (HAY_Parentesis < 0)
                                {
                                    return Manejo_Error("El token no fue definidio correctamente", PosActual + posinic);
                                }
                                else
                                {
                                    HayValor = true;
                                    PIPE();
                                    return Validar_Expression_Token(posinic + 1, texto, posfin);
                                }
                            }
                        case '*':
                            {
                                if (AnalizarSimbolo(posinic, texto, posfin))
                                {
                                    HayValor = true;
                                    return Validar_Expression_Token(posinic + 1, texto, posfin);
                                }
                                return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                            }
                        case '+':
                            {
                                if (AnalizarSimbolo(posinic, texto, posfin))
                                {
                                    HayValor = true;
                                    return Validar_Expression_Token(posinic + 1, texto, posfin);
                                }
                                return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                            }
                        case '?':
                            {
                                if (AnalizarSimbolo(posinic, texto, posfin))
                                {
                                    HayValor = true;
                                    return Validar_Expression_Token(posinic + 1, texto, posfin);
                                }
                                return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                            }
                        case '.': //Fin al token
                            {
                                if (AnalizarSimbolo(posinic, texto, posfin))
                                {
                                    if (HAY_Parentesis == 0)
                                    {
                                        if (!Vino_PIPE)
                                        {
                                            Defi_Nueva = posinic;
                                            HayValor = true;
                                            return true;
                                        }
                                    }
                                }
                                return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                            }
                        case '|':
                            {
                                if (HayValor)
                                {
                                    if (!Acc_Token)
                                    {
                                        bool tempo = false;
                                        for (int i = posinic + 1; posinic < texto.Length; i++)
                                        {
                                            if (texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                                            {
                                                switch (texto[i])
                                                {
                                                    case '\'':
                                                        {
                                                            tempo = true;
                                                            break;
                                                        }
                                                    case '\"':
                                                        {
                                                            tempo = true;
                                                            break;
                                                        }
                                                    case '(':
                                                        {
                                                            tempo = true;
                                                            break;
                                                        }
                                                    case '[':
                                                        {
                                                            tempo = true;
                                                            break;
                                                        }
                                                }
                                                if (!tempo)
                                                {
                                                    if (Char.IsLetter(texto[i]))
                                                    {
                                                        tempo = true;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                        }

                                        if (!tempo)
                                        {
                                            return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                                        }
                                        else
                                        {
                                            Vino_PIPE = true;
                                            return Validar_Expression_Token(posinic + 1, texto, posfin);
                                        }
                                    }
                                    else
                                    {
                                        return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                                    }
                                }
                                else
                                {
                                    return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                                }
                            }
                        default:
                            {
                                if (Char.IsLetter(texto[posinic]))
                                {
                                    string palabrita = Obtener_Palabra(texto, posinic);
                                    if (palabrita.ToLower() == "check")
                                    {
                                        posinic = OmitirEspacios(texto, posinic + 5);
                                        if (texto[posinic] == '.') //Quiere decir que después del check termino todo
                                        {
                                            if (HAY_Parentesis == 0)
                                            {
                                                Viene_Check = true;
                                                Defi_Nueva = posinic;
                                                Lista_Check[Lista_Check.Count - 1] = true;
                                                return true;
                                            }
                                        }
                                        return Manejo_Error("Se esperaba un punto para terminar la definción del token.", PosActual + posinic);
                                    }
                                    else if (Es_Reservada(palabrita))
                                    {
                                        return Manejo_Error("No puede utiilzar una palabra reservada en la definción de un token.", PosActual + posinic);
                                    }
                                    else if (Lista_Conjuntos.Contains(palabrita.ToLower()))
                                    {
                                        HayValor = true;
                                        Defi_Nueva = posinic + palabrita.Length;
                                        if (Vino_PIPE)
                                        {
                                            Vino_PIPE = false;
                                        }
                                        return Validar_Expression_Token(posinic + palabrita.Length, texto, posfin);
                                    }
                                    else
                                    {
                                        return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic + palabrita.Length);
                                    }
                                }
                                else
                                {
                                    return Manejo_Error("El token no fue definido correctamente.", PosActual + posinic);
                                }
                            }

                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return Manejo_Error("El token no fue definido correctamente", PosActual + posinic);
            }
        }

        /// <summary>
        /// Función utilizad para validar el símbolo antes del valor especial (*,|,.,?,+)
        /// </summary>
        /// <param name="inicio"></param>
        /// <param name="texto"></param>
        /// <param name="fin"></param>
        /// <returns></returns>
        private bool AnalizarSimbolo(int inicio, string texto, int fin)
        {
            for (int i = inicio - 1; i >= fin; i--)
            {
                if (texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                {
                    switch (texto[i]) //Se analiza los parentesis y comillas de cierre
                    {
                        case ')':
                            return true;
                        case '\'':
                            return true;
                        case '"':
                            return true;
                    }
                    if (Char.IsLetterOrDigit(texto[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Método utilizado para validar un PIPE
        /// </summary>
        private void PIPE()
        {
            if (Vino_PIPE)
            {
                Vino_PIPE = false;
            }
        }

        /// <summary>
        /// Función que valida que los operadores sean correctos
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Validar_Operador(string texto, int pos)
        {
            if (Char.IsLetter(texto[pos]))
            {
                string palabra = Obtener_Palabra(texto, pos);
                if (palabra.ToLower() == "left")
                {
                    pos = OmitirEspacios(texto, pos + palabra.Length);
                    if (texto[pos] == '.') //Termino el operador exitosamente
                    {
                        PosActual += pos + 1;
                        Defi_Nueva = pos;
                        Vino_L = true;
                        Analiza_Asociatividad(palabra.ToLower());
                        return true;
                    }
                    else
                    {
                        return Manejo_Error("Se esperaba un punto.", PosActual + pos);
                    }
                }
                else if (palabra.ToLower() == "right")
                {
                    pos = OmitirEspacios(texto, pos + palabra.Length);
                    if (texto[pos] == '.') //Termino el operador exitosamente
                    {
                        PosActual += pos + 1;
                        Defi_Nueva = pos;
                        Vino_R = true;
                        Analiza_Asociatividad(palabra.ToLower());
                        Presedencia_Token++;
                        return true;
                    }
                    else
                    {
                        return Manejo_Error("Se esperaba un punto.", PosActual + pos);
                    }
                }
            }
            else if (texto[pos] == '\"')
            {
                pos = OmitirEspacios(texto, pos + 1);
                int i;
                for (i = pos; i < texto.Length; i++)
                {
                    if (texto[i] == '\"')
                    {
                        string[] tempo = texto.Substring(pos, i - pos).Split(' ');
                        Token nuevoToken = new Token(Cont_Token, '\'' + tempo[0].ToLower() + '\'', Presedencia_Token, "");
                        Lista_Tokens.Add(nuevoToken);
                        // Lista_KeyWords.Add(tempo[0].ToString());
                        if (tempo.Length > 1)
                        {
                            return Manejo_Error("El operador analizado no esta definido correctamente.", PosActual + pos);
                        }
                        else
                        {
                            pos = OmitirEspacios(texto, i + 1);
                            Cont_Token++;
                            if (Char.IsLetter(texto[pos]))
                            {
                                return Validar_Operador(texto, pos);
                            }
                            else if (texto[pos] == ',') //Quiere decir que viene otro "j" , "l"
                            {
                                pos = OmitirEspacios(texto, pos + 1);
                                return Validar_Operador(texto, pos);
                            }
                            else if (texto[pos] == '.') //se llego a un punto 
                            {
                                //if (Vino_L == false || Vino_R == false)
                                //{
                                //    return Manejo_Error("No viene la palabra left o rigth para definir una keyword valida", PosActual + pos);
                                //}
                                PosActual += pos + 1;
                                Defi_Nueva = pos;
                                Analiza_Asociatividad("no_hay");
                                return true;
                            }
                            return Manejo_Error("Se esperaba una coma ','", PosActual + pos);
                        }
                    }
                }
                if (i == texto.Length)
                {
                    return Manejo_Error("El operador analizado no esta definido correctamente.", PosActual + i);
                }
            }
            else if (texto[pos] == '\'')
            {
                pos = OmitirEspacios(texto, pos + 1);
                int i;
                for (i = pos; i < texto.Length; i++)
                {
                    if (texto[i] == '\'')
                    {
                        string[] tempo = texto.Substring(pos, i - pos).Split(' ');
                        Token nuevoToken = new Token(Cont_Token, '\'' + tempo[0].ToLower() + '\'', Presedencia_Token, "");
                        Lista_Tokens.Add(nuevoToken);
                        //Lista_KeyWords.Add(tempo[0].ToString());
                        if (tempo.Length > 1)
                        {
                            return Manejo_Error("El operador analizado no esta definido correctamente.", PosActual + pos);
                        }
                        else
                        {
                            pos = OmitirEspacios(texto, i + 1);
                            Cont_Token++;
                            if (Char.IsLetter(texto[pos]))
                            {
                                return Validar_Operador(texto, pos);
                            }
                            else if (texto[pos] == ',') //Quiere decir que viene otro "j" , "l"
                            {
                                pos = OmitirEspacios(texto, pos + 1);
                                return Validar_Operador(texto, pos);
                            }
                            else if (texto[pos] == '.') //se llego a un punto 
                            {
                                //if (Vino_L == false || Vino_R == false)
                                //{
                                //    return Manejo_Error("No viene la palabra left o rigth para definir una keyword valida", PosActual + pos);
                                //}
                                Analiza_Asociatividad("no_hay");
                                PosActual += pos + 1;
                                Defi_Nueva = pos;
                                return true;
                            }
                            return Manejo_Error("Se esperaba una coma ','", PosActual + pos);
                        }
                    }
                }
                if (i == texto.Length)
                {
                    return Manejo_Error("El operador analizado no esta definido correctamente.", PosActual + i);
                }
            }
            else if (texto[pos] == '.')
            {
                if (Fin_Validar_Operador(texto, pos))
                {
                    if (Vino_L == false || Vino_R == false)
                    {
                        return Manejo_Error("No viene la palabra left o rigth para definir una keyword valida", PosActual + pos);
                    }
                    PosActual += pos + 1;
                    Defi_Nueva = pos;
                    Analiza_Asociatividad("no_hay");
                    return true;
                }
                return Manejo_Error("Se encontro un erro cuando se trato de definir el operador.", PosActual + pos);
            }
            return Manejo_Error("Se encontro un erro cuando se trato de definir el operador.", PosActual + pos);
        }

        /// <summary>
        /// Función que determina si se termino de analizar los operadores
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Fin_Validar_Operador(string texto, int pos)
        {
            for (int i = pos; i < texto.Length; i++)
            {
                if (!Char.IsWhiteSpace(texto[pos]))
                {
                    switch (texto[i])
                    {
                        case '\"':
                            {
                                return true;
                            }
                        case '\'':
                            {
                                return true;
                            }
                        default:
                            return false;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Métodoq que analiza la asociatividad de la palabra left o right
        /// </summary>
        /// <param name="palabra"></param>
        private void Analiza_Asociatividad(string palabra)
        {
            //Se va regresando hasta agregarle a todos los toknes su respectiva asociatividad
            for (int i = Lista_Tokens.Count - 1; i >= 0; i--)
            {
                if (Lista_Tokens[i].Asociatividad_Token == "")
                {
                    Lista_Tokens[i].Asociatividad_Token = palabra; //Se le asigna la asociatividad
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Función que valida si la keyword analizada es válida
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Validar_KeyWord(string texto, int pos)
        {
            pos = OmitirEspacios(texto, pos);
            if (pos == -1)
            {
                return Manejo_Error("Error al momento de definir los Keywords.", PosActual);
            }
            else
            {
                if (texto[pos] == '\"')
                {
                    pos = OmitirEspacios(texto, pos + 1);
                    int temporal = pos; //Variable Local Patita
                    string palabra = Obtener_Palabra(texto, pos);
                    pos = OmitirEspacios(texto, pos + palabra.Length);
                    if (texto[pos] == '\"')
                    {
                        if (EsPalabra(palabra))
                        {
                            //for(int i = 0; i < Lista_Tokens.Count; i ++)
                            //{
                            //    if (Lista_Tokens[i].Simbolo_Token.Contains(palabra.ToLower()))
                            //    {
                            //        return Manejo_Error("Se esperaba una coma o un punto para generar una keyword valida.", PosActual + pos);
                            //    }
                            //}
                            int temporal_token = Lista_Tokens[Lista_Tokens.Count - 1].Numero_Token;
                            Token nuevoToken = new Token(temporal_token + 1, '\'' + palabra.ToLower() + '\'', 0, "");
                            Lista_Tokens.Add(nuevoToken);
                            Lista_KeyWords.Add(palabra.ToLower());
                            pos = OmitirEspacios(texto, pos + 1);
                            if (texto[pos] == '.') //Finalizaron las keywords
                            {
                                PosActual += pos;
                                return true;
                            }
                            else if (texto[pos] == ',') //Hay más keywords
                            {
                                return Validar_KeyWord(texto, pos + 1);
                            }
                            else
                            {
                                return Manejo_Error("Se esperaba una coma o un punto para generar una keyword valida.", PosActual + pos);
                            }
                        }
                        return Manejo_Error("La defnición de la keyword no es válida.", PosActual + temporal);
                    }
                    else
                    {
                        return Manejo_Error("Se esperaba comilla doble que cierre en la definición de la Keyword.", PosActual + temporal);
                    }
                }
                else if (texto[pos] == '\'')
                {
                    pos = OmitirEspacios(texto, pos + 1);
                    int temporal = pos;
                    string palabra = Obtener_Palabra(texto, pos);
                    pos = OmitirEspacios(texto, pos + palabra.Length);
                    if (texto[pos] == '\'')
                    {
                        if (EsPalabra(palabra))
                        {
                            int temporal_token = Lista_Tokens[Lista_Tokens.Count - 1].Numero_Token;
                            Token nuevoToken = new Token(temporal_token + 1, '\'' + palabra.ToLower() + '\'', 0, "");
                            Lista_Tokens.Add(nuevoToken);
                            Lista_KeyWords.Add(palabra.ToLower());
                            pos = OmitirEspacios(texto, pos + 1);
                            if (texto[pos] == '.') //Finalizaron las keywords
                            {
                                PosActual += pos;
                                return true;
                            }
                            else if (texto[pos] == ',') //Hay más keywords
                            {
                                return Validar_KeyWord(texto, pos + 1);
                            }
                            return Manejo_Error("Se esperaba una coma o un punto para generar una keyword valida.", PosActual + pos - palabra.Length);
                        }
                        return Manejo_Error("La defnición de la keyword no es válida.", PosActual + temporal);
                    }
                    return Manejo_Error("Se esperaba comilla doble que cierre en la definición de la Keyword.", PosActual + temporal);
                }

                return Manejo_Error("Se esperaba la definición de una keyword válida", PosActual);
            }
        }

        /// <summary>
        /// Función que valida que el comentario sea correcto
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Validar_Comentarios(string texto, int pos)
        {
            pos = OmitirEspacios(texto, pos);
            if (Char.IsLetter(texto[pos]))
            {
                string palabrita = Obtener_Palabra(texto, pos);
                if (palabrita.ToLower() == "comments")
                {
                    Vino_Comment = true;
                    return Validar_Comentarios(texto, palabrita.Length + pos);
                }
                else if (palabrita.ToLower() == "to") /// quiere decir que el comentario ya inicio
                {
                    if (YaInicio) //Parte que nos dice que caracter o caracteres forman el inicio del comentario
                    {
                        Vino_TO = true;
                        return Validar_Comentarios(texto, palabrita.Length + pos);
                    }
                    else
                    {
                        return Manejo_Error("No se ha definido el inicio del comentario.", PosActual + pos);
                    }
                }
                else if (palabrita.ToLower() == "comentario") //Esto quiere decir que es un ALias de comentario
                {
                    if (Vino_Comment == false)
                    {
                        return Manejo_Error("No viene la palabra reservada 'TO' por ende el comentario esta incorrectamente definido.", PosActual + palabrita.Length);
                    }
                    //Lista_Alias_Coments.Add(palabrita);
                    Vino_Comentario_ESP = true;
                    return Validar_Comentarios(texto, pos + palabrita.Length);
                }
                else
                {
                    return Manejo_Error("Error en el alias del comentario no puede repetirse, ser un id conjunto o pertenecer a las reservadas", PosActual + palabrita.Length);
                }
                if (Vino_Comment)
                {
                    return Manejo_Error("Se esperaba la palabra TO para definir correctamente el comentario.", PosActual + pos);
                }
            }
            else if (texto[pos] == '\'')
            {
                if (Vino_TO)
                {
                    if (!def_coment)
                    {
                        return Manejo_Error("No se pudo definiri correctamente el comentario", PosActual + pos);
                    }
                    else
                    {
                        LogroCerrar = true;
                    }
                }
                else
                {
                    YaInicio = true;
                    def_coment = true;
                }
                string palabrita = "";
                bool tempo_found = false;
                int i;
                for (i = pos + 1; i < texto.Length; i++)
                {
                    if (texto[i] != '\'')
                    {
                        palabrita += texto[i];
                    }
                    else
                    {
                        tempo_found = true;
                        break;
                    }
                }
                if (tempo_found)
                {
                    palabrita = palabrita.TrimEnd();
                    palabrita = palabrita.TrimStart();
                    if (palabrita.IndexOf(' ') == -1 && palabrita != "")
                    {
                        int temporal = Lista_Tokens[Lista_Tokens.Count - 1].Numero_Token;
                        Token nuevoToken = new Token(temporal + 1, palabrita, 0, "");
                        Lista_Tokens.Add(nuevoToken);
                        Cont_Token++;
                        return Validar_Comentarios(texto, i + 1);
                    }
                }
                return Manejo_Error("El comentario definido es incorrecto ya que no viene comilla simple que cierre", PosActual + pos);
            }
            else if (texto[pos] == '\"')
            {
                if (Vino_TO)
                {
                    if (!def_coment)
                    {
                        return Manejo_Error("No se pudo definiri correctamente el comentario", PosActual + pos);
                    }
                    else
                    {
                        LogroCerrar = true;
                    }
                }
                else
                {
                    YaInicio = true;
                    def_coment = true;
                }
                string palabrita = "";
                bool tempo_found = false;
                int i;
                for (i = pos + 1; i < texto.Length; i++)
                {
                    if (texto[pos] == '\"')
                    {
                        palabrita += texto[i];
                    }
                    else
                    {
                        tempo_found = true;
                        break;
                    }
                }
                if (tempo_found)
                {
                    palabrita = palabrita.TrimEnd();
                    palabrita = palabrita.TrimStart();
                    if (palabrita.IndexOf(' ') == -1 && palabrita != "")
                    {
                        Token nuevoToken = new Token(Cont_Token, palabrita, 0, "");
                        Lista_Tokens.Add(nuevoToken);
                        Cont_Token++;
                        return Validar_Comentarios(texto, i + 1);
                    }
                }
                return Manejo_Error("El comentario definido es incorrecto", PosActual + pos);
            }
            else if (texto[pos] == '.') //Termino el comentario
            {
                if (LogroCerrar || Vino_Comentario_ESP)
                {
                    PosActual += pos + 1;
                    return true;
                }
            }
            return Manejo_Error("Error al intentar definir el comentario.", PosActual);
        }

        /// <summary>
        /// Función que analiza las producciones del arhcivo
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private bool Analiza_Producctions(string texto)
        {
            if (texto.Trim() == "" && Existen_Producciones)
            {
                return true;
            }
            int nTempo = OmitirEspacios(texto, 0);
            if (nTempo != -1)
            {
                if (texto[nTempo] == '<')
                {
                    nTempo = OmitirEspacios(texto, nTempo + 1);
                    int tempo = nTempo;
                    string palabrita = Obtener_Palabra(texto, nTempo);
                    nTempo = OmitirEspacios(texto, nTempo + palabrita.Length);
                    if (texto[nTempo] == '>')
                    {
                        nTempo = OmitirEspacios(texto, nTempo + 1);
                        if (texto[nTempo] == '-' && texto[nTempo + 1] == '>')
                        {
                            //Debugear
                            #region Fase 3

                            if (Analiza_Existencia_NT("<" + palabrita.ToLower() + ">"))
                            {
                                if (!Esta_Definido_NT("<" + palabrita.ToLower() + ">"))
                                {
                                    Lista_NT[Pos_NT].Producion_NT = Cont_Produ;
                                }
                            }
                            else
                            {
                                if (palabrita.ToLower() != "start")
                                {
                                    No_Terminales nuevoNT = new No_Terminales(No_NT, "<" + palabrita.ToLower() + ">", Cont_Produ);
                                    Lista_NT.Add(nuevoNT);
                                    No_NT++;
                                }
                                else
                                {
                                    No_Terminales nuevoNT = new No_Terminales(1, "<" + palabrita.ToLower() + ">", Cont_Produ);
                                    Lista_NT.Add(nuevoNT);
                                    Vino_Produ_Start = true;
                                }
                            }
                            Elementos.Clear();

                            #endregion
                            //Lista_Producciones.Add(palabrita.ToLower()); //Ya no existirá esta lista
                            PosActual += nTempo + 2;
                            if (Validar_Produccion(texto.Substring(nTempo + 2), 0, "<" + palabrita.ToLower() + ">"))
                            {
                                Existen_Producciones = true;
                                return Analiza_Producctions(texto.Substring(OtraProduccion + nTempo + 2));
                            }
                            return false;
                        }
                        return Manejo_Error("Se esperaba el símbolo de la flecha '->' para generar una produccón válida.", PosActual + palabrita.Length);
                    }
                    else
                    {
                        return Manejo_Error("Se esperaba el símbolo '>'", PosActual + nTempo);
                    }
                }
                if (Char.IsLetter(texto[nTempo]))
                {
                    nTempo = OmitirEspacios(texto, nTempo);
                    string palabrita2 = Obtener_Palabra(texto, nTempo);
                    nTempo = OmitirEspacios(texto, nTempo + palabrita2.Length);
                    if (Vino_Produ_Start == true)
                    {
                        if (palabrita2.ToLower() == "end")
                        {
                            nTempo = OmitirEspacios(texto, nTempo);
                            if (texto[nTempo] == '.')
                            {
                                if (ES_Inalcanzable_Produ())
                                {

                                }
                                Vino_Produ_Start = false;
                                return true;
                            }
                            return Manejo_Error("Se esperaba la palabra 'End' y '.' para poder terminar el archivo", PosActual + nTempo + palabrita2.Length);
                        }
                        else
                        {
                            return Manejo_Error("Se esperaba la palabra 'End' para poder terminar el archivo", PosActual + nTempo + palabrita2.Length);
                        }
                    }
                    return Manejo_Error("La producción generada no es válida esto se puede deber a: \n 1. No viene el símbolo '<' que inicializa una producción " +
                        " \n 2. La producción 'START' no existe en las producciones.", PosActual + nTempo + 5);
                }
                return Manejo_Error("Se esperaba el símbolo '<' por lo que la producción es incorrecta.", PosActual + nTempo);
            }
            else
            {
                return Manejo_Error("La definción de la producción es incorrecta.", PosActual);
            }
        }

        /// <summary>
        /// Método que analiza si una producción es inalcanzable
        /// </summary>
        /// <returns></returns>
        private bool ES_Inalcanzable_Produ()
        {
            List<int> tempo = new List<int>();
            for (int i = 0; i < Lista_Producciones.Count; i++)
            {
                for (int j = 0; j < Lista_Producciones[i].Elementos.Count; j++)
                {
                    int aux = Lista_Producciones[i].Elementos[j];
                    tempo.Add(-aux);

                    if (Lista_Producciones[i].Produccion_PR.ToString().Contains((tempo).ToString()))
                    {
                        Lista_Inalcanzables.Add(Lista_Producciones[i].Elementos[j].ToString());
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Función que analiza la existencia de un no terminal
        /// </summary>
        /// <param name="nt"></param>
        /// <returns></returns>
        private bool Analiza_Existencia_NT(string nt)
        {
            for (int i = 0; i < Lista_NT.Count; i++)
            {
                if (Lista_NT[i].No_Terminal_NT.Equals(nt.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Analiza si la definición de la producción se encuentra
        /// </summary>
        /// <param name="nt"></param>
        /// <returns></returns>
        private bool Esta_Definido_NT(string nt)
        {
            for (int i = 0; i < Lista_NT.Count; i++)
            {
                if (Lista_NT[i].No_Terminal_NT == nt.ToLower())
                {
                    if (Lista_NT[i].Producion_NT == -1)
                    {
                        Pos_NT = i;
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        bool Vino_Signos_Mayor_Menor;

        /// <summary>
        /// Función que valida y analiza la producción o producciones entrantes
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool Validar_Produccion(string texto, int tem, string ntActual)
        {
            tem = OmitirEspacios(texto, tem);
            if (tem == -1)
            {
                return Manejo_Error("La definición de la producción es erronea.", PosActual);
            }
            if (Char.IsLetter(texto[tem]) && texto[tem] != 'ɛ' && texto[tem] != 'Ɛ' && texto[tem] != 'Ԑ')
            {
                //Posiblemente meta la validación con los tokens
                string palabrita = Obtener_Palabra(texto, tem);
                if (SeDefinio_Token(palabrita.ToLower()))
                {
                    Vino_Algo_Antes_Llave = true;
                    Elementos.Add(palabrita.ToLower());
                    return Validar_Produccion(texto, tem + palabrita.Length, ntActual);
                }
                return Manejo_Error("La palabra ingresada no es un token.", PosActual + tem + palabrita.Length);
            }
            else if (texto[tem] == '<') //Hay producción
            {
                tem = OmitirEspacios(texto, tem + 1);
                string palabrita = Obtener_Palabra(texto, tem);
                tem = OmitirEspacios(texto, tem + palabrita.Length);
                if (texto[tem] == '>') //Fin producción 
                {
                    #region Fase 3

                    if (!Analiza_Existencia_NT("<" + palabrita.ToLower() + ">"))
                    {
                        No_Terminales nuevoNT = new No_Terminales(No_NT, "<" + palabrita.ToLower() + ">", -1);
                        Lista_NT.Add(nuevoNT);
                        No_NT++;
                    }
                    Elementos.Add("<" + palabrita + ">");

                    #endregion
                    Vino_Algo_Antes_Llave = true;
                    Vino_Signos_Mayor_Menor = true;
                    return Validar_Produccion(texto, tem + 1, ntActual);
                }
                return Manejo_Error("Se esperaba el símbolo '>'", PosActual + tem);
            }
            else if (texto[tem] == '\'') //Esto quiere decir que es un token
            {
                if ((tem + 2) < texto.Length)
                {
                    if (texto[tem + 2] == '\'')
                    {
                        if (Lista_Tokens.Count == 0)
                        {
                            Token nuevoToken = new Token(1, '\'' + texto[tem + 1].ToString().ToLower() + '\'', 0, "");
                            Lista_Tokens.Add(nuevoToken);
                        }
                        else
                        {
                            if (Numero_Token('\'' + texto[tem + 1].ToString() + '\'') == -1)
                            {
                                if (Revisar_Existencia_Tokens(Lista_Tokens, '\'' + texto[tem + 1].ToString() + '\'') == false)
                                {
                                    int temporal = Lista_Tokens[Lista_Tokens.Count - 1].Numero_Token;
                                    Token nuevoToken = new Token(temporal + 1, '\'' + texto[tem + 1].ToString().ToLower() + '\'', 0, "");
                                    Lista_Tokens.Add(nuevoToken);
                                }
                            }
                        }
                        Elementos.Add('\'' + texto[tem + 1].ToString() + '\'');
                        Vino_Algo_Antes_Llave = true;
                        return Validar_Produccion(texto, tem + 3, ntActual);
                    }
                    tem = OmitirEspacios(texto, tem + 1);
                    string palabrita = "";

                    for (int i = tem; i < texto.Length; i++)
                    {
                        if (texto[i] == '\'')
                        {
                            break;
                        }
                        else if (char.IsWhiteSpace(texto[i]))
                        {
                            break;
                        }
                        palabrita += texto[i];
                    }
                    tem = OmitirEspacios(texto, tem + palabrita.Length);
                    if (texto[tem] == '\'') //Meter posible validación con los keywords
                    {
                        if (Lista_Tokens.Count == 0)
                        {
                            Token nuevoToken = new Token(1, '\'' + texto[tem + 1].ToString().ToLower() + '\'', 0, "");
                            Lista_Tokens.Add(nuevoToken);
                        }
                        else
                        {
                            if (Numero_Token('\'' + palabrita.ToLower() + '\'') == -1)
                            {
                                if (Revisar_Existencia_Tokens(Lista_Tokens, '\'' + palabrita.ToLower() + '\'') == false)
                                {
                                    int temporal = Lista_Tokens[Lista_Tokens.Count - 1].Numero_Token;
                                    Token nuevoToken = new Token(temporal + 1, '\'' + palabrita.ToLower() + '\'', 0, "");
                                    Lista_Tokens.Add(nuevoToken);
                                }
                            }
                        }
                        Elementos.Add('\'' + palabrita + '\'');
                        Vino_Algo_Antes_Llave = true;
                        return Validar_Produccion(texto, tem + 1, ntActual);
                    }
                    return Manejo_Error("Se esperaba el signo de punto: \'.", PosActual + tem);
                }
                return Manejo_Error("La producción definida es incorrecta.", PosActual + tem);
            }
            else if (texto[tem] == '\"') //Esto quiere decir que es un token
            {
                if ((tem + 2) < texto.Length)
                {
                    if (texto[tem + 2] == '\"')
                    {
                        if (Lista_Tokens.Count == 0)
                        {
                            Token nuevoToken = new Token(1, '\'' + texto[tem + 1].ToString().ToLower() + '\'', 0, "");
                            Lista_Tokens.Add(nuevoToken);
                        }
                        else
                        {
                            if (Numero_Token('\'' + texto[tem + 1].ToString() + '\'') == -1)
                            {
                                if (Revisar_Existencia_Tokens(Lista_Tokens, '\'' + texto[tem + 1].ToString() + '\'') == false)
                                {
                                    int temporal = Lista_Tokens[Lista_Tokens.Count - 1].Numero_Token;
                                    Token nuevoToken = new Token(temporal + 1, '\'' + texto[tem + 1].ToString() + '\'', 0, "");
                                    Lista_Tokens.Add(nuevoToken);
                                }
                            }
                        }
                        Elementos.Add('\'' + texto[tem + 1].ToString() + '\'');
                        Vino_Algo_Antes_Llave = true;
                        return Validar_Produccion(texto, tem + 3, ntActual);
                    }
                    tem = OmitirEspacios(texto, tem + 1);
                    string palabrita = "";
                    for (int i = tem; i < texto.Length; i++)
                    {
                        if (texto[i] == '\"')
                        {
                            break;
                        }
                        else if (char.IsWhiteSpace(texto[i]))
                        {
                            break;
                        }
                        palabrita += texto[i];
                    }
                    tem = OmitirEspacios(texto, tem + palabrita.Length);
                    if (texto[tem] == '\"')
                    {
                        if (Lista_Tokens.Count == 0)
                        {
                            Token nuevoToken = new Token(1, '\'' + texto[tem + 1].ToString().ToLower() + '\'', 0, "");
                            Lista_Tokens.Add(nuevoToken);
                        }
                        else
                        {
                            if (Numero_Token('\'' + palabrita + '\'') == -1)
                            {
                                if (Revisar_Existencia_Tokens(Lista_Tokens, '\'' + palabrita + '\'') == false)
                                {
                                    int temporal = Lista_Tokens[Lista_Tokens.Count - 1].Numero_Token;
                                    Token nuevoToken = new Token(temporal + 1, '\'' + palabrita.ToLower() + '\'', 0, "");
                                    Lista_Tokens.Add(nuevoToken);
                                }
                            }
                        }
                        Elementos.Add('\'' + palabrita + '\'');
                        Vino_Algo_Antes_Llave = true;
                        return Validar_Produccion(texto, tem + 1, ntActual);
                    }
                    return Manejo_Error("Se esperaba el signo de punto: \'.", PosActual + tem);
                }
                return Manejo_Error("La producción definida es incorrecta.", PosActual + tem);
            }
            else if (texto[tem] == '{')
            {
                if (Vino_Algo_Antes_Llave == true)
                {
                    tem = OmitirEspacios(texto, tem + 1);
                    string palabritra = Obtener_Palabra(texto, tem);
                    tem = OmitirEspacios(texto, tem + palabritra.Length);
                    if (texto[tem] == '}')
                    {
                        int posTempo = OmitirEspacios(texto, tem + 1);
                        if (texto[posTempo] == '.')
                        {
                            if (Elementos.Count == 0)
                            {
                                Producciones nuevaProduccion = new Producciones(Lista_Producciones.Count + 1, 0, 0, ntActual);
                                Lista_Producciones.Add(nuevaProduccion);
                                Lista_Producciones[Lista_Producciones.Count - 1].Elementos.Add(0);
                                Cont_Produ++;
                                //if (Lista_Producciones[Lista_Producciones.Count - 2].Siguiente_PR == Lista_Producciones[Lista_Producciones.Count - 1].Produccion_PR)
                                //{
                                //    Lista_Producciones[Lista_Producciones.Count - 2].Siguiente_PR = 0;
                                //}
                                return Validar_Produccion(texto, tem + 1, ntActual);
                            }
                            else
                            {
                                Producciones nuevaProduccion = new Producciones(Lista_Producciones.Count + 1, Elementos.Count, 0, ntActual);
                                Lista_Producciones.Add(nuevaProduccion);
                                Agregar_Elemntos_Produciones(Lista_Producciones.Count - 1);
                                Elementos.Clear(); //Ya se agregaron los elementos necesarios
                                Cont_Produ++;
                            }
                            Ver_Produccion(ntActual);
                            Vino_Algo_Antes_Llave = false;
                            return Validar_Produccion(texto, tem + 1, ntActual);
                        }
                        else if (texto[posTempo] == '|')
                        {
                            if (Elementos.Count == 0)
                            {
                                Producciones nuevaProdu = new Producciones(Lista_Producciones.Count + 1, 0, Lista_Producciones.Count + 2, ntActual);
                                Lista_Producciones.Add(nuevaProdu);
                                Lista_Producciones[Lista_Producciones.Count - 1].Elementos.Add(0);
                                Arreglar_Los_Que_Vienen(); //Se modifica el siguente
                                Cont_Produ++;
                                return Validar_Produccion(texto, tem + 1, ntActual);
                            }
                            else
                            {
                                Producciones nuevaProdu = new Producciones(Lista_Producciones.Count + 1, Elementos.Count, Lista_Producciones.Count + 2, ntActual);
                                Lista_Producciones.Add(nuevaProdu);
                                Agregar_Elemntos_Produciones(Lista_Producciones.Count - 1); //Se agrega el elemento anterior ingresado
                                Elementos.Clear();
                                Cont_Produ++;
                            }
                            Ver_Produccion(ntActual);
                            return Validar_Produccion(texto, tem + 1, ntActual);
                        }
                        return Manejo_Error("Se esperaba un '.' o '|' después de la llave que cierra.", PosActual + posTempo - palabritra.Length);
                    }
                    return Manejo_Error("Se esperaba una llave que cierra '}'", PosActual + tem);
                }
                return Manejo_Error("No se puede definir una llave que abre '{' sin que antes venga por lo menos un epsilon", PosActual + tem);
            }
            else if (texto[tem] == '|')
            {
                if (Otro_Analisis_Produccion(texto, tem))
                {
                    if (Elementos.Count == 0)
                    {
                        Producciones nuevaProdu = new Producciones(Lista_Producciones.Count + 1, 0, Lista_Producciones.Count + 2, ntActual);
                        Lista_Producciones.Add(nuevaProdu);
                        Lista_Producciones[Lista_Producciones.Count - 1].Elementos.Add(0);
                        Arreglar_Los_Que_Vienen();
                        Cont_Produ++;
                        return Validar_Produccion(texto, tem + 1, ntActual);
                    }
                    else
                    {
                        Producciones nuevaProdu = new Producciones(Lista_Producciones.Count + 1, Elementos.Count, Lista_Producciones.Count + 2, ntActual);
                        Lista_Producciones.Add(nuevaProdu);
                        Agregar_Elemntos_Produciones(Lista_Producciones.Count - 1);
                        Elementos.Clear();
                        Cont_Produ++;
                    }
                    Ver_Produccion(ntActual);
                    Vino_Algo_Antes_Llave = false;
                    return Validar_Produccion(texto, tem + 1, ntActual);
                }
                return Manejo_Error("La producción definida es incorrecta.", PosActual + tem);
            }
            else if (texto[tem] == '.') //Fin produccón
            {
                if (Otro_Analisis_Produccion(texto, tem))
                {
                    if (Elementos.Count == 0)
                    {
                        Producciones nuevaProdu = new Producciones(Lista_Producciones.Count + 1, 0, 0, ntActual);
                        Lista_Producciones.Add(nuevaProdu);
                        Lista_Producciones[Lista_Producciones.Count - 1].Elementos.Add(0);
                        Cont_Produ++;
                        //if (Lista_Producciones[Lista_Producciones.Count - 2].Siguiente_PR == Lista_Producciones[Lista_Producciones.Count - 1].Produccion_PR)
                        //{
                        //    Lista_Producciones[Lista_Producciones.Count - 2].Siguiente_PR = 0;
                        //}
                        PosActual += tem + 1;
                        OtraProduccion = tem + 1;
                        return true;
                    }
                    else
                    {
                        Producciones nuevaProdu = new Producciones(Lista_Producciones.Count + 1, Elementos.Count, 0, ntActual);
                        Lista_Producciones.Add(nuevaProdu);
                        Agregar_Elemntos_Produciones(Lista_Producciones.Count - 1); //Se agrega el elemento anterior ingresado
                        Elementos.Clear();
                        Cont_Produ++;
                    }
                    Ver_Produccion(ntActual);
                    PosActual += tem + 1;
                    OtraProduccion = tem + 1;
                    Vino_Algo_Antes_Llave = false;
                    return true;
                }
                return Manejo_Error("La producción definida es incorrecta", PosActual + tem);
            }
            else if (texto[tem] == 'ɛ' || texto[tem] == 'Ɛ' || texto[tem] == 'Ԑ' || texto[tem] == '~')
            {
                Vino_Algo_Antes_Llave = true;
                return Validar_Produccion(texto, tem + 1, ntActual);
            }
            return Manejo_Error("La producción definida es incorrecta pues falta un punto para poder finalizarla", PosActual);
        }

        /// <summary>
        /// Función que Valida si los tokens de las producciones ya existen
        /// </summary>
        /// <param name="tempo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool Revisar_Existencia_Tokens(List<Token> tempo, string token)
        {
            for (int i = 0; i < tempo.Count; i++)
            {
                if (tempo[i].Simbolo_Token.Equals(token))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Función que valida que la producción se encuentre correctamente escrita
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        private bool Otro_Analisis_Produccion(string texto, int p)
        {
            for(int i = p -1; i > 0; i--)
            {
                if(!Char.IsWhiteSpace(texto[i]))
                {
                    switch(texto[i])
                    {
                        case 'ɛ':
                            return true;
                        case '_':
                            return true;
                        case '>':
                            if (texto[i - 1] == '-')
                            {
                                return false;
                            }
                            return true;
                        case '\'':
                            return true;
                        case '\"':
                            return true;
                        case '}':
                            return true;
                        default:
                            {
                                if(Char.IsLetterOrDigit(texto[i]))
                                {
                                    return true;
                                }
                                return false;
                            }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Función que obtiene el número del token, es como un contador
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private int Numero_Token(string token)
        {
            for(int i = 0; i < Lista_Tokens.Count; i++)
            {
                if(Char.IsLetter(Lista_Tokens[i].Simbolo_Token[0])) //Si no es letra entonces es un símbolo como + 
                {
                    if(Lista_Tokens[i].Simbolo_Token.ToLower() == token.ToLower())
                    {
                        return i;
                    }
                }
                else
                {
                    if(Lista_Tokens[i].Simbolo_Token.ToLower() == token.ToLower())
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Método que recorre los elementos y los agrega a la lista con su respectiva posicion
        /// </summary>
        /// <param name="nElemento"></param>
        private void Agregar_Elemntos_Produciones(int nElemento)
        {
            for(int i= 0; i < Elementos.Count; i++)
            {
                if(Elementos[i][0] == '\"' || Elementos[i][0] == '\'' || Char.IsLetter(Elementos[i][0]))
                {
                    int postempo = Numero_Token(Elementos[i].ToLower());
                    Lista_Producciones[nElemento].Elementos.Add(Lista_Tokens[postempo].Numero_Token);
                }
                else
                {
                    int postempo = Numero_NT(Elementos[i]);
                    Lista_Producciones[nElemento].Elementos.Add(-postempo);
                }
            }
        }

        /// <summary>
        /// Función auxiliar que obtiene el número del no terminal analizado
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        private int Numero_NT(string elemento)
        {
            for(int i = 0; i < Lista_NT.Count; i++)
            {
                if(Lista_NT[i].No_Terminal_NT == elemento.ToLower())
                {
                    return Lista_NT[i].Numero_NT;
                }
            }
            return -1;
        }

        /// <summary>
        /// Método que realiza la modificación del siguiente elementosi fuese necesario 
        /// </summary>
        private void Arreglar_Los_Que_Vienen()
        {
            if(Lista_Producciones[Lista_Producciones.Count - 2].Siguiente_PR == Lista_Producciones[Lista_Producciones.Count -1].Produccion_PR)
            {
                Lista_Producciones[Lista_Producciones.Count - 2].Siguiente_PR = Lista_Producciones[Lista_Producciones.Count - 1].Siguiente_PR;
            }
        }

        /// <summary>
        /// Método que verifica si la producción con lo cual se le asignara un nuevo valor a los siguientes
        /// </summary>
        /// <param name="prod"></param>
        private void Ver_Produccion(string prod)
        {
            for(int i = Lista_Producciones.Count-2; i >= 0; i--)
            {
                //Verifica una producción con no terminal válida
                if(Lista_Producciones[Lista_Producciones.Count - 1].NoTerminal_PR ==  Lista_Producciones[i].NoTerminal_PR)
                {
                    Lista_Producciones[i].Siguiente_PR = Lista_Producciones[Lista_Producciones.Count - 1].Produccion_PR;
                    break;
                }
            }
        }

        /// <summary>
        /// Método que arregla el start si este ya esta definido
        /// </summary>
        private void Se_Definio_Start()
        {
            bool tempo = false;
            for(int i=0; i < Lista_NT.Count; i++)
            {
                if(Lista_NT[i].No_Terminal_NT == "<start>")
                {
                    tempo = true;
                    Lista_NT[i].Numero_NT = 1;
                    No_Terminales tempo_aux = Lista_NT[i];
                    Lista_NT.RemoveAt(i);
                    Lista_NT.Insert(0, tempo_aux);
                }
            }
            if(!tempo)
            {
                No_Terminales tempo_aux = new No_Terminales(1, "<start>", -1);
                Lista_NT.Insert(0, tempo_aux);
            }
        }

        /// <summary>
        /// Método que se utiliza para determinar el first generado por las producciones
        /// </summary>
        public void Determinar_First()
        {
            int temporal = 1;
            while(temporal > 0)
            {
                /*Se deben de recorrer las listas de no terminales y producciones ya que van de la mano
                 Primero se debe colocar la de los no terminales por que es la lista que posiblemente sea la más pequeña 
                 Luego debo de recorrer la de producciones*/
                temporal = 0;
                for (int i = 0; i < Lista_NT.Count; i++)
                {
                    for(int j = 0; j < Lista_Producciones.Count; j++)
                    {
                        /*Debo de saber que producciones producen un no terminal*/
                        if(Lista_Producciones[j].NoTerminal_PR == Lista_NT[i].No_Terminal_NT)
                        {
                            /*Si la llegase a producir entonces debo de saber que elemento es el que produce y si este posee un First con anterioridad*/
                            int tempo_num_ele = Lista_Producciones[j].Elementos[0];

                            /*Si esto llegase a pasar es un no terminal*/
                            if(tempo_num_ele < 0)
                            {
                                /*debo de revisar si el nt analizad ya posee first*/
                                if(Posee_First_NT(-tempo_num_ele))
                                {
                                    /*Buscara agregar el first si el elemento analizado posee*/
                                    if(ADD_First(-tempo_num_ele, i))
                                    {
                                        temporal++;
                                    }
                                    /*Ya sea que logro agregar se va a verificar si el no terminal sigue siendo nulo, con respecto a los first*/
                                    if(Es_NT_Null(-tempo_num_ele))
                                    {
                                        bool produ_Null = true;
                                        /*Este recorreo todos los elementos para agregarlos a la lista de first correspondientes*/
                                        for(int z = 1; z < Lista_Producciones[j].Elementos.Count; z++)
                                        {
                                            int aux = Lista_Producciones[j].Elementos[z];
                                            //Es terminal si 
                                            if(aux > 0)
                                            {
                                                //Si no la contiene entonces agrega el terminal ya que es un elemento valido para un firts
                                                if(!Lista_NT[i].First_NT.Contains(aux))
                                                {
                                                    Lista_NT[i].First_NT.Add(aux);
                                                    temporal++;
                                                }
                                                produ_Null = false; 
                                                break;
                                            }
                                            //Es NT si 
                                            else if(aux < 0)
                                            {
                                                /*Se vuelve analizas si posee o no firts*/
                                                if(Posee_First_NT(-aux))
                                                {
                                                    if(ADD_First(-aux, i))
                                                    {
                                                        temporal++;
                                                    }
                                                    if(!Es_NT_Null(-aux))
                                                    {
                                                        produ_Null = false;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        //Analiza si la producción se encuentra vacía
                                        if(produ_Null)
                                        {
                                            if (!Lista_NT[i].First_NT.Contains(0)) //Verifica si la producción y el first de la misma tienen algo
                                            {
                                                Lista_NT[i].First_NT.Add(0);
                                                temporal++;
                                            }
                                        }
                                    }
                                }
                            }
                            /*Si esto llegase a pasar quiere decir que es un terminal*/
                            else if(tempo_num_ele > 0)
                            {
                                /*Ahora debo de revisar si el elemento ya es un first para el no terminal en la posición i*/
                                if(!Lista_NT[i].First_NT.Contains(tempo_num_ele))
                                {
                                    //Se agrega el first 
                                    Lista_NT[i].First_NT.Add(tempo_num_ele);
                                    temporal++;
                                }
                            }
                            //Epsilon ---> no estoy seguro si se agrega
                            else if (tempo_num_ele == 0)
                            {
                                /*Ahora debo de revisar si el elemento ya es un first para el no terminal en la posición i*/
                                if (!Lista_NT[i].First_NT.Contains(tempo_num_ele))
                                {
                                    //Se agrega el first 
                                    Lista_NT[i].First_NT.Add(tempo_num_ele);
                                    temporal++;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método que verifica si el terminal ya tiene first
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Posee_First_NT(int pos)
        {
            if(Lista_NT[pos-1].First_NT.Count  == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Función que valida si agrega o no el elemento a su respectiva posición
        /// </summary>
        /// <param name="elemento"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool ADD_First(int orgien, int destino)
        {
            bool logro_agregar = false;
            for(int i = 0; i < Lista_NT[orgien - 1].First_NT.Count; i++)
            {
                if(!Lista_NT[destino].First_NT.Contains(Lista_NT[orgien-1].First_NT[i])  && Lista_NT[orgien - 1].First_NT[i] != 0)
                {
                    //Con esto se agrega a la lista de first
                    Lista_NT[destino].First_NT.Add(Lista_NT[orgien - 1].First_NT[i]);
                    logro_agregar = true;
                }
            }
            return logro_agregar;
        }

        /// <summary>
        /// Determina si el no terminal es nulo, es decir, no tiene firts
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Es_NT_Null(int pos)
        {
            if(Lista_NT[pos-1].First_NT.Contains(0))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// función que devuelve el valor del no terminal almacenado
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public string  Imprimir_Valor_NT(int pos)
        {
            return Lista_NT[(-pos) - 1].No_Terminal_NT;
        }

        /// <summary>
        /// Función que devuelve el valor del símbolo del token almacenado
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public string Imprimr_Valor_Terminal(int pos)
        {
            return Lista_Tokens[pos - 1].Simbolo_Token;
        }

        #endregion

        /// <summary>
        /// Funnción que devuelve un error si lo encuentra
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Manejo_Error(String error, int pos)
        {
            PosError = pos;
            MsjoError = error;
           return false;
        }

        /// <summary>
        /// Función que devuelve un error para mostrar en pantalla
        /// </summary>
        /// <returns></returns>
        public string MostrarError()
        {
            return MsjoError;
        }

        /// <summary>
        /// Función que devuelve la posición del error para mostrar en pantalla
        /// </summary>
        /// <returns></returns>
        public int Ver_Pos_Erro()
        {
            return PosError;
        }

        /// <summary>
        /// Función que obtiene la palabra procesada
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="numero"></param>
        /// <returns></returns>
        private String Obtener_Palabra(string texto, int pos)
        {
            String palabrita = "";
            int espacio;

            for(int i = pos; i < texto.Length; i++)
            {
                if(Char.IsLetterOrDigit(texto[i]) || texto[i] == '_')
                {
                    palabrita += texto[i];
                }
                else
                {
                    break;
                }
            }
            return palabrita;
        }

        /// <summary>
        /// Función que valida si es una palabra reservada del archivo
        /// </summary>
        /// <param name="palabra"></param>
        /// <returns></returns>
        private bool Es_Reservada(String palabra)
        {
            switch(palabra.ToLower())
            {
                case "compiler":
                    {
                        return true;
                    }
                case "units":
                    {
                        return true;
                    }
                case "sets":
                    {
                        return true;
                    }
                case "tokens":
                    {
                        return true;
                    }
                case "right":
                    {
                        return true;
                    }
                case "left":
                    {
                        return true;
                    }
                case "keywords":
                    {
                        return true;
                    }
                case "comments":
                    {
                        return true;
                    }
                case "to":
                    {
                        return true;
                    }
                case "productions":
                    {
                        return true;
                    }
                case "end":
                    {
                        return true;
                    }
                case "chr":
                    {
                        return true;
                    }
                case "comentario":
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        /// <summary>
        /// Función que omitie lo espacios que encuentre
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="numero"></param>
        /// <returns></returns>
        private int OmitirEspacios(String texto, int pos)
        {
            if(pos == -1)
            {
                pos = 0;
            }
            for(int i = pos; i < texto.Length; i++)
            {
                if(!Char.IsWhiteSpace(texto[i]))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
