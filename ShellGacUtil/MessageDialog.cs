using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShellGacUtil
{
    public partial class MessageDialog : Form
    {
        private Size initSize; 

        public MessageDialog()
        {
            InitializeComponent();
            initSize = new Size(298, 147);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.SuspendLayout();
            if (lblDetails.Visible)
            {
                this.Size = initSize;
                lblDetails.Visible = false;
            }
            else
            {
                this.Size = new Size(298, 301);
                lblDetails.Visible = true;
            }
            this.ResumeLayout();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageDialog_Load(object sender, EventArgs e)
        {
            this.Size = initSize;
            lblDetails.Visible = false;
        }

        /// <summary>
        ///     Gets or sets the message text
        /// </summary>
        public string MessageText
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }


        /// <summary>
        ///     Gets or sets the message details
        /// </summary>
        public string MessageDetails
        {
            get { return lblDetails.Text; }
            set { lblDetails.Text = value; }
        }

        private string qualifiedName;

        /// <summary>
        ///     Gets or sets the qualified name
        /// </summary>
        public string QualifiedName
        {
            get { return qualifiedName; }
            set { qualifiedName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableClipboardCopy
        {
            get { return cmdCopy.Visible; }
            set { cmdCopy.Visible = value; }
        }
	

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(qualifiedName, true);
        }	
    }
}