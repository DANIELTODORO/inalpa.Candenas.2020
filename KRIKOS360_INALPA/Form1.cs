using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using SB.NET.eFlex.SDKLib;
using SB.NET.eFlex.SDKLib.Mappers;
using SB.NET.eFlex.SDKLib.Comprobantes;
using SB.Sistemas.eFlexware;
using SB.NET.eFlex.SDKLib.Tablas;

namespace KRIKOS360_INALPA
{
    public partial class Form1 : Form
    {
        string sResultado = "";
        EFlexSDK_TokenValidacion pToken = null;
        List<EFlexSDK_ComprobanteVentas> sComprobantes = null;
        DialogResult Resp;
        SBDABEJERMAN oSBDABEJERMAN;
        public Form1()
        {
            InitializeComponent();
            DtpDesde.Value = DtpDesde.MinDate;
            DtpHasta.Value = DtpHasta.MinDate;
            oSBDABEJERMAN = SBDABEJERMAN.obtenerinstancia();
            pToken = new EFlexSDK_TokenValidacion();
            Resp = DialogResult.OK;
            try
            {
                string filejsonEmpOrig = File.ReadAllText(@".\EmpresasOrig.json");
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
            try
            {
                //DatosFlexware datosFlexware = new DatosFlexware();
                //EmpresaFlex empresaFlex = new EmpresaFlex();
                //SB.RutinasDeBase.cStrConnection conex = new SB.RutinasDeBase.cStrConnection();
                //conex.Server = "LOCALHOST";
                //conex.PWD = "Sa.2017";
                //conex.AppName = "sa";
                //conex.DB = "sbdamode";
                //conex.UID = "sa";
                //empresaFlex.Codigo = "MODE";
                //datosFlexware.Empresa = empresaFlex;
                //datosFlexware.PuestoTrabajo = "1";
                //datosFlexware.SucursalCodigo = "";
                //datosFlexware.StrConex = conex;
                
                //var dd = ParamModulo.LeerValor(datosFlexware, "RTOELEC", "URLRENTAS", "", false);
            }
            catch
            {
                Resp = DialogResult.Abort;
                MessageBox.Show("No se pudo conectar con el Servidor");
            }
            try
            {
                 pToken = EFlexSDK_Procesos.ProcesosFlex.IniciarProcesoFlex("CNV", "gp74");
            }
            catch (EFlexSDK_Exception ex)
            {
                Resp = DialogResult.Abort;
                MessageBox.Show(ex.ToString());
            }
            if (Resp == DialogResult.Abort)
            {
                Application.Exit();
            }

        }  

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string patron = @"[^\w]";
            Regex regex = new Regex(patron);
            saveFileDialog1.Title = "Guardar Archivo KRIKOS 360 (.TXT)";
            saveFileDialog1.Filter = "TXT files (*.TXT)|*.TXT";
            saveFileDialog1.DefaultExt = "TXT";

            DialogResult result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                StringBuilder csvMemoria = new StringBuilder();

                int Registro = sComprobantes.Count();
                if (Registro != 0)
                {
                    foreach (EFlexSDK_ComprobanteVentas krikos360_010 in sComprobantes)
                    {
                        List<EFlexSDK_DatAdic> eFlexSDK_DatAdic = new List<EFlexSDK_DatAdic>();
                        eFlexSDK_DatAdic = krikos360_010.Comprobante_DatosAdicionales;
                        //EFlexSDK_DatAdic Dscv_GLN = new EFlexSDK_DatAdic();
                        //Dscv_GLN.Nombre = "Dscv_GLN";
                        //Dscv_GLN.Valor = "Prueba";
                        //eFlexSDK_DatAdic.Add(Dscv_GLN);

                        csvMemoria.Append(String.Format("{0};", "010")); // Bloque 010
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_Tipo)); // 01001
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_Numero).PadLeft(8, '0')); // 01002
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_Numero).PadLeft(8, '0')); // 01003
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_PtoVenta.PadLeft(5, '0'))); // 01004
                        csvMemoria.Append(String.Format("{0};", (krikos360_010.Comprobante_FechaEmision).ToString("yyyyMMdd", CultureInfo.InvariantCulture))); // 01005
                        if (krikos360_010.Comprobante_FechaVencimiento.Value.Date != null)
                        {
                            csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_FechaVencimiento.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture)));
                        }
                        else
                        {
                            csvMemoria.Append(String.Format("{0};", "        "));
                        }  // 01006
                        csvMemoria.Append(String.Format("{0};", "")); // 01007
                        csvMemoria.Append(String.Format("{0};", "")); // 01008
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_CondVenta)); //01009
                        if (krikos360_010.Comprobante_NumeroCAI != null) // Desde 01010
                        {
                            csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_NumeroCAI.PadLeft(14, ' ')));
                        }
                        else
                        {
                            csvMemoria.Append(String.Format("{0};", "".PadLeft(14, ' ')));
                        }// Hasta 01010
                        if (krikos360_010.Comprobante_FechaVencimientoCAI != null) // Desde 01011
                        {
                            csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_FechaVencimientoCAI.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture)));
                        }
                        else
                        {
                            csvMemoria.Append(String.Format("{0};", "        "));
                        } // Hasta 01011
                        csvMemoria.Append(String.Format("{0};", "")); //01012
                        csvMemoria.Append(String.Format("{0};", "")); //01013
                        csvMemoria.Append(String.Format("{0};", "")); //01014
                        csvMemoria.Append(String.Format("{0};", "")); //01015
                        csvMemoria.Append(String.Format("{0};", "")); //01016
                        csvMemoria.Append(String.Format("{0};", "")); //01017
                        csvMemoria.Append(String.Format("{0};", "")); //01018
                        csvMemoria.Append(String.Format("{0};", "")); //01019
                        csvMemoria.Append(String.Format("{0};", "")); //01020
                        csvMemoria.Append(String.Format("{0};", "")); //01021
                        csvMemoria.Append(String.Format("{0};", "")); //01022
                        csvMemoria.Append(String.Format("{0};", "")); //01023
                        if (krikos360_010.Comprobante_FechaEntrega != null) // Desde 01024
                        {
                            csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_FechaEntrega.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture)));
                        }
                        else
                        {
                            csvMemoria.Append(String.Format("{0};", "        "));
                        } // Hasta 01024
                        csvMemoria.Append(String.Format("{0}", ""));  //01025


                        csvMemoria.AppendLine();

                        //Bloque 020: Comprobamte de Referencia 

                        foreach (KrikosComprobantesAsoc_Result krikos360_020 in oSBDABEJERMAN.KrikosComprobantesAsoc(krikos360_010.Comprobante_ID))
                        {

                            if (krikos360_020.spvtco_TipoFijo == "RT")
                            {
                                csvMemoria.Append(String.Format("{0};", "020"));
                                csvMemoria.Append(String.Format("{0};", "RE"));
                                csvMemoria.Append(String.Format("{0};", krikos360_020.spv_CodPvt));
                                csvMemoria.Append(String.Format("{0};", krikos360_020.spv_Nro));
                                csvMemoria.Append(String.Format("{0};", ""));
                                csvMemoria.Append(String.Format("{0}", ""));
                                csvMemoria.AppendLine();
                            }
                            if (krikos360_020.spvtco_TipoFijo == "NP")
                            {
                                csvMemoria.Append(String.Format("{0};", "020"));
                                csvMemoria.Append(String.Format("{0};", "PC"));
                                csvMemoria.Append(String.Format("{0};", krikos360_020.spv_CodPvt));
                                csvMemoria.Append(String.Format("{0};", krikos360_020.spv_Nro));
                                csvMemoria.Append(String.Format("{0};", ""));
                                csvMemoria.Append(String.Format("{0}", ""));
                                csvMemoria.AppendLine();
                            }
                        }


                        //Bloque Identificacion del Emisor de la Factura

                            
                            csvMemoria.Append(String.Format("{0};", "030"));
                            csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_Empresa));
                            if(eFlexSDK_DatAdic != null)
                            {
                                csvMemoria.Append(String.Format("{0};", eFlexSDK_DatAdic.FirstOrDefault().Valor.ToString())); 
                            }
                            else
                            {
                                csvMemoria.Append(String.Format("{0};", ""));
                            }
                            
                            csvMemoria.Append(String.Format("{0};", "")); //siviva
                            //csvMemoria.Append(String.Format("{0};", krikos360_030.ead_NroIB));
                            //csvMemoria.Append(String.Format("{0};", krikos360_030.pvt_FIniAct.ToString("yyyyMMdd", CultureInfo.InvariantCulture)));
                            //csvMemoria.Append(String.Format("{0};", ""));
                            //csvMemoria.Append(String.Format("{0};", krikos360_030.pvt_Direc));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", "PAVON ARRIBA"));
                            //csvMemoria.Append(String.Format("{0};", krikos360_030.prv_descrip));
                            csvMemoria.Append(String.Format("{0};", "2109"));
                            //csvMemoria.Append(String.Format("{0};", ""));
                            //csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", "30-56702073-4"));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0}", ""));
                            csvMemoria.AppendLine();


                        csvMemoria.Append(String.Format("{0};", "040"));
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Cliente_RazonSocial));
                        csvMemoria.Append(String.Format("{0};", "9999999999999")); // gln
                        csvMemoria.Append(String.Format("{0};", "COD INT"));
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Cliente_SitIVA));
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Cliente_NumeroIIBB));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Cliente_Direccion));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Cliente_Localidad));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Cliente_CodigoPostal));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", "80"));
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Cliente_NroDocumento));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0}", ""));
                        csvMemoria.AppendLine();

                        csvMemoria.Append(String.Format("{0};", "045"));
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Cliente_RazonSocial));
                        csvMemoria.Append(String.Format("{0};", "9999999999999"));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", "80"));
                        csvMemoria.Append(String.Format("{0};", krikos360_010.Cliente_NroDocumento));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0}", ""));
                        csvMemoria.AppendLine();

                       
                        double TotalIva = 0;
                        foreach (EFlexSDK_ItemVentas krikos360_Items in (krikos360_010.Comprobante_Items))
                        {
                            if (krikos360_Items.Item_ImporteIVAInscrip != null)
                            {
                                TotalIva = TotalIva + Convert.ToDouble(krikos360_Items.Item_ImporteIVAInscrip);
                            }
                        }
                           
                        string Total = regex.Replace(TotalIva.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "");
                        csvMemoria.Append(String.Format("{0};", "050"));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", regex.Replace(krikos360_010.Comprobante_ImporteTotal.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", regex.Replace((krikos360_010.Comprobante_ImporteTotal - TotalIva).ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", Total.PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", "PES"));
                        csvMemoria.Append(String.Format("{0};", "00001000000"));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0')));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0}", ""));
                        csvMemoria.AppendLine();



                        foreach (EFlexSDK_ItemVentas krikos360_Items in (krikos360_010.Comprobante_Items))
                        {
                            csvMemoria.Append(String.Format("{0};", "100"));
                            csvMemoria.Append(String.Format("{0};", krikos360_Items.Item_NumeroRenglon.ToString().PadLeft(6, '0')));
                            csvMemoria.Append(String.Format("{0};", krikos360_Items.Item_CodigoArticulo));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", krikos360_Items.Item_DescripArticulo));
                            csvMemoria.Append(String.Format("{0};", regex.Replace(krikos360_Items.Item_CantidadUM1.ToString("0.00000", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0')));
                            EFlexSDK_Tablas eFlexSDK_Tablas = new EFlexSDK_Tablas(pToken);
                            List<EFlexSDK_ClaseArticulo> claseArt = eFlexSDK_Tablas.ObtenerClasesArticulo();
                            if (claseArt.FirstOrDefault().Clase_CodUnidadMedida1 == "UN")
                            {
                                csvMemoria.Append(String.Format("{0};", "12"));
                            }
                            else
                            {
                                csvMemoria.Append(String.Format("{0};", "00")); // asumo que son unidades                            csvMemoria.Append(String.Format("{0};", claseArt.FirstOrDefault().Clase_CodUnidadMedida1)); // asumo que son unidades
                            }
                            csvMemoria.Append(String.Format("{0};", regex.Replace(krikos360_Items.Item_PrecioUnitario.ToString("0.000", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0')));
                            if (krikos360_Items.Item_TasaIVAInscrip != null)
                            {
                                csvMemoria.Append(String.Format("{0};", regex.Replace(Convert.ToDouble(krikos360_Items.Item_TasaIVAInscrip).ToString("000.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(5, '0')));
                            }
                            else
                            {
                                csvMemoria.Append(String.Format("{0};", regex.Replace(0.ToString("000.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(5, '0')));
                            }
                            csvMemoria.Append(String.Format("{0};", ""));// subtotal
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));// numero de unidad
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", "1234567890123"));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0}", ""));
                            csvMemoria.AppendLine();
                        }


                        csvMemoria.Append(String.Format("{0};", "110"));
                        csvMemoria.Append(String.Format("{0};", "1".PadLeft(6, '0')));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0}", "").PadLeft(15, '0'));
                        csvMemoria.AppendLine();
                    }
                    try
                    {
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName, false,
                        System.Text.Encoding.Default);
                        sw.Write(csvMemoria.ToString());
                        sw.Close();
                        MessageBox.Show("Se Exporto Correctamente");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                else
                {
                    MessageBox.Show("No se encontrado la ruta del archivo");
                    return;
                }
            }
            else
            {
                MessageBox.Show("No hay comprobante para Generar");
                return;
            }
        }
    

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmIngresar_Click(object sender, EventArgs e)
        {  
            DataRowView EmpresaOrig = (DataRowView)cbxEmpresasOrig.SelectedItem;        
            try
            {
                EFlexSDK_Procesos.ProcesosFlex.AbrirEmpresaFlex(EmpresaOrig.Row.ItemArray[1].ToString(), EmpresaOrig.Row.ItemArray[2].ToString(), pToken);
                gbxAbrirEmpresa.Enabled = false;
                gbxFiltrosComp.Enabled = true;
                gbxExportar.Enabled = false;
            }
            catch (EFlexSDK_Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
                      
        }
        private void FrmSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnCancelarEmp_Click(object sender, EventArgs e)
        {
            limpiarFiltros();
            gbxAbrirEmpresa.Enabled = true;
            gbxFiltrosComp.Enabled = false;
            gbxExportar.Enabled = false;
            dgvDatos.DataSource = null;
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            sResultado = ListarComprobantes();

            if (!string.IsNullOrEmpty(sResultado))
            {
                MessageBox.Show("Error al consultar los comprobantes. " + Environment.NewLine + Environment.NewLine + sResultado, "SDK - CONSULTAS - Comprobantes");
                return;
            }
        }
        public string ListarComprobantes()
        {
            try
            {
                string sResultado = string.Empty;
                EFlexSDK_Ventas sVentas = new EFlexSDK_Ventas(pToken);
                List<EFlexSDK_FiltroComprobantes> MisFiltros = new List<EFlexSDK_FiltroComprobantes>();
                if (!(DtpDesde.Value == DtpDesde.MinDate && DtpHasta.Value == DtpHasta.MinDate))
                {
                    if (!(CbxComprobantes.SelectedItem != null))
                    {
                        MisFiltros.Add(new EFlexSDK_FiltroComprobantes()
                        {
                            Operacion = string.Empty,
                            Campo = "Comprobante_Tipo",
                            Accion = "IGUAL",
                            Valor = "FC"
                        });
                    }
                    if (!(DtpDesde.Value == DtpDesde.MinDate))
                    {
                        MisFiltros.Add(new EFlexSDK_FiltroComprobantes()
                        {
                            Operacion = "Y",
                            Campo = "Comprobante_FechaContabilizacion",
                            Accion = "MAYOR O IGUAL",

                            Valor = Convert.ToDateTime(DtpDesde.Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)
                        });
                    }
                    if (!(DtpHasta.Value == DtpHasta.MinDate))
                    {
                        MisFiltros.Add(new EFlexSDK_FiltroComprobantes()
                        {
                            Operacion = "Y",
                            Campo = "Comprobante_FechaContabilizacion",
                            Accion = "MENOR O IGUAL",
                            Valor = Convert.ToDateTime(DtpHasta.Value.ToString(), System.Globalization.CultureInfo.InvariantCulture)
                        });
                    }
                }
                else
                {
                    return sResultado = "No Aplicaron los Filtros Desde / Hasta (FECHA)";
                }
                sComprobantes = sVentas.ListarComprobantes(MisFiltros, pToken);

                if (sComprobantes == null)
                {
                    sResultado = sVentas.ObtenerListaErrores();
                }
                else
                {
                    int i;
                    dgvDatos.DataSource = null;
                    dgvDatos.DataSource = sComprobantes;
                    for (i = 9; i < dgvDatos.ColumnCount; i++)
                    {
                        dgvDatos.Columns[i].Visible = false;
                    }
                    limpiarFiltros();
                    if (dgvDatos.Rows.Count > 0)
                    {
                        gbxExportar.Enabled = true;
                        gbxFiltrosComp.Enabled = false;
                    }
                }

                return sResultado;
            }
            catch (EFlexSDK_Exception ex)
            {
                return ex.Message;
            }
        }
        private void limpiarFiltros()
        {
            rbWalmart.Checked = true;
            DtpDesde.Value = DtpDesde.MinDate;
            DtpHasta.Value = DtpHasta.MinDate;
        }
    }
}
