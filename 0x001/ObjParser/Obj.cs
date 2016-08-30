using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjParser
{
    public class ObjMesh
    {
        public Obj() { }
        public Obj(string path)
        {
            var obj = Obj.Load(path);
            this.Faces = obj.Faces;
            this.Vertices = obj.Vertices;
            this.VertexNormals = obj.VertexNormals;
            this._filepath = obj._filepath;
        }

        public List<Vertex> Vertices { get; set; }
        public List<VNormal> VertexNormals { get; set; }
        public List<Face> Faces { get; set; }

        private string _filepath;

        /// <summary>
        /// Load Wavefront OBJ and parse its contents.
        /// </summary>
        /// <param name="file">File path as string</param>
        /// <returns></returns>
        public static Obj Load(string file)
        {
            if (!File.Exists(file))
                throw new ArgumentException("The specified file doesn't exist");
            var lines = File.ReadAllLines(file);
            if (lines.Length < 3)
                throw new InvalidDataException("The file is incorrect");

            var obj = new Obj() { _filepath = file };
            obj.Parse(lines);
            return obj;
        }


        private void Parse(string[] lines)
        {
            Vertices = new List<Vertex>();
            VertexNormals = new List<VNormal>();
            Faces = new List<Face>();
            for (int i = 0; i < lines.Length; i++)
                if (lines[i].StartsWith("#") || lines[i].Length < 3)
                    continue;
                else if (lines[i].StartsWith("mtllib"))
                    ParseMaterial(lines[i]);
                else if (lines[i].StartsWith("v "))
                    Vertices.Add(Vertex.ParseString(lines[i]));
                else if (lines[i].StartsWith("vn "))
                    VertexNormals.Add(VNormal.ParseString(lines[i]));
                else if (lines[i].StartsWith("f "))
                    Faces.Add(Face.ParseString(lines[i]));
        }

        private void ParseMaterial(string line)
        {
            var file = line.Split(new char[] { ' ' })[1];
            var dir = Directory.GetParent(_filepath).FullName;
            var path = Path.Combine(dir, file);

            if (File.Exists(path))
            {
                //TODO: PARSE .MTL FILE
            }
        }
    }
}
