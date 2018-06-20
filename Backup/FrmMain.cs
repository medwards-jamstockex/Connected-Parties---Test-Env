using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Reflection;

namespace ConnectedParties
{
    public partial class FrmMain : Form
    {
        private List<Member> Members { get; set; }
        private List<Member> KeyMembers { get; set; }
        private List<MemberPosition> MemberPositions { get; set; } // 
        private List<InstrumentNote> Instruments { get; set; }
        private List<Relationship> Relationships { get; set; }
        private FrmLoading _frmLoading = null;
        delegate void CloseLoadingDialog_Del();
        delegate void LoadAutoCompleteData_Del();

        public FrmMain()
        {
            InitializeComponent();

            //((Control)this.tabPage2).Enabled = false;
            //((Control)this.tabPage2).Visible = false;
            //((Control)this.tabPage2).Hide();
            tabControl1.Controls.Remove(tabPage2);

            //Get Depend Sys Preference. Environment Color and Name
            DataTable sysSeting = Server.getTable("LIST_CONFIG_SYSTEMPREF_FULL", "SETTING_TAG = 'EnvironmentText' or SETTING_TAG = 'EnvironmentColour'", 2, ".1");
            //DataTable sysEnv = Server.getTable("LIST_CONFIG_SYSTEMPREF_FULL", "SETTING_TAG = 'EnvironmentText'", ".1");
            //DataTable sysColor = Server.getTable("LIST_CONFIG_SYSTEMPREF_FULL", "SETTING_TAG = 'EnvironmentColour'", ".1");

            var sysEnv = sysSeting.Rows[1]["SYSPREF_VALUE"];
            var sysColor = sysSeting.Rows[0]["SYSPREF_VALUE"].ToString();

            //List<string> list = new List<string>(sysColor);
            //var rgbColValues = sysColor.Split(',').ToList<int>();
            var rgbColValues = sysColor.Split(',').Select(n => int.Parse(n)).ToList();

            //this.BackColor = Color.FromArgb(rgbColValues[0], rgbColValues[1], rgbColValues[2]);

            menuStrip1.BackColor = Color.FromArgb(rgbColValues[0], rgbColValues[1], rgbColValues[2]);

            string appName = "RSU Depend Connected Parties Version {0}. (You are connected to " + sysEnv + ")";

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //this.Text = String.Format(appName, version);
            string applicationver = String.Format(appName, version);

            //this.Text = String.Format(CurrentUser.Instance.Name,appName, version);
            this.Text = string.Format(this.Text, CurrentUser.Instance.Environment, CurrentUser.Instance.Name, applicationver);
        }

        private void btnNewConnection_Click(object sender, EventArgs e)
        {
            FrmNewConnection frm = new FrmNewConnection(Members, Relationships);
            frm.ShowDialog();
        }

        private void CloseLoaingDialog()
        {
            if (this._frmLoading != null && this._frmLoading.InvokeRequired)
            {
                CloseLoadingDialog_Del d = new CloseLoadingDialog_Del(CloseLoaingDialog);
                this.Invoke(d);
            }
            else
            {
                this._frmLoading.Close();
            }
        }

