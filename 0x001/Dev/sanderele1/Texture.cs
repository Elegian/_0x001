using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using System.Drawing;
using System.Drawing.Imaging;

using System.Runtime.InteropServices;

using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace _0x001.Dev.sanderele1
{
    public class Texture : IDisposable
    {
        protected int textureID;
        public int TextureID
        {
            get
            {
                if (textureID < 0)
                {
                    textureID = GL.GenTexture(); 
                }

                return textureID;
            }
        }

        /// <summary>
        /// The <see cref="System.Drawing.Bitmap"/> associated with the <see cref="Texture"/> object.
        /// NOTE: This will get disposed when the texture is disposed!
        /// Sanderele1: Don't dispose of bitmap as it might be used for multiple texture objects?
        /// </summary>
        public Bitmap Bitmap { get; set; }

        /// <summary>
        /// Uploads the texture to the gpu.
        /// </summary>
        /// <returns></returns>
        public bool Upload()
        {
            //Lock the bitmap so we can access it
            BitmapData bitmapData = Bitmap.LockBits(new System.Drawing.Rectangle(0, 0, Bitmap.Width, Bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            //Copy the data from the bitmap to a byte array
            int length = bitmapData.Stride * bitmapData.Height;
            byte[] textureData = new byte[length];
            Marshal.Copy(bitmapData.Scan0, textureData, 0, textureData.Length);

            //Set some texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            //Upload the data to the gpu
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, textureData);
            Error.Print(GL.GetError());
            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {

                if (disposing)
                    Bitmap.Dispose();

                if (!(textureID < 0))
                {
                    GL.DeleteTexture(textureID);
                }

                disposedValue = true;
            }
        }

        ~Texture()
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
