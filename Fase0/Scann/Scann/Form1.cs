using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scann
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbPath.Text.Trim() == "")
                {
                    throw new Exception("Debe de seleccionar un archivo de texto.");
                }

                richTextBox1.Clear();

                Scann scanner = new Scann(tbPath.Text);
                Queue<string> salida = scanner.Start();

                while (salida.Count != 0)
                {
                    richTextBox1.AppendText(salida.Dequeue() + "\n");
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
            ofdBuscador.Filter = "Text File|*.tok";
            ofdBuscador.Title = "Select a TOK File";

            //Si no se ha elegido un archivo se termina el método
            if (ofdBuscador.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            //Coloco el nombre del archivo ubicado en el textbox
            tbPath.Text = ofdBuscador.FileName;
        }
    }
}
