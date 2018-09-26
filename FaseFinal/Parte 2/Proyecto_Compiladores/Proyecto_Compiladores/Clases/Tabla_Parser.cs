using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_Compiladores.Clases
{
    class Tabla_Parser
    {
        /*Fase 6*/
        public static List<string> Pila_Tokens = new List<string>();
        public static List<string> Columnas = new List<string>();
        public static List<Nodo_Tabla> Tabla = new List<Nodo_Tabla>();
        List<int> Pila_Tempo = new List<int>();

        /*Fase 1 Parte 2*/
        public static List<NodoTablaSimbolos> SuperTablaSimbo = new List<NodoTablaSimbolos>();
        List<string> Valores_En_Pila = new List<string>();
        List<int> Valores_Constantes = new List<int>();
        List<string> Valores_De_Afuera = new List<string>();

        int Error;
        int Pos_Shift;
        int Pos_Reduce;
        int Pos_Token;

        public static string Texto;

        /// <summary>
        /// Constructor de la Tabla que genera la tabla que se va utlizar para parsear
        /// </summary>
        public Tabla_Parser()
        {
            Iniciar_Todo();
        }

        #region Llenado y Genrar Tabla

        /// <summary>
        /// Método que inicia todos los procesos requeridos para calcular el parser
        /// </summary>
        private void Iniciar_Todo()
        {
            Crear_Generar_Tabla();
            Llenado_Tabla();
        }

        /// <summary>
        /// Método que genera la tabla del parseo
        /// </summary>
        private void Crear_Generar_Tabla()
        {
            Tabla.Clear();
            Columnas.Clear();

            //Se agregan los símbolos de los tokens y no terminales a las columnas
            for (int i = 0; i < Analizador.Lista_Tokens.Count; i++)
            {
                Columnas.Add(Analizador.Lista_Tokens[i].Simbolo_Token);
            }
            Columnas.Add("$");
            for (int i = 0; i < Analizador.Lista_NT.Count; i++)
            {
                Columnas.Add(Analizador.Lista_NT[i].No_Terminal_NT);
            }
        }

        /// <summary>
        /// Método que se utliza para llenar la tabla del parser con los valores básicos
        /// </summary>
        private void Llenado_Tabla()
        {
            //Ciclo General
            for (int i = 0;  i < Tabla_Estados.Kernels_.Count; i++)
            {
                //Se van a sacar los valores de los Nucleso
                for (int j = 0; j < Tabla_Estados.Kernels_[i].NT_Nuevo_Tempo.Count; j++)
                {
                    int Pos_Punto = Tabla_Estados.Kernels_[i].Produ_Nueva_Tempo[j].IndexOf(Tabla_Estados.Punto);
                    if (Pos_Punto + 1 == Tabla_Estados.Kernels_[i].Produ_Nueva_Tempo[j].Count)
                    {
                        for (int u = 0; u < Tabla_Estados.Kernels_[i].LA_Nuevo_Tempo[j].Count; u++)
                        {
                            string terminal = Valor_Simbolo(Tabla_Estados.Kernels_[i].LA_Nuevo_Tempo[j][u]);
                            int pos_te = Columnas.IndexOf(terminal);
                            int num_produ = Numero_Produccion(Tabla_Estados.Kernels_[i].Produ_Nueva_Tempo[j], Tabla_Estados.Kernels_[i].NT_Nuevo_Tempo[j]);
                            Nodo_Tabla nuevo_nodo = new Nodo_Tabla(num_produ, 'R', i, pos_te);
                            Insertar_Nuevo(nuevo_nodo);
                        }
                    }
                }

                //Se van a obtener las producciones propias del estado
                for (int j = 0; j < Tabla_Estados.Kernels_[i].NT_.Count; j++)
                {
                    int Pos_Punto = Tabla_Estados.Kernels_[i].Produ[j].IndexOf(Tabla_Estados.Punto);
                    if(Pos_Punto + 1 == Tabla_Estados.Kernels_[i].Produ[j].Count)
                    {
                        for (int u = 0; u < Tabla_Estados.Kernels_[i].LookA[j].Count; u++)
                        {
                            string terminal = Valor_Simbolo(Tabla_Estados.Kernels_[i].LookA[j][u]);
                            int pos_te = Columnas.IndexOf(terminal);
                            int num_produ = Numero_Produccion(Tabla_Estados.Kernels_[i].Produ[j], Tabla_Estados.Kernels_[i].NT_[j]);
                            Nodo_Tabla nuevo_nodo = new Nodo_Tabla(num_produ, 'R', i, pos_te);
                            Insertar_Nuevo(nuevo_nodo);
                        }
                    }
                }

                //Se van a leer los GoTo
                for (int j = 0; j < Tabla_Estados.Kernels_[i].N_Estado.Count; j++)
                {
                    int n_estado = Tabla_Estados.Kernels_[i].N_Estado[j];
                    string terminal = Tabla_Estados.Kernels_[i].GoTo[j];
                    int pos_termi = Columnas.IndexOf(terminal);

                    //Quiere decir que es un nt
                    if(terminal[0] == '<')
                    {
                        Nodo_Tabla nodo_Nuevo = new Nodo_Tabla(i, 'E', n_estado, pos_termi);
                        Insertar_Nuevo(nodo_Nuevo);
                    }
                    else //Es un t
                    {
                        Nodo_Tabla nodo_nuevo = new Nodo_Tabla(i, 'S', n_estado, pos_termi);
                        Insertar_Nuevo(nodo_nuevo);
                    }
                }
            }
        }

        /// <summary>
        /// Método que agrega un nuevo nodo a la tabla
        /// </summary>
        /// <param name="valor"></param>
        private bool Insertar_Nuevo(Nodo_Tabla valor)
        {
            for (int i = 0; i < Tabla.Count; i++)
            {
                int f = Tabla[i].Fila;
                int col = Tabla[i].Columna;

                if(Tabla[i].Fila == valor.Fila && Tabla[i].Columna == valor.Columna)
                {
                    Tabla[i].Lista_Nodos.Add(new Manejadora(valor.Lista_Nodos[0].Numero_Nodo, valor.Lista_Nodos[0].Caracter));
                    return true;
                }
            }
            Tabla.Add(valor);
            return true;
        }

        /// <summary>
        /// Función que devuelve el valor del símbolo según en LookAhead analizado
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private string Valor_Simbolo(int n)
        {
            //Se valida si es o no terminal
            if(n < 0)
            {
                return Analizador.Lista_NT[(-n) - 1].No_Terminal_NT;
            }
            else
            {
                if(n == Tabla_Estados.Dolar)
                {
                    return "$";
                }
                else
                {
                    return Analizador.Lista_Tokens[n - 1].Simbolo_Token;
                }
            }
        }

        /// <summary>
        /// Función que devuelve el núnero de la producción.
        /// </summary>
        /// <param name="produ">Lista que se utiliza para saber la cantidad de producciones que tiene</param>
        /// <param name="nt">Valor por parámetro que se utliza para saber que nt es el analizado </param>
        /// <returns></returns>
        private int Numero_Produccion(List<int> produ, string nt)
        {
            List<int> Tempo = new List<int>();
            Cambiar_Producciones(Tempo, produ);
            for (int i = 0; i < Analizador.Lista_Producciones.Count; i++)
            {
                if(nt == Analizador.Lista_Producciones[i].NoTerminal_PR)
                {
                    if(Es_Igual(Analizador.Lista_Producciones[i].Elementos,Tempo))
                    {
                        return Analizador.Lista_Producciones[i].Produccion_PR;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Método utlizado para transferir las producciones a la lista temporal dest para
        /// ser analizada posteriormente
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="ori"></param>
        private void Cambiar_Producciones(List<int> dest, List<int> ori)
        {
            if(ori.Count == 1)
            {
                dest.Add(0);
            }
            else
            {
                for (int i = 0; i < ori.Count; i++)
                {
                    if(ori[i] != Tabla_Estados.Punto)
                    {
                        dest.Add(ori[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Fiunción que analiza si las listas son iguales
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="ori"></param>
        /// <returns></returns>
        private bool Es_Igual(List<int> dest, List<int> ori)
        {
            if(dest.Count != ori.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < dest.Count; i++)
                {
                    if(dest[i] != ori[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        #endregion

        #region Parseo

        string[] tempoPalabrita = null;
        string concatenar = "";
        string agregar = "";

        /// <summary>
        /// Método que genera la expresón que se desea analizar
        /// </summary>
        public void Generar_Expresion()
        {
            string[] palabrita = frmCompis.Cadena_Tokens[0].Split(' ');
            Error = Analizador.Lista_Tokens.Count + 1;
            temporal = "";

            Pila_Tempo.Clear();
            Pila_Tokens.Clear();
            /*Valores para TS*/
            Valores_En_Pila.Clear();
            Valores_Constantes.Clear();
            Valores_De_Afuera.Clear();
            for (int i = 0; i < frmCompis.Cadena_Tokens.Count; i++)
            {
                palabrita = frmCompis.Cadena_Tokens[i].Split(' ');
                temporal = Buscar_Include(palabrita[0].ToString());
                string ruta = "";
                if (temporal == "include")
                {
                    /*Debo de analizar el archivo que se esta inlcuyendo. Con analizar me refiero a pasarlo a un archivo .txt temporal que se a a cargar
                     Luego pasar el archivo original a ese otro archivo a analizar
                     Combinados meterlos todos a lo que ya se tiene
                     Esto lo vas a tocar en el scanner para que te menta a analizar ese archivo en el temporal y te saque lo que necesites*/
                    for (int j = 1; j < frmCompis.Cadena_Tokens.Count; j++)
                    {
                        tempoPalabrita = frmCompis.Cadena_Tokens[j].Split(' ');
                        concatenar = concatenar + tempoPalabrita[0];
                        if(concatenar.Contains(".txt"))
                        {
                            ruta = "C:\\Users\\Joshy\\Dropbox\\URL\\3ero\\Segundo Ciclo\\Compiladores\\Pruebas\\Fase 7 - P2_Fase1\\Gramática Hulk\\" + concatenar;
                            break;
                        }
                    }

                    Agregar_Lista_Include(ruta);
                }

                int num_Token = int.Parse(palabrita[4]);
                Pila_Tempo.Add(num_Token);
                if(num_Token != Error)
                {
                    //Se agregan a la Pila de Tokens
                    if(num_Token > Analizador.Lista_Tokens.Count)
                    {
                        Pila_Tokens.Add(Analizador.Lista_KeyWords[num_Token - Analizador.Lista_Tokens.Count - 1]);
                    }
                    else
                    {
                        Pila_Tokens.Add(Analizador.Lista_Tokens[num_Token - 1].Simbolo_Token);
                    }
                    Valores_En_Pila.Add(palabrita[0]);
                }
            }

        }

        int cant_include = 0;
        string temporal = "";

        /// <summary>
        /// Función que busca los includes en su pos necesaria
        /// </summary>
        /// <param name="palabrita"></param>
        /// <returns></returns>
        public string Buscar_Include(string palabrita)
        {
            for (int i = 0; i < palabrita.Length; i++)
            {
                if (palabrita == "include")
                {
                    return temporal = "include";
                }
                cant_include++;
                break;
            }

            return "";
        }

        /// <summary>
        /// Método que agrega los métodos de los inlcude a la lista de cadenas para analizar
        /// </summary>
        /// <param name="ruta"></param>
        public void Agregar_Lista_Include(string ruta)
        {
            if (ruta != "")
            {
                StreamReader lector = new StreamReader(ruta);
                string linea = "";


                for (int j = 0; j < frmCompis.Cadena_Tokens.Count; j++)
                {
                    tempoPalabrita = frmCompis.Cadena_Tokens[j].Split(' ');
                    agregar = tempoPalabrita[0];

                    if (agregar == "class")
                    {
                        string tempo = "";
                        for (int k = j + 1; k < frmCompis.Cadena_Tokens.Count; k++)
                        {
                            tempoPalabrita = frmCompis.Cadena_Tokens[k].Split(' ');
                            tempo = tempoPalabrita[0];
                            if (tempo == "void")
                            {
                                int x = k;
                                while ((linea = lector.ReadLine()) != null)
                                {
                                    if (linea != "")
                                    {
                                        frmCompis.Cadena_Tokens.Insert(x, linea);

                                        if(frmCompis.Cadena_Tokens[x+1].Contains("void") || frmCompis.Cadena_Tokens[x+1].Contains("int") || frmCompis.Cadena_Tokens[x + 1].Contains("char"))
                                        {

                                        }
                                        else
                                        {
                                            x++;
                                        }
                                    }
                                }
                                lector.Close();
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método que analiza la expresión cargada para el análisis
        /// </summary>
        /// <returns></returns>
        public bool Inicio_Parser()
        {
            //Si se tiene el número del error entonces se deuvelve falso y por ende error en la gramática
            if(Pila_Tempo.Contains(Error))
            {
                return false;
            }

            List<string> Pilita = new List<string>(); //FILO
            Pilita.Clear();
            SuperTablaSimbo.Clear();
            Valores_De_Afuera.Clear();
            Pilita.Add("0");
            Pila_Tokens.Add("$");
            Texto = "";

            while(true)
            {
                //Debo de tener una condición de parada si no me voy a emlupar
                if(Pila_Tokens.Count == 1 && Pila_Tokens[0] == "$" && Pilita.Count == 2 && Pilita.Last() == "<start>")
                {
                   //Texto +=  Lista_Datos(Pilita) + "\t\t" + Lista_Datos(Pila_Tokens) + "\n";
                    return true;
                }


                if (Pilita.Count == 38)
                {

                }

                if(Valores_De_Afuera.Count == 160)
                {

                }

                string elemento = Pilita.Last();

                if(elemento == "")
                {

                }


                int estado;
               // Texto += Lista_Datos(Pilita) + "\t\t" + Lista_Datos(Pila_Tokens) + "\n";

                //Se va a verificar si el último elemento ingresado es un número
                if(int.TryParse(elemento, out estado)) //Se trata de convertir a entero
                {
                    int contador_tempora = 0;
                    contador_tempora++;

                    string tempoSeiguiente = Pila_Tokens.First();

                    if(tempoSeiguiente == "'include'")
                    {
                        while (Pila_Tokens.First() != "'public'")
                        {
                            Pila_Tokens.RemoveAt(0);
                            Valores_En_Pila.RemoveAt(0);
                        }
                    }

                     List <Manejadora> siguiene_dato = Encontrar_Siguiente(estado, Pila_Tokens.First());

                    #region SuperParche
                    //Super Parche

                    #region GramaticaJoshy
                    if ( frmCompis.SuperRuta.Contains("MiniCSharp.txt") && (Pila_Tokens.First() == "'case'" || Pila_Tokens.First() == "'default'" || Pila_Tokens.First() == "':'"))
                    {
                        switch(estado)
                        {
                            case 80:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(128, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 67:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(79, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 42:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(126, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 195:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(54, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 40:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(46, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 91:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(147, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 93:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(144, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 54:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(145, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 57:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(142, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 210:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(63, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 55:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(55, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                            case 90:
                                {
                                    List<Manejadora> temporal = new List<Manejadora>();
                                    Manejadora nuevoDato = new Manejadora(57, 'R');
                                    temporal.Add(nuevoDato);
                                    siguiene_dato = temporal;
                                    break;
                                }
                        }
                    }
                    #endregion

                    #endregion

                    if (siguiene_dato != null)
                    {
                        /*Si llegaran a darse colisiones es decir, que haya más de dos datos en un elemento de la tabla*/
                        if(siguiene_dato.Count > 1)
                        {
                            //Si existe shift y como es mayor a uno entonces sxiste reduce en esa posición tambien
                            if(Es_Shift(siguiene_dato))
                            {
                                Es_Reduce(siguiene_dato);
                                int tempo_shift = Analiza_Presedencia(Pila_Tokens.First());
                                int tempo_reduce = Analiza_Presedencia_Reduce(siguiene_dato[Pos_Reduce].Numero_Nodo);

                                /*
                                 * Si la precedencia de simb es mayor que la de regl entonces: desplazamiento. 
                                 * Si la precedencia de simb es menor que la de regl entonces: reducción.
                                 *  Si la precedencia de simb es igual a la de regl, debemos mirar la asociatividad
                                            - Si simb es asociativo por la izquierda: reducción
                                            - Si simb es asociativo por la derecha: desplazamiento
                                            - Si simb es no asociativo: error
                                 */
                                if (tempo_shift > tempo_reduce)
                                {
                                    Pilita.Add(Pila_Tokens.First()); //Se agrega a la pila
                                    Valores_De_Afuera.Add(Pila_Tokens.First()); //Se agrega a los valores de afuera
                                    Pila_Tokens.RemoveAt(0); //Se quita de la pila de tokens
                                    Pilita.Add(siguiene_dato[Pos_Shift].Numero_Nodo.ToString()); //Se agrega el número
                                }
                                else if (tempo_shift == tempo_reduce)
                                {
                                    switch (Analizador.Lista_Tokens[Pos_Token].Asociatividad_Token)
                                    {
                                        case "left": //Reduce
                                            {
                                                int tempo = Analizador.Lista_Producciones[siguiene_dato[Pos_Reduce].Numero_Nodo - 1].Longitud_PR * 2;
                                                HacerAccion(Analizador.Lista_Acciones[siguiene_dato[Pos_Reduce].Numero_Nodo - 1]);
                                                for (int i = 0; i < tempo; i++)
                                                {
                                                    Pilita.RemoveAt(Pilita.Count - 1);
                                                }
                                                Pilita.Add(Analizador.Lista_Producciones[siguiene_dato[Pos_Reduce].Numero_Nodo - 1].NoTerminal_PR);
                                                break;
                                            }
                                        case "right": //Shift
                                            {
                                                Pilita.Add(Pila_Tokens.First());
                                                Valores_De_Afuera.Add(Pila_Tokens.First()); //Se agrega a la lista de los valores a sacar
                                                Pila_Tokens.RemoveAt(0);                               
                                                Pilita.Add(siguiene_dato[Pos_Shift].Numero_Nodo.ToString());
                                                break;
                                            }
                                        default:
                                            {
                                                return false;
                                            }
                                    }
                                }
                                else // si tempo reduce > tempo shift
                                {
                                    /*Se va a multiplicar la longitud de la producción por dos, fue lo que se enseño en clase
                                     Esto con el objetivo de ir anulzndo el parser para validar la expresión si está o no buena*/
                                    int eliminar = Analizador.Lista_Producciones[siguiene_dato[Pos_Reduce].Numero_Nodo - 1].Longitud_PR * 2;
                                    HacerAccion(Analizador.Lista_Acciones[siguiene_dato[Pos_Reduce].Numero_Nodo - 1]);
                                    for (int i = 0; i < eliminar; i++)
                                    {
                                        Pilita.RemoveAt(Pilita.Count - 1);
                                    }
                                    //Una vez eliminada la cantidad entonces de debe de agregar el no terminal respectivo a la tabla
                                    Pilita.Add(Analizador.Lista_Producciones[siguiene_dato[Pos_Reduce].Numero_Nodo - 1].NoTerminal_PR);
                                }
                            }
                            else //Quiere decir que hay un reduce en shift
                            {
                                Es_Reduce(siguiene_dato);
                                /*Se va a multiplicar la longitud de la producción por dos, fue lo que se enseño en clase
                                 Esto con el objetivo de ir anulzndo el parser para validar la expresión si está o no buena*/
                                int eliminar = Analizador.Lista_Producciones[siguiene_dato[Pos_Reduce].Numero_Nodo - 1].Longitud_PR * 2;
                                HacerAccion(Analizador.Lista_Acciones[siguiene_dato[Pos_Reduce].Numero_Nodo - 1]);
                                for (int i = 0; i < eliminar; i++)
                                {
                                    Pilita.RemoveAt(Pilita.Count - 1);
                                }
                                //Una vez eliminada la cantidad entonces de debe de agregar el no terminal respectivo a la tabla
                                Pilita.Add(Analizador.Lista_Producciones[siguiene_dato[Pos_Reduce].Numero_Nodo - 1].NoTerminal_PR);
                            }
                        }
                        else //entonces se va a verificar si es shift o reduce el valor que exite 
                        {
                           if(siguiene_dato[0].Caracter == 'S')
                            {
                                Pilita.Add(Pila_Tokens.First());
                                Valores_De_Afuera.Add(Pila_Tokens.First());
                                Pila_Tokens.RemoveAt(0);
                                Pilita.Add(siguiene_dato[0].Numero_Nodo.ToString());
                            }
                            else if(siguiene_dato[0].Caracter == 'R')
                            {
                                HacerAccion(Analizador.Lista_Acciones[siguiene_dato[0].Numero_Nodo - 1]);
                                int eliminar = Analizador.Lista_Producciones[siguiene_dato[0].Numero_Nodo - 1].Longitud_PR * 2;
                                for (int i = 0; i < eliminar; i++)
                                {
                                    Pilita.RemoveAt(Pilita.Count - 1);
                                }
                                //Una vez eliminada la cantidad entonces de debe de agregar el no terminal respectivo a la tabla
                                Pilita.Add(Analizador.Lista_Producciones[siguiene_dato[0].Numero_Nodo - 1].NoTerminal_PR);
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (elemento == "") //Esto quiere decir que es un NT y se va a buscar analizar de otra manera
                {
                    return false;
                }
                else if (elemento[0] == '<')
                {
                    string temporal = SiguienteEstado(elemento, Pilita[Pilita.Count - 2]);

                    if(temporal == "")
                    {

                    }

                    Pilita.Add(temporal);
                }
            }
        }

        /// <summary>
        /// Función que devuelve una lista de datos con valores útilces para el parser.
        /// Devuelve como valor $0   num + num $
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        private string Lista_Datos(List<string> lista)
        {
            string tempo = "";
            for (int i = 0; i < lista.Count; i++)
            {
                tempo += lista[i];
            }
            return tempo;
        }

        /// <summary>
        /// Función que devuelve una lista de nodos capaz de encontrar el siguiente elemento
        /// que se va analizar
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="elemento"></param>
        /// <returns></returns>
        private List<Manejadora> Encontrar_Siguiente(int estado, string elemento)
        {
            int i;
            for (i = 0; i < Columnas.Count; i++)
            {
                string temporal = Columnas[i];
                if(elemento == temporal)
                {
                    break;
                }
            }
            if(i == Columnas.Count)
            {
                return null;
            }
            else
            {
                for (int j = 0; j < Tabla.Count; j++)
                {
                    if(Tabla[j].Fila == estado && Tabla[j].Columna == i) //si se cumple entonces si se devuelve el valor del siguiente dato a analzr
                    {
                        return Tabla[j].Lista_Nodos;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Funciíon que obtiene el siguiente estado que se va analizar dentro del parser
        /// </summary>
        /// <param name="elemento"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        private string SiguienteEstado(string elemento, string estado)
        {
            int fil = int.Parse(estado);
            int col = Columnas.IndexOf(elemento);
            for (int i = 0; i < Tabla.Count; i++)
            {
                if(Tabla[i].Fila == fil && Tabla[i].Columna == col)
                {
                    return Tabla[i].Lista_Nodos[0].Numero_Nodo.ToString();
                }
            }

            return "";
        }

        /// <summary>
        /// Función que verifica si se puede realizar un reduce
        /// </summary>
        /// <param name="tempo"></param>
        /// <returns></returns>
        private bool Es_Reduce(List<Manejadora> tempo)
        {
            for (int i = 0; i < tempo.Count; i++)
            {
                if(tempo[i].Caracter == 'R')
                {
                    Pos_Reduce = i;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Función que verifica si es necesario realizar un shift
        /// </summary>
        /// <param name="tempo"></param>
        /// <returns></returns>
        private bool Es_Shift(List<Manejadora> tempo)
        {
            for (int i = 0; i < tempo.Count; i++)
            {
                if(tempo[i].Caracter == 'S')
                {
                    Pos_Shift = i;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Función que devuelve la presedenncia que el token posee
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private int Analiza_Presedencia(string token)
        {
            for (int i = 0; i < Analizador.Lista_Tokens.Count; i++)
            {
                if (token == Analizador.Lista_Tokens[i].Simbolo_Token)
                {
                    Pos_Token = i;
                    return Analizador.Lista_Tokens[i].Prescedencia_Token;
                }
            }
            return 0;
        }

        /// <summary>
        /// Analiza la presdencia que posee el token en la producción
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private int Analiza_Presedencia_Reduce(int produ)
        {
            /*Se recorrera para saber el número de producción en la que esta
             Luego se recorrera para saber la cantidad de elementos que esta posee
             Se va ir a buscar el temrinal dentro de esos elementos*/
            for (int i = 0; i < Analizador.Lista_Producciones.Count; i++)
            {
                if(Analizador.Lista_Producciones[i].Produccion_PR == produ)
                {
                    for (int j = 0; j < Analizador.Lista_Producciones[i].Elementos.Count; j++)
                    {
                        int terminal = Analizador.Lista_Producciones[i].Elementos[j];
                        if(terminal > 0)
                        {
                            return Analizador.Lista_Tokens[terminal - 1].Prescedencia_Token;
                        }
                    }
                    return -1;
                }
            }
            return -1;
        }
       
        /// <summary>
        /// Método que realiza las acciones específicas de las acciones
        /// </summary>
        /// <param name="accion"></param>
        private void HacerAccion(string accion)
        {
            int cantidadParametros = 0;
            int temporal = 0;
            int numero1 = 0;
            int numero2 = 0;
            int resultado = 0;
            int contNiveles = 0;
            int parametros = 0;

            //switch (resultado)
            //{
            //    case 5 *  5:
            //        {
            //            break;
            //        }
            //}

            switch (accion.ToLower())
            {  
                case "nombreclase":
                    {
                        NodoTablaSimbolos nuevoSimbo = new NodoTablaSimbolos(); //Se crea una nueva tabla de símbolos
                        nuevoSimbo.QueTipo = "class"; //Es tipo clase
                        nuevoSimbo.Nombre = Valores_En_Pila[Valores_De_Afuera.Count - 1];
                        nuevoSimbo.EsMetodo = "class"; //El método es tipo clase
                        SuperTablaSimbo.Add(nuevoSimbo) ; //Se agrega a la tabla de símbolos
                        break;
                    }
                case "finclase":
                    {
                        break;
                    }
                case "nombremetodo":
                        NodoTablaSimbolos nuevoNodo = new NodoTablaSimbolos();
                        nuevoNodo.QueTipo = Valores_De_Afuera[Valores_De_Afuera.Count - 2];
                        nuevoNodo.Nombre = Valores_En_Pila[Valores_De_Afuera.Count-1];
                        parametros = 0;
                        if(nuevoNodo.QueTipo == "'int'" || nuevoNodo.QueTipo == "'char'")
                        {
                            nuevoNodo.EsMetodo = "Función";
                        }
                        else
                        {
                            nuevoNodo.EsMetodo = "Metodo";
                        }

                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Add(nuevoNodo);
                        contNiveles++; //Se aumenta para saber que  nivel es
                        break;
                case "parametrometodo":
                        cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableNombre.Add(Valores_En_Pila[Valores_De_Afuera.Count - 1]);
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableTipo.Add(Valores_En_Pila[Valores_De_Afuera.Count - 2]);
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableValor.Add("0");
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].Tamanon.Add(0);
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableGPS.Add("parametro");
                        parametros++;
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].Cant_Parametros.Add(parametros);
                        break;
                case "guardarvariable":
                    {

                        if (SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count != 0)
                        {
                            cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableNombre.Add(Valores_En_Pila[Valores_De_Afuera.Count - 1]);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableTipo.Add(Valores_En_Pila[Valores_De_Afuera.Count - 2]);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableValor.Add("0");
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].Tamanon.Add(0);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableGPS.Add("local");
                        }
                        else
                        {
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableNombre.Add(Valores_En_Pila[Valores_De_Afuera.Count - 1]);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableTipo.Add(Valores_En_Pila[Valores_De_Afuera.Count - 2]);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableValor.Add("0");
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].Tamanon.Add(0);

                            //if (SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count == 0)
                            //{
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableGPS.Add("global"); //Hablar con estuardo javier y emilio ya que falta saber si es global o no
                                                                                                  //}
                                                                                                  //else
                                                                                                  //{

                            //    SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableGPS.Add("local"); //Hablar con estuardo javier y emilio ya que falta saber si es global o no
                            //}
                        }
                        break;
                    }
                case "finexprevariable":
                    {
                        if (SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count != 0)
                        {
                            cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                            int posFinal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableValor.Count - 1;
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableValor[posFinal] = Valores_Constantes.Last().ToString();
                            Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        }
                        else
                        {
                            int posFinal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableValor.Count - 1;
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableValor[posFinal] = Valores_Constantes.Last().ToString();
                            Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        }

                        break;
                    }
                case "guardarvariableconst":
                    {
                        if (SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count != 0)
                        {
                            cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableNombre.Add(Valores_En_Pila[Valores_De_Afuera.Count - 1]);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableTipo.Add(Valores_En_Pila[Valores_De_Afuera.Count - 2]);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableValor.Add("0");
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].Tamanon.Add(0);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableGPS.Add("local");
                        }
                        else
                        {
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableNombre.Add(Valores_En_Pila[Valores_De_Afuera.Count - 1]);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableTipo.Add(Valores_En_Pila[Valores_De_Afuera.Count - 2]);
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableValor.Add("0");
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].Tamanon.Add(0);

                            //if (SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count == 0)
                            //{
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableGPS.Add("global"); //Hablar con estuardo javier y emilio ya que falta saber si es global o no
                                                                                                  //}
                                                                                                  //else
                                                                                                  //{

                            //    SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableGPS.Add("local"); //Hablar con estuardo javier y emilio ya que falta saber si es global o no
                            //}
                        }

                        break;
                    }
                case "finexprevariableconst":
                    {
                        if (SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count != 0)
                        {
                            cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                            int posFinal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableValor.Count - 1;
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].VariableValor[posFinal] = Valores_Constantes.Last().ToString();
                            Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        }
                        else
                        {
                            int posFinal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableValor.Count - 1;
                            SuperTablaSimbo[SuperTablaSimbo.Count - 1].VariableValor[posFinal] = Valores_Constantes.Last().ToString();
                            Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        }
                        break;
                    }
                case "accionif":
                    {
                        cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                        temporal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Count;
                        nuevoNodo = new NodoTablaSimbolos();
                        nuevoNodo.Nombre = "if" + temporal.ToString();
                        nuevoNodo.EsMetodo = "condicional";
                        nuevoNodo.QueTipo = "ninguno";
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Add(nuevoNodo);
                        break;
                    }
                case "accionelse":
                    {
                        cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                        temporal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Count;
                        nuevoNodo = new NodoTablaSimbolos();
                        nuevoNodo.Nombre = "else" + temporal.ToString();
                        nuevoNodo.EsMetodo = "condicional";
                        nuevoNodo.QueTipo = "ninguno";
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Add(nuevoNodo);
                        break;
                    }
                case "accionswitch":
                    {
                        cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                        temporal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Count;
                        nuevoNodo = new NodoTablaSimbolos();
                        nuevoNodo.Nombre = "switch" + temporal.ToString();
                        nuevoNodo.EsMetodo = "condicional";
                        nuevoNodo.QueTipo = "ninguno";
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Add(nuevoNodo);
                        break;
                    }
                case "accioncase":
                    {
                        break;
                    }
                case "accionwhile":
                    {
                        cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                        temporal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Count;
                        nuevoNodo = new NodoTablaSimbolos();
                        nuevoNodo.Nombre = "while" + temporal.ToString();
                        nuevoNodo.EsMetodo = "ciclo";
                        nuevoNodo.QueTipo = "ninguno";
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Add(nuevoNodo);
                        break;
                    }
                case "acciondo":
                    {
                        cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                        temporal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Count;
                        nuevoNodo = new NodoTablaSimbolos();
                        nuevoNodo.Nombre = "do" + temporal.ToString();
                        nuevoNodo.EsMetodo = "ciclo";
                        nuevoNodo.QueTipo = "ninguno";
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Add(nuevoNodo);
                        break;
                    }
                case "accionfor":
                    {
                        cantidadParametros = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos.Count;
                        temporal = SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Count;
                        nuevoNodo = new NodoTablaSimbolos();
                        nuevoNodo.Nombre = "do" + temporal.ToString();
                        nuevoNodo.EsMetodo = "ciclo";
                        nuevoNodo.QueTipo = "ninguno";
                        SuperTablaSimbo[SuperTablaSimbo.Count - 1].LosHijos[cantidadParametros - 1].LosHijos.Add(nuevoNodo);
                        break;
                    }
                case "condiigual":
                    {
                        break;
                    }
                case "condinoigual":
                    {
                        break;
                    }
                case "condimenorque":
                    {
                        break;
                    }
                case "condimayorque":
                    {
                        break;
                    }
                case "condimenoigualque":
                    {
                        break;
                    }
                case "condimayoigualque":
                    {
                        break;
                    }
                case "expresuma":
                    {
                        numero1 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        numero2 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        resultado = numero1 + numero2;
                        Valores_Constantes.Add(resultado);
                        break;
                    }
                case "expreresta":
                    {
                        numero2 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        numero1 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        resultado = numero1 - numero2;
                        Valores_Constantes.Add(resultado);
                        break;
                    }
                case "expremulti":
                    {
                        numero2 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        numero1 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        resultado = numero2 * numero1;
                        Valores_Constantes.Add(resultado);
                        break;
                    }
                case "expredivi":
                    {
                        numero2 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        numero1 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        resultado = numero1 / numero2;
                        Valores_Constantes.Add(resultado);
                        break;
                    }
                case "expremod":
                    {
                        numero2 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        numero1 = Valores_Constantes.Last();
                        Valores_Constantes.RemoveAt(Valores_Constantes.Count - 1);
                        resultado = numero2 % numero1;
                        Valores_Constantes.Add(resultado);
                        break;
                    }
                case "charliterales":
                    {
                        char tempoC = Valores_En_Pila[Valores_De_Afuera.Count - 1][1];
                        Valores_Constantes.Add(tempoC);
                        break;
                    }
                case "numliterales":
                    {
                        Valores_Constantes.Add(int.Parse(Valores_En_Pila[Valores_De_Afuera.Count - 1]));
                        break;
                    }
            }
        }

        #endregion
    }
}