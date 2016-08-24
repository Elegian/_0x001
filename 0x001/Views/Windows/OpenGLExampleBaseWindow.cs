using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.InteropServices;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace _0x001.Views.Windows
{
    public class OpenGLExampleBaseWindow : GameWindow
    {

        int shaderProgram;
        int VAO, VBO;
        Vector3[] vertecies = new Vector3[3] { new Vector3(-1f, -1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(1f, -1f, 0f) };

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            shaderProgram = GL.CreateProgram();

            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();

            GL.BindVertexArray(VAO);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * 3 * vertecies.Length), vertecies, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            LoadShaders();
        }

        public void LoadShaders()
        {

            #region Vertex Shader Compilation
            Console.WriteLine("Compiling vertex shader...");

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);


            using (StreamReader vertexSourceReader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Shaders", "vertex.vert")))
                GL.ShaderSource(vertexShader, vertexSourceReader.ReadToEnd());

            GL.CompileShader(vertexShader);

            //1 if compile success, 0 if not?
            int vertexShaderCompileSuccess;
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out vertexShaderCompileSuccess);

            if (vertexShaderCompileSuccess != 1)
            {
                string vertexShaderLog;
                GL.GetShaderInfoLog(vertexShader, out vertexShaderLog);
                Console.WriteLine("Compile error: " + vertexShaderLog);
            }
            else
            {
                Console.WriteLine("Successfully compiled vertex shader!");
            }
            #endregion

            #region Fragment Shader Compilation
            Console.WriteLine("Compiling fragment shader...");

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            using (StreamReader fragmentSourceReader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "Shaders", "fragment.frag")))
                GL.ShaderSource(fragmentShader, fragmentSourceReader.ReadToEnd());

            GL.CompileShader(fragmentShader);

            //1 if compile success, 0 if not?
            int fragmentShaderCompileSuccess;
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out fragmentShaderCompileSuccess);

            if (fragmentShaderCompileSuccess != 1)
            {
                string fragmentShaderLog;
                GL.GetShaderInfoLog(fragmentShader, out fragmentShaderLog);
                Console.WriteLine("Compile error: " + fragmentShaderLog);
            }
            else
            {
                Console.WriteLine("Successfully compiled fragment shader!");
            }
            #endregion

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            int linkSuccess;
            GL.GetProgram(shaderProgram, GetProgramParameterName.LinkStatus, out linkSuccess);
            if (linkSuccess != 1)
            {
                Console.WriteLine("Error: " + GL.GetProgramInfoLog(shaderProgram));
            }

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

            //Background color
            GL.ClearColor(0.2f, 0.2f, 0.2f, 2f);

            //GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * 3 * vertecies.Length), vertecies, BufferUsageHint.StaticDraw);

            //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            //GL.EnableVertexAttribArray(0);

            //Use shader program (tell the gpu what shaders to use?)
            GL.UseProgram(shaderProgram);
            //Bind our vertecies buffer
            GL.BindVertexArray(VAO);
            //Actually draw shit
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertecies.Length);
            //Unbind vertecies buffer
            GL.BindVertexArray(0);
            SwapBuffers();
            base.OnRenderFrame(e);
        }
    }
}
