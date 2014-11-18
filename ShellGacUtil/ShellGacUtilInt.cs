using System;
using System.GACManagedAccess;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using ShellGacUtil.Supports;
using System.Diagnostics;

namespace ShellGacUtil
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.ClassOfExtension, ".dll")]
    [COMServerAssociation(AssociationType.ClassOfExtension, ".exe")]
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

            //  Create a drop-down item.
            ToolStripMenuItem itemDropdown = new ToolStripMenuItem
            {
                Text = "&GAC Utilities",
                Image = Properties.Resource.MainIcon.ToBitmap(),
            };

            //  Add the submenu to the context menu.
            menu.Items.Add(itemDropdown);

            var register = new ToolStripMenuItem
            {
                Text = "Re&gister",
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
            register.Click += (s, a) => Register();
            unregister.Click += (s, a) => Unregister();
            showInfo.Click += (s, a) => ShowAssemblyInfo();
            copyName.Click += (s, a) => CopyQualifiedName();

            //  Add the item to the context menu.
            itemDropdown.DropDownItems.Add(register);
            itemDropdown.DropDownItems.Add(unregister);
            itemDropdown.DropDownItems.Add(showInfo);
            itemDropdown.DropDownItems.Add(copyName);

            //  Return the menu.
            return menu;
        }

        private static void ShowLog(object sender, LogEventArgs args)
        {
            MessageDialog msgDialog = new MessageDialog();
            msgDialog.MessageText = args.Message;
            msgDialog.QualifiedName = args.QualifiedName;
            msgDialog.MessageDetails = args.MessageDetails;
            msgDialog.EnableClipboardCopy = ( !string.IsNullOrEmpty(msgDialog.QualifiedName) );
            msgDialog.ShowDialog();
        }

        /// <summary>
        /// Copy the qualified name into the clipboard
        /// </summary>
        private void CopyQualifiedName()
        {
#if DEBUG
            if ( !Debugger.IsAttached )
                Debugger.Launch();
#endif
            var log = new LogCollector();
            GacManager gm = new GacManager();
            gm.Log += log.Collector;

            StringBuilder qlgNames = new StringBuilder();
            foreach ( string fileName in SelectedItemPaths )
            {
                var aName = gm.GetFullQualifiedName(fileName);
                if( !string.IsNullOrEmpty(aName) )
                    qlgNames.AppendLine(aName);
            }

            gm.Log -= log.Collector;

            Clipboard.SetDataObject(qlgNames.ToString(), true);
            if ( log.Error )
                ShowLog(this, log.MakeLogEventArgs()); 
        }
        
        /// <summary>
        /// Register the assembly into the GAC
        /// </summary>
        private void Register()
        {
#if DEBUG
            if ( !Debugger.IsAttached )
                Debugger.Launch();
#endif
            var log = new LogCollector();
            GacManager gm = new GacManager();
            gm.Log += log.Collector;

            foreach ( string fileName in SelectedItemPaths )
            {     
		        gm.AddAssemblyToCache(fileName);
            }

            gm.Log -= log.Collector;

            string msg = null;
            if ( !log.Error )
                msg = "Successfully added to the Global Assembly Cache.";
            else
                msg = "Failed to register the assembly.";

            ShowLog(this, new LogEventArgs(msg, log.Message, log.Message, log.Error)); 
        }

        /// <summary>
        /// Display the assembly informations
        /// </summary>
        private void ShowAssemblyInfo()
        {
#if DEBUG
            if ( !Debugger.IsAttached )
                Debugger.Launch();
#endif
            var log = new LogCollector();
            GacManager gm = new GacManager();
            gm.Log += log.Collector;

            foreach ( string fileName in SelectedItemPaths )
            {
                gm.ShowAssemblyInfo(fileName);
            }

            gm.Log -= log.Collector;

            ShowLog(this, log.MakeLogEventArgs()); 
        }

        /// <summary>
        /// Perform Unregistrations
        /// </summary>
        private void Unregister()
        {
#if DEBUG
            if ( !Debugger.IsAttached )
                Debugger.Launch();
#endif
            var log = new LogCollector();
            GacManager gm = new GacManager();
            gm.Log += log.Collector;

            foreach ( string fileName in SelectedItemPaths )
            {
                gm.RemoveAssemblyFromCache(fileName);
            }

            gm.Log -= log.Collector;

            string msg = null;
            if ( !log.Error )
                msg = "Successfully removed from the Global Assembly Cache.";
            else
                msg = "Failed to unregister the assembly.";

            ShowLog(this, new LogEventArgs(msg, log.Message, log.Message, log.Error)); 
        }
    }
}
