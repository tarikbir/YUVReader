using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace YUVReader
{
    public class RGBConvert
    {

        public static RGBVideo ConvertRGB(byte[] data, int width, int height, YUV.YUVFormat format)
        {
            if (data.Length <= 0 || data == null) return null;
            switch (format)
            {
                case YUV.YUVFormat.YUV444:
                    return ConvertYUV444(data, width, height);
                case YUV.YUVFormat.YUV422:
                    return ConvertYUV422(data, width, height);
                case YUV.YUVFormat.YUV420:
                    return ConvertYUV420(data, width, height);
                default:
                    return null;
            }
        }

        private static RGBVideo ConvertYUV420(byte[] data, int width, int height)
        {
            int pixelCount = width * height,
                frame = (data.Length * 2) / (pixelCount * 3),
                yCount = pixelCount,
                uCount = pixelCount / 6,
                vCount = uCount;

            return null;
        }

        private static RGBVideo ConvertYUV422(byte[] data, int width, int height)
        {
            int pixelCount = width * height,
                frame = data.Length / (pixelCount * 2),
                yCount = (pixelCount * 2) / 3,
                uCount = yCount / 2,
                vCount = uCount;

            return null;
        }

        private static RGBVideo ConvertYUV444(byte[] data, int width, int height)
        {
            int pixelCount = width * height,
                frame = data.Length / (pixelCount * 3),
                yCount = pixelCount / 3,
                uCount = yCount,
                vCount = yCount;

            return null;
        }

        //public static RGBVideo YUV420(byte[] data, int width, int height)
        //{
        //    byte[] rgbData = new byte[yCount * 3];
        //    int byteCount = pixelCount * 3;
        //    for (int f = 0; f < frame; f++)
        //    {
        //        int i = 0, j = 0;
        //        byte[] y = new byte[yCount], u = new byte[uCount], v = new byte[vCount];
        //        for (int p = 0; p < yCount; p++)
        //        {
        //            y[i++] = data[f * byteCount + j++];
        //        }
        //        i = 0;
        //        for (int p = 0; p < uCount; p++)
        //        {
        //            u[i++] = data[f * byteCount + j++];
        //        }
        //        i = 0;
        //        for (int p = 0; p < vCount; p++)
        //        {
        //            v[i++] = data[f * byteCount + j++];
        //        }

        //        for (int d = 0; d < yCount * 3; d++)
        //        {
        //            rgbData[d] = y[d / 3];
        //        }
        //        OpenFileDialog openFileDialog = new OpenFileDialog();
        //        Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
        //        BitmapData bitmapData = bitmap.LockBits(
        //                   new Rectangle(0, 0, bitmap.Width, bitmap.Height),
        //                   ImageLockMode.WriteOnly, bitmap.PixelFormat);
        //        Marshal.Copy(rgbData, 0, bitmapData.Scan0, rgbData.Length);
        //        bitmap.UnlockBits(bitmapData);

        //        bitmap.Save(openFileDialog.InitialDirectory + "//file//bit" + f + ".bmp");
        //    }
        //}

    }
}
