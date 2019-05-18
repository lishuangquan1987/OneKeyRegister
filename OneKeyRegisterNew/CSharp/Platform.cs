using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneKeyRegisterNew.CSharp
{
   public class Platform
    {
        public static bool Is32Bits()
        {
            if (IntPtr.Size == 4)
            {
                // 32-bit
                return true;
            }

            return false;
        }

        public static bool Is64Bits()
        {
            if (IntPtr.Size == 8)
            {
                // 64-bit
                return true;
            }

            return false;
        }
    }
}
