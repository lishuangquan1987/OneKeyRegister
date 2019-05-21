using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace OneKeyRegisterNew.CSharp
{
    public class CSharpRegisterNew : IRegister
    {
        public ExecuteResult Run(string path)
        {
            string regAsmPath = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe";
            if (!File.Exists(regAsmPath))
            {
                return new ExecuteResult() { IsSuccess = false, ErrorMsg = regAsmPath + "路径不存在！" };
            }

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo()
            {
                FileName = regAsmPath,
                Arguments = string.Format(" {0} {1} {2}", path, "/tlb", "/codebase"),
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };
            process.Start();            
            process.WaitForExit();
            string result = process.StandardOutput.ReadToEnd();
            string errResult = process.StandardError.ReadToEnd();
            process.Close();
            if (result.Contains("成功注册了类型") && result.Contains("成功注册了导出到"))
            {
                return new ExecuteResult() { IsSuccess = true };
            }
            else
            {
                return new ExecuteResult() { IsSuccess = false, ErrorMsg = result+errResult };
            }
        }
    }
}
