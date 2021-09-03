using AppBotVUR.Modelos;
using AppBotVUR.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppBotVUR
{
    public partial class FrmApiCaptcha : Form
    {
        public FrmApiCaptcha()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            salvarConfiguraciones();
        }

        private void getConfiguraciones()
        {
            try
            {
                textBox1.Text = Utiles.obtenerJSONConfiguraciones().apiKey;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void salvarConfiguraciones()
        {
            try
            {
                var config = Utiles.obtenerJSONConfiguraciones();
                config.apiKey = textBox1.Text;
                Utilidades.Utiles.salvarJSONConfiguraciones(config);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmApiCaptcha_Load(object sender, EventArgs e)
        {
            getConfiguraciones();
        }
    }
}
