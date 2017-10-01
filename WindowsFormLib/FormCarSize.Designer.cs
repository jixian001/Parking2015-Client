namespace WindowsFormLib
{
    partial class CFormCarSize
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
            this.components = new System.ComponentModel.Container();
            this.LblMinCar = new System.Windows.Forms.Label();
            this.LblMidCar = new System.Windows.Forms.Label();
            this.LblMaxCar = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LblTitle = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblMinCar
            // 
            this.LblMinCar.Location = new System.Drawing.Point(0, 9);
            this.LblMinCar.Name = "LblMinCar";
            this.LblMinCar.Size = new System.Drawing.Size(77, 26);
            this.LblMinCar.TabIndex = 0;
            this.LblMinCar.Text = "小车";
            this.LblMinCar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LblMinCar.Visible = false;
            // 
            // LblMidCar
            // 
            this.LblMidCar.Location = new System.Drawing.Point(86, 9);
            this.LblMidCar.Name = "LblMidCar";
            this.LblMidCar.Size = new System.Drawing.Size(77, 26);
            this.LblMidCar.TabIndex = 2;
            this.LblMidCar.Text = "中车";
            this.LblMidCar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LblMidCar.Visible = false;
            // 
            // LblMaxCar
            // 
            this.LblMaxCar.Location = new System.Drawing.Point(172, 9);
            this.LblMaxCar.Name = "LblMaxCar";
            this.LblMaxCar.Size = new System.Drawing.Size(77, 26);
            this.LblMaxCar.TabIndex = 4;
            this.LblMaxCar.Text = "大车";
            this.LblMaxCar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LblMaxCar.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(0, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 212);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // LblTitle
            // 
            this.LblTitle.Location = new System.Drawing.Point(6, 9);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(150, 26);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "车辆尺寸选择";
            // 
            // BtnCancel
            // 
            this.BtnCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BtnCancel.BackgroundImage = global::WindowsFormLib.Properties.Resources.close;
            this.BtnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnCancel.FlatAppearance.BorderSize = 0;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancel.ForeColor = System.Drawing.Color.Black;
            this.BtnCancel.Location = new System.Drawing.Point(260, 0);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(20, 20);
            this.BtnCancel.TabIndex = 6;
            this.BtnCancel.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.toolTip1.SetToolTip(this.BtnCancel, "关闭");
            this.BtnCancel.UseVisualStyleBackColor = false;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // CFormCarSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(282, 242);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LblMaxCar);
            this.Controls.Add(this.LblMidCar);
            this.Controls.Add(this.LblMinCar);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CFormCarSize";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "车辆尺寸选择";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblMinCar;
        private System.Windows.Forms.Label LblMidCar;
        private System.Windows.Forms.Label LblMaxCar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}