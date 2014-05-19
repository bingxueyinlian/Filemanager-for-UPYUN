using System;
using System.Net;
using System.Windows.Forms;

namespace Filemanager_for_UPYUN
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string bucketname = txtBucketName.Text.Trim();
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            UpYun upyun = new UpYun(bucketname, username, password);
            //upyun.setAuthType(true);//使用UPYUN签名认证
            try
            {
                int bucketUsage = upyun.getBucketUsage();
                MainForm mainForm = new MainForm(upyun, bucketUsage);
                mainForm.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("操作失败!");
            }

        }

    }
}
