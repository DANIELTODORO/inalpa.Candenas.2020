namespace KRIKOS360_INALPA
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.rbWalmart = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.gbxAbrirEmpresa = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.FrmSalir = new System.Windows.Forms.Button();
            this.FrmIngresar = new System.Windows.Forms.Button();
            this.cbxEmpresasOrig = new System.Windows.Forms.ComboBox();
            this.gbxExportar = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.BarraProceso = new System.Windows.Forms.ProgressBar();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.gbxFiltrosComp = new System.Windows.Forms.GroupBox();
            this.CbxComprobantes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancelarEmp = new System.Windows.Forms.Button();
            this.DtpHasta = new System.Windows.Forms.DateTimePicker();
            this.DtpDesde = new System.Windows.Forms.DateTimePicker();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbxAbrirEmpresa.SuspendLayout();
            this.gbxExportar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.gbxFiltrosComp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGenerar
            // 
            this.btnGenerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.Location = new System.Drawing.Point(209, 39);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(75, 23);
            this.btnGenerar.TabIndex = 0;
            this.btnGenerar.Text = "Generar";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(603, 39);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 3;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "Exportador Krikos 360";
            // 
            // rbWalmart
            // 
            this.rbWalmart.AutoSize = true;
            this.rbWalmart.Checked = true;
            this.rbWalmart.Location = new System.Drawing.Point(523, 21);
            this.rbWalmart.Name = "rbWalmart";
            this.rbWalmart.Size = new System.Drawing.Size(64, 17);
            this.rbWalmart.TabIndex = 6;
            this.rbWalmart.TabStop = true;
            this.rbWalmart.Text = "Walmart";
            this.rbWalmart.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(523, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.Text = "Super Día% ";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // gbxAbrirEmpresa
            // 
            this.gbxAbrirEmpresa.Controls.Add(this.label7);
            this.gbxAbrirEmpresa.Controls.Add(this.FrmSalir);
            this.gbxAbrirEmpresa.Controls.Add(this.FrmIngresar);
            this.gbxAbrirEmpresa.Controls.Add(this.cbxEmpresasOrig);
            this.gbxAbrirEmpresa.Location = new System.Drawing.Point(12, 12);
            this.gbxAbrirEmpresa.Name = "gbxAbrirEmpresa";
            this.gbxAbrirEmpresa.Size = new System.Drawing.Size(684, 63);
            this.gbxAbrirEmpresa.TabIndex = 14;
            this.gbxAbrirEmpresa.TabStop = false;
            this.gbxAbrirEmpresa.Text = "Empresa Origen";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Empresa:";
            // 
            // FrmSalir
            // 
            this.FrmSalir.BackColor = System.Drawing.Color.LightGray;
            this.FrmSalir.Location = new System.Drawing.Point(403, 22);
            this.FrmSalir.Name = "FrmSalir";
            this.FrmSalir.Size = new System.Drawing.Size(64, 21);
            this.FrmSalir.TabIndex = 2;
            this.FrmSalir.Text = "Salir";
            this.FrmSalir.UseVisualStyleBackColor = false;
            this.FrmSalir.Click += new System.EventHandler(this.FrmSalir_Click);
            // 
            // FrmIngresar
            // 
            this.FrmIngresar.BackColor = System.Drawing.Color.LightGray;
            this.FrmIngresar.Location = new System.Drawing.Point(333, 22);
            this.FrmIngresar.Name = "FrmIngresar";
            this.FrmIngresar.Size = new System.Drawing.Size(64, 21);
            this.FrmIngresar.TabIndex = 1;
            this.FrmIngresar.Text = "Ingresar";
            this.FrmIngresar.UseVisualStyleBackColor = false;
            this.FrmIngresar.Click += new System.EventHandler(this.FrmIngresar_Click);
            // 
            // cbxEmpresasOrig
            // 
            this.cbxEmpresasOrig.FormattingEnabled = true;
            this.cbxEmpresasOrig.Location = new System.Drawing.Point(81, 23);
            this.cbxEmpresasOrig.Name = "cbxEmpresasOrig";
            this.cbxEmpresasOrig.Size = new System.Drawing.Size(246, 21);
            this.cbxEmpresasOrig.TabIndex = 0;
            // 
            // gbxExportar
            // 
            this.gbxExportar.Controls.Add(this.button1);
            this.gbxExportar.Controls.Add(this.BarraProceso);
            this.gbxExportar.Controls.Add(this.btnGenerar);
            this.gbxExportar.Controls.Add(this.btnSalir);
            this.gbxExportar.Location = new System.Drawing.Point(12, 366);
            this.gbxExportar.Name = "gbxExportar";
            this.gbxExportar.Size = new System.Drawing.Size(684, 70);
            this.gbxExportar.TabIndex = 13;
            this.gbxExportar.TabStop = false;
            this.gbxExportar.Text = "Exportar:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(290, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // BarraProceso
            // 
            this.BarraProceso.Location = new System.Drawing.Point(6, 39);
            this.BarraProceso.Name = "BarraProceso";
            this.BarraProceso.Size = new System.Drawing.Size(197, 23);
            this.BarraProceso.TabIndex = 14;
            // 
            // dgvDatos
            // 
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.dgvDatos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDatos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatos.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDatos.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDatos.EnableHeadersVisualStyles = false;
            this.dgvDatos.GridColor = System.Drawing.Color.Black;
            this.dgvDatos.Location = new System.Drawing.Point(12, 156);
            this.dgvDatos.Name = "dgvDatos";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatos.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDatos.Size = new System.Drawing.Size(684, 204);
            this.dgvDatos.TabIndex = 11;
            // 
            // gbxFiltrosComp
            // 
            this.gbxFiltrosComp.Controls.Add(this.CbxComprobantes);
            this.gbxFiltrosComp.Controls.Add(this.label1);
            this.gbxFiltrosComp.Controls.Add(this.label2);
            this.gbxFiltrosComp.Controls.Add(this.label6);
            this.gbxFiltrosComp.Controls.Add(this.rbWalmart);
            this.gbxFiltrosComp.Controls.Add(this.btnCancelarEmp);
            this.gbxFiltrosComp.Controls.Add(this.radioButton2);
            this.gbxFiltrosComp.Controls.Add(this.DtpHasta);
            this.gbxFiltrosComp.Controls.Add(this.DtpDesde);
            this.gbxFiltrosComp.Controls.Add(this.btnFiltrar);
            this.gbxFiltrosComp.Controls.Add(this.label4);
            this.gbxFiltrosComp.Controls.Add(this.label5);
            this.gbxFiltrosComp.Location = new System.Drawing.Point(12, 82);
            this.gbxFiltrosComp.Name = "gbxFiltrosComp";
            this.gbxFiltrosComp.Size = new System.Drawing.Size(684, 68);
            this.gbxFiltrosComp.TabIndex = 12;
            this.gbxFiltrosComp.TabStop = false;
            this.gbxFiltrosComp.Text = "Filtros:";
            // 
            // CbxComprobantes
            // 
            this.CbxComprobantes.FormattingEnabled = true;
            this.CbxComprobantes.Location = new System.Drawing.Point(333, 39);
            this.CbxComprobantes.Name = "CbxComprobantes";
            this.CbxComprobantes.Size = new System.Drawing.Size(171, 21);
            this.CbxComprobantes.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Tipos Comprobantes:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Fecha de Emisión:";
            // 
            // btnCancelarEmp
            // 
            this.btnCancelarEmp.BackColor = System.Drawing.Color.LightGray;
            this.btnCancelarEmp.Location = new System.Drawing.Point(614, 39);
            this.btnCancelarEmp.Name = "btnCancelarEmp";
            this.btnCancelarEmp.Size = new System.Drawing.Size(64, 23);
            this.btnCancelarEmp.TabIndex = 10;
            this.btnCancelarEmp.Text = "Cancelar";
            this.btnCancelarEmp.UseVisualStyleBackColor = false;
            this.btnCancelarEmp.Click += new System.EventHandler(this.btnCancelarEmp_Click);
            // 
            // DtpHasta
            // 
            this.DtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpHasta.Location = new System.Drawing.Point(215, 39);
            this.DtpHasta.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.DtpHasta.Name = "DtpHasta";
            this.DtpHasta.Size = new System.Drawing.Size(100, 20);
            this.DtpHasta.TabIndex = 4;
            // 
            // DtpDesde
            // 
            this.DtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpDesde.Location = new System.Drawing.Point(54, 39);
            this.DtpDesde.MinDate = new System.DateTime(1999, 1, 1, 0, 0, 0, 0);
            this.DtpDesde.Name = "DtpDesde";
            this.DtpDesde.Size = new System.Drawing.Size(100, 20);
            this.DtpDesde.TabIndex = 3;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.LightGray;
            this.btnFiltrar.Location = new System.Drawing.Point(614, 13);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(64, 23);
            this.btnFiltrar.TabIndex = 9;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Hasta:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Desde:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(708, 440);
            this.Controls.Add(this.gbxAbrirEmpresa);
            this.Controls.Add(this.gbxExportar);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.gbxFiltrosComp);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Krikos360 - Inalpa";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.gbxAbrirEmpresa.ResumeLayout(false);
            this.gbxAbrirEmpresa.PerformLayout();
            this.gbxExportar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.gbxFiltrosComp.ResumeLayout(false);
            this.gbxFiltrosComp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.RadioButton rbWalmart;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox gbxAbrirEmpresa;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button FrmSalir;
        private System.Windows.Forms.Button FrmIngresar;
        private System.Windows.Forms.ComboBox cbxEmpresasOrig;
        private System.Windows.Forms.GroupBox gbxExportar;
        private System.Windows.Forms.ProgressBar BarraProceso;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.GroupBox gbxFiltrosComp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCancelarEmp;
        private System.Windows.Forms.DateTimePicker DtpHasta;
        private System.Windows.Forms.DateTimePicker DtpDesde;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CbxComprobantes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

