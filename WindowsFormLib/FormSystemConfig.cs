using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.PushService;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;

namespace WindowsFormLib
{
    public partial class CFormSystemConfig : Form
    {
        private  DataGridView DgvDeviceStatus = new System.Windows.Forms.DataGridView();

        public CFormSystemConfig()
        {
            InitializeComponent();
            InitDgvDeviceStatus();
            InitializeComboxItems();
            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnClose_Click);
            this.CboHallType.SelectedIndex = 0;

            InitGetCarOutStatus();
        }

        /// <summary>
        /// 更新某一设备是否可用状态值
        /// </summary>
        public void UpdateDeviceIsable(CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus)
        {
            if (null == deviceStatus)
            {
                return;
            }
            int nDeviceID = deviceStatus.devicecode;
            string strTag = deviceStatus.warehouse + "-" + nDeviceID;
            foreach (DataGridViewRow dgvr in DgvDeviceStatus.Rows)
            {
                if (null == dgvr || null == dgvr.Tag || dgvr.Tag.ToString() != nDeviceID.ToString())
                {// 行:设备号
                    continue;
                }
                bool isBreak = false;
                foreach (DataGridViewCell dgvc in dgvr.Cells)
                {
                    // 根据当前库车位信息列表设置车位状态
                    if (null == dgvc || null == dgvc.Tag)
                    {
                        continue;
                    }

                    if (null != deviceStatus.isable && strTag == dgvc.Tag.ToString())
                    {// 赋值单元格
                        if (0 != deviceStatus.isable)
                        {
                            dgvc.Value = "可用";
                        }
                        else
                        {
                            dgvc.Value = "不可用";
                        }
                        isBreak = true;
                        break;
                    }
                }

                if (isBreak)
                {
                    break;
                }
            }
        }

