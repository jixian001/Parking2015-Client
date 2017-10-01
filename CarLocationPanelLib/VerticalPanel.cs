using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BaseMethodLib;

namespace CarLocationPanelLib
{
    /// <summary>
    /// 通道是垂直布局的车位
    /// </summary>
    public class CVerticalPanel: CWareHousePanel
    {
        public CVerticalPanel()
            : base()
        {}

        public CVerticalPanel(Rectangle rectData)
            : base()
        {
            m_wareHouse = rectData.X;
            m_side = rectData.Y;
            m_column = rectData.Width;
            m_layer = rectData.Height;
            HandInitializeComponent();
        }

        #region 重载函数
        /// <summary>
        /// 手动初始化布局
        /// </summary>
        protected override void HandInitializeComponent()
        {
            //try
            //{
                // 
                // panel
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
                    m_lblETVEquip.Text = "E\nT\nV";//"ETV"
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
                bool isCrossLeft = true;// 边是否在通道左侧

                while (0 != num)
                {
                    int i = num % 10;// 边数

                    if (0 == i)
                    {// 通道
                        num = num / 10;
                        isCrossLeft = false;
                        continue;
                    }

                    if (isCrossLeft)
                    {
                        m_crossIndex++;
                    }
                    // 边文本
                    Label lblSide = new Label();
                    lblSide.Name = i.ToString();
                    lblSide.Text = "第" + i + "边";
                    lblSide.TextAlign = ContentAlignment.MiddleCenter;
                    lblSide.BackColor = System.Drawing.Color.LightGreen;
                    // 车位排列
                    DataGridView dgv = new DataGridView();
                    dgv.Font = new System.Drawing.Font("Arial Black", 12F);
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
                    dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 12F);
                    dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
                    dgv.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("宋体", 7F);
                    //dgv.RowHeadersWidth = 45;

                    //dgv.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Red;
                    //dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
                    //dgv.RowTemplate.Resizable = DataGridViewTriState.False;

                    for (int j = 0; j < col; j++)
                    {// 增加列
                        if (isCrossLeft)
                        {// 通道左侧边
                            dgv.Columns.Add("l" + (col - j), col - j + strColFlag);
                            //dgv.Columns[0].HeaderText = strFlag;
                        }
                        else
                        {
                            dgv.Columns.Add("l" + (j + 1), (j + 1) + strColFlag);
                        }

                        dgv.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                    }

                    for (int j = 0; j < row; j++)
                    {// 增加行
                        dgv.Rows.Add();

                        if (num == m_side)
                        {// 第一边
                            dgv.RowHeadersVisible = true;
                            dgv.Rows[j].HeaderCell.Value = (row - j).ToString();
                            dgv.Rows[j].HeaderCell.ToolTipText = (row - j) + strFlag;
                            //dgv.Rows[j].Cells[0].Value = row - j;
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
                this.Size = new System.Drawing.Size(1000, 500);
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
            int sideHigh = CStaticClass.ConfigControlSize();

            int width = Size.Width;
            int height = Size.Height;
            int maxCount = m_lDgvCarLocation.Count() > m_lLblSide.Count() ? m_lDgvCarLocation.Count() : m_lLblSide.Count();
            int curWidth = 0;
            int col = m_column;
            int row = m_layer;
            // 每个车位行高和列宽
            int rowHigh = (height - 2 * sideHigh - gap) / row;
            for (int i = 0; i < m_lDgvCarLocation.Count(); i++)
            {
                width -= m_lDgvCarLocation[i].RowHeadersVisible ? m_lDgvCarLocation[i].RowHeadersWidth : 0;
            }
            int colWidth = (width - sideHigh - (maxCount - 2) * gap) / (maxCount * col);

            for (int i = 0; i < maxCount; i++)
            {
                int sideWidth = colWidth * col;

                if (i < m_lDgvCarLocation.Count())
                {// 车位排列宽度和高度调整
                    sideWidth += m_lDgvCarLocation[i].RowHeadersVisible ? m_lDgvCarLocation[i].RowHeadersWidth : 0;// sideHigh;
                    m_lDgvCarLocation[i].Location = new System.Drawing.Point(curWidth, 0);
                    m_lDgvCarLocation[i].Size = new System.Drawing.Size(sideWidth, rowHigh * row + sideHigh);
                    for (int j = 0; j < col; j++)
                    {  //第一列的宽
                        //if (0 != j)
                        //{
                            m_lDgvCarLocation[i].Columns[j].Width = colWidth;
                        //}
                        //else if (0 == i)
                        //{// 第一边
                        //    m_lDgvCarLocation[i].Columns[j].Width = sideHigh;
                        //}
                        //else
                        //{
                        //    m_lDgvCarLocation[i].Columns[j].Width = 0;
                        //}
                    }

                    for (int j = 0; j < row; j++)
                    {
                        m_lDgvCarLocation[i].Rows[j].Height = rowHigh;
                    }
                }

                if (i < m_lLblSide.Count())
                {// 边
                    m_lLblSide[i].Location = new System.Drawing.Point(curWidth, rowHigh * row + sideHigh);
                    m_lLblSide[i].Size = new System.Drawing.Size(sideWidth, sideHigh);
                }

                curWidth += sideWidth;
                if (m_crossIndex == i)
                {// 通道ETV
                    m_lblCross.Location = new System.Drawing.Point(curWidth, 0);
                    m_lblCross.Size = new System.Drawing.Size(sideHigh, Size.Height);
                    m_lblETVEquip.Location = new System.Drawing.Point(curWidth, m_lDgvCarLocation[i].ColumnHeadersHeight + rowHigh * (row - 1));//m_lDgvCarLocation[i].ColumnHeadersHeight);
                    int nW = colWidth > rowHigh ? colWidth / 2 : colWidth;// sideHigh
                    nW = nW < sideHigh ? nW : sideHigh;
                    int nH = colWidth > rowHigh ? rowHigh : rowHigh / 2;// rowHigh
                    m_lblETVEquip.Size = new System.Drawing.Size(nW, nH);
                    curWidth += sideHigh;
                }
                else
                {
                    curWidth += gap;
                }
            }
            // 更新ETV位置
            UpdateDeviceStatus();
        }

        /// <summary>
        /// 获取当前单元格的车位地址
        /// </summary>
        protected override string GetAddressByCell(DataGridView dgv, Point pos)
        {
            int nDgvIndex = m_lDgvCarLocation.IndexOf(dgv);
            int side = dgv.TabIndex;
            int column = pos.Y + 1;

            if (nDgvIndex <= m_crossIndex)
            {// 边在通道左侧边
                column = dgv.ColumnCount - pos.Y;
            }

            int row = dgv.RowCount - pos.X;
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

            //获取当前ETV位置值
            for (int nDgvIndex = 0; nDgvIndex < m_lDgvCarLocation.Count; nDgvIndex++)
            {
                dgv = m_lDgvCarLocation[nDgvIndex];
                if (null == dgv || dgv.TabIndex != side)
                {
                    continue;
                }

                int nColumnIndex = column - 1;
                int row = layer;
                int nRowIndex = dgv.RowCount - row;

                if (nDgvIndex <= m_crossIndex)
                {// 边在通道左侧边
                    nColumnIndex = dgv.ColumnCount - column;
                }

                if (-1 < nColumnIndex && -1 < nRowIndex && nColumnIndex < dgv.ColumnCount && nRowIndex < dgv.RowCount)
                {
                    dgvc = dgv[nColumnIndex, nRowIndex];
                }
                break;
            }
        }
        #endregion
    }
}
