using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenToASCII
{
    public class Program
    {
        private static bool _isStopped = false;
        private static FrameDrawer _frameDrawer = new FrameDrawer();

        static void Main(string[] args)
        {
            _frameDrawer = new FrameDrawer();
            Console.OutputEncoding = Encoding.Unicode;

            while (!_isStopped)
            {
                Console.CursorVisible = false;
                Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
                FastConsole.WriteLine(_frameDrawer.GetFrame());
                FastConsole.Flush();
            }
        }
    }
}