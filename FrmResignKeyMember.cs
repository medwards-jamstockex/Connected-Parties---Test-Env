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
    public partial class FrmResignKeyMember : Form
    {
        /*
        string connStr = ConfigurationManager
            .ConnectionStrings["DependConnectionString"]
            .ConnectionString;*/

        private MemberPosition _memberPosition;
        private List<MemberPosition> _memberPositions;
        //private List<Relationship> _relationships;
        public FrmResignKeyMember(MemberPosition memberPosition)
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
            txtIssuer.Text = _memberPosition.KeyMemberIssuer;
            txtPosition.Text = _memberPosition.KeyMemberPosition;
            dpMemberJoin.Value = _memberPosition.KeyMemberJoinDate;
            txtAccountID.Text = _memberPosition.KeyMemberAccountID.ToString();

            txtKeyMember.Enabled = false;
            txtIssuer.Enabled = false;
            txtPosition.Enabled = false;
            dpMemberJoin.Enabled = false;
            txtAccountID.Enabled = false;
            DateTime resignOn = new DateTime(9998, 12, 31);
            if (!(_memberPosition.KeyMemberResignDate.Date == resignOn))
                dpMemberResign.Enabled = false;

        }

        private void btnResign_Click(object sender, EventArgs e)
        {
            //MemberConnection con = (MemberConnection)dgvConnections.SelectedRows[0].DataBoundItem;
            if (dpMemberResign.Enabled == false)
            {
                MessageBox.Show("Member has already resigned");
                return;
            }

            if (dpMemberJoin.Value > dpMemberResign.Value)
            {
                MessageBox.Show("Member resignation date cannot be before member join date. Please select a valid resignation date");
                    return;
            }

            string message = "You are about to remove {0} from position {1} with {2}.\n\nAre you sure that you want to do this?";
            var dResult = MessageBox.Show(string.Format(message, txtKeyMember.Text, txtPosition.Text, txtIssuer.Text),
                "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dResult == DialogResult.Yes)
            {
                try
                {
                    DataSource.ResignPosition(txtAccountID.Text, txtPosition.Text, txtIssuer.Text, dpMemberJoin.Value, dpMemberResign.Value);
                    // reload data grid
                    //LoadPositions(pos.KeyMemberAccountID);
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

    }
}
