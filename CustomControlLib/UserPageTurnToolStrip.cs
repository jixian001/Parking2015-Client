using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using CarLocationPanelLib.QueryService;
using BaseMethodLib;
using CarLocationPanelLib;

namespace CustomControlLib
{
    public class CUserPageTurnToolStrip : ToolStrip
    {
        private List<COperatorDto> m_lstOperator = new List<COperatorDto>();
        private List<struCustomerInfo> m_lstStruCUSTInfo = new List<struCustomerInfo>();
        private List<CSoundDto> m_lstSound = new List<CSoundDto>();
        private List<CLedContentDto> m_lstLedContent = new List<CLedContentDto>();
        private List<CICCardLogDto> m_lstICCardLog = new List<CICCardLogDto>();
        private List<CSystemLogDto> m_lstSystemLog = new List<CSystemLogDto>();
        private List<CTelegramLogDto> m_lstTelegramLog = new List<CTelegramLogDto>();
        private List<CDeviceFaultLogDto> m_lstDeviceFaultLog = new List<CDeviceFaultLogDto>();
        private List<CDeviceStatusLogDto> m_lstDeviceStatusLog = new List<CDeviceStatusLogDto>();
        private List<CTariffDto> m_lstTariff = new List<CTariffDto>();
        private ToolStripComboBox CboPages = null;
        private ToolStripSeparator TssGap1;
        private ToolStripButton BtnStart = null;
        private ToolStripButton BtnLeft = null;
        private ToolStripSeparator TssGap2;
        private ToolStripLabel LblPage = null;
        private ToolStripTextBox TxtPage = null;
        private ToolStripLabel LblSumPage = null;
        private ToolStripSeparator TssGap3;
        private ToolStripButton BtnRight = null;
        private ToolStripButton BtnEnd = null;
        private ToolStripLabel LblPageExplain = null;
        private bool m_isDataSourceChanged = true;
        private Image m_imageStartBtn = null;
        private Image m_imageLeftBtn = null;
        private Image m_imageRightBtn = null;
        private Image m_imageEndBtn = null;

        private int m_gap = 5;//CStaticClass.ConfigMinGap();
        private int m_sideWidth = 30;//CStaticClass.ConfigControlSize();
        private string m_strOf = "共 ";//CStaticClass.ConfigStringOf();
        private string m_strTo = " 至 ";//CStaticClass.ConfigStringTo();
        private string m_strPages = "页";//CStaticClass.ConfigStringPages();

