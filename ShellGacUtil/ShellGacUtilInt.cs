using System;
using System.Diagnostics;
using System.GACManagedAccess;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AssemblyRegUtil;
using AssemblyRegUtil.Supports;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;

namespace ShellGacUtil
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.ClassOfExtension, ".dll")]
    public class ShellGacUtilInt : SharpContextMenu
    {
        protected override bool CanShowMenu()
        {
            //  We always show the menu.
            return true;
        }

        protected override System.Windows.Forms.ContextMenuStrip CreateMenu()
        {
            //  Create the menu strip.
            var menu = new ContextMenuStrip();

            //  Create a 'count lines' item.
            var register = new ToolStripMenuItem
            {
                Text = "Re&gister",
                //Image = Properties.Resources.CountLines
            };

            var unregister = new ToolStripMenuItem
            {
                Text = "&Unregister",
            };

            var showInfo = new ToolStripMenuItem
            {
                Text = "Show &Assembly Info",

            };

            var copyName = new ToolStripMenuItem
            {
                Text = "Copy &Qualified Name",
            };

            //  When we click, we'll count the lines.
            register.Click += (sender, args) => Register();
            unregister.Click += (sender, args) => Unregister();
            showInfo.Click += (sender, args) => ShowAssemblyInfo();
            copyName.Click += (sender, args) => CopyQualifiedName();

            //  Add the item to the context menu.
            menu.Items.Add(register);
            menu.Items.Add(unregister);
            menu.Items.Add(showInfo);
            menu.Items.Add(copyName);

            //  Return the menu.
            return menu;
        }

        /// <summary>
        /// Copy the qualified name into the clipboard
        /// </summary>
        private void CopyQualifiedName()
        {
            GacManager gm = new GacManager();
            gm.Log += (sender, e) => FileLog(sender, e);

            foreach ( string fileName in SelectedItemPaths )
            {
                gm.CopyFullQualifiedName(fileName);
            }

            gm.Log -= (sender, e) => FileLog(sender, e);
        }

        /// <summary>
        /// Display the assembly informations
        /// </summary>
        private void ShowAssemblyInfo()
        {
            GacManager gm = new GacManager();
            gm.Log += (sender, e) => ShowLog(sender, e);

            foreach ( string fileName in SelectedItemPaths )
            {
                gm.ShowAssemblyInfo(fileName);
            }

            gm.Log -= (sender, e) => ShowLog(sender, e);
        }

        /// <summary>
        /// Register the assembly into the GAC
        /// </summary>
        private void Register()
        {
            GacManager gm = new GacManager();
            gm.Log += (sender, e) => RegisterLog(sender, e);

            foreach (string fileName in SelectedItemPaths)
            {
                gm.AddAssemblyToCache(fileName);
            }

            gm.Log -= (sender, e) => RegisterLog(sender, e);
        }

        /// <summary>
        ///     Perform Unregistrations
        /// </summary>
        private void Unregister()
        {
            GacManager gm = new GacManager();
            gm.Log += (sender, e) => UnRegisterLog(sender, e);

            foreach ( string fileName in SelectedItemPaths )
            {
                gm.RemoveAssemblyFromCache(fileName);
            }

            gm.Log -= (sender, e) => UnRegisterLog(sender, e);
        }

        private static void ShowLog(object sender, LogEventArgs args)
        {
            MessageDialog msgDialog = new MessageDialog();
            msgDialog.MessageText = args.Message;
            msgDialog.QualifiedName = args.Message;
            msgDialog.MessageDetails = args.MessageDetails;
            msgDialog.EnableClipboardCopy = true;
            msgDialog.ShowDialog();
        }

        private static void RegisterLog(object sender, LogEventArgs args)
        {
            MessageDialog msgDialog = new MessageDialog();

            if ( !args.IsError )
            {   // if the success contains into the message then its okay 
                msgDialog.MessageText = "Successfully added to the Global Assembly Cache.";
            }
            else
            {   // failure
                msgDialog.MessageText = "Failed to register the assembly.";
            }

            msgDialog.MessageDetails = args.Message;
            msgDialog.ShowDialog();
        }

        private static void UnRegisterLog(object sender, LogEventArgs args)
        {
            MessageDialog msgDialog = new MessageDialog();

            if ( !args.IsError )
            {   // if the success contains into the message then its okay 
                msgDialog.MessageText = "Successfully removed from the Global Assembly Cache.";
            }
            else
            {   // failure
                msgDialog.MessageText = "Failed to unregister the assembly.";
            }

            msgDialog.MessageDetails = args.Message;
            msgDialog.ShowDialog();
        }

        private static void FileLog(object sender, LogEventArgs args)
        {
#if DEBUG
            Logger.WriteLog(args.Message);
#endif
        }
    }
}
