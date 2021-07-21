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
using SB.NET.eFlex.SDKLib.Comprobantes;
using SB.NET.eFlex.SDKLib.Tablas;

namespace KRIKOS360_INALPA
{
    public partial class FrmModulo : Form
    {
        string sResultado = "";
        EFlexSDK_TokenValidacion pToken = null;
        List<EFlexSDK_ComprobanteVentas> sComprobantes;
        DialogResult Resp;
        SBDABEJERMAN oSBDABEJERMAN;
        DataRowView EmpresaOrig;
        public FrmModulo(EFlexSDK_TokenValidacion _Token, DataRowView _dtEmpresaOrig)
        {
            InitializeComponent();
            DtpDesde.Value = DateTime.Now;
            oSBDABEJERMAN = SBDABEJERMAN.obtenerinstancia();
            CbxComprobantes.SelectedIndex = 0;
            pToken = _Token;
            EmpresaOrig = _dtEmpresaOrig;
            Resp = DialogResult.OK;
            try
            {
                EFlexSDK_Procesos.ProcesosFlex.AbrirEmpresaFlex(EmpresaOrig.Row.ItemArray[1].ToString(), EmpresaOrig.Row.ItemArray[2].ToString(), pToken);
                gbxFiltrosComp.Enabled = true;
                gbxExportar.Enabled = false;
            }
            catch (EFlexSDK_Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                        EFlexSDK_Ventas eFlexSDK_Ventas = new EFlexSDK_Ventas(pToken);
                        List<EFlexSDK_FiltroComprobantes> FiltroComp = new List<EFlexSDK_FiltroComprobantes>();
                        EFlexSDK_FiltroComprobantes eFlexSDK_FiltroComprobantes = new EFlexSDK_FiltroComprobantes();
                        eFlexSDK_FiltroComprobantes.Accion = "IGUAL";
                        eFlexSDK_FiltroComprobantes.Campo = "Comprobante_ID";
                        eFlexSDK_FiltroComprobantes.Operacion = "";
                        eFlexSDK_FiltroComprobantes.Valor = krikos360_010.Comprobante_ID;

                        FiltroComp.Add(eFlexSDK_FiltroComprobantes);
                   
                        EFlexSDK_ComprobanteVentas CompCompleto =  eFlexSDK_Ventas.DetalleComprobante(FiltroComp,pToken);
                        
                        csvMemoria.Append(String.Format("{0};", "010")); // Bloque 010
                        switch(CbxComprobantes.SelectedItem.ToString())
                        {
                            case "FC":
                                csvMemoria.Append(String.Format("{0};", "001")); // 01001
                                break;
                            case "FCC":
                                csvMemoria.Append(String.Format("{0};", "201")); // 01001
                                break;
                            case "NC":
                                csvMemoria.Append(String.Format("{0};", "003")); // 01001
                                break;
                            case "NCC":
                                csvMemoria.Append(String.Format("{0};", "203")); // 01001
                                break;
                            case "ND":
                                csvMemoria.Append(String.Format("{0};", "002")); // 01001
                                break;
                            case "NCD":
                                csvMemoria.Append(String.Format("{0};", "202")); // 01001
                                break;
                        } 
                        
                        // FIN 01001

                        csvMemoria.Append(String.Format("{0};", CompCompleto.Comprobante_Numero).PadLeft(8, '0')); // 01002
                        csvMemoria.Append(String.Format("{0};", CompCompleto.Comprobante_Numero).PadLeft(8, '0')); // 01003
                        csvMemoria.Append(String.Format("{0};", CompCompleto.Comprobante_PtoVenta.PadLeft(5, '0'))); // 01004
                        csvMemoria.Append(String.Format("{0};", (CompCompleto.Comprobante_FechaEmision).ToString("yyyyMMdd", CultureInfo.InvariantCulture))); // 01005
                        if (CompCompleto.Comprobante_FechaVencimiento.Value.Date != null)
                        {
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Comprobante_FechaVencimiento.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture)));
                        }
                        else
                        {
                            csvMemoria.Append(String.Format("{0};", "        "));
                        }  // 01006

