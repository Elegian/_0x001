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
using System.Drawing;
using System.Drawing.Imaging;

using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace _0x001.Views.Windows
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector2 UV;

        public Vertex(float x, float y, float z, float u, float v)
        {
            Position = new Vector3(x, y, z);
            UV = new Vector2(u, v);
        }

        public Vertex(Vector3 position, Vector2 uv)
        {
            Position = position;
            UV = uv;
        }
    }

    public class OpenGLExampleBaseWindow : GameWindow
    {
        int shaderProgram;
        int VAO, VBO;
        //Vector3[] vertecies = new Vector3[3] { new Vector3(-1f, -1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(1f, -1f, 0f) };

        int texture;
        byte[] textureData;

        //int uvBuffer;
        //Vector2[] UVs = new Vector2[3] { new Vector2(0f, 0f), new Vector2(0f, 1f), new Vector2(1f, 0f) };

        Vertex[] vertecies = new Vertex[3] { new Vertex(-1, -1, 0, 0, 1), new Vertex(0, 1, 0, 0.5f, 0), new Vertex(1, -1, 0, 1, 1) };

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            GL.Enable(EnableCap.Texture2D);

            shaderProgram = GL.CreateProgram();

            LoadShaders();

            VAO = GL.GenVertexArray();

            GL.BindVertexArray(VAO);

            VBO = GL.GenBuffer();
            //uvBuffer = GL.GenBuffer();

            //Upload position & uv data
            GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * 5 * vertecies.Length), vertecies, BufferUsageHint.StaticDraw);

            //Bind position data pointers
            int objectPosition = GL.GetAttribLocation(shaderProgram, "Position");
            GL.EnableVertexAttribArray(objectPosition);
            GL.VertexAttribPointer(objectPosition, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            //GL.VertexPointer(3, VertexPointerType.Float, 3 * sizeof(float), 0);

            // PPPUUPPPUUPPPUU
            // 12345
            //    12345

            ////Upload UV data
            //GL.BindBuffer(BufferTarget.ArrayBuffer, uvBuffer);
            //GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * 2 * UVs.Length), UVs, BufferUsageHint.StaticDraw);

            //Bind UV data pointers
            int texturePosition = GL.GetAttribLocation(shaderProgram, "vertexUV");
            GL.EnableVertexAttribArray(texturePosition);
            GL.VertexAttribPointer(texturePosition, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
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

            //Generate texture buffer
            texture = GL.GenTexture();

            Bitmap bmp = (Bitmap)Bitmap.FromFile(Path.Combine(Environment.CurrentDirectory, "WinterWallpaper.jpg"));
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            int length = bmpData.Stride * bmpData.Height;
            textureData = new byte[length];
            Marshal.Copy(bmpData.Scan0, textureData, 0, length);
            bmp.UnlockBits(bmpData);

            //W * H * 3 (RGB, one byte each)
            //textureData = new byte[bmp.Width * bmp.Height * 3];
            //int currentPixelPosition = 0;

            ////Load texture in RGB format? TODO: Make a better version, as this is extremly slow, but ok for debugging as I am lazy
            //for (int i = 0; i < bmp.Width; i++)
            //    {
            //        for (int j = 0; j < bmp.Height; j++)
            //        {
            //            Color pixel = bmp.GetPixel(i, j);

            //            textureData[currentPixelPosition] = pixel.R;
            //            textureData[currentPixelPosition + 1] = pixel.G;
            //            textureData[currentPixelPosition + 2] = pixel.B;
            //            textureData[currentPixelPosition + 3] = 255;

            //            currentPixelPosition += 3;
            //        }
            //    }

            //GL.ActiveTexture(TextureUnit.Texture0 + texture);
            //GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, textureData);

            int textureLoc = GL.GetUniformLocation(shaderProgram, "textureSampler");
            GL.Uniform1(textureLoc, texture);

            bmp.Dispose();

            GL.LinkProgram(shaderProgram);
            GL.UseProgram(shaderProgram);

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
            GL.ActiveTexture(TextureUnit.Texture0 + texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            //Actually draw shit
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            //Unbind vertecies buffer
            GL.BindVertexArray(0);
            SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            GL.DeleteProgram(shaderProgram);
            GL.DeleteBuffer(VBO);
            //GL.DeleteBuffer(uvBuffer);

            //Delete shaders here, add it

            GL.DeleteVertexArray(VAO);

        }

        /// <summary>
        /// Converts a <see cref="Vector2"/> from screenspace to renderspace.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector2 ScreenspaceToRenderspace(Vector2 position)
        {
            return new Vector2(position.X / (ClientRectangle.Width / 2) - 1, position.Y / (ClientRectangle.Height / 2) - 1);
        }

        /// <summary>
        /// Converts a <see cref="Vector3"/> from screenspace to renderspace.
        /// NOTE: The z element is NOT converted.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector3 ScreenspaceToRenderspace(Vector3 position)
        {
            return new Vector3(position.X / (ClientRectangle.Width / 2) - 1, position.Y / (ClientRectangle.Height / 2) - 1, position.Z);
        }
    }
}
