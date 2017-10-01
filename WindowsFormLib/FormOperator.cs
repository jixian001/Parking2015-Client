using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;

namespace WindowsFormLib
{
    public partial class CFormOperator : Form
    {
        public CFormOperator()
        {
            InitializeComponent();
            this.CancelButton = this.BtnClose;
            this.CboType.SelectedIndex = 0;
        }

        #region 公有函数
        /// <summary>
        /// 获取操作员信息实例
        /// </summary>
        /// <returns></returns>
        public COperatorDto getOperatorInfo()
        {
            List<object> lstObject = new List<object>();
            foreach (object obj in this.LtbAssignedPermission.Items)
            {
                lstObject.Add(obj);
            }

            COperatorDto operatorDto = new COperatorDto
            {
                optcode = this.CTxtCode.Text.Trim(),
                optname = this.CTxtName.Text.Trim(),
                optpassword = this.CTxtPassWord.Text.Trim(),
                optphone = this.CTxtPhone.Text.Trim(),
                optaddr = this.TxtAddr.Text.Trim(),
                opttype = (int)CStaticClass.ConvertOperatorType(this.CboType.Text),
                optpermission = CStaticClass.ConvertOptPermission(lstObject.ToArray())
            };

            return operatorDto;
        }

        /// <summary>
        /// 填充操作员信息
        /// </summary>
        /// <param name="obj"></param>
        public void FillOperatorInfo(COperatorDto operatorDto)
        {
            //this.TxtPassWord.Text = "******";// 2
            this.CTxtCode.Text = operatorDto.optcode;
            this.CTxtName.Text = operatorDto.optname;
            this.CTxtPassWord.Text = operatorDto.optpassword;
            this.CTxtNewPassWord.Text = operatorDto.optpassword;
            this.CTxtPhone.Text = operatorDto.optphone;
            this.TxtAddr.Text = operatorDto.optaddr;
            this.CboType.Enabled = false;
            this.CboType.Text = CStaticClass.ConvertOperatorType(operatorDto.opttype);

            this.LtbAssignedPermission.Items.Clear();
            this.LtbAssignedPermission.Items.AddRange(CStaticClass.ConvertOptPermission(operatorDto.optpermission));

            if (string.IsNullOrEmpty(this.CTxtCode.Text))
            {
                this.CTxtCode.Enabled = true;
                this.BtnSave.Enabled = true;
                this.BtnModify.Enabled = false;
                this.BtnDelete.Enabled = false;
            }
            else
            {
                this.CTxtCode.Enabled = false;
                this.BtnSave.Enabled = false;
                this.BtnModify.Enabled = true;
                this.BtnDelete.Enabled = true;
            }
        }
        #endregion

