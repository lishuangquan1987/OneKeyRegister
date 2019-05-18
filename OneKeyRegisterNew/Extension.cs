using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneKeyRegisterNew
{
   public static class Extension
    {
        public static void ShowMsg(this RichTextBox rtb,string msg, LogTypes logTypes)
        {
            if (rtb.InvokeRequired)
            {
                rtb.Invoke(new Action<RichTextBox, string, LogTypes>(ShowMsg), rtb, msg, logTypes);
                return;
            }

            switch (logTypes)
            {
                case LogTypes.OK:
                    rtb.SelectionColor = System.Drawing.Color.Green;
                    break;
                case LogTypes.NG:
                    rtb.SelectionColor = System.Drawing.Color.Red;
                    break;
                case LogTypes.Info:
                    rtb.SelectionColor = System.Drawing.Color.Black;
                    break;
                default:
                    rtb.SelectionColor = System.Drawing.Color.Black;
                    break;
            }
            msg = string.Format("{0}:{1}\r\n",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),msg);
            rtb.AppendText(msg);
            rtb.ScrollToCaret();
        }
    }
}
