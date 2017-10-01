using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using BaseMethodLib;
using CarLocationPanelLib.QueryService;
using CarLocationPanelLib.PushService;

namespace CarLocationPanelLib
{
    /// <summary>
    /// 通道是水平布局的车位
    /// </summary>
    public class CPlaneShifting : CWareHousePanel
    {
        private delegate void beginInvokeDelegate(object sender, int nX, int nY);
        private List<Label> m_lLblLayer = new List<Label>();
        private List<Label> m_lLblETVEquip = new List<Label>();
        private List<Label> m_lLblCross = new List<Label>();

        public CPlaneShifting()
            : base()
        {           
            HandInitializeComponent();
        }

        public CPlaneShifting(Rectangle rectData) 
            : base()
        {
            m_wareHouse = rectData.X;  //1
            m_side = rectData.Y;       //201
            m_column = rectData.Width; //40
            m_layer = rectData.Height; //3

            HandInitializeComponent();
        }

        #region 重载函数
        /// <summary>
        /// 手动初始化布局
        /// </summary>
        protected override void HandInitializeComponent()
        {
            // 
            // tabPage1
            // 
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = m_wareHouse.ToString();
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "库" + m_wareHouse + "车位状态";

            // data
            string strLayerFlag = "层";
            string strColFlag = "列";
            string strSideFlag = "边";
            int col = m_column;
            int row = m_layer;   //3
            // 车位排列（某一边一层的所有列布局）
            DataGridView dgvTitle = new DataGridView();
            dgvTitle.RowHeadersVisible = false;
            dgvTitle.ColumnHeadersVisible = true;
            dgvTitle.AllowUserToAddRows = false;
            dgvTitle.AllowUserToResizeColumns = false;
            dgvTitle.AllowUserToResizeRows = false;
            dgvTitle.ScrollBars = ScrollBars.None;
            dgvTitle.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dgvTitle.Name = "列标题";
            m_lDgvCarLocation.Add(dgvTitle);
            this.Controls.Add(dgvTitle);
            for (int k = 0; k < col; k++)
            {
                // 增加列
                dgvTitle.Columns.Add((k + 1) + strColFlag, (k + 1) + strColFlag);
                dgvTitle.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            List<object> lstETV = CStaticClass.ConfigLstETVOrTVDeviceID(m_wareHouse);// 根据库号获取对应所有ETV设备号

            for (int i = 0; i < row * 2; i++)
            {
                // 
                // m_lblETVStatus
                // 
                // TV文本
                Label lblTV = new Label();
                string strEquopID = lstETV.Count > i ? lstETV[i].ToString() : "0";
                //lblTV.Font = new System.Drawing.Font("Arial Black", 20F);
                lblTV.BackColor = System.Drawing.Color.PaleGreen;
                lblTV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                lblTV.Name = strEquopID;// 设备号
                lblTV.Text = strEquopID;//strEquopID + "#";
                lblTV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lblTV.DoubleClick += m_lblETVStatus_DoubleClick;
                ToolTip tp = new ToolTip();
                tp.SetToolTip(lblTV, strEquopID + "号TV");
                m_lLblETVEquip.Add(lblTV);
                this.Controls.Add(lblTV);
            }
            //row=3
            for (int i = row; i >0; i--)
            {
                // 层文本
                Label lblLayer = new Label();
                lblLayer.Name = i.ToString();
                lblLayer.Text = "第\n" + i + "\n" + strLayerFlag;
                lblLayer.TextAlign = ContentAlignment.MiddleCenter;
                lblLayer.BackColor = System.Drawing.Color.LightGreen;
                m_lLblLayer.Add(lblLayer);
                this.Controls.Add(lblLayer);

                int num = m_side;  //201
                // 车位排列
                while (0 != num)  
                {
                    int j = num % 10;// 边数
                    num = num / 10;

                    if (0 == j)
                    {
                        // 通道
                        // 
                        // m_lblCross
                        // 
                        // 通道文本
                        Label lblCross = new Label();
                        lblCross.Text = "巷道";
                        lblCross.TextAlign = ContentAlignment.MiddleCenter;
                        lblCross.BackColor = Color.LightGray;
                        m_lLblCross.Add(lblCross);
                        this.Controls.Add(lblCross);
                        continue;
                    }

                    // 边文本
                    Label lblSide = new Label();
                    lblSide.Name = j.ToString() + i.ToString().PadLeft(2, '0');
                    lblSide.Text = j + strSideFlag;
                    lblSide.TextAlign = ContentAlignment.MiddleCenter;
                    lblSide.BackColor = System.Drawing.Color.Cyan;
                    m_lLblSide.Add(lblSide);
                    this.Controls.Add(lblSide);

                    // 车位排列（某一边一层的所有列布局）
                    DataGridView dgv = new DataGridView();
                    dgv.Font = new System.Drawing.Font("黑体", 8F);
                    dgv.RowHeadersVisible = false;
                    dgv.ColumnHeadersVisible = false;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToResizeColumns = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.ScrollBars = ScrollBars.None;
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                    dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
                    dgv.CellDoubleClick += new DataGridViewCellEventHandler(dgv_DoubleClick);
                    dgv.TabIndex = i;
                    dgv.CellClick += new DataGridViewCellEventHandler(dgv_CellClick);
                    dgv.SelectionChanged += new EventHandler(dgv_SelectionChanged);
                    dgv.CellMouseDown += new DataGridViewCellMouseEventHandler(dgv_CellMouseDown);
                    dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                    dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 12F);
                    dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                    dgv.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 7F);
                    for (int k = 0; k < col; k++)
                    {// 增加列
                        string str = j.ToString() + (k + 1).ToString().PadLeft(2, '0') + i.ToString().PadLeft(2, '0');
                        dgv.Columns.Add(str, (k + 1) + strColFlag);
                        dgv.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    dgv.Rows.Add();
                    dgv.Name = j.ToString() + i.ToString().PadLeft(2, '0');
                    m_lDgvCarLocation.Add(dgv);
                    this.Controls.Add(dgv);
                }
            }
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.Size = new System.Drawing.Size(1000, 500);
        }

