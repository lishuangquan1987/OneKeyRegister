using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace OneKeyRegisterNew.CSharp
{
   public class WindowsAPI
    {
        // Windows API to register a COM type library.
        [DllImport("Oleaut32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public extern static UInt32 RegisterTypeLib(ITypeLib tlib, string szFullPath, string szHelpDir);

        [DllImport("Oleaut32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        public extern static UInt32 UnRegisterTypeLib(ref Guid libID, UInt16 wVerMajor, UInt16 wVerMinor, int lcid, System.Runtime.InteropServices.ComTypes.SYSKIND syskind);
    }
}
