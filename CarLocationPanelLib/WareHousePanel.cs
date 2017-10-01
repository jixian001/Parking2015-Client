using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CarLocationPanelLib.QueryService;
using System.ServiceModel;
using BaseMethodLib;
using CarLocationPanelLib.PushService;

namespace CarLocationPanelLib
{
    public class CWareHousePanel : Panel
    {
        /// <summary>
        /// 外部回调连接事件
        /// </summary>
        public event CallbackCarLocationEventHandler CallbackCarLocationEvent;
        private delegate void beginInvokeDelegate(int nX, int nY);

        // 库号
        protected EnmTxtCarLocationAddr m_enmSrcLocAddr = EnmTxtCarLocationAddr.Init;
        protected int m_wareHouse = 0;
        protected int m_side = 0;// 边排列31024
        protected int m_column = 0;// 列
        protected int m_layer = 0;// 层
        protected int m_crossIndex = -1;// 通道索引
        protected Rectangle m_rectVehCount;
        protected List<DataGridView> m_lDgvCarLocation = new List<DataGridView>();
        protected List<Label> m_lLblSide = new List<Label>();
        protected Label m_lblETVEquip = new Label();
        // 巷道说明
        protected Label m_lblCross = new Label();
        // 修改车位状态界面
        protected Form m_form = new Form();
        protected TextBox m_txtCarLocStatusOld = new TextBox();
        protected ComboBox m_cboCarLocStatusNew = new ComboBox();
        // 车位右键菜单
        protected ContextMenuStrip m_contextMenuStrip = new ContextMenuStrip();
        protected string m_strCurrCarLocAddr = string.Empty;
        
        public CWareHousePanel()
            : base()
        {
            SetContextMenuStripItems();
            InitModifyCarLocStatusForm();
        }

        public CWareHousePanel(Rectangle rectData)
            : base()
        {
            m_wareHouse = rectData.X;
            m_side = rectData.Y;
            m_column = rectData.Width;
            m_layer = rectData.Height;
            HandInitializeComponent();
            SetContextMenuStripItems();
            InitModifyCarLocStatusForm();
        }

        #region 属性
        /// <summary>
        /// 车位状态信息：x->总车位个数，y->占用车位个数，width->剩余车位个数
        /// </summary>
        public Rectangle RectVehCount
        {
            get
            {
                return m_rectVehCount;
            }
            set
            {
                m_rectVehCount = value;
            }
        }

        /// <summary>
        /// 获取当前项目的库号和车位排列列表(库号，边排列，列数，层数)
        /// </summary>
        public Rectangle RectProjectData
        {
            get
            {
                return new Rectangle(m_wareHouse, m_side, m_column, m_layer);
            }
            set
            {
                m_wareHouse = value.X;
                m_side = value.Y;
                m_column = value.Width;
                m_layer = value.Height;
                HandInitializeComponent();
            }
        }

        /// <summary>
        /// 是否根据车位状态获取源地址
        /// </summary>
        public EnmTxtCarLocationAddr EnmSrcLocAddr
        {
            get
            {
                return m_enmSrcLocAddr;
            }
            set
            {
                m_enmSrcLocAddr = value;
            }
        }
        #endregion

        #region 虚函数
        /// <summary>
        /// 手动初始化布局
        /// </summary>
        protected virtual void HandInitializeComponent()
        {
            //try
            //{
                // 
                // tabPage1
                // 
                this.Location = new System.Drawing.Point(0, 0);
                this.Name = m_wareHouse.ToString();
                this.Padding = new System.Windows.Forms.Padding(3);
                this.Text = "库" + m_wareHouse + "车位状态";
                List<object> lstETV = CStaticClass.ConfigLstETVOrTVDeviceID(m_wareHouse);// 根据库号获取对应所有ETV设备号
                // 
                // m_lblETVStatus
                // 
                string strEquopID = lstETV.Count > 0 ? lstETV[0].ToString() : "0";
                m_lblETVEquip.BackColor = System.Drawing.Color.PaleGreen;
                m_lblETVEquip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                m_lblETVEquip.Name = strEquopID;// 设备号
                m_lblETVEquip.Text = strEquopID + "#";//"ETV"
                if (1 > lstETV.Count)
                {
                    List<object> lstHall = CStaticClass.ConfigLstHallDeviceID(m_wareHouse);// 根据库号获取对应所有车厅设备号
                    m_lblETVEquip.Name = lstHall.Count > 0 ? lstHall[0].ToString() : "0";// 设备号
                    m_lblETVEquip.Text = "ETV";//"ETV"
                }
                m_lblETVEquip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                m_lblETVEquip.DoubleClick += new EventHandler(m_lblETVStatus_DoubleClick);
                this.Controls.Add(m_lblETVEquip);
                // 
                // m_lblCross
                // 
                m_lblCross.Text = "巷道";
                m_lblCross.TextAlign = ContentAlignment.MiddleCenter;
                //this.Controls.Add(m_lblCross);

                // data
                string strFlag = "层";
                string strColFlag = "列";
                int col = m_column;
                int row = m_layer;
                int num = m_side;
                bool isCrossUp = true;// 边是否在通道上层

                while (0 != num)
                {
                    int i = num % 10;// 边数

                    if (0 == i)
                    {// 通道
                        num = num / 10;
                        isCrossUp = false;
                        continue;
                    }

                    if (isCrossUp)
                    {
                        m_crossIndex++;
                    }
                    // 边文本
                    Label lblSide = new Label();
                    lblSide.Name = i.ToString();
                    lblSide.Text = "第\n" + i + "\n边";
                    lblSide.TextAlign = ContentAlignment.MiddleCenter;
                    lblSide.BackColor = System.Drawing.Color.LightGreen;
                    // 车位排列
                    DataGridView dgv = new DataGridView();
                    dgv.ColumnHeadersVisible = false;
                    dgv.RowHeadersVisible = false;
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToResizeColumns = false;
                    dgv.AllowUserToResizeRows = false;
                    dgv.ScrollBars = ScrollBars.None;
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
                    dgv.EditMode = DataGridViewEditMode.EditProgrammatically;
                    dgv.CellDoubleClick += new DataGridViewCellEventHandler(dgv_DoubleClick);
                    dgv.TabIndex = i;
                    dgv.CellClick += new DataGridViewCellEventHandler(dgv_CellClick);
                    dgv.SelectionChanged += new EventHandler(dgv_SelectionChanged);
                    dgv.CellMouseDown += new DataGridViewCellMouseEventHandler(dgv_CellMouseDown);
                    dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    //dgv.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Red;
                    //dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
                    //dgv.RowTemplate.Resizable = DataGridViewTriState.False;

                    for (int j = 0; j <= col; j++)
                    {// 增加列
                        dgv.Columns.Add("l" + j, j + strColFlag);
                        dgv.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;

                        //第一边第一列的值
                        if (0 == j && num == m_side)
                        {
                            dgv.ColumnHeadersVisible = true;
                            dgv.Columns[0].HeaderText = strFlag;
                        }
                    }

                    for (int j = 0; j < row; j++)
                    {// 增加行
                        dgv.Rows.Add();

                        if (isCrossUp)
                        {// 通道上层边
                            dgv.Rows[j].Cells[0].Value = row - j;
                        }
                        else
                        {
                            dgv.Rows[j].Cells[0].Value = j + 1;
                        }
                    }

                    m_lLblSide.Add(lblSide);
                    m_lDgvCarLocation.Add(dgv);
                    this.Controls.Add(lblSide);
                    this.Controls.Add(dgv);
                    num = num / 10;
                }

                this.BackColor = System.Drawing.SystemColors.Control;
                this.BorderStyle = BorderStyle.Fixed3D;
                //this.Size = new System.Drawing.Size(1000, 500);
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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
            int maxCount = m_lDgvCarLocation.Count() > m_lLblSide.Count() ? m_lDgvCarLocation.Count() : m_lLblSide.Count();
            int curHigh = 0;
            int col = m_column;
            int row = m_layer;
            // 每个车位行高和列宽
            int rowHigh = (height - 2 * sideWith - (maxCount - 2) * gap) / (maxCount * row);
            int colWidth = (width - 2 * sideWith - gap) / col;

            for (int i = 0; i < maxCount; i++)
            {
                int sideHigh = rowHigh * row + sideWith;
                if (0 != i)
                {
                    sideHigh = rowHigh * row + 2;
                }

                if (i < m_lLblSide.Count())
                {// 边
                    m_lLblSide[i].Location = new System.Drawing.Point(0, curHigh);
                    m_lLblSide[i].Size = new System.Drawing.Size(sideWith, sideHigh);
                }

                if (i < m_lDgvCarLocation.Count())
                {// 车位排列宽度和高度调整
                    m_lDgvCarLocation[i].Location = new System.Drawing.Point(sideWith, curHigh);
                    m_lDgvCarLocation[i].Size = new System.Drawing.Size(colWidth * col + sideWith, sideHigh);
                    curHigh += sideHigh;
                    for (int j = 0; j <= col; j++)
                    {  //第一列的宽
                        if (0 != j)
                        {
                            m_lDgvCarLocation[i].Columns[j].Width = colWidth;
                        }
                        else
                        {
                            m_lDgvCarLocation[i].Columns[j].Width = sideWith;
                        }
                    }

                    for (int j = 0; j < row; j++)
                    {
                        m_lDgvCarLocation[i].Rows[j].Height = rowHigh;
                    }
                }

                if (m_crossIndex == i)
                {// 通道ETV
                    m_lblCross.Location = new System.Drawing.Point(0, curHigh);
                    m_lblCross.Size = new System.Drawing.Size(Size.Width, sideWith);
                    m_lblETVEquip.Location = new System.Drawing.Point(2 * sideWith, curHigh);
                    int nW = colWidth > rowHigh ? colWidth / 2 : colWidth;// colWidth
                    int nH = colWidth > rowHigh ? rowHigh : rowHigh / 2;// sideWith
                    nH = nH < sideWith ? nH : sideWith;
                    m_lblETVEquip.Size = new System.Drawing.Size(nW, nH);
                    curHigh += sideWith;
                }
                else
                {
                    curHigh += gap;
                }
            }
            // 更新ETV位置
            UpdateDeviceStatus();
        }

