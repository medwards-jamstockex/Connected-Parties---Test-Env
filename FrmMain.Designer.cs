namespace ConnectedParties
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnMemResign = new System.Windows.Forms.Button();
            this.btnGetMembers = new System.Windows.Forms.Button();
            this.dgvPositions = new System.Windows.Forms.DataGridView();
            this.txtMember = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNewMember = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gbNewConnection = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnGetConnections = new System.Windows.Forms.Button();
            this.dgvConnections = new System.Windows.Forms.DataGridView();
            this.txtKeyMember = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnNewConnection = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancelNoteChange = new System.Windows.Forms.Button();
            this.btnSaveNote = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblCurrentInstrument = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbInstruments = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositions)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbNewConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConnections)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(718, 409);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(710, 380);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Key Members";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnMemResign);
            this.groupBox2.Controls.Add(this.btnGetMembers);
            this.groupBox2.Controls.Add(this.dgvPositions);
            this.groupBox2.Controls.Add(this.txtMember);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 74);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(704, 303);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Find Connections";
            // 
            // btnMemResign
            // 
            this.btnMemResign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMemResign.Enabled = false;
            this.btnMemResign.Location = new System.Drawing.Point(9, 274);
            this.btnMemResign.Name = "btnMemResign";
            this.btnMemResign.Size = new System.Drawing.Size(179, 23);
            this.btnMemResign.TabIndex = 12;
            this.btnMemResign.Text = "Edit Selected Key Member Record";
            this.btnMemResign.UseVisualStyleBackColor = true;
            this.btnMemResign.Click += new System.EventHandler(this.btnMemResign_Click);
            // 
            // btnGetMembers
            // 
            this.btnGetMembers.Location = new System.Drawing.Point(466, 42);
            this.btnGetMembers.Name = "btnGetMembers";
            this.btnGetMembers.Size = new System.Drawing.Size(117, 35);
            this.btnGetMembers.TabIndex = 11;
            this.btnGetMembers.Text = "See Key Member Tenures";
            this.btnGetMembers.UseVisualStyleBackColor = true;
            this.btnGetMembers.Click += new System.EventHandler(this.btnGetMembers_Click);
            // 
            // dgvPositions
            // 
            this.dgvPositions.AllowUserToAddRows = false;
            this.dgvPositions.AllowUserToDeleteRows = false;
            this.dgvPositions.AllowUserToResizeRows = false;
            this.dgvPositions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPositions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPositions.Location = new System.Drawing.Point(9, 83);
            this.dgvPositions.Name = "dgvPositions";
            this.dgvPositions.RowHeadersVisible = false;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvPositions.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPositions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPositions.Size = new System.Drawing.Size(689, 185);
            this.dgvPositions.TabIndex = 10;
            // 
            // txtMember
            // 
            this.txtMember.Location = new System.Drawing.Point(9, 43);
            this.txtMember.Name = "txtMember";
            this.txtMember.Size = new System.Drawing.Size(451, 20);
            this.txtMember.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(282, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Enter the Member\'s account number to view their tenure(s)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNewMember);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 71);
            this.panel1.TabIndex = 1;
            // 
            // btnNewMember
            // 
            this.btnNewMember.Location = new System.Drawing.Point(7, 32);
            this.btnNewMember.Name = "btnNewMember";
            this.btnNewMember.Size = new System.Drawing.Size(148, 28);
            this.btnNewMember.TabIndex = 1;
            this.btnNewMember.Text = "Create New Key Member";
            this.btnNewMember.UseVisualStyleBackColor = true;
            this.btnNewMember.Click += new System.EventHandler(this.btnNewMember_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Key Members";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbNewConnection);
            this.tabPage1.Controls.Add(this.pnlTop);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(710, 380);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Connected Parties";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gbNewConnection
            // 
            this.gbNewConnection.Controls.Add(this.btnDelete);
            this.gbNewConnection.Controls.Add(this.btnGetConnections);
            this.gbNewConnection.Controls.Add(this.dgvConnections);
            this.gbNewConnection.Controls.Add(this.txtKeyMember);
            this.gbNewConnection.Controls.Add(this.label2);
            this.gbNewConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbNewConnection.Location = new System.Drawing.Point(3, 74);
            this.gbNewConnection.Name = "gbNewConnection";
            this.gbNewConnection.Size = new System.Drawing.Size(704, 303);
            this.gbNewConnection.TabIndex = 1;
            this.gbNewConnection.TabStop = false;
            this.gbNewConnection.Text = "Find Connections";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(9, 274);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(179, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete Selected Connection";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnGetConnections
            // 
            this.btnGetConnections.Location = new System.Drawing.Point(466, 42);
            this.btnGetConnections.Name = "btnGetConnections";
            this.btnGetConnections.Size = new System.Drawing.Size(117, 35);
            this.btnGetConnections.TabIndex = 11;
            this.btnGetConnections.Text = "See Connections";
            this.btnGetConnections.UseVisualStyleBackColor = true;
            this.btnGetConnections.Click += new System.EventHandler(this.btnGetConnections_Click);
            // 
            // dgvConnections
            // 
            this.dgvConnections.AllowUserToAddRows = false;
            this.dgvConnections.AllowUserToDeleteRows = false;
            this.dgvConnections.AllowUserToResizeRows = false;
            this.dgvConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvConnections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConnections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConnections.Location = new System.Drawing.Point(9, 83);
            this.dgvConnections.Name = "dgvConnections";
            this.dgvConnections.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvConnections.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvConnections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConnections.Size = new System.Drawing.Size(689, 185);
            this.dgvConnections.TabIndex = 10;
            // 
            // txtKeyMember
            // 
            this.txtKeyMember.Location = new System.Drawing.Point(9, 43);
            this.txtKeyMember.Name = "txtKeyMember";
            this.txtKeyMember.Size = new System.Drawing.Size(451, 20);
            this.txtKeyMember.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(320, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Enter the Key Member\'s account number to view their connections";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.btnNewConnection);
            this.pnlTop.Controls.Add(this.label1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(3, 3);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(704, 71);
            this.pnlTop.TabIndex = 0;
            // 
            // btnNewConnection
            // 
            this.btnNewConnection.Location = new System.Drawing.Point(7, 32);
            this.btnNewConnection.Name = "btnNewConnection";
            this.btnNewConnection.Size = new System.Drawing.Size(148, 28);
            this.btnNewConnection.TabIndex = 1;
            this.btnNewConnection.Text = "Create New Connection";
            this.btnNewConnection.UseVisualStyleBackColor = true;
            this.btnNewConnection.Click += new System.EventHandler(this.btnNewConnection_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connected Parties";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.cbInstruments);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(710, 380);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Symbol Notes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancelNoteChange);
            this.groupBox1.Controls.Add(this.btnSaveNote);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.lblCurrentInstrument);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(11, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 267);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnCancelNoteChange
            // 
            this.btnCancelNoteChange.Enabled = false;
            this.btnCancelNoteChange.Location = new System.Drawing.Point(403, 232);
            this.btnCancelNoteChange.Name = "btnCancelNoteChange";
            this.btnCancelNoteChange.Size = new System.Drawing.Size(75, 23);
            this.btnCancelNoteChange.TabIndex = 4;
            this.btnCancelNoteChange.Text = "Cancel";
            this.btnCancelNoteChange.UseVisualStyleBackColor = true;
            this.btnCancelNoteChange.Click += new System.EventHandler(this.btnCancelNoteChange_Click);
            // 
            // btnSaveNote
            // 
            this.btnSaveNote.Enabled = false;
            this.btnSaveNote.Location = new System.Drawing.Point(284, 232);
            this.btnSaveNote.Name = "btnSaveNote";
            this.btnSaveNote.Size = new System.Drawing.Size(107, 23);
            this.btnSaveNote.TabIndex = 3;
            this.btnSaveNote.Text = "Save Changes";
            this.btnSaveNote.UseVisualStyleBackColor = true;
            this.btnSaveNote.Click += new System.EventHandler(this.btnSaveNote_Click);
            // 
            // txtNote
            // 
            this.txtNote.Enabled = false;
            this.txtNote.Location = new System.Drawing.Point(13, 34);
            this.txtNote.MaxLength = 1000;
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(465, 192);
            this.txtNote.TabIndex = 2;
            this.txtNote.TextChanged += new System.EventHandler(this.txtNote_TextChanged);
            // 
            // lblCurrentInstrument
            // 
            this.lblCurrentInstrument.AutoSize = true;
            this.lblCurrentInstrument.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentInstrument.Location = new System.Drawing.Point(67, 16);
            this.lblCurrentInstrument.Name = "lblCurrentInstrument";
            this.lblCurrentInstrument.Size = new System.Drawing.Size(0, 15);
            this.lblCurrentInstrument.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Note for: ";
            // 
            // cbInstruments
            // 
            this.cbInstruments.FormattingEnabled = true;
            this.cbInstruments.Location = new System.Drawing.Point(11, 51);
            this.cbInstruments.Name = "cbInstruments";
            this.cbInstruments.Size = new System.Drawing.Size(250, 21);
            this.cbInstruments.TabIndex = 3;
            this.cbInstruments.SelectedIndexChanged += new System.EventHandler(this.cbInstruments_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Please select a symbol to see the note attached to it";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Symbol Notes";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exiToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(718, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exiToolStripMenuItem
            // 
            this.exiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.exiToolStripMenuItem.Name = "exiToolStripMenuItem";
            this.exiToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exiToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 433);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connected Parties |{0} | {1} | {2} |";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositions)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.gbNewConnection.ResumeLayout(false);
            this.gbNewConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConnections)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnNewConnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gbNewConnection;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvConnections;
        private System.Windows.Forms.TextBox txtKeyMember;
        private System.Windows.Forms.Button btnGetConnections;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblCurrentInstrument;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbInstruments;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancelNoteChange;
        private System.Windows.Forms.Button btnSaveNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnMemResign;
        private System.Windows.Forms.Button btnGetMembers;
        private System.Windows.Forms.DataGridView dgvPositions;
        private System.Windows.Forms.TextBox txtMember;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnNewMember;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}