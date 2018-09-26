using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    class Tabla_Estados
    {
        public static List<ESTRUCTURA_LALR> Kernels_ = new List<ESTRUCTURA_LALR>();
        public static int Punto, Dolar;
        bool Ingreso = true;
        bool EsNucleo;
        int Posicion;

        //Listas Temporales para moverme dentro del programa
        List<List<int>> Produ_Tempo;
        List<List<int>> LA_Tempo;
        List<string> No_Terminales_Tempo;
        List<List<int>> New_Nucleo;
        List<List<int>> New_LA;
        List<string> New_No_Terminal;

        /// <summary>
        /// Constructor de la clase que inicializa los valores
        /// </summary>
        public Tabla_Estados()
        {
            Produ_Tempo = new List<List<int>>();
            LA_Tempo = new List<List<int>>();
            No_Terminales_Tempo = new List<string>();
            New_Nucleo = new List<List<int>>();
            New_LA = new List<List<int>>();
            New_LA = new List<List<int>>();
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
                                Agregar_First_LA(nuevo_Estado, nuevo_Estado.LA_Nuevo_Tempo.Count - 1, -elem_tempo);
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
                                        }
                                        else
                                        {
                                            Agregar_First_LA(nuevo_Estado, nuevo_Estado.LA_Nuevo_Tempo.Count - 1, -elem_tempo);
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
            for (int i = 0; i < nuevo_Estado.NT_Nuevo_Tempo.Count; i++)
            {
                int elemento = nuevo_Estado.Produ_Nueva_Tempo[i][1];
                if (elemento < 0) /*Es un no terminal*/
                {
                    Obtener_Produccion(Valor_NT(elemento), nuevo_Estado, nuevo_Estado.LA_Nuevo_Tempo[i]);
                }
            }
            //Verifica quein es el nuevo kernel
            for (int i = nuevo_Estado.NT_Nuevo_Tempo.Count - 1; i > -1; i--)
            {
                nuevo_Estado.NT_.Insert(0, nuevo_Estado.NT_Nuevo_Tempo[i]);
                nuevo_Estado.Produ.Insert(0, nuevo_Estado.Produ_Nueva_Tempo[i]);
                nuevo_Estado.LookA.Insert(0, nuevo_Estado.LA_Nuevo_Tempo[i]);
            }
            nuevo_Estado.Produ_Nueva_Tempo.Clear();
            nuevo_Estado.LA_Nuevo_Tempo.Clear();
            nuevo_Estado.NT_Nuevo_Tempo.Clear();
            //Agrega el estado a la lista de kernels
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
        /// Método que va a obtener las producciones generadas después del punto. 
        /// Es similar al de los estados solo que esta se hace de manera interna. 
        /// Ejemplo 
        /// <S> -> .<P>
        /// entonces lo que hace esto es analizar todas las producciones de <P>
        /// </summary>
        /// <param name="nt"></param>
        /// <param name="estruc"></param>
        /// <param name="lookAhead"></param>
        private void Obtener_Produccion(string nt, ESTRUCTURA_LALR estruc, List<int> lookAhead)
        {
            int nuevas_produ = 0;
            List<int> tempos = new List<int>();
            List<int> LA = new List<int>();
            for (int i = 0; i < Analizador.Lista_Producciones.Count; i++)
            {
                if(Analizador.Lista_Producciones[i].NoTerminal_PR == nt)
                {
                    List<int> P_tempo= new List<int>();
                    Gene_Agre_Produccion(P_tempo, Analizador.Lista_Producciones[i].Elementos);
                    P_tempo.Insert(0, Punto);
                    //Ahora bien debo de verificar si esa producción exite
                    if(Existe_Produccion(P_tempo,estruc, nt))
                    {
                        //Si la producción exiete entonces no se va a generar una nueva producción
                        Ingreso = false;
                        for (int j = 0; j < estruc.LookA.Count; j++)
                        {
                            if (estruc.NT_[j] == nt)
                            {
                                if (Agregar_Elementos_(estruc.LookA[j], lookAhead))
                                {
                                    //Debo de actualizar si hubo algún cambio en los look ahead
                                    Actualizar_Look_Ahead(estruc, j, nt, estruc.LookA[j]);
                                }
                            }
                        }
                        break;
                    }
                    Ingreso = true;
                    nuevas_produ++; //hay nuevas producciones
                    estruc.Produ.Add(new List<int>());
                    estruc.LookA.Add(new List<int>());
                    //Agrega el LA respectivo a la producción
                    Agregar_Elementos_(estruc.LookA[estruc.LookA.Count - 1], lookAhead);
                    estruc.Produ[estruc.Produ.Count - 1] = P_tempo;
                    estruc.NT_.Add(nt);
                }
            }
            int tempo_2 = estruc.NT_.Count - nuevas_produ;
            int tempo_3 = estruc.NT_.Count;

            for (int i = tempo_2; i < tempo_3; i++)
            {
                if(estruc.Produ[i].Count > 1)
                {
                    int elemento = estruc.Produ[i][1];

                    if(elemento < 0)
                    {
                        bool tempo_bool = false;
                        //Se inicia en dos por que ya se reviso la pos 0 y la 1
                        for (int j = 2; j < estruc.Produ[i].Count; j++)
                        {
                            int elemento_2 = estruc.Produ[i][j];
                            if(elemento_2 >  0)
                            {
                                if(!LA.Contains(elemento_2))
                                {
                                    LA.Add(elemento_2);
                                }
                                Obtener_Produccion(Valor_NT(elemento), estruc, LA);
                                tempo_bool = true;
                                break;
                            }
                            else
                            {
                                Agregar_Elementos_(LA, Analizador.Lista_NT[(-elemento_2) - 1].First_NT);
                                if(!Es_Nulo_Terminal(elemento_2))
                                {
                                    tempo_bool = true;
                                    Obtener_Produccion(Valor_NT(elemento), estruc, LA);
                                    break;
                                }
                            }
                        }
                        if(!tempo_bool)
                        {
                            Agregar_Elementos_(LA, estruc.LookA[i]);
                            Obtener_Produccion(Valor_NT(elemento), estruc, LA);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Método que realiza la modificación de los LA si fuera necesario
        /// </summary>
        /// <param name="estruc"></param>
        /// <param name="pos"></param>
        /// <param name="nt"></param>
        /// <param name="LA"></param>
        private void Actualizar_Look_Ahead(ESTRUCTURA_LALR estruc, int pos, string nt, List<int> LA)
        {
            if (estruc.Produ[pos].Count > 1)
            {
                int elem = estruc.Produ[pos][1];
                if(elem < 0)
                {
                    bool tempo = false;
                    //Inicia en 2 por que en el primer if es mayor a uno y luego en elemento se lee la pos 1
                    for (int i = 2; i < estruc.Produ[pos].Count; i++)
                    {
                        elem = estruc.Produ[pos][i];
                        if(elem > 0)
                        {
                            tempo = true;
                            break;
                        }
                        if(!Es_Nulo_Terminal(elem))
                        {
                            tempo = true;
                            break;
                        }
                    }
                    if(!tempo)
                    {
                        for (int j = 0; j < estruc.LookA.Count; j++)
                        {
                            if(estruc.NT_[j] == Valor_NT(elem))
                            {
                                if(Agregar_Elementos_(estruc.LookA[j], LA))
                                {
                                    //Se vuelve a llamar a actualizar ya que exiten más valores a actualizar
                                    Actualizar_Look_Ahead(estruc, j, nt, LA);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Función que verifica si agrego el elemento o no
        /// </summary>
        /// <param name="dest"></param>
        /// <param name="ori"></param>
        /// <returns></returns>
        private bool Agregar_Elementos_(List<int> dest, List<int> ori)
        {
            bool tempo = false;
            for (int i = 0; i < ori.Count; i++)
            {
                if(!dest.Contains(ori[i]))
                {
                    if(ori[i] != 0)
                    {
                        dest.Add(ori[i]);
                        tempo = true;
                    }
                }
            }
            return tempo;
        }

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
                    if(padre[i] == ingresante[i])
                    {
                        return true;
                    }
                }
                return false;
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
        private void Agregar_First_LA(ESTRUCTURA_LALR kernel, int la_temp, int no_terminal)
        {
            //Se va a recorrer la lista de no terminales ya que ahí se encuentra el first
            for (int i = 0; i < Analizador.Lista_NT.Count; i++)
            {
                //Se determina el valor del elemento a agregar
                int elemento = Analizador.Lista_NT[i].First_NT[i];

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
        /// Método que determina los kernels, producciones y estados. 
        /// Además determina los LA y Goto a utilizar.
        /// </summary>
        private void Determinar_Kernels()
        {

        }
    }
}
