using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OneKeyRegisterNew.VB6
{
    class VB6Register : IRegister
    {
        private string functionName = "DllRegisterServer";
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
