using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OneKeyRegisterNew
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnRegisterOCX_Click(object sender, EventArgs e)
        {
            List<string> paths = new List<string>();
            string[] dlls = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory,"*.dll", SearchOption.TopDirectoryOnly);
            string[] ocxs = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.ocx", SearchOption.TopDirectoryOnly);
            paths.AddRange(dlls);
            paths.AddRange(ocxs);
            if (paths.Count == 0)
            {
                MessageBox.Show("no dll or ocx found in the current directory!");
                return;
            }
            RegisterForm frm = new RegisterForm(paths.ToArray(),new VB6.VB6Register());
            frm.Text = "register vb6 ocx or dll";
            frm.ShowDialog();
        }

        private void btnUnregisterVB6_Click(object sender, EventArgs e)
        {
            List<string> paths = new List<string>();
            string[] dlls = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.TopDirectoryOnly);
            string[] ocxs = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.ocx", SearchOption.TopDirectoryOnly);
            paths.AddRange(dlls);
            paths.AddRange(ocxs);
            if (paths.Count == 0)
            {
                MessageBox.Show("no dll or ocx found in the current directory!");
                return;
            }
            RegisterForm frm = new RegisterForm(paths.ToArray(), new VB6.VB6UnRegister());
            frm.Text = "unregister vb6 ocx or dll";
            frm.ShowDialog();
        }

        private void btnRegisterCSInterface_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"CSInterface.dll");
            if (!File.Exists(path))
            {
                MessageBox.Show("CSInterface.dll is not found in current directory!");
                return;
            }
            RegisterForm frm = new RegisterForm(new string[] { path},new CSharp.CSharpRegisterNew());
            frm.Text = "register csharp interface";
            frm.ShowDialog();
        }

        private void btnUnregisterCSInterface_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CSInterface.dll");
            if (!File.Exists(path))
            {
                MessageBox.Show("CSInterface.dll is not found in current directory!");
                return;
            }
            RegisterForm frm = new RegisterForm(new string[] { path }, new CSharp.CSharpUnregisterNew());
            frm.Text = "unregister csharp interface";
            frm.ShowDialog();
        }
    }
}
