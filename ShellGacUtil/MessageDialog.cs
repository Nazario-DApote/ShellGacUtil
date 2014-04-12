

#region Copyright notice
/*
 *      This is a as-is implementation and feel free to use the code (certainly with necessary modifications)
 * to fulfil any custom requrements.
 *      It would be well appreciated if you just keep the copyright notice intact. 
 * 
 *                                                                              Moim Hossain
 *                                                                              Sr. Software Engineer
 *                                                                              Orion Informatics.
 */
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
#endregion

namespace AssemblyRegUtil
{
    public partial class MessageDialog : Form
    {
        public MessageDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lblDetails.Visible)
            {
                this.Size = new Size(298, 147);
                lblDetails.Visible = false;
            }
            else
            {
                this.Size = new Size(298, 301);
                lblDetails.Visible = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageDialog_Load(object sender, EventArgs e)
        {
            this.Size = new Size(298, 147);
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