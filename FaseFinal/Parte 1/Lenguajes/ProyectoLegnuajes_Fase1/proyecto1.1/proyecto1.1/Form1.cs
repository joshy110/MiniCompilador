using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneradorScanner;

namespace Proyecto1._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tabControl1.Enabled = false;
            tabControl1.Visible = false;
            pDatos.Enabled = true;
            pDatos.Visible = true;
            pDatos.Location = posicion;
            tbPath.Text = "";
            this.Width = 379;
            this.Height = 214;
        }

        private System.Drawing.Point posicion = new System.Drawing.Point(0, 28);

        private void pbSearch_Click(object sender, EventArgs e)
        {
            ofdBuscador.FileName = "Seleccione un archivo.";
            ofdBuscador.Filter = "TOK File|*.tok";
            ofdBuscador.Title = "Select a TOK File"; 

            //Si no se ha elegido un archivo se termina el método
            if (ofdBuscador.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            //Coloco el nombre del archivo ubicado en el textbox
            tbPath.Text = ofdBuscador.FileName;
        }

        private void btEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbPath.Text == "")
                {
                    throw new Exception("Debe de seleccionar un archivo de texto.");
                }
                //Fase 1,2,3
                //Analisa si la gramática es correcta
                Analizador analisis = new Analizador(tbPath.Text); 
                GeneradorAutomata generadorAutomata = new GeneradorAutomata(analisis.TokensFinales, analisis.Conjuntos);
                //Genera los firts, last, follow
                analisis = null;
                generadorAutomata.Start();
                Utilidades.MostrarMensajeExito("Gramática correcta.");
                MostarFollow(generadorAutomata);

                //Fase 4
                //Se maneja el archivo de salida con el scanner
                sfdExportar.Filter = "TOK File|*.tok";
                sfdExportar.Title = "Save a TOK File";
                if (sfdExportar.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                string leyenda = sfdExportar.FileName;

                Scann generadorScanner = new Scann(generadorAutomata.Conjuntos, leyenda, generadorAutomata.Estados, generadorAutomata.Tokens);
                generadorAutomata = null;
                generadorScanner.Start();
            }
            catch (Exception ex)
            {
                Utilidades.MostrarUsuarioError(ex);
            }
        }

        private void MostarFollow(GeneradorAutomata generador)
        {
            pDatos.Enabled = false;
            pDatos.Visible = false;

            tabControl1.Enabled = true;
            tabControl1.Visible = true;
            tabControl1.Location = posicion;

            this.Width = 688;
            this.Height = 450;

            dgNodos.Rows.Clear();
            dgFollow.Rows.Clear();

            dgNodos.Rows.Add(generador.Funciones.Count);

            for (int i = 0; i < generador.Funciones.Count; i++)
            {
                for (int j = 0; j < generador.Funciones[i].Count(); j++)
                {
                    dgNodos.Rows[i].Cells[j].Value = generador.Funciones[i][j];
                }
            }

            dgFollow.Rows.Add(generador.Follow.Count);

            for (int i = 0; i < generador.Follow.Count; i++)
            {
                dgFollow.Rows[i].Cells[0].Value = (i + 1);
                string follows = "";
                foreach (int follow in generador.Follow[i + 1])
                {
                    follows += follow.ToString() + "  ";
                }
                dgFollow.Rows[i].Cells[1].Value = follows;
            }
        }
        
        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                tabControl1.Enabled = false;
                tabControl1.Visible = false;

                pDatos.Enabled = true;
                pDatos.Visible = true;
                pDatos.Location = posicion;
                tbPath.Text = "";
                this.Width = 379;
                this.Height = 214;
            }
            catch (Exception ex)
            {

                Utilidades.MostrarUsuarioError(ex);
            }
        }
    }
}
