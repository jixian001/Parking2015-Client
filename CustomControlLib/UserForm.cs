using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib;
using CarLocationPanelLib.QueryService;
using System.ServiceModel;

namespace CustomControlLib
{
    public partial class UserForm : Form
    {
        private List<string> m_lstICCardID = new List<string>();
        public UserForm()
        {
            InitializeComponent();
            Button BtnCancel = new Button();
            BtnCancel.Click += new System.EventHandler(BtnCancel_Click);
            this.CancelButton = BtnCancel;
        }

        /// <summary>
        /// 设置需要修改的IC卡列表
        /// </summary>
        /// <param name="lstICCardID"></param>
        public void SetLstICCardID(List<string> lstICCardID)
        {
            m_lstICCardID = lstICCardID;
        }

        /// <summary>
        /// 确定推迟n天批量修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
			    //modify by suhan 2015072
                if (this.RbtnDelay.Checked == true)
                {

                    int nDelayDays = 0;
                    int.TryParse(this.TxtDelayDays.Text, out nDelayDays);

                    if (null == m_lstICCardID || 0 >= m_lstICCardID.Count || 0 == nDelayDays)
                    {
                        MessageBox.Show("推迟天数不能为空，选择也不为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    DialogResult dr = MessageBox.Show("确认批量修改否？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                    if (dr == DialogResult.Cancel)
                    {
                        return;
                    }

                    EnmFaultType type = proxy.BatchModifyICCardDeadLine(nDelayDays, m_lstICCardID);
                    switch (type)
                    {
                        case EnmFaultType.Success:
                            {
                                MessageBox.Show("批量修改成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("批量修改失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                    }
                    this.Close();
                }
                else if (this.RbtnTariff.Checked == true)
                {
                    //DataTable dt = (DataTable)this.CboTariff.SelectedItem;
                    int nTariffID = (int)this.CboTariff.SelectedValue;//this.CboTariff.SelectedIdatatem.ToString();
                    EnmFaultType type = proxy.BatchModifyICCardTariffID(nTariffID, m_lstICCardID);
                    switch (type)
                    {
                        case EnmFaultType.Success:
                            {
                                MessageBox.Show("批量修改成功!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                break;
                            }
                        default:
                            {
                                MessageBox.Show("批量修改失败!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                    }
                    this.Close();

                }
				//end by suhan 2015072
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
        /// 计费标准单选框状态变化时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtnTariff_CheckedChanged(object sender, EventArgs e)
        {
            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (this.RbtnTariff.Checked == true)
                {
                    this.label1.Text = "计费标准";
                    this.CboTariff.Visible = true;
                    this.TxtDelayDays.Visible = false;
                    if (!CStaticClass.CheckPushService())
                    {// 检查服务
                        return;
                    }
                    //this.CboTariff.Items.Clear();
                    List<CTariffDto> lstTariffTBL = proxy.GetTariffList();
                    //DataTable dtTariffInfo = new DataTable();
                    //DataColumn dc1 = new DataColumn("TariffDescp", typeof(string));
                    //DataColumn dc2 = new DataColumn("TariffID", typeof(int));
                    //dtTariffInfo.Columns.Add(dc1);
                    //dtTariffInfo.Columns.Add(dc2);
                    //foreach (CTariffDto table in lstTariffTBL)
                    //{
                    //    DataRow dr = dtTariffInfo.NewRow();
                    //    dr[0] = table.tariffdescp;
                    //    dr[1] = table.id;
                    //    dtTariffInfo.Rows.Add(dr);
                    //    //this.CboTariff.Items.Add(table);
                    //}
                    //this.CboTariff.DisplayMember = "TariffDescp";
                    //this.CboTariff.ValueMember = "TariffID";
                    //this.CboTariff.DataSource = dtTariffInfo;
                    this.CboTariff.DisplayMember = "tariffdescp";
                    this.CboTariff.ValueMember = "id";
                    this.CboTariff.DataSource = lstTariffTBL;
                }
                else
                {
                    this.label1.Text = "推迟天数";
                    this.CboTariff.Visible = false;
                    this.TxtDelayDays.Visible = true;
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
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
