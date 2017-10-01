using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;
using CustomControlLib;

namespace WindowsFormLib
{
    public partial class CFormTariff : Form
    {
        private EnmICCardType m_currentICCardType = 0;
        private int m_nTariffID = 0;
        private Form m_form = new CFormCarLocation();
        private CUserTariffPanel m_cutpTariff = new CUserTariffPanel();

        public CFormTariff()
        {
            InitializeComponent();
            // 设置键盘“Esc”按钮
            Button BtnESC = new Button();
            BtnESC.Click += new EventHandler(BtnCancel_Click);
            this.CancelButton = BtnESC;

            // 增加计费标准的界面
            m_cutpTariff.ModifyDgvTariffRow += new EventHandler(SaveDgvTariffInfo);
            m_cutpTariff.BtnCancelClick += new EventHandler(BtnCancelForm_Click);
            m_form.Text = "计费标准";
            m_form.Controls.Clear();
            m_form.Controls.Add(m_cutpTariff);
            m_form.ClientSize = m_cutpTariff.Size;
            m_form.MaximizeBox = false;
            m_form.FormBorderStyle = FormBorderStyle.FixedDialog;
            m_form.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// 登陆界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormTariff_Load(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                // 查询所有标准收费信息
                List<CTariffDto> lstTariff = proxy.GetTariffList();

                if (EnmICCardType.Temp == m_currentICCardType)
                {
                    lstTariff = lstTariff.FindAll(s => s.iccardtype == (int)m_currentICCardType);
                }
                else
                {
                    lstTariff = lstTariff.FindAll(s => s.iccardtype != (int)EnmICCardType.Temp);
                }
                this.CboTariffDescp.Items.Clear();
                // 添加列表信息
                this.CboTariffDescp.Items.AddRange(lstTariff.ToArray());
                CTariffDto tariff = lstTariff.Find(s => s.id == m_nTariffID);

                if (null != tariff)
                {
                    this.CboTariffDescp.SelectedItem = tariff;
                }
                else
                { 
                    this.CutpTariff.Visible = false;
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
        /// 固定卡时收费标准“卡类型”值
        /// </summary>
        public void SetCboCardType(EnmICCardType cardType, int nTariffID)
        {
            m_currentICCardType = cardType;
            m_nTariffID = nTariffID;
        }

        #region button按钮单击触发事件
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                m_cutpTariff.SetCboCardType(m_currentICCardType);
                m_cutpTariff.SetTariffInfo(null);
                m_form.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                m_cutpTariff.SetCboCardType(m_currentICCardType);
                m_cutpTariff.SetTariffInfo((CTariffDto)this.CboTariffDescp.SelectedItem);
                m_form.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteSel_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                DialogResult dr = MessageBox.Show("确定删除选择吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                bool bFlag = false;
                CTariffDto tariff = (CTariffDto)this.CboTariffDescp.SelectedItem;
                EnmFaultType type = proxy.DeleteTariff(tariff);
                if (EnmFaultType.Success == type)
                {
                    this.CboTariffDescp.Items.Remove(tariff);
                    foreach (CTariffDto dto in this.CboTariffDescp.Items)
                    {
                        if (dto.id > tariff.id)
                        {
                            dto.id -= 1;
                        }
                    }
                    if (null == this.CboTariffDescp.Items || 1 > this.CboTariffDescp.Items.Count)
                    {
                        this.CutpTariff.Visible = false;
                    }
                    bFlag = true;
                }

                if (bFlag)
                {
                    MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("删除失败", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 关闭m_form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancelForm_Click(object sender, EventArgs e)
        {
            m_form.Close();
        }

        /// <summary>
        /// 确认选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (null != this.CboTariffDescp.SelectedItem)
            {
                CTariffDto tariff = (CTariffDto)this.CboTariffDescp.SelectedItem;
                DateTime fixStartTime;
                DateTime fixEndTime;
                this.CutpTariff.GetFixDateTime(out fixStartTime, out fixEndTime);
                if (null != this.Owner && typeof(CFormCustomer) == this.Owner.GetType())
                {
                    ((CFormCustomer)this.Owner).SetTariffID(tariff, fixStartTime, fixEndTime);
                    this.Close();
                }
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 保存列表中收费标准
        /// </summary>
        /// <param name="tariff"></param>
        private void SaveDgvTariffInfo(object sender, EventArgs e)
        {
            if (typeof(CTariffDto) != sender.GetType())
            {
                return;
            }

            CTariffDto tariff = (CTariffDto)sender;
            bool bAdd = true;
            // 更新
            foreach(CTariffDto dto in this.CboTariffDescp.Items)
            {
                if (dto.id == tariff.id)
                {
                    int nIndex = this.CboTariffDescp.Items.IndexOf(dto);

                    if (-1 != nIndex)
                    {
                        bAdd = false;
                        this.CboTariffDescp.Items.RemoveAt(nIndex);
                        this.CboTariffDescp.Items.Insert(nIndex, tariff);
                        break;
                    }
                }
            }

            // 添加列表信息
            if (bAdd)
            {
                tariff.id = this.CboTariffDescp.Items.Count + 1;
                this.CboTariffDescp.Items.Add(tariff);
            }
            m_form.Close();
        }

        /// <summary>
        /// 删除列表中收费标准
        /// </summary>
        /// <param name="tariff"></param>
        private void RemoveDgvTariffInfo(object sender, EventArgs e)
        {
            if (typeof(CTariffDto) != sender.GetType())
            {
                return;
            }

            CTariffDto tariff = (CTariffDto)sender;
            // 删除列表信息
            this.CboTariffDescp.Items.Remove(tariff);
            foreach (CTariffDto dto in this.CboTariffDescp.Items)
            {
                if (dto.id > tariff.id)
                {
                    dto.id -= 1;
                }
            }
            m_form.Close();
        }
        #endregion

        #region 计费标准处理
        /// <summary>
        /// 计费标准选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTariffDescp_SelectedIndexChanged(object sender, EventArgs e)
        {
            CTariffDto tariff = (CTariffDto)this.CboTariffDescp.SelectedItem;
            CutpTariff.SetTariffInfo(tariff);
            if ((int)EnmICCardType.Temp == tariff.iccardtype)
            {
                CutpTariff.SetFixVisable(false);
            }
            else
            {
                CutpTariff.SetFixVisable(true);
            }
            this.CutpTariff.Visible = true;
        }
       
        /// <summary>
        /// 计费标准绑定到数据值时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTariffDescp_Format(object sender, ListControlConvertEventArgs e)
        {
            try
            {
                CTariffDto tariff = (CTariffDto)e.ListItem;
                e.Value = tariff.tariffdescp;
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

    }
}
