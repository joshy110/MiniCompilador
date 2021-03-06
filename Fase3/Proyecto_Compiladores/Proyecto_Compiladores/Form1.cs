﻿using System;
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
        int espacio = 30;
        bool Carga_Exitosa;
        public frmCompis()
        {
            InitializeComponent();
            txtRuta.ReadOnly = true;
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
            //try
            //{
                Carga_Exitosa = false;
                if (rtxtTextoArchivo.Text.Trim() != "")
                {
                    rtxtTextoArchivo.SelectAll();
                    rtxtTextoArchivo.SelectionBackColor = Color.Transparent;
                    rtxtTextoArchivo.ForeColor = Color.Black;
                    Analisis = new Clases.Analizador();

                    if (Analisis.InicioAnalisi(rtxtTextoArchivo.Text))
                    {
                        //Verificar_Elementos();
                        rtxtTextoArchivo.SelectAll();
                        rtxtTextoArchivo.SelectionBackColor = Color.Green;
                        rtxtTextoArchivo.ForeColor = Color.White;
                        Clases.Utilidades.MostrarMensajeExito("Texto analizado correctamente");
                        Analisis.Determinar_First();
                        Tablas();
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
            //}
            //catch (Exception ex)
            //{
            //    Clases.Utilidades.MostrarUsuarioError(ex);
            //}
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
            for(int i = 0; i < Analisis.Lista_Tokens.Count; i++)
            {
                dgvTokens.Rows.Add();
                dgvTokens.Rows[i].Cells[0].Value = Analisis.Lista_Tokens[i].Numero_Token;
                dgvTokens.Rows[i].Cells[1].Value = Analisis.Lista_Tokens[i].Simbolo_Token;
                if(Analisis.Lista_Tokens[i].Prescedencia_Token == 0)
                {
                    dgvTokens.Rows[i].Cells[2].Value = ".";
                }
                else
                {
                    dgvTokens.Rows[i].Cells[2].Value = Analisis.Lista_Tokens[i].Prescedencia_Token;
                }
                if(Analisis.Lista_Tokens[i].Asociatividad_Token == "no_hay" || Analisis.Lista_Tokens[i].Asociatividad_Token == "" || Analisis.Lista_Tokens[i].Asociatividad_Token == "ES_T")
                {
                    dgvTokens.Rows[i].Cells[3].Value = "no existe asociatividad";
                }
                else
                {
                    dgvTokens.Rows[i].Cells[3].Value = Analisis.Lista_Tokens[i].Asociatividad_Token;
                }

            }
        }

        /// <summary>
        /// Método que muestra los no terminales existentes
        /// </summary>
        private void Tabla_NT()
        {
            dgvNoTerminales.Rows.Clear();
            for(int i = 0; i < Analisis.Lista_NT.Count; i++)
            {
                dgvNoTerminales.Rows.Add();
                dgvNoTerminales.Rows[i].Cells[0].Value = Analisis.Lista_NT[i].Numero_NT;
                dgvNoTerminales.Rows[i].Cells[1].Value = Analisis.Lista_NT[i].No_Terminal_NT;
                dgvNoTerminales.Rows[i].Cells[2].Value = Lista_Elementos(Analisis.Lista_NT[i].First_NT);
                if (Analisis.Lista_NT[i].Producion_NT == -1)
                {
                    dgvNoTerminales.Rows[i].Cells[3].Value = "Error";
                }
                else
                {
                    dgvNoTerminales.Rows[i].Cells[3].Value = Analisis.Lista_NT[i].Producion_NT;
                }
            }
        }

        /// <summary>
        /// Método que muestra las producciones existentes
        /// </summary>
        private void Tabla_Produ()
        {
            dgvProducciones.Rows.Clear();
            for(int i = 0; i < Analisis.Lista_Producciones.Count; i++)
            {
                dgvProducciones.Rows.Add();
                dgvProducciones.Rows[i].Cells[0].Value = Analisis.Lista_Producciones[i].Produccion_PR;
                dgvProducciones.Rows[i].Cells[1].Value = Analisis.Lista_Producciones[i].Longitud_PR;
                dgvProducciones.Rows[i].Cells[2].Value = Analisis.Lista_Producciones[i].Siguiente_PR;
                dgvProducciones.Rows[i].Cells[3].Value = Lista_Elementos(Analisis.Lista_Producciones[i].Elementos);

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
            int tempo = Analisis.Lista_Tokens.Count + 1;
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
            for(int i = 0; i < Analisis.Lista_Tokens.Count; i++)
            {
                string numeroToken = (i + 1).ToString();
                char inicio = Analisis.Lista_Tokens[i].Simbolo_Token[0];
                //Esto cuando venga algo que sea necesario ponerlo entre comillas simples o dobles
                if (inicio == '\'' || inicio == '\"')
                {
                    string simboloTempo = Analisis.Lista_Tokens[i].Simbolo_Token;
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
    }
}
