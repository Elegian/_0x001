using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x001.Dev.sanderele1
{
    public abstract class OpenGLControl : IDisposable
    {
        OpenGLWindow parentWindow;
        public OpenGLWindow ParentWindow { get { return parentWindow; } }

        OpenGLControl parent;
        public OpenGLControl Parent { get { return parent; } }

        /// <summary>
        /// Should only be called internally when adding this control as a child control to either a <see cref="OpenGLWindow"/> or a <see cref="OpenGLControl"/>  
        /// </summary>
        /// <param name="parentWindow">The parent window</param>
        /// <param name="parentControl">The parent control</param>
        internal void Initialize(OpenGLWindow parentWindow, OpenGLControl parentControl)
        {
            this.parentWindow = parentWindow;
            this.parent = parentControl;
        }

        /// <summary>
        /// Initialize your OpenGL resources here.
        /// This should only ever be called once.
        /// </summary>
        public abstract void InitializeGL();
        /// <summary>
        /// Clean up your OpenGL resources here
        /// </summary>
        public abstract void CleanupGL();

        #region IDisposable Support
        protected bool IsDisposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                CleanupGL();

                IsDisposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // OpenGL counts as unmanaged resources right? We don't want that shit taking up memory!
        ~OpenGLControl()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
