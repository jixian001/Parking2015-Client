using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;

namespace WindowsFormLib
{
    public partial class CFormDeviceFault : Form
    {
        // 各库设备故障汇总TabPage
        private List<TabPage> m_ltpWareHouse = new List<TabPage>();
        private ToolTip m_toolTip = new ToolTip();

        public CFormDeviceFault()
        {           
            HandInitalizeComponent();
            this.AutoScroll = true;
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
        /// 窗体首次显示时触发(窗体大小改变触发 OnResize  OnShown)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Maximized)
            //{// 最大化状态时
            int minGap = CStaticClass.ConfigMinGap();
            int nGap = 4 * minGap;
            int nLabelWidth = CStaticClass.ConfigFaultLabelWidth();
            int nLabelHight = CStaticClass.ConfigFaultLabelHeight();
            int gap = CStaticClass.ConfigMinGap();

            //int nWidth = Math.Max(this.TabDeviceFault.Width, this.Width - nGap);
            //int nHeight = Math.Max(this.TabDeviceFault.Height, this.Height - 2 * nGap);
            //int nTabHeight = Math.Max(this.TabDeviceFault.Height - 3 * nGap, this.Height - 3 * nGap);
            this.TabDeviceFault.Size = new Size(this.Width - nGap, this.Height - 2 * nGap);
            Size sTabSize = new Size(this.Width - nGap, this.Height - 3 * nGap);
            foreach (TabPage tp in this.TabDeviceFault.Controls)
            {
                tp.Size = sTabSize;
                //int nIndex = 0;
                //foreach (Label label in tp.Controls)
                //{
                //    int nRowCount = (this.TabDeviceFault.Height - 2 * gap) / (nLabelHight + gap) - 1;
                //    int row = (nIndex) % nRowCount;
                //    int column = (nIndex++) / nRowCount;
                //    int nx = column == 0 ? gap : column * nLabelWidth + (column + 1) * gap;
                //    int ny = row == 0 ? gap : row * nLabelHight + (row + 1) * gap;
                //    label.Location = new System.Drawing.Point(nx, ny);
                //}
            }
            //}
        }

        #region 公有函数
        /// <summary>
        /// 更新设备故障
        /// </summary>
        /// <param name="nWareHouse"></param>
        /// <param name="nDeviceID"></param>
        public void UpdateDeviceFault(int nWareHouse, int nDeviceID)
        {
            string strName = CStaticClass.ConvertWareHouse(nWareHouse) + nDeviceID + "#";// nWareHouse + "#库" + nDeviceID + "#";
           
            TabPage selectTab = m_ltpWareHouse.Find(a => a.Name.Contains(strName));

            if (null != selectTab)
            {
                this.TabDeviceFault.SelectTab(selectTab);
                #region
                //}
                //else
                //{
                //    TabPage tp = new TabPage();
                //    tp.AutoScroll = true;
                //    tp.Font = new System.Drawing.Font("宋体", 9F);
                //    tp.Name = strName;
                //    if (0 < nDeviceID && 10 > nDeviceID)
                //    {
                //        tp.Text = strName + "ETV";
                //    }
                //    else if (10 < nDeviceID && 20 > nDeviceID)
                //    {
                //        tp.Text = strName + "车厅";
                //    }
                //    else if (20 < nDeviceID && 30 > nDeviceID)
                //    {
                //        tp.Text = strName + "TV";
                //    }

                //    SetTabPageLayout(tp, nWareHouse, nDeviceID);
                //    m_ltpWareHouse.Add(tp);
                //    this.TabDeviceFault.Controls.Add(tp);
                //    this.TabDeviceFault.SelectTab(tp);
                //}
                #endregion
                UpdateFaultColor(this.TabDeviceFault.SelectedTab, nWareHouse, nDeviceID);
            }
        }

