
namespace AppBot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.dato = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDetener = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCargarDatos = new System.Windows.Forms.Button();
            this.btnBorrarDatos = new System.Windows.Forms.Button();
            this.lblCantidadRegistros = new System.Windows.Forms.Label();
            this.lblTotalProcesado = new System.Windows.Forms.Label();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.btnAbrirBot = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnExportar = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiposDeDocumentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDatos
            // 
            this.dgvDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dato});
            this.dgvDatos.Location = new System.Drawing.Point(28, 60);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.Size = new System.Drawing.Size(358, 178);
            this.dgvDatos.TabIndex = 1;
            // 
            // dato
            // 
            this.dato.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dato.HeaderText = "Datos";
            this.dato.Name = "dato";
            this.dato.Width = 60;
            // 
            // btnDetener
            // 
            this.btnDetener.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDetener.Enabled = false;
            this.btnDetener.Location = new System.Drawing.Point(179, 257);
            this.btnDetener.Name = "btnDetener";
            this.btnDetener.Size = new System.Drawing.Size(69, 23);
            this.btnDetener.TabIndex = 2;
            this.btnDetener.Text = "Detener";
            this.btnDetener.UseVisualStyleBackColor = true;
            this.btnDetener.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Resultado";
            // 
            // btnCargarDatos
            // 
            this.btnCargarDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCargarDatos.Location = new System.Drawing.Point(222, 18);
            this.btnCargarDatos.Name = "btnCargarDatos";
            this.btnCargarDatos.Size = new System.Drawing.Size(79, 23);
            this.btnCargarDatos.TabIndex = 10;
            this.btnCargarDatos.Text = "Cargar datos";
            this.btnCargarDatos.UseVisualStyleBackColor = true;
            this.btnCargarDatos.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnBorrarDatos
            // 
            this.btnBorrarDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBorrarDatos.Location = new System.Drawing.Point(307, 18);
            this.btnBorrarDatos.Name = "btnBorrarDatos";
            this.btnBorrarDatos.Size = new System.Drawing.Size(79, 23);
            this.btnBorrarDatos.TabIndex = 11;
            this.btnBorrarDatos.Text = "Borrar datos";
            this.btnBorrarDatos.UseVisualStyleBackColor = true;
            this.btnBorrarDatos.Click += new System.EventHandler(this.button8_Click);
            // 
            // lblCantidadRegistros
            // 
            this.lblCantidadRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCantidadRegistros.AutoSize = true;
            this.lblCantidadRegistros.Location = new System.Drawing.Point(285, 386);
            this.lblCantidadRegistros.Name = "lblCantidadRegistros";
            this.lblCantidadRegistros.Size = new System.Drawing.Size(10, 13);
            this.lblCantidadRegistros.TabIndex = 12;
            this.lblCantidadRegistros.Text = ".";
            // 
            // lblTotalProcesado
            // 
            this.lblTotalProcesado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalProcesado.AutoSize = true;
            this.lblTotalProcesado.Location = new System.Drawing.Point(421, 386);
            this.lblTotalProcesado.Name = "lblTotalProcesado";
            this.lblTotalProcesado.Size = new System.Drawing.Size(10, 13);
            this.lblTotalProcesado.TabIndex = 13;
            this.lblTotalProcesado.Text = ".";
            // 
            // btnProcesar
            // 
            this.btnProcesar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProcesar.Enabled = false;
            this.btnProcesar.Location = new System.Drawing.Point(104, 257);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(69, 23);
            this.btnProcesar.TabIndex = 15;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnAbrirBot
            // 
            this.btnAbrirBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbrirBot.Location = new System.Drawing.Point(29, 257);
            this.btnAbrirBot.Name = "btnAbrirBot";
            this.btnAbrirBot.Size = new System.Drawing.Size(69, 23);
            this.btnAbrirBot.TabIndex = 16;
            this.btnAbrirBot.Text = "Abrir bot";
            this.btnAbrirBot.UseVisualStyleBackColor = true;
            this.btnAbrirBot.Click += new System.EventHandler(this.button9_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(360, 409);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(0, 13);
            this.lblEstado.TabIndex = 20;
            // 
            // txtResultado
            // 
            this.txtResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResultado.Location = new System.Drawing.Point(19, 42);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.Size = new System.Drawing.Size(386, 257);
            this.txtResultado.TabIndex = 21;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(14, 41);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(433, 333);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnExportar);
            this.tabPage1.Controls.Add(this.dgvDatos);
            this.tabPage1.Controls.Add(this.btnBorrarDatos);
            this.tabPage1.Controls.Add(this.btnAbrirBot);
            this.tabPage1.Controls.Add(this.btnProcesar);
            this.tabPage1.Controls.Add(this.btnCargarDatos);
            this.tabPage1.Controls.Add(this.btnDetener);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(425, 307);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Procesamiento";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportar.Location = new System.Drawing.Point(311, 257);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(75, 23);
            this.btnExportar.TabIndex = 17;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtResultado);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(425, 322);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 381);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Procesados";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 386);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Total";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(402, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Version 1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Lavender;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(465, 24);
            this.menuStrip1.TabIndex = 26;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiposDeDocumentosToolStripMenuItem});
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.configuracionToolStripMenuItem.Text = "Configuracion";
            // 
            // tiposDeDocumentosToolStripMenuItem
            // 
            this.tiposDeDocumentosToolStripMenuItem.Name = "tiposDeDocumentosToolStripMenuItem";
            this.tiposDeDocumentosToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.tiposDeDocumentosToolStripMenuItem.Text = "Tipos de documentos";
            this.tiposDeDocumentosToolStripMenuItem.Click += new System.EventHandler(this.tiposDeDocumentosToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(465, 424);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.lblTotalProcesado);
            this.Controls.Add(this.lblCantidadRegistros);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Bot Runt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button btnDetener;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCargarDatos;
        private System.Windows.Forms.Button btnBorrarDatos;
        private System.Windows.Forms.Label lblCantidadRegistros;
        private System.Windows.Forms.Label lblTotalProcesado;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Button btnAbrirBot;
        private System.Windows.Forms.DataGridViewTextBoxColumn dato;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiposDeDocumentosToolStripMenuItem;
    }
}

