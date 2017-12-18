using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“INetTime”。
    [ServiceContract]

    public interface INetTime
    {
        [OperationContract(IsTerminating = false, IsInitiating = true)]
        DateTime GetTime();
        [OperationContract(IsTerminating = false, IsInitiating = true)]
        void SetTime(DateTime dateTime,out bool permitted);
        [OperationContract(IsTerminating = false, IsInitiating = true)]
        bool Login(string name,string password);
    }
}
