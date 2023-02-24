using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;

namespace Management_Football_Pitch
{
    public partial class fLogin : Form
    {
        Management_FootballPitchEntities db = new Management_FootballPitchEntities();
        public fLogin()
        {
            InitializeComponent();
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.Text = string.Empty;
            this.ControlBox = false;
        }


        private void fLogin_Load(object sender, EventArgs e)
        {
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Texts;

            string hash_pass = HasCode(txtPassword.Texts);
            var user = db.Accounts.Where(x => x.username == username && x.password == hash_pass).FirstOrDefault();
            if (user != null)
            {
                if (user.permission == "admin")
                {
                    if (!checkboxRemember.Checked)
                    {
                        txtPassword.Texts = "";
                    }
                    FormMainManagement f = new FormMainManagement();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Tài khoản không có quyền truy cập", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Texts = "";
                    txtUsername.Texts = "";
                    txtUsername.Focus();
                }
            }
            else
            {
                MessageBox.Show("Tài khoản và mật khẩu không đúng", "Thông Báo !", MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtPassword.Texts = "";
                txtPassword.Focus();
            }

        }
        public string HasCode(string str)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(str);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";

            foreach (byte item in hasData)
            {
                hasPass += item;
            }

            return hasPass;
        }


        public void ReadFile(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
               File.ReadLines(path);
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }

        }

        public void WriteFile(string path)
        {

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Checked = false");
                    sw.WriteLine("txtUsername = ''");
                    sw.WriteLine("txtPassword=''");
                }
            }
            else
            {

            }
           

        }


    }

}
