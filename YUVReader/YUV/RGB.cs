using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing;

namespace YUVReader.YUV
{
    public class RGB
    {
        public static byte[] YUV444(byte[] y, byte[] u, byte[] v)
        {
            byte[] rgb = new byte[2500];

            return rgb;
        }

        //public static byte[] YUV422(byte[] y, byte[] u, byte[] v)
        //{
        //    byte[] rgb;

        //    return rgb;
        //}

        /* public static byte[] YUV420(byte[] bytes)
         {

             int width=178, height=144;
             int pixelCount = width * height, frame;
             frame = (bytes.Length * 2) / (pixelCount * 3);
             int yCount = pixelCount, uCount = pixelCount / 6, vCount = uCount;
             byte[] data = new byte[yCount * 3];
             int byteCount = pixelCount * 3;
             for (int f = 0; f < frame; f++)
             {
                 int i = 0, j = 0;
                 byte[] y = new byte[yCount], u = new byte[uCount], v = new byte[vCount];
                 for (int p = 0; p < yCount; p++)
                 {
                     y[i++] = bytes[f * byteCount + j++];
                 }
                 i = 0;
                 for (int p = 0; p < uCount; p++)
                 {
                     u[i++] = bytes[f * byteCount + j++];
                 }
                 i = 0;
                 for (int p = 0; p < vCount; p++)
                 {
                     v[i++] = bytes[f * byteCount + j++];
                 }

                 for (int d = 0; d < yCount * 3; d++)
                 {
                     data[d] = y[d / 3];
                 }
                 OpenFileDialog openFileDialog = new OpenFileDialog();
                 Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                 BitmapData bmpData = bitmap.LockBits(
                            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                            ImageLockMode.WriteOnly, bitmap.PixelFormat);
                 Marshal.Copy(data, 0, bmpData.Scan0, data.Length);
                 bitmap.UnlockBits(bmpData);

                 bitmap.Save(openFileDialog.InitialDirectory + "//file//bit" + f + ".bmp");
             }
             return data;
         }

     }
     */
    }
}
