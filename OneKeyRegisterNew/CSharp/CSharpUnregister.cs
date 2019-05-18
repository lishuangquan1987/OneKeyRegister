// AssemblyUnregistration.cs
// Source codes for performing assembly un-registration and type library un-registration.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;

namespace OneKeyRegisterNew.CSharp
{
    partial class CSharpUnregister:IRegister
    {
        #region
        //public bool PerformAssemblyUnregistration(string strTargetAssemblyFilePath)
        //{
        //    try
        //    {
        //        RegistrationServices registration_services = new RegistrationServices();
        //        Assembly assembly = Assembly.LoadFrom(strTargetAssemblyFilePath);

        //        bool bRet = false;

        //        bRet = registration_services.UnregisterAssembly(assembly);

        //        if (bRet)
        //        {
        //            Console.WriteLine(string.Format("Successfully unregistered assembly [{0:S}].", strTargetAssemblyFilePath));
        //        }
        //        else
        //        {
        //            Console.WriteLine(string.Format("Failed to unregister assembly [{0:S}].", strTargetAssemblyFilePath));
        //        }

        //        return bRet;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(string.Format("An exception occurred. Exception description : [{0:S}].", ex.Message));
        //        return false;
        //    }
        //}
        #endregion
        public ExecuteResult Run(string strTargetAssemblyFilePath)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(strTargetAssemblyFilePath);
                Version version = assembly.GetName().Version;                
                GuidAttribute guid_attribute = (GuidAttribute)(assembly.GetCustomAttributes(typeof(GuidAttribute), false)[0]);
                Guid guid = new Guid(guid_attribute.Value);
                System.Runtime.InteropServices.ComTypes.SYSKIND syskind;
                string tlbFile = Path.Combine(Path.GetDirectoryName(strTargetAssemblyFilePath), Path.GetFileNameWithoutExtension(strTargetAssemblyFilePath) + ".tlb");

                if (Platform. Is32Bits())
                {
                    syskind = System.Runtime.InteropServices.ComTypes.SYSKIND.SYS_WIN32;
                }
                else if (Platform.Is64Bits())
                {
                    syskind = System.Runtime.InteropServices.ComTypes.SYSKIND.SYS_WIN64;
                }
                else
                {
                    //Console.WriteLine(string.Format("Unknown bit-ness."));
                    //return false;
                    return new ExecuteResult() { IsSuccess = false, ErrorMsg = "Unknown bit-ness." };
                }

                UInt32 uiRetTemp =WindowsAPI. UnRegisterTypeLib(ref guid, (UInt16)(version.Major), (UInt16)(version.Minor), 0, syskind);

                File.Delete(tlbFile);
                if (uiRetTemp == 0)
                {
                    //Console.WriteLine(string.Format("TypeLib File for assembly [{0:S}] unregistered.", strTargetAssemblyFilePath));
                    return new ExecuteResult { IsSuccess = true };
                }
                else
                {
                    //Console.WriteLine(string.Format("Failed to unregister TypeLib File for assembly [{0:S}]. Error code : [{1:D}]",
                    //    strTargetAssemblyFilePath, uiRetTemp));
                    //return false;
                    return new ExecuteResult()
                    {
                        IsSuccess = false,
                        ErrorMsg = string.Format("Failed to unregister TypeLib File for assembly [{0:S}]. Error code : [{1:D}]",
                        strTargetAssemblyFilePath, uiRetTemp)
                    };
                }

            }
            catch (Exception ex)
            {
                //Console.WriteLine(string.Format("An exception occurred. Exception description : [{0:S}].", ex.Message));
                //return false;
                return new ExecuteResult() { IsSuccess = false, ErrorMsg = string.Format("An exception occurred. Exception description : [{0:S}].", ex.Message) };
            }
        }
    }
}
