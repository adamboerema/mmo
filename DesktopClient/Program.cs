using System;

namespace DesktopClient
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using var game = new DesktopClient();
            game.Run();
        }
    }
}
