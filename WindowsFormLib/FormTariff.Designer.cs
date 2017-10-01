namespace WindowsFormLib
{
    partial class CFormTariff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormTariff));
            this.BtnDeleteSel = new System.Windows.Forms.Button();
            this.BtnModify = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.LblTariffDescp = new System.Windows.Forms.Label();
            this.CboTariffDescp = new System.Windows.Forms.ComboBox();
            this.CutpTariff = new CustomControlLib.CUserTariffPanel();
            this.BtnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnDeleteSel
            // 
            this.BtnDeleteSel.Location = new System.Drawing.Point(440, 529);
            this.BtnDeleteSel.Name = "BtnDeleteSel";
            this.BtnDeleteSel.Size = new System.Drawing.Size(85, 30);
            this.BtnDeleteSel.TabIndex = 27;
            this.BtnDeleteSel.Text = "删除选择";
            this.BtnDeleteSel.UseVisualStyleBackColor = true;
            this.BtnDeleteSel.Visible = false;
            this.BtnDeleteSel.Click += new System.EventHandler(this.BtnDeleteSel_Click);
            // 
            // BtnModify
            // 
            this.BtnModify.Location = new System.Drawing.Point(302, 529);
            this.BtnModify.Name = "BtnModify";
            this.BtnModify.Size = new System.Drawing.Size(85, 30);
            this.BtnModify.TabIndex = 29;
            this.BtnModify.Text = "修改";
            this.BtnModify.UseVisualStyleBackColor = true;
            this.BtnModify.Visible = false;
            this.BtnModify.Click += new System.EventHandler(this.BtnModify_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(172, 529);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(85, 30);
            this.BtnAdd.TabIndex = 28;
            this.BtnAdd.Text = "添加";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Visible = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // LblTariffDescp
            // 
            this.LblTariffDescp.AutoSize = true;
            this.LblTariffDescp.Location = new System.Drawing.Point(156, 9);
            this.LblTariffDescp.Name = "LblTariffDescp";
            this.LblTariffDescp.Size = new System.Drawing.Size(72, 16);
            this.LblTariffDescp.TabIndex = 30;
            this.LblTariffDescp.Text = "计费标准";
            // 
            // CboTariffDescp
            // 
            this.CboTariffDescp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboTariffDescp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboTariffDescp.FormattingEnabled = true;
            this.CboTariffDescp.Location = new System.Drawing.Point(228, 7);
            this.CboTariffDescp.Name = "CboTariffDescp";
            this.CboTariffDescp.Size = new System.Drawing.Size(253, 24);
            this.CboTariffDescp.TabIndex = 31;
            this.CboTariffDescp.SelectedIndexChanged += new System.EventHandler(this.CboTariffDescp_SelectedIndexChanged);
            this.CboTariffDescp.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.CboTariffDescp_Format);
            // 
            // CutpTariff
            // 
            this.CutpTariff.Enabled = false;
            this.CutpTariff.Location = new System.Drawing.Point(3, 42);
            this.CutpTariff.Margin = new System.Windows.Forms.Padding(4);
            this.CutpTariff.Name = "CutpTariff";
            this.CutpTariff.Padding = new System.Windows.Forms.Padding(4);
            this.CutpTariff.Size = new System.Drawing.Size(800, 480);
            this.CutpTariff.TabIndex = 26;
            this.CutpTariff.Text = "计费";
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(572, 3);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(85, 30);
            this.BtnOk.TabIndex = 32;
            this.BtnOk.Text = "选择";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // CFormTariff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 571);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.CutpTariff);
            this.Controls.Add(this.CboTariffDescp);
            this.Controls.Add(this.LblTariffDescp);
            this.Controls.Add(this.BtnModify);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.BtnDeleteSel);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CFormTariff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "收费标准";
            this.Load += new System.EventHandler(this.CFormTariff_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnDeleteSel;
        private System.Windows.Forms.Button BtnModify;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Label LblTariffDescp;
        private System.Windows.Forms.ComboBox CboTariffDescp;
        private CustomControlLib.CUserTariffPanel CutpTariff;
        private System.Windows.Forms.Button BtnOk;

    }
}