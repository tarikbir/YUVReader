using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUVReader
{
    public class RGBVideo
    {
        public Bitmap[] Source { get; set; }
        public YUV.YUVFormat Format { get; set; }
        public int Frame { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public RGBVideo(): this(null, Config.DefaultFormat, 0, Config.DefaultWidth, Config.DefaultHeight)
        { }

        public RGBVideo(YUV.YUVFormat format, int width, int height): this(null, format, 0, width, height)
        { }

        public RGBVideo(YUV.YUVFormat format, int frame, int width, int height): this(null, format, frame, width, height)
        { }

        public RGBVideo(Bitmap[] source, YUV.YUVFormat format, int frame, int width, int height)
        {
            Source = source;
            Format = format;
            Frame = frame;
            Width = width;
            Height = height;
        }
    }
}
