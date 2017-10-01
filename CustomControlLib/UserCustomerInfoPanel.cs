using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using System.Drawing;
using System.ComponentModel;
using System.ServiceModel;
using CarLocationPanelLib;

namespace CustomControlLib
{
    public class CUserCustomerInfoPanel : Panel
    {
        #region 自定义事件
        /// <summary>
        /// 修改按钮单击事件(添加)
        /// </summary>
        public event EventHandler BtnModifyClick;
        #endregion

        protected System.Windows.Forms.GroupBox GbxCustomerList;
        protected System.Windows.Forms.GroupBox GbxCustomerFind;
        protected System.Windows.Forms.Label LblCustomerCondition;
        protected System.Windows.Forms.Label LblCustomerData;
        protected CUserTextButton CTxtCustomerData;
        protected System.Windows.Forms.ComboBox CboCustomerCondition;
        protected System.Windows.Forms.Button BtnCustomerDelete;
        protected System.Windows.Forms.Button BtnCustomerAdd;
        protected System.Windows.Forms.Button BtnCustomerModify;
        protected CUserPageTurnToolStrip CupttsUers;
        protected System.Windows.Forms.Button BtnCustomerFind;
        protected System.Windows.Forms.ComboBox CboCustomerData;
        protected System.Windows.Forms.DataGridView DgvCustomer;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        protected System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        protected Image m_imageStartBtn = null;
        protected Image m_imageLeftBtn = null;
        protected Image m_imageRightBtn = null;
        protected Image m_imageEndBtn = null;

        public CUserCustomerInfoPanel()
            :base()
        {
            InitializeComponent();
        }

        #region 登陆界面
        /// <summary>
        /// 登陆界面时-绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CUserCustomerInfoPanel_Load(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                // 查询所有车主信息
                List<struCustomerInfo> lstStruCUSTInfo = new List<struCustomerInfo>();
                proxy.QueryCUSTInfo(ref lstStruCUSTInfo);
                // 添加列表信息
                this.DgvCustomer.DataSource = new BindingList<struCustomerInfo>(lstStruCUSTInfo);
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

        #region 属性
        /// <summary>
        /// 首页按钮图片
        /// </summary>
        public Image ImageStartBtn
        {
            get
            {
                return m_imageStartBtn;
            }
            set
            {
                m_imageStartBtn = value;
                this.CupttsUers.ImageStartBtn = m_imageStartBtn;
            }
        }

        /// <summary>
        /// 上一页按钮图片
        /// </summary>
        public Image ImageLeftBtn
        {
            get
            {
                return m_imageLeftBtn;
            }
            set
            {
                m_imageLeftBtn = value;
                this.CupttsUers.ImageLeftBtn = m_imageLeftBtn;
            }
        }

        /// <summary>
        /// 下一页按钮图片
        /// </summary>
        public Image ImageRightBtn
        {
            get
            {
                return m_imageRightBtn;
            }
            set
            {
                m_imageRightBtn = value;
                this.CupttsUers.ImageRightBtn = m_imageRightBtn;
            }
        }

        /// <summary>
        /// 尾页按钮图片
        /// </summary>
        public Image ImageEndBtn
        {
            get
            {
                return m_imageEndBtn;
            }
            set
            {
                m_imageEndBtn = value;
                this.CupttsUers.ImageEndBtn = m_imageEndBtn;
            }
        }
        
        /// <summary>
        /// 查找按钮
        /// </summary>
        public Button BtnFind
        {
            get
            {
                return BtnCustomerFind;
            }
            set
            {
                BtnCustomerFind = value;
            }
        }
        #endregion

