using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ObjParser
{
    public class Face
    {
        public int[] VertexIndexList { get; set; }
        public int[] TextureVertexIndexList { get; set; }

        public static Face ParseString(string data)
        {
            var segments = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length < 4)
                throw new ArgumentException("Input array must be of minimum length (4)");

            if (!segments[0].ToLower().Equals("f"))
                throw new ArgumentException("Data prefix must be 'f'");
            var face = new Face();

            int vcount = segments.Count() - 1;
            face.VertexIndexList = new int[vcount];
            face.TextureVertexIndexList = new int[vcount];

            bool success;
            for (int i = 0; i < vcount; i++)
            {
                string[] parts = segments[i + 1].Split('/');

                int vindex;
                success = int.TryParse(parts[0], out vindex);
                if (!success) throw new ArgumentException("Could not parse parameter as int");
                face.VertexIndexList[i] = vindex;

                if (parts.Count() > 1)
                {
                    success = int.TryParse(parts[1], out vindex);
                    if (!success) throw new ArgumentException("Could not parse parameter as int");
                    face.TextureVertexIndexList[i] = vindex;
                }
            }
            return face;
        }

        public override string ToString()
        {
            return string.Join("|", VertexIndexList);
        }
    }
}