        /// <summary>
        /// 获取当前单元格的车位地址
        /// </summary>
        protected virtual string GetAddressByCell(DataGridView dgv, Point pos)
        {
            int nDgvIndex = m_lDgvCarLocation.IndexOf(dgv);
            int side = dgv.TabIndex;
            int row = pos.X + 1;

            if (nDgvIndex <= m_crossIndex)
            {// 边在通道上层
                row = dgv.RowCount - pos.X;
            }

            int column = pos.Y;
            int layer = row;
            // 获取车位地址
            string strCarLocAddr = Convert.ToString(side) + Convert.ToString(column).PadLeft(2, '0') + Convert.ToString(layer).PadLeft(2, '0');
            return strCarLocAddr;            
        }

        /// <summary>
        /// 根据车位地址获取当前单元格
        /// </summary>
        /// <param name="strAddr"></param>
        /// <returns></returns>
        protected virtual void GetDgvcIndexByAddr(string strAddr, ref DataGridView dgv, ref DataGridViewCell dgvc)
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

            //获取当前ETV位置值
            for (int nDgvIndex = 0; nDgvIndex < m_lDgvCarLocation.Count; nDgvIndex++)
            {
                dgv = m_lDgvCarLocation[nDgvIndex];
                if (null == dgv || dgv.TabIndex != side)
                {
                    continue;
                }

                int nColumnIndex = column;
                int row = layer;
                int nRowIndex = row - 1;

                if (nDgvIndex <= m_crossIndex)
                {// 边在通道上层
                    nRowIndex = dgv.RowCount - row;
                }

                if (nColumnIndex < dgv.ColumnCount && nRowIndex < dgv.RowCount)
                {
                    dgvc = dgv[nColumnIndex, nRowIndex];
                }
                break;
            }
        }

        /// <summary>
        /// 更新车位状态
        /// </summary>
        public virtual void UpdateCarLocationStatus()
        {
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            //try
            //{
                // 获取当前库车位信息列表
                List<CarLocationPanelLib.QueryService.CCarLocationDto> lstClct = proxy.QueryCarPOSNByWareHouse(m_wareHouse);
                m_rectVehCount = new Rectangle();

                // 根据当前库车位信息列表设置车位状态
                foreach (DataGridView dgv in m_lDgvCarLocation)
                {
                    int nDgvIndex = m_lDgvCarLocation.IndexOf(dgv);
                    int side = dgv.TabIndex;
                    int nRowHeadersVisible = 0;// dgv.RowHeadersVisible ? 0 : 1;

                    foreach (DataGridViewRow dgvr in dgv.Rows)
                    {
                        foreach (DataGridViewCell dgvc in dgvr.Cells)
                        {
                            if (null == dgvc || nRowHeadersVisible > dgvc.ColumnIndex)
                            {
                                continue;
                            }

                            string strCarLocAddr = GetAddressByCell(dgv, new Point(dgvc.RowIndex, dgvc.ColumnIndex));
                            CarLocationPanelLib.QueryService.CCarLocationDto lct = lstClct.Find(delegate(CarLocationPanelLib.QueryService.CCarLocationDto dto)
                            { return (dto.warehouse == m_wareHouse) && (dto.carlocaddr == strCarLocAddr); });

                            if (lct != null)
                            {
                                SetLocationStatus(lct, dgvc);
                                dgvc.Tag = lct;
                            }
                            else
                            {
                                if (dgvc.ColumnIndex != nRowHeadersVisible - 1)
                                {
                                    dgvc.Style.BackColor = Color.Gray;
                                }
                            }
                        }
                    }
                }
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            proxy.Close();
        }

        /// <summary>
        /// 更新某一车位状态
        /// </summary>
        /// <param name="carLocation"></param>
        public virtual void UpdateCarLocationStatus(CarLocationPanelLib.PushService.CCarLocationDto carLocation)
        {
            if (null == carLocation)
            {
                return;
            }

            int side = (int)carLocation.carlocside;
            int layer = (int)carLocation.carloclayer;
            string coord = side.ToString() + layer.ToString().PadLeft(2, '0');

            // 根据当前库车位信息列表设置车位状态
            DataGridView dgv = m_lDgvCarLocation.Find(a => a.Name == coord);
            int nDgvIndex = m_lDgvCarLocation.IndexOf(dgv);
            int nRowHeadersVisible = 0;

            foreach (DataGridViewRow dgvr in dgv.Rows)
            {
                foreach (DataGridViewCell dgvc in dgvr.Cells)
                {
                    if (null == dgvc || nRowHeadersVisible > dgvc.ColumnIndex)
                    {
                        continue;
                    }

                    if (carLocation.carlocaddr != GetAddressByCell(dgv, new Point(dgvc.RowIndex, dgvc.ColumnIndex)))
                    {
                        continue;
                    }

                    SetLocationStatus(carLocation, dgvc);
                    dgvc.Tag = carLocation;
                    return;
                }
            }
        }