        /// <summary>
        /// 窗体大小改变触发
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            if (0 == m_column * m_layer * m_lLblSide.Count())
            {
                return;
            }

            int gap = CStaticClass.ConfigMinGap();// 重列边间隔
            int sideWith = CStaticClass.ConfigControlSize();

            int width = Size.Width;
            int height = Size.Height;
            int maxCount = m_lLblSide.Count() / m_layer;
            int curHigh = 40; //20
            int col = m_column;
            int row = m_layer;
            // 每个车位行高和列宽
            int curWith = 2 * sideWith + gap;
            int colWidth = (width - 2 * sideWith) / col;
            int sideHigh = (height - row * gap - curHigh) / row;

            DataGridView dgvTitle = m_lDgvCarLocation.Find(s => s.Name == "列标题");
            if (null != dgvTitle)
            {
                // 列标题排列
                dgvTitle.ColumnHeadersHeight = curHigh;
                dgvTitle.Location = new System.Drawing.Point(curWith, 0);
                dgvTitle.Size = new System.Drawing.Size(colWidth * col, curHigh);   //20
                for (int k = 0; k < col; k++)
                {  //第一列的宽
                    dgvTitle.Columns[k].Width = colWidth;
                }
            }

            int rowHigh = (sideHigh - maxCount * gap) / (maxCount + 1);
            int crossHigh = rowHigh;
            if (rowHigh > sideWith)
            {
                crossHigh = sideWith;
                rowHigh = (sideHigh - crossHigh - maxCount * gap) / maxCount;
            }
            for (int i = row; i >0; i--)
            {              
                //if (i < m_lLblLayer.Count())
                //{
                    //层标签
                int layNum = m_lLblLayer.Count();                  
                    m_lLblLayer[layNum- i].Location = new System.Drawing.Point(0, curHigh);
                    m_lLblLayer[layNum - i].Size = new System.Drawing.Size(sideWith, sideHigh);
                //}

                int num = m_side;
                // 车位排列宽度和高度调整
                while (0 != num)
                {
                    int j = num % 10;// 边数
                    num = num / 10;

                    string str = j.ToString() + i.ToString().PadLeft(2, '0');
                    Label lblSide = m_lLblSide.Find(s => s.Name == str);
                    if (null != lblSide)
                    {
                        // 边
                        lblSide.Location = new System.Drawing.Point(sideWith, curHigh);
                        lblSide.Size = new System.Drawing.Size(sideWith, rowHigh);
                    }

                    curWith = 2 * sideWith + gap;
                    if (0 == j)
                    {
                        // 通道ETV
                        m_lLblCross[i - 1].Location = new System.Drawing.Point(sideWith, curHigh);
                        m_lLblCross[i - 1].Size = new System.Drawing.Size(Size.Width, crossHigh);

                        m_lLblETVEquip[i - 1].Location = new System.Drawing.Point(curWith, curHigh);

                        m_lLblETVEquip[i + 2].Location = new Point(colWidth * 40, curHigh);
                        int nW = colWidth > rowHigh ? colWidth / 3 : colWidth;// colWidth
                        int nH = colWidth > rowHigh ? rowHigh : rowHigh / 3;// sideWith
                        //nH = nH < crossHigh ? nH : crossHigh;
                        nH = crossHigh;
                        m_lLblETVEquip[i - 1].Size = new System.Drawing.Size(nW, nH);
                        m_lLblETVEquip[i + 2].Size = new System.Drawing.Size(nW, nH);
                        curHigh += crossHigh + gap;
                        continue;
                    }

                    DataGridView dgv = m_lDgvCarLocation.Find(s => s.Name == str);
                    if (null != dgv)
                    {
                        // 车位排列
                        dgv.Location = new System.Drawing.Point(curWith, curHigh);
                        dgv.Size = new System.Drawing.Size(colWidth * col, rowHigh);
                        dgv.Rows[0].Height = rowHigh;
                        for (int k = 0; k < col; k++)
                        {  //第一列的宽
                            dgv.Columns[k].Width = colWidth;
                        }
                    }
                    curHigh += rowHigh + gap;
                }
            }
            // 更新ETV位置
            UpdateDeviceStatus();
        }
        
