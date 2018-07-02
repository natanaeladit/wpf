using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;


namespace App
{
    public class InputOutput
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static string[] ReadFilesFromFolder(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    return Directory.GetFiles(folderPath, "*.xml");
                }
            }
            catch
            {
                // Log error
            }
            return null;
        }

        public static string ReadFileContent(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
            }
            catch
            {
                // Log error
            }
            return null;
        }

        public static BitmapImage ReadImageFromBase64String(string base64)
        {
            try
            {
                byte[] binaryData = Convert.FromBase64String(base64);

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(binaryData);
                bi.EndInit();
                
                return bi;
            }
            catch
            {
                // Log error
            }
            return null;
        }

        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapImage retval;

            try
            {
                retval = (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return retval;
        }

        public static void DrawBitmapWithGreenBorder(Bitmap bmp)
        {
            const int borderSize = 20;
            Graphics gr = Graphics.FromImage(bmp);
            System.Drawing.Point pos = new System.Drawing.Point(0, 0);

            using (Brush border = new SolidBrush(Color.Green))
            {
                gr.FillRectangle(border, pos.X + borderSize, pos.Y + borderSize,
                    bmp.Width - borderSize, bmp.Height - borderSize);
            }

            gr.DrawImage(bmp, pos);
            gr.Dispose();
        }
    }
}
