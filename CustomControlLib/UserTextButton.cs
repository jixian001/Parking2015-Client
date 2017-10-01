using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using CarLocationPanelLib;

namespace CustomControlLib
{
    /// <summary>
    /// TextBox文本框输入类型
    /// </summary>
    public enum EnmTxtBoxType
    {
        /// <summary>
        /// 初始: 0
        /// </summary>
        Init = 0,
        /// <summary>
        /// 用户名（账号）：1
        /// </summary>
        Name,
        /// <summary>
        /// 用户密码：2
        /// </summary>
        PassWord,
        /// <summary>
        /// 用户卡号：3
        /// </summary>
        ICCard,
        /// <summary>
        /// 车位地址：4
        /// </summary>
        CarLocationAddr,
        /// <summary>
        /// 电话号码：5
        /// </summary>
        Mobile,
    }

    public class CUserTextButton : System.Windows.Forms.TextBox
    {
        public event EventHandler CallbackTextButtonEvent;
        private Button m_btn = null;
        private ToolTip m_toolTip = new ToolTip();
        private Image m_imageButton = null;
        private EnmTxtBoxType m_enmtxtType = EnmTxtBoxType.Init;
        private string m_strToolTip;
        private string m_strRegularExpression;
        private string m_strErrorText;
        private bool m_isButton = false;
        //bool m_isStart = true;

        /// <summary>
        /// 构造函数-设置控件输入格式限制提示文本
        /// </summary>
        public CUserTextButton()
            : base()
        {
            //this.LostFocus += new EventHandler(CCustomTextBox_LostFocus);
            //this.GotFocus += new EventHandler(CCustomTextBox_GotFocus);
            this.Validated += new EventHandler(CCustomTextBox_Validating);
            //this.TextChanged += new EventHandler(CCustomTextBox_Validating);

            m_btn = new Button();
            m_btn.BackgroundImageLayout = ImageLayout.Stretch;
            m_btn.FlatStyle = FlatStyle.Flat;
            //m_btn.BackColor = Color.WhiteSmoke;
            m_btn.Height = this.Height;
            m_btn.Width = 0;
            m_btn.Dock = DockStyle.Right;
            m_btn.Parent = this;
            m_btn.Click += new EventHandler(Button_Click);
        }