        /// <summary>
        /// 更新ETV当前位置
        /// </summary>
        public virtual void UpdateDeviceStatus()
        {
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            //try
            //{
                // 获取当前库车位信息列表
                Int16 nDeviceCode = 0;
                CBaseMethods.MyBase.StringToUInt16(m_lblETVEquip.Name, out nDeviceCode);
                CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus = proxy.GetDeviceStatus(m_wareHouse, nDeviceCode);

                if (null == deviceStatus)
                {
                    return;
                }

                // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色、正常绿色
                SetDeviceBackColor(deviceStatus);

                #region 根据当前库车位信息设置ETV设备位置
                // 根据当前库车位信息列表设置车位状态
                string strAddr = deviceStatus.deviceaddr;
                bool bETV = true;
                if ((int)EnmSMGType.ETV != deviceStatus.devicetype && null != strAddr && 3 < strAddr.Length)
                {
                    strAddr = strAddr.Substring(0, 3);
                    bETV = false;
                }
                if (CBaseMethods.MyBase.IsEmpty(deviceStatus.devicelayer))
                {
                    strAddr += "01";
                }
                else
                {
                    strAddr += deviceStatus.devicelayer.ToString().PadLeft(2, '0');
                }
                DataGridView dgv = null;
                DataGridViewCell cell = null;
                GetDgvcIndexByAddr(strAddr, ref dgv, ref cell);
                if (null == cell || null == dgv)
                {
                    return;
                }

                // 获取当前单元格相对位置
                int nX = dgv.RowHeadersVisible ? dgv.Location.X + dgv.RowHeadersWidth : dgv.Location.X;
                int nY = dgv.ColumnHeadersVisible ? dgv.Location.Y + dgv.ColumnHeadersHeight : dgv.Location.Y;
                for (int i = 0; i < cell.ColumnIndex && i < dgv.ColumnCount; i++)
                {
                    nX += dgv.Columns[i].Width;
                }
                if (!bETV)
                {
                    // 无ETV时处理
                    nX = m_lblETVEquip.Location.X;
                }
                for (int i = 0; i < cell.RowIndex && i < dgv.RowCount; i++)
                {
                    nY += dgv.Rows[i].Height;
                }
                #endregion

                #region 根据ETV设备层属性设置ETV设备位置--注释
                /*
                string strAddr = deviceStatus.deviceaddr;// "101";
                if (string.IsNullOrEmpty(deviceStatus.devicelayer))
                {
                    strAddr += "01";
                }
                else
                {
                    strAddr += deviceStatus.devicelayer.ToString().PadLeft(2, '0'); 
                }
                DataGridView dgv = null;
                DataGridViewCell cell = null;
                GetDgvcIndexByAddr(strAddr, ref dgv, ref cell);
                if (null == cell || null == dgv)
                {
                    return;
                }

                // 获取当前单元格相对位置
                int nX = m_lblETVEquip.Location.X;
                int nY = dgv.ColumnHeadersVisible ? dgv.Location.Y + dgv.ColumnHeadersHeight : dgv.Location.Y;
        
                for (int i = 0; i < cell.RowIndex && i < dgv.RowCount; i++)
                {
                    nY += dgv.Rows[i].Height;
                }*/
                #endregion

                // 设置ETV位置
                m_lblETVEquip.Location = new System.Drawing.Point(nX, nY);
                //m_lblETVEquip.BeginInvoke(new beginInvokeDelegate(SetETVLocation), nX, nY);
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            proxy.Close();
        }

        /// <summary>
        /// 更新某一ETV当前位置
        /// </summary>
        public virtual void UpdateDeviceStatus(CarLocationPanelLib.PushService.CDeviceStatusDto deviceStatus)
        {
            QueryServiceClient proxy = new QueryServiceClient();
            List<CarLocationPanelLib.QueryService.CDeviceFaultDto> lstDeviceFault = proxy.GetDeviceFault(m_wareHouse, deviceStatus.devicecode, string.Empty);
            lstDeviceFault = lstDeviceFault.FindAll(s => s.color == 1);
            proxy.Close();
            if (0 < lstDeviceFault.Count)
            {
                m_lblETVEquip.BackColor = Color.Red;
            }
            else
            {
                // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色、正常绿色
                SetDeviceBackColor(deviceStatus);
            }

            #region 根据当前库车位信息设置ETV设备位置
            // 根据当前库车位信息列表设置车位状态
            string strAddr = deviceStatus.deviceaddr;
            bool bETV = true;
            if ((int)EnmSMGType.ETV != deviceStatus.devicetype && null != strAddr && 3 < strAddr.Length)
            {
                strAddr = strAddr.Substring(0, 3);
                bETV = false;
            }
            if (CBaseMethods.MyBase.IsEmpty(deviceStatus.devicelayer))
            {
                strAddr += "01";
            }
            else
            {
                strAddr += deviceStatus.devicelayer.ToString().PadLeft(2, '0');
            }
            DataGridView dgv = null;
            DataGridViewCell cell = null;
            GetDgvcIndexByAddr(strAddr, ref dgv, ref cell);
            if (null == cell || null == dgv)
            {
                return;
            }

            // 获取当前单元格相对位置
            int nX = dgv.RowHeadersVisible ? dgv.Location.X + dgv.RowHeadersWidth : dgv.Location.X;
            int nY = dgv.ColumnHeadersVisible ? dgv.Location.Y + dgv.ColumnHeadersHeight : dgv.Location.Y;
            for (int i = 0; i < cell.ColumnIndex && i < dgv.ColumnCount; i++)
            {
                nX += dgv.Columns[i].Width;
            }
            if (!bETV)
            {
                // 无ETV时处理
                nX = m_lblETVEquip.Location.X;
            }
            for (int i = 0; i < cell.RowIndex && i < dgv.RowCount; i++)
            {
                nY += dgv.Rows[i].Height;
            }
            #endregion

            #region 根据ETV设备层属性设置ETV设备位置--注释
            /*
            string strAddr = "101";
            if (string.IsNullOrEmpty(deviceStatus.devicelayer))
            {
                strAddr += "01";
            }
            else
            {
                strAddr += deviceStatus.devicelayer.ToString().PadLeft(2, '0');
            }
            DataGridView dgv = null;
            DataGridViewCell cell = null;
            GetDgvcIndexByAddr(strAddr, ref dgv, ref cell);
            if (null == cell || null == dgv)
            {
                return;
            }

            // 获取当前单元格相对位置
            int nX = m_lblETVEquip.Location.X;
            int nY = dgv.ColumnHeadersVisible ? dgv.Location.Y + dgv.ColumnHeadersHeight : dgv.Location.Y;

            for (int i = 0; i < cell.RowIndex && i < dgv.RowCount; i++)
            {
                nY += dgv.Rows[i].Height;
            }*/
            #endregion

            // 设置ETV位置
            //m_lblETVEquip.Location = new System.Drawing.Point(nX, nY);
            m_lblETVEquip.BeginInvoke(new beginInvokeDelegate(SetETVLocation), nX, nY);
        }

        /// <summary>
        /// 更新设备故障报警情况
        /// </summary>
        public virtual List<CarLocationPanelLib.QueryService.CDeviceStatusDto> UpdateDeviceFault()
        {
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return new List<CarLocationPanelLib.QueryService.CDeviceStatusDto>();
            }

            QueryServiceClient proxy = new QueryServiceClient();
            //try
            //{
                List<CarLocationPanelLib.QueryService.CDeviceStatusDto> lstDeviceStatus = proxy.GetDeviceStatusList(m_wareHouse);

