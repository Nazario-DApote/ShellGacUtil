using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AssemblyRegUtil.Supports
{
    /// <summary>
    ///     Contains utility methods to accomplish operations in GAC (aka, Global Assembly Cache)
    /// </summary>
    public class GacManager
    {
        /// <summary>
        ///     Registers the assembly into the Global Assembly Cache
        /// </summary>
        /// <param name="m_fileName">The File name</param>
        public static void RegisterAssembly(string m_fileName)
        {
            string result = string.Empty;
            try
            {   // register the assembly
                result = RegisterAssemblyCode(m_fileName);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            MessageDialog msgDialog = new MessageDialog();

            if (result.ToLower().Contains("success"))
            {   // if the success contains into the message then its okay 
                msgDialog.MessageText = "Successfully added to the Global Assembly Cache.";
            }
            else
            {   // failureMessageDetails
                msgDialog.MessageText = "Failed to register the assembly.";
            }

            msgDialog. = result;
            msgDialog.ShowDialog();
        }

        /// <summary>
        ///     Unregisters an assembly from the GAC
        /// </summary>
        /// <param name="m_fileName"></param>
        public static void UnregisterAssembly(string m_fileName)
        {
            Logger.WriteLog("Inside Unregister()");
            string result = string.Empty;
            try
            {
                result = UnregisterCore(m_fileName);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            MessageDialog msgDialog = new MessageDialog();

            if (result.ToLower().Contains("uninstalled = 1"))
            {
                msgDialog.MessageText = "Successfully removed from the Global Assembly Cache.";
            }
            else
            {
                msgDialog.MessageText = "Failed to unregister the assembly.";
            }

            msgDialog.MessageDetails = result;
            msgDialog.ShowDialog();
        }

        /// <summary>
        ///     Copy the qualified name into the clipboard
        /// </summary>
        /// <param name="fileName">The file name</param>
        public static void CopyFullQualifiedName(string fileName)
        {
            try
            {
                Assembly assmbly = Assembly.LoadFile(fileName);
                Clipboard.SetDataObject(assmbly.FullName, true);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex.Message);
            }
        }

        /// <summary>
        ///     Displays the assembly informations
        /// </summary>
        /// <param name="fileName"></param>
        public static void ShowAssemblyInfo(string m_fileName)
        {
            string message = string.Empty;
            string messageDetails = string.Empty;
            try
            {
                Assembly assmbly = Assembly.LoadFile(m_fileName);

                message = assmbly.FullName;
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("Codebase : {0}\n", assmbly.CodeBase);
                sb.AppendFormat("EscapedCodeBase : {0}\n", assmbly.EscapedCodeBase);
                sb.AppendFormat("FullName : {0}\n", assmbly.FullName);
                sb.AppendFormat("Location : {0}\n", assmbly.Location);

                messageDetails = sb.ToString();
            }
            catch (Exception ex)
            {
                message = "Failed to read assembly info.";
                messageDetails = ex.Message;
            }

            MessageDialog msgDlg = new MessageDialog();
            msgDlg.MessageText = message;
            msgDlg.MessageDetails = messageDetails;
            msgDlg.QualifiedName = message;
            msgDlg.EnableClipboardCopy = true;
            msgDlg.ShowDialog();
        }

        /// <summary>
        ///     Performs the actual registration
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <returns>String representation of the operation result</returns>
        private static string RegisterAssemblyCode(string fileName)
        {
            Logger.WriteLog("Registering .. " + fileName);
            string envName = "VS100COMNTOOLS"; // For VS 2003 We need to use "VS71COMNTOOLS". 
            string toolPath = Environment.GetEnvironmentVariable(envName);
            string vsCmdLinePath = System.IO.Path.Combine(toolPath, "vsvars32.bat");

            Process process = new Process();

            using (System.IO.StreamReader reader = new System.IO.StreamReader(vsCmdLinePath))
            {
                string value = null;
                while (null != (value = reader.ReadLine()))
                {
                    if (value.IndexOf("FrameworkSDKDir") != -1)
                    {
                        string sdkPath = value.Substring(value.IndexOf("=") + 1).Trim();
                        string gacutilPath = System.IO.Path.Combine(sdkPath, @"bin\gacutil.exe");
                        string cmdLineArgument = " -i \"" + fileName + "\"";
                        Logger.WriteLog("Argument : " + cmdLineArgument);
                        process.StartInfo = new ProcessStartInfo(gacutilPath, cmdLineArgument);
                        break;
                    }
                }
            }
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            Logger.WriteLog("Output was : " + output);
            return output;
        }


        /// <summary>
        ///     Performs actual unregistration process
        /// </summary>
        /// <param name="fileName">File name of the assembly</param>
        /// <returns>String representation of the operation result</returns>
        private static string UnregisterCore(string fileName)
        {
            Logger.WriteLog("Unregistering .. " + fileName);
            string envName = "VS100COMNTOOLS"; // For VS 2003 We need to use "VS71COMNTOOLS". 
            string toolPath = Environment.GetEnvironmentVariable(envName);
            string vsCmdLinePath = System.IO.Path.Combine(toolPath, "vsvars32.bat");

            Process process = new Process();

            using (System.IO.StreamReader reader = new System.IO.StreamReader(vsCmdLinePath))
            {
                string value = null;
                while (null != (value = reader.ReadLine()))
                {
                    if (value.IndexOf("FrameworkSDKDir") != -1)
                    {
                        string sdkPath = value.Substring(value.IndexOf("=") + 1).Trim();
                        string gacutilPath = System.IO.Path.Combine(sdkPath, @"bin\gacutil.exe");
                        string asmName = Path.GetFileName(fileName);
                        if (asmName.ToLower().EndsWith(".dll"))
                            asmName = asmName.Substring(0, asmName.ToLower().LastIndexOf(".dll"));
                        string cmdLineArgument = " -u " + asmName;
                        Logger.WriteLog("Argument : " + cmdLineArgument);
                        process.StartInfo = new ProcessStartInfo(gacutilPath, cmdLineArgument);
                        break;
                    }
                }
            }
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.Start();

            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            Logger.WriteLog("Output was : " + output);
            return output;
        }
    }
}


