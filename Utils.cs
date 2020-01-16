using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR
{
    internal static class Utils
    {
        internal static int[] GetArrayRange(this int sourceArrayLength)
        {
            int[] Temp = new int[sourceArrayLength];
            for (int index = 0; index < sourceArrayLength; index++)
            {
                Temp[index] = index;
            }
            return Temp;
        }
    }
}
