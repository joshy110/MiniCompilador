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

namespace Scann
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tbPath.ReadOnly = true;
        }

        private void btEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbPath.Text.Trim() == "")
                {
                    throw new Exception("Debe de seleccionar un archivo de texto.");
                }

                richtxt_Resu.Clear();

                Scann scanner = new Scann(tbPath.Text);
                Queue<string> salida = scanner.Start();

                while (salida.Count != 0)
                {
                    richtxt_Resu.AppendText(salida.Dequeue() + "\n");
                }
            }
            catch (Exception ex)
            {

                Utilidades.MostrarUsuarioError(ex);
            }
        }

        private void pbSearch_Click(object sender, EventArgs e)
        {
            ofdBuscador.FileName = "Seleccione un archivo.";
            ofdBuscador.Filter = "Text File|*.txt";
            ofdBuscador.Title = "Select a TXT File";

            //Si no se ha elegido un archivo se termina el método
            if (ofdBuscador.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            //Coloco el nombre del archivo ubicado en el textbox
            tbPath.Text = ofdBuscador.FileName;
        }

        /// <summary>
        /// Evento que maneja la obtención del resultado del texto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportar_Click(object sender, EventArgs e)
        {
            if(richtxt_Resu.Text != "")
            {
                sfdLink.Filter = "Text files (*.txt) | *.txt";
                sfdLink.ShowDialog();
                string ruta = sfdLink.FileName;
                if(ruta != "")
                {
                    StreamWriter escritor = new StreamWriter(ruta);
                    string[] arreglo = richtxt_Resu.Text.Split('\n');
                    for (int i = 0; i < arreglo.Length; i++)
                    {
                        if(arreglo[i] != "")
                        {
                            escritor.WriteLine(arreglo[i]);
                        }
                    }
                    escritor.Close();
                    MessageBox.Show("Exportación Exitosa", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
