using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DXzonghejiaofei.Properties;

namespace DXzonghejiaofei.UI
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.stStrip = new System.Windows.Forms.StatusStrip();
            this.tslbMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslbcompany = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslbQQ = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsRefresh = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnConfigure = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.stStrip.SuspendLayout();
            this.cmsRefresh.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // stStrip
            // 
            this.stStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbMessage,
            this.tslbcompany,
            this.toolStripStatusLabel1,
            this.tslbQQ});
            this.stStrip.Location = new System.Drawing.Point(0, 647);
            this.stStrip.Name = "stStrip";
            this.stStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.stStrip.Size = new System.Drawing.Size(1085, 22);
            this.stStrip.TabIndex = 0;
            this.stStrip.Text = "statusStrip1";
            // 
            // tslbMessage
            // 
            this.tslbMessage.Name = "tslbMessage";
            this.tslbMessage.Size = new System.Drawing.Size(139, 17);
            this.tslbMessage.Text = "欢迎使用本软件   V2.0.0";
            // 
            // tslbcompany
            // 
            this.tslbcompany.IsLink = true;
            this.tslbcompany.Name = "tslbcompany";
            this.tslbcompany.Size = new System.Drawing.Size(634, 17);
            this.tslbcompany.Spring = true;
            this.tslbcompany.Text = "山东鼎信网络科技有限公司";
            this.tslbcompany.Click += new System.EventHandler(this.tslbcompany_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(134, 17);
            this.toolStripStatusLabel1.Text = "商务QQ：2355790406";
            // 
            // tslbQQ
            // 
            this.tslbQQ.DoubleClickEnabled = true;
            this.tslbQQ.Name = "tslbQQ";
            this.tslbQQ.Size = new System.Drawing.Size(158, 17);
            this.tslbQQ.Text = "技术支持QQ：2853721758";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "快捷互联--海南元力";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // cmsRefresh
            // 
            this.cmsRefresh.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.toolStripMenuItem3,
            this.toolStripSeparator3});
            this.cmsRefresh.Name = "cmsRefresh";
            this.cmsRefresh.Size = new System.Drawing.Size(149, 60);
            this.cmsRefresh.Text = "刷新";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F5)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem1.Text = "刷新";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(148, 22);
            this.toolStripMenuItem3.Text = "删除上月订单";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(145, 6);
            // 
            // btnConfigure
            // 
            this.btnConfigure.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnConfigure.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfigure.Image = global::DXzonghejiaofei.Properties.Resources.config;
            this.btnConfigure.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfigure.Location = new System.Drawing.Point(807, 238);
            this.btnConfigure.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfigure.Name = "btnConfigure";
            this.btnConfigure.Size = new System.Drawing.Size(265, 51);
            this.btnConfigure.TabIndex = 0;
            this.btnConfigure.Text = " 系统配置";
            this.btnConfigure.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfigure.UseVisualStyleBackColor = true;
            this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnPause.Enabled = false;
            this.btnPause.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPause.Image = global::DXzonghejiaofei.Properties.Resources.pause;
            this.btnPause.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPause.Location = new System.Drawing.Point(807, 174);
            this.btnPause.Margin = new System.Windows.Forms.Padding(4);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(265, 51);
            this.btnPause.TabIndex = 0;
            this.btnPause.Text = " 暂停充值";
            this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Image = global::DXzonghejiaofei.Properties.Resources.start;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStart.Location = new System.Drawing.Point(807, 110);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(265, 51);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = " 开始充值";
            this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::DXzonghejiaofei.Properties.Resources.restart_logo;
            this.pictureBox1.Location = new System.Drawing.Point(812, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(248, 71);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // rtxtLog
            // 
            this.rtxtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtxtLog.Location = new System.Drawing.Point(3, 19);
            this.rtxtLog.Margin = new System.Windows.Forms.Padding(4);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(794, 625);
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            // 
            // grpLog
            // 
            this.grpLog.Controls.Add(this.rtxtLog);
            this.grpLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpLog.Location = new System.Drawing.Point(0, 0);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(800, 647);
            this.grpLog.TabIndex = 2;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "日志";
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(812, 601);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(249, 43);
            this.btnClearLog.TabIndex = 4;
            this.btnClearLog.Text = "清除日志";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 669);
            this.Controls.Add(this.btnClearLog);
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnConfigure);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.stStrip);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DXzonghejiaofeiApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
            this.stStrip.ResumeLayout(false);
            this.stStrip.PerformLayout();
            this.cmsRefresh.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip stStrip;
        private ToolStripStatusLabel tslbcompany;
        private ToolStripStatusLabel tslbQQ;
        private ToolStripStatusLabel tslbMessage;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip cmsRefresh;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;
        private Button btnConfigure;
        private Button btnPause;
        private Button btnStart;
        private PictureBox pictureBox1;
        private RichTextBox rtxtLog;
        private GroupBox grpLog;
        private Button btnClearLog;
    }
}

