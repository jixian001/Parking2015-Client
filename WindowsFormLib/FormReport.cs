using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Reflection;

namespace WindowsFormLib
{
    public partial class CFormReport : Form
    {
        public CFormReport()
        {
            InitializeComponent();
            this.components = new System.ComponentModel.Container();
            // 设置键盘“Esc”按钮
            Button BtnCancel = new Button();
            this.CancelButton = BtnCancel;
            BtnCancel.Click += new EventHandler(BtnCancel_Click);
        }

        /// <summary>
        /// 登陆报表刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CFormReport_Load(object sender, EventArgs e)
        {
            this.RvStatistics.RefreshReport();
        }

        /// <summary>
        /// 窗体大小改变触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            this.RvStatistics.Size = this.ClientSize;
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
        /// 设置具体报表绑定数据源及资源名称
        /// </summary>
        /// <param name="dataObject"></param>
        /// <param name="strRReportEmbeddedResource"></param>
        public void SetReportBindingSource(object dataObject, string strRReportEmbeddedResource)
        {
            string dataName = "DataSet";
            string dataMember = "id";

            if (strRReportEmbeddedResource.Contains("SystemLog"))
            {
                dataName += "1";
                dataMember = "logid";
            }
            else if (strRReportEmbeddedResource.Contains("CardLog"))
            {
                dataName += "2";
            }
            else if (strRReportEmbeddedResource.Contains("TelegramLog"))
            {
                dataName += "4";
            }
            else if (strRReportEmbeddedResource.Contains("DeviceStatusLog"))
            {
                dataName += "5";
            }
            else if (strRReportEmbeddedResource.Contains("DeviceFaultLog"))
            {
                dataName += "6";
            }

            System.Windows.Forms.BindingSource DtoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(DtoBindingSource)).BeginInit();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            reportDataSource1.Name = dataName;
            reportDataSource1.Value = DtoBindingSource;
            this.RvStatistics.LocalReport.DataSources.Clear();
            this.RvStatistics.LocalReport.DataSources.Add(reportDataSource1);
            this.RvStatistics.DataBindings.Clear();
            this.RvStatistics.DataBindings.Add(new System.Windows.Forms.Binding("Tag", DtoBindingSource, dataMember, true));
            DtoBindingSource.DataSource = dataObject;
            this.RvStatistics.LocalReport.ReportEmbeddedResource = strRReportEmbeddedResource;
            ((System.ComponentModel.ISupportInitialize)(DtoBindingSource)).EndInit();
        }

        #region 私有函数
        /// <summary>
        /// 导出设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReportViewer_PreRender(object sender, EventArgs e)
        {
            ReportViewer rw = this.RvStatistics;
            if (rw == null)
            {
                return;
            }
            
            foreach (RenderingExtension re in rw.LocalReport.ListRenderingExtensions())
            {
                if (re.Name == "Excel")
                {
                    HideRender(re); 
                }
            }
        }

        /// <summary>
        /// 隐藏导出Excel
        /// </summary>
        /// <param name="re"></param>
        private static void HideRender(RenderingExtension re)
        {
             FieldInfo fi = re.GetType().GetField("m_serverExtension", BindingFlags.Instance | BindingFlags.NonPublic);
             if (fi != null)
             {
                 object actualExtension = fi.GetValue(re);
                 if (actualExtension != null)
                 {
                     PropertyInfo propInfo = actualExtension.GetType().GetProperty("Visible");
                     if (propInfo != null && propInfo.CanWrite)
                     {
                         propInfo.SetValue(actualExtension, false, null);
                     }
                 }
             }
        }
        #endregion
    }
}