        #region button按钮单击触发事件
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDisable_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboEquipWareHouse.Text) || string.IsNullOrEmpty(this.CboEquipID.Text))
                {
                    MessageBox.Show("库区，设备都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int nEquipID = CStaticClass.ConvertETVDescp(this.CboEquipID.Text);
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboEquipWareHouse.Text);
                CarLocationPanelLib.PushService.EnmFaultType type = push.SetDeviceMode(nWareHouse, nEquipID, 0);
                
                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "禁用设备成功：设备号-" + nEquipID + " 库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("禁用设备成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                        {
                            MessageBox.Show("没有找到指定设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotAllowed:
                        {
                            MessageBox.Show("设备当前状态与要修改的状态相同!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("禁用设备失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                }
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
            proxy.Close();
            push.Close();
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnable_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(this.CboEquipWareHouse.Text) || string.IsNullOrEmpty(this.CboEquipID.Text))
                {
                    MessageBox.Show("库区，设备都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dr = MessageBox.Show("确认启用？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                int nEquipID = CStaticClass.ConvertETVDescp(this.CboEquipID.Text);
                int nWareHouse = CStaticClass.ConvertWareHouse(this.CboEquipWareHouse.Text);
                CarLocationPanelLib.PushService.EnmFaultType type = push.SetDeviceMode(nWareHouse, nEquipID, 1);
               
                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "启用设备成功：设备号-" + nEquipID + " 库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("启用设备成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                        {
                            MessageBox.Show("没有找到指定设备!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotAutomatic:
                        {
                            MessageBox.Show("设备非全自动模式不能启用!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotAllowed:
                        {
                            MessageBox.Show("设备当前状态与要修改的状态相同!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.InvalidWareHouseID:
                        {
                            MessageBox.Show("无效库区号!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Null:
                        {//下一取车队列的传入的参数卡号为空
                            MessageBox.Show("无取车排队队列!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("下一取车队列的IC卡没有制卡!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.LossORCancel:
                        {
                            MessageBox.Show("下一取车队列的IC卡注销或挂失!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotMatch:
                        {
                            MessageBox.Show("下一取车队列的IC卡类型不正确!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NoCarInGarage:
                        {
                            MessageBox.Show("下一取车队列的用户没有车存在库内!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToSendTelegram:
                        {
                            MessageBox.Show("下一取车队列发送报文失败, 请重新刷卡取车!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.WorkQueueNotEmpty:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "启用成功，正在为取车排队的车主取车，请稍后片刻：设备号-" + nEquipID + " 库号-" + nWareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("启用成功，正在为取车排队的车主取车，请稍后片刻!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("启用设备失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                }
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
            proxy.Close();
            push.Close();
        }

        /// <summary>
        /// 车厅设置确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                    if (string.IsNullOrEmpty(this.CboHallWareHouse.Text) || string.IsNullOrEmpty(this.CboHallID.Text) || 0 > this.CboHallType.SelectedIndex)
                    {
                        MessageBox.Show("库区，车厅设备以及车厅类型都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    DialogResult dr = MessageBox.Show("确定修改吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                    if (dr == DialogResult.OK)
                    {
                        int nWareHouse = CStaticClass.ConvertWareHouse(this.CboHallWareHouse.Text);
                        int nHallID = CStaticClass.ConvertHallDescp(nWareHouse, this.CboHallID.Text);
                        EnmHallType enmHallType = CStaticClass.ConvertHallType(this.CboHallType.Text);
                        CarLocationPanelLib.PushService.EnmFaultType type = push.SetHallType(nWareHouse, nHallID, enmHallType);

                        switch (type)
                        {
                            case CarLocationPanelLib.PushService.EnmFaultType.Success:
                                {
                                    MessageBox.Show("设置车厅类型成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    break;
                                }
                            case CarLocationPanelLib.PushService.EnmFaultType.NotFoundEquip:
                                {
                                    MessageBox.Show("没有找到指定的车厅!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                            case CarLocationPanelLib.PushService.EnmFaultType.WorkQueueNotEmpty:
                                {
                                    MessageBox.Show("有人排队取车，不允许修改车厅类型!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                            case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                                {
                                    MessageBox.Show("修改数据库失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                            case CarLocationPanelLib.PushService.EnmFaultType.InvalidWareHouseID:
                                {
                                    MessageBox.Show("无效库区号!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                            case CarLocationPanelLib.PushService.EnmFaultType.FailToSendTelegram:
                                {
                                    MessageBox.Show("发送报文失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                            case CarLocationPanelLib.PushService.EnmFaultType.FailConnection:
                                {
                                    MessageBox.Show("PLC网络连接中断，请检查网络连接再继续", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                            case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                                {
                                    MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                }
                            default:
                                {
                                    MessageBox.Show("设置车厅类型失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                        }
                    }
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
            proxy.Close();
            push.Close();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 组合框选项变化、列表单元格值变化触发事件
        /// <summary>
        /// 设备库区改变值时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboEquipWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.CboEquipID.Items.Clear();
                int nCurWareHouse = CStaticClass.ConvertWareHouse(this.CboEquipWareHouse.Text); ;
                List<object> lstETV = CStaticClass.ConfigLstETVOrTVDeviceIDDescp(nCurWareHouse);// 根据库号获取对应所有ETV设备号
                this.CboEquipID.Items.AddRange(lstETV.ToArray());
                if (0 < lstETV.Count)
                {
                    this.CboEquipID.SelectedIndex = 0;
                }
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
            proxy.Close();
        }

        /// <summary>
        /// 车厅库区改变值时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboHallWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                this.CboHallID.Items.Clear();
                int nCurWareHouse = CStaticClass.ConvertWareHouse(this.CboHallWareHouse.Text);
                List<object> lstHall = CStaticClass.ConfigLstHallDeviceIDDescp(nCurWareHouse);// 根据库号获取对应所有车厅设备号
                this.CboHallID.Items.AddRange(lstHall.ToArray());
                this.CboHallID.SelectedIndex = 0;
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
            proxy.Close();
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 初始化组合框集合值(库区)
        /// </summary>
        private void InitializeComboxItems()
        {
            this.CboEquipWareHouse.Items.Clear();
            this.CboHallWareHouse.Items.Clear();
            this.CboEquipWareHouse.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboHallWareHouse.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
            this.CboEquipWareHouse.SelectedIndex = 0;
            this.CboHallWareHouse.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化设备状态值列表
        /// </summary>
        private void InitDgvDeviceStatus()
        {
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return;
            }

             QueryServiceClient proxy = new QueryServiceClient();
             //try
             //{
                 // 
                 // DgvDeviceStatus
                 // 
                 ((System.ComponentModel.ISupportInitialize)(DgvDeviceStatus)).BeginInit();
                 DgvDeviceStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                 DgvDeviceStatus.Location = new System.Drawing.Point(7, 19);
                 DgvDeviceStatus.Name = "DgvDeviceStatus";
                 DgvDeviceStatus.RowTemplate.Height = 23;
                 DgvDeviceStatus.TabIndex = 7;
                 DgvDeviceStatus.AllowUserToAddRows = false;
                 DgvDeviceStatus.AllowUserToResizeColumns = false;
                 DgvDeviceStatus.AllowUserToResizeRows = false;
                 DgvDeviceStatus.ScrollBars = ScrollBars.Both;
                 DgvDeviceStatus.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
                 DgvDeviceStatus.EditMode = DataGridViewEditMode.EditProgrammatically;
                 DgvDeviceStatus.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                 //DgvDeviceStatus.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
                 DgvDeviceStatus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                 DgvDeviceStatus.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                 DgvDeviceStatus.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                 //DgvDeviceStatus.CellClick += new DataGridViewCellEventHandler(dgv_CellClick);
                 //DgvDeviceStatus.SelectionChanged += new EventHandler(dgv_SelectionChanged);
                 List<object> lstWareHouse = CStaticClass.ConfigLstWareHouse();
                 List<object> lstWareHouseDescp = CStaticClass.ConfigLstWareHouseDescp();
                 List<object> lstETV = new List<object>();
                 List<object> lstHall = new List<object>();
                 int nHeight = DgvDeviceStatus.ColumnHeadersHeight + 3;

                 for (int j = 0; j < lstWareHouse.Count; j++)
                 {// 增加列
                     lstETV.AddRange(CStaticClass.ConfigLstETVOrTVDeviceID(Convert.ToInt32(lstWareHouse[j])));
                     lstHall.AddRange(CStaticClass.ConfigLstHallDeviceID(Convert.ToInt32(lstWareHouse[j])));
                     DgvDeviceStatus.Columns.Add("w" + lstWareHouse[j], lstWareHouseDescp[j].ToString());// + "# 库");
                     DgvDeviceStatus.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                 }

                 // 去除重复的数据
                 lstETV = lstETV.Distinct().ToList();
                 lstHall = lstHall.Distinct().ToList();
                 for (int j = 0; j < lstETV.Count; j++)
                 {// 增加行
                     DgvDeviceStatus.Rows.Add();
                     DgvDeviceStatus.Rows[j].HeaderCell.Value = CStaticClass.ConvertETVDescp(lstETV[j]);// lstETV[j] + "# ETV";
                     DgvDeviceStatus.Rows[j].Tag = lstETV[j];
                     nHeight += DgvDeviceStatus.Rows[j].Height;
                     for(int k = 0;k < DgvDeviceStatus.ColumnCount;k++) 
                     {
                         int nWareHouse = 0;
                         int.TryParse(DgvDeviceStatus.Columns[k].Name.Substring(1), out nWareHouse);
                         DgvDeviceStatus.Rows[j].Cells[k].Tag = nWareHouse + "-" + lstETV[j];
                         CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus = proxy.GetDeviceStatus(nWareHouse, Convert.ToInt32(lstETV[j]));
                         if (null != deviceStatus)
                         {
                             if (null != deviceStatus.isable && 0 != (int)deviceStatus.isable)
                             {
                                 DgvDeviceStatus.Rows[j].Cells[k].Value = "可用";
                             }
                             else
                             {
                                 DgvDeviceStatus.Rows[j].Cells[k].Value = "不可用";
                             }
                             //DgvDeviceStatus.Rows[j].Cells[k].Value = CStaticClass.ConvertBoolType(deviceStatus.isable);
                             //DgvDeviceStatus.Rows[j].Cells[k].Style.BackColor = deviceStatus.isable == 0 ? Color.Red : Color.Green;
                         }
                         else
                         {
                             DgvDeviceStatus.Rows[j].Cells[k].Value = "无";
                         }
                     }
                 }

                 for (int j = 0; j < lstHall.Count; j++)
                 {// 增加行
                     int row = j + lstETV.Count;
                     DgvDeviceStatus.Rows.Add();
                     DgvDeviceStatus.Rows[row].HeaderCell.Value = CStaticClass.ConvertHallDescp(lstHall[j]);// lstHall[j] + "# 车厅";
                     DgvDeviceStatus.Rows[row].Tag = lstHall[j];
                     nHeight += DgvDeviceStatus.Rows[row].Height;
                     for(int k = 0;k < DgvDeviceStatus.ColumnCount;k++)
                     {
                         int nWareHouse = 0;
                         int.TryParse(DgvDeviceStatus.Columns[k].Name.Substring(1), out nWareHouse);
                         DgvDeviceStatus.Rows[row].Cells[k].Tag = nWareHouse + "-" + lstHall[j];
                         CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus = proxy.GetDeviceStatus(nWareHouse, Convert.ToInt32(lstHall[j]));
                         if (null != deviceStatus)
                         {
                             if (null != deviceStatus.isable && 0 != (int)deviceStatus.isable)
                             {
                                 DgvDeviceStatus.Rows[row].Cells[k].Value = "可用";
                             }
                             else
                             {
                                 DgvDeviceStatus.Rows[row].Cells[k].Value = "不可用";
                             }

                             //DgvDeviceStatus.Rows[row].Cells[k].Value = CStaticClass.ConvertBoolType(deviceStatus.isable);
                             //DgvDeviceStatus.Rows[row].Cells[k].Style.BackColor = deviceStatus.isable == 0 ? Color.Red : Color.Green;
                         }
                         else
                         {
                             DgvDeviceStatus.Rows[row].Cells[k].Value = "无";
                         }
                     }
                 }

                 nHeight = Math.Min(nHeight, this.groupBox3.Height - DgvDeviceStatus.Location.Y);
                 DgvDeviceStatus.Size = new System.Drawing.Size(735, nHeight);
                 this.groupBox3.Controls.Add(DgvDeviceStatus);
                 ((System.ComponentModel.ISupportInitialize)(DgvDeviceStatus)).EndInit();
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
        /// 选择时触发，清空选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                dgv.ClearSelection();
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

        /// <summary>
        /// 单击车位单元格时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int gap = 2;
                DataGridView dgv = (DataGridView)sender;
                DataGridViewCell cell = dgv.CurrentCell;

                if (null == cell)
                {
                    return;
                }

                // 取消选择
                cell.Selected = false;
                // 获取当前单元格相对位置
                Point pos = new Point(0, cell.RowIndex * cell.Size.Height);
                if (dgv.RowHeadersVisible)
                {
                    pos.X += dgv.RowHeadersWidth;
                }

                for (int i = 0; i < cell.ColumnIndex && i < dgv.ColumnCount; i++)
                {
                    pos.X += dgv.Columns[i].Width;
                }

                if (dgv.ColumnHeadersVisible)
                {
                    pos.Y += dgv.ColumnHeadersHeight;
                }

                Rectangle bounds = new Rectangle(pos, cell.Size);
                Graphics dgvGRaphics = dgv.CreateGraphics();

                // 重绘上一次选择单元格的区域（擦除）
                if (null != dgv.Tag && dgv.Tag.GetType() == typeof(Rectangle))
                {
                    Rectangle erasureRect = (Rectangle)dgv.Tag;

                    if (erasureRect.X + erasureRect.Width == pos.X + gap && erasureRect.Y == pos.Y)
                    {// 重叠当前单元格左边
                        erasureRect.Width -= 2 * gap;
                    }
                    if (pos.X + bounds.Width == erasureRect.X && erasureRect.Y == pos.Y)
                    {// 重叠当前单元格右边
                        erasureRect.X += 2 * gap;
                    }
                    if (erasureRect.Y + erasureRect.Height == pos.Y + gap && erasureRect.X == pos.X)
                    {// 重叠当前单元格上边
                        erasureRect.Height -= 2 * gap;
                    }
                    if (pos.Y + bounds.Height == erasureRect.Y && erasureRect.X == pos.X)
                    {// 重叠当前单元格下边
                        erasureRect.Y += 2 * gap;
                    }

                    erasureRect.Width += gap;
                    erasureRect.Height += gap;

                    // 重绘上一次选择单元格的区域（擦除）
                    dgv.Invalidate(erasureRect);
                }

                // 绘制当前单元格选择框
                using (Pen pen = new Pen(Color.Blue))
                {
                    bounds.Width += gap;
                    bounds.Height += gap;
                    dgvGRaphics.DrawRectangle(pen, bounds);
                    dgv.Tag = bounds;
                }
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
        #endregion

        /// <summary>
        /// 允许厅外刷卡取车否
        /// </summary>
        private void InitGetCarOutStatus() 
        {           
            try
            {
                if (!CStaticClass.CheckPushService())
                {// 检查服务
                    return;
                }
                QueryServiceClient proxy = new QueryServiceClient();
                string status = proxy.GetUserSetFixCardOutLimit();               
                proxy.Close();
                if (status == "1") 
                {
                    lblAllow.Text = "允许";
                }
                else if (status == "0")
                {
                    lblAllow.Text = "禁止";
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAllow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string value;
            if (btn == btnAllow)
            {
                value = "1";
                lblAllow.Text = "允许";
            }
            else 
            {
                value = "0";
                lblAllow.Text = "禁止";
            }
            try
            {
                if (!CStaticClass.CheckPushService())
                {// 检查服务
                    return;
                }
                QueryServiceClient proxy = new QueryServiceClient();
                int status = proxy.SetUserSetFixCardOutLimit(value);
                proxy.Close();
                if (status == 1)
                {
                    MessageBox.Show("修改成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else 
                {
                    MessageBox.Show("修改失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