        /// <summary>
        /// 更新ETV当前位置
        /// </summary>
        public override void UpdateDeviceStatus()
        {
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            List<QueryService.CDeviceStatusDto> lstDeviceStatus = proxy.GetDeviceStatusList(m_wareHouse);

            foreach (QueryService.CDeviceStatusDto deviceStatus in lstDeviceStatus)
            {
                Label lblTV = m_lLblETVEquip.Find(s => s.Name == deviceStatus.devicecode.ToString());
                if (null == lblTV)
                {
                    return;
                }

                // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色、正常绿色
                SetDeviceBackColor(lblTV, deviceStatus);

                #region 根据当前库车位信息设置ETV设备位置
                // 根据当前库车位信息列表设置车位状态
                string strAddr = deviceStatus.deviceaddr;
               
                DataGridView dgv = null;
                DataGridViewCell cell = null;
                GetDgvcIndexByAddr(strAddr, ref dgv, ref cell);
                if (null == cell || null == dgv)
                {
                    return;
                }
                // 获取当前单元格相对位置
                int nX = dgv.Location.X;
                int nY = dgv.Location.Y;
                for (int i = 0; i < cell.ColumnIndex && i < dgv.ColumnCount; i++)
                {
                    nX += dgv.Columns[i].Width;
                }
                
                for (int i = 0; i < cell.RowIndex && i < dgv.RowCount; i++)
                {
                    nY += dgv.Rows[i].Height;
                }
                // 设置ETV位置
                lblTV.Location = new System.Drawing.Point(nX, lblTV.Location.Y);
                #endregion
            }
            proxy.Close();
        }

