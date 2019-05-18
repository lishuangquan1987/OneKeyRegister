using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace OneKeyRegisterNew
{
    public partial class RegisterForm : Form
    {
        private IRegister register;
        private string[] paths;
        private RegisterForm()
        {
            InitializeComponent();           
        }
        public RegisterForm(string[] paths,IRegister register):this()
        {
            this.register = register;
            this.paths = paths;
        }

        Thread thread;
        private void RegisterForm_Load(object sender, EventArgs e)
        {
             thread = new Thread(() =>
              {
                  //Thread.Sleep(500);
                  foreach (var i in paths)
                  {
                      var result = register.Run(i);
                      if (result.IsSuccess)
                      {
                          this.richTextBox1.ShowMsg(i + " success ", LogTypes.OK);
                      }
                      else
                      {
                          this.richTextBox1.ShowMsg(result.ErrorMsg, LogTypes.NG);
                      }
                  }
                  this.richTextBox1.ShowMsg("finish,press any key to exit...", LogTypes.Info);
              });
            thread.IsBackground = true;
            thread.Start();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            if (thread.ThreadState != ThreadState.Stopped)
            {
                return;
            }
            this.Close();
        }
    }
}
