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
    public partial class FrmTipoDocumentos : Form
    {
        public FrmTipoDocumentos()
        {
            InitializeComponent();
        }

        private void FrmTipoDocumentos_Load(object sender, EventArgs e)
        {
            obtenerTiposDocumentos();
        }

        private void obtenerTiposDocumentos()
        {
            String resultado = "";

            foreach (var item in Parametros.tiposIdentificacion)
            {
                resultado += item.Key + Environment.NewLine;
            }

            txtTiposDocumentos.Text = resultado;
        }
    }
}
