using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace CustomControlLib
{
    #region 定义枚举和结构类
    /// <summary>
    /// 节点类型
    /// </summary>
    public enum EnmNodeType
    {
        Init = 0,
        Circle,
        LineArrow,
        DownLineArrow,
        UpLineArrow,
        End,
        Rect
    }

    /// <summary>
    /// 流程作业类型
    /// </summary>
    public enum EnmFlowTaskType
    {
        Init = 0,
        /// <summary>
        /// 正常存车
        /// </summary>
        NormEnter,
        /// <summary>
        /// 正常取车
        /// </summary>
        NormExit,
        /// <summary>
        /// 取车排队
        /// </summary>
        QueueExit,
        /// <summary>
        /// 临时取物存车
        /// </summary>
        TmpFetchEnter,
        /// <summary>
        /// 临时取物
        /// </summary>
        TmpFetch,
        /// <summary>
        /// 挪移
        /// </summary>
        MoveCar,
        /// <summary>
        /// 移动
        /// </summary>
        MoveEquip,
        /// <summary>
        /// 塔库存车
        /// </summary>
        TowerEnter,
        /// <summary>
        /// 塔库转存车
        /// </summary>
        TowerExitEnter,
        /// <summary>
        /// 塔库取车转存
        /// </summary>
        TowerExit
    }

    /// <summary>
    /// 节点数据结构类
    /// </summary>
    public class CStructFlowChartNode
    {
        #region 成员变量
        /// <summary>
        /// 前一节点序号
        /// </summary>
        private int m_nPrevNode;

        /// <summary>
        /// 当前节点序号
        /// </summary>
        private int m_nCurrentNode;

        /// <summary>
        /// 位置坐标
        /// </summary>
        private Point m_posLocation;

        /// <summary>
        /// 节点说明
        /// </summary>
        private string m_strExplain;

        /// <summary>
        /// 节点提示报文/卡号
        /// </summary>
        private string m_strToolTip;

        /// <summary>
        /// 节点类型：圆节点、直线箭头
        /// </summary>
        private EnmNodeType m_enmNodeType;

        /// <summary>
        /// 节点颜色
        /// </summary>
        private Color m_colorNode;
        #endregion

        #region 属性
        /// <summary>
        /// 前一节点序号
        /// </summary>
        public int NPrevNode
        {
            get
            {
                return m_nPrevNode;
            }
            set
            {
                m_nPrevNode = value;
            }
        }

        /// <summary>
        /// 当前节点序号
        /// </summary>
        public int NCurrentNode
        {
            get
            {
                return m_nCurrentNode;
            }
            set
            {
                m_nCurrentNode = value;
            }
        }

        /// <summary>
        /// 位置坐标
        /// </summary>
        public Point PosLocation
        {
            get
            {
                return m_posLocation;
            }
            set
            {
                m_posLocation = value;
            }
        }

        /// <summary>
        /// 节点说明
        /// </summary>
        public string StrExplain
        {
            get
            {
                return m_strExplain;
            }
            set
            {
                m_strExplain = value;
            }
        }

        /// <summary>
        /// 节点提示报文/卡号
        /// </summary>
        public string StrToolTip
        {
            get
            {
                return m_strToolTip;
            }
            set
            {
                m_strToolTip = value;
            }
        }

        /// <summary>
        /// 节点类型：圆节点、直线箭头
        /// </summary>
        public EnmNodeType EnumNodeType
        {
            get
            {
                return m_enmNodeType;
            }
            set
            {
                m_enmNodeType = value;
            }
        }

        /// <summary>
        /// 节点颜色
        /// </summary>
        public Color ColorNode
        {
            get
            {
                return m_colorNode;
            }
            set
            {
                m_colorNode = value;
            }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_nNode"></param>
        /// <param name="_posLocation"></param>
        /// <param name="_strExplain"></param>
        /// <param name="_strToolTip"></param>
        /// <param name="_enmNodeType"></param>
        /// <param name="_colorNode"></param>
        public CStructFlowChartNode(int _nPrevNode, int _nCurrentNode, Point _posLocation, string _strExplain, string _strToolTip, EnmNodeType _enmNodeType, Color _colorNode)
        {
            m_nPrevNode = _nPrevNode;
            m_nCurrentNode = _nCurrentNode;
            m_posLocation = _posLocation;
            m_strExplain = _strExplain;
            m_strToolTip = _strToolTip;
            m_enmNodeType = _enmNodeType;
            m_colorNode = _colorNode;
        }
    }
    #endregion

    // 代理实现异步调用以设置TextBox控件text属性
    public delegate void RefreshCallback();
    /// <summary>
    /// 流程图控件
    /// </summary>
    public class CUserFlowChartControl : Control
    {
        /// <summary>
        /// 外部回调连接事件
        /// </summary>
        public event RefreshCallback CallbackResize;

        private List<CStructFlowChartNode> m_lstStruFlowChartNode = new List<CStructFlowChartNode>();
        private EnmFlowTaskType m_enmFlowTaskType;
        private Hashtable m_hashFlowChart = new Hashtable();
        private int m_nLayer = 0;

        #region 读取配置文件
        /// <summary>
        /// 解释文本说明流程节点颜色
        /// </summary>
        private Color m_explainTextColor = Color.Black;
        /// <summary>
        /// 解释报文说明流程节点颜色
        /// </summary>
        private Color m_toopLipTextColor = Color.BlueViolet;
        /// <summary>
        /// 正常流程节点颜色
        /// </summary>
        private Color m_normColor = Color.LightGreen;
        /// <summary>
        /// 结束流程节点颜色
        /// </summary>
        private Color m_endColor = Color.LightGray;
        /// <summary>
        /// 正在运行流程节点颜色
        /// </summary>
        private Color m_runningColor = Color.LightYellow;
        /// <summary>
        /// 故障流程节点颜色
        /// </summary>
        private Color m_errorColor = Color.Red;
        private int m_nNodeWidth = 80;
        private int m_nArrowWidth = 50;
        private int m_nNodeHeight = 50;
        private int m_nCircleGap = 10;
        private int m_nArrowGap = 6;
        #endregion

        public CUserFlowChartControl() :
            base()
        {
            base.Font = new System.Drawing.Font("宋体", 9F);
        }

        #region 属性
        public EnmFlowTaskType EnumFlowTaskType
        {
            get
            {
                return m_enmFlowTaskType;
            }
            set
            {
                m_enmFlowTaskType = value;
                SetLstStruFlowChartNode();
            }
        }
        #endregion

        #region 公有函数
        /// <summary>
        /// 初始化整个存车流程
        /// </summary>
        public void InitFlowChart()
        {
            foreach (CStructFlowChartNode node in m_lstStruFlowChartNode)
            {
                node.ColorNode = m_normColor;
            }
            m_nLayer = 0;

            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.InvokeRequired)
            {
                RefreshCallback d = new RefreshCallback(SetRefresh);
                this.Invoke(d, null);
            }
            else
            {
                this.Refresh();
            }
        }

        /// <summary>
        /// 设置存车流程当前运行的状态
        /// </summary>
        /// <param name="nPrevNode"></param>
        /// <param name="nCurrentNode"></param>
        public void SetFlowChartRunStatus(int nPrevNode, int nCurrentNode)
        {
            // 两个节点是初始化时则整个流程图初始化
            if (1 > nPrevNode && 1 > nCurrentNode)
            {
                InitFlowChart();
                return;
            }

            //if (!IsRightRunFlow(nPrevNode, nCurrentNode))
            //{
            //    return;
            //}

            CStructFlowChartNode nodeCurr = m_lstStruFlowChartNode.Find(s => s.PosLocation.Y == m_nLayer && s.NPrevNode == nPrevNode && s.NCurrentNode == nCurrentNode && IsNode(s.EnumNodeType));
            if (null == nodeCurr)
            {// 当前节点（非箭头）
                return;
            }
            nodeCurr.ColorNode = m_endColor;

            CStructFlowChartNode nodePrev = m_lstStruFlowChartNode.Find(s => s.PosLocation.X < nodeCurr.PosLocation.X && s.PosLocation.Y == m_nLayer && s.NCurrentNode == nCurrentNode && IsArrow(s.EnumNodeType));
            CStructFlowChartNode nodeNext = m_lstStruFlowChartNode.Find(s => s.PosLocation.X > nodeCurr.PosLocation.X && s.PosLocation.Y == m_nLayer && s.NPrevNode == nCurrentNode && IsArrow(s.EnumNodeType));

            if (null != nodePrev)
            {
                nodePrev.ColorNode = m_endColor;
                CStructFlowChartNode nodePrevU = m_lstStruFlowChartNode.Find(s => s.PosLocation.X < nodeCurr.PosLocation.X && s.NCurrentNode == nCurrentNode && s.EnumNodeType == EnmNodeType.UpLineArrow && s.ColorNode == m_runningColor);
                if (null != nodePrevU)
                {
                    nodePrevU.ColorNode = m_endColor;
                }
            }

            if (null != nodeNext && !IsJoint(nodeCurr))
            {// 当前节点非交接点，下一箭头才变化
                nodeNext.ColorNode = m_runningColor;

                if (EnmNodeType.UpLineArrow == nodeNext.EnumNodeType)
                {
                    m_nLayer = 0;
                }
            }
            // 结束
            if (EnmNodeType.End == nodeCurr.EnumNodeType)
            {
                m_nLayer = 0;
            }
            // 转下一流程图
            else if (EnmNodeType.Rect == nodeCurr.EnumNodeType)
            {
                EnumFlowTaskType -= 1;
                SetFlowChartRunStatus(nPrevNode, nPrevNode);
                SetFlowChartRunStatus(nPrevNode, nCurrentNode);
            }

            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.InvokeRequired)
            {
                RefreshCallback d = new RefreshCallback(SetRefresh);
                this.Invoke(d, null);
            }
            else
            {
                this.Refresh();
            }
        }
        #endregion

        #region 重载函数
        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;
            int nWidth = m_nNodeWidth;
            int nHeight = m_nNodeHeight;

            if (null == m_lstStruFlowChartNode || 1 > m_lstStruFlowChartNode.Count)
            {
                graph.DrawString("待命", base.Font, new Pen(m_explainTextColor).Brush, new Point(m_nCircleGap, m_nCircleGap));
                this.ClientSize = new System.Drawing.Size(nWidth, nHeight);
                return;
            }

            foreach (CStructFlowChartNode node in m_lstStruFlowChartNode)
            {
                nWidth = Math.Max(nWidth, (node.PosLocation.X / 2 + 1) * m_nNodeWidth + node.PosLocation.X / 2 * m_nArrowWidth);
                nHeight = Math.Max(nHeight, (node.PosLocation.Y + 1) * m_nNodeHeight);
                switch (node.EnumNodeType)
                {
                    case EnmNodeType.Circle:
                        {
                            DrawCircleNode(graph, node);
                            break;
                        }
                    case EnmNodeType.LineArrow:
                        {
                            DrawLineArrow(graph, node);
                            break;
                        }
                    case EnmNodeType.DownLineArrow:
                        {
                            DrawDownLineArrow(graph, node);
                            break;
                        }
                    case EnmNodeType.UpLineArrow:
                        {
                            DrawUpLineArrow(graph, node);
                            break;
                        }
                    case EnmNodeType.End:
                        {
                            DrawEnd(graph, node);
                            break;
                        }
                    case EnmNodeType.Rect:
                        {
                            DrawRect(graph, node);
                            break;
                        }
                }
            }

            nWidth += 4 * m_nNodeWidth;
            nHeight += m_nNodeHeight;
            this.ClientSize = new System.Drawing.Size(nWidth, nHeight);

            if (null != CallbackResize)
            {
                CallbackResize();
            }
        }
        #endregion

        #region 私有函数——绘制节点和箭头
        /// <summary>
        /// 绘制圆圈节点
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        private void DrawCircleNode(Graphics graph, CStructFlowChartNode node)
        {
            Pen pen = new Pen(node.ColorNode);
            string text = node.StrExplain;
            int nGap = m_nCircleGap + 1;
            int nX = (node.PosLocation.X / 2 + 1) * m_nNodeWidth + node.PosLocation.X / 2 * m_nArrowWidth;
            int nY = node.PosLocation.Y * m_nNodeHeight;

            Size sf = graph.MeasureString(text, base.Font).ToSize();
            int nRow = (0 == sf.Width % m_nNodeWidth) ? sf.Width / m_nNodeWidth : sf.Width / m_nNodeWidth + 1;
            int nTextWidth = Math.Min(sf.Width, m_nNodeWidth);
            // 绘制文字说明坐标位置
            int nX3 = nX + m_nCircleGap - nTextWidth / 2;
            int nY3 = nY + m_nNodeHeight - 2 * nGap - nRow * sf.Height;
            nY3 = nY3 > nY ? nY3 : nY;
            // 绘制文字说明坐标位置
            RectangleF rectF = new RectangleF(nX3, nY3, m_nNodeWidth - nGap, m_nNodeHeight - nGap);
            // 绘制节点圆圈坐标位置
            Rectangle rect = new Rectangle(nX, nY + m_nNodeHeight - nGap, m_nCircleGap, m_nCircleGap);//nX + nWidth - nGap

            graph.DrawString(text, base.Font, new Pen(m_explainTextColor).Brush, rectF);
            graph.DrawEllipse(pen, rect);
            graph.FillEllipse(pen.Brush, rect);
            graph.DrawString(node.StrToolTip, base.Font, new Pen(m_toopLipTextColor).Brush, new Point(nX - nGap, nY + m_nNodeHeight));
        }

        /// <summary>
        /// 绘制直线箭头
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        private void DrawLineArrow(Graphics graph, CStructFlowChartNode node)
        {
            Pen pen = new Pen(node.ColorNode);
            string text = node.StrExplain;
            int nGap = m_nArrowGap;
            int nX = node.PosLocation.X / 2 * m_nNodeWidth + node.PosLocation.X / 2 * m_nArrowWidth;
            int nX1 = nX - (m_nArrowWidth - m_nCircleGap);
            int nX2 = nX + m_nNodeWidth;
            int nY = node.PosLocation.Y * m_nNodeHeight + m_nNodeHeight - nGap;

            Size sf = graph.MeasureString(text, base.Font).ToSize();
            // 绘制文字说明坐标位置
            int nX3 = nX1 + (nX2 - nX1 - sf.Width) / 2;
            int nY3 = node.PosLocation.Y * m_nNodeHeight + m_nNodeHeight - 2 * nGap - sf.Height;

            graph.DrawString(text, base.Font, new Pen(m_explainTextColor).Brush, new Point(nX3, nY3));
            // 绘制直线
            graph.DrawLine(pen, nX1, nY, nX2, nY);
            // 绘制上箭头
            graph.DrawLine(pen, nX2 - nGap, nY - nGap, nX2, nY);
            // 绘制下箭头
            graph.DrawLine(pen, nX2 - nGap, nY + nGap, nX2, nY);
        }

        /// <summary>
        /// 绘制下垂直直线箭头
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        private void DrawDownLineArrow(Graphics graph, CStructFlowChartNode node)
        {
            Pen pen = new Pen(node.ColorNode);
            string text = node.StrExplain;
            int nGap = m_nArrowGap;
            int nX = node.PosLocation.X / 2 * m_nNodeWidth + node.PosLocation.X / 2 * m_nArrowWidth;
            int nX1 = nX - (m_nArrowWidth - m_nCircleGap) - m_nCircleGap / 2;
            int nX2 = nX + m_nNodeWidth;
            int nY = node.PosLocation.Y * m_nNodeHeight + m_nNodeHeight - nGap;
            nX1 = Math.Max(0, nX1);

            Size sf = graph.MeasureString(text, base.Font).ToSize();
            // 绘制文字说明坐标位置
            int nX3 = nX1 + (nX2 - nX1 - sf.Width) / 2;
            int nY3 = node.PosLocation.Y * m_nNodeHeight + m_nNodeHeight - 2 * nGap - sf.Height;

            graph.DrawString(text, base.Font, new Pen(m_explainTextColor).Brush, new Point(nX3, nY3));
            // 绘制垂直线
            graph.DrawLine(pen, nX1, m_nNodeHeight, nX1, nY);
            // 绘制直线
            graph.DrawLine(pen, nX1, nY, nX2, nY);
            // 绘制上箭头
            graph.DrawLine(pen, nX2 - nGap, nY - nGap, nX2, nY);
            // 绘制下箭头
            graph.DrawLine(pen, nX2 - nGap, nY + nGap, nX2, nY);
        }
       
        /// <summary>
        /// 绘制上垂直直线箭头
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        private void DrawUpLineArrow(Graphics graph, CStructFlowChartNode node)
        {
            Pen pen = new Pen(node.ColorNode);
            string text = node.StrExplain;
            int nGap = m_nArrowGap;
            int nX = node.PosLocation.X / 2 * m_nNodeWidth + node.PosLocation.X / 2 * m_nArrowWidth;
            int nX1 = nX - (m_nArrowWidth - m_nCircleGap) - m_nCircleGap / 2;
            int nX2 = nX + m_nNodeWidth + m_nCircleGap / 2;
            int nY = node.PosLocation.Y * m_nNodeHeight + m_nNodeHeight - nGap;

            Size sf = graph.MeasureString(text, base.Font).ToSize();
            // 绘制文字说明坐标位置
            int nX3 = nX1 + (nX2 - nX1 - sf.Width) / 2;
            int nY3 = node.PosLocation.Y * m_nNodeHeight + m_nNodeHeight - 2 * nGap - sf.Height;

            graph.DrawString(text, base.Font, new Pen(m_explainTextColor).Brush, new Point(nX3, nY3));
            // 绘制直线
            graph.DrawLine(pen, nX1, nY, nX2, nY);
            // 绘制垂直线
            graph.DrawLine(pen, nX2, nY, nX2, m_nNodeHeight);
            // 绘制上箭头
            graph.DrawLine(pen, nX2 - nGap, m_nNodeHeight + nGap, nX2, m_nNodeHeight);
            // 绘制下箭头
            graph.DrawLine(pen, nX2 + nGap, m_nNodeHeight + nGap, nX2, m_nNodeHeight);
        }

        /// <summary>
        /// 绘制结束节点
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        private void DrawEnd(Graphics graph, CStructFlowChartNode node)
        {
            Pen pen = new Pen(node.ColorNode);
            string text = node.StrExplain;
            int nGap = m_nCircleGap + 1;
            int nX = (node.PosLocation.X / 2 + 1) * m_nNodeWidth + node.PosLocation.X / 2 * m_nArrowWidth;
            int nY = node.PosLocation.Y * m_nNodeHeight;

            Size sf = graph.MeasureString(text, base.Font).ToSize();
            int nRow = (0 == sf.Width % m_nNodeWidth) ? sf.Width / m_nNodeWidth : sf.Width / m_nNodeWidth + 1;
            int nTextWidth = Math.Min(sf.Width, m_nNodeWidth);
            // 绘制文字说明坐标位置
            int nX3 = nX + m_nCircleGap - nTextWidth / 2;
            int nY3 = nY + m_nNodeHeight - 2 * nGap - nRow * sf.Height;
            nY3 = nY3 > nY ? nY3 : nY;
            // 绘制文字说明坐标位置
            RectangleF rectF = new RectangleF(nX3, nY3, m_nNodeWidth - nGap, nRow * sf.Height);//m_nNodeHeight - nGap);
            // 结束节点绘制矩阵的位置
            Rectangle rect = new Rectangle(nX, nY + m_nNodeHeight - nGap, 2 * m_nCircleGap, m_nCircleGap);

            graph.DrawString(text, base.Font, new Pen(m_explainTextColor).Brush, rectF);
            graph.DrawEllipse(pen, rect);
            graph.FillEllipse(pen.Brush, rect);
            graph.DrawString(node.StrToolTip, base.Font, new Pen(Color.Blue).Brush, new Point(nX + 5 * m_nCircleGap, nY + m_nNodeHeight - nGap));
        }
      
        /// <summary>
        /// 绘制矩阵说明转下一流程图节点
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="node"></param>
        private void DrawRect(Graphics graph, CStructFlowChartNode node)
        {
            Pen pen = new Pen(node.ColorNode);
            string text = node.StrExplain;
            int nGap = m_nCircleGap + 1;
            int nX = (node.PosLocation.X / 2 + 1) * m_nNodeWidth + node.PosLocation.X / 2 * m_nArrowWidth;
            int nY = node.PosLocation.Y * m_nNodeHeight;

            Size sf = graph.MeasureString(text, base.Font).ToSize();
            int nRow = (0 == sf.Width % m_nNodeWidth) ? sf.Width / m_nNodeWidth : sf.Width / m_nNodeWidth + 1;
            int nTextWidth = Math.Min(sf.Width, m_nNodeWidth);
            // 绘制文字说明坐标位置
            int nX3 = nX + m_nCircleGap - nTextWidth / 2;
            int nY3 = nY + m_nNodeHeight - 2 * nGap - nRow * sf.Height;
            nY3 = nY3 > nY ? nY3 : nY;
            // 结束节点绘制矩阵的位置
            Rectangle rect = new Rectangle(nX, nY + m_nNodeHeight - nRow * sf.Height / 2, sf.Width, nRow * sf.Height);

            graph.DrawString(text, base.Font, new Pen(m_explainTextColor).Brush, rect);
            graph.DrawRectangle(pen, rect);
            graph.DrawString(node.StrToolTip, base.Font, new Pen(Color.Blue).Brush, new Point(rect.X + rect.Width + 5 * m_nCircleGap, nY + m_nNodeHeight - nGap));
        }
        #endregion

        #region 私有函数——获取各种流程类型对应的节点列表
        private void SetLstStruFlowChartNode()
        {
            m_nLayer = 0;
            switch (m_enmFlowTaskType)
            {
                case EnmFlowTaskType.NormEnter:
                    {
                        m_lstStruFlowChartNode = GetNormEnterFlowList(ref m_hashFlowChart);
                        break;
                    }
                case EnmFlowTaskType.NormExit:
                    {
                        m_lstStruFlowChartNode = GetNormExitFlowList(ref m_hashFlowChart);
                        break;
                    }
                case EnmFlowTaskType.QueueExit:
                    {
                        m_lstStruFlowChartNode = GetQueueExitFlowList(ref m_hashFlowChart);
                        break;
                    }
                case EnmFlowTaskType.TmpFetchEnter:
                    {
                        m_lstStruFlowChartNode = GetTmpFetchEnterFlowList(ref m_hashFlowChart);
                        break;
                    }
                case EnmFlowTaskType.TmpFetch:
                    {
                        m_lstStruFlowChartNode = GetTmpFetchFlowList(ref m_hashFlowChart);
                        break;
                    }
                case EnmFlowTaskType.MoveCar:
                    {
                        m_lstStruFlowChartNode = GetMoveCarFlowList(ref m_hashFlowChart);
                        break;
                    }
                case EnmFlowTaskType.MoveEquip:
                    {
                        m_lstStruFlowChartNode = GetMoveEquipFlowList(ref m_hashFlowChart);
                        break;
                    }
                case EnmFlowTaskType.TowerEnter:
                    {
                        m_lstStruFlowChartNode = GetTowerEnterFlowList(ref m_hashFlowChart);
                        break;
                    }
                case EnmFlowTaskType.TowerExitEnter:
                    {
                        m_lstStruFlowChartNode = GetTowerExitEnterFlowList(ref m_hashFlowChart);
                        break;
                    }
                case EnmFlowTaskType.TowerExit:
                    {
                        m_lstStruFlowChartNode = GetTowerExitFlowList(ref m_hashFlowChart);
                        break;
                    }
                default:
                    {
                        m_lstStruFlowChartNode.Clear();
                        break;
                    }
            }

            // InvokeRequired需要比较调用线程ID和创建线程ID
            // 如果它们不相同则返回true
            if (this.InvokeRequired)
            {
                RefreshCallback d = new RefreshCallback(SetRefresh);
                this.Invoke(d, null);
            }
            else
            {
                this.Refresh();
            }
        }

        /// <summary>
        /// 获取正常存车流程列表
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetNormEnterFlowList(ref Hashtable htFlow)
        {
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            // 卡A正常存车车
            lstFlowChartNode.Add(new CStructFlowChartNode(1, 1, new Point(1, 0), "有车入库", "(1001,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(1, 2, new Point(2, 0), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(1, 2, new Point(3, 0), "卡A第一次刷卡", "卡A:xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 3, new Point(4, 0), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 3, new Point(5, 0), "卡A第二次刷卡", "卡A:xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 4, new Point(6, 0), "(1,9)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 4, new Point(7, 0), "车厅对中完毕", "(1001,101)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 5, new Point(8, 0), "(1,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 5, new Point(9, 0), "车厅确定有车入库", "(1001,54)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(5, 6, new Point(10, 0), "(1,54)(13,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(5, 6, new Point(11, 0), "ETVxxx装载完成", "(1013,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(12, 0), "(13,51)(14,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(13, 0), "ETVxxx卸载完成", "(1014,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, -1, new Point(14, 0), "(14,51)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, -1, new Point(15, 0), "结束", "卡A正常存车", EnmNodeType.End, m_normColor));
            // 卡A车辆尺寸超过了固定车位的尺寸
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 9, new Point(8, 1), "(1,2)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 9, new Point(9, 1), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(10, 1), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(11, 1), "结束", "卡A车辆尺寸超过固定车位的尺寸", EnmNodeType.End, m_normColor));
            // 卡A尺寸超限
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 8, new Point(6, 2), "(1,9)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 8, new Point(7, 2), "车体超限", "(1001,104)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(8, 9, new Point(8, 2), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(8, 9, new Point(9, 2), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(10, 2), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(11, 2), "结束", "卡A尺寸超限", EnmNodeType.End, m_normColor));
            // 卡A车辆开到位后，未刷卡或刷一次卡后，车辆离开车厅
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 9, new Point(4, 3), "", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 9, new Point(5, 3), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(6, 3), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(7, 3), "结束", "卡A车辆开到位后，未刷卡或刷卡一次卡后，车辆离开车厅", EnmNodeType.End, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 1, 2, 3, 4, 5, 6, 7, -1 });
            htFlow.Add(1, new int[] { 1, 2, 3, 4, 9, -1 });
            htFlow.Add(2, new int[] { 1, 2, 3, 8, 9, -1 });
            htFlow.Add(3, new int[] { 1, 2, 9, -1 });

            return lstFlowChartNode;
        }

        /// <summary>
        /// 获取正常取车流程列表 
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetNormExitFlowList(ref Hashtable htFlow)
        {
            #region 注释
            /* 
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 10, new Point(1, 0), "卡A刷取车", "卡A：xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 11, new Point(2, 0), "(3,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 11, new Point(3, 0), "车厅确认出车", "(1003,54)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(11, 6, new Point(4, 0), "(3,54)(13,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(11, 6, new Point(5, 0), "卡A装载完成", "(1013,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(6, 0), "(13,51)(14,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(7, 0), "卡A卸载完成", "(1014,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 12, new Point(8, 0), "(14,51)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 12, new Point(9, 0), "卡A允许取车", "(1003,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 13, new Point(10, 0), "(3,2)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 13, new Point(11, 0), "车辆离开车厅", "(1003,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(13, -1, new Point(12, 0), "(3,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(13, -1, new Point(13, 0), "结束", "卡A正常取车", EnmNodeType.End, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 10, 11, 6, 7, 12, 13, -1 });

            return lstFlowChartNode;*/
            #endregion

            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 10, new Point(1, 0), "卡A卸载完成", "(1014,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 6, new Point(2, 0), "(13,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 6, new Point(3, 0), "ETVxxx装载完成", "(1013,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(4, 0), "(13,51)(14,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(5, 0), "卡B卸载完成 ", "(1014,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 12, new Point(6, 0), "(14,51)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 12, new Point(7, 0), "卡B允许取车", "(1003,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 13, new Point(8, 0), "(3,2)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 13, new Point(9, 0), "车辆离开车厅", "(1003,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(13, -1, new Point(10, 0), "(3,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(13, -1, new Point(11, 0), "结束", "卡B排队取车", EnmNodeType.End, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 11, new Point(2, 1), "(3,1)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 11, new Point(3, 1), "车厅确认出车 ", "(1003,54)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(11, 7, new Point(4, 1), "(3,54)", "", EnmNodeType.UpLineArrow, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 10, 6, 7, 12, 13, -1 });
            htFlow.Add(1, new int[] { 10, 11, 7 });

            return lstFlowChartNode;
        }

        /// <summary>
        /// 获取取车排队流程列表 
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetQueueExitFlowList(ref Hashtable htFlow)
        {
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 7, new Point(1, 0), "卡A卸载完成", "(1014,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 6, new Point(2, 0), "(13,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 6, new Point(3, 0), "ETVxxx装载完成", "(1013,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(4, 0), "(13,51)(14,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(5, 0), "卡B卸载完成 ", "(1014,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 12, new Point(6, 0), "(14,51)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 12, new Point(7, 0), "卡B允许取车", "(1003,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 13, new Point(8, 0), "(3,2)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 13, new Point(9, 0), "车辆离开车厅", "(1003,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(13, -1, new Point(10, 0), "(3,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(13, -1, new Point(11, 0), "结束", "卡B排队取车", EnmNodeType.End, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 11, new Point(2, 1), "(3,1)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 11, new Point(3, 1), "车厅确认出车 ", "(1003,54)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(11, 7, new Point(4, 1), "(3,54)", "", EnmNodeType.UpLineArrow, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 7, 6, 7, 12, 13, -1 });
            htFlow.Add(1, new int[] { 7, 11, 7 });

            return lstFlowChartNode;
        }

        /// <summary>
        /// 获取临时取物存车流程列表
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetTmpFetchEnterFlowList(ref Hashtable htFlow)
        {
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            // 卡A正常存车车
            lstFlowChartNode.Add(new CStructFlowChartNode(19, 19, new Point(1, 0), "卡A允许取物", "(1002,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(19, 2, new Point(2, 0), "(2,2)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(19, 2, new Point(3, 0), "卡A第一次刷卡", "卡A:xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 3, new Point(4, 0), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 3, new Point(5, 0), "卡A第二次刷卡", "卡A:xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 4, new Point(6, 0), "(1,9)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 4, new Point(7, 0), "车厅对中完毕", "(1001,101)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 5, new Point(8, 0), "(1,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 5, new Point(9, 0), "车厅确定有车入库", "(1001,54)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(5, 6, new Point(10, 0), "(1,54)(13,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(5, 6, new Point(11, 0), "ETVxxx装载完成", "(1013,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(12, 0), "(13,51)(14,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(13, 0), "ETVxxx卸载完成", "(1014,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, -1, new Point(14, 0), "(14,51)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, -1, new Point(15, 0), "结束", "卡A正常存车", EnmNodeType.End, m_normColor));
            // 卡A车辆尺寸超过了固定车位的尺寸
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 9, new Point(8, 1), "(1,2)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 9, new Point(9, 1), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(10, 1), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(11, 1), "结束", "卡A车辆尺寸超过固定车位的尺寸", EnmNodeType.End, m_normColor));
            // 卡A尺寸超限
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 8, new Point(6, 2), "(1,9)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 8, new Point(7, 2), "车体超限", "(1001,104)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(8, 9, new Point(8, 2), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(8, 9, new Point(9, 2), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(10, 2), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(11, 2), "结束", "卡A尺寸超限", EnmNodeType.End, m_normColor));
            // 卡A车辆开到位后，未刷卡或刷一次卡后，车辆离开车厅
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 9, new Point(4, 3), "", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 9, new Point(5, 3), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(6, 3), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(7, 3), "结束", "卡A车辆开到位后，未刷卡或刷卡一次卡后，车辆离开车厅", EnmNodeType.End, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 19, 2, 3, 4, 5, 6, 7, -1 });
            htFlow.Add(1, new int[] { 19, 2, 3, 4, 9, -1 });
            htFlow.Add(2, new int[] { 19, 2, 3, 8, 9, -1 });
            htFlow.Add(3, new int[] { 19, 2, 9, -1 });

            return lstFlowChartNode;
        }

        /// <summary>
        /// 获取临时取物流程列表 
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetTmpFetchFlowList(ref Hashtable htFlow)
        {
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            lstFlowChartNode.Add(new CStructFlowChartNode(17, 17, new Point(1, 0), "卡A临时取物", "卡A：xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(17, 18, new Point(2, 0), "(2,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(17, 18, new Point(3, 0), "车厅确认临时取物", "(1002,54)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(18, 6, new Point(4, 0), "(2,54)(13,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(18, 6, new Point(5, 0), "卡A装载完成", "(1013,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(6, 0), "(13,51)(14,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(7, 0), "卡A卸载完成", "(1014,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 19, new Point(8, 0), "(14,51)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, 19, new Point(9, 0), "卡A允许取物", "(1002,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(19, 20, new Point(10, 0), "(2,2)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(19, 20, new Point(11, 0), "车辆离开车厅", "(1002,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(20, -1, new Point(12, 0), "(2,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(20, -1, new Point(13, 0), "结束", "卡A临时取物后直接离开", EnmNodeType.End, m_normColor));

            lstFlowChartNode.Add(new CStructFlowChartNode(19, 2, new Point(10, 1), "(2,2)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(19, 2, new Point(11, 1), "切换到正常刷卡存车流程", "卡A临时取物后再存车", EnmNodeType.Rect, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 17, 18, 6, 7, 19, 20, -1 });
            htFlow.Add(1, new int[] { 19, 2 });

            return lstFlowChartNode;
        }
      
        /// <summary>
        /// 获取挪移流程列表 
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetMoveCarFlowList(ref Hashtable htFlow)
        {
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            lstFlowChartNode.Add(new CStructFlowChartNode(14, 14, new Point(1, 0), "确定挪移", "", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(14, 6, new Point(2, 0), "(13,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(14, 6, new Point(3, 0), "ETVxxx装载完成", "(1013,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(4, 0), "(13,51)(14,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(6, 7, new Point(5, 0), "ETVxxx卸载完成", "(1014,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, -1, new Point(6, 0), "(14,51)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(7, -1, new Point(7, 0), "结束", "挪移", EnmNodeType.End, m_normColor));
            
            htFlow.Clear();
            htFlow.Add(0, new int[] { 14, 6, 7, -1 });

            return lstFlowChartNode;
        }
       
        /// <summary>
        /// 获取移动流程列表 
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetMoveEquipFlowList(ref Hashtable htFlow)
        {
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            lstFlowChartNode.Add(new CStructFlowChartNode(15, 15, new Point(1, 0), "确定移动", "", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(15, 16, new Point(2, 0), "(11,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(15, 16, new Point(3, 0), "ETVxxx移动完成", "(1011,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(16, -1, new Point(4, 0), "(11,51)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(16, -1, new Point(5, 0), "结束", "移动", EnmNodeType.End, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 15, 16, -1 });

            return lstFlowChartNode;
        }

        /// <summary>
        /// 获取塔库存车流程列表
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetTowerEnterFlowList(ref Hashtable htFlow)
        {
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            // 卡A正常存车车
            lstFlowChartNode.Add(new CStructFlowChartNode(1, 1, new Point(1, 0), "有车入库", "(1001,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(1, 2, new Point(2, 0), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(1, 2, new Point(3, 0), "卡A第一次刷卡", "卡A:xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 3, new Point(4, 0), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 3, new Point(5, 0), "卡A第二次刷卡", "卡A:xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 4, new Point(6, 0), "(1,9)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 4, new Point(7, 0), "车厅对中完毕", "(1001,101)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 5, new Point(8, 0), "(1,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 5, new Point(9, 0), "车厅确定存车梳齿交换完成", "(1001,54)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(5, -1, new Point(10, 0), "(1,54)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(5, -1, new Point(11, 0), "结束", "卡A正常存车", EnmNodeType.End, m_normColor));
            // 卡A车辆尺寸超过了固定车位的尺寸
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 9, new Point(8, 1), "(1,2)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 9, new Point(9, 1), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(10, 1), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(11, 1), "结束", "卡A车辆尺寸超过固定车位的尺寸", EnmNodeType.End, m_normColor));
            // 卡A尺寸超限
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 8, new Point(6, 2), "(1,9)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 8, new Point(7, 2), "车体超限", "(1001,104)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(8, 9, new Point(8, 2), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(8, 9, new Point(9, 2), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(10, 2), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(11, 2), "结束", "卡A尺寸超限", EnmNodeType.End, m_normColor));
            // 卡A车辆开到位后，未刷卡或刷一次卡后，车辆离开车厅
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 9, new Point(4, 3), "", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 9, new Point(5, 3), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(6, 3), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(7, 3), "结束", "卡A车辆开到位后，未刷卡或刷卡一次卡后，车辆离开车厅", EnmNodeType.End, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 1, 2, 3, 4, 5, -1 });
            htFlow.Add(1, new int[] { 1, 2, 3, 4, 9, -1 });
            htFlow.Add(2, new int[] { 1, 2, 3, 8, 9, -1 });
            htFlow.Add(3, new int[] { 1, 2, 9, -1 });

            return lstFlowChartNode;
        }
       
        /// <summary>
        /// 获取塔库取车转存流程列表 
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetTowerExitFlowList(ref Hashtable htFlow)
        {
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 10, new Point(1, 0), "卡A刷取车", "", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 11, new Point(2, 0), "(3,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(10, 11, new Point(3, 0), "车厅确认出车", "(1003,54)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(11, 12, new Point(4, 0), "(3,54)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(11, 12, new Point(5, 0), "卡A允许取车", "(1003,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 13, new Point(6, 0), "(3,2)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 13, new Point(7, 0), "车辆离开车厅", "(1003,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(13, -1, new Point(8, 0), "(3,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(13, -1, new Point(9, 0), "结束", "塔库卡A正常取车", EnmNodeType.End, m_normColor));

            lstFlowChartNode.Add(new CStructFlowChartNode(12, 2, new Point(6, 1), "(3,2)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 2, new Point(6, 1), "切换到正常刷卡存车流程", "塔库卡A出库（转存）", EnmNodeType.Rect, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 10, 11, 12, 13, -1 });
            htFlow.Add(1, new int[] { 12, 2 });

            return lstFlowChartNode;
        }

        /// <summary>
        /// 获取塔库转存车流程列表
        /// </summary>
        /// <returns></returns>
        private List<CStructFlowChartNode> GetTowerExitEnterFlowList(ref Hashtable htFlow)
        {
            List<CStructFlowChartNode> lstFlowChartNode = new List<CStructFlowChartNode>();
            // 卡A正常存车车
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 12, new Point(1, 0), "卡A允许取车", "(1003,1)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 2, new Point(2, 0), "(3,2)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(12, 2, new Point(3, 0), "卡A第一次刷卡", "卡A:xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 3, new Point(4, 0), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 3, new Point(5, 0), "卡A第二次刷卡", "卡A:xxx", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 4, new Point(6, 0), "(1,9)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 4, new Point(7, 0), "车厅对中完毕", "(1001,101)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 5, new Point(8, 0), "(1,1)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 5, new Point(9, 0), "车厅确定存车梳齿交换完成", "(1001,54)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(5, -1, new Point(10, 0), "(1,54)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(5, -1, new Point(11, 0), "结束", "卡A正常存车", EnmNodeType.End, m_normColor));
            // 卡A车辆尺寸超过了固定车位的尺寸
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 9, new Point(8, 1), "(1,2)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(4, 9, new Point(9, 1), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(10, 1), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(11, 1), "结束", "卡A车辆尺寸超过固定车位的尺寸", EnmNodeType.End, m_normColor));
            // 卡A尺寸超限
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 8, new Point(6, 2), "(1,9)", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(3, 8, new Point(7, 2), "车体超限", "(1001,104)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(8, 9, new Point(8, 2), "", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(8, 9, new Point(9, 2), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(10, 2), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(11, 2), "结束", "卡A尺寸超限", EnmNodeType.End, m_normColor));
            // 卡A车辆开到位后，未刷卡或刷一次卡后，车辆离开车厅
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 9, new Point(4, 3), "", "", EnmNodeType.DownLineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(2, 9, new Point(5, 3), "车辆离开车厅", "(1001,4)", EnmNodeType.Circle, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(6, 3), "(1,55)", "", EnmNodeType.LineArrow, m_normColor));
            lstFlowChartNode.Add(new CStructFlowChartNode(9, -1, new Point(7, 3), "结束", "卡A车辆开到位后，未刷卡或刷卡一次卡后，车辆离开车厅", EnmNodeType.End, m_normColor));

            htFlow.Clear();
            htFlow.Add(0, new int[] { 12, 2, 3, 4, 5, -1 });
            htFlow.Add(1, new int[] { 12, 2, 3, 4, 9, -1 });
            htFlow.Add(2, new int[] { 12, 2, 3, 8, 9, -1 });
            htFlow.Add(3, new int[] { 12, 2, 9, -1 });

            return lstFlowChartNode;
        }
        #endregion

        #region 私有函数——逻辑处理
        /// <summary>
        ///  当前节点是否为交接点
        /// </summary>
        /// <param name="nCurrentNode"></param>
        /// <returns></returns>
        private bool IsJoint(CStructFlowChartNode node)
        {
            if (null == node)
            {
                return true;
            }

            if (null != m_lstStruFlowChartNode.Find(s => s.NPrevNode == node.NCurrentNode && s.PosLocation.X > node.PosLocation.X 
                                                          && s.EnumNodeType == EnmNodeType.DownLineArrow))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 当前节点是否为箭头
        /// </summary>
        /// <param name="enmNodeType"></param>
        /// <returns></returns>
        private bool IsArrow(EnmNodeType enmNodeType)
        {
            return (enmNodeType == EnmNodeType.LineArrow || enmNodeType == EnmNodeType.DownLineArrow || enmNodeType == EnmNodeType.UpLineArrow);
        }

        /// <summary>
        /// 当前节点是否为节点（圆圈和结束）
        /// </summary>
        /// <param name="enmNodeType"></param>
        /// <returns></returns>
        private bool IsNode(EnmNodeType enmNodeType)
        {
            return (enmNodeType == EnmNodeType.Circle || enmNodeType == EnmNodeType.End || enmNodeType == EnmNodeType.Rect);
        }

        /// <summary>
        /// 根据前一节点和当前节点获取是否是正确的运行流程
        /// </summary>
        /// <param name="nPrevNode"></param>
        /// <param name="nCurrentNode"></param>
        /// <returns></returns>
        private bool IsRightRunFlow(int nPrevNode, int nCurrentNode)
        {
            CStructFlowChartNode node = m_lstStruFlowChartNode.Find(s => s.NPrevNode == nPrevNode && s.NCurrentNode == nCurrentNode);

            if (null == node)
            {
                //m_nLayer = 0;
            }
            else if (EnmNodeType.DownLineArrow == node.EnumNodeType)
            {
                m_nLayer = node.PosLocation.Y;
            }

            if (nPrevNode == nCurrentNode)
            {
                return true;
            }

            bool bFlag = false;
            if (!m_hashFlowChart.ContainsKey(m_nLayer) || typeof(int[]) != m_hashFlowChart[m_nLayer].GetType())
            {// 流程工作类型的hash表对应的层列表
                return false;
            }
            int[] ints = (int[])m_hashFlowChart[m_nLayer];

            for (int i = 0; i < ints.Count() - 1; i++)
            {
                if (nPrevNode == ints[i] && nCurrentNode == ints[i+1])
                {
                    return true;
                }
            }
            int nPrevIndex = ints.ToList().IndexOf(nPrevNode);
            int nCurrentIndex = ints.ToList().LastIndexOf(nCurrentNode);
            
            if (-1 != nPrevIndex && nPrevIndex + 1 == nCurrentIndex)
            {
                return true;
            }
            else if (nPrevIndex >= nCurrentIndex)
            {// 前一节点大于当前节点=>后退流程

            }
            else if (-1 == nPrevIndex && 0 < nCurrentIndex && nCurrentIndex < ints.Count())
            {// 前一节点无，则当前节点故障
                CStructFlowChartNode nodeError = m_lstStruFlowChartNode.Find(s => s.PosLocation.Y == m_nLayer && s.NPrevNode == ints[nCurrentIndex - 1] && s.NCurrentNode == nCurrentNode && IsNode(s.EnumNodeType));

                if (null != nodeError)
                {// 当前节点故障
                    nodeError.ColorNode = m_errorColor;
                    // InvokeRequired需要比较调用线程ID和创建线程ID
                    // 如果它们不相同则返回true
                    if (this.InvokeRequired)
                    {
                        RefreshCallback d = new RefreshCallback(SetRefresh);
                        this.Invoke(d, null);
                    }
                    else
                    {
                        this.Refresh();
                    }
                }
            }
            else
            {// 跳跃若干节点情况=>流程异常处理
                for (int i = nPrevIndex; i <= nCurrentIndex; i++)
                {
                    Color color = m_errorColor;
                    if (1 > i || i >= ints.Count())
                    {
                        if (0 == i && (-1 == nPrevIndex || i == nPrevIndex || i == nCurrentIndex))
                        {
                            if (i == nPrevIndex)
                            {// 当前节点运行操作
                                color = m_endColor;
                            }

                            CStructFlowChartNode nodePrevError = m_lstStruFlowChartNode.Find(s => s.PosLocation.Y == m_nLayer && s.NCurrentNode == ints[i] && IsNode(s.EnumNodeType));
                            if (null != nodePrevError)
                            {// 当前节点故障
                                nodePrevError.ColorNode = color;
                            }
                            continue;
                        }

                        continue;
                    }

                    CStructFlowChartNode nodeError = m_lstStruFlowChartNode.Find(s => s.PosLocation.Y == m_nLayer && s.NPrevNode == ints[i - 1] && s.NCurrentNode == ints[i] && IsNode(s.EnumNodeType));

                    if (i == nPrevIndex || i == nCurrentIndex)
                    {// 前一节点和当前节点运行操作
                        color = m_endColor;
                    }
                    
                    if (null != nodeError)
                    {// 中间漏掉的节点故障
                        nodeError.ColorNode = color;
                    }
                }
                // InvokeRequired需要比较调用线程ID和创建线程ID
                // 如果它们不相同则返回true
                if (this.InvokeRequired)
                {
                    RefreshCallback d = new RefreshCallback(SetRefresh);
                    this.Invoke(d, null);
                }
                else
                {
                    this.Refresh();
                }
            }
            return bFlag;
        }
        #endregion

        #region 线程间代理
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nx"></param>
        /// <param name="nY"></param>
        private void SetRefresh()
        {
            this.Refresh();
        }
        #endregion
    }
}
