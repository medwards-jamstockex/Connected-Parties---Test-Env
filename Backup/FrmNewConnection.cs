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
    public partial class FrmNewConnection : Form
    {
        /*
        string connStr = ConfigurationManager
            .ConnectionStrings["DependConnectionString"]
            .ConnectionString;*/
 
        private List<Member> _members;
        private List<Relationship> _relationships;
        public FrmNewConnection(List<Member> members, List<Relationship> relationships)
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

            txtConnectedParty.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtConnectedParty.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtConnectedParty.AutoCompleteCustomSource = namesource; 
          
            // load the relationships
            cbRelationship.DataSource = _relationships;
            cbRelationship.DisplayMember = "Description";
            cbRelationship.ValueMember = "ID";
            cbRelationship.SelectedIndex = -1;
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

            Member conMember = GetUserSelectedMember(txtConnectedParty.Text);
            if (conMember == null)
            {
                MessageBox.Show("Please select a valid \"Connected Party\"");
                return;
            }

            if (cbRelationship.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a relationship");
                return;
            }

            if (keyMember.NameID == conMember.NameID && cbRelationship.SelectedValue.ToString() != "5")
            {
                MessageBox.Show(@"If Key Member and Connected Party are the same
                                relationship must be self");
                return;
            }

            // attempt to save
            try
            {
                DataSource.SaveConnection(keyMember.NameID, conMember.NameID,
                (int)cbRelationship.SelectedValue);

                MessageBox.Show("Connection saved!");

                txtConnectedParty.Text = "";
                cbRelationship.SelectedIndex = -1;

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
