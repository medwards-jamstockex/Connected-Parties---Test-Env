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
    public partial class FrmEditKeyMember : Form
    {
        /*
        string connStr = ConfigurationManager
            .ConnectionStrings["DependConnectionString"]
            .ConnectionString;*/

        private MemberPosition _memberPosition;
        private List<MemberPosition> _memberPositions;
        //private List<Relationship> _relationships;
        public FrmEditKeyMember(MemberPosition memberPosition)
        {
            InitializeComponent();

            //_memberPositions = memberPositions;
            _memberPosition = memberPosition;
            //_relationships = relationships;
            InitControls();
        }

        private void InitControls()
        {
            txtKeyMember.Text = _memberPosition.KeyMemberName;
            //cbIssuer.Text = _memberPosition.KeyMemberIssuer;
            txtPosition.Text = _memberPosition.RSUMemberPosition.ToString().Trim();
            dpMemberJoin.Value = _memberPosition.KeyMemberJoinDate;
            txtMembership.Text = _memberPosition.KeyMemberPosition.ToString().Trim();

            DateTime resignOn = new DateTime(9998, 12, 31);
            dpMemberResign.MaxDate = resignOn;
            dpMemberResign.Value = _memberPosition.KeyMemberResignDate;
            txtAccountID.Text = _memberPosition.KeyMemberAccountID.ToString();

            cbIssuer.DataSource = Server.getTable("LIST_ISSUER", "Order by ISSUER_NAME", -1, "");
            cbIssuer.DisplayMember = "ISSUER_NAME";
            cbIssuer.ValueMember = "ISSUER_ID";
            cbIssuer.SelectedIndex = cbIssuer.FindStringExact(_memberPosition.KeyMemberIssuer);

            cbMembership.DataSource = Server.getTable("CONFIG_DIRECTOR_STATUS", "Order by DIRECTOR_STATUS_DESCRIPTION", -1, "");
            cbMembership.DisplayMember = "DIRECTOR_STATUS_DESCRIPTION";
            cbMembership.ValueMember = "DIRECTOR_STATUS_CODE";
            cbMembership.SelectedIndex = cbMembership.FindStringExact(_memberPosition.KeyMemberPosition);
            //cbMembership.SelectedItem = _memberPosition.KeyMemberPosition;

            cbJoinReason.DataSource = Server.getTable("CONFIG_JOIN_REASON", "Order by JOIN_DESCRIPTION", -1, "");
            cbJoinReason.DisplayMember = "JOIN_REASON";
            cbJoinReason.ValueMember = "JOIN_REASON";
            cbJoinReason.SelectedIndex = cbJoinReason.FindStringExact(_memberPosition.KeyMemberJoinReason);
            cbJoinReason.DisplayMember = "JOIN_DESCRIPTION";

            cbReportType.DataSource = Server.getTable("CONFIG_REPORT_TYPE", "Order by REPTYPE_DESCRIPTION", -1, "");
            cbReportType.DisplayMember = "REPTYPE";
            cbReportType.ValueMember = "REPTYPE";
            //cbReportType.SelectedIndex = -1;
            cbReportType.SelectedIndex = cbReportType.FindStringExact(_memberPosition.KeyMemberReportType);
            cbReportType.DisplayMember = "REPTYPE_DESCRIPTION";
 

            txtKeyMember.Enabled = false;
            cbIssuer.Enabled = false;
            //txtPosition.Enabled = false;
            //dpMemberJoin.Enabled = false;
            txtAccountID.Enabled = false;
            //DateTime resignOn = new DateTime(9999, 12, 31);
            //if (!(_memberPosition.KeyMemberResignDate.Date == resignOn))
            //    dpMemberResign.Enabled = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //MemberConnection con = (MemberConnection)dgvConnections.SelectedRows[0].DataBoundItem;
            //if (dpMemberResign.Enabled == false)
            //{
            //    MessageBox.Show("Member has already resigned");
            //    return;
            //}
            //MemberPosition keyMember = GetUserSelectedMember(txtKeyMember.Text, (int)cbIssuer.SelectedValue);

            var te = DataSource.GetPositions(_memberPosition.KeyMemberAccountID, (int)cbIssuer.SelectedValue, cbMembership.SelectedValue.ToString());
            if (te.Count() > 0)
            {
                MessageBox.Show("Member already registed for position a with " + cbIssuer.Text + "!");
                return;
            }


            if (dpMemberJoin.Value > dpMemberResign.Value)
            {
                MessageBox.Show("Member resignation date cannot be before member join date. Please select a valid resignation date");
                return;
            }

            string message = "You are about to update {0} holding position {1} with {2}.\n\nAre you sure that you want to do this?";
            var dResult = MessageBox.Show(string.Format(message, txtKeyMember.Text, txtPosition.Text, cbIssuer.Text),
                "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dResult == DialogResult.Yes)
            {
                try
                {
                    DataSource.EditKeyMember(txtAccountID.Text, txtMembership.Text, cbIssuer.SelectedValue.ToString(), dpMemberJoin.Value, dpMemberResign.Value,
                                                cbJoinReason.SelectedValue.ToString(), cbReportType.SelectedValue.ToString(), _memberPosition.KeyMemberTRN, txtPosition.Text.ToString().Trim(), cbMembership.SelectedValue.ToString());
                    // reload data grid
                    //LoadPositions(pos.KeyMemberAccountID);
                    //EditKeyMember(string KeyMemberAccountID, string txtPosition, string txtIssuer, DateTime MemberJoin, DateTime MemberResign,
                    //                     string memJoinReason, string memReportType, string memTRN, string RSUPosition)

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oops! Something went wrong. \n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private MemberPosition GetUserSelectedMember(string selected, int issuer)
        {
            MemberPosition memPos = null;
            DateTime resignOn = new DateTime(9998, 12, 31);


            try
            {
                memPos = _memberPositions
                    .Where(x => x.KeyMemberAccountID == Int32.Parse(selected.Split('-').ElementAt(1).Trim())
                            && x.KeyMemberIssuer == issuer.ToString()
                            && x.KeyMemberResignDate == resignOn)
                    .SingleOrDefault();

                //mem = _members
                //.Where(x => x.AccountReference == selected.Split('-').Last().Trim())
                //.SingleOrDefault();
            }
            catch { }

            return memPos;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string message = "You are about to delete {0} holding position {1} with {2}.\n\nAre you sure that you want to do this?";
            var dResult = MessageBox.Show(string.Format(message, txtKeyMember.Text, txtPosition.Text, cbIssuer.Text),
                "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dResult == DialogResult.Yes)
            {
                try
                {
                    var cbIssuerval = cbIssuer.SelectedValue == null ? "" : cbIssuer.SelectedValue.ToString();

                    DataSource.DeleteKeyMember(txtAccountID.Text, cbMembership.SelectedValue.ToString(), cbIssuerval, dpMemberJoin.Value, dpMemberResign.Value,
                                                cbJoinReason.SelectedValue.ToString(), cbReportType.SelectedValue.ToString(), _memberPosition.KeyMemberTRN, txtPosition.Text.ToString().Trim());
                    // reload data grid
                    //LoadPositions(pos.KeyMemberAccountID);
                    //EditKeyMember(string KeyMemberAccountID, string txtPosition, string txtIssuer, DateTime MemberJoin, DateTime MemberResign,
                    //                     string memJoinReason, string memReportType, string memTRN, string RSUPosition)

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Oops! Something went wrong. \n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtKeyMember_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
