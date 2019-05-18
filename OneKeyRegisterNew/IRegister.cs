using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneKeyRegisterNew
{
   public interface IRegister
    {
        /// <summary>
        /// 执行注册或者非注册
        /// </summary>
        /// <param name="path">全路径</param>
        ExecuteResult Run(string path);
    }
}
