
namespace AppBotVUR
{
    partial class FrmTipoDocumentos
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTiposDocumentos = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tipo de documentos";
            // 
            // txtTiposDocumentos
            // 
            this.txtTiposDocumentos.Enabled = false;
            this.txtTiposDocumentos.Location = new System.Drawing.Point(40, 51);
            this.txtTiposDocumentos.Multiline = true;
            this.txtTiposDocumentos.Name = "txtTiposDocumentos";
            this.txtTiposDocumentos.Size = new System.Drawing.Size(245, 100);
            this.txtTiposDocumentos.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(149, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Copiar al portapapeles";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmTipoDocumentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 205);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtTiposDocumentos);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "FrmTipoDocumentos";
            this.Text = "Tipo de documentos";
            this.Load += new System.EventHandler(this.FrmTipoDocumentos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTiposDocumentos;
        private System.Windows.Forms.Button button1;
    }
}