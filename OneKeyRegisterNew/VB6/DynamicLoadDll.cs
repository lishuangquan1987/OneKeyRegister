using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace OneKeyRegisterNew.VB6
{
    /// <summary>
    /// 动态加载DLL
    /// </summary>
    public class DynamicLoadDll
    {
        private delegate int DllRegisterHandler();
        /// <summary>
        /// 根据非托管库的句柄，函数名称和指定委托类型，返回委托
        /// </summary>
        /// <param name="dllModule"></param>
        /// <param name="functionName"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        private static Delegate GetFunctionAddress(int dllModule, string functionName, Type t)
        {
            int address = Kernel32Helper.GetProcAddress(dllModule, functionName); //得到地址
            if (address == 0)
                return null;
            else
                return Marshal.GetDelegateForFunctionPointer(new IntPtr(address), t);//将非托管函数指针转换为委托。
        }
        /// <summary>
        /// 根据dll的路径，dll中的方法名，动态调用非托管dll的方法
        /// </summary>
        /// <param name="dllPath">dll的路径</param>
        /// <param name="functionName">要调用的dll的方法名称</param>
        /// <param name="delegateType">要将对应非托管dll中的方法名映射为C#的签名一致的委托类型</param>
        /// <param name="msg">执行过程中的错误信息</param>
        /// <param name="par">执行方法的参数</param>
        /// <returns></returns>
        private static object DynamicInvoke(string dllPath, string functionName, Type delegateType, out string msg, params object[] par)
        {
            msg = "";
            int address = 0;
            try
            {
                address = Kernel32Helper.LoadLibrary(dllPath);
                if (address == 0)
                {
                    //msg = "加载dll失败，dll路径：" + dllPath;
                    msg = "fail to load dll :" + dllPath;
                    return null;
                }
                Delegate d1 = GetFunctionAddress(address, functionName, delegateType);
                if (d1 == null)
                {
                    //msg = "获取函数名称失败，函数名称：" + functionName;
                    msg = "fail to load function :" + functionName;
                    return null;
                }
                object result = d1.DynamicInvoke(par);
                Kernel32Helper.FreeLibrary(address);
                return result;
            }
            catch (Exception e)
            {
                if (address != 0)
                {
                    Kernel32Helper.FreeLibrary(address);
                }
                msg = e.Message;
                return null;
            }
        }

        public static object DynamicRegister(string dllPath, string functionName, out string msg, params object[] par)
        {
            return DynamicInvoke(dllPath,functionName,typeof(DllRegisterHandler),out msg,par);
        }
    }


    public class Kernel32Helper
    {
        /// <summary>
        /// API LoadLibrary
        /// </summary>
        [DllImport("kernel32")]
        public static extern int LoadLibrary(string funcName);

        /// <summary>
        /// API GetProcAddress
        /// </summary>
        [DllImport("kernel32")]
        public static extern int GetProcAddress(int handle, string funcName);

        /// <summary>
        /// API FreeLibrary
        /// </summary>
        [DllImport("kernel32")]
        public static extern int FreeLibrary(int handle);
    }
}
