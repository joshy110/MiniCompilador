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
                        if ((Token == 95) || (Token >= 97 && Token <= 122) || (Token >= 65 && Token <= 90))
                        {
                            Estado = 2;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token >= 48 && Token <= 57))
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
                        else if ((Token == 43))
                        {
                            Estado = 5;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 45))
                        {
                            Estado = 6;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 38))
                        {
                            Estado = 7;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 124))
                        {
                            Estado = 8;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 62))
                        {
                            Estado = 9;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 61))
                        {
                            Estado = 10;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 60))
                        {
                            Estado = 11;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 33))
                        {
                            Estado = 12;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 47))
                        {
                            Estado = 13;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 42))
                        {
                            Estado = 14;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 37))
                        {
                            Estado = 15;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 46))
                        {
                            Estado = 16;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 44))
                        {
                            Estado = 17;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 123))
                        {
                            Estado = 18;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 125))
                        {
                            Estado = 19;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 40))
                        {
                            Estado = 20;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 41))
                        {
                            Estado = 21;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 59))
                        {
                            Estado = 22;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 58))
                        {
                            Estado = 23;
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
                        if ((Token == 95) || (Token >= 97 && Token <= 122) || (Token >= 65 && Token <= 90))
                        {
                            Estado = 2;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token >= 48 && Token <= 57))
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
                        if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 3;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 4:
                        if ((Token == 95) || (Token >= 97 && Token <= 122) || (Token >= 65 && Token <= 90))
                        {
                            Estado = 24;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 25;
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
                        if ((Token == 43))
                        {
                            Estado = 26;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 6:
                        if ((Token == 45))
                        {
                            Estado = 27;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 7:
                        if ((Token == 38))
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
                    case 8:
                        if ((Token == 124))
                        {
                            Estado = 29;
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
                    case 9:
                        if ((Token == 61))
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
                    case 10:
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
                    case 11:
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
                        if ((Token == 61))
                        {
                            Estado = 33;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 24:
                        if ((Token == 39))
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
                    case 25:
                        if ((Token == 39))
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
                    default: Salir = true; break;
                }
            }
            switch (Estado)
            {
                case 2: NumToken = 1; break;
                case 3: NumToken = 2; break;
                case 5: NumToken = 4; break;
                case 6: NumToken = 5; break;
                case 9: NumToken = 8; break;
                case 10: NumToken = 14; break;
                case 11: NumToken = 10; break;
                case 12: NumToken = 15; break;
                case 13: NumToken = 16; break;
                case 14: NumToken = 17; break;
                case 15: NumToken = 18; break;
                case 16: NumToken = 21; break;
                case 17: NumToken = 22; break;
                case 18: NumToken = 44; break;
                case 19: NumToken = 45; break;
                case 20: NumToken = 46; break;
                case 21: NumToken = 47; break;
                case 22: NumToken = 48; break;
                case 23: NumToken = 49; break;
                case 26: NumToken = 19; break;
                case 27: NumToken = 20; break;
                case 28: NumToken = 6; break;
                case 29: NumToken = 7; break;
                case 30: NumToken = 9; break;
                case 31: NumToken = 13; break;
                case 32: NumToken = 11; break;
                case 33: NumToken = 12; break;
                case 34: NumToken = 3; break;
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
                case 1:
                    {
                        switch (Lexema)
                        {
                            case "public":
                                {
                                    NumToken = 23;
                                }
                                break;
                            case "class":
                                {
                                    NumToken = 24;
                                }
                                break;
                            case "int":
                                {
                                    NumToken = 25;
                                }
                                break;
                            case "char":
                                {
                                    NumToken = 26;
                                }
                                break;
                            case "void":
                                {
                                    NumToken = 27;
                                }
                                break;
                            case "const":
                                {
                                    NumToken = 28;
                                }
                                break;
                            case "do":
                                {
                                    NumToken = 29;
                                }
                                break;
                            case "else":
                                {
                                    NumToken = 30;
                                }
                                break;
                            case "for":
                                {
                                    NumToken = 31;
                                }
                                break;
                            case "if":
                                {
                                    NumToken = 32;
                                }
                                break;
                            case "default":
                                {
                                    NumToken = 33;
                                }
                                break;
                            case "ref":
                                {
                                    NumToken = 34;
                                }
                                break;
                            case "return":
                                {
                                    NumToken = 35;
                                }
                                break;
                            case "switch":
                                {
                                    NumToken = 36;
                                }
                                break;
                            case "case":
                                {
                                    NumToken = 37;
                                }
                                break;
                            case "while":
                                {
                                    NumToken = 38;
                                }
                                break;
                            case "null":
                                {
                                    NumToken = 39;
                                }
                                break;
                            case "break":
                                {
                                    NumToken = 40;
                                }
                                break;
                            case "include":
                                {
                                    NumToken = 41;
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
