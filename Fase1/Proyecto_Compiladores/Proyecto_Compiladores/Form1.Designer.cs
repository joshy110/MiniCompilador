namespace Proyecto_Compiladores
{
    partial class frmCompis
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
            this.pbCargaArchivo = new System.Windows.Forms.PictureBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.rtxtTextoArchivo = new System.Windows.Forms.RichTextBox();
            this.lbTituloIngreso = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OFDLink = new System.Windows.Forms.OpenFileDialog();
            this.btnAnalizar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCargaArchivo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCargaArchivo
            // 
            this.pbCargaArchivo.Image = global::Proyecto_Compiladores.Properties.Resources.abrirArchivo_4;
            this.pbCargaArchivo.Location = new System.Drawing.Point(580, 12);
            this.pbCargaArchivo.Name = "pbCargaArchivo";
            this.pbCargaArchivo.Size = new System.Drawing.Size(64, 53);
            this.pbCargaArchivo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCargaArchivo.TabIndex = 1;
            this.pbCargaArchivo.TabStop = false;
            this.pbCargaArchivo.Click += new System.EventHandler(this.pbCargaArchivo_Click);
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(11, 32);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(563, 20);
            this.txtRuta.TabIndex = 2;
            // 
            // rtxtTextoArchivo
            // 
            this.rtxtTextoArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtTextoArchivo.Location = new System.Drawing.Point(11, 89);
            this.rtxtTextoArchivo.Name = "rtxtTextoArchivo";
            this.rtxtTextoArchivo.Size = new System.Drawing.Size(633, 397);
            this.rtxtTextoArchivo.TabIndex = 0;
            this.rtxtTextoArchivo.Text = "";
            // 
            // lbTituloIngreso
            // 
            this.lbTituloIngreso.AutoSize = true;
            this.lbTituloIngreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTituloIngreso.Location = new System.Drawing.Point(189, 59);
            this.lbTituloIngreso.Name = "lbTituloIngreso";
            this.lbTituloIngreso.Size = new System.Drawing.Size(162, 24);
            this.lbTituloIngreso.TabIndex = 3;
            this.lbTituloIngreso.Text = "Texto Ingresado";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ruta Archivo";
            // 
            // OFDLink
            // 
            this.OFDLink.FileName = "openFileDialog1";
            // 
            // btnAnalizar
            // 
            this.btnAnalizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalizar.Location = new System.Drawing.Point(204, 492);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(163, 32);
            this.btnAnalizar.TabIndex = 6;
            this.btnAnalizar.Text = "Analizar";
            this.btnAnalizar.UseVisualStyleBackColor = true;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
            // 
            // frmCompis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 536);
            this.Controls.Add(this.btnAnalizar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbTituloIngreso);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.pbCargaArchivo);
            this.Controls.Add(this.rtxtTextoArchivo);
            this.Name = "frmCompis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compiladores";
            ((System.ComponentModel.ISupportInitialize)(this.pbCargaArchivo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbCargaArchivo;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.RichTextBox rtxtTextoArchivo;
        private System.Windows.Forms.Label lbTituloIngreso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog OFDLink;
        private System.Windows.Forms.Button btnAnalizar;
    }
}

