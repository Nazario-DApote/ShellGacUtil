using System;
using System.GACManagedAccess;
using System.Text;

namespace ShellGacUtil.Supports
{
    internal class LogCollector : IDisposable
    {
        private StringBuilder _Message;

        public string Message
        {
            get { return _Message.ToString(); }
        }

        private StringBuilder _QualifiedName;

        public string QualifiedName
        {
            get { return _QualifiedName.ToString(); }
        }

        private StringBuilder _MessageDetails;

        public string MessageDetails
        {
            get { return _MessageDetails.ToString(); }
        }

        bool _Error = false;

        public bool Error
        {
            get { return _Error; }
        }

        public LogCollector()
        {
            _Message = new StringBuilder();
            _QualifiedName = new StringBuilder();
            _MessageDetails = new StringBuilder();
        }

        public void Collector(object sender, LogEventArgs e)
        {
            _Error = _Error & e.IsError;
            _Message.AppendLine(e.Message);
            _QualifiedName.AppendLine(e.QualifiedName);
            _MessageDetails.AppendLine(e.MessageDetails);
        }

        public LogEventArgs MakeLogEventArgs()
        {
            return new LogEventArgs(_Message.ToString(), _QualifiedName.ToString(), _MessageDetails.ToString(), _Error);
        }

        #region IDisposable Members

        private bool disposed = false;

        //Implement IDisposable.
        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if ( !disposed )
            {
                if ( disposing )
                {
                    // Free other state (managed objects).
                    //someone want the deterministic release of all resources
                    //Let us release all the managed resources
                    ReleaseManagedResources();
                }
                else
                {
                    // Do nothing, no one asked a dispose, the object went out of
                    // scope and finalized is called so lets next round of GC 
                    // release these resources
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                disposed = true;

                // Release the unmanaged resource in any case as they will not be 
                // released by GC
                //ReleaseUnmangedResources();
            }
        }

        // Use C# destructor syntax for finalization code.
        ~LogCollector()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        //private void ReleaseUnmangedResources()
        //{
        //}

        private void ReleaseManagedResources()
        {
            _Message.Clear();
            _Message = null;
            _MessageDetails.Clear();
            _MessageDetails = null;
            _QualifiedName.Clear();
            _QualifiedName = null;
        }
        #endregion
    }
}
