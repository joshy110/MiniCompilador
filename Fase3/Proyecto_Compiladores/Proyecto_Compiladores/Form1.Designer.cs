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
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.rtxtTextoArchivo = new System.Windows.Forms.RichTextBox();
            this.lbTituloIngreso = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.OFDLink = new System.Windows.Forms.OpenFileDialog();
            this.btnAnalizar = new System.Windows.Forms.Button();
            this.dgvTokens = new System.Windows.Forms.DataGridView();
            this.No_Token = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Simbolo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Precedencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Asociatividad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvNoTerminales = new System.Windows.Forms.DataGridView();
            this.CNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFirst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CProdu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvProducciones = new System.Windows.Forms.DataGridView();
            this.CProdu_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSigueProdu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CElements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picbGuardar = new System.Windows.Forms.PictureBox();
            this.pbCargaArchivo = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbTokens = new System.Windows.Forms.TabPage();
            this.tbNT = new System.Windows.Forms.TabPage();
            this.tbProducciones = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTokens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoTerminales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbGuardar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCargaArchivo)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tbTokens.SuspendLayout();
            this.tbNT.SuspendLayout();
            this.tbProducciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(11, 32);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(527, 20);
            this.txtRuta.TabIndex = 2;
            // 
            // rtxtTextoArchivo
            // 
            this.rtxtTextoArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtTextoArchivo.Location = new System.Drawing.Point(7, 89);
            this.rtxtTextoArchivo.Name = "rtxtTextoArchivo";
            this.rtxtTextoArchivo.Size = new System.Drawing.Size(601, 493);
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
            this.btnAnalizar.Location = new System.Drawing.Point(26, 588);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(544, 32);
            this.btnAnalizar.TabIndex = 6;
            this.btnAnalizar.Text = "Analizar";
            this.btnAnalizar.UseVisualStyleBackColor = true;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
            // 
            // dgvTokens
            // 
            this.dgvTokens.AllowUserToAddRows = false;
            this.dgvTokens.AllowUserToDeleteRows = false;
            this.dgvTokens.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvTokens.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTokens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTokens.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No_Token,
            this.C_Simbolo,
            this.C_Precedencia,
            this.C_Asociatividad});
            this.dgvTokens.Location = new System.Drawing.Point(3, 6);
            this.dgvTokens.Name = "dgvTokens";
            this.dgvTokens.ReadOnly = true;
            this.dgvTokens.Size = new System.Drawing.Size(548, 538);
            this.dgvTokens.TabIndex = 7;
            // 
            // No_Token
            // 
            this.No_Token.HeaderText = "Numero Token";
            this.No_Token.Name = "No_Token";
            this.No_Token.ReadOnly = true;
            this.No_Token.Width = 95;
            // 
            // C_Simbolo
            // 
            this.C_Simbolo.HeaderText = "Simbolo";
            this.C_Simbolo.Name = "C_Simbolo";
            this.C_Simbolo.ReadOnly = true;
            this.C_Simbolo.Width = 69;
            // 
            // C_Precedencia
            // 
            this.C_Precedencia.HeaderText = "Precedencia";
            this.C_Precedencia.Name = "C_Precedencia";
            this.C_Precedencia.ReadOnly = true;
            this.C_Precedencia.Width = 92;
            // 
            // C_Asociatividad
            // 
            this.C_Asociatividad.HeaderText = "Asociatividad";
            this.C_Asociatividad.Name = "C_Asociatividad";
            this.C_Asociatividad.ReadOnly = true;
            this.C_Asociatividad.Width = 95;
            // 
            // dgvNoTerminales
            // 
            this.dgvNoTerminales.AllowUserToAddRows = false;
            this.dgvNoTerminales.AllowUserToDeleteRows = false;
            this.dgvNoTerminales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNoTerminales.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvNoTerminales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNoTerminales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CNumero,
            this.CNT,
            this.CFirst,
            this.CProdu});
            this.dgvNoTerminales.Location = new System.Drawing.Point(6, 6);
            this.dgvNoTerminales.Name = "dgvNoTerminales";
            this.dgvNoTerminales.ReadOnly = true;
            this.dgvNoTerminales.Size = new System.Drawing.Size(539, 538);
            this.dgvNoTerminales.TabIndex = 8;
            // 
            // CNumero
            // 
            this.CNumero.HeaderText = "Numero";
            this.CNumero.Name = "CNumero";
            this.CNumero.ReadOnly = true;
            this.CNumero.Width = 69;
            // 
            // CNT
            // 
            this.CNT.HeaderText = "No Terminal";
            this.CNT.Name = "CNT";
            this.CNT.ReadOnly = true;
            this.CNT.Width = 89;
            // 
            // CFirst
            // 
            this.CFirst.HeaderText = "First";
            this.CFirst.Name = "CFirst";
            this.CFirst.ReadOnly = true;
            this.CFirst.Width = 51;
            // 
            // CProdu
            // 
            this.CProdu.HeaderText = "Produccion";
            this.CProdu.Name = "CProdu";
            this.CProdu.ReadOnly = true;
            this.CProdu.Width = 86;
            // 
            // dgvProducciones
            // 
            this.dgvProducciones.AllowUserToAddRows = false;
            this.dgvProducciones.AllowUserToDeleteRows = false;
            this.dgvProducciones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProducciones.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvProducciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CProdu_P,
            this.CLongitud,
            this.CSigueProdu,
            this.CElements});
            this.dgvProducciones.Location = new System.Drawing.Point(3, 6);
            this.dgvProducciones.Name = "dgvProducciones";
            this.dgvProducciones.ReadOnly = true;
            this.dgvProducciones.Size = new System.Drawing.Size(545, 541);
            this.dgvProducciones.TabIndex = 9;
            // 
            // CProdu_P
            // 
            this.CProdu_P.HeaderText = "Produccion";
            this.CProdu_P.Name = "CProdu_P";
            this.CProdu_P.ReadOnly = true;
            this.CProdu_P.Width = 86;
            // 
            // CLongitud
            // 
            this.CLongitud.HeaderText = "Longitud";
            this.CLongitud.Name = "CLongitud";
            this.CLongitud.ReadOnly = true;
            this.CLongitud.Width = 73;
            // 
            // CSigueProdu
            // 
            this.CSigueProdu.HeaderText = "Siguiente Produccion";
            this.CSigueProdu.Name = "CSigueProdu";
            this.CSigueProdu.ReadOnly = true;
            this.CSigueProdu.Width = 122;
            // 
            // CElements
            // 
            this.CElements.HeaderText = "Elementos";
            this.CElements.Name = "CElements";
            this.CElements.ReadOnly = true;
            this.CElements.Width = 81;
            // 
            // picbGuardar
            // 
            this.picbGuardar.Image = global::Proyecto_Compiladores.Properties.Resources.Guardar;
            this.picbGuardar.Location = new System.Drawing.Point(905, 588);
            this.picbGuardar.Name = "picbGuardar";
            this.picbGuardar.Size = new System.Drawing.Size(70, 64);
            this.picbGuardar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbGuardar.TabIndex = 11;
            this.picbGuardar.TabStop = false;
            this.picbGuardar.Click += new System.EventHandler(this.picbGuardar_Click);
            // 
            // pbCargaArchivo
            // 
            this.pbCargaArchivo.Image = global::Proyecto_Compiladores.Properties.Resources.abrirArchivo_4;
            this.pbCargaArchivo.Location = new System.Drawing.Point(544, 12);
            this.pbCargaArchivo.Name = "pbCargaArchivo";
            this.pbCargaArchivo.Size = new System.Drawing.Size(64, 53);
            this.pbCargaArchivo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCargaArchivo.TabIndex = 1;
            this.pbCargaArchivo.TabStop = false;
            this.pbCargaArchivo.Click += new System.EventHandler(this.pbCargaArchivo_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbTokens);
            this.tabControl1.Controls.Add(this.tbNT);
            this.tabControl1.Controls.Add(this.tbProducciones);
            this.tabControl1.Location = new System.Drawing.Point(614, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(559, 576);
            this.tabControl1.TabIndex = 12;
            // 
            // tbTokens
            // 
            this.tbTokens.Controls.Add(this.dgvTokens);
            this.tbTokens.Location = new System.Drawing.Point(4, 22);
            this.tbTokens.Name = "tbTokens";
            this.tbTokens.Padding = new System.Windows.Forms.Padding(3);
            this.tbTokens.Size = new System.Drawing.Size(551, 550);
            this.tbTokens.TabIndex = 0;
            this.tbTokens.Text = "Tokens";
            this.tbTokens.UseVisualStyleBackColor = true;
            // 
            // tbNT
            // 
            this.tbNT.Controls.Add(this.dgvNoTerminales);
            this.tbNT.Location = new System.Drawing.Point(4, 22);
            this.tbNT.Name = "tbNT";
            this.tbNT.Padding = new System.Windows.Forms.Padding(3);
            this.tbNT.Size = new System.Drawing.Size(551, 550);
            this.tbNT.TabIndex = 1;
            this.tbNT.Text = "No Terminales";
            this.tbNT.UseVisualStyleBackColor = true;
            // 
            // tbProducciones
            // 
            this.tbProducciones.Controls.Add(this.dgvProducciones);
            this.tbProducciones.Location = new System.Drawing.Point(4, 22);
            this.tbProducciones.Name = "tbProducciones";
            this.tbProducciones.Size = new System.Drawing.Size(551, 550);
            this.tbProducciones.TabIndex = 2;
            this.tbProducciones.Text = "Producciones";
            this.tbProducciones.UseVisualStyleBackColor = true;
            // 
            // frmCompis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 654);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.picbGuardar);
            this.Controls.Add(this.btnAnalizar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbTituloIngreso);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.pbCargaArchivo);
            this.Controls.Add(this.rtxtTextoArchivo);
            this.Name = "frmCompis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compiladores";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTokens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoTerminales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbGuardar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCargaArchivo)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tbTokens.ResumeLayout(false);
            this.tbNT.ResumeLayout(false);
            this.tbProducciones.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridView dgvTokens;
        private System.Windows.Forms.DataGridView dgvNoTerminales;
        private System.Windows.Forms.DataGridView dgvProducciones;
        private System.Windows.Forms.PictureBox picbGuardar;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Token;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Simbolo;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Precedencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Asociatividad;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbNT;
        private System.Windows.Forms.TabPage tbProducciones;
        private System.Windows.Forms.TabPage tbTokens;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFirst;
        private System.Windows.Forms.DataGridViewTextBoxColumn CProdu;
        private System.Windows.Forms.DataGridViewTextBoxColumn CProdu_P;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSigueProdu;
        private System.Windows.Forms.DataGridViewTextBoxColumn CElements;
    }
}

