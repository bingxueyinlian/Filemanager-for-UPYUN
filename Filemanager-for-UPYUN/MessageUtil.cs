using System.Windows.Forms;

namespace Filemanager_for_UPYUN
{
    public static class MessageUtil
    {
        public static void Show(string msg)
        {
            MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Confirm(string msg)
        {
            return MessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }
    }
}
