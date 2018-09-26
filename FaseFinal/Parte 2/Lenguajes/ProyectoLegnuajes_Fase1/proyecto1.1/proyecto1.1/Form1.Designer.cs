namespace Proyecto1._1
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
            this.pbSearch = new System.Windows.Forms.PictureBox();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.lbPath = new System.Windows.Forms.Label();
            this.btEnviar = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.analizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pDatos = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbNodos = new System.Windows.Forms.TabPage();
            this.dgNodos = new System.Windows.Forms.DataGridView();
            this.C1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIRST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NULLABLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpFollow = new System.Windows.Forms.TabPage();
            this.dgFollow = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Follow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sfdExportar = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.pDatos.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbNodos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNodos)).BeginInit();
            this.tpFollow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFollow)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdBuscador
            // 
            this.ofdBuscador.FileName = "ofdBuscador";
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
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(23, 36);
            this.tbPath.Multiline = true;
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(280, 30);
            this.tbPath.TabIndex = 6;
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analizarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1020, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // analizarToolStripMenuItem
            // 
            this.analizarToolStripMenuItem.Name = "analizarToolStripMenuItem";
            this.analizarToolStripMenuItem.Size = new System.Drawing.Size(127, 20);
            this.analizarToolStripMenuItem.Text = "Selector De Archivos";
            this.analizarToolStripMenuItem.Click += new System.EventHandler(this.analizarToolStripMenuItem_Click);
            // 
            // pDatos
            // 
            this.pDatos.Controls.Add(this.tbPath);
            this.pDatos.Controls.Add(this.btEnviar);
            this.pDatos.Controls.Add(this.pbSearch);
            this.pDatos.Controls.Add(this.lbPath);
            this.pDatos.Location = new System.Drawing.Point(0, 27);
            this.pDatos.Name = "pDatos";
            this.pDatos.Size = new System.Drawing.Size(361, 145);
            this.pDatos.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbNodos);
            this.tabControl1.Controls.Add(this.tpFollow);
            this.tabControl1.Location = new System.Drawing.Point(367, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(653, 372);
            this.tabControl1.TabIndex = 0;
            // 
            // tbNodos
            // 
            this.tbNodos.Controls.Add(this.dgNodos);
            this.tbNodos.Location = new System.Drawing.Point(4, 22);
            this.tbNodos.Name = "tbNodos";
            this.tbNodos.Padding = new System.Windows.Forms.Padding(3);
            this.tbNodos.Size = new System.Drawing.Size(645, 346);
            this.tbNodos.TabIndex = 0;
            this.tbNodos.Text = "Análisis de Nodos";
            this.tbNodos.UseVisualStyleBackColor = true;
            // 
            // dgNodos
            // 
            this.dgNodos.AllowUserToAddRows = false;
            this.dgNodos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNodos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.C1,
            this.Operador,
            this.C2,
            this.FIRST,
            this.LAST,
            this.NULLABLE});
            this.dgNodos.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgNodos.EnableHeadersVisualStyles = false;
            this.dgNodos.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dgNodos.Location = new System.Drawing.Point(6, 6);
            this.dgNodos.Name = "dgNodos";
            this.dgNodos.ReadOnly = true;
            this.dgNodos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgNodos.Size = new System.Drawing.Size(634, 334);
            this.dgNodos.TabIndex = 0;
            // 
            // C1
            // 
            this.C1.HeaderText = "C1";
            this.C1.Name = "C1";
            this.C1.ReadOnly = true;
            // 
            // Operador
            // 
            this.Operador.HeaderText = "OPERADOR";
            this.Operador.Name = "Operador";
            this.Operador.ReadOnly = true;
            this.Operador.Width = 90;
            // 
            // C2
            // 
            this.C2.HeaderText = "C2";
            this.C2.Name = "C2";
            this.C2.ReadOnly = true;
            // 
            // FIRST
            // 
            this.FIRST.HeaderText = "FIRST";
            this.FIRST.Name = "FIRST";
            this.FIRST.ReadOnly = true;
            // 
            // LAST
            // 
            this.LAST.HeaderText = "LAST";
            this.LAST.Name = "LAST";
            this.LAST.ReadOnly = true;
            // 
            // NULLABLE
            // 
            this.NULLABLE.HeaderText = "NULLABLE";
            this.NULLABLE.Name = "NULLABLE";
            this.NULLABLE.ReadOnly = true;
            // 
            // tpFollow
            // 
            this.tpFollow.Controls.Add(this.dgFollow);
            this.tpFollow.Location = new System.Drawing.Point(4, 22);
            this.tpFollow.Name = "tpFollow";
            this.tpFollow.Padding = new System.Windows.Forms.Padding(3);
            this.tpFollow.Size = new System.Drawing.Size(645, 346);
            this.tpFollow.TabIndex = 1;
            this.tpFollow.Text = "Follow";
            this.tpFollow.UseVisualStyleBackColor = true;
            // 
            // dgFollow
            // 
            this.dgFollow.AllowUserToAddRows = false;
            this.dgFollow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFollow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Follow});
            this.dgFollow.Location = new System.Drawing.Point(106, 51);
            this.dgFollow.Name = "dgFollow";
            this.dgFollow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFollow.Size = new System.Drawing.Size(444, 233);
            this.dgFollow.TabIndex = 0;
            // 
            // No
            // 
            this.No.HeaderText = "No.";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // Follow
            // 
            this.Follow.HeaderText = "Follow";
            this.Follow.Name = "Follow";
            this.Follow.ReadOnly = true;
            this.Follow.Width = 300;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 408);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pDatos);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analizador de Sintáxis";
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pDatos.ResumeLayout(false);
            this.pDatos.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbNodos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgNodos)).EndInit();
            this.tpFollow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFollow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog ofdBuscador;
        private System.Windows.Forms.PictureBox pbSearch;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.Button btEnviar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem analizarToolStripMenuItem;
        private System.Windows.Forms.Panel pDatos;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbNodos;
        private System.Windows.Forms.TabPage tpFollow;
        private System.Windows.Forms.DataGridView dgFollow;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Follow;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operador;
        private System.Windows.Forms.DataGridViewTextBoxColumn C2;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIRST;
        private System.Windows.Forms.DataGridViewTextBoxColumn LAST;
        private System.Windows.Forms.DataGridViewTextBoxColumn NULLABLE;
        private System.Windows.Forms.DataGridView dgNodos;
        private System.Windows.Forms.SaveFileDialog sfdExportar;
    }
}

