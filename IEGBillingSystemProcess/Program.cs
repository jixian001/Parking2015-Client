using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormLib;
using LOGManagementLib;
using System.ServiceModel;

namespace IEGBillingSystemProcess
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {               
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CFormBilling());
            }
            catch (TimeoutException exception)
            {
                CLOGException.Trace("GetExceptionInfo:" + exception.ToString());
                MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (FaultException exception)
            {
                CLOGException.Trace("GetExceptionInfo:" + exception.ToString());
                MessageBox.Show("系统出现FaultException异常，强制关闭应用程序！", "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (CommunicationException exception)
            {
                CLOGException.Trace("GetExceptionInfo:" + exception.ToString());
                MessageBox.Show("There was a communication problem. " + "请检查服务端是否打开，并打开服务！", "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            catch (Exception exception)
            {
                CLOGException.Trace("GetExceptionInfo:" + exception.ToString());
                MessageBox.Show("应用程序异常，强制关闭！", "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
