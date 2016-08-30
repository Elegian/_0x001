using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using OpenTK;

namespace _0x001.Dev.sanderele1
{
    public class Mesh
    {
        public Vector3[] Points;
        public Vector3[] Normals;
        public Vector2[] UVs;
        public int[] Indicies; 

        /// <summary>
        /// Will attempt to load a mesh from a .obj file.
        /// NOTE: Will return null if the path is invalid.
        /// Sanderele1: This was made in a hurry, please fix this up in the future to not be shit.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [Obsolete("Not finished")]
        public static Mesh LoadFromObjFile(string path)
        {
            if (!File.Exists(path))
                return null;

            List<Vector3> Vertecies = new List<Vector3>();
            List<Vector3> Normals = new List<Vector3>();
            List<Vector2> UVs = new List<Vector2>();
            List<int> Indicies = new List<int>();

            using (StreamReader strr = new StreamReader(path))
            {
                string line = strr.ReadLine().ToLower();

                //Vertecies 
                if (line.StartsWith("v"))
                {
                    string[] values = line.Substring(2).Split(' ');
                    if (values.Length != 3)
                    {
                        Console.WriteLine("Mesh::LoadFromObjFile() Vertex definition did not contain exactly thee dimensions from the file: " + path);
                        return null;
                    }

                    float x, y, z;

                    try
                    {
                        x = float.Parse(values[0]);
                        y = float.Parse(values[1]);
                        z = float.Parse(values[2]);
                    }
                    catch
                    {
                        Console.WriteLine("Mesh::LoadFromObjFile() Failed parsing vertex position from the file: " + path);
                        return null;
                    }

                    Vertecies.Add(new Vector3(x, y, z));
                }
                //Normals
                else if (line.StartsWith("vn"))
                {
                    string[] values = line.Substring(2).Split(' ');
                    if (values.Length != 3)
                    {
                        Console.WriteLine("Mesh::LoadFromObjFile() Normal definition did not contain exactly thee dimensions from the file: " + path);
                        return null;
                    }

                    float x, y, z;

                    try
                    {
                        x = float.Parse(values[0]);
                        y = float.Parse(values[1]);
                        z = float.Parse(values[2]);
                    }
                    catch
                    {
                        Console.WriteLine("Mesh::LoadFromObjFile() Failed parsing normal position from the file: " + path);
                        return null;
                    }

                    Normals.Add(new Vector3(x, y, z));
                }
                //UV Coords
                else if (line.StartsWith("vt"))
                {
                    string[] values = line.Substring(2).Split(' ');
                    if (values.Length != 2)
                    {
                        Console.WriteLine("Mesh::LoadFromObjFile() UV definition did not contain exactly two dimensions from the file: " + path);
                        return null;
                    }

                    float x, y;

                    try
                    {
                        x = float.Parse(values[0]);
                        y = float.Parse(values[1]);
                    }
                    catch
                    {
                        Console.WriteLine("Mesh::LoadFromObjFile() Failed parsing uv position from the file: " + path);
                        return null;
                    }

                    UVs.Add(new Vector2(x, y));
                }
                //Indecies NOTE: Starts at 1, not 0
                else if (line.StartsWith("f"))
                {
                    string[] split = new string[] { "//" };
                    string[] values = line.Substring(2).Split(' ');
                    foreach (string value in values)
                    {
                        string[] indecies = value.Split(split, StringSplitOptions.RemoveEmptyEntries);
                    }
                }
            }


                return null;
        }

    }
}
