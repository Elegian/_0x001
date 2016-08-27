using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

namespace _0x002.TextureOperations
{
    public class LoadTexture
    {
        /// <summary>
        /// Loading the texture
        /// </summary>
        /// <param name="path">Full dir of file</param>
        /// <param name="dimesion">TextureTarget.Texture2D and so on...</param>
        /// <returns></returns>

        public static int Load(string filename, string path, TextureTarget dimesion)
        {
            if(!File.Exists(path))
            {
                throw new FileNotFoundException("Texture not found at " + path);
            }

            int id = GL.GenTexture();
            GL.BindTexture(dimesion, id);

            Bitmap bitmap = new Bitmap(path);
            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(dimesion, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);
            GL.TexParameter(dimesion, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(dimesion, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexParameter(dimesion, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(dimesion, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return id;
        }
    }
}
