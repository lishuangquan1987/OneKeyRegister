// ConversionEventHandler.cs
// Source codes for handling assembly reference resolution.
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
    public class ConversionEventHandler : ITypeLibExporterNotifySink
    {
        public ConversionEventHandler(bool bVerbose)
        {
            m_bVerbose = bVerbose;
        }

        public void ReportEvent(ExporterEventKind eventKind, int eventCode, string eventMsg)
        {
            // Handle the warning event here.
            if (m_bVerbose)
            {
                Console.WriteLine("ConversionEventHandler.ReportEvent() [eventKind : {0:S}] [eventCode : {1:D}] [eventMsg : {2:S}]", eventKind.ToString(), eventCode, eventMsg);
            }
        }

        public Object ResolveRef(Assembly asm)
        {
            try
            {
                // Resolve the reference here and return a correct type library.
                if (m_bVerbose)
                {
                    Console.WriteLine("ConversionEventHandler.ResolveRef() [assembly : {0:S}]", asm.FullName);
                }

                string strAssemblyDirectory = Path.GetDirectoryName(asm.Location);
                string strAssemblyFileNameWithoutExtension = Path.GetFileNameWithoutExtension(asm.Location);
                string strTypeLibFullPath = strAssemblyDirectory + "\\" + strAssemblyFileNameWithoutExtension + ".tlb";
                TypeLibConverter converter = new TypeLibConverter();
                ConversionEventHandler eventHandler = new ConversionEventHandler(m_bVerbose);
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
                    Console.WriteLine(string.Format("Unknown bit-ness."));
                    return null;
                }

                ICreateTypeLib create_typeLib = null;

                try
                {
                    create_typeLib = (ICreateTypeLib)(converter.ConvertAssemblyToTypeLib
                        (asm, strTypeLibFullPath, flags, eventHandler));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(string.Format("Unable to convert assembly [{0:S}] into a Type Lib. Exception description : [{1:S}]",
                        strAssemblyFileNameWithoutExtension, ex.Message));
                    return null;
                }

                try
                {
                    // SaveAllChanges() will create the TypeLib physical file
                    // based on strTargetTypeLibFullPath.
                    create_typeLib.SaveAllChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Unable to save TypeLib File [{0:S}] registered. Exception description : [{1:S}]", strTypeLibFullPath, ex.Message));
                    return null;
                }

                ITypeLib typelib = (ITypeLib)create_typeLib;
                UInt32 uiRetTemp = WindowsAPI.RegisterTypeLib(typelib, strTypeLibFullPath, null);

                if (uiRetTemp == 0)
                {
                    Console.WriteLine(string.Format("TypeLib File [{0:S}] registered.", strTypeLibFullPath));
                }
                else
                {
                    Console.WriteLine(string.Format("Failed to register TypeLib File [{0:S}]. Error code : [{1:D}]",
                        strTypeLibFullPath, uiRetTemp));
                    return null;
                }

                return typelib;
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("An exception occurred. Exception description : [{0:S}].", ex.Message));
                return null;
            }
        }

        private bool m_bVerbose = false;
    }
}
