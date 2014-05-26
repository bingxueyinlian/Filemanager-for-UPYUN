using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Filemanager_for_UPYUN
{
    public partial class LoginForm : Form
    {
        /// <summary>
        /// UpYun对象
        /// </summary>
        public UpYun UpYun { get; private set; }

        /// <summary>
        /// 已使用空间
        /// </summary>
        public int BucketUsage { get; private set; }
        private string savePath = "upyun";
        private string fileName = "upyun_user.txt";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            savePath = String.Concat(userPath, Path.DirectorySeparatorChar, savePath);
            fileName = String.Concat(userPath, Path.DirectorySeparatorChar, fileName);

            string userinfo = GetUserInto();
            if (!String.IsNullOrEmpty(userinfo))
            {
                string[] infos = userinfo.Split(new string[] { "[$$$$]" }, StringSplitOptions.RemoveEmptyEntries);
                if (infos.Length > 2)
                {
                    txtBucketName.Text = infos[0];
                    txtUserName.Text = infos[1];
                    txtPassword.Text = infos[2];
                }
            }
            else
            {
                //测试用数据
                //txtBucketName.Text = "myres";
                //txtUserName.Text = "op1";
                //txtPassword.Text = "123456789+";
            }

            txtBucketName.Select(0, 0);//避免一打开就选中全部文字
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string bucketname = txtBucketName.Text.Trim();
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            bool isVilid = CheckInputVilid(bucketname, username, password);
            if (!isVilid)
            {
                return;
            }

            this.UpYun = new UpYun(bucketname, username, password);
            //this.UpYun.setAuthType(true);//使用UPYUN签名认证
            try
            {
                //用获取已使用空间的方式来测试登陆是否成功
                this.BucketUsage = this.UpYun.getBucketUsage();

                if (chkSaveInfo.Checked)
                {
                    SaveUserInfo(bucketname, username, password);
                }
                else
                {
                    DeleteUserInfo();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (WebException we)
            {
                if (we.Status == WebExceptionStatus.ConnectFailure)
                {
                    MessageUtil.Warning("无法连接到服务器，请检查网络连接是否正常！");
                    return;
                }
                HttpWebResponse res = we.Response as HttpWebResponse;
                if (res != null && res.StatusCode == HttpStatusCode.Unauthorized)
                {
                    MessageUtil.Warning("登陆失败,请检查用户信息是否有误！");
                    return;
                }
            }
        }

        //验证输入是否有效!
        private bool CheckInputVilid(string bucketname, string username, string password)
        {
            if (bucketname == String.Empty)
            {
                MessageUtil.Warning("请输入空间名！");
                txtBucketName.Focus();
                return false;
            }
            if (bucketname == String.Empty)
            {
                MessageUtil.Warning("请输入用户名！");
                txtUserName.Focus();
                return false;
            }
            if (password == String.Empty)
            {
                MessageUtil.Warning("请输入密码！");
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        //删除用户信息
        private void DeleteUserInfo()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch { }
        }

        //获取用户数据
        private string GetUserInto()
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string encryptedString = File.ReadAllText(fileName);
                    return EncryptUtil.DecryptString(encryptedString);
                }
            }
            catch { }
            return String.Empty;
        }

        //保存用户数据
        private void SaveUserInfo(string bucketname, string username, string password)
        {
            try
            {
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                string userinfo = String.Format("{0}[$$$$]{1}[$$$$]{2}", bucketname, username, password);//用[$$$$]分隔
                userinfo = EncryptUtil.EncryptString(userinfo);
                File.WriteAllText(fileName, userinfo);
            }
            catch { }

        }

        private void lnkFogotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkFogotPassword.LinkVisited = true;
            Process.Start("https://www.upyun.com/cp/#/lostpass/");
        }

        private void lnkRegist_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnkRegist.LinkVisited = true;
            Process.Start("https://www.upyun.com/cp/#/register/");
        }
    }
}
