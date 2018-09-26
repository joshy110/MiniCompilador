using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    public class Scann
    {
        private ManejoArchivoEntrada ArchivoEntrada;
        protected int Token;

        public Scann(string path)
        {
            ArchivoEntrada = new ManejoArchivoEntrada(path);

            while (Token != 65535)
            {

            }
        }

        public int TomaToken()
        {
            int Estado = 0;
            string Lexema = "";
            bool Salir = false;
            bool Error = false;
            int NumToken;
            ArchivoEntrada.LeerSiguiente();
            while (!Salir && Token != 65535)
            {
                switch (Estado)
                {
                    case 1:
                        if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 2;
                            Lexema += ((char)Token).ToString();
                            ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 61))
                        {
                            Estado = 3;
                            Lexema += ((char)Token).ToString();
                            ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 58))
                        {
                            Estado = 4;
                            Lexema += ((char)Token).ToString();
                            ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 95) || (Token >= 97 && Token <= 122) || (Token >= 65 && Token <= 90))
                        {
                            Estado = 5;
                            Lexema += ((char)Token).ToString();
                            ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Salir = true;
                        }
                        break;
                    case 2:
                        if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 2;
                            Lexema += ((char)Token).ToString();
                            ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                    case 4:
                        if ((Token == 61))
                        {
                            Estado = 6;
                            Lexema += ((char)Token).ToString();
                            ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Error = true;
                            Salir = true;
                        }
                        break;
                    case 5:
                        if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 5;
                            Lexema += ((char)Token).ToString();
                            ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 95) || (Token >= 97 && Token <= 122) || (Token >= 65 && Token <= 90))
                        {
                            Estado = 5;
                            Lexema += ((char)Token).ToString();
                            ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
                            Salir = true;
                        }
                        break;
                }
            }
            switch (Estado)
            {
                case 2: NumToken = 1; break;
                case 3: NumToken = 2; break;
                case 5: NumToken = 4; break;
                case 6: NumToken = 3; break;
                default: NumToken = -1; Error = true; break;
            }
            return NumToken;
        }
    }
}
