using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scann
{
    public class Scann
    {
        private ManejoArchivoEntrada ArchivoEntrada;
        protected int Token;
        string Lexema = "";
        private bool Error;
        private bool Salir;

        public Scann(string path)
        {
            ArchivoEntrada = new ManejoArchivoEntrada(path);
        }

        public Queue<string> Start()
        {
            Queue<string> Resultado = new Queue<string>();
            Token = ArchivoEntrada.LeerSiguiente();
            EliminarEspacios();
            while (Token != 65535 && Token != -1)
            {
                int salida = TomaToken();
                if (Error)
                {
                    Resultado.Enqueue("Error -1: " + Lexema + " = "  + "Token no reconocido");
                }
                else
                {
                    Resultado.Enqueue(Lexema + " = "+" Token "+ salida);
                }
                EliminarEspacios();
            }
            ArchivoEntrada.Cerrar();
            return Resultado;
        }

        private void EliminarEspacios()
        {
            while (Token == 9 || Token == 10 || Token == 13 || Token == 26 || Token == 32)
            {
                Token = ArchivoEntrada.LeerSiguiente();
            }
        }
        public int TomaToken()
        {
            int Estado = 1;
            Lexema = "";
            Salir = false;
            Error = false;
            int NumToken;
            while (!Salir && Token != 65535 && Token != -1)
            {
                switch (Estado)
                {
                    case 1:
                        if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 2;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 34))
                        {
                            Estado = 3;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 39))
                        {
                            Estado = 4;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 95) || (Token >= 97 && Token <= 122) || (Token >= 65 && Token <= 90))
                        {
                            Estado = 5;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 61))
                        {
                            Estado = 6;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 60))
                        {
                            Estado = 7;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 62))
                        {
                            Estado = 8;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 58))
                        {
                            Estado = 9;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 43))
                        {
                            Estado = 10;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 45))
                        {
                            Estado = 11;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 111))
                        {
                            Estado = 12;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 42))
                        {
                            Estado = 13;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 97))
                        {
                            Estado = 14;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 110))
                        {
                            Estado = 15;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 100))
                        {
                            Estado = 16;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 109))
                        {
                            Estado = 17;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 59))
                        {
                            Estado = 18;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 46))
                        {
                            Estado = 19;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 123))
                        {
                            Estado = 20;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 125))
                        {
                            Estado = 21;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 40))
                        {
                            Estado = 22;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 41))
                        {
                            Estado = 23;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 91))
                        {
                            Estado = 24;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 93))
                        {
                            Estado = 25;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 44))
                        {
                            Estado = 26;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 2:
                        if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 2;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 3:
                        if ((Token >= 32 && Token <= 254))
                        {
                            Estado = 27;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 4:
                        if ((Token >= 32 && Token <= 254))
                        {
                            Estado = 28;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 5:
                        if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 5;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 95) || (Token >= 97 && Token <= 122) || (Token >= 65 && Token <= 90))
                        {
                            Estado = 5;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 7:
                        if ((Token == 61))
                        {
                            Estado = 29;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 62))
                        {
                            Estado = 30;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 8:
                        if ((Token == 61))
                        {
                            Estado = 31;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 9:
                        if ((Token == 61))
                        {
                            Estado = 32;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 12:
                        if ((Token == 114))
                        {
                            Estado = 33;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 14:
                        if ((Token == 110))
                        {
                            Estado = 34;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 15:
                        if ((Token == 111))
                        {
                            Estado = 35;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 16:
                        if ((Token == 105))
                        {
                            Estado = 36;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 17:
                        if ((Token == 111))
                        {
                            Estado = 37;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 19:
                        if ((Token == 46))
                        {
                            Estado = 38;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 27:
                        if ((Token == 34))
                        {
                            Estado = 39;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 28:
                        if ((Token == 39))
                        {
                            Estado = 39;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 34:
                        if ((Token == 100))
                        {
                            Estado = 40;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 35:
                        if ((Token == 116))
                        {
                            Estado = 41;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 36:
                        if ((Token == 118))
                        {
                            Estado = 42;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    case 37:
                        if ((Token == 100))
                        {
                            Estado = 43;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                            Salir = true;
                        }
                        break;
                    default: Salir = true; break;
                }
            }
            switch (Estado)
            {
                case 2: NumToken = 1; break;
                case 5: NumToken = 3; break;
                case 6: NumToken = 4; break;
                case 7: NumToken = 6; break;
                case 8: NumToken = 7; break;
                case 9: NumToken = 52; break;
                case 10: NumToken = 11; break;
                case 11: NumToken = 12; break;
                case 13: NumToken = 14; break;
                case 18: NumToken = 43; break;
                case 19: NumToken = 44; break;
                case 20: NumToken = 45; break;
                case 21: NumToken = 46; break;
                case 22: NumToken = 47; break;
                case 23: NumToken = 48; break;
                case 24: NumToken = 49; break;
                case 25: NumToken = 50; break;
                case 26: NumToken = 53; break;
                case 29: NumToken = 9; break;
                case 30: NumToken = 5; break;
                case 31: NumToken = 8; break;
                case 32: NumToken = 10; break;
                case 33: NumToken = 13; break;
                case 38: NumToken = 51; break;
                case 39: NumToken = 2; break;
                case 40: NumToken = 15; break;
                case 41: NumToken = 18; break;
                case 42: NumToken = 17; break;
                case 43: NumToken = 16; break;
                default: NumToken = -1; Error = true; break;
            }
            if (!Error)
            {
                if (Lexema != "")
                {
                    NumToken = BuscarAcciones(NumToken);
                }
            }
            return NumToken;
        }
        public int BuscarAcciones(int NumToken)
        {
            switch (NumToken)
            {
                case 3:
                    {
                        switch (Lexema)
                        {
                            case "program":
                                {
                                    NumToken = 19;
                                }
                                break;
                            case "include":
                                {
                                    NumToken = 20;
                                }
                                break;
                            case "const":
                                {
                                    NumToken = 21;
                                }
                                break;
                            case "type":
                                {
                                    NumToken = 22;
                                }
                                break;
                            case "var":
                                {
                                    NumToken = 23;
                                }
                                break;
                            case "record":
                                {
                                    NumToken = 24;
                                }
                                break;
                            case "array":
                                {
                                    NumToken = 25;
                                }
                                break;
                            case "of":
                                {
                                    NumToken = 26;
                                }
                                break;
                            case "procedure":
                                {
                                    NumToken = 27;
                                }
                                break;
                            case "function":
                                {
                                    NumToken = 28;
                                }
                                break;
                            case "if":
                                {
                                    NumToken = 29;
                                }
                                break;
                            case "then":
                                {
                                    NumToken = 30;
                                }
                                break;
                            case "else":
                                {
                                    NumToken = 31;
                                }
                                break;
                            case "for":
                                {
                                    NumToken = 32;
                                }
                                break;
                            case "to":
                                {
                                    NumToken = 33;
                                }
                                break;
                            case "while":
                                {
                                    NumToken = 34;
                                }
                                break;
                            case "do":
                                {
                                    NumToken = 35;
                                }
                                break;
                            case "exit":
                                {
                                    NumToken = 36;
                                }
                                break;
                            case "end":
                                {
                                    NumToken = 37;
                                }
                                break;
                            case "case":
                                {
                                    NumToken = 38;
                                }
                                break;
                            case "break":
                                {
                                    NumToken = 39;
                                }
                                break;
                            case "downto":
                                {
                                    NumToken = 40;
                                }
                                break;
                        }
                    }
                    break;
            }
            return NumToken;
        }


    }
}
