using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUVReader
{
    public static class Config
    {
        public static int DefaultWidth { get; set; } = 176;
        public static int DefaultHeight { get; set; } = 144;

        public static YUV.YUVFormat DefaultFormat { get; set; } = YUV.YUVFormat.YUV420;
    }
}
