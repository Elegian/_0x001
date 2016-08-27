using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace _0x001.Dev.sanderele1
{
    public class ShaderProgram : IDisposable
    {
        private int shaderProgramID;
        public int ShaderProgramID
        {
            get
            {
                if (!(shaderProgramID > 0))
                    shaderProgramID = GL.CreateProgram();

                return shaderProgramID;
            }
        }

        public List<Shader> Shaders = new List<Shader>();

        public bool IsBound { get; protected set; }

        /// <summary>
        /// Will attempt to bind this shaderprogram.
        /// NOTE: This will not bind again if it is already bound without calling <see cref="Unbind"/> unless the argument "force" is true.  
        /// </summary>
        /// <param name="force">Should the shaderprogram be forced to rebind?</param>
        public void Bind(bool force = false)
        {
            if (force)
            {
                GL.UseProgram(ShaderProgramID);
                IsBound = true;
                return;
            }

            if (!IsBound)
            {
                GL.UseProgram(ShaderProgramID);
                IsBound = true;
            }
        }

        public void Unbind()
        {
            if (IsBound)
            {
                GL.UseProgram(0);
                IsBound = false;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Shaders != null)
                    {
                        foreach (Shader shader in Shaders)
                            shader.Dispose();

                        Shaders = null;
                    }
                }

                //Avoid creating a new shader program, so we use shaderProgramID instead of ShaderProgramID
                if (shaderProgramID > 0)
                    GL.DeleteProgram(shaderProgramID);

                disposedValue = true;
            }
        }

         ~ShaderProgram()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
