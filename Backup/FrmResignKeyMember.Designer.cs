namespace ConnectedParties
{
    partial class FrmResignKeyMember
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
            this.btnResign = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtKeyMember = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dpMemberJoin = new System.Windows.Forms.DateTimePicker();
            this.txtIssuer = new System.Windows.Forms.TextBox();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.dpMemberResign = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAccountID = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Key Member (account number)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Issuer";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Membership type";
            // 
            // btnResign
            // 
            this.btnResign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResign.Location = new System.Drawing.Point(241, 230);
            this.btnResign.Name = "btnResign";
            this.btnResign.Size = new System.Drawing.Size(132, 23);
            this.btnResign.TabIndex = 6;
            this.btnResign.Text = "Member Resign";
            this.btnResign.UseVisualStyleBackColor = true;
            this.btnResign.Click += new System.EventHandler(this.btnResign_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(379, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtKeyMember
            // 
            this.txtKeyMember.Location = new System.Drawing.Point(12, 26);
            this.txtKeyMember.Name = "txtKeyMember";
            this.txtKeyMember.Size = new System.Drawing.Size(451, 20);
            this.txtKeyMember.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Membership Join Date";
            // 
            // dpMemberJoin
            // 
            this.dpMemberJoin.Location = new System.Drawing.Point(12, 157);
            this.dpMemberJoin.Name = "dpMemberJoin";
            this.dpMemberJoin.Size = new System.Drawing.Size(200, 20);
            this.dpMemberJoin.TabIndex = 19;
            // 
            // txtIssuer
            // 
            this.txtIssuer.Location = new System.Drawing.Point(12, 69);
            this.txtIssuer.Name = "txtIssuer";
            this.txtIssuer.Size = new System.Drawing.Size(451, 20);
            this.txtIssuer.TabIndex = 20;
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(12, 112);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(451, 20);
            this.txtPosition.TabIndex = 21;
            // 
            // dpMemberResign
            // 
            this.dpMemberResign.Location = new System.Drawing.Point(12, 199);
            this.dpMemberResign.Name = "dpMemberResign";
            this.dpMemberResign.Size = new System.Drawing.Size(200, 20);
            this.dpMemberResign.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Membership Resign Date";
            // 
            // txtAccountID
            // 
            this.txtAccountID.Location = new System.Drawing.Point(241, 157);
            this.txtAccountID.Name = "txtAccountID";
            this.txtAccountID.Size = new System.Drawing.Size(198, 20);
            this.txtAccountID.TabIndex = 24;
            this.txtAccountID.Visible = false;
            // 
            // FrmResignKeyMember
            // 
            this.AcceptButton = this.btnResign;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(476, 259);
            this.Controls.Add(this.txtAccountID);
            this.Controls.Add(this.dpMemberResign);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.txtIssuer);
            this.Controls.Add(this.dpMemberJoin);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtKeyMember);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnResign);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmResignKeyMember";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Member Resigning";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnResign;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtKeyMember;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dpMemberJoin;
        private System.Windows.Forms.TextBox txtIssuer;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.DateTimePicker dpMemberResign;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAccountID;
    }
}