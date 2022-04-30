using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ScreenToASCII
{
    public class ScreenCapture
    {
        private int _monitorSizeX = 0;
        private int _monitorSizeY = 0;

        public ScreenCapture(int screenWidth, int screenHeight)
        {
            _monitorSizeX = screenWidth;
            _monitorSizeY = screenHeight;
        }

        public unsafe Color[,] GetRawScreenshot()
        {
            Color[,] screenshot = new Color[_monitorSizeX, _monitorSizeY];

            Bitmap rawCapture = new Bitmap(_monitorSizeX, _monitorSizeY);
            Rectangle captureRect = Screen.AllScreens[0].Bounds;
            Graphics capture = Graphics.FromImage(rawCapture);
            capture.CopyFromScreen(captureRect.Left, captureRect.Top, 0, 0, rawCapture.Size);

            int width = rawCapture.Width;
            int height = rawCapture.Height;

            int bytesPerPixel = 4;
            int maxPointerLenght = width * height * bytesPerPixel;
            int stride = width * bytesPerPixel;
            byte R, G, B, A;


            BitmapData bData = rawCapture.LockBits(new Rectangle(0, 0, rawCapture.Width, rawCapture.Height),
            ImageLockMode.ReadOnly, rawCapture.PixelFormat);
            byte* scan0 = (byte*)bData.Scan0.ToPointer();

            for (int i = 0, x = 0, y = 0; i < maxPointerLenght; i += bytesPerPixel)
            {
                B = scan0[i + 0];
                G = scan0[i + 1];
                R = scan0[i + 2];
                A = scan0[i + 3];

                screenshot[x, y] = Color.FromArgb(R, G, B, 255);

                if (x == width - 1)
                {
                    y++;
                    x = -1;
                }
                x++;
            }

            rawCapture.UnlockBits(bData);

            return screenshot;
        }

    }
}
