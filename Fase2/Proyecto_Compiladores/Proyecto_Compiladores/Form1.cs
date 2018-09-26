using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Compiladores
{
    public partial class frmCompis : Form
    {
        Clases.Analizador Analisis;
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
                if (rtxtTextoArchivo.Text.Trim() != "")
                {
                    rtxtTextoArchivo.SelectAll();
                    rtxtTextoArchivo.SelectionBackColor = Color.Transparent;
                    rtxtTextoArchivo.ForeColor = Color.Black;
                    Analisis = new Clases.Analizador();

                    if (Analisis.InicioAnalisi(rtxtTextoArchivo.Text))
                    {
                        rtxtTextoArchivo.SelectAll();
                        rtxtTextoArchivo.SelectionBackColor = Color.Green;
                        rtxtTextoArchivo.ForeColor = Color.White;
                        Clases.Utilidades.MostrarMensajeExito("Texto analizado correctamente");
                        Tablas();
                    }
                    else
                    {
                        rtxtTextoArchivo.Select(0, Analisis.Ver_Pos_Erro());
                        rtxtTextoArchivo.SelectionBackColor = Color.Red;
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
                if(Analisis.Lista_NT[i].Producion_NT == -1)
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
                aux += tempo[i].ToString() + ",";
            }

            return aux.Substring(0, aux.Length - 1);
        }
    }
}
