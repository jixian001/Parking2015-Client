using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormLib
{
    public partial class FrmPassword : Form
    {
        public FrmPassword()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (TxtName.Text.Trim() == "") 
            {
                TxtName.Focus();
                return;
            }
            if (TxtPassWord.Text.Trim() == "") 
            {
                TxtPassWord.Focus();
                return;
            }
            if (TxtName.Text.Trim() != "admin") 
            {
                TxtName.Focus();
                MessageBox.Show("请输入正确的帐号！");
                return;
            }
            if (TxtPassWord.Text.Trim() != "6096") 
            {
                TxtPassWord.Focus();
                MessageBox.Show("请输入正确的密码！");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}
