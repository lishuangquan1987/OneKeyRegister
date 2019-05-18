using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace OneKeyRegisterNew.CSharp
{
    // The managed definition of the ICreateTypeLib interface.
    [ComImport()]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("00020406-0000-0000-C000-000000000046")]
    public interface ICreateTypeLib
    {
        IntPtr CreateTypeInfo(string szName, System.Runtime.InteropServices.ComTypes.TYPEKIND tkind);
        void SetName(string szName);
        void SetVersion(short wMajorVerNum, short wMinorVerNum);
        void SetGuid(ref Guid guid);
        void SetDocString(string szDoc);
        void SetHelpFileName(string szHelpFileName);
        void SetHelpContext(int dwHelpContext);
        void SetLcid(int lcid);
        void SetLibFlags(uint uLibFlags);
        void SaveAllChanges();
    }
}
