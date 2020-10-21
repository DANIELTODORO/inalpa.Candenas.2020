namespace KRIKOS360_INALPA
{
    partial class FrmLogin
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
            this.label7 = new System.Windows.Forms.Label();
            this.FrmSalir = new System.Windows.Forms.Button();
            this.FrmIngresar = new System.Windows.Forms.Button();
            this.cbxEmpresasOrig = new System.Windows.Forms.ComboBox();
            this.TxbUsuario = new System.Windows.Forms.TextBox();
            this.TxbContraseña = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Empresa:";
            // 
            // FrmSalir
            // 
            this.FrmSalir.BackColor = System.Drawing.Color.DarkOrange;
            this.FrmSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FrmSalir.ForeColor = System.Drawing.Color.White;
            this.FrmSalir.Location = new System.Drawing.Point(306, 46);
            this.FrmSalir.Name = "FrmSalir";
            this.FrmSalir.Size = new System.Drawing.Size(99, 30);
            this.FrmSalir.TabIndex = 14;
            this.FrmSalir.Text = "Salir";
            this.FrmSalir.UseVisualStyleBackColor = false;
            this.FrmSalir.Click += new System.EventHandler(this.FrmSalir_Click);
            // 
            // FrmIngresar
            // 
            this.FrmIngresar.BackColor = System.Drawing.Color.DarkOrange;
            this.FrmIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FrmIngresar.ForeColor = System.Drawing.Color.White;
            this.FrmIngresar.Location = new System.Drawing.Point(306, 10);
            this.FrmIngresar.Name = "FrmIngresar";
            this.FrmIngresar.Size = new System.Drawing.Size(99, 30);
            this.FrmIngresar.TabIndex = 13;
            this.FrmIngresar.Text = "Ingresar";
            this.FrmIngresar.UseVisualStyleBackColor = false;
            this.FrmIngresar.Click += new System.EventHandler(this.FrmIngresar_Click);
            // 
            // cbxEmpresasOrig
            // 
            this.cbxEmpresasOrig.FormattingEnabled = true;
            this.cbxEmpresasOrig.Location = new System.Drawing.Point(97, 64);
            this.cbxEmpresasOrig.Name = "cbxEmpresasOrig";
            this.cbxEmpresasOrig.Size = new System.Drawing.Size(173, 21);
            this.cbxEmpresasOrig.TabIndex = 12;
            // 
            // TxbUsuario
            // 
            this.TxbUsuario.Location = new System.Drawing.Point(97, 13);
            this.TxbUsuario.Name = "TxbUsuario";
            this.TxbUsuario.Size = new System.Drawing.Size(173, 20);
            this.TxbUsuario.TabIndex = 16;
            this.TxbUsuario.Text = "ADMIN";
            // 
            // TxbContraseña
            // 
            this.TxbContraseña.Location = new System.Drawing.Point(97, 39);
            this.TxbContraseña.Name = "TxbContraseña";
            this.TxbContraseña.PasswordChar = '*';
            this.TxbContraseña.Size = new System.Drawing.Size(173, 20);
            this.TxbContraseña.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Contraseña:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Usuario:";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(429, 98);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxbContraseña);
            this.Controls.Add(this.TxbUsuario);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FrmSalir);
            this.Controls.Add(this.FrmIngresar);
            this.Controls.Add(this.cbxEmpresasOrig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Planexware";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button FrmSalir;
        private System.Windows.Forms.Button FrmIngresar;
        private System.Windows.Forms.ComboBox cbxEmpresasOrig;
        private System.Windows.Forms.TextBox TxbUsuario;
        private System.Windows.Forms.TextBox TxbContraseña;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}