                foreach (CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus in lstDeviceStatus)
                {
                    if (null == deviceStatus)
                    {
                        continue;
                    }
                    QueryService.CCarLocationDto lct = new QueryService.CCarLocationDto
                    {
                        warehouse = deviceStatus.warehouse,
                        carlocaddr = deviceStatus.deviceaddr
                    };
                    CICCardDto iccard = new CICCardDto();
                    proxy.QueryCarPOSNByAddr(ref lct, ref iccard);
                    if (null != lct && (int)EnmLocationType.Hall != lct.carloctype)
                    {
                        // 车厅作为车位时，显示为车位信息而不是车厅信息
                        continue;
                    }
                    List<CarLocationPanelLib.QueryService.CDeviceFaultDto> lstDeviceFault = proxy.GetDeviceFault(m_wareHouse, deviceStatus.devicecode, string.Empty);
                    lstDeviceFault = lstDeviceFault.FindAll(s => s.color == 1);

                    if ((int)EnmSMGType.ETV == deviceStatus.devicetype)
                    {// ETV设备
                        if (0 >= lstDeviceFault.Count)
                        {// 该设备无报警
                            // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色、正常绿色
                            SetDeviceBackColor(deviceStatus);
                        }
                        else
                        {
                            m_lblETVEquip.BackColor = Color.Red;
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
                        {// 该设备无报警
                            //不可用/不可接受指令为黄色
                            if (CBaseMethods.MyBase.IsEmpty(deviceStatus.isable) || CBaseMethods.MyBase.IsEmpty(deviceStatus.isavailable))
                            {
                                dgvc.Style.BackColor = Color.Yellow;
                            }
                            else 
                            {
                                dgvc.Style.BackColor = Color.DarkKhaki;
                            }
                        }
                        else
                        {// 该设备有报警
                            dgvc.Style.BackColor = Color.Red;
                        }
                    }
                }
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            proxy.Close();
            return lstDeviceStatus;
        }

