using OpenTK.Windowing.Desktop;
using System;

namespace Tugas_Individu
{
    class Program
    {
        static void Main(string[] args)
        {
            var ourWindow = new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(1920, 1080),
                Title = "Tugas Individu - Object 3D"
            };

            using (var win = new Window(GameWindowSettings.Default, ourWindow))
            {
                win.Run();
            }
        }
    }
}
