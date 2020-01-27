using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR
{
    public static partial class Camera
    {
        public static float X { get; set; }
        public static float Y { get; set; }

        public static float Xvel { get; set; }
        public static float Yvel { get; set; }

        internal static void Update(object caller, EventArgs e)
        {
            X += Xvel;
            Y += Yvel;
        }
    }
}