        #region 公有函数
        /// <summary>
        /// 验收文本是否符合要求
        /// </summary>
        /// <returns>true:符合   false:不符合</returns>
        public bool IsValidatedSuccess()
        {
            try
            {
                if (string.IsNullOrEmpty(m_strRegularExpression) || m_strToolTip == this.Text)
                {
                    return false;
                }

                Regex regex = new Regex(m_strRegularExpression);

                if (!regex.IsMatch(this.Text))
                {
                    MessageBox.Show("请输入正确的文本", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }

                return false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 内部按钮控件是否可用
        /// </summary>
        public bool EnabledButton
        {
            get
            {
                return m_isButton;
            }
            set
            {
                m_isButton = value;

                if (m_isButton)
                {
                    m_btn.Width = this.Height;
                }
                else
                {
                    m_btn.Width = 0;
                }
            }
        }

        /// <summary>
        /// 按钮图片
        /// </summary>
        public Image ImageButton
        {
            get
            {
                return m_imageButton;
            }
            set
            {
                m_imageButton = value;
                m_btn.BackgroundImage = m_imageButton;
            }
        }

        /// <summary>
        /// 文本框输入类型
        /// </summary>
        public EnmTxtBoxType EnmTxtType
        {
            get
            {
                return m_enmtxtType;
            }
            set
            {
                m_enmtxtType = value;
                SetTextBoxParams();
            }
        }

        /*
        /// <summary>
        /// 输入格式限制提示文本
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
                m_toolTip.SetToolTip(this, m_strToolTip);
                CCustomTextBox_LostFocus(this, null);
            }
        }

        /// <summary>
        /// 输入格式限制正则表达式
        /// </summary>
        public string StrRegularExpression
        {
            get
            {
                return m_strRegularExpression;
            }
            set
            {
                m_strRegularExpression = value;
            }
        }*/
        #endregion

        #region 事件处理
        /// <summary>
        /// 控件失去焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CCustomTextBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (EnmTxtBoxType.PassWord == m_enmtxtType)
                {
                    return;
                }
                else if (string.IsNullOrEmpty(this.Text) || m_strToolTip == this.Text)
                {
                    this.Text = m_strToolTip;
                    this.ForeColor = System.Drawing.Color.Gray;
                }
                //else if (m_isStart)
                //{
                //    m_isStart = false;
                //    this.Text = this.Text.Substring(0, 1);
                //    this.ForeColor = System.Drawing.Color.Black;
                //}
                else
                {
                    this.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 控件接收焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CCustomTextBox_GotFocus(object sender, EventArgs e)
        {
            try
            {
                this.ForeColor = System.Drawing.Color.Black;
                if (m_strToolTip == this.Text)
                {
                    this.Text = "";
                }

                //if (m_strToolTip == this.Text)
                //{// 将光标定位到文本框的起点
                //    m_isStart = true;
                //    this.SelectionStart = 0;
                //    this.ForeColor = System.Drawing.Color.Gray;
                //}
                //else if (m_isStart)
                //{
                //    m_isStart = false;
                //    this.Text = this.Text.Substring(0, 1);
                //    this.ForeColor = System.Drawing.Color.Black;
                //}
                //else
                //{
                //    this.ForeColor = System.Drawing.Color.Black;
                //}
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CCustomTextBox_Validating(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(m_strRegularExpression) || m_strToolTip == this.Text 
                    || string.IsNullOrEmpty(this.Text))
                {
                    return;
                }

                Regex regex = new Regex(m_strRegularExpression);

                if (!regex.IsMatch(this.Text))
                {
                    MessageBox.Show(m_strErrorText, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 文本框上的按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (null != CallbackTextButtonEvent)
                {
                    CallbackTextButtonEvent(this, e);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(CStaticClass.GetExceptionInfo(exception), "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 私有函数
        private void SetTextBoxParams()
        {
            // 读取配置文件（根据TextBox文本框输入类型EnmTxtBoxType获取）
            switch (m_enmtxtType)
            {
                case EnmTxtBoxType.Name:
                    {
                        m_strToolTip = "";// "不含有 ^%&',;=?$\" 等字符";
                        m_strRegularExpression = "";// @"[%&',;=?\x22]";
                        m_strErrorText = "请输入正确的文本";
                        break;
                    }
                case EnmTxtBoxType.PassWord:
                    {
                        m_strToolTip = "以字母开头，长度在1-18之间，只能包含字符、数字和下划线";
                        m_strRegularExpression = @"([a-zA-Z]|[0-9]|[_]){1,18}$";
                        m_strErrorText = "请输入正确的密码";
                        break;
                    }
                case EnmTxtBoxType.ICCard:
                    {
                        m_strToolTip = "非零开头的数字";
                        m_strRegularExpression = "^([0-9]*)$";
                        m_strErrorText = "请输入正确的非零开头的数字";
                        break;
                    }
                case EnmTxtBoxType.CarLocationAddr:
                    {
                        m_strToolTip = "3或者5位数字。如：20105表示2边1列5层或者尺寸111";
                        m_strRegularExpression = @"^((\d{3})|(\d{5}))$";
                        m_strErrorText = "请输入正确的车位地址或者车位尺寸";
                        break;
                    }
                case EnmTxtBoxType.Mobile:
                    {
                        m_strToolTip = "有数字组成。如：0511-4405222或者13456789023";
                        m_strRegularExpression = @"^((\d{3}[-]\d{8})|(\d{4}[-]\d{7,8})|(\d{11})|(\d{4}))$";
                        m_strErrorText = "请输入正确的电话号码";
                        break;
                    }
            }

            m_toolTip.SetToolTip(this, m_strToolTip);
            this.Text = "";
            //CCustomTextBox_LostFocus(this, null);
        }
        #endregion
    }
}