        private void LoadAutoCompleteData()
        {
            if (this.txtKeyMember.InvokeRequired || this.cbInstruments.InvokeRequired)
            {
                LoadAutoCompleteData_Del d = new LoadAutoCompleteData_Del(LoadAutoCompleteData);
                this.Invoke(d);
            }
            else
            {
                AutoCompleteStringCollection namesource = new AutoCompleteStringCollection();
                namesource.AddRange((from m in Members select m.UIName).ToArray());

                txtKeyMember.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtKeyMember.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtKeyMember.AutoCompleteCustomSource = namesource;

                txtMember.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtMember.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtMember.AutoCompleteCustomSource = namesource;

                cbInstruments.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbInstruments.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbInstruments.Items.AddRange(Instruments.ToArray());
                cbInstruments.DisplayMember = "ShortName";
                cbInstruments.ValueMember = "Isin";
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // load the members_frmLoading = new FrmLoading();
            Thread t = new Thread(delegate()
            {
                try
                {
                    Members = DataSource.GetMembers();
                    KeyMembers = DataSource.GetKeyMembers();
                    Relationships = DataSource.GetRelationships();
                    Instruments = DataSource.GetInstrumentNotes();

                    // set the autocomplete feature
                    LoadAutoCompleteData();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                CloseLoaingDialog();
            });

            t.IsBackground = true;
            t.Start();

            _frmLoading = new FrmLoading();
            _frmLoading.ShowDialog();
            t.Join();
        }

        private void btnGetConnections_Click(object sender, EventArgs e)
        {
            if(txtKeyMember.Text.Trim() == "")
                return;

            // get the key member's id
            int memId;
            if(!Int32.TryParse(txtKeyMember.Text.Split('-').Last().Trim(), out memId))
            {
                MessageBox.Show("Please select a valid member");
                return;
            }

            LoadConnections(memId);

            if (dgvConnections.Rows.Count == 0)
            {
                MessageBox.Show("Member has no connected parties");
            }
        }

        private void LoadConnections(int keyMemberNameId)
        {
            List<MemberConnection> connections = new List<MemberConnection>();

            try
            {
                connections = DataSource.GetConnections(keyMemberNameId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops! Something went wrong. \n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<MemberConnection> detailedConnections = (from c in connections
                                                          join keym in Members on c.KeyMemberNameID equals keym.NameID
                                                          join conp in Members on c.ConnectedPartyNameID equals conp.NameID
                                                          select new MemberConnection
                                                          {
                                                              ConnectedPartyNameID = c.ConnectedPartyNameID,
                                                              ConnectedPartyName = string.Format("[{0}] {1}", conp.AccountReference, conp.AccountName),
                                                              KeyMemberNameID = c.KeyMemberNameID,
                                                              KeyMemberName = string.Format("[{0}] {1}", keym.AccountReference, keym.AccountName),
                                                              Relationship = c.Relationship,
                                                              EnteredBy = c.EnteredBy,
                                                              EnteredAt = c.EnteredAt
                                                          }).ToList();            

            // load the grid
            dgvConnections.DataSource = detailedConnections;
            dgvConnections.Columns["ConnectedPartyNameID"].Visible = false;
            dgvConnections.Columns["KeyMemberNameID"].Visible = false;
            dgvConnections.Columns["ConnectedPartyName"].HeaderText = "Connected Party";
            dgvConnections.Columns["KeyMemberName"].HeaderText = "Key Member";
            dgvConnections.Columns["EnteredBy"].HeaderText = "Entered by";
            dgvConnections.Columns["EnteredAt"].HeaderText = "Entered";
            dgvConnections.Columns["EnteredAt"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";

            if (detailedConnections.Count == 0)
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
        }

        private void LoadPositions(int keyMemberAccountId)
        {
            List<MemberPosition> positions = new List<MemberPosition>();

            try
            {
                positions = DataSource.GetPositions(keyMemberAccountId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops! Something went wrong. \n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<MemberPosition> detailedPositions = (from p in positions
                                                      join keym in KeyMembers on p.KeyMemberAccountID equals keym.AccountID
                                                      //join conp in Members on c.ConnectedPartyNameID equals conp.NameID
                                                      select new MemberPosition
                                                      {
                                                          KeyMemberAccountID = p.KeyMemberAccountID,
                                                          //KeyMemberAccountRef = keym.AccountReference,
                                                          RSUMemberPosition = p.RSUMemberPosition,
                                                          KeyMemberJoinReason = p.KeyMemberJoinReason,
                                                          KeyMemberReportType = p.KeyMemberReportType,
                                                          KeyMemberTRN = p.KeyMemberTRN,
                        
                                                          //KeyMemberAccountRef = p.KeyMemberAccountRef,
                                                          KeyMemberIssuer = p.KeyMemberIssuer,
                                                          KeyMemberName = string.Format("[{0}] {1}", keym.AccountReference, keym.AccountName),
                                                          //KeyMemberName = string.Format("[{0}] {1}", p.KeyMemberAccountRef, p.KeyMemberName),
                                                          KeyMemberPosition = p.KeyMemberPosition,
                                                          KeyMemberJoinDate = p.KeyMemberJoinDate,
                                                          KeyMemberResignDate = p.KeyMemberResignDate,
                                                          EnteredBy = p.EnteredBy,
                                                          EnteredAt = p.EnteredAt

                                                      }).ToList();

            // load the grid
            dgvPositions.DataSource = detailedPositions;
            //dgvPositions.Columns["ConnectedPartyNameID"].Visible = false;
            dgvPositions.Columns["KeyMemberAccountID"].Visible = false;
            dgvPositions.Columns["KeyMemberTRN"].Visible = false;
            dgvPositions.Columns["RSUMemberPosition"].Visible = false;
            dgvPositions.Columns["KeyMemberNatRestrict"].Visible = false;
            dgvPositions.Columns["KeyMemberJoinReason"].Visible = false;
            dgvPositions.Columns["KeyMemberReportType"].Visible = false;
            //dgvPositions.Columns["KeyMemberAccountRef"].Visible = false;
            //dgvPositions.Columns["ConnectedPartyName"].HeaderText = "Connected Party";
            dgvPositions.Columns["KeyMemberName"].HeaderText = "Key Member";
            dgvPositions.Columns["KeyMemberIssuer"].HeaderText = "Issuer";
            dgvPositions.Columns["KeyMemberPosition"].HeaderText = "Member Position";
            dgvPositions.Columns["KeyMemberJoinDate"].HeaderText = "Member Appointed";
            dgvPositions.Columns["KeyMemberResignDate"].HeaderText = "Member Resigned";
            dgvPositions.Columns["EnteredBy"].HeaderText = "Entered by";
            dgvPositions.Columns["EnteredAt"].HeaderText = "Entered";
            dgvPositions.Columns["KeyMemberJoinDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dgvPositions.Columns["KeyMemberResignDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";
            dgvPositions.Columns["EnteredAt"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm";

            if (detailedPositions.Count == 0)
            {
                btnMemResign.Enabled = false;
            }
            else
            {
                btnMemResign.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvConnections.Rows.Count > 0)
            {
                MemberConnection con = (MemberConnection)dgvConnections.SelectedRows[0].DataBoundItem;
                
                string message = "You are about to delete the connection between {0} and {1}.\n\nAre you sure that you want to do this?";
                var dResult = MessageBox.Show(string.Format(message, con.KeyMemberName, con.ConnectedPartyName),
                    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dResult == DialogResult.Yes)
                {
                    try
                    {
                        DataSource.DeleteConnection(con.KeyMemberNameID, con.ConnectedPartyNameID);
                        // reload data grid
                        LoadConnections(con.KeyMemberNameID);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Oops! Something went wrong. \n\n" + ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cbInstruments_SelectedIndexChanged(object sender, EventArgs e)
        {
            // reset notes controls
            if (cbInstruments.SelectedIndex != -1)
            {
                txtNote.Text = ((InstrumentNote)cbInstruments.SelectedItem).Note;
                txtNote.Enabled = true;
                lblCurrentInstrument.Text = ((InstrumentNote)cbInstruments.SelectedItem).ShortName;
            }
            else
            {
                txtNote.Text = string.Empty;
                txtNote.Enabled = false;
                lblCurrentInstrument.Text = "";
            }
        }

        private void txtNote_TextChanged(object sender, EventArgs e)
        {
            if (cbInstruments.SelectedIndex != -1)
            {
                bool userChange = txtNote.Text != ((InstrumentNote)cbInstruments.SelectedItem).Note;
                btnSaveNote.Enabled = userChange;
                btnCancelNoteChange.Enabled = userChange;
            }
        }

        private void btnCancelNoteChange_Click(object sender, EventArgs e)
        {
            txtNote.Text = ((InstrumentNote)cbInstruments.SelectedItem).Note;
        }

        private void btnSaveNote_Click(object sender, EventArgs e)
        {
            try
            {
                var note = (InstrumentNote)cbInstruments.SelectedItem;
                DataSource.SaveInstrumentNote(((InstrumentNote)cbInstruments.SelectedItem).Isin,
                    txtNote.Text);

                ((InstrumentNote)cbInstruments.SelectedItem).Note = txtNote.Text;
                btnCancelNoteChange.Enabled = false;
                btnSaveNote.Enabled = false;
                MessageBox.Show("Your note has been saved!", "Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oops! Something went wrong.\n\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNewMember_Click(object sender, EventArgs e)
        {
            FrmNewKeyMember frm = new FrmNewKeyMember(Members, Relationships);
            frm.ShowDialog();
        }

        private void btnGetMembers_Click(object sender, EventArgs e)
        {
            if (txtMember.Text.Trim() == "")
                return;

            // get the key member's id
            int memAccId;
            if (!Int32.TryParse(txtMember.Text.Split('-').ElementAtOrDefault(1).Trim(), out memAccId))
            {
                MessageBox.Show("Please select a valid member");
                return;
            }

            LoadPositions(memAccId);

            if (dgvPositions.Rows.Count == 0)
            {
                MessageBox.Show("Client is not a Key Member");
            }

        }

        private void btnMemResign_Click(object sender, EventArgs e)
        {
            if (dgvPositions.Rows.Count > 0)
            {
                //dgvPositions.SelectedRows

                MemberPosition pos = (MemberPosition)dgvPositions.SelectedRows[0].DataBoundItem;

                //List<DataGridViewRow> rowCollection = new List<DataGridViewRow>();

                //foreach(DataGridViewCell cell in dgvPositions.SelectedCells)
                //{
                //    rowCollection.Add(dgvPositions.Rows[cell.RowIndex]);
                //}
                
                //MemberPosition memPos = new MemberPosition();
                //memPos.KeyMemberAccountID = rowCollection[0].;
                //MemberPositions = rowCollection;

                //FrmResignKeyMember frm = new FrmResignKeyMember(pos); //dgvConnections.SelectedRows[0].DataBoundItem
                //frm.ShowDialog();
                FrmEditKeyMember frm = new FrmEditKeyMember(pos); //dgvConnections.SelectedRows[0].DataBoundItem
                frm.ShowDialog();

                //MemberConnection con = (MemberConnection)dgvConnections.SelectedRows[0].DataBoundItem;

                //string message = "You are about to delete the connection between {0} and {1}.\n\nAre you sure that you want to do this?";
                //var dResult = MessageBox.Show(string.Format(message, con.KeyMemberName, con.ConnectedPartyName),
                //    "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //if (dResult == DialogResult.Yes)
                //{
                //    try
                //    {
                //        DataSource.ResignPosition(pos.KeyMemberAccountID, pos.KeyMemberIssuer, pos.KeyMemberPosition, pos.KeyMemberJoinDate, pos.KeyMemberResignDate);
                //        // reload data grid
                LoadPositions(pos.KeyMemberAccountID);
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show("Oops! Something went wrong. \n\n" + ex.Message,
                //            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //}
            }
        }

    }
}
