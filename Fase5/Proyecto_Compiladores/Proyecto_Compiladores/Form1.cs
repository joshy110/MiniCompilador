using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Proyecto_Compiladores
{
    public partial class frmCompis : Form
    {
        Clases.Analizador Analisis;
        Clases.Tabla_Estados Estados;
        int espacio = 30;
        bool Carga_Exitosa;
        public frmCompis()
        {
            InitializeComponent();
            txtRuta.ReadOnly = true;
            dgvNoTerminales.DefaultCellStyle.ForeColor = Color.Black;
            dgvProducciones.DefaultCellStyle.ForeColor = Color.Black;
            dgvTokens.DefaultCellStyle.ForeColor = Color.Black;
        }

        /// <summary>
        /// Método que carga el archivo de entrada para posterior análisis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbCargaArchivo_Click(object sender, EventArgs e)
        {
            Clases.ManejoArchivoEntrada carga = new Clases.ManejoArchivoEntrada();
            rtxtTextoArchivo.Text = "";
            txtRuta.Text = "";
            OFDLink.Filter = "Text Files (*.txt) | *.txt";
            dgvTokens.Rows.Clear();
            dgvProducciones.Rows.Clear();
            dgvNoTerminales.Rows.Clear();
            richtxt_Estados.Text = "";
            if (OFDLink.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }
            else
            {
                txtRuta.Text = OFDLink.FileName;
                rtxtTextoArchivo.Text = carga.CargaArchivo(txtRuta.Text, rtxtTextoArchivo.Text);
                rtxtTextoArchivo.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            Carga_Exitosa = false;
            if (rtxtTextoArchivo.Text.Trim() != "")
            {
                rtxtTextoArchivo.SelectAll();
                rtxtTextoArchivo.SelectionBackColor = Color.Transparent;
                rtxtTextoArchivo.ForeColor = Color.Black;
                Analisis = new Clases.Analizador();
                Estados = new Clases.Tabla_Estados();

                if (Analisis.InicioAnalisi(rtxtTextoArchivo.Text))
                {
                    //Verificar_Elementos();
                    rtxtTextoArchivo.SelectAll();
                    rtxtTextoArchivo.SelectionBackColor = Color.Green;
                    rtxtTextoArchivo.ForeColor = Color.White;
                    Clases.Utilidades.MostrarMensajeExito("Texto analizado correctamente");
                    Analisis.Determinar_First();
                    Tablas();
                    Estados.Inicio();
                    Mostrar_Estados();
                    Carga_Exitosa = true;
                }
                else
                {
                    rtxtTextoArchivo.Select(0, Analisis.Ver_Pos_Erro());
                    rtxtTextoArchivo.SelectionBackColor = Color.Red;
                    Carga_Exitosa = false;
                    MessageBox.Show(Analisis.MostrarError(), "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No se ha ingresados ningun texto para analizar.");
            }
        }

        /// <summary>
        /// Evento que maneja el guardado de los archivos .Tok y .Dat en sus respectivos formatos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picbGuardar_Click(object sender, EventArgs e)
        {
            if (Carga_Exitosa)
            {
                FolderBrowserDialog abrir = new FolderBrowserDialog();
                abrir.ShowDialog();
                string path = abrir.SelectedPath;
                Archivo_TOK(path);
                Archivo_DAT(path);
                MessageBox.Show("Archivos .Tok y .DAT generados exitosamente", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se ha cargado ningún archivo para analizar. \n Ingrese un arhchivo.", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tablas()
        {
            Tabla_Token();
            Tabla_NT();
            Tabla_Produ();
        }

        /// <summary>
        /// Método que llena la tabla de valores corrspondiente
        /// </summary>
        private void Tabla_Token()
        {
            dgvTokens.Rows.Clear();
            for(int i = 0; i < Clases.Analizador.Lista_Tokens.Count; i++)
            {
                dgvTokens.Rows.Add();
                dgvTokens.Rows[i].Cells[0].Value = Clases.Analizador.Lista_Tokens[i].Numero_Token;
                dgvTokens.Rows[i].Cells[1].Value = Clases.Analizador.Lista_Tokens[i].Simbolo_Token;
                if(Clases.Analizador.Lista_Tokens[i].Prescedencia_Token == 0)
                {
                    dgvTokens.Rows[i].Cells[2].Value = ".";
                }
                else
                {
                    dgvTokens.Rows[i].Cells[2].Value = Clases.Analizador.Lista_Tokens[i].Prescedencia_Token;
                }
                if(Clases.Analizador.Lista_Tokens[i].Asociatividad_Token == "no_hay" || Clases.Analizador.Lista_Tokens[i].Asociatividad_Token == "" || Clases.Analizador.Lista_Tokens[i].Asociatividad_Token == "ES_T")
                {
                    dgvTokens.Rows[i].Cells[3].Value = "no existe asociatividad";
                }
                else
                {
                    dgvTokens.Rows[i].Cells[3].Value = Clases.Analizador.Lista_Tokens[i].Asociatividad_Token;
                }

            }
        }

        /// <summary>
        /// Método que muestra los no terminales existentes
        /// </summary>
        private void Tabla_NT()
        {
            dgvNoTerminales.Rows.Clear();
            for(int i = 0; i < Clases.Analizador.Lista_NT.Count; i++)
            {
                dgvNoTerminales.Rows.Add();
                dgvNoTerminales.Rows[i].Cells[0].Value = Clases.Analizador.Lista_NT[i].Numero_NT;
                dgvNoTerminales.Rows[i].Cells[1].Value = Clases.Analizador.Lista_NT[i].No_Terminal_NT;
                dgvNoTerminales.Rows[i].Cells[2].Value = Lista_Elementos(Clases.Analizador.Lista_NT[i].First_NT);
                if (Clases.Analizador.Lista_NT[i].Producion_NT == -1)
                {
                    dgvNoTerminales.Rows[i].Cells[3].Value = "Error";
                }
                else
                {
                    dgvNoTerminales.Rows[i].Cells[3].Value = Clases.Analizador.Lista_NT[i].Producion_NT;
                }
            }
        }

        /// <summary>
        /// Método que muestra las producciones existentes
        /// </summary>
        private void Tabla_Produ()
        {
            dgvProducciones.Rows.Clear();
            for(int i = 0; i < Clases.Analizador.Lista_Producciones.Count; i++)
            {
                dgvProducciones.Rows.Add();
                dgvProducciones.Rows[i].Cells[0].Value = Clases.Analizador.Lista_Producciones[i].Produccion_PR;
                dgvProducciones.Rows[i].Cells[1].Value = Clases.Analizador.Lista_Producciones[i].Longitud_PR;
                dgvProducciones.Rows[i].Cells[2].Value = Clases.Analizador.Lista_Producciones[i].Siguiente_PR;
                dgvProducciones.Rows[i].Cells[3].Value = Lista_Elementos(Clases.Analizador.Lista_Producciones[i].Elementos);

            }
        }

        /// <summary>
        /// Método que me sirve para sacar los valores de los elementos
        /// </summary>
        /// <param name="tempo"></param>
        /// <returns></returns>
        private string Lista_Elementos(List<int> tempo)
        {
            string aux = "";

            for(int i = 0; i < tempo.Count; i++)
            {
                //es un no terminal
                if(tempo[i] < 0)
                {
                    aux += "[" + Analisis.Imprimir_Valor_NT(tempo[i]) + "] -> ";
                }
                //Es un terminal
                else if(tempo[i] > 0)
                {
                    aux += "[" + Analisis.Imprimr_Valor_Terminal(tempo[i]) + "] -> ";
                }
                else
                {
                    aux += "[ ɛ ] ->";
                }
            }
            if (aux.Length > 3)
            {
                return aux.Substring(0, aux.Length - 3);
            }

            return aux;
        }

        /// <summary>
        /// Método que escribe en un archivo con extensión .tok los tokens
        /// </summary>
        /// <param name="path"></param>
        private void Archivo_TOK(string path)
        {
            int tok_Actual = 0;
            int tempo = Clases.Analizador.Lista_Tokens.Count + 1;
            StreamWriter escritorToken = new StreamWriter(path + "\\" + Analisis.Nombre_Compi + ".tok");
            escritorToken.WriteLine("Tokens");

            /*Se agregan los conjuntos*/
            for (int i = 0; i < Analisis.Lista_Conjuntos.Count; i++)
            {
                string nombreConjunto = Analisis.Lista_Conjuntos[i];
                string defConjunto = Analisis.Lista_Estructura_Conjuntos[i];
                escritorToken.WriteLine("\t" + nombreConjunto + " { " + defConjunto + " } ");
            }

            /*TOKENS*/
            for(int i = 0; i < Clases.Analizador.Lista_Tokens.Count; i++)
            {
                string numeroToken = (i + 1).ToString();
                char inicio = Clases.Analizador.Lista_Tokens[i].Simbolo_Token[0];
                //Esto cuando venga algo que sea necesario ponerlo entre comillas simples o dobles
                if (inicio == '\'' || inicio == '\"')
                {
                    string simboloTempo = Clases.Analizador.Lista_Tokens[i].Simbolo_Token;
                    string defToken = "";
                    for(int j = 0; j < simboloTempo.Length - 1; j++)
                    {
                        defToken += inicio.ToString() + simboloTempo[j].ToString() + inicio.ToString();
                    }
                    escritorToken.WriteLine("\t Token " + numeroToken + " = " + defToken + "\t"+ " ;");
                }
                else if (Char.IsLetter(inicio))
                {
                    string reservadas = "";
                    string defToken = Analisis.Lista_Estructura_Tokens[tok_Actual];
                    if(Analisis.Lista_Check[tok_Actual])
                    {
                        defToken = Convertir_Check(defToken);
                        for(int j = 1; j < Analisis.Lista_KeyWords.Count; j++)
                        {
                            reservadas += "\n"+ "\t" + tempo.ToString() + " = '" + Analisis.Lista_KeyWords[j] + "'";
                            tempo++;
                        }
                    }
                    if(reservadas != "")
                    {
                        escritorToken.WriteLine("\t Token " + numeroToken + " = " + defToken.ToLower() + "{" + reservadas + "} " + " ;");
                    }
                    else
                    {
                        escritorToken.WriteLine("\t Token " + numeroToken + " = " + defToken.ToLower() + " ;");
                    }
                    tok_Actual++;
                }
            }
            escritorToken.WriteLine("END");
            escritorToken.WriteLine("Error = " + tempo.ToString());
            escritorToken.Close();

        }

        /// <summary>
        /// Método que general el archivo .Dat 
        /// </summary>
        /// <param name="path"></param>
        private void Archivo_DAT(string path)
        {
           StreamWriter esritorDat = new StreamWriter(path + "\\" + Analisis.Nombre_Compi + ".dat");

            /*TABLA TOKENS*/
            esritorDat.WriteLine("".PadRight(espacio * 5, '#'));
            esritorDat.WriteLine("TABLA DE TOKENS");
            esritorDat.WriteLine("".PadRight(espacio * 5, '-'));
            esritorDat.WriteLine("\t" + "NUMERO TOKEN".PadRight(espacio, ' ') + "\t" + "|" + "\t" +"SÍMBOLO TOKEN".PadRight(espacio, ' ') + "\t" + "|" + "\t"  + "PRECEDENCIA".PadRight(espacio, ' ') + "\t" + "|" + "\t"+ "ASOCIATIVIDAD".PadRight(espacio, ' ') + "\t" + "|");
            esritorDat.WriteLine("".PadRight(espacio * 5, '-'));
            for (int i = 0; i < dgvTokens.Rows.Count - 1; i++)
            {
                for(int j = 0; j < dgvTokens.Columns.Count; j++)
                {
                    esritorDat.Write("\t" + dgvTokens.Rows[i].Cells[j].Value.ToString().PadRight(espacio, ' ') + "\t" + "|");
                }
                esritorDat.WriteLine();
            }

            /*TABLA NO TERMINALES*/
            esritorDat.WriteLine("".PadRight(espacio * 5, '#'));
            esritorDat.WriteLine("TABLA NO TERMINALES");
            esritorDat.WriteLine("".PadRight(espacio * 5, '-'));
            esritorDat.WriteLine("\t" + "NUMERO NO TERMINAL".PadRight(espacio, ' ') + "\t" + "|" + "\t" + "NO TERMINAL".PadRight(espacio, ' ') + "\t" + "|" + "\t" + "FIRST".PadRight(espacio, ' ') + "\t" + "|" + "\t" + "PRODUCCIÓN".PadRight(espacio, ' ') + "\t" + "|");
            esritorDat.WriteLine("".PadRight(espacio * 5, '-'));
            for(int i= 0; i < dgvNoTerminales.Rows.Count - 1; i++)
            {
                for(int j= 0; j < dgvNoTerminales.Columns.Count; j++)
                {
                    esritorDat.Write("\t" + dgvNoTerminales.Rows[i].Cells[j].Value.ToString().PadRight(espacio, ' ') + "\t" + "|");
                }
                esritorDat.WriteLine();
            }

            /*TABLA DE PROUDCCIONES*/
            esritorDat.WriteLine("".PadRight(espacio * 5, '#'));
            esritorDat.WriteLine("TABLA DE PRODUCCIONES");
            esritorDat.WriteLine("".PadRight(espacio * 5, '-'));
            esritorDat.WriteLine("\t" + "PRODUCCIÓN".PadRight(espacio, ' ') + "\t" + "|" + "\t" + "LONGITUD".PadRight(espacio, ' ') + "\t" + "|" +"\t" + "SIGUIENTE PRODUCCIÓN".PadRight(espacio, ' ') + "\t" + "|" + "\t" + "ELEMENTOS".PadRight(espacio, ' ') + "\t" + "|");
            esritorDat.WriteLine("".PadRight(espacio * 5, '-'));
            for (int i = 0; i < dgvProducciones.Rows.Count -1; i++)
            {
                for(int j = 0; j < dgvProducciones.Columns.Count; j++)
                {
                    esritorDat.Write("\t" + dgvProducciones.Rows[i].Cells[j].Value.ToString().PadRight(espacio, ' ') + "\t" + "|");
                }
                esritorDat.WriteLine();
            }
            esritorDat.WriteLine("".PadRight(espacio * 5, '#'));
            esritorDat.Close();

        }

        /// <summary>
        /// Función que quita el check de los tokens
        /// </summary>
        /// <param name="palabra"></param>
        /// <returns></returns>
        private string Convertir_Check(string palabra)
        {
            palabra = palabra.ToLower();
            int x = palabra.IndexOf("check");
            return palabra.Substring(0, palabra.Length - (palabra.Length - x));
        }

        /// <summary>
        /// Método que muestra los estados obtenidos de los kernels
        /// </summary>
        private void Mostrar_Estados()
        {
            string texto_estados = "";
            texto_estados += Clases.Tabla_Estados.Kernels_[0].Numero + " :";
            for (int i = 0; i < Clases.Tabla_Estados.Kernels_[0].N_Estado.Count; i++)
            {
                texto_estados += "(" + Clases.Tabla_Estados.Kernels_[0].N_Estado[i] + Clases.Tabla_Estados.Kernels_[0].GoTo[i] + ")";
            }
            texto_estados += "\n";
            //Se obtiene las producciones y La temporales
            for (int i = 0; i < Clases.Tabla_Estados.Kernels_[0].NT_Nuevo_Tempo.Count; i++)
            {
                texto_estados += " \t" +  "[" + Clases.Tabla_Estados.Kernels_[0].NT_Nuevo_Tempo[i] + "->" + Lista_Tabla_Estados(Clases.Tabla_Estados.Kernels_[0].Produ_Nueva_Tempo[i]) 
                        + "      |      " + Lista_Tabla_Estados(Clases.Tabla_Estados.Kernels_[0].LA_Nuevo_Tempo[i]) + "\n";
            }
            //Se obtiene las producciones y La reales
            for (int i = 0; i < Clases.Tabla_Estados.Kernels_[0].NT_.Count; i++)
            {
                texto_estados += " \t" +  Clases.Tabla_Estados.Kernels_[0].NT_[i] + "->" + Lista_Tabla_Estados(Clases.Tabla_Estados.Kernels_[0].Produ[i])
                    + "      |      " + Lista_Tabla_Estados(Clases.Tabla_Estados.Kernels_[0].LookA[i]) + "\n";
            }
            texto_estados += "\n";
            //Se hacen los estados y Gotos
            for (int i = 1; i < Clases.Tabla_Estados.Kernels_.Count; i++)
            {
                texto_estados += Clases.Tabla_Estados.Kernels_[i].Numero + " : Goto";
                for (int j = 0; j < Clases.Tabla_Estados.Kernels_[i].N_Estado.Count; j++)
                {
                    texto_estados += "(" + Clases.Tabla_Estados.Kernels_[i].N_Estado[j] + "," + Clases.Tabla_Estados.Kernels_[i].GoTo[j] + ")";
                }
                texto_estados += "\n";
                //Se sacan los kernesl temporales
                for (int j = 0; j < Clases.Tabla_Estados.Kernels_[i].NT_Nuevo_Tempo.Count; j++)
                {
                    texto_estados += " \t" + "[" + Clases.Tabla_Estados.Kernels_[i].NT_Nuevo_Tempo[j] + "->" + Lista_Tabla_Estados(Clases.Tabla_Estados.Kernels_[i].Produ_Nueva_Tempo[j])
                    + "      |      " + Lista_Tabla_Estados(Clases.Tabla_Estados.Kernels_[i].LA_Nuevo_Tempo[j]) + "\n";
                }
                //Se sacan los kernels reales
                for (int j = 0; j < Clases.Tabla_Estados.Kernels_[i].NT_.Count; j++)
                {
                    texto_estados += " \t" +  "[" + Clases.Tabla_Estados.Kernels_[i].NT_[j] + "->" + Lista_Tabla_Estados(Clases.Tabla_Estados.Kernels_[i].Produ[j])
                    + "      |      " + Lista_Tabla_Estados(Clases.Tabla_Estados.Kernels_[i].LookA[j]) + "\n";
                }
                texto_estados += "\n";
            }
            richtxt_Estados.Text = texto_estados;
        }

        /// <summary>
        /// Función que obtiene la lista de terminales o no termiales para escribirlos en la tabla de estados
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        private string Lista_Tabla_Estados(List<int> lista)
        {
            string texto_aux = "";
            for (int i = 0; i < lista.Count; i++)
            {
                if(lista[i] < 0)
                {
                    texto_aux += Analisis.Imprimir_Valor_NT(lista[i]) + " ";
                }
                else
                {
                    if (lista[i] == Clases.Tabla_Estados.Punto)
                    {
                        texto_aux += ". ";
                    }
                    else if (lista[i] == Clases.Tabla_Estados.Dolar)
                    {
                        texto_aux += "$ ";
                    }
                    else
                    {
                        texto_aux += Analisis.Imprimr_Valor_Terminal(lista[i]) + " ";
                    }
                }
            }
            if(texto_aux.Length > 1)
            {
                return texto_aux.Substring(0, texto_aux.Length - 1);
            }

            return texto_aux;
        }
    }
}
