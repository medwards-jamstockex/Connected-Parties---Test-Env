﻿namespace ConnectedParties
{
    partial class FrmNewKeyMember
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMembership = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtKeyMember = new System.Windows.Forms.TextBox();
            this.cbIssuer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbJoinReason = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dpMemberJoin = new System.Windows.Forms.DateTimePicker();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key Member (type the account number)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Issuer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 170);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Membership type";
            // 
            // cbMembership
            // 
            this.cbMembership.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMembership.FormattingEnabled = true;
            this.cbMembership.Location = new System.Drawing.Point(16, 190);
            this.cbMembership.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbMembership.Name = "cbMembership";
            this.cbMembership.Size = new System.Drawing.Size(600, 24);
            this.cbMembership.TabIndex = 5;
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Location = new System.Drawing.Point(321, 432);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(176, 28);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Add Member";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(505, 432);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtKeyMember
            // 
            this.txtKeyMember.Location = new System.Drawing.Point(16, 32);
            this.txtKeyMember.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKeyMember.Name = "txtKeyMember";
            this.txtKeyMember.Size = new System.Drawing.Size(600, 22);
            this.txtKeyMember.TabIndex = 8;
            // 
            // cbIssuer
            // 
            this.cbIssuer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIssuer.FormattingEnabled = true;
            this.cbIssuer.Location = new System.Drawing.Point(16, 85);
            this.cbIssuer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbIssuer.Name = "cbIssuer";
            this.cbIssuer.Size = new System.Drawing.Size(600, 24);
            this.cbIssuer.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 223);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Member Nationality Restriction";
            // 
            // cbJoinReason
            // 
            this.cbJoinReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbJoinReason.FormattingEnabled = true;
            this.cbJoinReason.Location = new System.Drawing.Point(16, 293);
            this.cbJoinReason.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbJoinReason.Name = "cbJoinReason";
            this.cbJoinReason.Size = new System.Drawing.Size(600, 24);
            this.cbJoinReason.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 273);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Join Reason Code";
            // 
            // cbReportType
            // 
            this.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Location = new System.Drawing.Point(16, 345);
            this.cbReportType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(600, 24);
            this.cbReportType.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 325);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(145, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Report Type Indicator";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 379);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Membership Join Date";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 242);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(18, 17);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // dpMemberJoin
            // 
            this.dpMemberJoin.Location = new System.Drawing.Point(16, 399);
            this.dpMemberJoin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dpMemberJoin.Name = "dpMemberJoin";
            this.dpMemberJoin.Size = new System.Drawing.Size(265, 22);
            this.dpMemberJoin.TabIndex = 19;
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(19, 139);
            this.txtPosition.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(600, 22);
            this.txtPosition.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 119);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "Position";
            // 
            // FrmNewKeyMember
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(635, 474);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dpMemberJoin);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbReportType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbJoinReason);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbIssuer);
            this.Controls.Add(this.txtKeyMember);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.cbMembership);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNewKeyMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Member";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMembership;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtKeyMember;
        private System.Windows.Forms.ComboBox cbIssuer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbJoinReason;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DateTimePicker dpMemberJoin;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label label8;
    }
}