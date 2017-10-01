using CustomControlLib;
namespace WindowsFormLib
{
    partial class CFormCustomerManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CFormCustomerManage));
            this.TctlCustomer = new System.Windows.Forms.TabControl();
            this.TpICCard = new System.Windows.Forms.TabPage();
            this.GbxICCardInfo = new System.Windows.Forms.GroupBox();
            this.BtnICCardLogout = new System.Windows.Forms.Button();
            this.BtnICCardCancelLoss = new System.Windows.Forms.Button();
            this.BtnICCardLoss = new System.Windows.Forms.Button();
            this.TxtICCardLogoutTime = new System.Windows.Forms.TextBox();
            this.TxtWareHouse = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtICCardNewTime = new System.Windows.Forms.TextBox();
            this.TxtICCardLossTime = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.TxtICCardStatus = new System.Windows.Forms.TextBox();
            this.TxtCarLocAddr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtICCardID = new System.Windows.Forms.TextBox();
            this.TxtICCardType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.GbxICCardFind = new System.Windows.Forms.GroupBox();
            this.CTxtICCardIDOld = new CustomControlLib.CUserTextButton();
            this.LblICCardIDOld = new System.Windows.Forms.Label();
            this.BtnICCardChange = new System.Windows.Forms.Button();
            this.BtnICCardClose = new System.Windows.Forms.Button();
            this.TxtICCardPhy = new System.Windows.Forms.TextBox();
            this.BtnICCardFind = new System.Windows.Forms.Button();
            this.BtnICCardCreate = new System.Windows.Forms.Button();
            this.BtnICCardRead = new System.Windows.Forms.Button();
            this.CTxtICCardIDFind = new CustomControlLib.CUserTextButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TpCustomer = new System.Windows.Forms.TabPage();
            this.CucipCustomer = new CustomControlLib.CUserCustomerInfoPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.TctlCustomer.SuspendLayout();
            this.TpICCard.SuspendLayout();
            this.GbxICCardInfo.SuspendLayout();
            this.GbxICCardFind.SuspendLayout();
            this.TpCustomer.SuspendLayout();
            this.SuspendLayout();
            // 
            // TctlCustomer
            // 
            this.TctlCustomer.Controls.Add(this.TpICCard);
            this.TctlCustomer.Controls.Add(this.TpCustomer);
            this.TctlCustomer.Location = new System.Drawing.Point(0, 0);
            this.TctlCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.TctlCustomer.Name = "TctlCustomer";
            this.TctlCustomer.SelectedIndex = 0;
            this.TctlCustomer.Size = new System.Drawing.Size(849, 517);
            this.TctlCustomer.TabIndex = 0;
            // 
            // TpICCard
            // 
            this.TpICCard.BackColor = System.Drawing.SystemColors.Control;
            this.TpICCard.Controls.Add(this.GbxICCardInfo);
            this.TpICCard.Controls.Add(this.GbxICCardFind);
            this.TpICCard.Location = new System.Drawing.Point(4, 26);
            this.TpICCard.Margin = new System.Windows.Forms.Padding(4);
            this.TpICCard.Name = "TpICCard";
            this.TpICCard.Padding = new System.Windows.Forms.Padding(4);
            this.TpICCard.Size = new System.Drawing.Size(841, 487);
            this.TpICCard.TabIndex = 1;
            this.TpICCard.Text = "IC卡";
            // 
            // GbxICCardInfo
            // 
            this.GbxICCardInfo.Controls.Add(this.BtnICCardLogout);
            this.GbxICCardInfo.Controls.Add(this.BtnICCardCancelLoss);
            this.GbxICCardInfo.Controls.Add(this.BtnICCardLoss);
            this.GbxICCardInfo.Controls.Add(this.TxtICCardLogoutTime);
            this.GbxICCardInfo.Controls.Add(this.TxtWareHouse);
            this.GbxICCardInfo.Controls.Add(this.label9);
            this.GbxICCardInfo.Controls.Add(this.label10);
            this.GbxICCardInfo.Controls.Add(this.TxtICCardNewTime);
            this.GbxICCardInfo.Controls.Add(this.TxtICCardLossTime);
            this.GbxICCardInfo.Controls.Add(this.label11);
            this.GbxICCardInfo.Controls.Add(this.label12);
            this.GbxICCardInfo.Controls.Add(this.TxtICCardStatus);
            this.GbxICCardInfo.Controls.Add(this.TxtCarLocAddr);
            this.GbxICCardInfo.Controls.Add(this.label7);
            this.GbxICCardInfo.Controls.Add(this.label8);
            this.GbxICCardInfo.Controls.Add(this.TxtICCardID);
            this.GbxICCardInfo.Controls.Add(this.TxtICCardType);
            this.GbxICCardInfo.Controls.Add(this.label5);
            this.GbxICCardInfo.Controls.Add(this.label6);
            this.GbxICCardInfo.Location = new System.Drawing.Point(0, 230);
            this.GbxICCardInfo.Margin = new System.Windows.Forms.Padding(4);
            this.GbxICCardInfo.Name = "GbxICCardInfo";
            this.GbxICCardInfo.Padding = new System.Windows.Forms.Padding(4);
            this.GbxICCardInfo.Size = new System.Drawing.Size(829, 256);
            this.GbxICCardInfo.TabIndex = 2;
            this.GbxICCardInfo.TabStop = false;
            this.GbxICCardInfo.Text = "IC卡信息";
            // 
            // BtnICCardLogout
            // 
            this.BtnICCardLogout.Location = new System.Drawing.Point(565, 211);
            this.BtnICCardLogout.Margin = new System.Windows.Forms.Padding(4);
            this.BtnICCardLogout.Name = "BtnICCardLogout";
            this.BtnICCardLogout.Size = new System.Drawing.Size(107, 35);
            this.BtnICCardLogout.TabIndex = 22;
            this.BtnICCardLogout.Text = "注销";
            this.BtnICCardLogout.UseVisualStyleBackColor = true;
            this.BtnICCardLogout.Click += new System.EventHandler(this.BtnICCardLogout_Click);
            // 
            // BtnICCardCancelLoss
            // 
            this.BtnICCardCancelLoss.Location = new System.Drawing.Point(353, 211);
            this.BtnICCardCancelLoss.Margin = new System.Windows.Forms.Padding(4);
            this.BtnICCardCancelLoss.Name = "BtnICCardCancelLoss";
            this.BtnICCardCancelLoss.Size = new System.Drawing.Size(107, 35);
            this.BtnICCardCancelLoss.TabIndex = 21;
            this.BtnICCardCancelLoss.Text = "取消挂失";
            this.BtnICCardCancelLoss.UseVisualStyleBackColor = true;
            this.BtnICCardCancelLoss.Click += new System.EventHandler(this.BtnICCardCancelLoss_Click);
            // 
            // BtnICCardLoss
            // 
            this.BtnICCardLoss.Location = new System.Drawing.Point(139, 211);
            this.BtnICCardLoss.Margin = new System.Windows.Forms.Padding(4);
            this.BtnICCardLoss.Name = "BtnICCardLoss";
            this.BtnICCardLoss.Size = new System.Drawing.Size(107, 35);
            this.BtnICCardLoss.TabIndex = 20;
            this.BtnICCardLoss.TabStop = false;
            this.BtnICCardLoss.Text = "挂失";
            this.BtnICCardLoss.UseVisualStyleBackColor = true;
            this.BtnICCardLoss.Click += new System.EventHandler(this.BtnICCardLoss_Click);
            // 
            // TxtICCardLogoutTime
            // 
            this.TxtICCardLogoutTime.Enabled = false;
            this.TxtICCardLogoutTime.Location = new System.Drawing.Point(511, 115);
            this.TxtICCardLogoutTime.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardLogoutTime.Name = "TxtICCardLogoutTime";
            this.TxtICCardLogoutTime.Size = new System.Drawing.Size(311, 26);
            this.TxtICCardLogoutTime.TabIndex = 19;
            // 
            // TxtWareHouse
            // 
            this.TxtWareHouse.Enabled = false;
            this.TxtWareHouse.Location = new System.Drawing.Point(511, 155);
            this.TxtWareHouse.Margin = new System.Windows.Forms.Padding(4);
            this.TxtWareHouse.Name = "TxtWareHouse";
            this.TxtWareHouse.Size = new System.Drawing.Size(311, 26);
            this.TxtWareHouse.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(412, 155);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label9.Size = new System.Drawing.Size(99, 27);
            this.label9.TabIndex = 17;
            this.label9.Text = "库区";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(412, 115);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label10.Size = new System.Drawing.Size(99, 27);
            this.label10.TabIndex = 16;
            this.label10.Text = "注销时间";
            // 
            // TxtICCardNewTime
            // 
            this.TxtICCardNewTime.Enabled = false;
            this.TxtICCardNewTime.Location = new System.Drawing.Point(511, 39);
            this.TxtICCardNewTime.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardNewTime.Name = "TxtICCardNewTime";
            this.TxtICCardNewTime.Size = new System.Drawing.Size(311, 26);
            this.TxtICCardNewTime.TabIndex = 15;
            // 
            // TxtICCardLossTime
            // 
            this.TxtICCardLossTime.Enabled = false;
            this.TxtICCardLossTime.Location = new System.Drawing.Point(511, 75);
            this.TxtICCardLossTime.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardLossTime.Name = "TxtICCardLossTime";
            this.TxtICCardLossTime.Size = new System.Drawing.Size(311, 26);
            this.TxtICCardLossTime.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(412, 75);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label11.Size = new System.Drawing.Size(99, 27);
            this.label11.TabIndex = 13;
            this.label11.Text = "挂失时间";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(412, 39);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label12.Size = new System.Drawing.Size(99, 27);
            this.label12.TabIndex = 12;
            this.label12.Text = "制卡时间";
            // 
            // TxtICCardStatus
            // 
            this.TxtICCardStatus.Enabled = false;
            this.TxtICCardStatus.Location = new System.Drawing.Point(101, 115);
            this.TxtICCardStatus.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardStatus.Name = "TxtICCardStatus";
            this.TxtICCardStatus.Size = new System.Drawing.Size(311, 26);
            this.TxtICCardStatus.TabIndex = 11;
            // 
            // TxtCarLocAddr
            // 
            this.TxtCarLocAddr.Enabled = false;
            this.TxtCarLocAddr.Location = new System.Drawing.Point(101, 155);
            this.TxtCarLocAddr.Margin = new System.Windows.Forms.Padding(4);
            this.TxtCarLocAddr.Name = "TxtCarLocAddr";
            this.TxtCarLocAddr.Size = new System.Drawing.Size(311, 26);
            this.TxtCarLocAddr.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(2, 155);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label7.Size = new System.Drawing.Size(99, 27);
            this.label7.TabIndex = 9;
            this.label7.Text = "卡对应车位";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(2, 115);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label8.Size = new System.Drawing.Size(99, 27);
            this.label8.TabIndex = 8;
            this.label8.Text = "卡状态";
            // 
            // TxtICCardID
            // 
            this.TxtICCardID.Enabled = false;
            this.TxtICCardID.Location = new System.Drawing.Point(101, 39);
            this.TxtICCardID.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardID.Name = "TxtICCardID";
            this.TxtICCardID.Size = new System.Drawing.Size(311, 26);
            this.TxtICCardID.TabIndex = 7;
            // 
            // TxtICCardType
            // 
            this.TxtICCardType.Enabled = false;
            this.TxtICCardType.Location = new System.Drawing.Point(101, 75);
            this.TxtICCardType.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardType.Name = "TxtICCardType";
            this.TxtICCardType.Size = new System.Drawing.Size(311, 26);
            this.TxtICCardType.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(2, 75);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(99, 27);
            this.label5.TabIndex = 1;
            this.label5.Text = "卡类型";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(2, 39);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label6.Size = new System.Drawing.Size(99, 27);
            this.label6.TabIndex = 0;
            this.label6.Text = "用户卡号";
            // 
            // GbxICCardFind
            // 
            this.GbxICCardFind.Controls.Add(this.label1);
            this.GbxICCardFind.Controls.Add(this.CTxtICCardIDOld);
            this.GbxICCardFind.Controls.Add(this.LblICCardIDOld);
            this.GbxICCardFind.Controls.Add(this.BtnICCardChange);
            this.GbxICCardFind.Controls.Add(this.BtnICCardClose);
            this.GbxICCardFind.Controls.Add(this.TxtICCardPhy);
            this.GbxICCardFind.Controls.Add(this.BtnICCardFind);
            this.GbxICCardFind.Controls.Add(this.BtnICCardCreate);
            this.GbxICCardFind.Controls.Add(this.BtnICCardRead);
            this.GbxICCardFind.Controls.Add(this.CTxtICCardIDFind);
            this.GbxICCardFind.Controls.Add(this.label3);
            this.GbxICCardFind.Controls.Add(this.label4);
            this.GbxICCardFind.Location = new System.Drawing.Point(0, 11);
            this.GbxICCardFind.Margin = new System.Windows.Forms.Padding(4);
            this.GbxICCardFind.Name = "GbxICCardFind";
            this.GbxICCardFind.Padding = new System.Windows.Forms.Padding(4);
            this.GbxICCardFind.Size = new System.Drawing.Size(829, 211);
            this.GbxICCardFind.TabIndex = 1;
            this.GbxICCardFind.TabStop = false;
            this.GbxICCardFind.Text = "查询";
            // 
            // CTxtICCardIDOld
            // 
            this.CTxtICCardIDOld.EnabledButton = false;
            this.CTxtICCardIDOld.EnmTxtType = CustomControlLib.EnmTxtBoxType.ICCard;
            this.CTxtICCardIDOld.ImageButton = null;
            this.CTxtICCardIDOld.Location = new System.Drawing.Point(101, 115);
            this.CTxtICCardIDOld.Margin = new System.Windows.Forms.Padding(4);
            this.CTxtICCardIDOld.Name = "CTxtICCardIDOld";
            this.CTxtICCardIDOld.Size = new System.Drawing.Size(372, 26);
            this.CTxtICCardIDOld.TabIndex = 11;
            // 
            // LblICCardIDOld
            // 
            this.LblICCardIDOld.Location = new System.Drawing.Point(2, 117);
            this.LblICCardIDOld.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblICCardIDOld.Name = "LblICCardIDOld";
            this.LblICCardIDOld.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LblICCardIDOld.Size = new System.Drawing.Size(99, 27);
            this.LblICCardIDOld.TabIndex = 10;
            this.LblICCardIDOld.Text = "换卡旧卡号";
            // 
            // BtnICCardChange
            // 
            this.BtnICCardChange.Location = new System.Drawing.Point(408, 154);
            this.BtnICCardChange.Margin = new System.Windows.Forms.Padding(4);
            this.BtnICCardChange.Name = "BtnICCardChange";
            this.BtnICCardChange.Size = new System.Drawing.Size(107, 35);
            this.BtnICCardChange.TabIndex = 9;
            this.BtnICCardChange.Text = "换卡";
            this.BtnICCardChange.UseVisualStyleBackColor = true;
            this.BtnICCardChange.Click += new System.EventHandler(this.BtnICCardChange_Click);
            // 
            // BtnICCardClose
            // 
            this.BtnICCardClose.Location = new System.Drawing.Point(664, 154);
            this.BtnICCardClose.Margin = new System.Windows.Forms.Padding(4);
            this.BtnICCardClose.Name = "BtnICCardClose";
            this.BtnICCardClose.Size = new System.Drawing.Size(107, 35);
            this.BtnICCardClose.TabIndex = 8;
            this.BtnICCardClose.Text = "关闭";
            this.BtnICCardClose.UseVisualStyleBackColor = true;
            this.BtnICCardClose.Click += new System.EventHandler(this.BtnICCardClose_Click);
            // 
            // TxtICCardPhy
            // 
            this.TxtICCardPhy.BackColor = System.Drawing.SystemColors.Window;
            this.TxtICCardPhy.Location = new System.Drawing.Point(101, 35);
            this.TxtICCardPhy.Margin = new System.Windows.Forms.Padding(4);
            this.TxtICCardPhy.Name = "TxtICCardPhy";
            this.TxtICCardPhy.ReadOnly = true;
            this.TxtICCardPhy.Size = new System.Drawing.Size(372, 26);
            this.TxtICCardPhy.TabIndex = 7;
            // 
            // BtnICCardFind
            // 
            this.BtnICCardFind.Location = new System.Drawing.Point(536, 154);
            this.BtnICCardFind.Margin = new System.Windows.Forms.Padding(4);
            this.BtnICCardFind.Name = "BtnICCardFind";
            this.BtnICCardFind.Size = new System.Drawing.Size(107, 35);
            this.BtnICCardFind.TabIndex = 6;
            this.BtnICCardFind.Text = "查询";
            this.BtnICCardFind.UseVisualStyleBackColor = true;
            this.BtnICCardFind.Click += new System.EventHandler(this.BtnICCardFind_Click);
            // 
            // BtnICCardCreate
            // 
            this.BtnICCardCreate.Location = new System.Drawing.Point(225, 154);
            this.BtnICCardCreate.Margin = new System.Windows.Forms.Padding(4);
            this.BtnICCardCreate.Name = "BtnICCardCreate";
            this.BtnICCardCreate.Size = new System.Drawing.Size(153, 35);
            this.BtnICCardCreate.TabIndex = 5;
            this.BtnICCardCreate.Text = "制卡/修改用户卡号";
            this.BtnICCardCreate.UseVisualStyleBackColor = true;
            this.BtnICCardCreate.Click += new System.EventHandler(this.BtnICCardCreate_Click);
            // 
            // BtnICCardRead
            // 
            this.BtnICCardRead.Location = new System.Drawing.Point(97, 154);
            this.BtnICCardRead.Margin = new System.Windows.Forms.Padding(4);
            this.BtnICCardRead.Name = "BtnICCardRead";
            this.BtnICCardRead.Size = new System.Drawing.Size(107, 35);
            this.BtnICCardRead.TabIndex = 4;
            this.BtnICCardRead.Text = "读卡";
            this.BtnICCardRead.UseVisualStyleBackColor = true;
            this.BtnICCardRead.Click += new System.EventHandler(this.BtnICCardRead_Click);
            // 
            // CTxtICCardIDFind
            // 
            this.CTxtICCardIDFind.EnabledButton = false;
            this.CTxtICCardIDFind.EnmTxtType = CustomControlLib.EnmTxtBoxType.ICCard;
            this.CTxtICCardIDFind.ImageButton = null;
            this.CTxtICCardIDFind.Location = new System.Drawing.Point(101, 75);
            this.CTxtICCardIDFind.Margin = new System.Windows.Forms.Padding(4);
            this.CTxtICCardIDFind.Name = "CTxtICCardIDFind";
            this.CTxtICCardIDFind.Size = new System.Drawing.Size(372, 26);
            this.CTxtICCardIDFind.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(2, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(99, 27);
            this.label3.TabIndex = 1;
            this.label3.Text = "用户卡号";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(2, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(99, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "物理卡号";
            // 
            // TpCustomer
            // 
            this.TpCustomer.BackColor = System.Drawing.SystemColors.Control;
            this.TpCustomer.Controls.Add(this.CucipCustomer);
            this.TpCustomer.Location = new System.Drawing.Point(4, 26);
            this.TpCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.TpCustomer.Name = "TpCustomer";
            this.TpCustomer.Padding = new System.Windows.Forms.Padding(4);
            this.TpCustomer.Size = new System.Drawing.Size(841, 487);
            this.TpCustomer.TabIndex = 0;
            this.TpCustomer.Text = "车主管理";
            // 
            // CucipCustomer
            // 
            this.CucipCustomer.BackColor = System.Drawing.SystemColors.Control;
            this.CucipCustomer.ImageEndBtn = global::WindowsFormLib.Properties.Resources.endPage;
            this.CucipCustomer.ImageLeftBtn = global::WindowsFormLib.Properties.Resources.leftPage;
            this.CucipCustomer.ImageRightBtn = global::WindowsFormLib.Properties.Resources.rightPage;
            this.CucipCustomer.ImageStartBtn = global::WindowsFormLib.Properties.Resources.startPage;
            this.CucipCustomer.Location = new System.Drawing.Point(0, 0);
            this.CucipCustomer.Margin = new System.Windows.Forms.Padding(4);
            this.CucipCustomer.Name = "CucipCustomer";
            this.CucipCustomer.Padding = new System.Windows.Forms.Padding(4);
            this.CucipCustomer.Size = new System.Drawing.Size(841, 487);
            this.CucipCustomer.TabIndex = 0;
            this.CucipCustomer.Text = "车主管理";
            this.CucipCustomer.BtnModifyClick += new System.EventHandler(this.BtnCustomerModify_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(475, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "（四位用户卡号，不足的补0）";
            // 
            // CFormCustomerManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 516);
            this.Controls.Add(this.TctlCustomer);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "CFormCustomerManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户管理";
            this.TctlCustomer.ResumeLayout(false);
            this.TpICCard.ResumeLayout(false);
            this.GbxICCardInfo.ResumeLayout(false);
            this.GbxICCardInfo.PerformLayout();
            this.GbxICCardFind.ResumeLayout(false);
            this.GbxICCardFind.PerformLayout();
            this.TpCustomer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TctlCustomer;
        private System.Windows.Forms.TabPage TpCustomer;
        private System.Windows.Forms.TabPage TpICCard;
        private System.Windows.Forms.GroupBox GbxICCardFind;
        private System.Windows.Forms.Button BtnICCardClose;
        private System.Windows.Forms.TextBox TxtICCardPhy;
        private System.Windows.Forms.Button BtnICCardFind;
        private System.Windows.Forms.Button BtnICCardCreate;
        private System.Windows.Forms.Button BtnICCardRead;
        private CUserTextButton CTxtICCardIDFind;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox GbxICCardInfo;
        private System.Windows.Forms.Button BtnICCardLogout;
        private System.Windows.Forms.Button BtnICCardCancelLoss;
        private System.Windows.Forms.Button BtnICCardLoss;
        private System.Windows.Forms.TextBox TxtICCardLogoutTime;
        private System.Windows.Forms.TextBox TxtWareHouse;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TxtICCardNewTime;
        private System.Windows.Forms.TextBox TxtICCardLossTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TxtICCardStatus;
        private System.Windows.Forms.TextBox TxtCarLocAddr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TxtICCardID;
        private System.Windows.Forms.TextBox TxtICCardType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private CUserCustomerInfoPanel CucipCustomer;
        private System.Windows.Forms.Button BtnICCardChange;
        private CUserTextButton CTxtICCardIDOld;
        private System.Windows.Forms.Label LblICCardIDOld;
        private System.Windows.Forms.Label label1;
    }
}