using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using System.Drawing;
using CarLocationPanelLib;

namespace CustomControlLib
{
    public class CUserCustomerBillInfoPanel : CUserCustomerInfoPanel
    {
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        protected System.Windows.Forms.DataGridViewCheckBoxColumn CheckBoxS;
        protected System.Windows.Forms.CheckBox CbSelectAllSound;
        protected System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1;
        protected System.Windows.Forms.Button BtnBatchModify;

        public CUserCustomerBillInfoPanel()
            : base()
        { }

        #region 重载函数
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected override void InitializeComponent()
        {
            this.GbxCustomerList = new System.Windows.Forms.GroupBox();
            this.CupttsUers = new CustomControlLib.CUserPageTurnToolStrip();
            this.DgvCustomer = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckBoxS = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CbSelectAllSound = new System.Windows.Forms.CheckBox();
            this.dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GbxCustomerFind = new System.Windows.Forms.GroupBox();
            this.CboCustomerData = new System.Windows.Forms.ComboBox();
            this.BtnCustomerFind = new System.Windows.Forms.Button();
            this.BtnCustomerDelete = new System.Windows.Forms.Button();
            this.BtnCustomerAdd = new System.Windows.Forms.Button();
            this.BtnCustomerModify = new System.Windows.Forms.Button();
            this.CTxtCustomerData = new CustomControlLib.CUserTextButton();
            this.CboCustomerCondition = new System.Windows.Forms.ComboBox();
            this.LblCustomerData = new System.Windows.Forms.Label();
            this.LblCustomerCondition = new System.Windows.Forms.Label();
            this.BtnBatchModify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.GbxCustomerList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCustomer)).BeginInit();
            this.GbxCustomerFind.SuspendLayout();
            // 
            // tabPage1
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.GbxCustomerList);
            this.Controls.Add(this.GbxCustomerFind);
            this.Location = new System.Drawing.Point(4, 26);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "tabPage1";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(841, 487);
            this.TabIndex = 0;
            this.Text = "车主管理";
            // 
            // GbxCustomerList
            // 
            this.GbxCustomerList.Controls.Add(this.BtnBatchModify);
            this.GbxCustomerList.Controls.Add(this.CbSelectAllSound);
            this.GbxCustomerList.Controls.Add(this.CupttsUers);
            this.GbxCustomerList.Controls.Add(this.DgvCustomer);
            this.GbxCustomerList.Location = new System.Drawing.Point(0, 140);
            this.GbxCustomerList.Margin = new System.Windows.Forms.Padding(4);
            this.GbxCustomerList.Name = "GbxCustomerList";
            this.GbxCustomerList.Padding = new System.Windows.Forms.Padding(4);
            this.GbxCustomerList.Size = new System.Drawing.Size(837, 347);
            this.GbxCustomerList.TabIndex = 1;
            this.GbxCustomerList.TabStop = false;
            this.GbxCustomerList.Text = "车主列表";
            // 
            // BtnSaveSound
            // 
            this.BtnBatchModify.Location = new System.Drawing.Point(100, 20);
            this.BtnBatchModify.Margin = new System.Windows.Forms.Padding(4);
            this.BtnBatchModify.Name = "BtnBatchModify";
            this.BtnBatchModify.Size = new System.Drawing.Size(100, 30);
            this.BtnBatchModify.TabIndex = 9;
            this.BtnBatchModify.Text = "批量修改";
            this.BtnBatchModify.UseVisualStyleBackColor = true;
            this.BtnBatchModify.Click += new System.EventHandler(this.BtnBatchModify_Click);
            // 
            // CupttsUers
            // 
            this.CupttsUers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CupttsUers.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CupttsUers.Location = new System.Drawing.Point(4, 354);
            this.CupttsUers.Name = "CupttsUers";
            this.CupttsUers.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.CupttsUers.Size = new System.Drawing.Size(829, 33);
            this.CupttsUers.TabIndex = 8;
            this.CupttsUers.Tag = this.DgvCustomer;
            this.CupttsUers.Text = "CupttsUers";
            // 
            // DgvCustomer
            // 
            this.DgvCustomer.AllowUserToAddRows = false;
            this.DgvCustomer.AllowUserToResizeRows = false;
            this.DgvCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvCustomer.AutoGenerateColumns = false;
            this.DgvCustomer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column9,
            this.Column8,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.CheckBoxS});
            this.DgvCustomer.Location = new System.Drawing.Point(3, 60);
            this.DgvCustomer.Name = "DgvCustomer";
            this.DgvCustomer.RowHeadersVisible = false;
            this.DgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvCustomer.Size = new System.Drawing.Size(829, 270);
            this.DgvCustomer.TabIndex = 0;
            this.DgvCustomer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(base.DgvCustomer_CellDoubleClick);
            this.DgvCustomer.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(base.DgvCustomer_CellFormatting);
            this.DgvCustomer.DataSourceChanged += new EventHandler(this.CupttsUers.UpdateLayout);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "strName";
            //this.Column1.FillWeight = 72.63921F;
            this.Column1.HeaderText = "姓名";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "strICCardID";
            //this.Column2.FillWeight = 79.48557F;
            this.Column2.HeaderText = "用户卡号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "nICCardStatus";
            //this.Column3.FillWeight = 85.48165F;
            this.Column3.HeaderText = "卡状态";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "nICCardType";
            //this.Column4.FillWeight = 90.6097F;
            this.Column4.HeaderText = "卡类型";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "dtStartTime";
            this.Column12.FillWeight = 210.6097F;
            dataGridViewCellStyle1.Format = "yyyy-MM-dd HH:mm:ss";
            this.Column12.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column12.HeaderText = "起始日期";
            this.Column12.Name = "Column2";
            this.Column12.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "dtDeadLine";
            this.Column13.FillWeight = 210.6097F;
            this.Column13.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column13.HeaderText = "截止日期";
            this.Column13.Name = "Column3";
            this.Column13.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "nWareHouse";
            //this.Column5.FillWeight = 95.23381F;
            this.Column5.HeaderText = "库区";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "strCarPOSN";
            //this.Column6.FillWeight = 99.75758F;
            this.Column6.HeaderText = "分配车位";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "nPriorityID";
            //this.Column11.FillWeight = 99.75758F;
            this.Column11.HeaderText = "优先级";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "strTelphone";
            //this.Column7.FillWeight = 104.7255F;
            this.Column7.HeaderText = "住宅电话";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "strMobile";
            //this.Column9.FillWeight = 111.502F;
            this.Column9.HeaderText = "移动电话";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "strLicPlteNbr";
            //this.Column8.FillWeight = 121.858F;
            this.Column8.HeaderText = "车牌号";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "strAddress";
            //this.Column10.FillWeight = 138.7068F;
            this.Column10.HeaderText = "住址";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // CheckBoxS
            // 
            //this.CheckBoxS.DataPropertyName = "soundishand";
            //this.CheckBoxS.FillWeight = 46.08193F;
            this.CheckBoxS.HeaderText = "选择";
            this.CheckBoxS.Name = "CheckBoxS";
            this.CheckBoxS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CheckBoxS.TrueValue = true;
            this.CheckBoxS.FalseValue = false;
            // 
            // CbSelectAllSound
            // 
            this.CbSelectAllSound.AutoSize = true;
            this.CbSelectAllSound.Location = new System.Drawing.Point(900, 25);
            this.CbSelectAllSound.Margin = new System.Windows.Forms.Padding(4);
            this.CbSelectAllSound.Name = "CbSelectAllSound";
            this.CbSelectAllSound.Size = new System.Drawing.Size(30, 20);
            this.CbSelectAllSound.TabIndex = 10;
            this.CbSelectAllSound.Text = "全选";
            this.CbSelectAllSound.UseVisualStyleBackColor = true;
            this.CbSelectAllSound.CheckedChanged += new System.EventHandler(this.CbSelectAllSound_CheckedChanged);
            // 
            // GbxCustomerFind
            // 
            this.GbxCustomerFind.Controls.Add(this.CboCustomerData);
            this.GbxCustomerFind.Controls.Add(this.BtnCustomerFind);
            this.GbxCustomerFind.Controls.Add(this.BtnCustomerDelete);
            this.GbxCustomerFind.Controls.Add(this.BtnCustomerAdd);
            this.GbxCustomerFind.Controls.Add(this.BtnCustomerModify);
            this.GbxCustomerFind.Controls.Add(this.CTxtCustomerData);
            this.GbxCustomerFind.Controls.Add(this.CboCustomerCondition);
            this.GbxCustomerFind.Controls.Add(this.LblCustomerData);
            this.GbxCustomerFind.Controls.Add(this.LblCustomerCondition);
            this.GbxCustomerFind.Location = new System.Drawing.Point(0, 11);
            this.GbxCustomerFind.Margin = new System.Windows.Forms.Padding(4);
            this.GbxCustomerFind.Name = "GbxCustomerFind";
            this.GbxCustomerFind.Padding = new System.Windows.Forms.Padding(4);
            this.GbxCustomerFind.Size = new System.Drawing.Size(837, 120);
            this.GbxCustomerFind.TabIndex = 0;
            this.GbxCustomerFind.TabStop = false;
            this.GbxCustomerFind.Text = "车主查询";
            // 
            // CboCustomerData
            // 
            this.CboCustomerData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCustomerData.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CboCustomerData.FormattingEnabled = true;
            this.CboCustomerData.Location = new System.Drawing.Point(150, 81);
            this.CboCustomerData.Name = "CboCustomerData";
            this.CboCustomerData.Size = new System.Drawing.Size(240, 24);
            this.CboCustomerData.TabIndex = 8;
            this.CboCustomerData.Visible = false;
            // 
            // BtnCustomerFind
            // 
            this.BtnCustomerFind.Location = new System.Drawing.Point(524, 35);
            this.BtnCustomerFind.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCustomerFind.Name = "BtnCustomerFind";
            this.BtnCustomerFind.Size = new System.Drawing.Size(100, 31);
            this.BtnCustomerFind.TabIndex = 7;
            this.BtnCustomerFind.Text = "查询";
            this.BtnCustomerFind.UseVisualStyleBackColor = true;
            this.BtnCustomerFind.Click += new System.EventHandler(this.BtnCustomerFind_Click);
            // 
            // BtnCustomerClose
            // 
            this.BtnCustomerDelete.Location = new System.Drawing.Point(689, 81);
            this.BtnCustomerDelete.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCustomerDelete.Name = "BtnCustomerClose";
            this.BtnCustomerDelete.Size = new System.Drawing.Size(100, 31);
            this.BtnCustomerDelete.TabIndex = 6;
            this.BtnCustomerDelete.Text = "删除";
            this.BtnCustomerDelete.UseVisualStyleBackColor = true;
            this.BtnCustomerDelete.Click += new System.EventHandler(this.BtnCustomerDelete_Click);
            // 
            // BtnCustomerAdd
            // 
            this.BtnCustomerAdd.Location = new System.Drawing.Point(524, 81);
            this.BtnCustomerAdd.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCustomerAdd.Name = "BtnCustomerAdd";
            this.BtnCustomerAdd.Size = new System.Drawing.Size(100, 31);
            this.BtnCustomerAdd.TabIndex = 5;
            this.BtnCustomerAdd.Text = "添加";
            this.BtnCustomerAdd.UseVisualStyleBackColor = true;
            this.BtnCustomerAdd.Click += new System.EventHandler(this.BtnCustomerAdd_Click);
            // 
            // BtnCustomerModify
            // 
            this.BtnCustomerModify.Location = new System.Drawing.Point(689, 35);
            this.BtnCustomerModify.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCustomerModify.Name = "BtnCustomerModify";
            this.BtnCustomerModify.Size = new System.Drawing.Size(100, 31);
            this.BtnCustomerModify.TabIndex = 4;
            this.BtnCustomerModify.Text = "修改";
            this.BtnCustomerModify.UseVisualStyleBackColor = true;
            this.BtnCustomerModify.Click += new System.EventHandler(this.BtnCustomerModify_Click);
            // 
            // CTxtCustomerData
            // 
            this.CTxtCustomerData.Enabled = false;
            this.CTxtCustomerData.EnabledButton = false;
            this.CTxtCustomerData.EnmTxtType = CustomControlLib.EnmTxtBoxType.Init;
            this.CTxtCustomerData.ImageButton = null;
            this.CTxtCustomerData.Location = new System.Drawing.Point(150, 81);
            this.CTxtCustomerData.Margin = new System.Windows.Forms.Padding(4);
            this.CTxtCustomerData.Name = "CTxtCustomerData";
            this.CTxtCustomerData.Size = new System.Drawing.Size(240, 26);
            this.CTxtCustomerData.TabIndex = 3;
            //this.CTxtCustomerData.CallbackTextButtonEvent += BtnCloseClick
            // 
            // CboCustomerCondition
            // 
            this.CboCustomerCondition.FormattingEnabled = true;
            this.CboCustomerCondition.Items.AddRange(new object[] {
            "用户卡号",
            "库区",
            "卡类型",
            "卡状态",
            //"分配车位",
            "姓名",
            "移动电话",
            "车牌号",
            "所有"});
            this.CboCustomerCondition.Location = new System.Drawing.Point(150, 35);
            this.CboCustomerCondition.Margin = new System.Windows.Forms.Padding(4);
            this.CboCustomerCondition.Name = "CboCustomerCondition";
            this.CboCustomerCondition.Size = new System.Drawing.Size(240, 24);
            this.CboCustomerCondition.TabIndex = 2;
            this.CboCustomerCondition.Text = "所有";
            this.CboCustomerCondition.SelectedIndexChanged += new EventHandler(CboCustomerCondition_SelectedIndexChanged);
            this.CboCustomerCondition.DropDownStyle = ComboBoxStyle.DropDownList;
            this.CboCustomerCondition.FlatStyle = FlatStyle.Popup;
            // 
            // LblCustomerData
            // 
            this.LblCustomerData.Location = new System.Drawing.Point(3, 81);
            this.LblCustomerData.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblCustomerData.Name = "label2";
            this.LblCustomerData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblCustomerData.Size = new System.Drawing.Size(147, 27);
            this.LblCustomerData.TabIndex = 1;
            this.LblCustomerData.Text = "查询数据";
            // 
            // LblCustomerCondition
            // 
            this.LblCustomerCondition.Location = new System.Drawing.Point(3, 35);
            this.LblCustomerCondition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblCustomerCondition.Name = "label1";
            this.LblCustomerCondition.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblCustomerCondition.Size = new System.Drawing.Size(147, 27);
            this.LblCustomerCondition.TabIndex = 0;
            this.LblCustomerCondition.Text = "查询条件";


            this.ResumeLayout(false);
            this.GbxCustomerList.ResumeLayout(false);
            this.GbxCustomerList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvCustomer)).EndInit();
            this.GbxCustomerFind.ResumeLayout(false);
            this.GbxCustomerFind.PerformLayout();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 窗体首次显示时触发(窗体大小改变触发 OnShown-OnResize)
        /// </summary>
        /// <param name="e"></param>
        public override void OnResize()
        {
            int width = this.ClientSize.Width;// > CStaticClass.ConfigMinWidth() ? this.ClientSize.Width : CStaticClass.ConfigMinWidth();
            int height = this.ClientSize.Height;// > CStaticClass.ConfigMinHeight() ? this.ClientSize.Height : CStaticClass.ConfigMinHeight();
            int gap = CStaticClass.ConfigMainGap();
            int minGap = CStaticClass.ConfigMinGap();

            // 车主查找
            int nFindHeight = this.GbxCustomerFind.Height;
            this.GbxCustomerFind.Size = new System.Drawing.Size(width, nFindHeight);
            int nWithLen = this.LblCustomerCondition.Width + this.CboCustomerCondition.Width + this.BtnCustomerFind.Width + this.BtnCustomerModify.Width + 6 * gap;
            int nLeft = Math.Max((width - nWithLen) / 2, minGap);
            int nTop = this.LblCustomerCondition.Location.Y;
            this.LblCustomerCondition.Location = new Point(nLeft, nTop);
            this.CboCustomerCondition.Location = new Point(LblCustomerCondition.Location.X + LblCustomerCondition.Width, nTop);
            this.BtnCustomerFind.Location = new Point(CboCustomerCondition.Location.X + CboCustomerCondition.Width + 3 * gap, nTop);
            this.BtnCustomerModify.Location = new Point(BtnCustomerFind.Location.X + BtnCustomerFind.Width + 3 * gap, nTop);
            nTop = this.LblCustomerData.Location.Y;
            this.LblCustomerData.Location = new Point(nLeft, nTop);
            this.CTxtCustomerData.Location = new Point(LblCustomerCondition.Location.X + LblCustomerCondition.Width, nTop);
            this.CboCustomerData.Location = new Point(LblCustomerCondition.Location.X + LblCustomerCondition.Width, nTop);
            this.BtnCustomerAdd.Location = new Point(CboCustomerCondition.Location.X + CboCustomerCondition.Width + 3 * gap, nTop);
            this.BtnCustomerDelete.Location = new Point(BtnCustomerFind.Location.X + BtnCustomerFind.Width + 3 * gap, nTop);

            // 车主列表 
            nTop = this.GbxCustomerList.Location.Y;
            this.GbxCustomerList.Size = new System.Drawing.Size(width, height - nTop);
            this.CbSelectAllSound.Location = new Point(width - this.CbSelectAllSound.Width, this.CbSelectAllSound.Location.Y);
            this.DgvCustomer.Size = new System.Drawing.Size(width, this.GbxCustomerList.Height - this.DgvCustomer.Location.Y - this.CupttsUers.Height - 2 * minGap);
            this.CupttsUers.Location = new Point(4, this.CbSelectAllSound.Location.Y + minGap);// 翻页
        }
        #endregion

        #region 车主信息
        /// <summary>
        /// 全选“选择”复选框选择否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbSelectAllSound_CheckedChanged(object sender, EventArgs e)
        {
            //modify by suhan 2015072
            //int flag = this.CbSelectAllSound.Checked ? 1 : 0;
            bool flag = this.CbSelectAllSound.Checked ? true : false;
            //modify over
            foreach (DataGridViewRow dgvr in this.DgvCustomer.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvr.Cells["CheckBoxS"];
                cell.Value = flag;
            }
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBatchModify_Click(object sender, EventArgs e)
        {
            List<string> lstICCardID = new List<string>();
            foreach (DataGridViewRow dgvr in this.DgvCustomer.Rows)
            {
			    //modify by suhan 2015072
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvr.Cells["CheckBoxS"];
                if (null != cell.Value && typeof(bool) == cell.Value.GetType() && (bool)cell.Value
                    && null != dgvr.DataBoundItem && typeof(struCustomerInfo) == dgvr.DataBoundItem.GetType())
                {// 选择项的定期卡、固定车位卡需要修改
                    lstICCardID.Add(((struCustomerInfo)dgvr.DataBoundItem).strICCardID);
                }
				//end by suhan 2015072
            }

            if (null == lstICCardID || 0 >= lstICCardID.Count)
            {
                MessageBox.Show("选择不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            UserForm form = new UserForm();
            form.SetLstICCardID(lstICCardID);
            form.ShowDialog();
            CUserCustomerInfoPanel_Load(sender, e);
            foreach (DataGridViewRow dgvr in this.DgvCustomer.Rows)
            {
                if (null == dgvr.DataBoundItem || typeof(struCustomerInfo) != dgvr.DataBoundItem.GetType())
                {
                    continue;
                }
                if (lstICCardID.Remove(((struCustomerInfo)dgvr.DataBoundItem).strICCardID))
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvr.Cells["CheckBoxS"];
                    cell.Value = true;
                }

                if (null == lstICCardID || 0 >= lstICCardID.Count)
                {
                    return;
                }
            }
        }
        #endregion

    }
}
