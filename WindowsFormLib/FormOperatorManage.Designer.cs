using CustomControlLib;
namespace WindowsFormLib
{
    partial class CFormOperatorManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormOperatorManage));
            this.DgvOperator = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CupttsOpt = new CustomControlLib.CUserPageTurnToolStrip();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnModify = new System.Windows.Forms.Button();
            this.BtnDeleteSel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvOperator)).BeginInit();
            this.SuspendLayout();
            // 
            // DgvOperator
            // 
            this.DgvOperator.AllowUserToAddRows = false;
            this.DgvOperator.AllowUserToResizeRows = false;
            this.DgvOperator.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvOperator.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1,
            this.Column6,
            this.Column4,
            this.Column5,
            this.Column3,
            this.Column7});
            this.DgvOperator.Location = new System.Drawing.Point(2, 36);
            this.DgvOperator.Name = "DgvOperator";
            this.DgvOperator.RowHeadersVisible = false;
            this.DgvOperator.RowTemplate.Height = 23;
            this.DgvOperator.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvOperator.Size = new System.Drawing.Size(540, 318);
            this.DgvOperator.TabIndex = 10;
            this.DgvOperator.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvOperator_CellDoubleClick);
            this.DgvOperator.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvOperator_CellFormatting);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "optcode";
            this.Column2.HeaderText = "账户";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "optname";
            this.Column1.HeaderText = "姓名";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "optpassword";
            this.Column6.HeaderText = "密码";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "optphone";
            this.Column4.HeaderText = "电话";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "optaddr";
            this.Column5.HeaderText = "住址";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "opttype";
            this.Column3.HeaderText = "类型";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "optpermission";
            this.Column7.HeaderText = "权限";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // CupttsOpt
            // 
            this.CupttsOpt.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CupttsOpt.ImageEndBtn = global::WindowsFormLib.Properties.Resources.endPage;
            this.CupttsOpt.ImageLeftBtn = global::WindowsFormLib.Properties.Resources.leftPage;
            this.CupttsOpt.ImageRightBtn = global::WindowsFormLib.Properties.Resources.rightPage;
            this.CupttsOpt.ImageStartBtn = global::WindowsFormLib.Properties.Resources.startPage;
            this.CupttsOpt.Location = new System.Drawing.Point(0, 0);
            this.CupttsOpt.Name = "CupttsOpt";
            this.CupttsOpt.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.CupttsOpt.Size = new System.Drawing.Size(546, 33);
            this.CupttsOpt.TabIndex = 11;
            this.CupttsOpt.Tag = this.DgvOperator;
            this.CupttsOpt.Text = "CupttsOpt";
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(84, 364);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(85, 30);
            this.BtnAdd.TabIndex = 12;
            this.BtnAdd.Text = "添加";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnModify
            // 
            this.BtnModify.Location = new System.Drawing.Point(214, 364);
            this.BtnModify.Name = "BtnModify";
            this.BtnModify.Size = new System.Drawing.Size(85, 30);
            this.BtnModify.TabIndex = 13;
            this.BtnModify.Text = "修改";
            this.BtnModify.UseVisualStyleBackColor = true;
            this.BtnModify.Click += new System.EventHandler(this.BtnModify_Click);
            // 
            // BtnDeleteSel
            // 
            this.BtnDeleteSel.Location = new System.Drawing.Point(344, 364);
            this.BtnDeleteSel.Name = "BtnDeleteSel";
            this.BtnDeleteSel.Size = new System.Drawing.Size(85, 30);
            this.BtnDeleteSel.TabIndex = 14;
            this.BtnDeleteSel.Text = "删除选择";
            this.BtnDeleteSel.UseVisualStyleBackColor = true;
            this.BtnDeleteSel.Click += new System.EventHandler(this.BtnDeleteSel_Click);
            // 
            // CFormOperatorManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 406);
            this.Controls.Add(this.BtnDeleteSel);
            this.Controls.Add(this.BtnModify);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.CupttsOpt);
            this.Controls.Add(this.DgvOperator);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CFormOperatorManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "操作员管理";
            this.Load += new System.EventHandler(this.CFormOperatorManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvOperator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CUserPageTurnToolStrip CupttsOpt;
        private System.Windows.Forms.DataGridView DgvOperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnModify;
        private System.Windows.Forms.Button BtnDeleteSel;

    }
}