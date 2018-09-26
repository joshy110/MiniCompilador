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
        String MsjoError, Nombre_Compi;

        bool Caracter, Comillas, Apostrofe, Conjunto;
        bool Viene_Check, Vino_Token, Aceptar_Token, Vino_PIPE, HayValor, Acc_Token, Vino_L, Vino_R, Vino_Algo_Antes_Llave, Vino_Producciones_Titulo;
        bool Existen_Producciones, Vino_TO, Vino_Comment, YaInicio, LogroCerrar, def_coment, Vino_Comentario_ESP;

        char[] ArregloCompleto_Chars;
        int x, y; //Pos_Token;
        int PosError, PosActual, Defi_Nueva, Fin_CHR, HAY_Parentesis, OtraProduccion;
        public int Vino_Start;
        int[] rango;

        List<string> Lista_Units;
        List<string> Lista_Conjuntos;
        List<string> Lista_Tokens;
        List<string> Lista_Alias_Coments;
        List<string> Lista_KeyWords;
        List<string> Lista_Producciones;

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
            Lista_Tokens = new List<string>();
            Lista_KeyWords = new List<string>();
            Lista_Producciones = new List<string>();
            Lista_Alias_Coments = new List<string>();
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
            if(Analiza_Titulo(texto)) //Compiler Joshy
            {
                if(PosActual < texto.Length)
                {
                    if(Existe_Unidades(texto.Substring(PosActual)))  //Son las UNITS
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
                    if(PosActual < texto.Length)
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

            if(Char.IsLetter(texto[nTempo]))
            {
                String tempoPalabra = Obtener_Palabra(texto, nTempo);
                if(tempoPalabra.ToLower() == "compiler")
                {
                    nTempo = OmitirEspacios(texto, nTempo + 8);

                    if (Char.IsLetter(texto[nTempo])) //Se obtiene el nombre que se le dio al compilador
                    {
                        Nombre_Compi = Obtener_Palabra(texto, nTempo);
                        int punto = Encontro_Punto_Final(texto, nTempo + Nombre_Compi.Length);

                        if(punto == -1)
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
            if(Char.IsLetter(texto[nTempo]))
            {
                string palabra = Obtener_Palabra(texto, nTempo);
                if(Es_Reservada(palabra)) //Si se encuentra en las reservadas encontrara error ya que una unidad no puede ser una palabra reservada
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
                if(texto[nTempo] == '.')
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
            if(terminoUnidades == -1)
            {
                return Manejo_Error("Se esperaba punto al final de las unidades", PosActual + nTempo);
            }
            string[] unidadesTempo = texto.Substring(nTempo, terminoUnidades - nTempo).Split(',');
            int posunidad = 0; 
            for(int i = 0; i< unidadesTempo.Length; i++)
            {
                string nombreU = unidadesTempo[i].TrimStart(); //Me tira el string actual 
                nombreU = nombreU.TrimEnd(); 
                if(!EsPalabra(nombreU) || Es_Reservada(nombreU) || nombreU.IndexOf(' ') != -1)
                {
                    return Manejo_Error("La unidad no esta definida correctamente. Esto se puede deber a: \n 1. " 
                        + nombreU +" pertenece a las palabras reservada. \n 2. No viene punto final", PosActual  + posunidad + nTempo + nombreU.Length);
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
            if(palabra == "")
            {
                return false;
            }
            else
            {
                if(Char.IsLetter(palabra[0]))
                {
                    for(int i = 0; i < palabra.Length; i++)
                    {
                        if(!Char.IsLetterOrDigit(palabra[i]) || palabra[i] == '_')
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
            if(nTempo == -1)
            {
                return Manejo_Error("La definición del compilador esta incompleta", PosActual);
            }
            else
            {
                PosActual += nTempo;
                if(Char.IsLetter(texto[nTempo]))
                {
                    string palabra = Obtener_Palabra(texto, nTempo);
                    if (Es_Reservada(palabra))
                    {
                        if(palabra.ToLower() == "tokens")
                        {
                            PosActual += nTempo + palabra.Length;
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
                        if(Lista_Conjuntos.Contains(palabra))
                        {
                            return Manejo_Error("El conjunto ya se definio previamente", PosActual);
                        }
                        PosActual += palabra.Length;
                        nTempo = OmitirEspacios(texto, 0);
                        PosActual += nTempo;
                        if(texto[nTempo] == '=')
                        {
                            nTempo++;
                            nTempo = OmitirEspacios(texto, nTempo);
                            switch(texto[nTempo])
                            {
                                case '\'':
                                    {
                                        Caracter = Comillas = Apostrofe = false;
                                        rango[0] = rango[1] = -1;
                                        int final = Obtener_Final_Conjunto(texto);
                                        Lista_Conjuntos.Add(palabra);
                                        if (Valida_Conjunto(texto, nTempo, final))
                                        {
                                            PosActual += Defi_Nueva + 1;
                                            return Analiza_Conjuntos(texto.Substring(final + 1));
                                        }
                                        return false;
                                    }
                                case '\"':
                                    {
                                        Caracter = Comillas = Apostrofe = false;
                                        rango[0] = rango[1] = -1;
                                        int final = Obtener_Final_Conjunto(texto);
                                        Lista_Conjuntos.Add(palabra);
                                        if (Valida_Conjunto(texto, nTempo, final))
                                        {
                                            PosActual += Defi_Nueva + 1;
                                            return Analiza_Conjuntos(texto.Substring(final + 1));
                                        }
                                        return false;
                                    }
                                case 'c':
                                    {
                                        Caracter = Comillas = Apostrofe = false;
                                        rango[0] = rango[1] = -1;
                                        int final = Obtener_Final_Conjunto(texto);
                                        Lista_Conjuntos.Add(palabra);
                                        if (Valida_Conjunto(texto, nTempo, final))
                                        {
                                            PosActual += Defi_Nueva + 1;
                                            return Analiza_Conjuntos(texto.Substring(final + 1));
                                        }
                                        return false;
                                    }
                                case 'C':
                                    {
                                        Caracter = Comillas = Apostrofe = false;
                                        rango[0] = rango[1] = -1;
                                        int final = Obtener_Final_Conjunto(texto);
                                        Lista_Conjuntos.Add(palabra);
                                        if (Valida_Conjunto(texto, nTempo, final))
                                        {
                                            PosActual += Defi_Nueva + 1;
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
            for(int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == '.')
                {
                    cont_Puntos++;
                    string y = texto[i+1].ToString();
                    if(texto[i+1] == '.' || texto[i + 1] == '\'' || texto[i +1] == '\"' ||texto[i + 1] == '+' || texto[i+1] == 'c' || texto[i+1] == 'C')
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
                if(pos_chr_actual <= pos_chr_final)
                {
                    pos_chr_actual = OmitirEspacios(texto, pos_chr_actual);
                    switch(texto[pos_chr_actual])
                    {
                        case '\'':
                            {
                                if((pos_chr_actual + 1) < texto.Length)
                                {
                                    if(texto[pos_chr_actual + 2] == '\'')
                                    {
                                        if(rango[0] == -1 && rango[1] == -1)
                                        {
                                            rango[0] = texto[pos_chr_actual + 1];
                                            Apostrofe = Conjunto = Caracter = true; //Hay conjunto válido.
                                            return Valida_Conjunto(texto, pos_chr_actual + 3, pos_chr_final);
                                        }
                                        else
                                        {
                                            if(Apostrofe)
                                            {
                                                if(rango[1] == -1)
                                                {
                                                    rango[1] = texto[pos_chr_actual + 1];
                                                }
                                                if(rango[1] >= rango[0])
                                                {
                                                    Caracter = Comillas = Apostrofe = Conjunto = false;
                                                    rango[0] = rango[1] = - 1;
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
                                if((pos_chr_actual + 1) < texto.Length)
                                {
                                    if(texto[pos_chr_actual + 1] == '.')
                                    {
                                        if(rango[0] != -1)
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
                                if(rango[1] == -1)
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
                                if(chr_Num != -1)
                                {
                                   if(rango[0] == -1 && rango[1] == -1)
                                    {
                                        Caracter = Conjunto = true;
                                        rango[0] = chr_Num;
                                        char tempo = (char)chr_Num;
                                        return Valida_Conjunto(texto, Fin_CHR + 1, pos_chr_final);
                                    }
                                    else
                                    {
                                        if(Caracter)
                                        {
                                            rango[1] = chr_Num;
                                            if (rango[1] >= rango[0])
                                            {
                                                if(rango[1] > 255)
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
                    if(!Conjunto && rango[1] == -1)
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
            if((pos_actual +2) < texto.Length)
            {
                string x = texto[pos_actual].ToString();
                string chr = texto[pos_actual].ToString() + texto[pos_actual + 1].ToString() + texto[pos_actual + 2].ToString();
                if(chr.ToLower() == "chr")
                {
                    for(int i = pos_actual + 3; i < texto.Length; i++)
                    {
                        if(texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                        {
                            if(texto[i] == '(')
                            {
                                int patito = Fin_Parentesis_CHR(texto, i + 1);
                                if (patito != -1)
                                {
                                    string numeroCHR = texto.Substring(i + 1, patito - i - 1);
                                    if(Es_Numero(numeroCHR))
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
            for(int i = pos; i < texto.Length; i++)
            {
                if(texto[i].Equals(')') && Char.IsNumber(texto[i-1]))
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
            for(int i=0; i < texto.Length; i++)
            {
                if(texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                {
                    pos = i; //Encontro la pos
                    break;
                }
            }
            string patito = "";
            int n_temp = -1;
            if(pos != -1)
            {
                for(int i = pos; i < texto.Length; i++)
                {
                    if(Char.IsDigit(texto[i]))
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

            if(n_temp != -1)
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
            while(i < texto.Length)
            {
                if(texto[i] != ' ' && texto[i] != '\t' && texto[i] != '\n')
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
            for(int i = pos; i < texto.Length; i++)
            {
                if(texto[i] != ' '&& texto[i] !=  '\t' && texto[i] != '\n')
                {
                    if(texto[i] == '\'' || texto[i] == '\"' || texto[i] == '.')
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
            if(Char.IsLetter(texto[nTempo]))
            {
                string palabra = Obtener_Palabra(texto, nTempo);
                if(palabra.ToLower() == "tokens")
                {
                    PosActual += nTempo + palabra.Length;
                    return Validar_Tokens(texto.Substring(nTempo+6));
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
                    if(Validar_Operador(texto.Substring(nTempo), 0))
                    {
                        Aceptar_Token = true;
                        Vino_R = false;
                        Vino_L = false;
                        return Validar_Tokens(texto.Substring(Defi_Nueva + 1 + nTempo));
                    }
                    return false;
                }
                else if(Char.IsLetter(texto[nTempo]))
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
                    else if(palabra.ToLower() == "comments")
                    {
                        Vino_TO = Vino_Comment = YaInicio = Vino_Comentario_ESP = false;
                        PosActual += nTempo;
                        if(Validar_Comentarios(texto.Substring(nTempo),0))
                        {
                            return Validar_Tokens(texto.Substring(texto.IndexOf('.') + 1));
                        }

                    }
                    else if(palabra.ToLower() == "productions")
                    {
                        PosActual += palabra.Length;
                        Existen_Producciones = false;
                        Vino_Producciones_Titulo = true;
                        //PosActual = texto.IndexOf(palabra) + 11;
                        if (Analiza_Producctions(texto.Substring(nTempo + palabra.Length)))
                        {
                            return true; //Aca iria el Fin archivo
                        }
                        return false;
                    }
                    else if(Es_Reservada(palabra))
                    {
                        return Manejo_Error("La palabra: " + palabra + " pertenece a las reservadas.", nTempo);
                    }
                    else
                    {
                        texto = texto.Substring(nTempo + palabra.Length);
                        if(Lista_Tokens.Contains(palabra))
                        {
                            return Manejo_Error("El token ya se definio previamente", PosActual);
                        }
                        PosActual += palabra.Length;
                        nTempo = OmitirEspacios(texto, 0);
                        PosActual += nTempo;
                        switch(texto[nTempo])
                        {
                            case '=':
                                {
                                    Vino_PIPE = HayValor = false;
                                    Lista_Tokens.Add(palabra);
                                    if(Verificar_Toknes(texto.Substring(nTempo + 1), 0)) /*TERMINAR MÁS TARDE*/
                                    {
                                        PosActual += Defi_Nueva + 1 + nTempo;
                                        string tontita = texto[PosActual].ToString();
                                        return Validar_Tokens(texto.Substring(Defi_Nueva + 2 + nTempo));
                                    }
                                    return false;
                                }
                            default:
                                return Manejo_Error("El token esta mal definido porque se esperaba un signo igual", PosActual -palabra.Length);
                        }
                    }
                }
                else if(!Aceptar_Token)
                {
                    return Manejo_Error("Se debe de ingresar un Token.", PosActual + nTempo);
                }
                else if(Vino_Producciones_Titulo == false)
                {
                    return Manejo_Error("Se debe de definir el nombre de 'PRODUCTIONS'.", PosActual);
                }
                return false;
            }
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
            if(Validar_Expression_Token(pos, texto, pos))
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
            if(texto != "")
            {
                if(posinic < texto.Length)
                {
                    posinic = OmitirEspacios(texto, posinic);
                    switch (texto[posinic])
                    {
                        case '\'':
                            {
                                if((posinic+2) < texto.Length)
                                {
                                    string tontito = texto[posinic + 2].ToString();
                                    string tontito2 = texto[posinic + 1].ToString();

                                    if((texto[posinic + 2] == '\'' && texto[posinic + 1] != '\'') || texto[posinic + 1] == '\'')
                                    {
                                        HayValor = true;
                                        PIPE();
                                        Defi_Nueva = posinic + 2;
                                        if(Vino_PIPE)
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
                                if (texto.Length >= (posinic+1))
                                {
                                    if(texto[posinic+1] != ')')
                                    {
                                        bool tempo = false;
                                        for(int i = posinic + 1; i < texto.Length; i++)
                                        {
                                            if(texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                                            {
                                                switch(texto[i])
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
                                                if(!tempo)
                                                {
                                                    if(Char.IsLetter(texto[i]))
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
                                        if(!tempo)
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
                                if(HAY_Parentesis < 0)
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
                                if(AnalizarSimbolo(posinic, texto, posfin))
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
                                   if(!Acc_Token)
                                    {
                                        bool tempo = false;
                                        for(int i = posinic +1; posinic < texto.Length; i++)
                                        {
                                            if (texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                                            {
                                                switch(texto[i])
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
                                                if(!tempo)
                                                {
                                                    if(Char.IsLetter(texto[i]))
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

                                        if(!tempo)
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
                                if(Char.IsLetter(texto[posinic]))
                                {
                                    string palabrita = Obtener_Palabra(texto, posinic);
                                    if(palabrita.ToLower() == "check")
                                    {
                                        posinic = OmitirEspacios(texto, posinic + 5);
                                        if(texto[posinic] == '.') //Quiere decir que después del check termino todo
                                        {
                                            if (HAY_Parentesis == 0)
                                            {
                                                Viene_Check = true;
                                                Defi_Nueva = posinic;
                                                return true;
                                            }
                                        }
                                        return Manejo_Error("Se esperaba un punto para terminar la definción del token.", PosActual + posinic);
                                    }
                                    else if (Es_Reservada(palabrita))
                                    {
                                        return Manejo_Error("No puede utiilzar una palabra reservada en la definción de un token.", PosActual + posinic);
                                    }
                                    else if(Lista_Conjuntos.Contains(palabrita.ToLower()))
                                    {
                                        HayValor = true;
                                        Defi_Nueva = posinic + palabrita.Length;
                                        if(Vino_PIPE)
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
                if(texto[i] != ' ' && texto[i] != '\n' && texto[i] != '\t')
                {
                    switch(texto[i]) //Se analiza los parentesis y comillas de cierre
                    {
                        case ')':
                            return true;
                        case '\'':
                            return true;
                        case '"':
                            return true;
                    }
                    if(Char.IsLetterOrDigit(texto[i]))
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
            if(Vino_PIPE)
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
            if(Char.IsLetter(texto[pos]))
            {
                string palabra = Obtener_Palabra(texto, pos);
                if(palabra.ToLower() == "left")
                {
                    pos = OmitirEspacios(texto, pos + palabra.Length);
                    if(texto[pos] == '.') //Termino el operador exitosamente
                    {
                        PosActual += pos + 1;
                        Defi_Nueva = pos;
                        Vino_L = true;
                        return true;
                    }
                    else
                    {
                        return Manejo_Error("Se esperaba un punto.", PosActual + pos);
                    }
                }
                else if(palabra.ToLower() == "right")
                {
                    pos = OmitirEspacios(texto, pos + palabra.Length);
                    if (texto[pos] == '.') //Termino el operador exitosamente
                    {
                        PosActual += pos + 1;
                        Defi_Nueva = pos;
                        Vino_R = true;
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
                        Lista_KeyWords.Add(tempo[0].ToString());
                        if (tempo.Length > 1)
                        {
                            return Manejo_Error("El operador analizado no esta definido correctamente.", PosActual + pos);
                        }
                        else
                        {
                            pos = OmitirEspacios(texto, i + 1);
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
                                return true;
                            }
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
                        Lista_KeyWords.Add(tempo[0].ToString());
                        if (tempo.Length > 1)
                        {
                            return Manejo_Error("El operador analizado no esta definido correctamente.", PosActual + pos);
                        }
                        else
                        {
                            pos = OmitirEspacios(texto, i + 1);
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
                                if (Vino_L == false || Vino_R == false)
                                {
                                    return Manejo_Error("No viene la palabra left o rigth para definir una keyword valida", PosActual + pos);
                                }
                                PosActual += pos + 1;
                                Defi_Nueva = pos;
                                return true;
                            }
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
                    if(Vino_L == false || Vino_R == false)
                    {
                        return Manejo_Error("No viene la palabra left o rigth para definir una keyword valida", PosActual + pos);
                    }
                    PosActual += pos + 1;
                    Defi_Nueva = pos;
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
        /// Función que valida si la keyword analizada es válida
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool Validar_KeyWord(string texto, int pos)
        {
            pos = OmitirEspacios(texto, pos);
            if(pos == -1)
            {
                return Manejo_Error("Error al momento de definir los Keywords.", PosActual);
            }
            else
            {
                if(texto[pos] == '\"')
                {
                    pos = OmitirEspacios(texto, pos + 1);
                    int temporal = pos; //Variable Local Patita
                    string palabra = Obtener_Palabra(texto, pos);
                    pos = OmitirEspacios(texto, pos + palabra.Length);
                    if(texto[pos] == '\"')
                    {
                        if(EsPalabra(palabra))
                        {
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
                else if(texto[pos]=='\'')
                {
                    pos = OmitirEspacios(texto, pos + 1);
                    int temporal = pos; //Variable Local Patita
                    string palabra = Obtener_Palabra(texto, pos);
                    pos = OmitirEspacios(texto, pos + palabra.Length);
                    if (texto[pos] == '\'')
                    {
                        if (EsPalabra(palabra))
                        {
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
            if(Char.IsLetter(texto[pos]))
            {
                string palabrita = Obtener_Palabra(texto, pos);
                if(palabrita.ToLower() == "comments")
                {
                    Vino_Comment = true;
                    return Validar_Comentarios(texto, palabrita.Length + pos);
                }
                else if(palabrita.ToLower() == "to") /// quiere decir que el comentario ya inicio
                {
                    if(YaInicio) //Parte que nos dice que caracter o caracteres forman el inicio del comentario
                    {
                        Vino_TO = true;
                        return Validar_Comentarios(texto, palabrita.Length + pos);
                    }
                    else
                    {
                        return Manejo_Error("No se ha definido el inicio del comentario.", PosActual + pos);
                    }
                }
                else if(palabrita.ToLower() == "comentario") //Esto quiere decir que es un ALias de comentario
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
            else if(texto[pos] == '\'')
            {
                if(Vino_TO)
                {
                    if(!def_coment)
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
                for(i = pos+1; i < texto.Length; i++)
                {
                    if(texto[i] != '\'')
                    {
                        palabrita += texto[i];
                    }
                    else
                    {
                        tempo_found = true;
                        break;
                    }
                }
                if(tempo_found)
                {
                    palabrita = palabrita.TrimEnd();
                    palabrita = palabrita.TrimStart();
                    if(palabrita.IndexOf(' ') == -1 && palabrita != "")
                    {
                        return Validar_Comentarios(texto, i + 1);
                    }
                }
                return Manejo_Error("El comentario definido es incorrecto ya que no viene comilla simple que cierre", PosActual + pos);
            }
            else if(texto[pos] == '\"')
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
                    if (texto[pos] == '\'')
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
                        return Validar_Comentarios(texto, i + 1);
                    }
                }
                return Manejo_Error("El comentario definido es incorrecto", PosActual + pos);
            }
            else if(texto[pos] == '.') //Termino el comentario
            {
                if(LogroCerrar || Vino_Comentario_ESP)
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
            if(texto.Trim() == "" && Existen_Producciones)
            {
                return true;
            }
            int nTempo = OmitirEspacios(texto, 0);
            if( nTempo != -1)
            {
                if(texto[nTempo] == '<')
                {
                    nTempo = OmitirEspacios(texto, nTempo + 1);
                    int tempo = nTempo;
                    string palabrita = Obtener_Palabra(texto, nTempo);
                    nTempo = OmitirEspacios(texto, nTempo + palabrita.Length);
                    if (texto[nTempo] == '>')
                    {
                        nTempo = OmitirEspacios(texto, nTempo + 1);
                        if(texto[nTempo] == '-' && texto[nTempo + 1] == '>')
                        {
                            Lista_Producciones.Add(palabrita.ToLower());
                            PosActual += nTempo + 2;
                            if (Validar_Produccion(texto.Substring(nTempo + 2), 0))
                            {
                                Existen_Producciones = true;
                                return Analiza_Producctions(texto.Substring(OtraProduccion + nTempo + 2));
                            }
                            return false;
                        }
                        return Manejo_Error("Se esperaba el símbolo de la flecha '->' para generar una produccón válida.", PosActual +palabrita.Length);
                    }
                    else
                    {
                        return Manejo_Error("Se esperaba el símbolo '>'", PosActual + nTempo);
                    }
                }
                if(Char.IsLetter(texto[nTempo]))
                {
                    string start = "START";
                    nTempo = OmitirEspacios(texto, nTempo);
                    string palabrita2 = Obtener_Palabra(texto, nTempo);
                    nTempo = OmitirEspacios(texto, nTempo + palabrita2.Length);
                    if(Lista_Producciones.Contains(start.ToLower()))
                    {
                        if (palabrita2.ToLower() == "end")
                        {
                            nTempo = OmitirEspacios(texto, nTempo);
                            if(texto[nTempo] == '.')
                            {
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
                        " \n 2. La producción 'START' no existe en las producciones.", PosActual + nTempo + start.Length);
                }
                return Manejo_Error("Se esperaba el símbolo '<' por lo que la producción es incorrecta.", PosActual + nTempo);
            }
            else
            {
                return Manejo_Error("La definción de la producción es incorrecta.", PosActual);
            }
        }

        bool Vino_Signos_Mayor_Menor;

        /// <summary>
        /// Función que valida y analiza la producción o producciones entrantes
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool Validar_Produccion(string texto, int tem)
        {
            tem = OmitirEspacios(texto, tem);
            if(tem == -1)
            {
                return Manejo_Error("La definición de la producción es erronea.", PosActual);
            }
            if(Char.IsLetter(texto[tem]) && texto[tem] != 'ɛ' && texto[tem] != 'Ɛ' && texto[tem] != 'Ԑ')
            {
                //Posiblemente meta la validación con los tokens
                string palabrita = Obtener_Palabra(texto, tem);
                if (Lista_Tokens.Contains(palabrita.ToLower()))
                {
                    Vino_Algo_Antes_Llave = true;
                    return Validar_Produccion(texto, tem + palabrita.Length);
                }
                return Manejo_Error("La palabra ingresada no es un token.", PosActual + tem + palabrita.Length);
            }
            else if(texto[tem] == '<') //Hay producción
            {
                tem = OmitirEspacios(texto, tem + 1);
                string palabrita = Obtener_Palabra(texto, tem);
                tem = OmitirEspacios(texto, tem + palabrita.Length);
                if(texto[tem] == '>') //Fin producción 
                {
                    Vino_Algo_Antes_Llave = true;
                    Vino_Signos_Mayor_Menor = true;
                    return Validar_Produccion(texto, tem + 1);
                }
                return Manejo_Error("Se esperaba el símbolo '>'", PosActual + tem);
            }
            else if(texto[tem] == '\'') //Esto quiere decir que es un token
            {
                string x = texto[tem].ToString();
                if((tem +2)  < texto.Length)
                {
                    if(texto[tem +2] == '\'')
                    {
                        Vino_Algo_Antes_Llave = true;
                        return Validar_Produccion(texto, tem + 3);
                    }
                    tem = OmitirEspacios(texto, tem + 1);
                    string palabrita = "";

                    for(int i = tem; i < texto.Length; i++)
                    {
                        if(texto[i] == '\'')
                        {
                            break;
                        }
                        else if(char.IsWhiteSpace(texto[i]))
                        {
                            break;
                        }
                        palabrita += texto[i];
                    }
                    tem = OmitirEspacios(texto, tem + palabrita.Length);
                    if(texto[tem] == '\'') //Meter posible validación con los keywords
                    {
                        Vino_Algo_Antes_Llave = true;
                        return Validar_Produccion(texto, tem + 1);
                    }
                    return Manejo_Error("Se esperaba el signo de punto: \'.", PosActual + tem);
                }
                return Manejo_Error("La producción definida es incorrecta.", PosActual + tem);
            }
            else if(texto[tem] == '\"') //Esto quiere decir que es un token
            {
                if ((tem + 2) < texto.Length)
                {
                    if (texto[tem + 2] == '\"')
                    {
                        Vino_Algo_Antes_Llave = true;
                        return Validar_Produccion(texto, tem + 2);
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
                        Vino_Algo_Antes_Llave = true;
                        return Validar_Produccion(texto, tem + 1);
                    }
                    return Manejo_Error("Se esperaba el signo de punto: \'.", PosActual + tem);
                }
                return Manejo_Error("La producción definida es incorrecta.", PosActual + tem);
            }
            else if(texto[tem] == '{')
            {
                if (Vino_Algo_Antes_Llave == true)
                {
                    tem = OmitirEspacios(texto, tem + 1);
                    string palabritra = Obtener_Palabra(texto, tem);
                    tem = OmitirEspacios(texto, tem + palabritra.Length);
                    if (texto[tem] == '}')
                    {
                        int posTempo = OmitirEspacios(texto, tem + 1);
                        if (texto[posTempo] == '.' || texto[posTempo] == '|')
                        {
                            Vino_Algo_Antes_Llave = false;
                            return Validar_Produccion(texto, tem + 1);
                        }
                        return Manejo_Error("Se esperaba un '.' o '|' después de la llave que cierra.", PosActual + posTempo - palabritra.Length);
                    }
                    return Manejo_Error("Se esperaba una llave que cierra '}'", PosActual + tem );
                }
                return Manejo_Error("No se puede definir una llave que abre '{' sin que antes venga por lo menos un epsilon", PosActual + tem);
            }
            else if(texto[tem] == '|')
            {
                if(Otro_Analisis_Produccion(texto, tem))
                {
                    Vino_Algo_Antes_Llave = false;
                    return Validar_Produccion(texto, tem + 1);
                }
                return Manejo_Error("La producción definida es incorrecta.", PosActual + tem);
            }
            else if(texto[tem] == '.') //Fin produccón
            {
                if(Otro_Analisis_Produccion(texto, tem))
                {
                    PosActual += tem + 1;
                    OtraProduccion = tem + 1;
                    Vino_Algo_Antes_Llave = false;
                    return true;
                }
                return Manejo_Error("La producción definida es incorrecta", PosActual + tem);
            }
            else if(texto[tem] == 'ɛ' || texto[tem] == 'Ɛ' || texto[tem] == 'Ԑ' || texto[tem] == '~')
            {
                Vino_Algo_Antes_Llave = true;
                return Validar_Produccion(texto, tem + 1);
            }
            return Manejo_Error("La producción definida es incorrecta pues falta un punto para poder finalizarla", PosActual);
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
