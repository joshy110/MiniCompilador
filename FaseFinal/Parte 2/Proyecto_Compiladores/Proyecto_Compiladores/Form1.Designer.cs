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
            this.label1 = new System.Windows.Forms.Label();
            this.OFDLink = new System.Windows.Forms.OpenFileDialog();
            this.btnAnalizar = new System.Windows.Forms.Button();
            this.dgvProducciones = new System.Windows.Forms.DataGridView();
            this.CProdu_P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLongitud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CSigueProdu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CElements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picbGuardar = new System.Windows.Forms.PictureBox();
            this.pbCargaArchivo = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tb_TXT_Tokens = new System.Windows.Forms.TabPage();
            this.dgvTokens = new System.Windows.Forms.DataGridView();
            this.No_Token = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Simbolo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Precedencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Asociatividad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbNT_Produ = new System.Windows.Forms.TabPage();
            this.dgvNoTerminales = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tb_Estados = new System.Windows.Forms.TabPage();
            this.richtxt_Estados = new System.Windows.Forms.RichTextBox();
            this.tbTablaParser = new System.Windows.Forms.TabPage();
            this.dgv_Tabla_Parseo = new System.Windows.Forms.DataGridView();
            this.tbParseo = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAnalizarExpre = new System.Windows.Forms.Button();
            this.richtxtAnalizarExpre = new System.Windows.Forms.RichTextBox();
            this.btn_Carga_Expression = new System.Windows.Forms.Button();
            this.richtxtExpres = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbGuardar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCargaArchivo)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tb_TXT_Tokens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTokens)).BeginInit();
            this.tbNT_Produ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoTerminales)).BeginInit();
            this.tb_Estados.SuspendLayout();
            this.tbTablaParser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Tabla_Parseo)).BeginInit();
            this.tbParseo.SuspendLayout();
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
            this.rtxtTextoArchivo.Location = new System.Drawing.Point(3, 3);
            this.rtxtTextoArchivo.Name = "rtxtTextoArchivo";
            this.rtxtTextoArchivo.Size = new System.Drawing.Size(596, 544);
            this.rtxtTextoArchivo.TabIndex = 0;
            this.rtxtTextoArchivo.Text = "";
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
            this.btnAnalizar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAnalizar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAnalizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnAnalizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnAnalizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalizar.Location = new System.Drawing.Point(639, 26);
            this.btnAnalizar.Name = "btnAnalizar";
            this.btnAnalizar.Size = new System.Drawing.Size(544, 26);
            this.btnAnalizar.TabIndex = 6;
            this.btnAnalizar.Text = "Analizar";
            this.btnAnalizar.UseVisualStyleBackColor = false;
            this.btnAnalizar.Click += new System.EventHandler(this.btnAnalizar_Click);
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
            this.dgvProducciones.Location = new System.Drawing.Point(578, 0);
            this.dgvProducciones.Name = "dgvProducciones";
            this.dgvProducciones.ReadOnly = true;
            this.dgvProducciones.Size = new System.Drawing.Size(582, 541);
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
            this.picbGuardar.Location = new System.Drawing.Point(573, 639);
            this.picbGuardar.Name = "picbGuardar";
            this.picbGuardar.Size = new System.Drawing.Size(50, 52);
            this.picbGuardar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbGuardar.TabIndex = 11;
            this.picbGuardar.TabStop = false;
            this.picbGuardar.Click += new System.EventHandler(this.picbGuardar_Click);
            // 
            // pbCargaArchivo
            // 
            this.pbCargaArchivo.Image = global::Proyecto_Compiladores.Properties.Resources.abrirArchivo_4;
            this.pbCargaArchivo.Location = new System.Drawing.Point(549, 9);
            this.pbCargaArchivo.Name = "pbCargaArchivo";
            this.pbCargaArchivo.Size = new System.Drawing.Size(60, 46);
            this.pbCargaArchivo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCargaArchivo.TabIndex = 1;
            this.pbCargaArchivo.TabStop = false;
            this.pbCargaArchivo.Click += new System.EventHandler(this.pbCargaArchivo_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tb_TXT_Tokens);
            this.tabControl1.Controls.Add(this.tbNT_Produ);
            this.tabControl1.Controls.Add(this.tb_Estados);
            this.tabControl1.Controls.Add(this.tbTablaParser);
            this.tabControl1.Controls.Add(this.tbParseo);
            this.tabControl1.Location = new System.Drawing.Point(12, 59);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1171, 576);
            this.tabControl1.TabIndex = 12;
            // 
            // tb_TXT_Tokens
            // 
            this.tb_TXT_Tokens.BackColor = System.Drawing.Color.DarkOrange;
            this.tb_TXT_Tokens.Controls.Add(this.dgvTokens);
            this.tb_TXT_Tokens.Controls.Add(this.rtxtTextoArchivo);
            this.tb_TXT_Tokens.Location = new System.Drawing.Point(4, 22);
            this.tb_TXT_Tokens.Name = "tb_TXT_Tokens";
            this.tb_TXT_Tokens.Size = new System.Drawing.Size(1163, 550);
            this.tb_TXT_Tokens.TabIndex = 4;
            this.tb_TXT_Tokens.Text = "Texto Ingresado y Tokens";
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
            this.dgvTokens.Location = new System.Drawing.Point(605, 3);
            this.dgvTokens.Name = "dgvTokens";
            this.dgvTokens.ReadOnly = true;
            this.dgvTokens.Size = new System.Drawing.Size(548, 538);
            this.dgvTokens.TabIndex = 8;
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
            // tbNT_Produ
            // 
            this.tbNT_Produ.BackColor = System.Drawing.Color.DarkOrange;
            this.tbNT_Produ.Controls.Add(this.dgvNoTerminales);
            this.tbNT_Produ.Controls.Add(this.dgvProducciones);
            this.tbNT_Produ.Location = new System.Drawing.Point(4, 22);
            this.tbNT_Produ.Name = "tbNT_Produ";
            this.tbNT_Produ.Size = new System.Drawing.Size(1163, 550);
            this.tbNT_Produ.TabIndex = 2;
            this.tbNT_Produ.Text = "No Terminales y Producciones";
            // 
            // dgvNoTerminales
            // 
            this.dgvNoTerminales.AllowUserToAddRows = false;
            this.dgvNoTerminales.AllowUserToDeleteRows = false;
            this.dgvNoTerminales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvNoTerminales.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvNoTerminales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNoTerminales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dgvNoTerminales.Location = new System.Drawing.Point(3, 3);
            this.dgvNoTerminales.Name = "dgvNoTerminales";
            this.dgvNoTerminales.ReadOnly = true;
            this.dgvNoTerminales.Size = new System.Drawing.Size(569, 538);
            this.dgvNoTerminales.TabIndex = 10;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Numero";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 69;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "No Terminal";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 89;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "First";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 51;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Produccion";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 86;
            // 
            // tb_Estados
            // 
            this.tb_Estados.BackColor = System.Drawing.Color.DarkOrange;
            this.tb_Estados.Controls.Add(this.richtxt_Estados);
            this.tb_Estados.Location = new System.Drawing.Point(4, 22);
            this.tb_Estados.Name = "tb_Estados";
            this.tb_Estados.Size = new System.Drawing.Size(1163, 550);
            this.tb_Estados.TabIndex = 3;
            this.tb_Estados.Text = "Tabla_Estados";
            // 
            // richtxt_Estados
            // 
            this.richtxt_Estados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richtxt_Estados.Location = new System.Drawing.Point(3, 3);
            this.richtxt_Estados.Name = "richtxt_Estados";
            this.richtxt_Estados.Size = new System.Drawing.Size(1157, 544);
            this.richtxt_Estados.TabIndex = 0;
            this.richtxt_Estados.Text = "";
            // 
            // tbTablaParser
            // 
            this.tbTablaParser.BackColor = System.Drawing.Color.DarkOrange;
            this.tbTablaParser.Controls.Add(this.dgv_Tabla_Parseo);
            this.tbTablaParser.Location = new System.Drawing.Point(4, 22);
            this.tbTablaParser.Name = "tbTablaParser";
            this.tbTablaParser.Size = new System.Drawing.Size(1163, 550);
            this.tbTablaParser.TabIndex = 5;
            this.tbTablaParser.Text = "Tabla Parser";
            // 
            // dgv_Tabla_Parseo
            // 
            this.dgv_Tabla_Parseo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Tabla_Parseo.Location = new System.Drawing.Point(3, 3);
            this.dgv_Tabla_Parseo.Name = "dgv_Tabla_Parseo";
            this.dgv_Tabla_Parseo.Size = new System.Drawing.Size(1157, 544);
            this.dgv_Tabla_Parseo.TabIndex = 0;
            // 
            // tbParseo
            // 
            this.tbParseo.BackColor = System.Drawing.Color.Peru;
            this.tbParseo.Controls.Add(this.label3);
            this.tbParseo.Controls.Add(this.label2);
            this.tbParseo.Controls.Add(this.btnAnalizarExpre);
            this.tbParseo.Controls.Add(this.richtxtAnalizarExpre);
            this.tbParseo.Controls.Add(this.btn_Carga_Expression);
            this.tbParseo.Controls.Add(this.richtxtExpres);
            this.tbParseo.Location = new System.Drawing.Point(4, 22);
            this.tbParseo.Name = "tbParseo";
            this.tbParseo.Size = new System.Drawing.Size(1163, 550);
            this.tbParseo.TabIndex = 6;
            this.tbParseo.Text = "Parseo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(8, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Expresión a Analizar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(8, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Parseo";
            // 
            // btnAnalizarExpre
            // 
            this.btnAnalizarExpre.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAnalizarExpre.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAnalizarExpre.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnAnalizarExpre.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnAnalizarExpre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnalizarExpre.Location = new System.Drawing.Point(278, 521);
            this.btnAnalizarExpre.Name = "btnAnalizarExpre";
            this.btnAnalizarExpre.Size = new System.Drawing.Size(540, 26);
            this.btnAnalizarExpre.TabIndex = 13;
            this.btnAnalizarExpre.Text = "Analizar Expresion";
            this.btnAnalizarExpre.UseVisualStyleBackColor = false;
            this.btnAnalizarExpre.Click += new System.EventHandler(this.btnAnalizarExpre_Click_1);
            // 
            // richtxtAnalizarExpre
            // 
            this.richtxtAnalizarExpre.Location = new System.Drawing.Point(3, 300);
            this.richtxtAnalizarExpre.Name = "richtxtAnalizarExpre";
            this.richtxtAnalizarExpre.Size = new System.Drawing.Size(1157, 216);
            this.richtxtAnalizarExpre.TabIndex = 12;
            this.richtxtAnalizarExpre.Text = "";
            this.richtxtAnalizarExpre.TextChanged += new System.EventHandler(this.richtxtAnalizarExpre_TextChanged);
            // 
            // btn_Carga_Expression
            // 
            this.btn_Carga_Expression.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Carga_Expression.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Carga_Expression.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btn_Carga_Expression.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn_Carga_Expression.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Carga_Expression.Location = new System.Drawing.Point(278, 253);
            this.btn_Carga_Expression.Name = "btn_Carga_Expression";
            this.btn_Carga_Expression.Size = new System.Drawing.Size(540, 28);
            this.btn_Carga_Expression.TabIndex = 11;
            this.btn_Carga_Expression.Text = "Cargar Expresion";
            this.btn_Carga_Expression.UseVisualStyleBackColor = false;
            this.btn_Carga_Expression.Click += new System.EventHandler(this.btn_Carga_Expression_Click_1);
            // 
            // richtxtExpres
            // 
            this.richtxtExpres.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richtxtExpres.Location = new System.Drawing.Point(3, 30);
            this.richtxtExpres.Name = "richtxtExpres";
            this.richtxtExpres.Size = new System.Drawing.Size(1157, 217);
            this.richtxtExpres.TabIndex = 10;
            this.richtxtExpres.Text = "";
            // 
            // frmCompis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(1195, 697);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.picbGuardar);
            this.Controls.Add(this.btnAnalizar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRuta);
            this.Controls.Add(this.pbCargaArchivo);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "frmCompis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compiladores";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picbGuardar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCargaArchivo)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tb_TXT_Tokens.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTokens)).EndInit();
            this.tbNT_Produ.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoTerminales)).EndInit();
            this.tb_Estados.ResumeLayout(false);
            this.tbTablaParser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Tabla_Parseo)).EndInit();
            this.tbParseo.ResumeLayout(false);
            this.tbParseo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbCargaArchivo;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.RichTextBox rtxtTextoArchivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog OFDLink;
        private System.Windows.Forms.Button btnAnalizar;
        private System.Windows.Forms.DataGridView dgvProducciones;
        private System.Windows.Forms.PictureBox picbGuardar;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbNT_Produ;
        private System.Windows.Forms.DataGridViewTextBoxColumn CProdu_P;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLongitud;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSigueProdu;
        private System.Windows.Forms.DataGridViewTextBoxColumn CElements;
        private System.Windows.Forms.TabPage tb_Estados;
        private System.Windows.Forms.RichTextBox richtxt_Estados;
        private System.Windows.Forms.TabPage tb_TXT_Tokens;
        private System.Windows.Forms.DataGridView dgvNoTerminales;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridView dgvTokens;
        private System.Windows.Forms.DataGridViewTextBoxColumn No_Token;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Simbolo;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Precedencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Asociatividad;
        private System.Windows.Forms.TabPage tbTablaParser;
        private System.Windows.Forms.DataGridView dgv_Tabla_Parseo;
        private System.Windows.Forms.TabPage tbParseo;
        private System.Windows.Forms.Button btnAnalizarExpre;
        private System.Windows.Forms.RichTextBox richtxtAnalizarExpre;
        private System.Windows.Forms.Button btn_Carga_Expression;
        private System.Windows.Forms.RichTextBox richtxtExpres;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

