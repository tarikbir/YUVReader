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
        public int Frame { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }
}
