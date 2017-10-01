using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib;
using CarLocationPanelLib.QueryService;
using System.ServiceModel;

namespace WindowsFormLib
{
    public partial class CFormModifyPassWord : Form
    {
        public CFormModifyPassWord()
        {
            InitializeComponent();
            this.CancelButton = this.BtnPassWordCancel;
        }

        /// <summary>
        /// 确认密码修改
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
            try
            {
                if (this.TxtOldPassWord.Text.Trim() != CStaticClass.myOperator.optpassword)
                {
                    MessageBox.Show("原密码错误，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(this.TxtNewPassWord.Text))
                {
                    MessageBox.Show("新密码不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (this.TxtNewPassWord.Text.Trim() != this.TxtConfirmNewPW.Text.Trim())
                {
                    MessageBox.Show("两次填写的密码不一致，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                EnmFaultType type = proxy.UpdatePassword(ref CStaticClass.myOperator, this.TxtNewPassWord.Text.Trim());

                switch (type)
                {
                    case EnmFaultType.Success:
                        {
                            MessageBox.Show("修改密码成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Close();
                            break;
                        }
                    case EnmFaultType.UserNameError:
                        {
                            MessageBox.Show("账户错误，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.PasswordError:
                        {
                            MessageBox.Show("密码错误，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("修改密码失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 取消密码修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPassWordCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
