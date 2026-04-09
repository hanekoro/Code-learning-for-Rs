using System;
using System.Windows.Forms;
using 坡度基滤波;

namespace PointCloudSlopeFilter
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