                        csvMemoria.Append(String.Format("{0};", "")); // 01007
                        csvMemoria.Append(String.Format("{0};", "")); // 01008
                        csvMemoria.Append(String.Format("{0};", CompCompleto.Comprobante_CondVenta)); //01009
                        if (CompCompleto.Comprobante_NumeroCAI != null) // Desde 01010
                        {
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Comprobante_NumeroCAI.PadLeft(14, ' ')));
                        }
                        else
                        {
                            csvMemoria.Append(String.Format("{0};", "".PadLeft(14, ' ')));
                        }// Hasta 01010
                        if (CompCompleto.Comprobante_FechaVencimientoCAI != null) // Desde 01011
                        {
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Comprobante_FechaVencimientoCAI.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture)));
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

                        if (CompCompleto.Comprobante_FechaContabilizacion != null) // Desde 01024
                        {
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Comprobante_FechaContabilizacion.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture)));
                        }
                        else
                        {
                            csvMemoria.Append(String.Format("{0};", "        "));
                        }   // Hasta 01024
                        
                        csvMemoria.Append(String.Format("{0}", ""));  //01025
                        csvMemoria.AppendLine();

                        //Bloque 020: Comprobamte de Referencia 
                                                
                        foreach (KrikosComprobantesAsoc_Result krikos360_020 in oSBDABEJERMAN.KrikosComprobantesAsoc(CompCompleto.Comprobante_ID))
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
                            
                        }
                        if (krikos360_010.Comprobante_Mensaje != "")
                        {
                            
                                csvMemoria.Append(String.Format("{0};", "020"));
                                csvMemoria.Append(String.Format("{0};", "PC"));
                                csvMemoria.Append(String.Format("{0};", "00001"));
                                csvMemoria.Append(String.Format("{0};", krikos360_010.Comprobante_Mensaje));
                                csvMemoria.Append(String.Format("{0};", ""));
                                csvMemoria.Append(String.Format("{0}", ""));
                                csvMemoria.AppendLine();
                            
                        }

                        //Bloque Identificacion del Emisor de la Factura


                        csvMemoria.Append(String.Format("{0};", "030"));
                            csvMemoria.Append(String.Format("{0};", "INALPA S.A."));
                            csvMemoria.Append(String.Format("{0};", "7792350000009")); // gln 
                            csvMemoria.Append(String.Format("{0};", "01")); //siv_iva
                            csvMemoria.Append(String.Format("{0};", "921-752786-6")); 
                            csvMemoria.Append(String.Format("{0};", "20160101"));  
                            csvMemoria.Append(String.Format("{0};", ""));                                                                                        
                            csvMemoria.Append(String.Format("{0};", "Amenábar 2046"));                                                                                                                                                                                                                                                                                                            //csvMemoria.Append(String.Format("{0};", krikos360_030.pvt_Direc));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", "PAVON ARRIBA"));
                            csvMemoria.Append(String.Format("{0};", "SANTA FE"));
                            csvMemoria.Append(String.Format("{0};", "2109"));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", "30567020734"));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0};", ""));
                            csvMemoria.Append(String.Format("{0}", ""));
                            csvMemoria.AppendLine();


                            csvMemoria.Append(String.Format("{0};", "040")); 
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Cliente_RazonSocial)); //04001
                            if(CompCompleto.Cliente_Codigo == "005109")
                            {
                                csvMemoria.Append(String.Format("{0};", "7799011000002")); // 04002
                            }
                            if (CompCompleto.Cliente_Codigo == "000468")
                            {
                                csvMemoria.Append(String.Format("{0};", "7798130250008")); // 04002
                            }
                            csvMemoria.Append(String.Format("{0};", "123456789")); //04003
                            csvMemoria.Append(String.Format("{0};", "01")); //04004
                            csvMemoria.Append(String.Format("{0};", "01")); //04005 codigo de provincia  1- Capital Fedral 2-Buenos Aires 21- Santa fe
                            csvMemoria.Append(String.Format("{0};", "")); //04006
                            csvMemoria.Append(String.Format("{0};", "")); //04007
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Cliente_Direccion)); //04008
                            csvMemoria.Append(String.Format("{0};", "")); //04009                                
                            csvMemoria.Append(String.Format("{0};", "")); //04010
                            csvMemoria.Append(String.Format("{0};", "")); //04011
                            csvMemoria.Append(String.Format("{0};", "")); //04012
                            csvMemoria.Append(String.Format("{0};", "")); //04013
                            csvMemoria.Append(String.Format("{0};", "")); //04014
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Cliente_Localidad));//04015
                            csvMemoria.Append(String.Format("{0};", "")); //04016
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Cliente_CodigoPostal));//04017
                            csvMemoria.Append(String.Format("{0};", "")); //04018
                            csvMemoria.Append(String.Format("{0};", "")); //04019
                            csvMemoria.Append(String.Format("{0};", "80")); //04020
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Cliente_NroDocumento));//04021
                            csvMemoria.Append(String.Format("{0};", "")); //04022
                            csvMemoria.Append(String.Format("{0};", "")); //04023
                            csvMemoria.Append(String.Format("{0};", "")); //04024
                            csvMemoria.Append(String.Format("{0};", "")); //04025
                            csvMemoria.Append(String.Format("{0}", ""));  //04026
                            csvMemoria.AppendLine();
                            
                            csvMemoria.Append(String.Format("{0};", "045")); //04500
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Cliente_RazonSocial)); //04501
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Comprobante_Proyecto)); //04502
                            csvMemoria.Append(String.Format("{0};", "")); //04503
                            csvMemoria.Append(String.Format("{0};", "")); //04504
                            csvMemoria.Append(String.Format("{0};", "")); //04505
                            csvMemoria.Append(String.Format("{0};", "")); //04506
                            csvMemoria.Append(String.Format("{0};", "")); //04507
                            csvMemoria.Append(String.Format("{0};", "")); //04508
                            csvMemoria.Append(String.Format("{0};", "")); //04509
                            csvMemoria.Append(String.Format("{0};", "")); //04510
                            csvMemoria.Append(String.Format("{0};", "")); //04511
                            csvMemoria.Append(String.Format("{0};", "")); //04512
                            csvMemoria.Append(String.Format("{0};", "")); //04513
                            csvMemoria.Append(String.Format("{0};", "")); //04514
                            csvMemoria.Append(String.Format("{0};", "")); //04515
                            csvMemoria.Append(String.Format("{0};", "")); //04516
                            csvMemoria.Append(String.Format("{0};", "")); //04517
                            csvMemoria.Append(String.Format("{0};", "")); //04518
                            csvMemoria.Append(String.Format("{0};", "")); //04519
                            csvMemoria.Append(String.Format("{0};", "80")); //04520
                            csvMemoria.Append(String.Format("{0};", CompCompleto.Cliente_NroDocumento)); //04521
                            csvMemoria.Append(String.Format("{0};", "")); //04522
                            csvMemoria.Append(String.Format("{0};", "")); //04523
                            csvMemoria.Append(String.Format("{0};", "")); //04524
                            csvMemoria.Append(String.Format("{0};", "")); //04525
                            csvMemoria.Append(String.Format("{0};", "")); //04526
                            csvMemoria.Append(String.Format("{0}", "")); //04527
                            csvMemoria.AppendLine();
                       
                        
                        //Fin del bloque 045 

                        double TotalIva = 0;
                        foreach (EFlexSDK_ItemVentas krikos360_Items in (CompCompleto.Comprobante_Items))
                        {
                            if (krikos360_Items.Item_ImporteIVAInscrip != null)
                            {
                                TotalIva = TotalIva + Convert.ToDouble(krikos360_Items.Item_ImporteIVAInscrip);
                            }
                        }
                           
                        string Total = regex.Replace(TotalIva.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "");
                        
                        csvMemoria.Append(String.Format("{0};", "050"));
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05001
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05002
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05003
                        csvMemoria.Append(String.Format("{0};", regex.Replace(CompCompleto.Comprobante_ImporteTotal.ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0'))); //05004
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05005
                        csvMemoria.Append(String.Format("{0};", regex.Replace((CompCompleto.Comprobante_ImporteTotal - TotalIva).ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0'))); //05006
                        csvMemoria.Append(String.Format("{0};", Total.PadLeft(15, '0'))); //05007
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05008
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05009
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05010
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05011
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05012
                        csvMemoria.Append(String.Format("{0};", "".PadLeft(15, '0'))); //05013
                        csvMemoria.Append(String.Format("{0};", "")); //05014
                        csvMemoria.Append(String.Format("{0};", "PES")); //05015
                        csvMemoria.Append(String.Format("{0};", "00001000000")); //05016
                        csvMemoria.Append(String.Format("{0};", "")); //05017
                        csvMemoria.Append(String.Format("{0};", "")); //05018
                        csvMemoria.Append(String.Format("{0};", "")); //05019
                        csvMemoria.Append(String.Format("{0};", "")); //05020
                        csvMemoria.Append(String.Format("{0};", "")); //05021
                        csvMemoria.Append(String.Format("{0};", "")); //05022
                        csvMemoria.Append(String.Format("{0};", "")); //05023
                        csvMemoria.Append(String.Format("{0};", "")); //05024
                        csvMemoria.Append(String.Format("{0}", ""));  //05025
                        csvMemoria.AppendLine();

                        //Fin del bloque 050

                        csvMemoria.Append(String.Format("{0};", "060")); 
                        csvMemoria.Append(String.Format("{0};", "02100")); //06001
                        csvMemoria.Append(String.Format("{0};", regex.Replace((TotalIva).ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0'))); //06002
                        csvMemoria.Append(String.Format("{0};", regex.Replace((CompCompleto.Comprobante_ImporteTotal - TotalIva).ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0'))); //06003
                        csvMemoria.Append(String.Format("{0}", "000000000000000")); //06004
                        csvMemoria.AppendLine();
                        //Fin del bloque 060

                        foreach (EFlexSDK_ItemVentas krikos360_Items in (CompCompleto.Comprobante_Items))
                        {
                            if (krikos360_Items.Item_Tipo == "A")
                            {
                                string CodUnidadM1 = "";
                                EFlexSDK_Tablas Tabla = new EFlexSDK_Tablas(pToken);
                                CodUnidadM1 = Tabla.ObtenerClasesArticulo().Where(x => x.Clase_Codigo == krikos360_Items.Item_Articulo_Clase).FirstOrDefault().Clase_CodUnidadMedida1;

                                csvMemoria.Append(String.Format("{0};", "100"));
                                csvMemoria.Append(String.Format("{0};", krikos360_Items.Item_NumeroRenglon.ToString().PadLeft(6, '0'))); //10001
                                csvMemoria.Append(String.Format("{0};", krikos360_Items.Item_CodigoArticulo));//10002
                                csvMemoria.Append(String.Format("{0};", ""));//10003
                                csvMemoria.Append(String.Format("{0};", krikos360_Items.Item_DescripArticulo));//10004
                                csvMemoria.Append(String.Format("{0};", regex.Replace(krikos360_Items.Item_CantidadUM1.ToString("0.00000", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0')));//10005
                                

                                if (CodUnidadM1 == "PC" || CodUnidadM1 == "UN" || CodUnidadM1 == "KG" || CodUnidadM1 == "LA" || CodUnidadM1 == "MT" || CodUnidadM1 == "LT")
                                {
                                    if (CodUnidadM1 == "PC")
                                    {
                                        csvMemoria.Append(String.Format("{0};", "62"));
                                    }
                                    if (CodUnidadM1 == "UN")
                                    {
                                        csvMemoria.Append(String.Format("{0};", "07"));
                                    }
                                    if (CodUnidadM1 == "KG")
                                    {
                                        csvMemoria.Append(String.Format("{0};", "01"));
                                    }
                                    if (CodUnidadM1 == "LA")
                                    {
                                        csvMemoria.Append(String.Format("{0};", "07"));
                                    }
                                    if (CodUnidadM1 == "MT")
                                    {
                                        csvMemoria.Append(String.Format("{0};", "02"));
                                    }
                                    if (CodUnidadM1 == "LT")
                                    {
                                        csvMemoria.Append(String.Format("{0};", "05"));
                                    }
                                } // 10006
                                else
                                {
                                    csvMemoria.Append(String.Format("{0};", " NO INFORMA O NO ESTA DEFINIDA LA CLASE")); // NO INFORMA O NO ESTA DEFINIDA
                                } //10006

                                csvMemoria.Append(String.Format("{0};", regex.Replace(krikos360_Items.Item_PrecioUnitario.ToString("0.000", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0')));//10007
                                csvMemoria.Append(String.Format("{0};", "02100"));//10008
                                
                                if (krikos360_Items.Item_TasaIVAInscrip != null)
                                {
                                    csvMemoria.Append(String.Format("{0};", regex.Replace(Convert.ToDouble(krikos360_Items.Item_ImporteIVAInscrip).ToString("000.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0')));
                                } //10009
                                else
                                {
                                    csvMemoria.Append(String.Format("{0};", regex.Replace(0.ToString("000.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(5, '0')));
                                } //10009

                                csvMemoria.Append(String.Format("{0};", regex.Replace(Convert.ToDouble(krikos360_Items.Item_ImporteTotal).ToString("0.00", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0'))); //10010
                                csvMemoria.Append(String.Format("{0};", ""));//10011
                                csvMemoria.Append(String.Format("{0};", ""));//10012
                                csvMemoria.Append(String.Format("{0};", ""));//10013
                                csvMemoria.Append(String.Format("{0};", ""));//10014
                                csvMemoria.Append(String.Format("{0};", "000000000002400")); //10015 numero de unidad
                                csvMemoria.Append(String.Format("{0};", ""));//10016
                                csvMemoria.Append(String.Format("{0};", ""));//10017
                                csvMemoria.Append(String.Format("{0};", "DU"));//10018
                                csvMemoria.Append(String.Format("{0};", regex.Replace(krikos360_Items.Item_PrecioUnitario.ToString("0.000", System.Globalization.CultureInfo.GetCultureInfo("en-US")), "").PadLeft(15, '0')));//10019
                                if (krikos360_Items.Item_CodigoBarras != null)
                                {
                                    csvMemoria.Append(String.Format("{0};", krikos360_Items.Item_CodigoBarras.ToString().PadLeft(13, '0')));//10020
                                }
                                else
                                {
                                    MessageBox.Show("El Articulo: "+ krikos360_Items.Item_DescripArticulo+"," + krikos360_Items.Item_CodigoArticulo+". "+ "No tiene Codigo de Barra"  );
                                }
                                csvMemoria.Append(String.Format("{0};", ""));//10021
                                csvMemoria.Append(String.Format("{0};", ""));//10022
                                csvMemoria.Append(String.Format("{0};", ""));//10023
                                csvMemoria.Append(String.Format("{0};", ""));//10024
                                csvMemoria.Append(String.Format("{0}", ""));//10025
                                csvMemoria.AppendLine();
                            }
                        }
                        csvMemoria.Append(String.Format("{0};", "110"));
                        csvMemoria.Append(String.Format("{0};", "1".PadLeft(6, '0')));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0};", ""));
                        csvMemoria.Append(String.Format("{0}", "").PadLeft(15, '0'));
                        csvMemoria.AppendLine();


                        if(CbxComprobantes.SelectedItem.ToString()== "FCC" || CbxComprobantes.SelectedItem.ToString() == "NCC")
                        {
                            csvMemoria.Append(String.Format("{0};", "140")); // 14000
                            csvMemoria.Append(String.Format("{0};", "CodigoAFIP")); // 14001
                            csvMemoria.Append(String.Format("{0}", "2101|2850778330000000906012")); // 14002
                            csvMemoria.AppendLine();
                        }

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

        private void btnCancelarEmp_Click(object sender, EventArgs e)
        {
            limpiarFiltros();
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
                sComprobantes = new List<EFlexSDK_ComprobanteVentas>();
                int anio = DtpDesde.Value.Year;
                int mes = DtpDesde.Value.Month;
                List<EFlexSDK_FiltroComprobantes> MisFiltros = new List<EFlexSDK_FiltroComprobantes>
                {
                    new EFlexSDK_FiltroComprobantes()
                    {
                        Operacion = "",
                        Campo = "Comprobante_FechaEmision",
                        Accion = "MAYOR O IGUAL",
                        Valor =  new DateTime(anio,1,mes)
                    }       
                };

                if (CbxComprobantes.SelectedItem != null)
                {
                    EFlexSDK_FiltroComprobantes eFlexSDK_Filtro_Comp = new EFlexSDK_FiltroComprobantes();
                    eFlexSDK_Filtro_Comp.Operacion = "Y";
                    eFlexSDK_Filtro_Comp.Campo = "Comprobante_Tipo";
                    eFlexSDK_Filtro_Comp.Accion = "IGUAL";
                    eFlexSDK_Filtro_Comp.Valor = CbxComprobantes.SelectedItem.ToString();
                    eFlexSDK_Filtro_Comp.Enlazada = false;
                    MisFiltros.Add(eFlexSDK_Filtro_Comp);
                }
              
                EFlexSDK_FiltroComprobantes eFlexSDK_Filtro_Emp = new EFlexSDK_FiltroComprobantes();
                eFlexSDK_Filtro_Emp.Operacion = "Y";
                eFlexSDK_Filtro_Emp.Campo = "Cliente_Codigo";
                eFlexSDK_Filtro_Emp.Accion = "IGUAL";
                if(rbWalmart.Checked)
                {
                    eFlexSDK_Filtro_Emp.Valor = "005109";
                }
                else
                {
                    eFlexSDK_Filtro_Emp.Valor = "000468";
                }

                MisFiltros.Add(eFlexSDK_Filtro_Emp);

                EFlexSDK_DatAdic eFlexSDK_DatAdic = new EFlexSDK_DatAdic();
                eFlexSDK_DatAdic.Nombre = "";
                eFlexSDK_DatAdic.Valor = "";
                
                sComprobantes = sVentas.ListarComprobantes(MisFiltros, pToken).Where(x=> x.Comprobante_FechaEmision == DtpDesde.Value ).ToList();
                

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
            DtpDesde.Value = DateTime.Now;
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
        private void cbxEmpresasOrig_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                DialogResult = DialogResult.Cancel;
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            limpiarFiltros();
            gbxFiltrosComp.Enabled = true;
            gbxExportar.Enabled = false;
            dgvDatos.DataSource = null;
            sComprobantes = null;
        }
    }
}
