using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OneKeyRegisterNew.VB6
{
    public class VB6UnRegister : IRegister
    {
        private string functionName = "DllUnregisterServer";
        public ExecuteResult Run(string path)
        {
            if (!File.Exists(path))
            {
                return new ExecuteResult() { IsSuccess = false, ErrorMsg = "path- " + path + " is not exist" };
            }
            string msg = "";
            object o = DynamicLoadDll.DynamicRegister(path, functionName, out msg);
            if (o != null && (int)o >= 0)
            {
                return new ExecuteResult() { IsSuccess = true };
            }
            else
            {
                return new ExecuteResult() { IsSuccess = false, ErrorMsg = msg };
            }
        }
    }
}
