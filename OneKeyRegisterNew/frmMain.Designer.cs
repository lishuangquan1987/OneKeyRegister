namespace OneKeyRegisterNew
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRegisterOCX = new System.Windows.Forms.Button();
            this.btnRegisterCSInterface = new System.Windows.Forms.Button();
            this.btnUnregisterVB6 = new System.Windows.Forms.Button();
            this.btnUnregisterCSInterface = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRegisterOCX
            // 
            this.btnRegisterOCX.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegisterOCX.Location = new System.Drawing.Point(25, 16);
            this.btnRegisterOCX.Name = "btnRegisterOCX";
            this.btnRegisterOCX.Size = new System.Drawing.Size(120, 85);
            this.btnRegisterOCX.TabIndex = 0;
            this.btnRegisterOCX.Text = "一键注册本程序目录下VB6的OCX/DLL";
            this.btnRegisterOCX.UseVisualStyleBackColor = true;
            this.btnRegisterOCX.Click += new System.EventHandler(this.btnRegisterOCX_Click);
            // 
            // btnRegisterCSInterface
            // 
            this.btnRegisterCSInterface.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRegisterCSInterface.Location = new System.Drawing.Point(174, 16);
            this.btnRegisterCSInterface.Name = "btnRegisterCSInterface";
            this.btnRegisterCSInterface.Size = new System.Drawing.Size(120, 85);
            this.btnRegisterCSInterface.TabIndex = 1;
            this.btnRegisterCSInterface.Text = "一键注册本程序目录下CSInterface.DLL";
            this.btnRegisterCSInterface.UseVisualStyleBackColor = true;
            this.btnRegisterCSInterface.Click += new System.EventHandler(this.btnRegisterCSInterface_Click);
            // 
            // btnUnregisterVB6
            // 
            this.btnUnregisterVB6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnregisterVB6.Location = new System.Drawing.Point(323, 16);
            this.btnUnregisterVB6.Name = "btnUnregisterVB6";
            this.btnUnregisterVB6.Size = new System.Drawing.Size(120, 85);
            this.btnUnregisterVB6.TabIndex = 2;
            this.btnUnregisterVB6.Text = "一键卸载本程序目录下VB6的OCX/DLL";
            this.btnUnregisterVB6.UseVisualStyleBackColor = true;
            this.btnUnregisterVB6.Click += new System.EventHandler(this.btnUnregisterVB6_Click);
            // 
            // btnUnregisterCSInterface
            // 
            this.btnUnregisterCSInterface.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUnregisterCSInterface.Location = new System.Drawing.Point(473, 16);
            this.btnUnregisterCSInterface.Name = "btnUnregisterCSInterface";
            this.btnUnregisterCSInterface.Size = new System.Drawing.Size(120, 85);
            this.btnUnregisterCSInterface.TabIndex = 3;
            this.btnUnregisterCSInterface.Text = "一键卸载本程序目录下CSInterface.DLL";
            this.btnUnregisterCSInterface.UseVisualStyleBackColor = true;
            this.btnUnregisterCSInterface.Click += new System.EventHandler(this.btnUnregisterCSInterface_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 121);
            this.Controls.Add(this.btnUnregisterCSInterface);
            this.Controls.Add(this.btnUnregisterVB6);
            this.Controls.Add(this.btnRegisterCSInterface);
            this.Controls.Add(this.btnRegisterOCX);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "One Key Register Tool";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegisterOCX;
        private System.Windows.Forms.Button btnRegisterCSInterface;
        private System.Windows.Forms.Button btnUnregisterVB6;
        private System.Windows.Forms.Button btnUnregisterCSInterface;
    }
}

