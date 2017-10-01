using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;

namespace WindowsFormLib
{
    public partial class CFormHall : Form
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
        /// 车厅信息
        /// </summary>
        public CFormHall(CFormDeviceFault formDeviceFault)
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
        /// 填充车厅信息界面
        /// </summary>
        /// <param name="carLocation"></param>
        public void FillFormHall(CDeviceStatusDto hallStatusTable)
        {
            m_equipDeviceStatus = hallStatusTable;
            if (null == hallStatusTable)
            {
                MessageBox.Show("没有指定设备！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //this.Close();
                return;
            }

            int side;
            int.TryParse(hallStatusTable.deviceaddr.Substring(0, 1), out side);
            int column;
            int.TryParse(hallStatusTable.deviceaddr.Substring(1, 2), out column);
            int layer;
            int.TryParse(hallStatusTable.deviceaddr.Substring(3, 2), out layer);
            string strAvailable = "可接受新指令";

            if (0 == hallStatusTable.isavailable)
            {
                strAvailable = "不可接受新指令";
            }

            this.TxtWareHouse.Text = CStaticClass.ConvertWareHouse(hallStatusTable.warehouse);
            this.TxtCurTask.Text = CStaticClass.ConvertTaskType(hallStatusTable.tasktype);// 当前作业
            this.TxtHallAddr.Text = side + "边" + column + "列" + layer + "层"+" (当前层: "+hallStatusTable.devicelayer+"层)";
            this.TxtEquipID.Text = CStaticClass.ConvertHallDescp(hallStatusTable.warehouse, hallStatusTable.devicecode);
            this.TxtHallMode.Text = CStaticClass.ConvertDeviceMode(hallStatusTable.devicemode);
            this.TxtHallType.Text = CStaticClass.ConvertHallType(hallStatusTable.halltype);
            this.TxtCurTaskStatus.Text = CStaticClass.ConvertFlowNodeDescpType(hallStatusTable.currentnode); // 当前作业状态（详细情况 如：有车入库）
            this.TxtIsAvailable.Text = strAvailable;
            this.TxtICCardID.Text = hallStatusTable.iccode;
            this.TxtEntryAutoStep.Text = hallStatusTable.instep.ToString();
            this.TxtExitAutoStep.Text = hallStatusTable.outstep.ToString();

            if (hallStatusTable.tasktype == (int)EnmTaskType.Init) 
            {
                this.TxtCurTaskStatus.Text = "";
            }
        }

        /// <summary>
        /// 车厅状态故障汇总
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
                int nHallID = CStaticClass.ConvertHallDescp(nWareHouse, this.TxtEquipID.Text);
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
