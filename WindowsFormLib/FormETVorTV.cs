using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;

namespace WindowsFormLib
{
    public partial class CFormETVorTV : Form
    {
        private CFormDeviceFault m_formDeviceFault = null;
        private CDeviceStatusDto m_equipDeviceStatus = null;
        /// <summary>
        /// 属性-设备对象
        /// </summary>
        public CDeviceStatusDto EquipDeviceStatus
        {
            get
            {
                return m_equipDeviceStatus;
            }
            set
            {
                m_equipDeviceStatus = value;
            }
        }

        /// <summary>
        /// ETV或者TV信息
        /// </summary>
        public CFormETVorTV(CFormDeviceFault formDeviceFault)
        {
            m_formDeviceFault = formDeviceFault;
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
        /// 填充ETV或者TV信息界面
        /// </summary>
        /// <param name="carLocation"></param>
        public void FillFormEquip(CDeviceStatusDto equipStatusTable)
        {
            m_equipDeviceStatus = equipStatusTable;
            if (null == equipStatusTable)
            {
                MessageBox.Show("没有指定设备！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            int side = 0;
            int column = 0;
            int layer = 0;
            if (null != equipStatusTable.deviceaddr)
            {
                if (0 < equipStatusTable.deviceaddr.Length)
                {
                    int.TryParse(equipStatusTable.deviceaddr.Substring(0, 1), out side);
                }

                if (2 < equipStatusTable.deviceaddr.Length)
                { 
                    int.TryParse(equipStatusTable.deviceaddr.Substring(1, 2), out column); 
                }
            }

            if (null != equipStatusTable.devicelayer)
            {
                layer = (int)equipStatusTable.devicelayer;
            }

            string strAble = "可用";
            string strAvailable = "可接受指令";

            if (0 == equipStatusTable.isable)
            {
                strAble = "不可用";
            } 

            if (0 == equipStatusTable.isavailable)
            {
                strAvailable = "不可接受指令";
            }

            this.TxtWareHouse.Text = CStaticClass.ConvertWareHouse(equipStatusTable.warehouse);
            this.TxtICCardID.Text = equipStatusTable.iccode;
            this.TxtCurTask.Text = CStaticClass.ConvertTaskType(equipStatusTable.tasktype);// 当前作业
            this.TxtEquipAddr.Text = side + "边，" + column + "列，" + layer + "层"; // equipStatusTable.deviceaddr;
            if ((int)EnmSMGType.ETV != equipStatusTable.devicetype)
            {
                this.TxtEquipID.Text = CStaticClass.ConvertHallDescp(equipStatusTable.warehouse, equipStatusTable.devicecode);
            }
            else
            { 
                this.TxtEquipID.Text = CStaticClass.ConvertETVDescp(equipStatusTable.devicecode);
            }
            this.TxtEquipMode.Text = CStaticClass.ConvertDeviceMode(equipStatusTable.devicemode);
            this.TxtCurTaskStatus.Text = CStaticClass.ConvertFlowNodeDescpType(equipStatusTable.currentnode); ;// 当前作业状态（详细情况 如：正在装载）
            this.TxtIsable.Text = strAble;
            this.TxtIsavailable.Text = strAvailable;
            this.TxtRunStep.Text = equipStatusTable.runstep.ToString();
            this.TxtLoadStep.Text = equipStatusTable.instep.ToString();
            this.TxtUnloadStep.Text = equipStatusTable.outstep.ToString();
            if (equipStatusTable.tasktype == (int)EnmTaskType.Init) 
            {
                this.TxtCurTaskStatus.Text = "";
            }
        }

        /// <summary>
        /// ETV或者TV状态故障汇总
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeviceFault_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.TxtWareHouse.Text) || string.IsNullOrEmpty(this.TxtEquipID.Text))
                {
                    MessageBox.Show("库区，设备为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nWareHouse = CStaticClass.ConvertWareHouse(this.TxtWareHouse.Text);
                int nHallID =Convert.ToInt32( this.TxtEquipID.Text.Substring(0,2));
                if(0 >= nHallID)
                {
                    nHallID = CStaticClass.ConvertHallDescp(this.TxtEquipID.Text);
                }
                m_formDeviceFault.UpdateDeviceFault(nWareHouse, nHallID);
                m_formDeviceFault.ShowDialog();
            }
            catch (TimeoutException)
            {
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FaultException exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (CommunicationException exception)
            {
                MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