        #region 公有函数
        /// <summary>
        /// 窗体首次显示时触发(窗体大小改变触发 OnShown-OnResize)
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnResize()
        {
            int width = this.ClientSize.Width > CStaticClass.ConfigMinWidth() ? this.ClientSize.Width : CStaticClass.ConfigMinWidth();
            int height = this.ClientSize.Height > CStaticClass.ConfigMinHeight() ? this.ClientSize.Height : CStaticClass.ConfigMinHeight();
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
            this.DgvCustomer.Size = new System.Drawing.Size(width, this.GbxCustomerList.Height - this.DgvCustomer.Location.Y - this.CupttsUers.Height - minGap);
            this.CupttsUers.Location = new Point(4, this.DgvCustomer.Location.Y + this.DgvCustomer.Height + minGap);// 翻页
        }

        /// <summary>
        /// 保存列表中车主信息
        /// </summary>
        /// <param name="customerInfo"></param>
        public bool SaveDgvCustomerInfo(struCustomerInfo customerInfo)
        {
            // 插入
            this.CupttsUers.AddDataItem(customerInfo);
            CStaticClass.SaveStruCUSTInfo(customerInfo, 0);
            //((BindingList<struCustomerInfo>)this.DgvCustomer.DataSource).Add(customerInfo);
            return true;
        }

        /// <summary>
        /// 修改列表中车主信息
        /// </summary>
        /// <param name="customerInfo"></param>
        public bool ModifyDgvCustomerInfo(struCustomerInfo customerInfo)
        {
            // 更新
            this.CupttsUers.ModifyDataItem(customerInfo);
            CStaticClass.SaveStruCUSTInfo(customerInfo, 1);
            //BindingList<struCustomerInfo> dataBingList = (BindingList<struCustomerInfo>)this.DgvCustomer.DataSource;
            //struCustomerInfo dgvr = dataBingList.SingleOrDefault(s => s.strICCardID == customerInfo.strICCardID);
            //int index = dataBingList.IndexOf(dgvr);
            //if (-1 == index)
            //{
            //    dataBingList.Add(customerInfo);
            //}
            //else
            //{
            //    dataBingList.Remove(dgvr);
            //    dataBingList.Insert(index, customerInfo);
            //}
            return true;
        }

        /// <summary>
        /// 删除列表中车主信息
        /// </summary>
        /// <param name="customerInfo"></param>
        public void DeleteDgvCustomerInfo(struCustomerInfo customerInfo)
        {
            // 删除
            this.CupttsUers.DeleteDataItem(customerInfo);
            CStaticClass.SaveStruCUSTInfo(customerInfo, 2);
            //BindingList<struCustomerInfo> dataBingList = (BindingList<struCustomerInfo>)this.DgvCustomer.DataSource;
            //struCustomerInfo dgvr = dataBingList.SingleOrDefault(s => s.strICCardID == customerInfo.strICCardID && s.strName == customerInfo.strName);

            //((BindingList<struCustomerInfo>)this.DgvCustomer.DataSource).Remove(dgvr);
        }
        #endregion

        #region 车主信息
        /// <summary>
        /// 车主信息查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCustomerFind_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                int index = this.CboCustomerCondition.SelectedIndex;
                string strInfo = "所有";

                if (this.CTxtCustomerData.Visible && this.CTxtCustomerData.Enabled)
                {
                    if (string.IsNullOrEmpty(this.CTxtCustomerData.Text))
                    {
                        strInfo = string.Empty;
                    }
                    else
                    { 
                        strInfo = this.CTxtCustomerData.Text.Trim();
                    }
                }
                else if (this.CboCustomerData.Visible)
                {
                    if (string.IsNullOrEmpty(this.CboCustomerData.Text))
                    {
                        strInfo = string.Empty;
                    }
                    else
                    {
                        strInfo = this.CboCustomerData.Text.Trim();
                    }
                }

