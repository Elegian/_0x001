using OpenTK;
using _0x001.Views.Windows;

namespace _0x001
{
    class Program
    {
        static void Main(string[] args)
        {
                ToolkitOptions Options = new ToolkitOptions() { Backend = PlatformBackend.PreferNative, EnableHighResolution = true };
                using (Toolkit.Init(Options))
                using (OpenGLExampleBaseWindow MainWindow = new OpenGLExampleBaseWindow())
                {
                    MainWindow.Run();
                }
        }
    }
}