        public CUserPageTurnToolStrip():base()
        {
            InitializeComponent();
        }

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
                this.BtnStart.Image = m_imageStartBtn;
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
                this.BtnLeft.Image = m_imageLeftBtn;
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
                this.BtnRight.Image = m_imageRightBtn;
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
                this.BtnEnd.Image = m_imageEndBtn;
            }
        }
        #endregion

        #region 公有函数
        /// <summary>
        /// 增加数据项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddDataItem(object data)
        {
            // 所有列表处理
            if (data.GetType() == typeof(COperatorDto))
            {
                m_lstOperator.Add((COperatorDto)data);
            }
            else if (data.GetType() == typeof(struCustomerInfo))
            {
                m_lstStruCUSTInfo.Add((struCustomerInfo)data);
            }
            else if (data.GetType() == typeof(CSoundDto))
            {
                m_lstSound.Add((CSoundDto)data);
            }
            else if (data.GetType() == typeof(CLedContentDto))
            {
                m_lstLedContent.Add((CLedContentDto)data);
            }
            else if (data.GetType() == typeof(CICCardLogDto))
            {
                m_lstICCardLog.Add((CICCardLogDto)data);
            }
            else if (data.GetType() == typeof(CSystemLogDto))
            {
                m_lstSystemLog.Add((CSystemLogDto)data);
            }
            else if (data.GetType() == typeof(CTelegramLogDto))
            {
                m_lstTelegramLog.Add((CTelegramLogDto)data);
            }
            else if (data.GetType() == typeof(CDeviceFaultLogDto))
            {
                m_lstDeviceFaultLog.Add((CDeviceFaultLogDto)data);
            }
            else if (data.GetType() == typeof(CDeviceStatusLogDto))
            {
                m_lstDeviceStatusLog.Add((CDeviceStatusLogDto)data);
            }
            else if (data.GetType() == typeof(CTariffDto))
            {
                m_lstTariff.Add((CTariffDto)data);
            }

            UpdatePages();
            //int nCurPage = 0;
            //int.TryParse(this.TxtPage.Text.Trim(), out nCurPage);
            //ShowCurrentPage(nCurPage);
        }

        /// <summary>
        /// 修改数据项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ModifyDataItem(object data)
        {
            // 所有列表处理
            if (data.GetType() == typeof(COperatorDto))
            {
                COperatorDto dto = (COperatorDto)data;
                int index = m_lstOperator.FindIndex(s => s.optcode == dto.optcode);
                if (-1 == index)
                {
                    m_lstOperator.Add(dto);
                }
                else
                {
                    m_lstOperator.RemoveAt(index);
                    m_lstOperator.Insert(index, dto);
                }
            }
            else if (data.GetType() == typeof(struCustomerInfo))
            {
                struCustomerInfo dto = (struCustomerInfo)data;
                int index = m_lstStruCUSTInfo.FindIndex(s => s.strICCardID == dto.strICCardID);
                if (-1 == index)
                {
                    m_lstStruCUSTInfo.Add(dto);
                }
                else
                {
                    m_lstStruCUSTInfo.RemoveAt(index);
                    m_lstStruCUSTInfo.Insert(index, dto);
                }
            }
            else if (data.GetType() == typeof(CSoundDto))
            {
                CSoundDto dto = (CSoundDto)data;
                int index = m_lstSound.FindIndex(s => s.soundcode == dto.soundcode);
                if (-1 == index)
                {
                    m_lstSound.Add(dto);
                }
                else
                {
                    m_lstSound.RemoveAt(index);
                    m_lstSound.Insert(index, dto);
                }
            }
            else if (data.GetType() == typeof(CLedContentDto))
            {
                CLedContentDto dto = (CLedContentDto)data;
                int index = m_lstLedContent.FindIndex(s => s.id == dto.id);
                if (-1 == index)
                {
                    m_lstLedContent.Add(dto);
                }
                else
                {
                    m_lstLedContent.RemoveAt(index);
                    m_lstLedContent.Insert(index, dto);
                }
            }
            else if (data.GetType() == typeof(CICCardLogDto))
            {
                CICCardLogDto dto = (CICCardLogDto)data;
                int index = m_lstICCardLog.FindIndex(s => s.id == dto.id);
                if (-1 == index)
                {
                    m_lstICCardLog.Add(dto);
                }
                else
                {
                    m_lstICCardLog.RemoveAt(index);
                    m_lstICCardLog.Insert(index, dto);
                }
            }
            else if (data.GetType() == typeof(CSystemLogDto))
            {
                CSystemLogDto dto = (CSystemLogDto)data;
                int index = m_lstSystemLog.FindIndex(s => s.logid == dto.logid);
                if (-1 == index)
                {
                    m_lstSystemLog.Add(dto);
                }
                else
                {
                    m_lstSystemLog.RemoveAt(index);
                    m_lstSystemLog.Insert(index, dto);
                }
            }
            else if (data.GetType() == typeof(CTelegramLogDto))
            {
                CTelegramLogDto dto = (CTelegramLogDto)data;
                int index = m_lstTelegramLog.FindIndex(s => s.id == dto.id);
                if (-1 == index)
                {
                    m_lstTelegramLog.Add(dto);
                }
                else
                {
                    m_lstTelegramLog.RemoveAt(index);
                    m_lstTelegramLog.Insert(index, dto);
                }
            }
            else if (data.GetType() == typeof(CDeviceFaultLogDto))
            {
                CDeviceFaultLogDto dto = (CDeviceFaultLogDto)data;
                int index = m_lstDeviceFaultLog.FindIndex(s => s.id == dto.id);
                if (-1 == index)
                {
                    m_lstDeviceFaultLog.Add(dto);
                }
                else
                {
                    m_lstDeviceFaultLog.RemoveAt(index);
                    m_lstDeviceFaultLog.Insert(index, dto);
                }
            }
            else if (data.GetType() == typeof(CDeviceStatusLogDto))
            {
                CDeviceStatusLogDto dto = (CDeviceStatusLogDto)data;
                int index = m_lstDeviceStatusLog.FindIndex(s => s.id == dto.id);
                if (-1 == index)
                {
                    m_lstDeviceStatusLog.Add(dto);
                }
                else
                {
                    m_lstDeviceStatusLog.RemoveAt(index);
                    m_lstDeviceStatusLog.Insert(index, dto);
                }
            }
            else if (data.GetType() == typeof(CTariffDto))
            {
                CTariffDto dto = (CTariffDto)data;
                int index = m_lstTariff.FindIndex(s => s.id == dto.id);
                if (-1 == index)
                {
                    m_lstTariff.Add(dto);
                }
                else
                {
                    m_lstTariff.RemoveAt(index);
                    m_lstTariff.Insert(index, dto);
                }
            }

            UpdatePages();
        }

        /// <summary>
        /// 删除数据项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool DeleteDataItem(object data)
        {
            bool flag = DeleteDataList(data);
            UpdatePages();
            return flag;
        }
       
        /// <summary>
        /// 移除数据列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool DeleteDataList(object data)
        {
            bool flag = true;

            // 所有列表处理
            if (data.GetType() == typeof(COperatorDto))
            {
                COperatorDto dto = (COperatorDto)data;
                flag = m_lstOperator.RemoveAll(s => s.optcode == dto.optcode) > 0 ? true : false;
            }
            else if (data.GetType() == typeof(struCustomerInfo))
            {
                struCustomerInfo dto = (struCustomerInfo)data;
                flag = m_lstStruCUSTInfo.RemoveAll(s => s.strICCardID == dto.strICCardID && s.strName == dto.strName) > 0 ? true : false;
            }
            else if (data.GetType() == typeof(CSoundDto))
            {
                CSoundDto dto = (CSoundDto)data;
                flag = m_lstSound.RemoveAll(s => s.soundcode == dto.soundcode) > 0 ? true : false;
            }
            else if (data.GetType() == typeof(CLedContentDto))
            {
                CLedContentDto dto = (CLedContentDto)data;
                flag = m_lstLedContent.RemoveAll(s => s.id == dto.id) > 0 ? true : false;
            }
            else if (data.GetType() == typeof(CICCardLogDto))
            {
                CICCardLogDto dto = (CICCardLogDto)data;
                flag = m_lstICCardLog.RemoveAll(s => s.id == dto.id) > 0 ? true : false;
            }
            else if (data.GetType() == typeof(CSystemLogDto))
            {
                CSystemLogDto dto = (CSystemLogDto)data;
                flag = m_lstSystemLog.RemoveAll(s => s.logid == dto.logid) > 0 ? true : false;
            }
            else if (data.GetType() == typeof(CTelegramLogDto))
            {
                CTelegramLogDto dto = (CTelegramLogDto)data;
                flag = m_lstTelegramLog.RemoveAll(s => s.id == dto.id) > 0 ? true : false;
            }
            else if (data.GetType() == typeof(CDeviceFaultLogDto))
            {
                CDeviceFaultLogDto dto = (CDeviceFaultLogDto)data;
                flag = m_lstDeviceFaultLog.RemoveAll(s => s.id == dto.id) > 0 ? true : false;
            }
            else if (data.GetType() == typeof(CDeviceStatusLogDto))
            {
                CDeviceStatusLogDto dto = (CDeviceStatusLogDto)data;
                flag = m_lstDeviceStatusLog.RemoveAll(s => s.id == dto.id) > 0 ? true : false;
            }
            else if (data.GetType() == typeof(CTariffDto))
            {
                CTariffDto dto = (CTariffDto)data;
                flag = m_lstTariff.RemoveAll(s => s.id == dto.id) > 0 ? true : false;
            }

            return flag;
        }

        /// <summary>
        /// 更新页码
        /// </summary>
        public void UpdatePages()
        {
            // 获取总行数
            int nSum = GetCurrentListSum();

            // 当前页开始索引
            int nPageMaxRow = 0;
            CBaseMethods.MyBase.StringToUInt32(this.CboPages.Text, out nPageMaxRow);
            int nCurPage = 0;// 当前页码
            int.TryParse(this.TxtPage.Text.Trim(), out nCurPage);
            int nIndex = (nCurPage - 1) * nPageMaxRow;
            // 当前页行数
            int nCount = nSum - nIndex < nPageMaxRow ? nSum - nIndex : nPageMaxRow;
            int nSumPage = nSum % nPageMaxRow == 0 ? nSum / nPageMaxRow : nSum / nPageMaxRow + 1;
            nSumPage = nSumPage > 0 ? nSumPage : 1;
            this.LblSumPage.Text = m_strOf + nSumPage + m_strPages;
            this.LblSumPage.ToolTipText = "共" + nSumPage + "页";
            this.BtnLeft.Enabled = false;

            if (1 < nSumPage)
            {
                this.BtnRight.Enabled = true;
            }
            else
            {
                this.BtnRight.Enabled = false;
            }

            if (0 > nCount)
            {
                return;
            }

            m_isDataSourceChanged = false;

            // 设置列表显示的数据绑定值
            SetCurrentDataSource(nIndex, nCount);

            m_isDataSourceChanged = true;
            this.LblPageExplain.Text = (nIndex + 1) + m_strTo + (nIndex + nCount) + " " + m_strOf + nSum;
            this.LblPageExplain.ToolTipText = "总记录：" + nSum;
        }
        #endregion

        #region 界面布局
        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.CboPages = new System.Windows.Forms.ToolStripComboBox();
            this.TssGap1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnStart = new System.Windows.Forms.ToolStripButton();
            this.BtnLeft = new System.Windows.Forms.ToolStripButton();
            this.TssGap2 = new System.Windows.Forms.ToolStripSeparator();
            this.LblPage = new System.Windows.Forms.ToolStripLabel();
            this.TxtPage = new System.Windows.Forms.ToolStripTextBox();
            this.LblSumPage = new System.Windows.Forms.ToolStripLabel();
            this.TssGap3 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnRight = new System.Windows.Forms.ToolStripButton();
            this.BtnEnd = new System.Windows.Forms.ToolStripButton();
            this.LblPageExplain = new System.Windows.Forms.ToolStripLabel();
            this.SuspendLayout();
            // 
            // CboPages
            // 
            this.CboPages.AutoSize = false;
            this.CboPages.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "50",
            "100"});
            this.CboPages.Name = "CboPages";
            this.CboPages.Size = new System.Drawing.Size(60, 24);
            this.CboPages.Text = "20";
            this.CboPages.SelectedIndexChanged += new System.EventHandler(this.CboPages_SelectedIndexChanged);
            // 
            // TssGap1
            // 
            this.TssGap1.Name = "TssGap1";
            this.TssGap1.Size = new System.Drawing.Size(6, 30);
            // 
            // BtnStart
            // 
            this.BtnStart.AutoSize = false;
            this.BtnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(30, 30);
            this.BtnStart.Text = "首页";
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnLeft
            // 
            this.BtnLeft.AutoSize = false;
            this.BtnLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnLeft.Name = "BtnLeft";
            this.BtnLeft.Size = new System.Drawing.Size(30, 30);
            this.BtnLeft.Text = "上一页";
            this.BtnLeft.Click += new System.EventHandler(this.BtnLeft_Click);
            // 
            // TssGap2
            // 
            this.TssGap2.Name = "TssGap2";
            this.TssGap2.Size = new System.Drawing.Size(6, 30);
            // 
            // LblPage
            // 
            this.LblPage.AutoSize = false;
            this.LblPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LblPage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Underline);
            this.LblPage.ForeColor = System.Drawing.Color.Blue;
            this.LblPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LblPage.Name = "LblPage";
            this.LblPage.Size = new System.Drawing.Size(60, 30);
            this.LblPage.Text = "转至";//"Go To";
            this.LblPage.ToolTipText = "转至";
            this.LblPage.Click += new System.EventHandler(this.TxtPage_TextChanged);
            // 
            // TxtPage
            // 
            this.TxtPage.AutoSize = false;
            this.TxtPage.Name = "TxtPage";
            this.TxtPage.Size = new System.Drawing.Size(60, 30);
            // 
            // LblSumPage
            // 
            this.LblSumPage.AutoSize = false;
            this.LblSumPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LblSumPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LblSumPage.Name = "LblSumPage";
            this.LblSumPage.Size = new System.Drawing.Size(90, 30);
            this.LblSumPage.Text = "共1页"; //"of 1 pages";
            this.LblSumPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblSumPage.ToolTipText = "共1页";
            // 
            // TssGap3
            // 
            this.TssGap3.Name = "TssGap3";
            this.TssGap3.Size = new System.Drawing.Size(6, 30);
            // 
            // BtnRight
            // 
            this.BtnRight.AutoSize = false;
            this.BtnRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnRight.Name = "BtnRight";
            this.BtnRight.Size = new System.Drawing.Size(30, 30);
            this.BtnRight.Text = "下一页";
            this.BtnRight.Click += new System.EventHandler(this.BtnRight_Click);
            // 
            // BtnEnd
            // 
            this.BtnEnd.AutoSize = false;
            this.BtnEnd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnEnd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnEnd.Name = "BtnEnd";
            this.BtnEnd.Size = new System.Drawing.Size(30, 30);
            this.BtnEnd.Text = "尾页";
            this.BtnEnd.Click += new System.EventHandler(this.BtnEnd_Click);
            // 
            // LblPageExplain
            // 
            this.LblPageExplain.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.LblPageExplain.AutoSize = false;
            this.LblPageExplain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LblPageExplain.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LblPageExplain.Name = "LblPageExplain";
            this.LblPageExplain.Size = new System.Drawing.Size(120, 30);
            this.LblPageExplain.Text = "0-0 of 0";
            this.LblPageExplain.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LblPageExplain.ToolTipText = "总记录：0";
            // 
            // CUserPageTurnToolStrip
            // 
            this.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CboPages,
            this.TssGap1,
            this.BtnStart,
            this.BtnLeft,
            this.TssGap2,
            this.LblPage,
            this.TxtPage,
            this.LblSumPage,
            this.TssGap3,
            this.BtnRight,
            this.BtnEnd,
            this.LblPageExplain});
            this.Name = "TlsManage";
            this.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Size = new System.Drawing.Size(1008, 30);
            this.TabIndex = 8;
            this.Text = "toolStrip1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       
        /// <summary>
        /// 窗体大小改变触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            this.CboPages.Size = new System.Drawing.Size(2 * m_sideWidth, m_sideWidth);
            this.TssGap1.Size = new System.Drawing.Size(m_gap, m_sideWidth);
            this.BtnStart.Size = new System.Drawing.Size(m_sideWidth, m_sideWidth);
            this.BtnLeft.Size = new System.Drawing.Size(m_sideWidth, m_sideWidth);
            this.TssGap2.Size = new System.Drawing.Size(m_gap, m_sideWidth);
            this.LblPage.Size = new System.Drawing.Size(2 * m_sideWidth, m_sideWidth);
            //this.TxtPage.Size = new System.Drawing.Size(2 * m_sideWidth, m_sideWidth);
            //this.LblSumPage.Size = new System.Drawing.Size(3 * m_sideWidth, m_sideWidth);
            this.TssGap3.Size = new System.Drawing.Size(m_gap, m_sideWidth);
            this.BtnRight.Size = new System.Drawing.Size(m_sideWidth, m_sideWidth);
            this.BtnEnd.Size = new System.Drawing.Size(m_sideWidth, m_sideWidth);
            //this.LblPageExplain.Size = new System.Drawing.Size(4 * m_sideWidth, m_sideWidth);
            this.TxtPage.AutoSize = true;
            this.LblSumPage.AutoSize = true;
            this.LblPageExplain.AutoSize = true;
        }

        /// <summary>
        /// 更新翻页布局
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateLayout(object sender, EventArgs e)
        {
            int nSum = SetBindingListValue();

            if (-1 == nSum)
            {
                return;
            }

            // 当前页开始索引
            int nPageMaxRow = 0;
            CBaseMethods.MyBase.StringToUInt32(this.CboPages.Text, out nPageMaxRow);
            int nIndex = 0;
            // 当前页行数
            int nCount = nSum - nIndex < nPageMaxRow ? nSum - nIndex : nPageMaxRow;
            int nSumPage = nSum % nPageMaxRow == 0 ? nSum / nPageMaxRow : nSum / nPageMaxRow + 1;
            nSumPage = nSumPage > 0 ? nSumPage : 1;
            this.TxtPage.Text = "1";
            this.LblSumPage.Text = m_strOf + nSumPage + m_strPages;
            this.LblSumPage.ToolTipText = "共" + nSumPage + "页";
            this.LblPageExplain.Text = (nIndex + 1) + m_strTo + nCount + " " + m_strOf + nSum;
            this.LblPageExplain.ToolTipText = "总记录：" + nSum;
            this.BtnLeft.Enabled = false;

            if (1 < nSumPage)
            {
                this.BtnRight.Enabled = true;
            }
            else
            {
                this.BtnRight.Enabled = false;
            }

            ShowCurrentPage(1);
        }
        #endregion

        #region 翻页处理
        /// <summary>
        /// 一页的最大行数改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 获取总行数
                int nSum = GetCurrentListSum();

                // 当前页开始索引
                int nPageMaxRow = 0;
                CBaseMethods.MyBase.StringToUInt32(this.CboPages.Text, out nPageMaxRow);
                int nIndex = 0;
                // 当前页行数
                int nCount = nSum - nIndex < nPageMaxRow ? nSum - nIndex : nPageMaxRow;
                int nSumPage = nSum % nPageMaxRow == 0 ? nSum / nPageMaxRow : nSum / nPageMaxRow + 1;
                nSumPage = nSumPage > 0 ? nSumPage : 1;
                this.LblSumPage.Text = m_strOf + nSumPage + m_strPages;
                this.LblSumPage.ToolTipText = "共" + nSumPage + "页";
                this.BtnLeft.Enabled = false;

                if (1 < nSumPage)
                {
                    this.BtnRight.Enabled = true;
                }
                else
                {
                    this.BtnRight.Enabled = false;
                }

                if (0 >= nCount)
                {
                    return;
                }

                m_isDataSourceChanged = false;

                // 设置列表显示的数据绑定值
                SetCurrentDataSource(nIndex, nCount);

                m_isDataSourceChanged = true;
                this.TxtPage.Text = "1";
                this.LblPageExplain.Text = (nIndex + 1) + m_strTo + (nIndex + nCount) + " " + m_strOf + nSum;
                this.LblPageExplain.ToolTipText = "总记录：" + nSum;
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 起始页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            try
            {
                int nSumPage = 0;
                string strSumPage = this.LblSumPage.Text.Trim().Replace(m_strOf, "").Trim();
                strSumPage = strSumPage.Replace(m_strPages, "").Trim();
                int.TryParse(strSumPage, out nSumPage);
                this.BtnLeft.Enabled = false;

                if (1 < nSumPage)
                {
                    this.BtnRight.Enabled = true;
                }
                else
                {
                    this.BtnRight.Enabled = false;
                }

                ShowCurrentPage(1);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 向前翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLeft_Click(object sender, EventArgs e)
        {
            try
            {
                int nCurPage = 0;
                int.TryParse(this.TxtPage.Text.Trim(), out nCurPage);
                this.BtnRight.Enabled = true;

                if (2 >= nCurPage)
                {
                    this.BtnLeft.Enabled = false;
                }

                ShowCurrentPage(nCurPage - 1);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 向后翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRight_Click(object sender, EventArgs e)
        {
            try
            {
                int nCurPage = 0, nSumPage = 0;
                string strSumPage = this.LblSumPage.Text.Trim().Replace(m_strOf, "").Trim();
                strSumPage = strSumPage.Replace(m_strPages, "").Trim();
                int.TryParse(this.TxtPage.Text.Trim(), out nCurPage);
                int.TryParse(strSumPage, out nSumPage);
                this.BtnLeft.Enabled = true;

                if (nSumPage <= nCurPage + 1)
                {
                    this.BtnRight.Enabled = false;
                }

                ShowCurrentPage(nCurPage + 1);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 结尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEnd_Click(object sender, EventArgs e)
        {
            try
            {
                int nSumPage = 0;
                string strSumPage = this.LblSumPage.Text.Trim().Replace(m_strOf, "").Trim();
                strSumPage = strSumPage.Replace(m_strPages, "").Trim();
                int.TryParse(strSumPage, out nSumPage);
                this.BtnRight.Enabled = false;

                if (1 < nSumPage)
                {
                    this.BtnLeft.Enabled = true;
                }
                else
                {
                    this.BtnLeft.Enabled = false;
                }

                ShowCurrentPage(nSumPage);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 页文本改变触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPage_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //this.LblPage.ForeColor = System.Drawing.Color.Gray;
                int nCurPage = 0, nSumPage = 0;
                string strSumPage = this.LblSumPage.Text.Trim().Replace(m_strOf, "").Trim();
                strSumPage = strSumPage.Replace(m_strPages, "").Trim();
                int.TryParse(this.TxtPage.Text.Trim(), out nCurPage);
                int.TryParse(strSumPage, out nSumPage);

                if (1 < nCurPage)
                {
                    this.BtnLeft.Enabled = true;
                }
                else
                {
                    this.BtnLeft.Enabled = false;
                }

                if (nCurPage < nSumPage)
                {
                    this.BtnRight.Enabled = true;
                }
                else
                {
                    this.BtnRight.Enabled = false;
                }

                if (1 > nCurPage)
                {
                    this.TxtPage.Text = "1";
                    MessageBox.Show("该页码必须为正数", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nCurPage = 1;
                }
                else if (nCurPage > nSumPage)
                {
                    this.TxtPage.Text = nSumPage.ToString();
                    MessageBox.Show("该页码必须小于或等于" + nSumPage, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nCurPage = nSumPage;
                }

                ShowCurrentPage(nCurPage);
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 显示当前页面信息
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nCount"></param>
        private void ShowCurrentPage(int nCurPage)
        {
            if (1 > nCurPage)
            {
                return;
            }

            // 获取总行数
            int nSum = GetCurrentListSum();
            // 当前页开始索引
            int nPageMaxRow = 0;
            CBaseMethods.MyBase.StringToUInt32(this.CboPages.Text, out nPageMaxRow);
            int nIndex = (nCurPage - 1) * nPageMaxRow;
            // 当前页行数
            int nCount = nSum - nIndex < nPageMaxRow ? nSum - nIndex : nPageMaxRow;

            if (0 >= nCount)
            {
                return;
            }

            m_isDataSourceChanged = false;
            // 设置列表显示的数据绑定值
            SetCurrentDataSource(nIndex, nCount);
            m_isDataSourceChanged = true;
            this.TxtPage.Text = nCurPage.ToString();
            this.LblPageExplain.Text = (nIndex + 1) + m_strTo + (nIndex + nCount) + " " + m_strOf + nSum;
            this.LblPageExplain.ToolTipText = "总记录：" + nSum;
        }

        /// <summary>
        /// 设置绑定列表值
        /// </summary>
        /// <returns>返回绑定数据列表总数</returns>
        private int SetBindingListValue()
        {
            if (!m_isDataSourceChanged || null == this.Tag || this.Tag.GetType() != typeof(DataGridView) || null == ((DataGridView)this.Tag).DataSource)
            {
                return -1;
            }

            DataGridView udgv = (DataGridView)this.Tag;
            int nSum = udgv.RowCount;

            // 获取所有列表
            if (udgv.DataSource.GetType() == typeof(BindingList<COperatorDto>))
            {
                m_lstOperator = ((BindingList<COperatorDto>)udgv.DataSource).ToList();
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<struCustomerInfo>))
            {
                m_lstStruCUSTInfo = ((BindingList<struCustomerInfo>)udgv.DataSource).ToList();
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CSoundDto>))
            {
                m_lstSound = ((BindingList<CSoundDto>)udgv.DataSource).ToList();
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CLedContentDto>))
            {
                m_lstLedContent = ((BindingList<CLedContentDto>)udgv.DataSource).ToList();
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CICCardLogDto>))
            {
                m_lstICCardLog = ((BindingList<CICCardLogDto>)udgv.DataSource).ToList();
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CSystemLogDto>))
            {
                m_lstSystemLog = ((BindingList<CSystemLogDto>)udgv.DataSource).ToList();
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CTelegramLogDto>))
            {
                m_lstTelegramLog = ((BindingList<CTelegramLogDto>)udgv.DataSource).ToList();
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CDeviceFaultLogDto>))
            {
                m_lstDeviceFaultLog = ((BindingList<CDeviceFaultLogDto>)udgv.DataSource).ToList();
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CDeviceStatusLogDto>))
            {
                m_lstDeviceStatusLog = ((BindingList<CDeviceStatusLogDto>)udgv.DataSource).ToList();
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CTariffDto>))
            {
                m_lstTariff = ((BindingList<CTariffDto>)udgv.DataSource).ToList();
            }
            
            return nSum;
        }

        /// <summary>
        /// 获取当前列表总个数(DataGridView)
        /// </summary>
        /// <returns></returns>
        private int GetCurrentListSum()
        {
            if (null == this.Tag || this.Tag.GetType() != typeof(DataGridView) || null == ((DataGridView)this.Tag).DataSource)
            {
                return 0;
            }
            
            DataGridView udgv = (DataGridView)this.Tag;
            int nSum = 0;
            // 获取总行数
            if (udgv.DataSource.GetType() == typeof(BindingList<COperatorDto>))
            {
                nSum = m_lstOperator.Count;
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<struCustomerInfo>))
            {
                nSum = m_lstStruCUSTInfo.Count;
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CSoundDto>))
            {
                nSum = m_lstSound.Count;
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CLedContentDto>))
            {
                nSum = m_lstLedContent.Count;
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CICCardLogDto>))
            {
                nSum = m_lstICCardLog.Count;
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CSystemLogDto>))
            {
                nSum = m_lstSystemLog.Count;
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CTelegramLogDto>))
            {
                nSum = m_lstTelegramLog.Count;
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CDeviceFaultLogDto>))
            {
                nSum = m_lstDeviceFaultLog.Count;
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CDeviceStatusLogDto>))
            {
                nSum = m_lstDeviceStatusLog.Count;
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CTariffDto>))
            {
                nSum = m_lstTariff.Count;
            }

            return nSum;
        }

        /// <summary>
        /// 设置当前DataGridView列表的数据源数据
        /// </summary>
        /// <param name="nIndex"></param>
        /// <param name="nCount"></param>
        private void SetCurrentDataSource(int nIndex, int nCount)
        {
            if (null == this.Tag || this.Tag.GetType() != typeof(DataGridView) || null == ((DataGridView)this.Tag).DataSource)
            {
                return;
            }

            DataGridView udgv = (DataGridView)this.Tag;
            // 设置列表显示的数据绑定值
            if (udgv.DataSource.GetType() == typeof(BindingList<COperatorDto>))
            {
                udgv.DataSource = new BindingList<COperatorDto>(m_lstOperator.GetRange(nIndex, nCount));
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<struCustomerInfo>))
            {
                udgv.DataSource = new BindingList<struCustomerInfo>(m_lstStruCUSTInfo.GetRange(nIndex, nCount));
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CSoundDto>))
            {
                udgv.DataSource = new BindingList<CSoundDto>(m_lstSound.GetRange(nIndex, nCount));
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CLedContentDto>))
            {
                udgv.DataSource = new BindingList<CLedContentDto>(m_lstLedContent.GetRange(nIndex, nCount));
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CICCardLogDto>))
            {
                udgv.DataSource = new BindingList<CICCardLogDto>(m_lstICCardLog.GetRange(nIndex, nCount));
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CSystemLogDto>))
            {
                udgv.DataSource = new BindingList<CSystemLogDto>(m_lstSystemLog.GetRange(nIndex, nCount));
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CTelegramLogDto>))
            {
                udgv.DataSource = new BindingList<CTelegramLogDto>(m_lstTelegramLog.GetRange(nIndex, nCount));
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CDeviceFaultLogDto>))
            {
                udgv.DataSource = new BindingList<CDeviceFaultLogDto>(m_lstDeviceFaultLog.GetRange(nIndex, nCount));
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CDeviceStatusLogDto>))
            {
                udgv.DataSource = new BindingList<CDeviceStatusLogDto>(m_lstDeviceStatusLog.GetRange(nIndex, nCount));
            }
            else if (udgv.DataSource.GetType() == typeof(BindingList<CTariffDto>))
            {
                udgv.DataSource = new BindingList<CTariffDto>(m_lstTariff.GetRange(nIndex, nCount));
            }
        }
        #endregion
    }
}
