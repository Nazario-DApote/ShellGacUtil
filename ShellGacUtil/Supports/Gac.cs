using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace System.GACManagedAccess
{
    public class GacManager
    {
        /// <summary>
        /// Copy the qualified name into the clipboard
        /// </summary>
        /// <param name="fileName">The file name</param>
        public string GetFullQualifiedName(string fileName)
        {
            try
            {
                Assembly assmbly = Assembly.LoadFile(fileName);
                return assmbly.FullName;
            }
            catch ( Exception ex )
            {
                OnLog(ex.Message);
            }

            return string.Empty;
        }

        /// <summary>
        /// Displays the assembly informations
        /// </summary>
        /// <param name="fileName"></param>
        public void ShowAssemblyInfo(string m_fileName)
        {
            try
            {
                Assembly assmbly = Assembly.LoadFile(m_fileName);
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("Codebase : {0}\n", assmbly.CodeBase);
                sb.AppendFormat("EscapedCodeBase : {0}\n", assmbly.EscapedCodeBase);
                sb.AppendFormat("FullName : {0}\n", assmbly.FullName);
                sb.AppendFormat("Location : {0}\n", assmbly.Location);

                OnLog(new LogEventArgs(assmbly.FullName, sb.ToString()));
            }
            catch ( Exception ex )
            {
                OnLog( new LogEventArgs(ex) );
            }
        }

        /// <summary>
        /// Event to indicate log message
        /// </summary>
        public event EventHandler<LogEventArgs> Log;

        /// <summary>
        /// Called to signal to subscribers that log message
        /// </summary>
        /// <param name="e"></param>
        private void OnLog(LogEventArgs e)
        {
            if (Log != null)
                Log(this, e);
        }

        private void OnLog(string str, params object[] args)
        {
            if (Log != null)
                Log(this, new LogEventArgs(string.Format(str, args)));
        }

        public IEnumerator<string> GetEnumerator(string assemblyName)
        {
            AssemblyCacheEnum v = new AssemblyCacheEnum(assemblyName);
            yield return v.GetNextAssembly();
        }

        public bool IsInstalled(string assemblyName)
        {
            return !string.IsNullOrEmpty(GetAssemblyInfo(assemblyName));
        }

        public string GetAssemblyInfo(string assemblyName)
        {
            string info = null;
            try
            {
                info = AssemblyCache.QueryAssemblyInfo(assemblyName);
            }
            catch
            {
                // do nothing
            }

            return info;
        }

        private List<string> getAssembliesFromFolder(string path)
        {
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);
            List<string> assemblies = new List<string>();

            if (IsValidPath(path))
            {
                //detect whether its a directory or file
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    // Directory
                    string[] arrayFiles = Directory.GetFiles(path, "*.dll");
                    assemblies.AddRange(arrayFiles);
                }
                else
                {
                    // File
                    assemblies.Add(path);
                }
            }

            return assemblies;
        }

        public void AddAssemblyToCache(string[] assemblies)
        {
            Publish GACPublisher = new Publish();

            foreach (var a in assemblies)
            {
                if (IsValidPath(a))
                {
                    try
                    {
                        GACPublisher.GacInstall(a);
                        OnLog("Assembly {0} registered in the GAC", a);
                    }
                    catch (Exception e)
                    {
                        OnLog(new LogEventArgs(e));
                    }
                }
                else 
                    OnLog(new LogEventArgs(string.Format("Invalid file path: {0}", a), true));
            }
        }

        public void AddAssemblyToCache(string path)
        {
            if(!string.IsNullOrEmpty(path))
                AddAssemblyToCache(getAssembliesFromFolder(path).ToArray());
        }

        public void RemoveAssemblyFromCache(string[] assemblies)
        {
            Publish GACPublisher = new Publish();

            foreach (var a in assemblies)
            {
                try
                {
                    GACPublisher.GacRemove(a);
                    OnLog("Assembly {0} un-registered from the GAC", a);
                }
                catch (Exception e)
                {
                    OnLog(new LogEventArgs(e));
                }
            }
        }

        private bool IsValidPath(string fName)
        {
            bool isValid = false;
            try
            {
                isValid = File.Exists(fName) || Directory.Exists(fName);
            }
            catch
            {
                // do nothing
            }
            return isValid;
        }

        public void RemoveAssemblyFromCache(string path)
        {     
            if(!string.IsNullOrEmpty(path))
                RemoveAssemblyFromCache(getAssembliesFromFolder(path).ToArray());
        }

        public void RemoveAssemblyFromCacheBySignature(string assemblyInfo)
        {
            IEnumerator<string> e = GetEnumerator(assemblyInfo);

            while (e != null && e.MoveNext())
            {
                string info = GetAssemblyInfo(e.Current);
                if (!string.IsNullOrEmpty(info))
                    RemoveAssemblyFromCache(info);
            }
        }

    }

    public class LogEventArgs : EventArgs
    {
        string _message;
        bool _error = false;
        string _msgDetails;
        string _qualifiedName;

        public string QualifiedName
        {
            get { return _qualifiedName; }
            private set { _qualifiedName = value; }
        }

        public string MessageDetails
        {
            get { return _msgDetails; }
            private set { _msgDetails = value; }
        }

        public bool IsError
        {
            get { return _error; }
            private set { _error = value; }
        }

        public string Message
        {
            get { return _message; }
            private set { _message = value; }
        }

        public LogEventArgs(string msg, string dtls = "")
            : this(msg, msg, dtls, false)
        {
        }

        public LogEventArgs(string msg, string qName, string dtls)
            : this(msg, qName, dtls, false)
        {
        }

        public LogEventArgs(string msg, bool error)
            : this(msg, msg, string.Empty, error)
        {
        }

        public LogEventArgs(string msg, string dtls, bool error)
            : this(msg, msg, dtls, error)
        {
        }

        public LogEventArgs(Exception e)
            : this(e.Message, e.Message, string.Empty, true)
        {
        }

        public LogEventArgs(string msg, string qName, string dtls, bool error)
        {
            Message = msg;
            MessageDetails = dtls;
            QualifiedName = qName;
            IsError = error;
        }

    }
}
