using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YUVReader.YUV
{
    public class RGB
    { 

        public static RGB YUV2RGB(byte[] RGB ,byte y, byte u, byte v)
        {
            RGB rgb = new RGB();





            /*rgb. = YUV.Y + (1.140 * YUV.V);
            rgb. = YUV.Y - ( 0.395 * YUV.U) - (0.581 * YUV.V);
            rgb. = YUV.Y + (2.032 * YUV.U);*/

            return rgb;
        }
    }
}
