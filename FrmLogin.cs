using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Reflection;

namespace ConnectedParties
{
    public partial class FrmLogin : Form
    {
        private readonly string _appDesc = Assembly.GetExecutingAssembly().GetName().Name
            + " Ver: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public FrmLogin()
        {
            InitializeComponent();

            cbEnvironment.DataSource = System.Enum.GetValues(typeof(WorkingEnvironment));
            cbEnvironment.SelectedIndex = -1;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "" ||
                txtUsername.Text.Trim() == "" ||
                txtParticipantCode.Text.Trim() == "")
            {
                MessageBox.Show("Please enter your username, your password, and your participant code.");
                return;
            }

            if (cbEnvironment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an enrironment");
                return;
            }           

            //
            string sessionID = string.Empty;
            try
            {
                string endpointConf = (WorkingEnvironment)cbEnvironment.SelectedItem == WorkingEnvironment.RSU ?
                    "RDServiceTrain" : "RDServiceProduction";

                Server.environ = endpointConf;

                Server _server = new Server(endpointConf);

                var response = Server.Login(txtUsername.Text.Trim(), txtPassword.Text.Trim(), txtParticipantCode.Text.Trim());

                //ConnectedParties.RDService.RDServiceClient dependClient =
                //    new ConnectedParties.RDService.RDServiceClient(endpointConf);
                //var response = dependClient.DependLogin(out sessionID, txtUsername.Text.Trim(),
                //    ComputeMD5Hash(txtPassword.Text.Trim()), _appDesc, txtParticipantCode.Text.Trim());

                if (Server.rt.HasError)
                {
                    MessageBox.Show(Server.rt.ErrorInfo.ErrorText);
                    return;
                }

                // get the user details
                string[] userDetails = DataSource.Getuser(txtUsername.Text.Trim(), txtParticipantCode.Text.Trim());
                CurrentUser.Instance.LoginName = txtUsername.Text.Trim();
                CurrentUser.Instance.SessionID = sessionID;
                CurrentUser.Instance.UserID = int.Parse(userDetails[0]);
                CurrentUser.Instance.Name = userDetails[1];
                CurrentUser.Instance.Environment = (WorkingEnvironment)cbEnvironment.SelectedItem;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops! Something went wrong. \n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ComputeMD5Hash(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));

            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hash)
                stringBuilder.AppendFormat("{0:x2}", b);

            return stringBuilder.ToString();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void cbEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