        /// <summary>
        /// 更新某一ETV当前位置
        /// </summary>
        public override void UpdateDeviceStatus(CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus)
        {
            Label lblTV = m_lLblETVEquip.Find(s => s.Name == deviceStatus.devicecode.ToString());
            if (null == lblTV)
            {
                return;
            }
            QueryServiceClient proxy = new QueryServiceClient();
            List<CarLocationPanelLib.QueryService.CDeviceFaultDto> lstDeviceFault = proxy.GetDeviceFault(m_wareHouse, deviceStatus.devicecode, string.Empty);
            lstDeviceFault = lstDeviceFault.FindAll(s => s.color == 1);
            proxy.Close();
            if (0 < lstDeviceFault.Count)
            {
                lblTV.BackColor = Color.Red;
            }
            else
            {
                // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色、正常绿色
                SetDeviceBackColor(lblTV, deviceStatus);
            }

            #region 根据当前库车位信息设置ETV设备位置
            // 根据当前库车位信息列表设置车位状态
            string strAddr = deviceStatus.deviceaddr;           
            
            DataGridView dgv = null;
            DataGridViewCell cell = null;
            GetDgvcIndexByAddr(strAddr, ref dgv, ref cell);
            if (null == dgv || null == cell)
            {
                return;
            }
            // 获取当前单元格相对位置
            int nX = dgv.Location.X;
            int nY = dgv.Location.Y;
            for (int i = 0; i < cell.ColumnIndex && i < dgv.ColumnCount; i++)
            {
                nX += dgv.Columns[i].Width;
            }

            for (int i = 0; i < cell.RowIndex && i < dgv.RowCount; i++)
            {
                nY += dgv.Rows[i].Height;
            }
            // 设置ETV位置
            lblTV.BeginInvoke(new beginInvokeDelegate(SetETVLocation), lblTV, nX, nY);
            #endregion
        }

        /// <summary>
        /// 更新设备故障报警情况
        /// </summary>
        public override List<CarLocationPanelLib.QueryService.CDeviceStatusDto> UpdateDeviceFault()
        {
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return new List<CarLocationPanelLib.QueryService.CDeviceStatusDto>();
            }

            QueryServiceClient proxy = new QueryServiceClient();
            List<CarLocationPanelLib.QueryService.CDeviceStatusDto> lstDeviceStatus = proxy.GetDeviceStatusList(m_wareHouse);

