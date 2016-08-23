using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace _0x001
{
    class Program
    {
        static void Main(string[] args)
        {
            ToolkitOptions Options = new ToolkitOptions() { Backend = PlatformBackend.PreferNative, EnableHighResolution = true };
            using (Toolkit.Init(Options))
            using (GameWindow MainWindow = new GameWindow())
            {
                MainWindow.Run();
            }
        }
    }
}
