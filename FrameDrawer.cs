using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenToASCII
{
    public class FrameDrawer
    {
        private int _monitorSizeX = 0;
        private int _monitorSizeY = 0;
        private int _consoleWindowSizeX = 0;
        private int _consoleWindowSizeY = 0;
        private ScreenCapture _screenCapture;

        private readonly string _gradient = "             .......:::::!/r(l1Z4H9W8$@";

        public FrameDrawer()
        {
            _monitorSizeX = Screen.AllScreens[0].WorkingArea.Width;
            _monitorSizeY = Screen.AllScreens[0].WorkingArea.Height;

            _screenCapture = new ScreenCapture(_monitorSizeX, _monitorSizeY);
        }

        public string GetFrame()
        {
            _consoleWindowSizeX = Console.WindowWidth;
            _consoleWindowSizeY = Console.WindowHeight;

            string result = "";
            Color[,] _screenshot = _screenCapture.GetRawScreenshot();

            for (int y = 0; y < _consoleWindowSizeY; y++)
            {
                result += "\n";
                result += getLine(_screenshot, y);
            }

            return result;
        }

        private string getLine(Color[,] screenshot, int lineY)
        {
            string result = "";
            int screenXMod = (int)Math.Floor((decimal)(_monitorSizeX / _consoleWindowSizeX));
            int screenYMod = (int)Math.Floor((decimal)(_monitorSizeY / _consoleWindowSizeY));

            for (int x = 0; x < _consoleWindowSizeX; x++)
            {
                Color pxc = screenshot[x * screenXMod, lineY * screenYMod];
                float avgNormolized = ((float)((pxc.R + pxc.G + pxc.B) / 3)) / 255;
                //float avgNormolized = pxc.GetBrightness();
                result += _gradient[(int)(avgNormolized * _gradient.Length) - 1];
            }

            return result;
        }
    }
}
