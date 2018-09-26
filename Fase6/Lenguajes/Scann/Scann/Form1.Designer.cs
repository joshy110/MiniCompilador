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
            this.tbPath = new System.Windows.Forms.TextBox();
            this.btEnviar = new System.Windows.Forms.Button();
            this.pbSearch = new System.Windows.Forms.PictureBox();
            this.lbPath = new System.Windows.Forms.Label();
            this.richtxt_Resu = new System.Windows.Forms.RichTextBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.sfdLink = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdBuscador
            // 
            this.ofdBuscador.FileName = "ofdBuscador";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(12, 27);
            this.tbPath.Multiline = true;
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(365, 30);
            this.tbPath.TabIndex = 6;
            // 
            // btEnviar
            // 
            this.btEnviar.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btEnviar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btEnviar.Location = new System.Drawing.Point(93, 63);
            this.btEnviar.Name = "btEnviar";
            this.btEnviar.Size = new System.Drawing.Size(220, 23);
            this.btEnviar.TabIndex = 9;
            this.btEnviar.Text = "Analizar";
            this.btEnviar.UseVisualStyleBackColor = false;
            this.btEnviar.Click += new System.EventHandler(this.btEnviar_Click);
            // 
            // pbSearch
            // 
            this.pbSearch.Image = ((System.Drawing.Image)(resources.GetObject("pbSearch.Image")));
            this.pbSearch.Location = new System.Drawing.Point(383, 27);
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
            this.lbPath.Location = new System.Drawing.Point(9, 8);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(108, 13);
            this.lbPath.TabIndex = 7;
            this.lbPath.Text = "Selección de Archivo";
            // 
            // richtxt_Resu
            // 
            this.richtxt_Resu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richtxt_Resu.Location = new System.Drawing.Point(12, 92);
            this.richtxt_Resu.Name = "richtxt_Resu";
            this.richtxt_Resu.ReadOnly = true;
            this.richtxt_Resu.Size = new System.Drawing.Size(401, 329);
            this.richtxt_Resu.TabIndex = 13;
            this.richtxt_Resu.Text = "";
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnExportar.Location = new System.Drawing.Point(93, 427);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(220, 23);
            this.btnExportar.TabIndex = 14;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 462);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.btEnviar);
            this.Controls.Add(this.richtxt_Resu);
            this.Controls.Add(this.pbSearch);
            this.Controls.Add(this.lbPath);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdBuscador;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Button btEnviar;
        private System.Windows.Forms.PictureBox pbSearch;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.RichTextBox richtxt_Resu;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.SaveFileDialog sfdLink;
    }
}

