using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace _0x001.Dev.sanderele1
{
    /// <summary>
    /// Types of shaders, is a bitfield.
    /// The reason this exists, and we don't use <see cref="OpenTK.Graphics.OpenGL.ShaderType"/> is that this supports bitfields 
    /// </summary>
    public enum ShaderType : byte
    {
        Vertex = 1,
        Fragment = 2
    }

    /// <summary>
    /// Represents a OpenGL shader.
    /// NOTE: Disposing of this will NOT delete <see cref="ShaderProgram"/>. You MUST delete <see cref="ShaderProgram"/> manually whenever you are finished with it. 
    /// </summary>
    public class Shader : IDisposable
    {

        public string SourcePath { get; set; }
        public ShaderProgram ShaderProgram { get; protected set; }
        public int ShaderID { get; protected set; } = -1;

        public ShaderType Type { get; protected set; }

        public Shader(ShaderType type, ShaderProgram shaderProgram)
        {
            if (shaderProgram == null)
                throw new ArgumentNullException("shaderProgram");
            Type = type;
            ShaderProgram = shaderProgram;
            ShaderProgram.Shaders.Add(this);
           
        }

        public Shader(string sourcePath, ShaderType type, ShaderProgram shaderProgram)
        {
            if (shaderProgram == null)
                throw new ArgumentNullException("shaderProgram");
            SourcePath = sourcePath;
            Type = type;
            ShaderProgram = shaderProgram;
            ShaderProgram.Shaders.Add(this);
        }

        /// <summary>
        /// Will attempt to compile the shader from the path specified in <see cref="SourcePath"/>
        /// <!--NOTE: If <see cref="ShaderProgram"/> is not a valid shader program id, one will be created. (Not true anymore)-->
        /// NOTE: If <see cref="SourcePath"/> is an ivalid file path, this method will return false. 
        /// </summary>
        /// <returns>Returns true if the compilation succeeded, returns false if reading from source, compiling or linking failed!</returns>
        public bool Compile()
        {
            if (!File.Exists(SourcePath))
                return false;

            ////Not needed as the shader program will automatically create an id
            ////Create shader program if none exists.
            //if (!(ShaderProgram.ShaderProgramID > 0))
            //{
            //    Console.WriteLine("Shader::Compile() Could not compile as the supplied shaderprogram contained an invalid id: " + ShaderProgram.ShaderProgramID);
            //    return false;
            //}

            if (!(ShaderID > 0))
                ShaderID = Shader.CreateShaderID(Type);

            using (StreamReader strr = new StreamReader(SourcePath))
            {
                GL.ShaderSource(ShaderID, strr.ReadToEnd());
            }

            GL.CompileShader(ShaderID);

            int shaderCompileCode;
            GL.GetShader(ShaderID, ShaderParameter.CompileStatus, out shaderCompileCode);
            if (shaderCompileCode != 1)
            {
                string compileLog = GL.GetShaderInfoLog(ShaderID);

                Console.WriteLine("Shader::Compile() Failed to compile shader with id '" + ShaderID + "', and source: '" + SourcePath + "'");
                Console.WriteLine("Compile Error: " + compileLog);
                return false;
            }

            GL.AttachShader(ShaderProgram.ShaderProgramID, ShaderID);
            GL.LinkProgram(ShaderProgram.ShaderProgramID);

            int programLinkStatus;
            GL.GetProgram(ShaderProgram.ShaderProgramID, GetProgramParameterName.LinkStatus, out programLinkStatus);
            if (programLinkStatus != 1)
            {
                string linkLog = GL.GetProgramInfoLog(ShaderProgram.ShaderProgramID);
                Console.WriteLine("Shader::Compile() Failed to link shader with id '" + ShaderID + "' to program with id '" + ShaderID + "'. Shader source: '" + SourcePath + "'");
                Console.WriteLine("Link Log: " + linkLog);
                return false;
            }
            GL.DeleteShader(ShaderID);
            return true;
        }

        /// <summary>
        /// Tries to create a shader id from the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int CreateShaderID(ShaderType type)
        {
            switch (type)
            {
                case ShaderType.Fragment:
                     return GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.FragmentShader);
                case ShaderType.Vertex:
                    return GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.VertexShader);
                default:
                    return -1;
            }
        }

        #region Set Uniform functions
        public bool SetUniform(string uniformName, float value)
        {
            ShaderProgram.Bind();
            int uniform = GL.GetUniformLocation(ShaderProgram.ShaderProgramID, uniformName);
            if (uniform < 0)
                return false;

            GL.Uniform1(uniform, value);
            ShaderProgram.Unbind();
            return true;
        }

        public bool SetUniform(string uniformName, float[] value)
        {
            ShaderProgram.Bind();
            int uniform = GL.GetUniformLocation(ShaderProgram.ShaderProgramID, uniformName);
            if (uniform < 0)
                return false;

            GL.Uniform1(uniform, value.Length, value);
            ShaderProgram.Unbind();
            return true;
        }

        public bool SetUniform(string uniformName, int value)
        {
            ShaderProgram.Bind();
            int uniform = GL.GetUniformLocation(ShaderProgram.ShaderProgramID, uniformName);
            if (uniform < 0)
                return false;

            GL.Uniform1(uniform, value);
            ShaderProgram.Unbind();
            return true;
        }

        public bool SetUniform(string uniformName, int[] value)
        {
            ShaderProgram.Bind();
            int uniform = GL.GetUniformLocation(ShaderProgram.ShaderProgramID, uniformName);
            if (uniform < 0)
                return false;

            GL.Uniform1(uniform, value.Length, value);
            ShaderProgram.Unbind();
            return true;
        }

        public bool SetUniform(string uniformName, Vector2 value)
        {
            ShaderProgram.Bind();
            int uniform = GL.GetUniformLocation(ShaderProgram.ShaderProgramID, uniformName);
            if (uniform < 0)
                return false;

            GL.Uniform2(uniform, value);
            ShaderProgram.Unbind();
            return true;
        }

        public bool SetUniform(string uniformName, Vector3 value)
        {
            ShaderProgram.Bind();
            int uniform = GL.GetUniformLocation(ShaderProgram.ShaderProgramID, uniformName);
            if (uniform < 0)
                return false;

            GL.Uniform3(uniform, value);
            ShaderProgram.Unbind();
            return true;
        }

        public bool SetUniform(string uniformName, Vector4 value)
        {
            ShaderProgram.Bind();
            int uniform = GL.GetUniformLocation(ShaderProgram.ShaderProgramID, uniformName);
            if (uniform < 0)
                return false;

            GL.Uniform4(uniform, value);
            ShaderProgram.Unbind();
            return true;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (ShaderID > 0)
                    GL.DeleteShader(ShaderID);

                if (disposing)
                    ShaderProgram?.Shaders?.Remove(this);
                

                disposedValue = true;
            }
        }

        ~Shader()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