        /// <summary>
        /// 更新单个label设备故障
        /// </summary>
        /// <param name="deviceFault"></param>
        public void UpdateDeviceFault(CarLocationPanelLib.PushService.CDeviceFaultDto deviceFault)
        {
            int nWareHouse = (int)deviceFault.warehouse;
            int nDeviceID = (int)deviceFault.devicecode;
            string strName = CStaticClass.ConvertWareHouse(nWareHouse) + nDeviceID + "#";// nWareHouse + "#库" + nDeviceID + "#";
            TabPage selectTab = m_ltpWareHouse.Find(a => a.Name.Contains(strName));

            if (null == selectTab)
            {
                TabPage tp = new TabPage();
                tp.AutoScroll = true;
                tp.Font = new System.Drawing.Font("宋体", 9F);
                tp.Name = strName;
                if (0 < nDeviceID && 10 > nDeviceID)
                {
                    tp.Text = strName + "ETV";
                }
                else if (10 < nDeviceID && 20 > nDeviceID)
                {
                    tp.Text = strName + "车厅";
                }
                else if (20 < nDeviceID && 30 > nDeviceID)
                {
                    tp.Text = strName + "TV";
                }

                SetTabPageLayout(tp, nWareHouse, nDeviceID);
                m_ltpWareHouse.Add(tp);
                this.TabDeviceFault.Controls.Add(tp);
                selectTab = tp;
                //this.TabDeviceFault.SelectTab(tp);
            }

            UpdateFaultColor(selectTab, deviceFault);
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 手动初始化
        /// </summary>
        /// <param name="nWareHouseCount"></param>
        private void HandInitalizeComponent()
        {
            //try
            //{
                InitializeComponent();
                this.TabDeviceFault.Font = new System.Drawing.Font("微软雅黑", 12F);

                List<object> lstWareHouse = CStaticClass.ConfigLstWareHouse();

                foreach (object objWH in lstWareHouse)
                {
                    if (typeof(int) != objWH.GetType())
                    {
                        continue;
                    }

                    int i = (int)objWH;
                    List<object> lstHall = CStaticClass.ConfigLstHallDeviceID(i);// 根据库号获取对应所有车厅设备号
                    List<object> lstETV = CStaticClass.ConfigLstETVOrTVDeviceID(i);// 根据库号获取对应所有ETV设备号

                    // 添加车厅故障汇总TabPage布局
                    foreach (object obj in lstHall)
                    {
                        TabPage tp = new TabPage();
                        tp.AutoScroll = true;
                        tp.Font = new System.Drawing.Font("宋体", 9F);
                        string strDescp = CStaticClass.ConvertWareHouse(i);// +CStaticClass.ConvertHallDescp(obj);
                        tp.Name = strDescp + (int)obj + "#";// i + "#库" + (int)obj + "#";
                        //tp.Text = strDescp + CStaticClass.ConvertHallDescp(i, obj);// i + "#库" + (int)obj + "#车厅";
                        tp.Text = CStaticClass.ConvertHallDescp(i, obj);                        
                        SetTabPageLayout(tp, i, (int)obj);
                        m_ltpWareHouse.Add(tp);
                        this.TabDeviceFault.Controls.Add(tp);
                    }

                    foreach (object obj in lstETV)
                    {
                        TabPage tp = new TabPage();
                        tp.AutoScroll = true;
                        tp.Font = new System.Drawing.Font("宋体", 9F);
                        string strDescp = CStaticClass.ConvertWareHouse(i);// +CStaticClass.ConvertETVDescp(obj);
                        tp.Name = strDescp + (int)obj + "#";// i + "#库" + (int)obj + "#";
                        //tp.Text = strDescp + CStaticClass.ConvertETVDescp(obj);// i + "#库" + (int)obj + "#ETV";
                        tp.Text = CStaticClass.ConvertETVDescp(obj);// i + "#库" + (int)obj + "#ETV";
                        SetTabPageLayout(tp, i, (int)obj);
                        m_ltpWareHouse.Add(tp);
                        this.TabDeviceFault.Controls.Add(tp);
                    }
                }
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        /// <summary>
        /// 设置设备TabPage布局
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="nWareHouse"></param>
        /// <param name="nDeviceID"></param>
        private void SetTabPageLayout(TabPage tabPage, int nWareHouse, int nDeviceID)
        {
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            //try
            //{
                int nLabelWidth = CStaticClass.ConfigFaultLabelWidth();
                int nLabelHight = CStaticClass.ConfigFaultLabelHeight();
                int gap = CStaticClass.ConfigMinGap();
                int nHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - 6 * gap;
                int nRowCount = (nHeight - 2 * gap) / (nLabelHight + gap) - 1;
                // 
                // tabPage
                // 
                tabPage.Location = new System.Drawing.Point(0, 0);
                tabPage.Padding = new System.Windows.Forms.Padding(3);
                tabPage.UseVisualStyleBackColor = true;
                tabPage.BackColor = System.Drawing.SystemColors.Control;

                // 根据库号和设备号获取所有故障列表（除内容为“备用”）
                List<CarLocationPanelLib.QueryService.CDeviceFaultDto> lstDeviceFault = proxy.GetDeviceFault(nWareHouse, nDeviceID, "备用");

                for (int i = 0; i < lstDeviceFault.Count; i++)
                {
                    CarLocationPanelLib.QueryService.CDeviceFaultDto deviceFault = lstDeviceFault[i];

                    if (null == deviceFault)
                    {
                        continue;
                    }

                    int row = i % nRowCount;
                    int column = i / nRowCount;
                    // 
                    // label
                    // 
                    int nx = column == 0 ? gap : column * nLabelWidth + (column + 1) * gap;
                    int ny = row == 0 ? gap : row * nLabelHight + (row + 1) * gap;
                    Label label = new Label();
                    label.Name = "L" + deviceFault.faultaddress;
                    label.BackColor = System.Drawing.Color.LightSkyBlue;
                    label.Location = new System.Drawing.Point(nx, ny);
                    label.Size = new System.Drawing.Size(nLabelWidth, nLabelHight);
                    label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    label.Text = deviceFault.faultdescp;
                    m_toolTip.SetToolTip(label, deviceFault.faultdescp);
                    tabPage.Controls.Add(label);

                    if (this.TabDeviceFault.Width < nx + nLabelWidth + gap)
                    {
                        this.TabDeviceFault.Width = nx + nLabelWidth + gap;
                        //this.ClientSize = this.TabDeviceFault.Size;
                    }

                    switch (deviceFault.color)
                    {
                        case 4:
                            label.BackColor = Color.Cyan;
                            break;
                        case 1:
                            label.BackColor = Color.Red;
                            break;
                        case 2:
                            label.BackColor = Color.Yellow;
                            break;
                        case 3:
                            label.BackColor = Color.Green;
                            break;
                        default:
                            label.BackColor = Color.LightSkyBlue;
                            break;
                    }
                }
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            proxy.Close();
        }

        /// <summary>
        /// 更新设备故障颜色
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="nWareHouse"></param>
        /// <param name="nDeviceID"></param>
        private void UpdateFaultColor(TabPage tabPage, int nWareHouse, int nDeviceID)
        {
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            //try
            //{
                // 根据库号和设备号获取所有故障列表（除内容为“备用”）
                List<CarLocationPanelLib.QueryService.CDeviceFaultDto> lstDeviceFault = proxy.GetDeviceFault(nWareHouse, nDeviceID, "备用");

                if (null == lstDeviceFault)
                {
                    //无状态
                    MessageBox.Show("无状态", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (Control label in tabPage.Controls)
                {
                    int nPLCAddr;
                    int.TryParse(label.Name.Substring(1), out nPLCAddr);
                    CarLocationPanelLib.QueryService.CDeviceFaultDto deviceFault = lstDeviceFault.Find(a => a.faultaddress == nPLCAddr);

                    if (null != deviceFault)
                    {
                        switch (deviceFault.color)
                        {
                            case 4:
                                label.BackColor = Color.Cyan;
                                break;
                            case 1:
                                label.BackColor = Color.Red;
                                break;
                            case 2:
                                label.BackColor = Color.Yellow;
                                break;
                            case 3:
                                label.BackColor = Color.Green;
                                break;
                            default:
                                label.BackColor = Color.LightSkyBlue;
                                break;
                        }
                    }
                    else
                    {
                        label.BackColor = Color.LightSkyBlue;
                    }
                }
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            proxy.Close();
        }
      
        /// <summary>
        /// 更新单个label设备故障颜色
        /// </summary>
        /// <param name="tabPage"></param>
        /// <param name="deviceFault"></param>
        private void UpdateFaultColor(TabPage tabPage, CarLocationPanelLib.PushService.CDeviceFaultDto deviceFault)
        {
            //try
            //{
                if (null == deviceFault || null == tabPage)
                {
                    return;
                }

                foreach (Control label in tabPage.Controls)
                {
                    int nPLCAddr;
                    int.TryParse(label.Name.Substring(1), out nPLCAddr);
                    if (deviceFault.faultaddress != nPLCAddr)
                    {
                        continue;
                    }

                    switch (deviceFault.color)
                    {
                        case 4:
                            label.BackColor = Color.Cyan;
                            break;
                        case 1:
                            label.BackColor = Color.Red;
                            break;
                        case 2:
                            label.BackColor = Color.Yellow;
                            break;
                        case 3:
                            label.BackColor = Color.Green;
                            break;
                        default:
                            label.BackColor = Color.LightSkyBlue;
                            break;
                    }

                    return;
                }
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        #endregion
    }
}
