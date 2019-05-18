// AssemblyRegistration.cs
// Source codes for performing assembly registration and type library creation and registration.
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
    public class CSharpRegister:IRegister
    {
        private ITypeLibExporterNotifySink m_pITypeLibExporterNotifySink;
        public CSharpRegister()
        {
            m_pITypeLibExporterNotifySink = new ConversionEventHandler(true);
        }

        #region
        //static bool PerformAssemblyRegistration(string strTargetAssemblyFilePath, bool bCodeBase)
        //{
        //    try
        //    {
        //        RegistrationServices registration_services = new RegistrationServices();
        //        Assembly assembly = Assembly.LoadFrom(strTargetAssemblyFilePath);
        //        AssemblyRegistrationFlags flags;

        //        bool bRet = false;

        //        if (bCodeBase == true)
        //        {
        //            flags = AssemblyRegistrationFlags.SetCodeBase;
        //        }
        //        else
        //        {
        //            flags = AssemblyRegistrationFlags.None;
        //        }

        //        bRet = registration_services.RegisterAssembly(assembly, flags);

        //        if (bRet)
        //        {
        //            Console.WriteLine(string.Format("Successfully registered assembly [{0:S}].", strTargetAssemblyFilePath));

        //            if (m_bVerbose)
        //            {
        //                Type[] types = registration_services.GetRegistrableTypesInAssembly(assembly);

        //                Console.WriteLine(string.Format("Types Registered :"));

        //                foreach (Type type in types)
        //                {
        //                    Console.WriteLine(string.Format("GUID : [{0:S}] [{1:S}].", type.GUID.ToString(), type.FullName));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine(string.Format("Failed to register assembly [{0:S}].", strTargetAssemblyFilePath));
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
                string strTargetAssemblyDirectory = Path.GetDirectoryName(strTargetAssemblyFilePath);
                string strTargetAssemblyFileNameWithoutExtension = Path.GetFileNameWithoutExtension(strTargetAssemblyFilePath);
                string strTargetTypeLibFullPath = strTargetAssemblyDirectory + "\\" + strTargetAssemblyFileNameWithoutExtension + ".tlb";

                TypeLibConverter converter = new TypeLibConverter();
                Assembly assembly = Assembly.LoadFrom(strTargetAssemblyFilePath);
                TypeLibExporterFlags flags;

                if (Platform.Is32Bits())
                {
                    flags = TypeLibExporterFlags.ExportAs32Bit;
                }
                else if (Platform.Is64Bits())
                {
                    flags = TypeLibExporterFlags.ExportAs64Bit;
                }
                else
                {
                    //Console.WriteLine(string.Format("Unknown bit-ness."));
                    //return false;
                    return new ExecuteResult() { IsSuccess=false,ErrorMsg= "Unknown bit-ness." };
                }

                ICreateTypeLib create_typeLib = (ICreateTypeLib)(converter.ConvertAssemblyToTypeLib
                    (assembly, strTargetTypeLibFullPath, flags, m_pITypeLibExporterNotifySink));

                // SaveAllChanges() will create the TypeLib physical file
                // based on strTargetTypeLibFullPath.
                create_typeLib.SaveAllChanges();

                ITypeLib typelib = (ITypeLib)create_typeLib;

                UInt32 uiRetTemp = WindowsAPI.RegisterTypeLib(typelib, strTargetTypeLibFullPath, null);

                if (uiRetTemp == 0)
                {
                    //Console.WriteLine(string.Format("TypeLib File [{0:S}] registered.", strTargetTypeLibFullPath));
                    return new ExecuteResult() { IsSuccess = true };
                }
                else
                {
                    //Console.WriteLine(string.Format("Failed to register TypeLib File [{0:S}]. Error code : [{1:D}]",
                    //    strTargetTypeLibFullPath, uiRetTemp));
                    //return false;
                    return new ExecuteResult()
                    {
                        IsSuccess = false,
                        ErrorMsg = string.Format("Failed to register TypeLib File [{0:S}]. Error code : [{1:D}]",
                        strTargetTypeLibFullPath, uiRetTemp)
                    };
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(string.Format("An exception occurred. Exception description : [{0:S}].", ex.Message));
                //return false;
                return new ExecuteResult() { IsSuccess = false,ErrorMsg= string.Format("An exception occurred. Exception description : [{0:S}].", ex.Message) };
            }
        }
    }
}