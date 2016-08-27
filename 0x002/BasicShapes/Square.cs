using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0x002.BasicShapes
{
    public class Square
    {

        public static void Draw(Vector2 position, Vector2 scale, Color color, Vector2 origin)
        {
            // not completed yet 

            Vector2[] vertices = new Vector2[4]
            {
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(1, 1),
                new Vector2(0, 1),
            };

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            for(int i = 0; i < 4; i++)
            {
                GL.Vertex2(vertices[i]);
                vertices[i] -= origin;
                vertices[i] *= scale;
                vertices[i] += position;
            }
            GL.End();
        }
    }
}
