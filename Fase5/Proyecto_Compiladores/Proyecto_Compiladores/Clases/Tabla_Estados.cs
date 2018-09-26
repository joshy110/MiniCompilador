using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    class Tabla_Estados
    {
        public static List<ESTRUCTURA_LALR> Kernels_;
        public static int Punto, Dolar;
        bool Ingreso = true;
        bool EsNucleo;
        int Posicion;

        //Listas Temporales para moverme dentro del programa
        List<List<int>> Produ_Tempo_;
        List<List<int>> LA_Tempo_;
        List<string> No_Terminales_Tempo;
        List<List<int>> New_Nucleo;
        List<List<int>> New_LA;
        List<string> New_No_Terminal;

        /// <summary>
        /// Constructor de la clase que inicializa los valores
        /// </summary>
        public Tabla_Estados()
        {
            Produ_Tempo_ = new List<List<int>>();
            LA_Tempo_ = new List<List<int>>();
            No_Terminales_Tempo = new List<string>();
            New_No_Terminal = new List<string>();
            New_Nucleo = new List<List<int>>();
            New_LA = new List<List<int>>();
            New_LA = new List<List<int>>();
            Kernels_ = new List<ESTRUCTURA_LALR>();
        }

        /// <summary>
        /// Métdo que inicializa el Goto y Look Ahead
        /// </summary>
        public void Inicio()
        {
            Estado_Inicial();
            Determinar_Kernels();
        }

        /// <summary>
        /// Método que caulcula el estado inicial.
        /// </summary>
        private void Estado_Inicial()
        {
            Colocar_Valor_Punto();
            List<int> LA_Interno = new List<int>();
            LA_Interno.Add(Dolar); //Se agrega el look ahead incial que es el dolar
            Determinar_Estados("<start>", LA_Interno); //Se manda el start ya que es el NT que manda todo
        }

        /// <summary>
        /// Método que determina el valor que el punto va a poseer. 
        /// Igual que el Dolar.
        /// </summary>
        private void Colocar_Valor_Punto()
        {
            //Se les agrega el valor +1 y +2 de las listas de tokens
            Punto = Analizador.Lista_Tokens.Count + 1;
            Dolar = Punto + 1;
        }

        /// <summary>
        /// Método que determina los estados y producciones que va teener c/estado
        /// </summary>
        /// <param name="NT"></param>
        /// <param name="LookAhead"></param>
        private void Determinar_Estados(string NT, List<int> LookAhead)
        {
            ESTRUCTURA_LALR nuevo_Estado = new ESTRUCTURA_LALR();
            nuevo_Estado.Numero = Kernels_.Count();
            /*Se van a recorrer las producciones para generar las nuevas, es decir,
             * se les va agregar el punto
             */
            foreach(var p in Analizador.Lista_Producciones)
            {
                //Se verifica que la producción sea la que se mandó por parámetro
                if (p.NoTerminal_PR == NT)
                {
                    //Lista temporal que va a manejar las producciones internas
                    List<int> P_Tempo = new List<int>();
                    nuevo_Estado.LA_Nuevo_Tempo.Add(new List<int>());
                    nuevo_Estado.Produ_Nueva_Tempo.Add(new List<int>());

                    /*Hacer un método que me genere o agrege las producciones al no terminal analizado.
                     Se agregan los elementos de lo que se esta recorriendo*/
                    Gene_Agre_Produccion(P_Tempo, p.Elementos);
                    //Ahora debo de agregar el punto en la posición 0
                    P_Tempo.Insert(0, Punto);
                    nuevo_Estado.Produ_Nueva_Tempo[nuevo_Estado.Produ_Nueva_Tempo.Count - 1] = P_Tempo;
                    //Se agrega el no terminal esperado
                    nuevo_Estado.NT_Nuevo_Tempo.Add(NT);
                    //Se agrega el LookAhead
                    nuevo_Estado.LA_Nuevo_Tempo[nuevo_Estado.LA_Nuevo_Tempo.Count - 1] = LookAhead;
                    int elemento = p.Elementos[0];
                    if(elemento < 0)
                    {
                        if(p.Elementos.Count > 1)
                        {
                            int elem_tempo = p.Elementos[1];
                            //Esto quiere decir que es un terminal entonces solo se agrega y listo
                            if(elem_tempo > 0)
                            {
                                nuevo_Estado.LA_Nuevo_Tempo[nuevo_Estado.LA_Nuevo_Tempo.Count - 1].Add(elem_tempo);
                            }
                            else //es un no terminal entonces se debe de procesar 
                            {
                                //Primero se agrega el look ahead que posean first
                                Agregar_First_LA_Nucle(nuevo_Estado, nuevo_Estado.LA_Nuevo_Tempo.Count - 1, -elem_tempo);
                                /* Luego de agregar el first se va a verificar si es nullo el elemenot ya que si es así
                                 * se va a verificar el siguiente elemento en la producción para ver si es o no nulo 
                                 */
                                 if(Es_Nulo_Terminal(elem_tempo))
                                {
                                    //Se inicia en dos por que ya se analizaron las posciones 1 y 2
                                    for (int j = 2; j < p.Elementos.Count; j++)
                                    {
                                        elem_tempo = p.Elementos[j]; // Se agrega un nuevo valor al temporal
                                        if(elem_tempo > 0) // este if es el mismo solo que se uso otro for para seguir verificando
                                        {
                                            //agrega el elemenot terminal
                                            nuevo_Estado.LA_Nuevo_Tempo[nuevo_Estado.LA_Nuevo_Tempo.Count - 1].Add(elem_tempo);
                                            break;
                                        }
                                        else
                                        {
                                            Agregar_First_LA_Nucle(nuevo_Estado, nuevo_Estado.LA_Nuevo_Tempo.Count - 1, -elem_tempo);
                                            if(!Es_Nulo_Terminal(elem_tempo))
                                            {
                                                break;
                                            }
                                        }

                                    }
                                }


                            }
                        }
                    }

                }
            }
            Ampliar_Ver_Estados(nuevo_Estado);
            //Se agregan a la listas Reales los kernesl
            for (int i = nuevo_Estado.NT_Nuevo_Tempo.Count - 1; i > -1; i--)
            {
                nuevo_Estado.NT_.Insert(0, nuevo_Estado.NT_Nuevo_Tempo[i]);
                nuevo_Estado.Produ.Insert(0, nuevo_Estado.Produ_Nueva_Tempo[i]);
                nuevo_Estado.LookA.Insert(0, nuevo_Estado.LA_Nuevo_Tempo[i]);
            }
            nuevo_Estado.Produ_Nueva_Tempo.Clear();
            nuevo_Estado.LA_Nuevo_Tempo.Clear();
            nuevo_Estado.NT_Nuevo_Tempo.Clear();
            Kernels_.Add(nuevo_Estado);
        }

        /// <summary>
        /// Método que sirve para agregar las producciones
        /// </summary>
        private void Gene_Agre_Produccion(List<int> destino, List<int> fuente)
        {
            for (int i = 0; i < fuente.Count; i++)
            {
                //Quiere decir que hay algo ahí para agregar
                if(fuente[i] != 0)
                {
                    destino.Add(fuente[i]);
                }
            }
        }

        /// <summary>
        /// Amplia los estados existentens para verificar que no se haya escapado ninguno
        /// </summary>
        /// <param name="nuevo"></param>
        private void Ampliar_Ver_Estados(ESTRUCTURA_LALR nuevo)
        {
            //Se recorren los núcleos
            for (int i = 0; i < nuevo.LA_Nuevo_Tempo.Count; i++)
            {
                int pos_Punto = nuevo.Produ_Nueva_Tempo[i].IndexOf(Punto);
                if(pos_Punto + 1 < nuevo.Produ_Nueva_Tempo[i].Count)
                {
                    int elemento = nuevo.Produ_Nueva_Tempo[i][pos_Punto + 1];
                    if(elemento < 0)
                    {
                        bool produ = false;
                        List<int> Padres_Look_Ahead = new List<int>();
                        for (int j = pos_Punto + 2; j < nuevo.Produ_Nueva_Tempo[i].Count; j++)
                        {
                            int elemento_2 = nuevo.Produ_Nueva_Tempo[i][j];
                            if(elemento_2 > 0)
                            {
                                produ = true;
                                Padres_Look_Ahead.Add(elemento_2);
                                break;
                            }
                            else
                            {
                                Agregar_Elementos_LA(Padres_Look_Ahead, Analizador.Lista_NT[(-elemento_2) - 1].First_NT);
                                if(!Es_Nulo_Terminal(elemento_2))
                                {
                                    produ = true;
                                    break;
                                }
                            }
                        }
                        if(!produ)
                        {
                            Agregar_Elementos_LA(Padres_Look_Ahead, nuevo.LA_Nuevo_Tempo[i]);
                        }
                        Agregando_Producciones_2_0(Valor_NT(elemento), nuevo, Padres_Look_Ahead);

                    }
                }
            }
            for (int i = 0; i < nuevo.NT_.Count; i++)
            {
                if(nuevo.Produ[i].Count > 1)
                {
                    int elemento = nuevo.Produ[i][1];
                    if(elemento < 0)
                    {
                        bool produccion = false;
                        List<int> LA_Padres = new List<int>();
                        for (int j = 2; j < nuevo.Produ[i].Count; j++)
                        {
                            int elemento_2 = nuevo.Produ[i][j];
                            if(elemento_2 > 0)
                            {
                                produccion = true;
                                LA_Padres.Add(elemento_2);
                                break;
                            }
                            else
                            {
                                Agregar_Elementos_LA(LA_Padres, Analizador.Lista_NT[(-elemento_2) - 1].First_NT);
                                if(!Es_Nulo_Terminal(elemento_2))
                                {
                                    produccion = true;
                                    break;
                                }
                            }
                        }
                        //Se manda el Look ahead al padre
                        if(!produccion)
                        {
                            Agregar_Elementos_LA(LA_Padres, nuevo.LookA[i]);
                        }
                        //Se generan las prodccuones de los nucles dle estado
                        Agregando_Producciones_2_0(Valor_NT(elemento), nuevo, LA_Padres);
                    }
                }
            }
        }

        /// <summary>
        /// Método que vuelve agregar las producciones que quedaron sin agregarse
        /// </summary>
        /// <param name="nt"></param>
        /// <param name="nuevo"></param>
        /// <param name="LA_P"></param>
        private void Agregando_Producciones_2_0(string nt, ESTRUCTURA_LALR nuevo, List<int> LA_P)
        {
            for (int i = 0; i < Analizador.Lista_Producciones.Count; i++)
            {
                if(nt == Analizador.Lista_Producciones[i].NoTerminal_PR)
                {
                    List<int> new_Produ = new List<int>();
                    Gene_Agre_Produccion(new_Produ, Analizador.Lista_Producciones[i].Elementos);
                    new_Produ.Insert(0, Punto);
                    //Se verifica si la producción ya exite
                    if(Existe_Produccion(new_Produ, nuevo, nt))
                    {
                        for (int j = 0; j < nuevo.LookA.Count; j++)
                        {
                            if(nuevo.NT_[j] == nt)
                            {
                                if(Agregar_Elementos_LA(nuevo.LookA[j], LA_P))
                                {
                                    Actualizar_LA_Producciones(nuevo, j, nt, LA_P);
                                }
                            }
                        }
                    }
                    else //Se garega el Look Ahead
                    {
                        nuevo.LookA.Add(new List<int>());
                        Agregar_Elementos_LA(nuevo.LookA[nuevo.LookA.Count - 1], LA_P);
                        nuevo.Produ.Add(new List<int>());
                        Gene_Agre_Produccion(nuevo.Produ[nuevo.Produ.Count - 1], new_Produ);
                        nuevo.NT_.Add(nt);
                    }
                }
            }
        }

        ///// <summary>
        ///// Método que va a obtener las producciones generadas después del punto. 
        ///// Es similar al de los estados solo que esta se hace de manera interna. 
        ///// Ejemplo 
        ///// <S> -> .<P>
        ///// entonces lo que hace esto es analizar todas las producciones de <P>
        ///// </summary>
        ///// <param name="nt"></param>
        ///// <param name="estruc"></param>
        ///// <param name="lookAhead"></param>
        //private void Obtener_Produccion(string nt, ESTRUCTURA_LALR estruc, List<int> lookAhead)
        //{
        //    int nuevas_produ = 0;
        //    List<int> tempos = new List<int>();
        //    List<int> LA_tempos = new List<int>();
        //    for (int i = 0; i < Analizador.Lista_Producciones.Count; i++)
        //    {
        //        if(Analizador.Lista_Producciones[i].NoTerminal_PR == nt)
        //        {
        //            List<int> P_tempo= new List<int>();
        //            Gene_Agre_Produccion(P_tempo, Analizador.Lista_Producciones[i].Elementos);
        //            P_tempo.Insert(0, Punto);
        //            //Ahora bien debo de verificar si esa producción exite
        //            if(Existe_Produccion(P_tempo,estruc, nt))
        //            {
        //                Ingreso = false;
        //                for (int j = 0; j < estruc.LookA.Count; j++)
        //                {
        //                    if(estruc.NT_[j] == nt)
        //                    {
        //                        if (Agregar_Elementos_LA(estruc.LookA[j], lookAhead))
        //                        {
        //                            Actualizar_LA_Producciones(estruc, j, nt, estruc.LookA[j]);
        //                        }
        //                    }
        //                }
        //                break;
        //            }
        //            Ingreso = true;
        //            nuevas_produ++;
        //            estruc.Produ.Add(new List<int>());
        //            estruc.LookA.Add(new List<int>());
        //            Agregar_Elementos_LA(estruc.LookA[estruc.LookA.Count - 1], lookAhead);
        //            estruc.Produ[estruc.Produ.Count - 1] = P_tempo;
        //            estruc.NT_.Add(nt);
        //        }
        //    }
        //    int num = estruc.NT_.Count - nuevas_produ;
        //    int tempo_f = estruc.NT_.Count;
        //    for (int i = num; i < tempo_f; i++)
        //    {
        //        if (estruc.Produ[i].Count > 1)
        //        {
        //            int elemento = estruc.Produ[i][1];
        //            if(elemento < 0)
        //            {
        //                bool tempo_bool = false;
        //                for (int j = 2; j < estruc.Produ[i].Count; j++)
        //                {
        //                    int tempo_2 = estruc.Produ[i][j];
        //                    if(tempo_2 > 0)
        //                    {
        //                        if(!LA_tempos.Contains(tempo_2))
        //                        {
        //                            LA_tempos.Add(tempo_2);
        //                        }
        //                        Obtener_Produccion(Valor_NT(elemento), estruc, LA_tempos);
        //                        tempo_bool = true;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        Agregar_Elementos_LA(LA_tempos, Analizador.Lista_NT[((-tempo_2) - 1)].First_NT);
        //                        if(!Es_Nulo_Terminal(tempo_2))
        //                        {
        //                            tempo_bool = true;
        //                            Obtener_Produccion(Valor_NT(elemento), estruc, LA_tempos);
        //                        }
        //                    }
        //                }
        //                if(!tempo_bool)
        //                {
        //                    Agregar_Elementos_LA(LA_tempos, estruc.LookA[i]);
        //                    Obtener_Produccion(Valor_NT(elemento), estruc, LA_tempos);
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Se va ir a verificar en la lista de temporales de la estructura para ver si ya existe
        /// la producción que se desea agregar
        /// </summary>
        /// <param name="p"></param>
        /// <param name="estruc"></param>
        /// <param name="nt"></param>
        /// <returns></returns>
        private bool Existe_Produccion(List<int> p, ESTRUCTURA_LALR estruc, string nt)
        {
            EsNucleo = false;
            //Se busca en la temporal
            for (int i = 0; i < estruc.Produ_Nueva_Tempo.Count; i++)
            {
                //Se verifica si el no terminal con sus producciones ya existe
                if(p.Count == estruc.Produ_Nueva_Tempo[i].Count && estruc.NT_Nuevo_Tempo[i] == nt)
                {
                    //necesitare otro método que me valide las listas de ambas producciones para verifcar si son iguales
                    if(Existe_Estado(p,estruc.Produ_Nueva_Tempo[i]))
                    {
                        return true;
                    }
                }
            }
            //Se busca en la real
            for (int i = 0; i < estruc.Produ.Count; i++)
            {
                if (p.Count == estruc.Produ[i].Count && estruc.NT_[i] == nt)
                {
                    if (Existe_Estado(p, estruc.Produ[i]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Funciín que valida si las producciones, sus listas, ya existen
        /// </summary>
        /// <param name="padre"></param>
        /// <param name="ingresante"></param>
        /// <returns></returns>
        private bool Existe_Estado(List<int> padre, List<int> ingresante)
        {
            //Si contienen la misma longitud entonces se va a verificar que cada uno de sus contenidos no sean iguales
            if(padre.Count != ingresante.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < padre.Count; i++)
                {
                    if(padre[i] != ingresante[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Funciín que obtiene el valor no terminal
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private string Valor_NT(int num)
        {
            return Analizador.Lista_NT[(-num) - 1].No_Terminal_NT;
        }

        /// <summary>
        /// Verifica si el terminal ingresado es nulo o no
        /// </summary>
        /// <param name="valor"></param>
        private bool Es_Nulo_Terminal(int valor)
        {
            if(Analizador.Lista_NT[(-valor) - 1].First_NT.Contains(0))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Agrega el first al look ahead respectivo
        /// </summary>
        /// <param name="kernel"></param>
        /// <param name="la_temp"></param>
        /// <param name="no_terminal"></param>
        private void Agregar_First_LA_Nucle(ESTRUCTURA_LALR kernel, int la_temp, int no_terminal)
        {
            //Se va a recorrer la lista de no terminales ya que ahí se encuentra el first
            for (int i = 0; i < Analizador.Lista_NT[no_terminal].First_NT.Count; i++)
            {
                //Se determina el valor del elemento a agregar
                int elemento = Analizador.Lista_NT[no_terminal].First_NT[i];

                //Se verifica si ese elemento esta en la lista temporal
                if(!kernel.LA_Nuevo_Tempo[la_temp].Contains(elemento))
                {
                    //Se verifica que no sea nulo
                    if( elemento != 0)
                    {
                        kernel.LA_Nuevo_Tempo[la_temp].Add(elemento);
                    }
                }
            }
        }

        /// <summary>
        /// Función que agrega los elementos al LA respectivo
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="origen"></param>
        /// <returns></returns>
        private bool Agregar_Elementos_LA(List<int> dest, List<int> origen)
        {
            bool tempo = false;
            for (int i = 0; i < origen.Count; i++)
            {
                if(!dest.Contains(origen[i]))
                {
                    if(origen[i] != 0)
                    {
                        dest.Add(origen[i]);
                        tempo = true;
                    }
                }
            }
            return tempo;
        }

        /// <summary>
        /// Método que actualiza el LA de las producciones que se agregaron si fuese necesario
        /// </summary>
        /// <param name="estrcu"></param>
        /// <param name="pos"></param>
        /// <param name="nt"></param>
        /// <param name="LookAhead"></param>
        private void Actualizar_LA_Producciones(ESTRUCTURA_LALR estrcu, int pos, string nt, List<int> LookAhead)
        {
            if(estrcu.Produ[pos].Count > 1)
            {
                int elemento = estrcu.Produ[pos][1];
                if(elemento < 0)
                {
                    bool tempo = false;
                    for (int i = 2; i < estrcu.Produ[pos].Count; i++)
                    {
                        elemento = estrcu.Produ[pos][i];
                        if(elemento > 0)
                        {
                            tempo = true;
                            break;
                        }
                        if(!Es_Nulo_Terminal(elemento))
                        {
                            tempo = true;
                            break;
                        }
                    }
                    if(!tempo)
                    {
                        for (int j = 0; j < estrcu.LookA.Count; j++)
                        {
                            if(estrcu.NT_[j] == Valor_NT(elemento))
                            {
                                if(Agregar_Elementos_LA(estrcu.LookA[j],LookAhead))
                                {
                                    Actualizar_LA_Producciones(estrcu, j, nt, LookAhead);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método que determina los kernels, producciones y estados. 
        /// Además determina los LA y Goto a utilizar.
        /// </summary>
        private void Determinar_Kernels()
        {
            for (int i = 0; i < Kernels_.Count; i++)
            {
                Enlazar_Listas(i);
                //Se empieza a recorrer ya con todas las producciones necesarias
                for (int j = 0; j < Produ_Tempo_.Count; j++)
                {
                    //Se establece la posción del punto
                    int Localizar_Punto = Produ_Tempo_[j].IndexOf(Punto);
                    if(Localizar_Punto + 1 < Produ_Tempo_[j].Count)
                    {
                        int elemento = Produ_Tempo_[j][Localizar_Punto + 1];
                        New_LA.Clear();
                        New_Nucleo.Clear();
                        New_No_Terminal.Clear();
                        Jalar_Nucle(elemento, j);
                        //Se va a verificar si el kernel creado ya existe
                        if(!Estado_Existente(New_Nucleo, New_No_Terminal))
                        {
                            //LA lista de kernels tiene los valores a comparar entonces se puede volver a inicializar
                            ESTRUCTURA_LALR nuevo_E = new ESTRUCTURA_LALR();
                            nuevo_E.Numero = Kernels_.Count();
                            Agregar_Listas_A_Listas(nuevo_E.LA_Nuevo_Tempo, New_LA);
                            Agregar_Listas_A_Listas(nuevo_E.Produ_Nueva_Tempo, New_Nucleo);
                            //Inicia la creación del Goto
                            nuevo_E.N_Estado.Add(i);
                            if(elemento < 0)
                            {
                                nuevo_E.GoTo.Add(Valor_NT(elemento));
                            }
                            else
                            {
                                nuevo_E.GoTo.Add(Analizador.Lista_Tokens[elemento - 1].Simbolo_Token);
                            }
                            //Agrega los nt a la lista temporal
                            for (int q = 0; q <New_No_Terminal.Count ; q++)
                            {
                                nuevo_E.NT_Nuevo_Tempo.Add(New_No_Terminal[q]);
                            }
                            Ampliar_Ver_Estados(nuevo_E);
                            Kernels_.Add(nuevo_E);
                        }
                        else //Ya exites
                        {
                            Kernels_[Posicion].N_Estado.Add(i);
                            if(elemento < 0)
                            {
                                Kernels_[Posicion].GoTo.Add(Valor_NT(elemento));
                            }
                            else
                            {
                                Kernels_[Posicion].GoTo.Add(Analizador.Lista_Tokens[elemento - 1].Simbolo_Token);
                            }
                            //Se actualiza el LA
                            Modificar_LA(Posicion, New_LA, New_No_Terminal, New_Nucleo);
                        }
                        j = -1;
                    }
                    else
                    {
                        Produ_Tempo_.Remove(Produ_Tempo_[j]);
                        No_Terminales_Tempo.Remove(No_Terminales_Tempo[j]);
                        LA_Tempo_.Remove(LA_Tempo_[j]);
                        j = -1;
                    }
                }
            }
        }

        /// <summary>
        /// Método que actualiza los LA
        /// </summary>
        /// <param name="e"></param>
        /// <param name="La_t"></param>
        /// <param name="nt"></param>
        /// <param name="nucleos"></param>
        private void Modificar_LA(int e, List<List<int>> La_t, List<string> nt, List<List<int>> nucleos)
        {
            List<List<bool>> Generar_Cambios = new List<List<bool>>();
            bool hay_cambio = false;
            Hacer_Cambios(e, Generar_Cambios);
            for (int i = 0; i < Kernels_[e].Produ_Nueva_Tempo.Count; i++)
            {
                for (int j = 0; j < nt.Count; j++)
                {
                    if(Kernels_[e].NT_Nuevo_Tempo[i] == nt[j] && Existe_Nucleo(Kernels_[e].Produ_Nueva_Tempo[i], nucleos[j]))
                    {
                        if(Agregar_Elementos_LA(Kernels_[e].LA_Nuevo_Tempo[i], La_t[j]))
                        {
                            Generar_Cambios[0][i] = true;
                            hay_cambio = true;
                            Actualizar_Produccion_Look_Ahead(e, i, Generar_Cambios);

                        }
                    }
                }
            }
            if(hay_cambio)
            {
                Actualizar_Cambios_Estados(e, Generar_Cambios);
            }

        }

        /// <summary>
        /// Método que realiza las validaciones requeridas para hacer los cambios
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cambio"></param>
        private void Hacer_Cambios(int e, List<List<bool>> cambio)
        {
            cambio.Clear();
            cambio.Add(new List<bool>());
            cambio.Add(new List<bool>());
            for (int i = 0; i < Kernels_[e].Produ_Nueva_Tempo.Count; i++)
            {
                cambio[0].Add(false);
            }
            for (int i = 0; i < Kernels_[e].Produ.Count; i++)
            {
                cambio[1].Add(false);
            }
        }

        /// <summary>
        /// Función que valida si el núcle que se trata de ingresar ya existe
        /// </summary>
        /// <param name="agregandos"></param>
        /// <param name="entrantes"></param>
        /// <returns></returns>
        private bool Existe_Nucleo(List<int> agregandos, List<int> entrantes)
        {
            List<int> tempo1 = new List<int>();
            List<int> tempo2 = new List<int>();
            for (int i = 0; i < agregandos.Count; i++)
            {
                if(agregandos[i] != Punto)
                {
                    tempo1.Add(agregandos[i]);
                }
            }
            for (int i = 0; i < entrantes.Count; i++)
            {
                if (entrantes[i] != Punto)
                {
                    tempo2.Add(entrantes[i]);
                }
            }
            if(Existe_Estado(tempo1, tempo2))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Método que actualiza las producciones cuando ya se estan generando los kernels
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pos_n"></param>
        /// <param name="cambios"></param>
        private void Actualizar_Produccion_Look_Ahead(int e, int pos_n, List<List<bool>> cambios)
        {
            int Punto_pos = Kernels_[e].Produ_Nueva_Tempo[pos_n].IndexOf(Punto);
            if(Punto_pos + 1 < Kernels_[e].Produ_Nueva_Tempo[pos_n].Count)
            {
                int elemento = Kernels_[e].Produ_Nueva_Tempo[pos_n][Punto_pos + 1];
                if(elemento < 0)
                {
                    bool produ = false;
                    List<int> temporal = new List<int>();
                    for (int i = Punto_pos + 2; i < Kernels_[e].Produ_Nueva_Tempo[pos_n].Count; i++)
                    {
                        int elemnto_2 = Kernels_[e].Produ_Nueva_Tempo[pos_n][i];
                        if(elemnto_2 > 0)
                        {
                            if(!temporal.Contains(elemnto_2))
                            {
                                temporal.Add(elemnto_2);
                            }
                            produ = true;
                            break;
                        }
                        else
                        {
                            Agregar_Elementos_LA(temporal, Analizador.Lista_NT[(-elemnto_2) - 1].First_NT);
                            if(!Es_Nulo_Terminal(elemnto_2))
                            {
                                produ = true;
                                break;
                            }
                        }
                    }
                    if(!produ)
                    {
                        Agregar_Elementos_LA(temporal, Kernels_[e].LA_Nuevo_Tempo[pos_n]);
                        for (int j = 0; j < Kernels_[e].LookA.Count; j++)
                        {
                            if(Kernels_[e].NT_[j] == Valor_NT(elemento))
                            {
                                if(Agregar_Elementos_LA(Kernels_[e].LookA[j], Kernels_[e].LA_Nuevo_Tempo[pos_n]))
                                {
                                    cambios[1][j] = true;
                                    Actualizar_LA_Producciones(Kernels_[e], j, Kernels_[e].NT_[pos_n], temporal);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método que actualiza los estados si fuese necesario
        /// </summary>
        /// <param name="e"></param>
        /// <param name="cambios"></param>
        private void Actualizar_Cambios_Estados(int e, List<List<bool>> cambios)
        {
            for (int i = 0; i < cambios[0].Count; i++)
            {
                //Se va a verificar si se dio un cambio
                if(cambios[0][i])
                {
                    int t = Kernels_[e].Produ_Nueva_Tempo[i].IndexOf(Punto) + 1;
                    if(t < Kernels_[e].Produ_Nueva_Tempo[i].Count)
                    {
                        int t_2 = Kernels_[e].Produ_Nueva_Tempo[i][t];
                        int el_que_sigue;
                        if(t_2 < 0)
                        {
                            el_que_sigue = Buscar_Estados(Valor_NT(t_2), e);
                        }
                        else
                        {
                            el_que_sigue = Buscar_Estados((Analizador.Lista_Tokens[t_2 - 1].Simbolo_Token), e);
                        }
                        if(el_que_sigue != -1)
                        {
                            Modificar_LA(el_que_sigue, Kernels_[e].LA_Nuevo_Tempo, Kernels_[e].NT_Nuevo_Tempo, Kernels_[e].Produ_Nueva_Tempo);
                        }
                    }
                }
            }
            for (int i = 0; i < cambios[1].Count; i++)
            {
                //Se va a verificar si se dio un cambio
                if (cambios[1][i])
                {
                    int t = Kernels_[e].Produ[i].IndexOf(Punto) + 1;
                    if (t < Kernels_[e].Produ[i].Count)
                    {
                        int t_2 = Kernels_[e].Produ[i][t];
                        int el_que_sigue;
                        if (t_2 < 0)
                        {
                            el_que_sigue = Buscar_Estados(Valor_NT(t_2), e);
                        }
                        else
                        {
                            el_que_sigue = Buscar_Estados((Analizador.Lista_Tokens[t_2 - 1].Simbolo_Token), e);
                        }
                        if (el_que_sigue != -1)
                        {
                            Modificar_LA(el_que_sigue, Kernels_[e].LookA, Kernels_[e].NT_, Kernels_[e].Produ);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Función que relaiza la busqueda del estado que sigue
        /// </summary>
        /// <param name="nt"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private int Buscar_Estados(string nt, int e)
        {
            for (int i = 0; i < Kernels_.Count; i++)
            {
                for (int j = 0; j < Kernels_[i].GoTo.Count; j++)
                {
                    if(Kernels_[i].GoTo[j] == nt)
                    {
                        if(Kernels_[i].N_Estado[j] == e)
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Método que junta las listas a modo de generar un nuevo estado
        /// </summary>
        /// <param name="estado_analizar"></param>
        private void Enlazar_Listas(int estado_analizar)
        {
            Produ_Tempo_.Clear();
            LA_Tempo_.Clear();
            No_Terminales_Tempo.Clear();
            //Primero se agregan las temporales
            for (int i = 0; i < Kernels_[estado_analizar].Produ_Nueva_Tempo.Count; i++)
            {
                Produ_Tempo_.Add(Kernels_[estado_analizar].Produ_Nueva_Tempo[i]);
                LA_Tempo_.Add(Kernels_[estado_analizar].LA_Nuevo_Tempo[i]);
                No_Terminales_Tempo.Add(Kernels_[estado_analizar].NT_Nuevo_Tempo[i]);
            }
            //Luego se agregan las reales
            for (int i = 0; i < Kernels_[estado_analizar].Produ.Count; i++)
            {
                Produ_Tempo_.Add(Kernels_[estado_analizar].Produ[i]);
                LA_Tempo_.Add(Kernels_[estado_analizar].LookA[i]);
                No_Terminales_Tempo.Add(Kernels_[estado_analizar].NT_[i]);
            }
        }

        /// <summary>
        /// Método que obtiene el núcleo para obtener los cambios y seguir obteniendo 
        /// producciones o valores 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="pos"></param>
        private void Jalar_Nucle(int e, int pos)
        {
            for (int i = 0; i < Produ_Tempo_.Count; i++)
            {
                int produ = Produ_Tempo_[i].IndexOf(Punto);
                if(produ + 1 < Produ_Tempo_[i].Count)
                {
                    if (Produ_Tempo_[i][produ + 1] == e)
                    {
                        New_Nucleo.Add(new List<int>());
                        Gene_Agre_Produccion(New_Nucleo[New_Nucleo.Count - 1], Produ_Tempo_[i]);
                        New_Nucleo[New_Nucleo.Count - 1][produ] = New_Nucleo[New_Nucleo.Count - 1][produ + 1];
                        New_Nucleo[New_Nucleo.Count - 1][produ + 1] = Punto;
                        Produ_Tempo_.Remove(Produ_Tempo_[i]); //Ya se termino de analizar esa producción
                        New_LA.Add(new List<int>());
                        Agregar_Elementos_LA(New_LA[New_LA.Count - 1], LA_Tempo_[i]);
                        LA_Tempo_.Remove(LA_Tempo_[i]); //Se remueve el La analizado
                        New_No_Terminal.Add(No_Terminales_Tempo[i]);
                        No_Terminales_Tempo.Remove(No_Terminales_Tempo[i]); //Se remueve el nt analizado
                        i = -1;
                    }
                }
            }
        }

        /// <summary>
        /// Función que determina si el estado que se esta tratando de ingresar ya existe
        /// </summary>
        /// <returns></returns>
        private bool Estado_Existente(List<List<int>> nucleo, List<string> nts)
        {
            for (int i = 0; i < Kernels_.Count; i++)
            {
                if(nucleo.Count == Kernels_[i].NT_Nuevo_Tempo.Count)
                {
                    List<bool> Found1 = new List<bool>();
                    List<bool> Found2 = new List<bool>();
                    for (int k = 0; k < nucleo.Count; k++)
                    {
                        Found1.Add(false);
                        Found2.Add(false);
                    }
                    for (int j = 0; j < nucleo.Count; j++)
                    {
                        for (int k = 0; k < Kernels_[i].NT_Nuevo_Tempo.Count; k++)
                        {
                            if(nts[j] == Kernels_[i].NT_Nuevo_Tempo[k] && Existe_Estado(nucleo[j], Kernels_[i].Produ_Nueva_Tempo[k]) && !Found1[j] && !Found2[k])
                            {
                                Found1[j] = Found2[k] = true;
                            }
                        }
                    }
                    if(!Found1.Contains(false))
                    {
                        Posicion = i;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Método que agrega las listas que ya se tenian a sus propias listas. 
        /// Agrega a la lista temporal el LA
        /// Agrega a la lista tempoiral la nueva produ
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="ori"></param>
        private void Agregar_Listas_A_Listas(List<List<int>> dest, List<List<int>> ori)
        {
            for (int i = 0; i < ori.Count; i++)
            {
                dest.Add(new List<int>());
                for (int j = 0; j < ori[i].Count; j++)
                {
                    dest[i].Add(ori[i][j]);
                }
            }
        }
    }
}
