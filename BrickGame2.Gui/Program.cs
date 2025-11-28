using System;
using System.Windows.Forms;

namespace BrickGame2.Gui   // <- to samo co w MenuForm
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MenuForm());   // <--- TU MA BYÆ MenuForm
        }
    }
}
