using Newtonsoft.Json;
using SB.NET.eFlex.SDKLib;
using SB.NET.eFlex.SDKLib.Comprobantes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KRIKOS360_INALPA
{
    public partial class FrmLogin : Form
    {
        EFlexSDK_TokenValidacion pToken = null;
        DialogResult Resp;
        public FrmLogin()
        {
            InitializeComponent();
            TxbUsuario.Select();
            pToken = new EFlexSDK_TokenValidacion();
            Resp = DialogResult.OK;
            try
            {
                string filejsonEmpOrig = File.ReadAllText(@"C:\EmpresasOrig.json");
                // Posicion de DataTable de Parametros DT[0] Nombre de la empresa - [1] Codigo de la Empresa - [2] Puesto de trabajo - [3] Punto de Venta  
                DataTable dtEmpresaOrig = (DataTable)JsonConvert.DeserializeObject(value: filejsonEmpOrig, type: typeof(DataTable));
                cbxEmpresasOrig.DataSource = dtEmpresaOrig;
                cbxEmpresasOrig.DisplayMember = "NomEmpresa";
            }
            catch
            {
                Resp = DialogResult.Abort;
                MessageBox.Show("Cotrolar la Ubicación del Archivo de Configuración EmpresasOrig.json");
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
        }

        private void FrmIngresar_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(TxbUsuario.Text)))
            {
                try
                {
                    pToken = EFlexSDK_Procesos.ProcesosFlex.IniciarProcesoFlex(TxbUsuario.Text, TxbContraseña.Text);
                    DataRowView emp = (DataRowView)cbxEmpresasOrig.SelectedItem;
                    this.Hide();
                    FrmModulo frmModulo = new FrmModulo(pToken, emp);
                    DialogResult result = frmModulo.ShowDialog();
                    if (result == DialogResult.Cancel)
                    {
                        Close();
                    }
                }
                catch (EFlexSDK_Exception ex)
                {
                    Resp = DialogResult.Abort;
                    MessageBox.Show(ex.ToString());
                }
                if (Resp == DialogResult.Abort)
                {
                    TxbContraseña.Text = "";
                    TxbUsuario.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña Incorrecta");
            }
        }

        private void FrmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
