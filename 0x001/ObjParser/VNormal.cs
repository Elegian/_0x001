using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjParser
{
    public class VNormal
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public int Index { get; set; }

        public static VNormal ParseString(string data)
        {
            var nfi = new NumberFormatInfo();
            var f = NumberStyles.Float;
            nfi.NegativeSign = "-";
            var segments = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length < 4)
                throw new ArgumentException("Input array must be of minimum length (4)");

            if (segments[0] != "vn")
                throw new ArgumentException("Data prefix must be 'v'");
            bool sc = false;
            var ver = new VNormal();
            double x, y, z;

            sc = double.TryParse(segments[1], f, nfi, out x);
            if (!sc) throw new ArgumentException("Could not parse X parameter as double"); else ver.X = x;

            sc = double.TryParse(segments[2], f, nfi, out y);
            if (!sc) throw new ArgumentException("Could not parse Y parameter as double"); else ver.Y = y;

            sc = double.TryParse(segments[3], f, nfi, out z);
            if (!sc) throw new ArgumentException("Could not parse Z parameter as double"); else ver.Z = z;

            return ver;
        }

        public override string ToString()
        {
            return string.Format("X: {0} Y: {1} Z: {2}", X, Y, Z);
        }
    }
}