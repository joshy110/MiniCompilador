using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compiladores.Clases
{
    public class Scann
    {
        private ArchivoEntrada ArchivoEntrada;
        protected int Token;
        string Lexema = "";
        private bool Error;
        private bool Salir;

        public Scann(string path)
        {
            ArchivoEntrada = new ArchivoEntrada(path);
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
                    Resultado.Enqueue("Error -1: " + Lexema + " = " + "Token no reconocido");
                }
                else
                {
                    Resultado.Enqueue(Lexema + " = " + " Token " + salida);
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
                        if ((Token == 43))
                        {
                            Estado = 2;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token == 42))
                        {
                            Estado = 3;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 4;
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
                        if ((Token >= 48 && Token <= 57))
                        {
                            Estado = 4;
                            Lexema += ((char)Token).ToString();
                            Token = ArchivoEntrada.LeerSiguiente();
                        }
                        else
                        {
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
                case 4: NumToken = 3; break;
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
            }
            return NumToken;
        }
    }
}

