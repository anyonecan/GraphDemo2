using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GraphDemo
{

    // try this
    public class Global
    {
        public static int nElements;
        // call it with Global.nElements

      //  public static Read rr;
        // call it with Global.rr---inconsis accessibility, graphdemo.read is less acc than this
    }
    // end of try
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GraphDemo());
           
        }
    }
}
