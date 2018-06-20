using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace ConnectedParties
{
    public partial class FrmNewKeyMember : Form
    {
        /*
        string connStr = ConfigurationManager
            .ConnectionStrings["DependConnectionString"]
            .ConnectionString;*/
 
        private List<Member> _members;
        private List<Relationship> _relationships;
        public FrmNewKeyMember(List<Member> members, List<Relationship> relationships)
        {
            InitializeComponent();

            _members = members;
            _relationships = relationships;
            InitControls();
        }

        private void InitControls()
        {
            AutoCompleteStringCollection namesource = new AutoCompleteStringCollection();
            namesource.AddRange((from m in _members select m.UIName).ToArray());

            txtKeyMember.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtKeyMember.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtKeyMember.AutoCompleteCustomSource = namesource;

            //txtConnectedParty.AutoCompleteMode = AutoCompleteMode.Suggest;
            //txtConnectedParty.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txtConnectedParty.AutoCompleteCustomSource = namesource; 

            cbIssuer.DataSource = Server.getTable("LIST_ISSUER", "Order by ISSUER_NAME", -1, "");
            cbIssuer.DisplayMember = "ISSUER_NAME";
            cbIssuer.ValueMember = "ISSUER_ID";

            cbIssuer.SelectedIndex = -1;

            cbMembership.DataSource = Server.getTable("CONFIG_DIRECTOR_STATUS", "Order by DIRECTOR_STATUS_DESCRIPTION", -1, "");
            cbMembership.DisplayMember = "DIRECTOR_STATUS_DESCRIPTION";
            cbMembership.ValueMember = "DIRECTOR_STATUS_CODE";
            cbMembership.SelectedIndex = -1;

            cbJoinReason.DataSource = Server.getTable("CONFIG_JOIN_REASON", "Order by JOIN_DESCRIPTION", -1, "");
            cbJoinReason.DisplayMember = "JOIN_DESCRIPTION";
            cbJoinReason.ValueMember = "JOIN_REASON";
            cbJoinReason.SelectedIndex = -1;

            cbReportType.DataSource = Server.getTable("CONFIG_REPORT_TYPE", "Order by REPTYPE_DESCRIPTION", -1, "");
            cbReportType.DisplayMember = "REPTYPE_DESCRIPTION";
            cbReportType.ValueMember = "REPTYPE";
            cbReportType.SelectedIndex = -1;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //rel = cbRelationship.SelectedText.ToString();
            
            // validate the controls
            Member keyMember = GetUserSelectedMember(txtKeyMember.Text);
            if (keyMember == null)
            {
                MessageBox.Show("Please select a valid \"Key Member\"");
                return;
            }

            if (cbIssuer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a issuer");
                return;
            }

            if (cbMembership.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a membership type");
                return;
            }

            if (cbJoinReason.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a member join reason");
                return;
            }

            if (cbReportType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a member report type");
                return;
            }

            // attempt to save
            try
            {
                var te = DataSource.GetPositions(keyMember.AccountID, (int)cbIssuer.SelectedValue);
                if (te.Count() > 0)
                {
                    MessageBox.Show("Member already registed for position a with " + cbIssuer.Text + "!");
                    return;
                }

                //MessageBox.Show("Connection saved!");

                //int keyMemAccountId, int issuerId, int memTypeId, int memJoinReason, int memReportType, DateTime memJoinDate
                DataSource.SaveMember(keyMember.AccountID, keyMember.TRN, (int)cbIssuer.SelectedValue, cbMembership.SelectedValue.ToString(), 
                                       cbJoinReason.SelectedValue.ToString(), cbReportType.SelectedValue.ToString(), dpMemberJoin.Value.Date,
                                       txtPosition.Text.ToString().Trim());

                MessageBox.Show("Key Member saved!");

                //txtConnectedParty.Text = "";
                //cbMembership.SelectedIndex = -1;

                // highlight the text for key member
                txtKeyMember.Focus();
                txtKeyMember.SelectionStart = 0;
                txtKeyMember.SelectionLength = txtKeyMember.Text.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops! Something went wrong.\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private Member GetUserSelectedMember(string selected)
        {
            Member mem = null;
            try
            {
                mem = _members
                    .Where(x => x.NameID == Int32.Parse(selected.Split('-').Last().Trim()))
                    .SingleOrDefault();

                //mem = _members
                //.Where(x => x.AccountReference == selected.Split('-').Last().Trim())
                //.SingleOrDefault();
            }
            catch { }

            return mem;
        }

    }
}
