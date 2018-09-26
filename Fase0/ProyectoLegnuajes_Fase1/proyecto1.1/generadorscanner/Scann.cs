using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorScanner
{
    public class Scann
    {
        public Dictionary<int, Token> Tokens { get; private set; }
        private Dictionary<string, List<ParteConjunto>> Conjuntos;
        private ManejoArchivoSalida Salida;
        private Dictionary<int, Estado> Estados;

        public Scann(Dictionary<string, List<ParteConjunto>> conjuntos, String path, Dictionary<int, Estado> estados, Dictionary<int, Token> tokens)
        {
            Conjuntos = conjuntos;
            Tokens = tokens;
            Estados = estados;
            Salida = new ManejoArchivoSalida(path);
        }

        public void Start()
        {
            //Creación de la función TomaToken
            Salida.EscribirLinea("public int TomaToken()");
            Salida.EscribirLinea("{");
            Salida.EscribirLinea("\tint Estado = 1;");
            Salida.EscribirLinea("\tLexema = \"\";");
            Salida.EscribirLinea("\tSalir = false;");
            Salida.EscribirLinea("\tError = false;");
            Salida.EscribirLinea("\tint NumToken;");
            Salida.EscribirLinea("\twhile(!Salir && Token != 65535 && Token != -1)");
            Salida.EscribirLinea("\t{");

            Salida.EscribirLinea("\t\tswitch(Estado)");
            Salida.EscribirLinea("\t\t{");
            for (int i = 0; i < Estados.Count; i++)
            {
                if (Estados.ElementAt(i).Value.EstadosSiguientes.Count() != 0)
                {
                    StringEstado(Estados.ElementAt(i).Key);
                }
            }
            Salida.EscribirLinea("\t\t\tdefault: Salir = true; break;");
            Salida.EscribirLinea("\t\t}");

            Salida.EscribirLinea("\t}");
            Salida.EscribirLinea("\tswitch(Estado)");
            Salida.EscribirLinea("\t{");

            for (int i = 0; i < Estados.Count; i++)
            {
                if (Estados.ElementAt(i).Value.TokenAceptacion != null)
                {
                    Salida.EscribirLinea("\t\tcase " + Estados.ElementAt(i).Key + ": NumToken=" + Estados.ElementAt(i).Value.TokenAceptacion + "; break;");
                }
            }
            
            Salida.EscribirLinea("\t\tdefault: NumToken = -1; Error = true; break;");

            Salida.EscribirLinea("\t}");
            Salida.EscribirLinea("\tif(!Error)");
            Salida.EscribirLinea("\t{");

            Salida.EscribirLinea("\t\tif(Lexema!=\"\")");
            Salida.EscribirLinea("\t\t{");
            Salida.EscribirLinea("\t\t\tNumToken = BuscarAcciones(NumToken);");
            Salida.EscribirLinea("\t\t}");

            Salida.EscribirLinea("\t}");
            Salida.EscribirLinea("\treturn NumToken;");
            Salida.EscribirLinea("}");

            //Creación de la función Buscar Acciones
            Salida.EscribirLinea("public int  BuscarAcciones(int NumToken)");
            Salida.EscribirLinea("{");
            Salida.EscribirLinea("\tswitch(NumToken)");
            Salida.EscribirLinea("\t{");

            CaseTokenPalabra();

            Salida.EscribirLinea("\t}");
            Salida.EscribirLinea("\treturn NumToken;");
            Salida.EscribirLinea("}");

            Salida.CerrarArchivo();
        }

        private void CaseTokenPalabra()
        {
            for (int i = 0; i < Tokens.Count; i++)
            {
                if (Tokens.ElementAt(i).Value.PalabrasReservadas.Count > 0)
                {
                    Salida.EscribirLinea("\t\tcase " + Tokens.ElementAt(i).Key + ":");
                    Salida.EscribirLinea("\t\t{");

                    Salida.EscribirLinea("\t\t\tswitch(Lexema)");
                    Salida.EscribirLinea("\t\t\t{");

                    for (int j = 0; j < Tokens.ElementAt(i).Value.PalabrasReservadas.Count; j++)
                    {
                        Salida.EscribirLinea("\t\t\t\tcase \"" + Tokens.ElementAt(i).Value.PalabrasReservadas.ElementAt(j).Value + "\":");
                        Salida.EscribirLinea("\t\t\t\t{");
                        Salida.EscribirLinea("\t\t\t\t\tNumToken=" + Tokens.ElementAt(i).Value.PalabrasReservadas.ElementAt(j).Key+";");
                        Salida.EscribirLinea("\t\t\t\t}");
                        Salida.EscribirLinea("\t\t\t\tbreak;");
                    }

                    Salida.EscribirLinea("\t\t\t}");

                    Salida.EscribirLinea("\t\t}");
                    Salida.EscribirLinea("\t\tbreak;");
                }
            }
        }
        
        private void StringEstado(int estado)
        {
            Salida.EscribirLinea("\t\t\tcase " + estado.ToString() + ":");

            Salida.EscribirContiguo("\t\t\t\t");
            for (int i = 0; i < Estados[estado].EstadosSiguientes.Count(); i++)
            {
                Salida.EscribirContiguo("if(");
                //Se añaden los datos del conjunto que se utilizan
                if (Estados[estado].EstadosSiguientes.ElementAt(i).Key[0].Equals('\''))
                {
                    int x = Convert.ToInt32(Estados[estado].EstadosSiguientes.ElementAt(i).Key[1]);
                    Salida.EscribirContiguo("(Token=="+ Convert.ToInt32(Estados[estado].EstadosSiguientes.ElementAt(i).Key[1]).ToString() +")");
                }
                else
                {
                    string conjunto = Estados[estado].EstadosSiguientes.ElementAt(i).Key;
                    Salida.EscribirContiguo(Conjuntos[conjunto.ToLower()][0].ToString());

                    for (int j = 1; j < Conjuntos[conjunto.ToLower()].Count; j++)
                    {
                        Salida.EscribirContiguo("||");
                        Salida.EscribirContiguo(Conjuntos[conjunto.ToLower()][j].ToString());
                    }
                }
                Salida.EscribirLinea(")");

                Salida.EscribirLinea("\t\t\t\t{");

                Salida.EscribirLinea("\t\t\t\t\tEstado = " + Estados[estado].EstadosSiguientes.ElementAt(i).Value + ";");
                Salida.EscribirLinea("\t\t\t\t\tLexema+=((char)Token).ToString();");
                Salida.EscribirLinea("\t\t\t\t\tToken=ArchivoEntrada.LeerSiguiente();");
                
                Salida.EscribirLinea("\t\t\t\t}");
                Salida.EscribirContiguo("\t\t\t\telse ");
            }
            Salida.EscribirLinea("");
            Salida.EscribirLinea("\t\t\t\t{");

            if (Estados[estado].TokenAceptacion == null)
            {
                Salida.EscribirLinea("\t\t\t\t\tError = true;");
                Salida.EscribirLinea("\t\t\t\t\tLexema+=((char)Token).ToString();");
                Salida.EscribirLinea("\t\t\t\t\tToken=ArchivoEntrada.LeerSiguiente();");
            }
            
            Salida.EscribirLinea("\t\t\t\t\tSalir = true;");
            Salida.EscribirLinea("\t\t\t\t}");

            Salida.EscribirLinea("\t\t\tbreak;");
        }
    }
}
