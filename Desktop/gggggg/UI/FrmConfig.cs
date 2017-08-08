using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DXzonghejiaofei.BLL;
using DXzonghejiaofei.BLL.Consumer;
using DXzonghejiaofei.Models;
using DXzonghejiaofei.Models.Consumer;
using DXzonghejiaofei.Properties;

namespace DXzonghejiaofei.UI
{
    public partial class FrmConfig : Form
    {
        private static SystemConfig _sysConfig;
        private static UserConfig _usrConfig;

        public FrmConfig(SystemConfig sysConfig,UserConfig usrConfig)
        {
            InitializeComponent();

            _sysConfig=sysConfig;
            _usrConfig=usrConfig;
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            txtServerUrl.Text =_sysConfig.ServerUrl;
            txtChannelId.Text =_sysConfig.ChannelId;
            txtSecretKey.UseSystemPasswordChar=true;
            txtSecretKey.Text =_sysConfig.Secretkey;

            txtUserName.Text =_usrConfig.UserName;
            txtUserKey.UseSystemPasswordChar=true;
            txtUserKey.Text =_usrConfig.UserKey;
            txtUserUrl.Text = _usrConfig.UserUrl;
            txtUserPayPwd.UseSystemPasswordChar = true;
            txtUserPayPwd.Text = _usrConfig.UserPayKey;
        }

        private void btnSaveSystemConfig_Click(object sender,EventArgs e)
        {
            var serverUrl = txtServerUrl.Text.Trim();
            if (string.IsNullOrEmpty(serverUrl)||!serverUrl.StartsWith("http://"))
            {
                MessageBox.Show(Resources.ServerUrlErrMsg,Resources.TipMsg,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            var channelId = txtChannelId.Text.Trim();
            if(string.IsNullOrEmpty(channelId))
            {
                MessageBox.Show(Resources.ChannelIdErrMsg,Resources.TipMsg,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            var secretKey = txtSecretKey.Text.Trim();
            if(string.IsNullOrEmpty(secretKey))
            {
                MessageBox.Show(Resources.SecretKeyErrMsg,Resources.TipMsg,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _sysConfig.ServerUrl = serverUrl;
            _sysConfig.ChannelId = channelId;
            _sysConfig.Secretkey = secretKey;

            var sysConfig = SystemConfigManager.Save(_sysConfig);
            MessageBox.Show(sysConfig.Message);
        }

        private void btnSaveUserConfig_Click(object sender,EventArgs e)
        {
            var userName = txtUserName.Text.Trim();
            if(string.IsNullOrEmpty(userName))
            {
                MessageBox.Show(Resources.UserNameErrMsg,Resources.TipMsg,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            var userKey = txtUserKey.Text.Trim();
            if(string.IsNullOrEmpty(userKey))
            {
                MessageBox.Show(Resources.UserKeyErrMsg,Resources.TipMsg,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            var userUrl = txtUserUrl.Text.Trim();
            if(string.IsNullOrEmpty(userUrl))
            {
                MessageBox.Show(Resources.UserUrlErrMsg,Resources.TipMsg,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _usrConfig.UserName = userName;
            _usrConfig.UserKey = userKey;
            _usrConfig.UserUrl = userUrl;
            var usrConfig = UserConfigManager.Save(_usrConfig);
            MessageBox.Show(usrConfig.Message);
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