                if (0 > index || string.IsNullOrEmpty(strInfo))
                {
                    MessageBox.Show("查询条件，查询数据都不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<struCustomerInfo> lstStruCUSTInfo = new List<struCustomerInfo>();
                proxy.QueryCUSTInfo(ref lstStruCUSTInfo);
                CStaticClass.SetLstStruCUSTInfo(lstStruCUSTInfo);

                switch (index)
                {
                    case 4:// 姓名
                        {
                            lstStruCUSTInfo = lstStruCUSTInfo.FindAll(s => s.strName.Contains(strInfo));
                            break;
                        }
                    case 5:// 移动电话
                        {
                            lstStruCUSTInfo = lstStruCUSTInfo.FindAll(s => s.strMobile.Contains(strInfo));
                            break;
                        }
                    case 6:// 车牌号
                        {
                            lstStruCUSTInfo = lstStruCUSTInfo.FindAll(s => s.strLicPlteNbr.Contains(strInfo));
                            break;
                        }
                    case 0:// 用户卡号
                        {
                            lstStruCUSTInfo = lstStruCUSTInfo.FindAll(s => s.strICCardID == CStaticClass.ConvertICCardID(strInfo));
                            break;
                        }
                    case 2:// 卡类型
                        {
                            lstStruCUSTInfo = lstStruCUSTInfo.FindAll(s => CStaticClass.ConvertICCardType(s.nICCardType) == strInfo);
                            break;
                        }
                    case 1:// 库区
                        {
                            lstStruCUSTInfo = lstStruCUSTInfo.FindAll(s => CStaticClass.ConvertWareHouse(s.nWareHouse) == strInfo);
                            break;
                        }
                    case 3:// 卡状态（分配车位）
                        {
                            lstStruCUSTInfo = lstStruCUSTInfo.FindAll(s => CStaticClass.ConvertICCardStatus(s.nICCardStatus) == strInfo);
                            break;
                        }
                    default:
                        {
                            break;
                        }

                }

                // 添加列表信息
                this.DgvCustomer.DataSource = new BindingList<struCustomerInfo>(lstStruCUSTInfo);
                if (null == lstStruCUSTInfo || 0 == lstStruCUSTInfo.Count)
                {
                    MessageBox.Show("抱歉，没有找到符合条件的记录！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
        /// 车主信息修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCustomerModify_Click(object sender, EventArgs e)
        {
            try
            {//弹出窗口
                if (1 != this.DgvCustomer.SelectedRows.Count)
                {
                    MessageBox.Show("请选择单行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                struCustomerInfo Customer = (struCustomerInfo)this.DgvCustomer.SelectedRows[0].DataBoundItem;

                if (null != BtnModifyClick)
                {
                    BtnModifyClick(Customer, e);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 车主信息添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCustomerAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != BtnModifyClick)
                {
                    BtnModifyClick(null, e);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCustomerDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

            if (dr == DialogResult.Cancel)
            {
                return;
            }

            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                //弹出窗口
                if (1 != this.DgvCustomer.SelectedRows.Count)
                {
                    MessageBox.Show("请选择单行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                struCustomerInfo Customer = (struCustomerInfo)this.DgvCustomer.SelectedRows[0].DataBoundItem;
                if (string.IsNullOrEmpty(Customer.strICCardID))
                {
                    MessageBox.Show("用户卡号不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EnmFaultType type = proxy.DeleteCustomer(Customer.strICCardID);

                switch (type)
                {
                    case EnmFaultType.Success:
                        {
                            DeleteDgvCustomerInfo(Customer);
                            MessageBox.Show("删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case EnmFaultType.NoICCardInfo:
                        {
                            MessageBox.Show("没有制卡！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.CarInGarage:
                        {
                            MessageBox.Show("当前卡有车存在车库", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.TaskOnICCard:
                        {
                            MessageBox.Show("当前卡有作业正在操作", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.NoBoundCustomer:
                        {
                            MessageBox.Show("当前卡没有绑定车主", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.NoCustomerInfo:
                        {
                            MessageBox.Show("没有车主信息", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.FailToDelete:
                        {
                            MessageBox.Show("删除数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("删除失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        /// <summary>
        /// 双击车主列表行弹出“车主信息”对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DgvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {//弹出窗口
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    struCustomerInfo Customer = (struCustomerInfo)this.DgvCustomer.Rows[e.RowIndex].DataBoundItem;

                    if (null != BtnModifyClick)
                    {
                        BtnModifyClick(Customer, e);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 卡类型和卡状态转换格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DgvCustomer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (null == e.Value)
                {
                    if (e.ColumnIndex > -1 && e.RowIndex > -1)
                    {
                        struCustomerInfo Customer = (struCustomerInfo)this.DgvCustomer.Rows[e.RowIndex].DataBoundItem;

                        switch (e.ColumnIndex)
                        {
                            case 0:
                                {
                                    e.Value = Customer.strName;
                                    break;
                                }
                            case 1:
                                {
                                    e.Value = Customer.strICCardID;
                                    break;
                                }
                            case 2:
                                {
                                    e.Value = CStaticClass.ConvertICCardStatus(Customer.nICCardStatus);
                                    break;
                                }
                            case 3:
                                {
                                    e.Value = CStaticClass.ConvertICCardType(Customer.nICCardType);
                                    break;
                                }
                            case 4:
                                {
                                    e.Value = CStaticClass.ConvertWareHouse(Customer.nWareHouse);
                                    break;
                                }
                            case 5:
                                {
                                    e.Value = Customer.strCarPOSN;
                                    break;
                                }
                            case 6:
                                {
                                    e.Value = Customer.strTelphone;
                                    break;
                                }
                            case 7:
                                {
                                    e.Value = Customer.strMobile;
                                    break;
                                }
                            case 8:
                                {
                                    e.Value = Customer.strLicPlteNbr;
                                    break;
                                }
                            case 9:
                                {
                                    e.Value = Customer.strAddress;
                                    break;
                                }
                            case 10:
                                {
                                    e.Value = CStaticClass.ConvertPriorityID(Customer.nPriorityID);
                                    break;
                                }
                        }
                    }

                    return;
                }

                // 卡类型
                if (3 == e.ColumnIndex)
                {
                    e.Value = CStaticClass.ConvertICCardType((int)e.Value);
                }
                // 卡状态
                else if (2 == e.ColumnIndex)
                {
                    e.Value = CStaticClass.ConvertICCardStatus((int)e.Value);
                }
                // 库区4
                else if (4 == e.ColumnIndex)
                {
                    e.Value = CStaticClass.ConvertWareHouse((int)e.Value);
                }
                // 优先级
                else if (10 == e.ColumnIndex)
                {
                    e.Value = CStaticClass.ConvertPriorityID((int)e.Value);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 查询条件值改变事件(“所有”条件处理)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CboCustomerCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int nSelectedIndex = this.CboCustomerCondition.SelectedIndex;
                //if (null != e && typeof(HandledMouseEventArgs) == e.GetType())
                //{// 鼠标滚轮事件处理
                //    if (((HandledMouseEventArgs)e).Delta > 0 && 0 < nSelectedIndex)
                //    {// 后退
                //        nSelectedIndex = nSelectedIndex - 1;
                //    }
                //    else if (((HandledMouseEventArgs)e).Delta < 0 && nSelectedIndex + 1 < this.CboCustomerCondition.Items.Count)
                //    {// 前进
                //        nSelectedIndex = nSelectedIndex + 1;
                //    }
                //}

                this.CTxtCustomerData.EnabledButton = false;
                if (nSelectedIndex + 1 == this.CboCustomerCondition.Items.Count)
                {
                    this.CboCustomerData.Visible = false;
                    this.CTxtCustomerData.Visible = true;
                    this.CTxtCustomerData.EnmTxtType = EnmTxtBoxType.Init;
                    this.CTxtCustomerData.Text = "";
                    this.CTxtCustomerData.Enabled = false;
                }
                else
                {
                    switch (nSelectedIndex)
                    {
                        case 4:// 姓名
                            {
                                this.CboCustomerData.Visible = false;
                                this.CTxtCustomerData.Visible = true;
                                this.CTxtCustomerData.Enabled = true;
                                this.CTxtCustomerData.EnmTxtType = EnmTxtBoxType.Name;
                                break;
                            }
                        case 5:// 移动电话
                            {
                                this.CboCustomerData.Visible = false;
                                this.CTxtCustomerData.Visible = true;
                                this.CTxtCustomerData.Enabled = true;
                                this.CTxtCustomerData.EnmTxtType = EnmTxtBoxType.Mobile;
                                break;
                            }
                        case 6:// 车牌号
                            {
                                this.CboCustomerData.Visible = false;
                                this.CTxtCustomerData.Visible = true;
                                this.CTxtCustomerData.Enabled = true;
                                this.CTxtCustomerData.EnmTxtType = EnmTxtBoxType.Name;
                                break;
                            }
                        case 0:// 用户卡号
                            {
                                this.CboCustomerData.Visible = false;
                                this.CTxtCustomerData.Visible = true;
                                this.CTxtCustomerData.Enabled = true;
                                this.CTxtCustomerData.EnmTxtType = EnmTxtBoxType.ICCard;
                                break;
                            }
                        case 3:// 卡状态（分配车位）
                            {
                                this.CTxtCustomerData.Visible = false;
                                this.CboCustomerData.Visible = true;
                                this.CboCustomerData.Items.Clear();
                                this.CboCustomerData.Items.AddRange(new object[] { "正常", "挂失", "注销" });
                                break;
                            }
                        case 2:// 卡类型
                            {
                                this.CTxtCustomerData.Visible = false;
                                this.CboCustomerData.Visible = true;
                                this.CboCustomerData.Items.Clear();
                                this.CboCustomerData.Items.AddRange(new object[] { "临时卡", "定期卡", "固定车位卡" });
                                break;
                            }
                        case 1:// 库区
                            {
                                this.CTxtCustomerData.Visible = false;
                                this.CboCustomerData.Visible = true;
                                this.CboCustomerData.Items.Clear();
                                this.CboCustomerData.Items.AddRange(CStaticClass.ConfigLstWareHouseDescp().ToArray());
                                this.CboCustomerData.SelectedIndex = 0;
                                break;
                            }
                        default:
                            {
                                this.CboCustomerData.Visible = false;
                                this.CTxtCustomerData.Visible = true;
                                this.CTxtCustomerData.Enabled = true;
                                this.CTxtCustomerData.EnmTxtType = EnmTxtBoxType.Init;
                                break;
                            }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 虚函数
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected virtual void InitializeComponent()
        {
            this.GbxCustomerList = new System.Windows.Forms.GroupBox();
            this.CupttsUers = new CustomControlLib.CUserPageTurnToolStrip();
            this.DgvCustomer = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            // CupttsUers
            // 
            this.CupttsUers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CupttsUers.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.CupttsUers.Location = new System.Drawing.Point(4, 310);
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
            this.Column11});
            this.DgvCustomer.Location = new System.Drawing.Point(3, 27);
            this.DgvCustomer.Name = "DgvCustomer";
            this.DgvCustomer.RowHeadersVisible = false;
            this.DgvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvCustomer.Size = new System.Drawing.Size(829, 279);
            this.DgvCustomer.TabIndex = 0;
            this.DgvCustomer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvCustomer_CellDoubleClick);
            this.DgvCustomer.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvCustomer_CellFormatting);
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
            this.Column2.FillWeight = 79.48557F;
            this.Column2.HeaderText = "用户卡号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "nICCardStatus";
            this.Column3.FillWeight = 85.48165F;
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
            // Column5
            // 
            this.Column5.DataPropertyName = "nWareHouse";
            this.Column5.FillWeight = 95.23381F;
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
        #endregion
    }
}
