using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;

namespace WindowsFormLib
{
    public partial class CFormOperatorManage : Form
    {
        private CFormOperator m_formOperator = new CFormOperator();

        public CFormOperatorManage()
        {
            InitializeComponent();
            this.DgvOperator.DataSourceChanged += new EventHandler(this.CupttsOpt.UpdateLayout);
            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnCancel_Click);
        }

        /// <summary>
        /// 登陆时操作员列表显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormOperatorManage_Load(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                // 查询所有操作员信息(除天达维护人员)
                List<COperatorDto> lstOperatorDto = new List<COperatorDto>();
                proxy.GetOperatorList(ref lstOperatorDto);
                lstOperatorDto = lstOperatorDto.FindAll(s => (EnmOperatorType)s.opttype != EnmOperatorType.CIMCWorker);
                // 添加列表信息
                this.DgvOperator.DataSource = new BindingList<COperatorDto>(lstOperatorDto);
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

        #region 公有函数
        /// <summary>
        /// 插入界面操作员列表行数据
        /// </summary>
        /// <param name="strOptCode"></param>
        public bool SaveDgvOperatorRow(COperatorDto operatorDto)
        {// 插入
            this.CupttsOpt.AddDataItem(operatorDto);
            //((BindingList<COperatorDto>)this.DgvOperator.DataSource).Add(operatorDto);
            return true;
        }

        /// <summary>
        /// 修改界面操作员列表行数据
        /// </summary>
        /// <param name="strOptCode"></param>
        public bool ModifyDgvOperatorRow(COperatorDto operatorDto)
        {// 更新
            this.CupttsOpt.ModifyDataItem(operatorDto);
            //BindingList<COperatorDto> dataBingList = (BindingList<COperatorDto>)this.DgvOperator.DataSource;
            //COperatorDto dgvr = dataBingList.SingleOrDefault(s => s.optcode == operatorDto.optcode);
            //int index = dataBingList.IndexOf(dgvr);
            //dataBingList.Remove(dgvr);
            //dataBingList.Insert(index, operatorDto);
            return true;
        }

        /// <summary>
        /// 移除界面操作员列表行数据
        /// </summary>
        /// <param name="strOptCode"></param>
        public bool RemoveDgvOperatorRow(COperatorDto operatorDto)
        {
            // 删除
            return this.CupttsOpt.DeleteDataItem(operatorDto);
            //BindingList<COperatorDto> dataBingList = (BindingList<COperatorDto>)this.DgvOperator.DataSource;
            //COperatorDto dgvr = dataBingList.SingleOrDefault(s => s.optcode == operatorDto.optcode);
            //return ((BindingList<COperatorDto>)this.DgvOperator.DataSource).Remove(dgvr);
        }
        #endregion

        #region button按钮单击触发事件
        /// <summary>
        /// 取消操作员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                (new CFormOperator()).ShowDialog(this);
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
                if (1 != this.DgvOperator.SelectedRows.Count)
                {
                    MessageBox.Show("请选择单行！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (null == m_formOperator)
                {
                    m_formOperator = new CFormOperator();
                }

                m_formOperator.FillOperatorInfo((COperatorDto)this.DgvOperator.SelectedRows[0].DataBoundItem);
                m_formOperator.ShowDialog(this);
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
                // 判断是否选择当前操作员
                foreach (DataGridViewRow dgvr in this.DgvOperator.SelectedRows)
                {
                    if (((COperatorDto)dgvr.DataBoundItem).optcode == CStaticClass.myOperator.optcode)
                    {
                        MessageBox.Show("不允许删除当前操作员的信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                DialogResult dr = MessageBox.Show("确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                foreach (DataGridViewRow dgvr in this.DgvOperator.SelectedRows)
                {
                    COperatorDto operatorDto = (COperatorDto)dgvr.DataBoundItem;
                    bool flag = proxy.DeleteOperator(operatorDto);

                    if (flag)
                    {
                        this.CupttsOpt.DeleteDataList(operatorDto);
                        //((BindingList<COperatorDto>)this.DgvOperator.DataSource).Remove(operatorDto);
                    }
                    //else
                    //{
                    //    MessageBox.Show("没有该操作员", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                MessageBox.Show("删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.CupttsOpt.UpdatePages();
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

        #region 操作员DataGridView列表处理
        /// <summary>
        /// 双击操作员列表行触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvOperator_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //弹出窗口
                if (e.ColumnIndex > -1 && e.RowIndex > -1)
                {
                    if (null == m_formOperator)
                    {
                        m_formOperator = new CFormOperator();
                    }

                    m_formOperator.FillOperatorInfo((COperatorDto)this.DgvOperator.Rows[e.RowIndex].DataBoundItem);
                    m_formOperator.ShowDialog(this);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 操作员列表单元格格式设置（类型5、密码2）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvOperator_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (null == e.Value)
            {
                return;
            }

            // 类型
            if (5 == e.ColumnIndex)
            {
                if (e.Value.GetType() == typeof(int))
                {
                    e.Value = CStaticClass.ConvertOperatorType((int)e.Value);
                }
            }
            // 密码
            else if (2 == e.ColumnIndex)
            {
                e.Value = "******";
            }
        }
        #endregion
    }
}