        #region button按钮单击触发事件
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.CTxtCode.Text) || string.IsNullOrEmpty(this.CTxtPassWord.Text) || string.IsNullOrEmpty(this.CboType.Text))
                {
                    MessageBox.Show("用户名，密码，类型都不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (this.CTxtPassWord.Text.Trim()  != this.CTxtNewPassWord.Text.Trim())
                {
                    MessageBox.Show("两次填写的密码不一致，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                COperatorDto operatorDto = getOperatorInfo();
                EnmFaultType type = proxy.AddOperator(operatorDto);

                switch (type)
                {
                    case EnmFaultType.Success:
                        {
                            if (null != this.Owner && typeof(CFormOperatorManage) == this.Owner.GetType())
                            {
                                ((CFormOperatorManage)this.Owner).SaveDgvOperatorRow(operatorDto);
                            }

                            DialogResult dr = MessageBox.Show("保存成功，是否继续添加？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                            if (dr == DialogResult.OK)
                            {
                                ClearOperatorControls();
                                return;
                            }
                            //MessageBox.Show("保存成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Close();
                            break;
                        }
                    case EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.OPRTypeNull:
                        {
                            MessageBox.Show("操作管理员类型为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.FailToInsert:
                        {
                            MessageBox.Show("插入数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("保存失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModify_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.CTxtCode.Text) || string.IsNullOrEmpty(this.CTxtPassWord.Text) || string.IsNullOrEmpty(this.CboType.Text))
                {
                    MessageBox.Show("用户名，密码，类型都不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (this.CTxtPassWord.Text.Trim() != this.CTxtNewPassWord.Text.Trim())
                {
                    MessageBox.Show("两次填写的密码不一致，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                COperatorDto operatorDto = getOperatorInfo();
                EnmFaultType type = proxy.AddOperator(operatorDto);

                switch (type)
                {
                    case EnmFaultType.Success:
                        {
                            if (null != this.Owner && typeof(CFormOperatorManage) == this.Owner.GetType())
                            {
                                ((CFormOperatorManage)this.Owner).ModifyDgvOperatorRow(operatorDto);
                            }

                            MessageBox.Show("修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Close();
                            break;
                        }
                    case EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.OPRTypeNull:
                        {
                            MessageBox.Show("操作管理员类型为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.FailToInsert:
                        {
                            MessageBox.Show("插入数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("修改失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                if (string.IsNullOrEmpty(this.CTxtCode.Text))
                {
                    MessageBox.Show("用户名不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (CStaticClass.myOperator.optcode == this.CTxtCode.Text.Trim())
                {
                    MessageBox.Show("不允许删除当前操作员的信息！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                COperatorDto operatorDto = getOperatorInfo();
                bool flag = proxy.DeleteOperator(operatorDto);

                if (!flag)
                {
                    MessageBox.Show("删除数据库失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 移除界面操作员列表行数据
                if (null != this.Owner && typeof(CFormOperatorManage) == this.Owner.GetType() 
                    && ((CFormOperatorManage)this.Owner).RemoveDgvOperatorRow(operatorDto))
                {
                    MessageBox.Show("删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("没有该操作员！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 移进
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMove_Click(object sender, EventArgs e)
        {
            if (null == this.LtbAllocPermission.SelectedItems)
            {
                return;
            }

            // 移动选择的多选，不能移动相同项
            foreach (object obj in this.LtbAllocPermission.SelectedItems)
            {
                if (!this.LtbAssignedPermission.Items.Contains(obj))
                {
                    this.LtbAssignedPermission.Items.Add(obj);
                }
            }

            if (1 < this.LtbAllocPermission.SelectedItems.Count)
            {
                return;
            }

            int nIndex = this.LtbAllocPermission.SelectedIndex;
            this.LtbAllocPermission.SetSelected(nIndex, false);// 删除当前选择 
            // 如果移动一项时，设置当前移动选择项为下一个
            if (nIndex + 1 < this.LtbAllocPermission.Items.Count)
            {
                this.LtbAllocPermission.SelectedIndex = nIndex + 1;
            }
            else if (0 < this.LtbAllocPermission.Items.Count)
            {
                this.LtbAllocPermission.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 移出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (null == this.LtbAssignedPermission.SelectedItems)
            {
                return;
            }

            int nIndex = this.LtbAssignedPermission.SelectedIndex;
            ListBox.SelectedObjectCollection lstItems = this.LtbAssignedPermission.SelectedItems;
            // 移除选择的多选
            for (int i = 0; i < lstItems.Count; i++)
            {
                this.LtbAssignedPermission.Items.Remove(lstItems[i--]);
            }

            // 如果移除一项时，设置当前移动选择项为上一个
            if (0 == nIndex && nIndex < this.LtbAssignedPermission.Items.Count)
            {
                this.LtbAssignedPermission.SelectedIndex = nIndex;
            }
            else if (-1 < nIndex - 1)
            {
                this.LtbAssignedPermission.SelectedIndex = nIndex - 1;
            }
        }

        /// <summary>
        /// 操作员类型改变触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LtbAllocPermission.Items.Clear();
            this.LtbAssignedPermission.Items.Clear();
            this.LtbAllocPermission.Items.AddRange(CStaticClass.ConvertOptPermission((EnmOperatorType)CStaticClass.ConvertOperatorType(this.CboType.Text.Trim())));
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 对比COperatorDto两个表格是否相等
        /// </summary>
        /// <param name="operatorDto1"></param>
        /// <param name="operatorDto2"></param>
        /// <returns></returns>
        private bool CompareOperatorDto(COperatorDto operatorDto1, COperatorDto operatorDto2)
        {
            if (null == operatorDto1 || null == operatorDto2)
            {
                return false;
            }

            if (operatorDto1.optcode == operatorDto2.optcode && operatorDto1.optname == operatorDto2.optname
                   && operatorDto1.optpassword == operatorDto2.optpassword && operatorDto1.optphone == operatorDto2.optphone
                   && operatorDto1.opttype == operatorDto2.opttype && operatorDto1.optaddr == operatorDto2.optaddr)
            {
                return true;
            }

            return false;
        }
      
        /// <summary>
        /// 清空操作员信息控件
        /// </summary>
        /// <returns></returns>
        private void ClearOperatorControls()
        {
            this.CTxtCode.Text = "";
            this.CTxtName.Text = "";
            this.CTxtPassWord.Text = "";
            this.CTxtNewPassWord.Text = "";
            this.CTxtPhone.Text = "";
            this.TxtAddr.Text = "";
            this.CboType.Enabled = true;
            this.CboType.Text = null;// "";
            this.LtbAssignedPermission.Items.Clear();

            if (string.IsNullOrEmpty(this.CTxtCode.Text))
            {
                this.CTxtCode.Enabled = true;
                this.BtnSave.Enabled = true;
                this.BtnModify.Enabled = false;
                this.BtnDelete.Enabled = false;
            }
            else
            {
                this.CTxtCode.Enabled = false;
                this.BtnSave.Enabled = false;
                this.BtnModify.Enabled = true;
                this.BtnDelete.Enabled = true;
            }
        }
        #endregion
    }
}
