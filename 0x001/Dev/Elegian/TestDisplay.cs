using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x001.Dev.Elegian
{
    class TestDisplay : GameWindow
    {
        public TestDisplay(int width, int height) : base (width, height)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.Orange);

            _0x002.BasicShapes.Square.Draw(new Vector2(2,3), new Vector2(0.2f, 0.2f), Color.Red, new Vector2(1, 1));

         //   GL.Begin(PrimitiveType.Quads);
         //
         //   GL.Vertex2(0, 0);
         //   GL.Vertex2(1, 0);
         //   GL.Vertex2(1, -1);
         //   GL.Vertex2(0, -1);
         //
         //   GL.End();

            this.SwapBuffers();

        }

    }
}