        /// <summary>
        /// 更新某一设备故障报警情况
        /// </summary>
        public virtual string UpdateDeviceFault(CarLocationPanelLib.PushService.CDeviceFaultDto deviceFault)
        {
            string strResult = string.Empty; 
            if (!CStaticClass.GetPushServiceConnectFlag())
            {// 服务器通道断开时
                return strResult;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            //try
            //{
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
                {// ETV设备
                    if (0 >= lstDeviceFault.Count)
                    {// 该设备无报警
                        // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色、正常绿色
                        SetDeviceBackColor(deviceStatus);
                    }
                    else
                    {
                        m_lblETVEquip.BackColor = Color.Red;
                        strResult = CStaticClass.ConvertWareHouse(m_wareHouse) + CStaticClass.ConvertHallDescp(m_wareHouse, deviceStatus.devicecode);//  "   " + m_wareHouse + "#库车厅" + deviceStatus.devicecode;
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
                    {// 该设备无报警
                        //不可用/不可接受指令为黄色
                        if (CBaseMethods.MyBase.IsEmpty(deviceStatus.isable) || CBaseMethods.MyBase.IsEmpty(deviceStatus.isavailable))
                        {
                            dgvc.Style.BackColor = Color.Yellow;
                        }
                        else
                        { 
                            dgvc.Style.BackColor = Color.DarkKhaki; 
                        }
                    } 
                    else
                    {// 该设备有报警
                        dgvc.Style.BackColor = Color.Red;
                        strResult = CStaticClass.ConvertWareHouse(m_wareHouse) + CStaticClass.ConvertHallDescp(m_wareHouse, deviceStatus.devicecode);//  "   " + m_wareHouse + "#库车厅" + deviceStatus.devicecode;
                    }
                }
            //}
            //catch (TimeoutException)
            //{
            //    MessageBox.Show("The service operation timed out. ", "超时", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (FaultException exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "SOAP错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (CommunicationException exception)
            //{
            //    MessageBox.Show("There was a communication problem. " + CStaticClass.GetExceptionInfo(exception), "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //catch (Exception exception)
            //{
            //    MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            proxy.Close();
            return strResult;
        }
        #endregion

        #region 私有函数
        /// <summary>
        /// 设置车位状态颜色
        /// </summary>
        /// <param name="lct"></param>
        /// <param name="dgvc"></param>
        protected void SetLocationStatus(CarLocationPanelLib.PushService.CCarLocationDto lct, DataGridViewCell dgvc)
        {
            dgvc.ToolTipText = "车位地址：" + lct.carlocaddr;
            if (!string.IsNullOrEmpty(lct.iccode))
            {
                dgvc.ToolTipText += " IC卡号：" + lct.iccode;
            }

            // 朝向2边字体白色
            if (2 == lct.direction)
            {
                dgvc.Style.ForeColor = Color.White;
            }
            else
            {
                dgvc.Style.ForeColor = Color.Black;
            }
            dgvc.Value = string.Empty;
            #region 界面显示绑定卡信息
            QueryServiceClient proxy = new QueryServiceClient();
            CICCardDto iccardTBL = new CICCardDto();
            iccardTBL.warehouse = lct.warehouse;
            iccardTBL.carlocaddr = lct.carlocaddr;
            proxy.QueryICCardInfoByAddress(ref iccardTBL);
            if (null != iccardTBL)
            {
                dgvc.Value = CStaticClass.GetCUSTNameByICCardID(iccardTBL.iccode);               
            }
            #endregion
            // 获取车主姓名
            string strValue = CStaticClass.GetCUSTNameByICCardID(lct.iccode);// lct.iccode;
            if (lct.carloctype == (int)EnmLocationType.Normal)//正常车位
            {
                switch ((PushService.EnmLocationStatus)lct.carlocstatus)
                {
                    case PushService.EnmLocationStatus.Space:              //空闲
                        {
                            if (dgvc.Style.BackColor != Color.LightYellow && dgvc.Style.BackColor != Color.DimGray)
                            {// 空闲或者禁用转空闲不变化
                                if (lct.carlocsize == CStaticClass.ConfigCarMaxSize())
                                {// 空余大车个数
                                    m_rectVehCount.Height++;
                                }
                                m_rectVehCount.Y--;
                                m_rectVehCount.Width++;
                            }
                            dgvc.Style.BackColor = Color.LightYellow;
                            break;
                        }
                    case PushService.EnmLocationStatus.Occupy:            //占用
                        {
                            if (lct.carlocsize == CStaticClass.ConfigCarMaxSize())
                            {// 空余大车个数
                                m_rectVehCount.Height--;
                            }
                            m_rectVehCount.Y++;
                            m_rectVehCount.Width--;
                            dgvc.Style.BackColor = Color.Purple;
                            dgvc.Value = strValue;// lct.iccode;
                            //dgvc.Value = global::CarLocationPanelLib.Properties.Resources.CarGrey;
                            break;
                        }
                    case PushService.EnmLocationStatus.Entering:         //正在入库
                        {
                            dgvc.Style.BackColor = Color.Violet;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    case PushService.EnmLocationStatus.Outing:          //正在出库
                        {
                            dgvc.Style.BackColor = Color.SkyBlue;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    case PushService.EnmLocationStatus.MovingVEH:          //库内搬移
                        {
                            dgvc.Style.BackColor = Color.GreenYellow;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    case PushService.EnmLocationStatus.TmpFetch:          //临时取物出库
                        {
                            dgvc.Style.BackColor = Color.Blue;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    case PushService.EnmLocationStatus.VehRotation:        //旋转
                        {
                            dgvc.Style.BackColor = Color.DarkSalmon;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    default:                                //初始
                        {
                            dgvc.Style.BackColor = Color.DarkGray;
                            break;
                        }
                }
            }
            else if (lct.carloctype == (int)EnmLocationType.Disable)//禁用车位
            {
                dgvc.Style.BackColor = Color.DimGray;
            }
            else if (lct.carloctype == (int)EnmLocationType.Hall)//车厅
            {
                if (!CStaticClass.GetPushServiceConnectFlag())
                {// 服务器通道断开时
                    return;
                }

                dgvc.Style.BackColor = Color.DarkKhaki;                
                CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus = proxy.GetDeviceStatusByAddr(m_wareHouse, lct.carlocaddr.Substring(0,3)+"04", EnmSMGType.Hall);
                if (null == deviceStatus)
                {                    
                    return;
                }
                dgvc.Value = (deviceStatus.devicecode - 10).ToString()+"厅";//"#车厅"

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
            { //初始
                dgvc.Style.BackColor = Color.DarkGray;
            }
            proxy.Close();

            //if (lct.IsLocked)
            //{
            //    dgvc.Style.BackColor = Color.DarkOrange;
            //}
        }
       
        /// <summary>
        /// 设置车位状态颜色
        /// </summary>
        /// <param name="lct"></param>
        /// <param name="dgvc"></param>
        protected void SetLocationStatus(CarLocationPanelLib.QueryService.CCarLocationDto lct, DataGridViewCell dgvc)
        {
            dgvc.ToolTipText = "车位地址：" + lct.carlocaddr;
            if (!string.IsNullOrEmpty(lct.iccode))
            {
                dgvc.ToolTipText += " IC卡号：" + lct.iccode;
            }

            // 朝向2边字体白色
            if (2 == lct.direction)
            {
                dgvc.Style.ForeColor = Color.White;
            }
            else
            {
                dgvc.Style.ForeColor = Color.Black;
            }
            dgvc.Value = string.Empty;
            #region 界面显示绑定卡信息
            QueryServiceClient proxy = new QueryServiceClient();
            CICCardDto iccardTBL = new CICCardDto();
            iccardTBL.warehouse = lct.warehouse;
            iccardTBL.carlocaddr = lct.carlocaddr;
            proxy.QueryICCardInfoByAddress(ref iccardTBL);
            if (null != iccardTBL)
            {
                dgvc.Value = CStaticClass.GetCUSTNameByICCardID(iccardTBL.iccode);
            }
            #endregion
            // 获取车主姓名
            string strValue = CStaticClass.GetCUSTNameByICCardID(lct.iccode);// lct.iccode;
            if (lct.carloctype == (int)EnmLocationType.Normal)//正常车位
            {
                m_rectVehCount.X++;
                switch ((PushService.EnmLocationStatus)lct.carlocstatus)
                {
                    case PushService.EnmLocationStatus.Space:              //空闲
                        {
                            if (lct.carlocsize == CStaticClass.ConfigCarMaxSize())
                            {// 空余大车个数
                                m_rectVehCount.Height++;
                            }
                            m_rectVehCount.Width++;
                            dgvc.Style.BackColor = Color.LightYellow;
                            break;
                        }
                    case PushService.EnmLocationStatus.Occupy:            //占用
                        {
                            m_rectVehCount.Y++;
                            dgvc.Style.BackColor = Color.Purple;
                            dgvc.Value = strValue;// lct.iccode;
                            //dgvc.Value = global::CarLocationPanelLib.Properties.Resources.CarGrey;
                            break;
                        }
                    case PushService.EnmLocationStatus.Entering:         //正在入库
                        {
                            dgvc.Style.BackColor = Color.Violet;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    case PushService.EnmLocationStatus.Outing:          //正在出库
                        {
                            dgvc.Style.BackColor = Color.SkyBlue;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    case PushService.EnmLocationStatus.MovingVEH:          //库内搬移
                        {
                            dgvc.Style.BackColor = Color.GreenYellow;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    case PushService.EnmLocationStatus.TmpFetch:          //临时取物出库
                        {
                            dgvc.Style.BackColor = Color.Blue;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    case PushService.EnmLocationStatus.VehRotation:        //旋转
                        {
                            dgvc.Style.BackColor = Color.DarkSalmon;
                            dgvc.Value = strValue;// lct.iccode;
                            break;
                        }
                    default:                                //初始
                        {
                            dgvc.Style.BackColor = Color.DarkGray;
                            break;
                        }
                }
            }
            else if (lct.carloctype == (int)EnmLocationType.Disable)//禁用车位
            {
                m_rectVehCount.X++;
                dgvc.Style.BackColor = Color.DimGray;
            }
            else if (lct.carloctype == (int)EnmLocationType.Hall)//车厅
            {
                if (!CStaticClass.GetPushServiceConnectFlag())
                {// 服务器通道断开时
                    return;
                }

                dgvc.Style.BackColor = Color.DarkKhaki;
                //QueryServiceClient proxy = new QueryServiceClient();
                CarLocationPanelLib.QueryService.CDeviceStatusDto deviceStatus = proxy.GetDeviceStatusByAddr(m_wareHouse, lct.carlocaddr.Substring(0,3)+"04", EnmSMGType.Hall);
                if (null == deviceStatus)
                {
                    //proxy.Close();
                    return;
                }
                dgvc.Value = (deviceStatus.devicecode-10).ToString()+"厅";//"#车厅"
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
            { //初始
                dgvc.Style.BackColor = Color.DarkGray;
            } 
            proxy.Close();

            //if (lct.IsLocked)
            //{
            //    dgvc.Style.BackColor = Color.DarkOrange;
            //}
        }

        /// <summary>
        /// 设置ETV位置
        /// </summary>
        /// <param name="nx"></param>
        /// <param name="nY"></param>
        private void SetETVLocation(int nX, int nY)
        {
            m_lblETVEquip.Location = new System.Drawing.Point(nX, nY);
        }

        /// <summary>
        /// 设置具体界面地址值
        /// </summary>
        /// <param name="strLocAddr"></param>
        protected void SetLocAddr(string strLocAddr)
        {
            CCarLocationEventArgs ea = new CCarLocationEventArgs();
            ea.ParentForm = this.Tag;
            ea.StrLocAddr = strLocAddr;
            ea.EnmSrcLocAddr = m_enmSrcLocAddr;

            if (null != CallbackCarLocationEvent)
            {
                CallbackCarLocationEvent(this, ea);
            }
        }

        /// <summary>
        /// 是否正确车位
        /// 1、禁用车位（该车位不允许禁用，请选择空闲正常车位）
        /// 2、启用车位（该车位不允许启用，请选择无效车位）
        /// 3、手动挪移源车位、手动出库车位、手动指令挪移动作的源车位、手动指令出库动作的源车位（请选择已存车的车位）
        /// 4、手动挪移目的车位、手动入库车位、手动指令挪移动作的目的车位（请选择正常空闲的车位）
        /// 5、手动指令移动源设备和手动指令出库目的车厅改变成下拉框
        /// 6、手动指令移动目的车位（请选择有效车位）
        /// 7、临时取物目的车厅选择时（请选择全自动的进出两用车厅）
        /// </summary>
        /// <param name="carLocation"></param>
        /// <returns></returns>
        private bool IsRightCarLocation(CarLocationPanelLib.QueryService.CCarLocationDto carLocation)
        {
            switch (m_enmSrcLocAddr)
            {
                case EnmTxtCarLocationAddr.Dis:
                    {
                        if (null == carLocation || carLocation.carloctype == (int)EnmLocationType.Hall)
                        {
                            MessageBox.Show("请选择车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        return true;
                    }
                case EnmTxtCarLocationAddr.HandInJogSrc:
                    {
                        if (null == carLocation || carLocation.carlocstatus != (int)PushService.EnmLocationStatus.Occupy)
                        {
                            MessageBox.Show("请选择已存车的车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        return true;
                    }
                case EnmTxtCarLocationAddr.HandOut:
                    {
                        if (null == carLocation || carLocation.carlocstatus != (int)PushService.EnmLocationStatus.Occupy)
                        {
                            MessageBox.Show("请选择已存车的车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        return true;
                    }
                case EnmTxtCarLocationAddr.HandInJogDest:
                    {
                        if (null == carLocation || carLocation.carloctype != (int)EnmLocationType.Normal || carLocation.carlocstatus != (int)PushService.EnmLocationStatus.Space)
                        {
                            MessageBox.Show("请选择正常空闲的车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        return true;
                    }
                case EnmTxtCarLocationAddr.HandIn:
                    {
                        if (null == carLocation || carLocation.carloctype != (int)EnmLocationType.Normal || carLocation.carlocstatus != (int)PushService.EnmLocationStatus.Space)
                        {
                            MessageBox.Show("请选择正常空闲的车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        return true;
                    }
                case EnmTxtCarLocationAddr.HandOrderSrc:
                    {
                        if (null == carLocation || carLocation.carlocstatus != (int)PushService.EnmLocationStatus.Occupy)
                        {
                            MessageBox.Show("请选择已存车的车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        return true;
                    }
                case EnmTxtCarLocationAddr.HandOrderDest:
                    {
                        if (null == carLocation)
                        {
                            MessageBox.Show("请选择地址!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        return true;
                    }
                case EnmTxtCarLocationAddr.Customer:
                    {
                        if (null == carLocation || carLocation.carloctype != (int)EnmLocationType.Normal)
                        {
                            MessageBox.Show("请选择正常的车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        return true;
                    }
                default:
                    {
                        break;
                    }
            }

            return true;
        }

        /// <summary>
        /// 设置ETV设备界面背景颜色变化
        /// </summary>
        /// <param name="deviceStatus"></param>
        private void SetDeviceBackColor(PushService.CDeviceStatusDto deviceStatus)
        {
            // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色
            if ((int)EnmModel.Automatic != deviceStatus.devicemode || CBaseMethods.MyBase.IsEmpty(deviceStatus.isable))
            {
                m_lblETVEquip.BackColor = Color.Yellow;
            }
            else if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.tasktype))
            {
                m_lblETVEquip.BackColor = Color.LightSkyBlue;
            }
            //else if (CBaseMethods.MyBase.IsEmpty(deviceStatus.isable) || CBaseMethods.MyBase.IsEmpty(deviceStatus.isavailable))
            //{
            //    m_lblETVEquip.BackColor = Color.Yellow;
            //}
            else
            {
                m_lblETVEquip.BackColor = Color.PaleGreen;
            }
        }

        /// <summary>
        /// 设置ETV设备界面背景颜色变化
        /// </summary>
        /// <param name="deviceStatus"></param>
        private void SetDeviceBackColor(QueryService.CDeviceStatusDto deviceStatus)
        {
            // 正在作业ETV颜色为蓝色、不可用/不可接受指令为黄色
            if ((int)EnmModel.Automatic != deviceStatus.devicemode || CBaseMethods.MyBase.IsEmpty(deviceStatus.isable))
            {
                m_lblETVEquip.BackColor = Color.Yellow;
            } 
            else if (!CBaseMethods.MyBase.IsEmpty(deviceStatus.tasktype))
            {
                m_lblETVEquip.BackColor = Color.LightSkyBlue;
            }
            //else if (CBaseMethods.MyBase.IsEmpty(deviceStatus.isable) || CBaseMethods.MyBase.IsEmpty(deviceStatus.isavailable))
            //{
            //    m_lblETVEquip.BackColor = Color.Yellow;
            //}
            else
            {
                m_lblETVEquip.BackColor = Color.PaleGreen;
            }
        }
        #endregion

        #region 车位、车厅、设备ETV双击触发
        /// <summary>
        /// 选择时触发，清空选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                dgv.ClearSelection();
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

        /// <summary>
        /// 单击车位单元格时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int gap = 2;
                DataGridView dgv = (DataGridView)sender;
                DataGridViewCell cell = dgv.CurrentCell;

                if (null == cell)
                {
                    return;
                }

                // 取消选择
                cell.Selected = false;
                // 获取当前单元格相对位置
                Point pos = new Point(0, cell.RowIndex * cell.Size.Height);
                if (dgv.RowHeadersVisible)
                {
                    pos.X += dgv.RowHeadersWidth;
                }

                for (int i = 0; i < cell.ColumnIndex && i < dgv.ColumnCount; i++)
                {
                    pos.X += dgv.Columns[i].Width;
                }

                if (dgv.ColumnHeadersVisible)
                {
                    pos.Y += dgv.ColumnHeadersHeight;
                }

                Rectangle bounds = new Rectangle(pos, cell.Size);
                Graphics dgvGRaphics = dgv.CreateGraphics();

                // 重绘上一次选择单元格的区域（擦除）
                if (null != dgv.Tag && dgv.Tag.GetType() == typeof(Rectangle))
                {
                    Rectangle erasureRect = (Rectangle)dgv.Tag;

                    if (erasureRect.X + erasureRect.Width == pos.X + gap && erasureRect.Y == pos.Y)
                    {// 重叠当前单元格左边
                        erasureRect.Width -= 2 * gap;
                    }
                    if (pos.X + bounds.Width == erasureRect.X && erasureRect.Y == pos.Y)
                    {// 重叠当前单元格右边
                        erasureRect.X += 2 * gap;
                    }
                    if (erasureRect.Y + erasureRect.Height == pos.Y + gap && erasureRect.X == pos.X)
                    {// 重叠当前单元格上边
                        erasureRect.Height -= 2 * gap;
                    }
                    if (pos.Y + bounds.Height == erasureRect.Y && erasureRect.X == pos.X)
                    {// 重叠当前单元格下边
                        erasureRect.Y += 2 * gap;
                    }

                    erasureRect.Width += gap;
                    erasureRect.Height += gap;

                    // 重绘上一次选择单元格的区域（擦除）
                    dgv.Invalidate(erasureRect);
                }

                // 绘制当前单元格选择框
                using (Pen pen = new Pen(Color.Blue))
                {
                    bounds.Width += gap;
                    bounds.Height += gap;
                    //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                    dgvGRaphics.DrawRectangle(pen, bounds);
                    dgv.Tag = bounds;
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
        }

        /// <summary>
        /// 双击车位单元格时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgv_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int nRowHeadersVisible = -1;// ((DataGridView)sender).RowHeadersVisible ? -1 : 0;
            int nColumnHeadersVisible = -1;// ((DataGridView)sender).ColumnHeadersVisible ? -1 : 0;

            if (nRowHeadersVisible >= e.RowIndex || nColumnHeadersVisible >= e.ColumnIndex)
            {
                return;
            }

            m_strCurrCarLocAddr = GetAddressByCell((DataGridView)sender, new Point(e.RowIndex, e.ColumnIndex));
            OnClickCarLocationInfo(null, null);
        }

        /// <summary>
        /// ETV移动框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void m_lblETVStatus_DoubleClick(object sender, EventArgs e)
        {
            if (m_enmSrcLocAddr != EnmTxtCarLocationAddr.FormCarLocation &&
                m_enmSrcLocAddr != EnmTxtCarLocationAddr.FormETV &&
                m_enmSrcLocAddr != EnmTxtCarLocationAddr.FormHall)
            {// 双击获取车位地址类型
                MessageBox.Show("请选择车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //SetLocAddr(m_lblETVEquip.Name);
                return;
            }

            if (null == sender || typeof(Label) != sender.GetType())
            {
                return;
            }
            Label lblTV = (Label)sender;

            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                int nEquipID;
                int.TryParse(lblTV.Name, out nEquipID);
                CarLocationPanelLib.QueryService.CDeviceStatusDto equipStatusTable = proxy.GetDeviceStatus(m_wareHouse, nEquipID);

                if (null == equipStatusTable)
                {
                    MessageBox.Show("没有指定设备！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CCarLocationEventArgs ea = new CCarLocationEventArgs();
                ea.EnmSrcLocAddr = EnmTxtCarLocationAddr.FormETV;
                ea.ParamDto = equipStatusTable;

                if (null != CallbackCarLocationEvent)
                {
                    CallbackCarLocationEvent(this, ea);
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
        #endregion

        #region 车位右键菜单
        /// <summary>
        /// 单元格弹出右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dgv_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex >= 0)
                    {
                        // 获取当前单元格车位地址
                        m_strCurrCarLocAddr = GetAddressByCell((DataGridView)sender, new Point(e.RowIndex, e.ColumnIndex));
                        //弹出操作菜单
                        m_contextMenuStrip.Show(MousePosition.X, MousePosition.Y);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "应用程序异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        /// <summary>
        /// 设置右键菜单项
        /// </summary>
        protected void SetContextMenuStripItems()
        {
            m_contextMenuStrip.Font=new System.Drawing.Font("微软雅黑", 10.5F);
            m_contextMenuStrip.Items.Add("车位信息", null, new EventHandler(OnClickCarLocationInfo));
            ToolStripSeparator separator1=new ToolStripSeparator();
            m_contextMenuStrip.Items.Add(separator1);           
            m_contextMenuStrip.Items.Add("出库", null, new EventHandler(OnClickHandOut));
            ToolStripSeparator separator2 = new ToolStripSeparator();
            m_contextMenuStrip.Items.Add(separator2);
            m_contextMenuStrip.Items.Add("禁用", null, new EventHandler(OnClickDisable));
            m_contextMenuStrip.Items.Add("启用", null, new EventHandler(OnClickEnable));
            ToolStripSeparator separator3 = new ToolStripSeparator();
            m_contextMenuStrip.Items.Add(separator3);
            m_contextMenuStrip.Items.Add("更改车位状态", null, new EventHandler(OnClickModifyCarLocStatus));
        }

        /// <summary>
        /// 车位信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClickCarLocationInfo(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                string strCarLocAddr = m_strCurrCarLocAddr;
                // 根据库号和车位地址获取车位信息，并处理
                CarLocationPanelLib.QueryService.CCarLocationDto carLocation = new CarLocationPanelLib.QueryService.CCarLocationDto();
                carLocation.warehouse = m_wareHouse;
                carLocation.carlocaddr = strCarLocAddr;
                CICCardDto iccard = new CICCardDto();
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryCarPOSNByAddr(ref carLocation, ref iccard);

                if (m_enmSrcLocAddr != EnmTxtCarLocationAddr.FormCarLocation &&
                    m_enmSrcLocAddr != EnmTxtCarLocationAddr.FormETV &&
                    m_enmSrcLocAddr != EnmTxtCarLocationAddr.FormHall)
                {// 双击获取车位地址类型
                    if (IsRightCarLocation(carLocation))
                    {
                        SetLocAddr(strCarLocAddr);
                    }
                    return;
                }

                if (type == CarLocationPanelLib.QueryService.EnmFaultType.Fail || null == carLocation)
                {
                    MessageBox.Show("无效车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (carLocation.carloctype == (int)EnmLocationType.Hall)
                {
                    // 车厅
                    string hallAddrs = carLocation.carlocaddr.Substring(0, 3) + "04";
                    CarLocationPanelLib.QueryService.CDeviceStatusDto hallStatusTable = proxy.GetDeviceStatusByAddr(m_wareHouse, hallAddrs, EnmSMGType.Hall);

                    if (null == hallStatusTable)
                    {
                        MessageBox.Show("没有指定设备！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    CCarLocationEventArgs ea = new CCarLocationEventArgs();
                    ea.EnmSrcLocAddr = EnmTxtCarLocationAddr.FormHall;
                    ea.ParamDto = hallStatusTable;

                    if (null != CallbackCarLocationEvent)
                    {
                        CallbackCarLocationEvent(this, ea);
                    }
                }
                else
                {
                    //if (carLocation.carloctype != (int)EnmLocationType.Normal && carLocation.carloctype != (int)EnmLocationType.Temp)//正常车位
                    //{
                    //    MessageBox.Show("非正常车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    CCarLocationEventArgs ea = new CCarLocationEventArgs();
                    if (null == iccard)
                    {
                        ea.NICType = -1;
                    }
                    else
                    {
                        ea.NICType = (int)iccard.ictype;
                    }

                    ea.EnmSrcLocAddr = EnmTxtCarLocationAddr.FormCarLocation;
                    ea.ParamDto = carLocation;

                    if (null != CallbackCarLocationEvent)
                    {
                        CallbackCarLocationEvent(this, ea);
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
        /// 更改车位状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClickModifyCarLocStatus(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            try
            {
                string strCarLocAddr = m_strCurrCarLocAddr;
                // 根据库号和车位地址获取车位信息，并处理
                CarLocationPanelLib.QueryService.CCarLocationDto carLocation = new CarLocationPanelLib.QueryService.CCarLocationDto();
                carLocation.warehouse = m_wareHouse;
                carLocation.carlocaddr = strCarLocAddr;
                CICCardDto iccard = new CICCardDto();
                CarLocationPanelLib.QueryService.EnmFaultType type = proxy.QueryCarPOSNByAddr(ref carLocation, ref iccard);

                if (type == CarLocationPanelLib.QueryService.EnmFaultType.Fail || null == carLocation)
                {
                    MessageBox.Show("无效车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (carLocation.carloctype == (int)EnmLocationType.Normal)
                {// 车位
                    m_txtCarLocStatusOld.Text = CStaticClass.ConvertCarLocStatus(carLocation.carlocstatus);
                    m_form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("非正常车位!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// 手动出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClickHandOut(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                string strCarLocAddr = m_strCurrCarLocAddr;
                string msg = "";
                try
                {                   
                    if (strCarLocAddr != null)
                    {
                        msg = " 车位地址：" + strCarLocAddr +
                            " （" + strCarLocAddr.Substring(0, 1) + "边"
                        + Convert.ToInt32(strCarLocAddr.Substring(1, 2)) + "列"
                        + Convert.ToInt32(strCarLocAddr.Substring(3)) + "层" + "） ";
                    }
                }
                catch { }

                DialogResult dr = MessageBox.Show(msg+"确定出库吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                if (string.IsNullOrEmpty(strCarLocAddr))
                {
                    MessageBox.Show("无效车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.PushService.EnmFaultType type = push.ManualVEHExit(m_wareHouse, strCarLocAddr);
                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "手动出库成功：车位-" + strCarLocAddr + "  库号-" + m_wareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("出库成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("获取车位信息失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.HallEquip:
                        {
                            MessageBox.Show("当前车位是车厅设备", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotNormalCarPOSN:
                        {
                            MessageBox.Show("不是正常车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.CarInGarage:
                        {
                            MessageBox.Show("当前车位无车，无车出库", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Null:
                        {
                            MessageBox.Show("空闲车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("出库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 禁用车位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClickDisable(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                string strCarLocAddr = m_strCurrCarLocAddr;
                if (string.IsNullOrEmpty(strCarLocAddr))
                {
                    MessageBox.Show("无效车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.PushService.EnmFaultType type = push.ModifyCarPOSNType(m_wareHouse, strCarLocAddr, EnmLocationType.Disable);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "禁用车位成功：车位-" + strCarLocAddr + "  库号-" + m_wareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);                           
                            string msg = "";
                            try
                            {
                                msg = " 车位地址：" + strCarLocAddr +
                                    " （" + strCarLocAddr.Substring(0, 1) + "边"
                                + Convert.ToInt32(strCarLocAddr.Substring(1, 2)) + "列"
                                + Convert.ToInt32(strCarLocAddr.Substring(3)) + "层" + "）";
                            }
                            catch
                            { }
                            MessageBox.Show(msg+"禁用成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到指定车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotAllowed:
                        {
                            MessageBox.Show("该车位非空闲正常车位，不允许禁用该车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("禁用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("禁用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }
      
        /// <summary>
        /// 启用车位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClickEnable(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                string strCarLocAddr = m_strCurrCarLocAddr;
                if (string.IsNullOrEmpty(strCarLocAddr))
                {
                    MessageBox.Show("无效车位！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                CarLocationPanelLib.PushService.EnmFaultType type = push.ModifyCarPOSNType(m_wareHouse, strCarLocAddr, EnmLocationType.Normal);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "启用车位成功：车位-" + strCarLocAddr + "  库号-" + m_wareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            string msg = "";
                            try
                            {
                                msg = " 车位地址：" + strCarLocAddr +
                                    " （" + strCarLocAddr.Substring(0, 1) + "边"
                                + Convert.ToInt32(strCarLocAddr.Substring(1, 2)) + "列"
                                + Convert.ToInt32(strCarLocAddr.Substring(3)) + "层" + "）";
                            }
                            catch
                            { }
                            MessageBox.Show(msg+"启用成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到指定车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.NotAllowed:
                        {
                            MessageBox.Show("该车位非禁用车位，不允许启用该车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("启用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }
      
        /// <summary>
        /// 一键出库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClickAllExit(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                DialogResult dr = MessageBox.Show("确定一键出库吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                CarLocationPanelLib.PushService.EnmFaultType type = push.InitAllCarPOSN(m_wareHouse);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "一键出库成功： 库号-" + m_wareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("一键出库成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("一键出库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }
       
        /// <summary>
        /// 一键禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClickAllDisable(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                DialogResult dr = MessageBox.Show("确定一键禁用吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                CarLocationPanelLib.PushService.EnmFaultType type = push.ModifyAllCarPOSNType(m_wareHouse, EnmLocationType.Disable);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "一键禁用成功： 库号-" + m_wareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("一键禁用成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("一键禁用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }
      
        /// <summary>
        /// 一键启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClickAllEnable(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                DialogResult dr = MessageBox.Show("确定一键启用吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);

                if (dr == DialogResult.Cancel)
                {
                    return;
                }
                CarLocationPanelLib.PushService.EnmFaultType type = push.ModifyAllCarPOSNType(m_wareHouse, EnmLocationType.Normal);

                switch (type)
                {
                    case CarLocationPanelLib.PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "一键启用成功： 库号-" + m_wareHouse;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("一键启用成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case CarLocationPanelLib.PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("一键启用失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

       
        /// <summary>
        /// 初始化修改车位状态界面
        /// </summary>
        protected void InitModifyCarLocStatusForm()
        {
            Label LblCarLocStatusOld = new Label();
            Label LblCarLocStatusNew = new Label();
            Button BtnOK = new Button();
            Button BtnCancel = new Button();
            m_form.Controls.Add(LblCarLocStatusOld);
            m_form.Controls.Add(LblCarLocStatusNew);
            m_form.Controls.Add(m_txtCarLocStatusOld);
            m_form.Controls.Add(m_cboCarLocStatusNew);
            m_form.Controls.Add(BtnOK);
            m_form.Size = new System.Drawing.Size(350, 260);
            m_form.MaximizeBox = false;
            m_form.CancelButton = BtnCancel;
            m_form.FormBorderStyle = FormBorderStyle.FixedDialog;
            m_form.StartPosition = FormStartPosition.CenterScreen;
            m_form.Font = new System.Drawing.Font("宋体", 12F); ;
            m_form.Text = "修改车位状态";
            // 
            // LblCarLocStatusOld
            // 
            LblCarLocStatusOld.Location = new System.Drawing.Point(60, 50);
            LblCarLocStatusOld.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LblCarLocStatusOld.Name = "LblCarLocStatusOld";
            LblCarLocStatusOld.Size = new System.Drawing.Size(140, 16);
            LblCarLocStatusOld.Text = "修改前车位状态";
            LblCarLocStatusOld.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtCarLocStatusOld
            // 
            m_txtCarLocStatusOld.Enabled = false;
            m_txtCarLocStatusOld.Location = new System.Drawing.Point(200, 50);
            m_txtCarLocStatusOld.Margin = new System.Windows.Forms.Padding(4);
            m_txtCarLocStatusOld.Name = "TxtCarLocStatusOld";
            m_txtCarLocStatusOld.Size = new System.Drawing.Size(92, 26);
            // 
            // LblCarLocStatusNew
            // 
            LblCarLocStatusNew.Location = new System.Drawing.Point(60, 100);
            LblCarLocStatusNew.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            LblCarLocStatusNew.Name = "LblCarLocStatusNew";
            LblCarLocStatusNew.Size = new System.Drawing.Size(140, 16);
            LblCarLocStatusNew.Text = "修改后车位状态";
            LblCarLocStatusNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TxtCarLocStatusNew
            // 
            m_cboCarLocStatusNew.Location = new System.Drawing.Point(200, 100);
            m_cboCarLocStatusNew.Margin = new System.Windows.Forms.Padding(4);
            m_cboCarLocStatusNew.Name = "TxtCarLocStatusNew";
            m_cboCarLocStatusNew.Size = new System.Drawing.Size(92, 26);
            m_cboCarLocStatusNew.Items.AddRange(new object[] { "空闲", "占用", "入库", "出库", "库内挪移", "临时取物", "车辆旋转" });
            m_cboCarLocStatusNew.DropDownStyle = ComboBoxStyle.DropDownList;
            m_cboCarLocStatusNew.SelectedItem = 0;
            // 
            // BtnOK
            // 
            BtnOK.Location = new System.Drawing.Point(101, 160);
            BtnOK.Margin = new System.Windows.Forms.Padding(4);
            BtnOK.Name = "BtnOK";
            BtnOK.Size = new System.Drawing.Size(133, 31);
            BtnOK.TabIndex = 2;
            BtnOK.Text = "确定修改";
            BtnOK.UseVisualStyleBackColor = true;
            BtnOK.Click += new System.EventHandler(BtnOK_Click);
            // 
            // BtnCancel
            // 
            BtnCancel.Click += new System.EventHandler(BtnCancel_Click);
        }

        /// <summary>
        /// 确定修改车位状态信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnOK_Click(object sender, EventArgs e)
        {
            if (!CStaticClass.CheckPushService())
            {// 检查服务
                return;
            }

            QueryServiceClient proxy = new QueryServiceClient();
            PushServiceClient push = new PushServiceClient(new System.ServiceModel.InstanceContext(CStaticClass.myCallback));
            try
            {
                if (string.IsNullOrEmpty(m_strCurrCarLocAddr) || 0 > m_cboCarLocStatusNew.SelectedIndex)
                {
                    MessageBox.Show("车位、车位状态都不为空!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (m_cboCarLocStatusNew.Text == m_txtCarLocStatusOld.Text)
                {
                    MessageBox.Show("请选择与修改前不一致的车位状态!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PushService.EnmFaultType type = push.ModifyCarPOSNStatus(m_wareHouse, m_strCurrCarLocAddr, (PushService.EnmLocationStatus)CStaticClass.ConvertCarLocStatus(m_cboCarLocStatusNew.Text));

                switch (type)
                {
                    case PushService.EnmFaultType.Success:
                        {
                            CSystemLogDto log = new CSystemLogDto();
                            log.curtime = CStaticClass.CurruntDateTime();
                            log.logdescp = "修改车位状态成功：车位-" + m_strCurrCarLocAddr + "  库号-" + m_wareHouse + "  修改前车位状态-" + m_txtCarLocStatusOld.Text + "  修改后车位状态-" + m_cboCarLocStatusNew.Text;
                            log.optcode = CStaticClass.myOperator.optcode;
                            log.optname = CStaticClass.myOperator.optname;
                            proxy.InsertSysLog(log);
                            MessageBox.Show("修改车位状态成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    case PushService.EnmFaultType.NotFoundCarPOSN:
                        {
                            MessageBox.Show("没有找到指定车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case PushService.EnmFaultType.NotAllowed:
                        {
                            MessageBox.Show("该车位非空闲正常车位，不允许禁用该车位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case PushService.EnmFaultType.FailToUpdate:
                        {
                            MessageBox.Show("更新数据库失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case PushService.EnmFaultType.Fail:
                        {
                            MessageBox.Show("修改车位状态失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case PushService.EnmFaultType.Exception:
                        {
                            MessageBox.Show(CStaticClass.GetExceptionInfo(null), "连接异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("修改车位状态失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            push.Close();
        }

        /// <summary>
        /// 关闭修改车位状态界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            m_form.Close();
        }
        #endregion
    }
}
