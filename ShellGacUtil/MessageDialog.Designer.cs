namespace ShellGacUtil
{
    partial class MessageDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageDialog));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cmdOk = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lnkDetails = new System.Windows.Forms.LinkLabel();
            this.lblDetails = new System.Windows.Forms.Label();
            this.cmdCopy = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 254);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(294, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdOk.Location = new System.Drawing.Point(226, 228);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(56, 23);
            this.cmdOk.TabIndex = 1;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(12, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(268, 57);
            this.lblMessage.TabIndex = 2;
            // 
            // lnkDetails
            // 
            this.lnkDetails.AutoSize = true;
            this.lnkDetails.Location = new System.Drawing.Point(12, 66);
            this.lnkDetails.Name = "lnkDetails";
            this.lnkDetails.Size = new System.Drawing.Size(39, 13);
            this.lnkDetails.TabIndex = 3;
            this.lnkDetails.TabStop = true;
            this.lnkDetails.Text = "Details";
            this.lnkDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDetails_LinkClicked);
            // 
            // lblDetails
            // 
            this.lblDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDetails.Location = new System.Drawing.Point(12, 80);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(270, 143);
            this.lblDetails.TabIndex = 4;
            // 
            // cmdCopy
            // 
            this.cmdCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCopy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmdCopy.Image")));
            this.cmdCopy.Location = new System.Drawing.Point(253, 43);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(29, 23);
            this.cmdCopy.TabIndex = 5;
            this.toolTip1.SetToolTip(this.cmdCopy, "Copy Assembly Qualified Name into Clipboard");
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Visible = false;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // MessageDialog
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdOk;
            this.ClientSize = new System.Drawing.Size(294, 276);
            this.Controls.Add(this.cmdCopy);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.lnkDetails);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(310, 163);
            this.Name = "MessageDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GAC Utility";
            this.Load += new System.EventHandler(this.MessageDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.LinkLabel lnkDetails;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.Button cmdCopy;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}