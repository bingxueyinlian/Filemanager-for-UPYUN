using System;
using System.Windows.Forms;

namespace Filemanager_for_UPYUN
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm loginForm = new LoginForm();//登陆窗口
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                //启动主窗口
                Application.Run(new MainForm(loginForm.UpYun, loginForm.BucketUsage));
            }
        }
    }
}
