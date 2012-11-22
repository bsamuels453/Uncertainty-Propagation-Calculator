#region

using System;
using System.Windows.Forms;

#endregion

namespace Uncertainty_Propagation_Calculator{
    internal static class Program{
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(){
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}