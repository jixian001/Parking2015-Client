using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib;

namespace WindowsFormLib
{
    public partial class CFormCarSize : Form
    {
        public CFormCarSize()
        {
            InitializeComponent();
            this.LblMinCar.Click += new EventHandler(LblMinCar_Click);
            this.LblMidCar.Click += new EventHandler(LblMidCar_Click);
            this.LblMaxCar.Click += new EventHandler(LblMaxCar_Click);
            List<string> lstCarSize = CStaticClass.ConfigLstProjectSize();
            HandLayout(lstCarSize);
            // 设置键盘“Esc”按钮
            this.CancelButton = this.BtnCancel;
        }

        /// <summary>
        /// 点击键盘“Esc”关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LblMinCar_Click(object sender, EventArgs e)
        {

        }

        private void LblMidCar_Click(object sender, EventArgs e)
        {

        }

        private void LblMaxCar_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 单击尺寸选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label_Click(object sender, EventArgs e)
        {
            if (null != this.Owner && this.Owner.GetType() == typeof(CFormCustomer))
            {
                ((CFormCustomer)this.Owner).SetReserveCarSize(((Label)sender).Text.Trim());
                this.Close();
            }
        }

        /// <summary>
        /// 手动布局
        /// </summary>
        /// <param name="lstCarSize"></param>
        private void HandLayout(List<string> lstCarSize)
        {
            int nCurWidth = 3, nCurHeight = 20;
            int nGap = 12;

            for (int i = 0; i < lstCarSize.Count; i++ )
            {
                string str = lstCarSize[i].Replace('（', '(');
                str = str.Replace('）', ' ').Trim();
                string[] strs = str.Split('(');
                string strText = string.Empty;
                string strToolTip = string.Empty;
                if (0 < strs.Count())
                {
                    strText = strs[0];
                } 
                if (1 < strs.Count())
                {
                    strToolTip = strs[1];
                }
                Label label = new Label();
                this.toolTip1.SetToolTip(label, strToolTip);
                label.Click += new EventHandler(label_Click);
                label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label.Name = "label" + (i + 1);
                label.Size = new System.Drawing.Size(45, 30);
                label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                label.Location = new System.Drawing.Point(nCurWidth, nCurHeight);
                label.Text = strText;
                nCurWidth += label.Width + nGap;

                if (nCurWidth + label.Width + nGap >= this.groupBox1.Width)
                {
                    nCurWidth = 3;
                    nCurHeight += label.Height + nGap;
                }

                this.groupBox1.Controls.Add(label);
            }
        }
    }
}
