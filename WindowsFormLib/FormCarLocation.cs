using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;

namespace WindowsFormLib
{
    public partial class CFormCarLocation : Form
    {      
        /// <summary>
        /// 车位信息
        /// </summary>
        public CFormCarLocation()
        {
            InitializeComponent();
            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnCancel_Click);
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

        /// <summary>
        /// 填充车位信息界面
        /// </summary>
        /// <param name="carLocation"></param>
        public void FillFormCarLocation(CCarLocationDto carLocation, int nICType)
        {
            try
            {
                this.TxtWareHouse.Text = CStaticClass.ConvertWareHouse(carLocation.warehouse);
                this.TxtAddress.Text = carLocation.carlocside + "边，" + carLocation.carloccolumn + "列，" + carLocation.carloclayer + "层";
                this.TxtCarLocStatus.Text = CStaticClass.ConvertCarLocStatus(carLocation.carlocstatus);
                this.TxtUserID.Text = carLocation.iccode;
                this.TxtUserType.Text = CStaticClass.ConvertICCardType(nICType);
                this.TxtInTime.Text = carLocation.carintime.ToString();
                this.TxtCarLocSize.Text = carLocation.carlocsize;

                this.TxtCarSize.Text = carLocation.carsize;
                this.TxtCarWheelBase.Text = carLocation.carwheelbase == null ? "0" : carLocation.carwheelbase.ToString();
                this.txtCarLG.Text = carLocation.overallLg==null?"0":carLocation.overallLg.ToString();
                this.txtOverhang.Text = carLocation.overhang==null?"0":carLocation.overhang.ToString();
                this.txtWeight.Text = carLocation.carweight == null ? "0" : carLocation.carweight.ToString();
                this.txtOffCenter.Text = carLocation.offcenter == null ? "0" : carLocation.offcenter.ToString();
                this.txtRearwheel.Text = carLocation.rearwheeldis == null ? "0" : carLocation.rearwheeldis.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     
     }
}
