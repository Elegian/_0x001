using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using _0x001.Views.Windows;

namespace _0x001
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool areYouElegian = false;

            //if(areYouElegian)
            //{
            //    _0x001.Dev.Elegian.TestDisplay td = new Dev.Elegian.TestDisplay(640, 640);
            //    td.Run();

            //}
            //else
            //{
                ToolkitOptions Options = new ToolkitOptions() { Backend = PlatformBackend.PreferNative, EnableHighResolution = true };
                using (Toolkit.Init(Options))
                using (OpenGLExampleBaseWindow MainWindow = new OpenGLExampleBaseWindow())
                {
                    MainWindow.Run();
                }
            //}
        }
    }
}
