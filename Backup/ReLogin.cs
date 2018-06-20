using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConnectedParties
{
    public partial class ReLogin : Form
    {
        public ReLogin()
        {
            InitializeComponent();
            lbl_User.Text = "User: " + CurrentUser.Instance.LoginName;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string password = String.Empty;

            password = txt_newpassword.Text.Trim();

            //this.Hide();

             if (password == "")
             {
                 MessageBox.Show("Please enter your password.");
                 foreach (Form form in Application.OpenForms)
                 {
                     if (form is ReLogin)
                     {
                         form.Show();
                         break;
                     }
                 }
                 return;
             }
             Server.Login(CurrentUser.Instance.LoginName, password, CurrentUser.Instance.Environment.ToString());

             if (Server.SessionID == null || Server.rt.ErrorInfo.ErrorReference == "ML001051")
             {
                 LogDetails.writeLog(Server.logfile, "Login failed. Terminating");
                 LogDetails.writeLog(Server.logfile, Server.rt.ErrorInfo.ErrorText.ToString());
                 MessageBox.Show(Server.rt.ErrorInfo.ErrorText.ToString());

                 //Server.isLoggedIn();

                 //Application.Exit();
                 //return;
             }
             this.Hide();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            Environment.Exit(1);
            //Server.Logout();
            return;
        }
    }
}
