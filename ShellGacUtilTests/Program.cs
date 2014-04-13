using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ShellGacUtil;

namespace ShellGacUtilTests
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var msgDlg = new MessageDialog();
            msgDlg.EnableClipboardCopy = true;
            msgDlg.QualifiedName = "QualifiedName";
            msgDlg.MessageText = "MessageText";
            msgDlg.MessageDetails = "MessageDetails";

            Application.Run(msgDlg);
        }
    }
}
