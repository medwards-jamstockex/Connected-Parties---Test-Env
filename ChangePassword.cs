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
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            
            string uniqueReference, oldpass, newpass,   confirmpass;
            uniqueReference = Utils.GenerateUniqueReference(); // reference for operation used for auditing
            //sessionID = 
            
            oldpass = txt_oldpassword.Text.Trim();
            newpass = txt_newpassword.Text.Trim();
            confirmpass = txt_confirmpassword.Text.Trim();

            if (oldpass == "")
            {
                MessageBox.Show("Please enter your username, your password, and your participant code.");
                return;
            }
            if (newpass == "")
            {
                MessageBox.Show("Please enter your username, your password, and your participant code.");
                return;
            }
            if (confirmpass == "")
            {
                MessageBox.Show("Please enter your username, your password, and your participant code.");
                return;
            }
            if (confirmpass != newpass)
            {
                MessageBox.Show("Please enter your username, your password, and your participant code.");
                return;
            }
            
            if (Server.ChangePass(uniqueReference, oldpass, newpass, Server.SessionID))
            {
                MessageBox.Show("Your password has been changed");
                this.Dispose();
            }
            else
            {
                MessageBox.Show(@"Your password has not been changed.
                                 Please contact your administrator");
                Application.Exit();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
