using System;
using System.Windows.Forms;

namespace Ikaros
{
    internal static class Program
    {
        private static TrayMenu menu;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            menu = new TrayMenu();
            Application.Run(menu);
        }
    }
}
