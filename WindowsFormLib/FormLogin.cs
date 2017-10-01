using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib;
using System.ServiceModel;
using LOGManagementLib;

namespace WindowsFormLib
{
    public partial class CFormLogin : Form
    {
        private int m_nlogNumb = 0;
        //CSplashScreenForm splashScreen;

        public CFormLogin(string strTitle)
        {
            //splashScreen = new CSplashScreenForm();
            //SplashScreenManager.ShowForm(new Form(), typeof(CSplashScreenForm), true, true, false);
            //splashScreen.SetInfo("启动登陆界面", 0, 100);
            InitializeComponent();
            this.CancelButton = this.BtnCancel;
            this.LblTitle.Text = strTitle;
            this.timer1.Interval = 5000;// 毫秒(30分钟)
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //SplashScreenManager.CloseForm(false);
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="strTitle"></param>
        public void SetTitle(string strTitle)
        {
            this.LblTitle.Text = strTitle;
        }

        /// <summary>
        /// 系统登陆密码验证
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
                if (string.IsNullOrEmpty(this.TxtName.Text) || string.IsNullOrEmpty(this.TxtPassWord.Text))
                {
                    MessageBox.Show("账户和密码都不能为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                COperatorDto operatorTBL = new COperatorDto();
                operatorTBL.optcode = this.TxtName.Text.Trim();
                operatorTBL.optpassword = this.TxtPassWord.Text.Trim();
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.CheckPassword(ref operatorTBL);

                switch (type)
                {
                    case CarLocationPanelLib.QueryService.EnmFaultType.Success:
                        {
                            CStaticClass.myOperator = operatorTBL;
                            this.Close();
                            this.DialogResult = DialogResult.Yes;
                            this.timer1.Start();
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.UserNameError:
                        {
                            MessageBox.Show("账户错误，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            m_nlogNumb++;
                            if (m_nlogNumb == 3)
                            {
                                this.Close();
                            }
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.PasswordError:
                        {
                            MessageBox.Show("密码错误，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            m_nlogNumb++;
                            if (m_nlogNumb == 3)
                            {
                                this.Close();
                            }
                            break;
                        }
                    case CarLocationPanelLib.QueryService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("连接数据库失败，请核对服务端连接数据库源配置文件是否正确？", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region WCF推送服务器处理
        /// <summary>
        /// 计时器(WCF心跳包)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool bConnected = false;
            DateTime dt = CStaticClass.CurruntDateTime();

            try
            {
                if (CommunicationState.Faulted != CStaticClass.myPushProxy.State)
                {
                    bConnected = true;
                }
            }
            catch (Exception ex)
            {
                CLOGException.Trace("CFormLogin.timer1_Tick Exception: bConnected " + bConnected + dt.ToString("yyyy/MM/dd HH:mm:ss.ffff") + ex.ToString());
                bConnected = false;
            }
            try
            {
                if (!bConnected)
                {
                    CLOGException.Trace("CFormLogin.timer1_Tick 0: bConnected " + bConnected + dt.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
                    CStaticClass.OpenPushService();
                    CLOGException.Trace("CFormLogin.timer1_Tick 1: bConnected " + bConnected + dt.ToString("yyyy/MM/dd HH:mm:ss.ffff"));
                }
            }
            catch (TimeoutException exception)
            {
                CStaticClass.GetExceptionInfo(exception);
            }
            catch (FaultException exception)
            {
                CStaticClass.GetExceptionInfo(exception);
            }
            catch (CommunicationException exception)
            {
                CStaticClass.GetExceptionInfo(exception);
            }
            catch (Exception exception)
            {
                CStaticClass.GetExceptionInfo(exception);
            }
        }
        #endregion
    }
}