            foreach (CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus in lstDeviceStatus)
            {
                if (null == deviceStatus)
                {
                    continue;
                }

                Label lblTV = m_lLblETVEquip.Find(s => s.Name == deviceStatus.devicecode.ToString());
                if (null == lblTV)
                {
                    continue;
                }

                List<CarLocationPanelLib.QueryService.CDeviceFaultDto> lstDeviceFault = proxy.GetDeviceFault(m_wareHouse, deviceStatus.devicecode, string.Empty);
                lstDeviceFault = lstDeviceFault.FindAll(s => s.color == 1);

                if ((int)EnmSMGType.ETV == deviceStatus.devicetype)
                {// ETV设备
                    if (0 >= lstDeviceFault.Count)
                    {// 该设备无报警
                     // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色、正常绿色
                        SetDeviceBackColor(lblTV, deviceStatus);
                    }
                    else
                    {
                        lblTV.BackColor = Color.Red;
                    }
                }
                else
                {// 车厅
                 // 根据当前库车位信息列表设置车位状态
                    DataGridView dgvHall = null;
                    DataGridViewCell dgvc = null;
                    GetDgvcIndexByAddr(deviceStatus.deviceaddr, ref dgvHall, ref dgvc);
                    if (null == dgvc || (int)EnmSMGType.Hall != deviceStatus.devicetype)
                    { }
                    else if (0 >= lstDeviceFault.Count)
                    {
                        // 该设备无报警
                        //不可用/不可接受指令为黄色
                        if ((int)EnmModel.Automatic != deviceStatus.devicemode)
                        {
                            dgvc.Style.BackColor = Color.LightGray;
                        }
                        else if (deviceStatus.isable == 0)
                        {
                            dgvc.Style.BackColor = Color.Brown;
                        }
                        else if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.tasktype))
                        {
                            dgvc.Style.BackColor = Color.LightPink;
                        }
                        else if (deviceStatus.isavailable == 0)
                        {
                            dgvc.Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dgvc.Style.BackColor = Color.PaleGreen;
                        }
                    }
                    else
                    {// 该设备有报警
                        dgvc.Style.BackColor = Color.Red;
                    }
                }
            }
            proxy.Close();
            return lstDeviceStatus;
        }

        /// <summary>
        /// 更新某一设备故障报警情况
        /// </summary>
        public override string UpdateDeviceFault(CarLocationPanelLib.PushService.CDeviceFaultDto deviceFault)
        {
            string strResult = string.Empty;
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return strResult;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            if (null == deviceFault.devicecode)
            {
                return strResult;
            }

            CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus = proxy.GetDeviceStatus(m_wareHouse, (int)deviceFault.devicecode);

            if (null == deviceStatus)
            {
                return strResult;
            }            

            List<CarLocationPanelLib.QueryService.CDeviceFaultDto> lstDeviceFault = proxy.GetDeviceFault(m_wareHouse, deviceStatus.devicecode, string.Empty);
            lstDeviceFault = lstDeviceFault.FindAll(s => s.color == 1);

            if ((int)EnmSMGType.ETV == deviceStatus.devicetype)
            {
                Label lblTV = m_lLblETVEquip.Find(s => s.Name == deviceStatus.devicecode.ToString());
                if (null == lblTV)
                {
                    return strResult;
                }
                // ETV设备
                if (0 >= lstDeviceFault.Count)
                {// 该设备无报警
                 // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色、正常绿色
                    SetDeviceBackColor(lblTV, deviceStatus);
                }
                else
                {
                    lblTV.BackColor = Color.Red;
                    strResult = CStaticClass.ConvertWareHouse(m_wareHouse) + CStaticClass.ConvertHallDescp(m_wareHouse, deviceStatus.devicecode);//  "   " + m_wareHouse + "#库车厅" + deviceStatus.devicecode;
                }
            }
            else
            {// 车厅
             // 根据当前库车位信息列表设置车位状态
                DataGridView dgvHall = null;
                DataGridViewCell dgvc = null;
                
                List<string> hallsAddrs = new List<string>();
                hallsAddrs.Add(deviceStatus.deviceaddr.Substring(0, 3) + "03");
                hallsAddrs.Add(deviceStatus.deviceaddr.Substring(0, 3) + "02");
                hallsAddrs.Add(deviceStatus.deviceaddr.Substring(0, 3) + "01");
                foreach (string hallAddrs in hallsAddrs)
                {
                    GetDgvcIndexByAddr(hallAddrs, ref dgvHall, ref dgvc);
                    if (null == dgvc || (int)EnmSMGType.Hall != deviceStatus.devicetype)
                    {

                    }
                    else if (0 >= lstDeviceFault.Count)
                    {   
                        // 该设备无报警
                        //不可用/不可接受指令为黄色
                        if ((int)EnmModel.Automatic != deviceStatus.devicemode)
                        {
                            dgvc.Style.BackColor = Color.LightGray;
                        }
                        else if (deviceStatus.isable == 0)
                        {
                            dgvc.Style.BackColor = Color.Brown;
                        }
                        else if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.tasktype))
                        {
                            dgvc.Style.BackColor = Color.LightPink;
                        }
                        else if (deviceStatus.isavailable == 0)
                        {
                            dgvc.Style.BackColor = Color.Yellow;
                        }
                        else
                        {
                            dgvc.Style.BackColor = Color.PaleGreen;
                        }
                    }
                    else
                    {// 该设备有报警
                        dgvc.Style.BackColor = Color.Red;
                        strResult = CStaticClass.ConvertWareHouse(m_wareHouse) + CStaticClass.ConvertHallDescp(m_wareHouse, deviceStatus.devicecode);//  "   " + m_wareHouse + "#库车厅" + deviceStatus.devicecode;
                    }
                }
            }
            proxy.Close();
            return strResult;
        }

        /// <summary>
        /// 获取当前单元格的车位地址
        /// </summary>
        protected override string GetAddressByCell(DataGridView dgv, Point pos)
        {
            string strCarLocAddr = string.Empty;
            if (null == dgv)
            {
                return strCarLocAddr;
            }
            
            // 获取车位地址
            strCarLocAddr = dgv.Columns[pos.Y].Name;
            return strCarLocAddr;
        }

        /// <summary>
        /// 根据车位地址获取当前单元格
        /// </summary>
        /// <param name="strAddr"></param>
        /// <returns></returns>
        protected override void GetDgvcIndexByAddr(string strAddr, ref DataGridView dgv, ref DataGridViewCell dgvc)
        {
            // 车位地址参数长度不正确或者不为正整数
            if (5 != strAddr.Length || !CBaseMethods.MyBase.IsUnsignedNumber(strAddr))
            {
                return;
            }

            Int16 side = 0, column = 0, layer = 0;
            CBaseMethods.MyBase.StringToUInt16(strAddr.Substring(0, 1), out side);
            CBaseMethods.MyBase.StringToUInt16(strAddr.Substring(1, 2), out column);
            CBaseMethods.MyBase.StringToUInt16(strAddr.Substring(3, 2), out layer);
            string str = side.ToString() + layer.ToString().PadLeft(2, '0');
            dgv = m_lDgvCarLocation.Find(s => s.Name == str);
            if (null == dgv)
            {
                return;
            }

            for (int k = 0; k < dgv.Columns.Count; k++)
            {  
                if (dgv.Columns[k].Name == strAddr)
                {
                    dgvc = dgv.Rows[0].Cells[k];
                    return;
                }
            }
        }
        #endregion
        
        #region 私有函数
        /// <summary>
        /// 设置ETV设备界面背景颜色变化
        /// </summary>
        /// <param name="lblTV"></param>
        /// <param name="deviceStatus"></param>
        private void SetDeviceBackColor(Label lblTV, PushService.CDeviceStatusDto deviceStatus)
        {
            // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色
            if ((int)EnmModel.Automatic != deviceStatus.devicemode)
            {
                lblTV.BackColor = Color.LightGray;
            }
            else if (deviceStatus.isable == 0)
            {
                lblTV.BackColor = Color.Brown;
            }
            else if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.tasktype))
            {
                lblTV.BackColor = Color.LightPink;
            }
            else if (deviceStatus.isavailable == 0)
            {
                lblTV.BackColor = Color.Yellow;
            }
            else
            {
                lblTV.BackColor = Color.PaleGreen;
            }
        }

        /// <summary>
        /// 设置ETV设备界面背景颜色变化
        /// </summary>
        /// <param name="lblTV"></param>
        /// <param name="deviceStatus"></param>
        private void SetDeviceBackColor(Label lblTV, QueryService.CDeviceStatusDto deviceStatus)
        {
            // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色
            if ((int)EnmModel.Automatic != deviceStatus.devicemode)
            {
                lblTV.BackColor = Color.LightGray;
            }
            else if (deviceStatus.isable==0)
            {
                lblTV.BackColor = Color.Brown;
            }
            else if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.tasktype))
            {
                lblTV.BackColor = Color.LightPink;
            }
            else if (deviceStatus.isavailable == 0)
            {
                lblTV.BackColor = Color.Yellow;
            }
            else 
            {
                lblTV.BackColor = Color.PaleGreen;
            }
        }
        
        /// <summary>
        /// 设置ETV位置
        /// </summary>
        /// <param name="nx"></param>
        /// <param name="nY"></param>
        private void SetETVLocation(object sender, int nX, int nY)
        {
            if (null == sender || typeof(Label) != sender.GetType())
            {
                return;
            }
            Label lblTV = (Label)sender;
            lblTV.Location = new System.Drawing.Point(nX, lblTV.Location.Y);
        }
        #endregion
    }
}
