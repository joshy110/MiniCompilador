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
                    Clases.Analizador Analisis = new Clases.Analizador();

                    if (Analisis.InicioAnalisi(rtxtTextoArchivo.Text))
                    {
                        rtxtTextoArchivo.SelectAll();
                        rtxtTextoArchivo.SelectionBackColor = Color.Green;
                        rtxtTextoArchivo.ForeColor = Color.White;
                        Clases.Utilidades.MostrarMensajeExito("Texto analizado correctamente");
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
    }
}
