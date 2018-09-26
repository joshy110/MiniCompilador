namespace Scann
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ofdBuscador = new System.Windows.Forms.OpenFileDialog();
            this.pDatos = new System.Windows.Forms.Panel();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btEnviar = new System.Windows.Forms.Button();
            this.pbSearch = new System.Windows.Forms.PictureBox();
            this.lbPath = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.pDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdBuscador
            // 
            this.ofdBuscador.FileName = "ofdBuscador";
            // 
            // pDatos
            // 
            this.pDatos.Controls.Add(this.tbPath);
            this.pDatos.Controls.Add(this.btEnviar);
            this.pDatos.Controls.Add(this.pbSearch);
            this.pDatos.Controls.Add(this.lbPath);
            this.pDatos.Location = new System.Drawing.Point(12, 12);
            this.pDatos.Name = "pDatos";
            this.pDatos.Size = new System.Drawing.Size(361, 145);
            this.pDatos.TabIndex = 12;
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(23, 36);
            this.tbPath.Multiline = true;
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(280, 30);
            this.tbPath.TabIndex = 6;
            // 
            // btEnviar
            // 
            this.btEnviar.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEnviar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btEnviar.Location = new System.Drawing.Point(145, 100);
            this.btEnviar.Name = "btEnviar";
            this.btEnviar.Size = new System.Drawing.Size(75, 23);
            this.btEnviar.TabIndex = 9;
            this.btEnviar.Text = "Analizar";
            this.btEnviar.UseVisualStyleBackColor = false;
            this.btEnviar.Click += new System.EventHandler(this.btEnviar_Click);
            // 
            // pbSearch
            // 
            this.pbSearch.Image = ((System.Drawing.Image)(resources.GetObject("pbSearch.Image")));
            this.pbSearch.Location = new System.Drawing.Point(309, 36);
            this.pbSearch.Name = "pbSearch";
            this.pbSearch.Size = new System.Drawing.Size(30, 30);
            this.pbSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSearch.TabIndex = 8;
            this.pbSearch.TabStop = false;
            this.pbSearch.Click += new System.EventHandler(this.pbSearch_Click);
            // 
            // lbPath
            // 
            this.lbPath.AutoSize = true;
            this.lbPath.Location = new System.Drawing.Point(20, 20);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(108, 13);
            this.lbPath.TabIndex = 7;
            this.lbPath.Text = "Selección de Archivo";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 175);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(361, 178);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 365);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.pDatos);
            this.Name = "Form1";
            this.Text = "Form1";
            this.pDatos.ResumeLayout(false);
            this.pDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdBuscador;
        private System.Windows.Forms.Panel pDatos;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btEnviar;
        private System.Windows.Forms.PictureBox pbSearch;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

