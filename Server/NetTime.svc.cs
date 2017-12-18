using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“NetTime”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 NetTime.svc 或 NetTime.svc.cs，然后开始调试。
    public class NetTime : INetTime
    {
       static DateTime ChangHeTime;
        /// <summary>
        /// 权限
        /// </summary>
        int privilege = 0;
        public DateTime GetTime()
        {
            if (ChangHeTime != new DateTime())
            {
                return ChangHeTime;
            }
            else
            {
                return DateTime.Now;
            }
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool Login(string name, string password)
        {
            if (name == "admin" && password == "2018")
            {
                privilege = 100;
                return true;
            }
            else if (name == "user" && password == "1234")
            {
                privilege = 10;
                return true;
            }
            else
            {
                privilege = 0;
                return false;
            }
        }
        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="dateTime">要设置的时间</param>
        /// <param name="permitted">是否被允许</param>
        public void SetTime(DateTime dateTime, out bool permitted)
        {
            if (!CheckPrivilege(100))
            {
                permitted = false;
                return;
            }
            permitted = true;
            ChangHeTime = dateTime;
        }
        /// <summary>
        /// 检查是否满足权限
        /// </summary>
        /// <param name="MethodPrivilege">方法所需权限</param>
        private bool CheckPrivilege(int MethodPrivilege)
        {
            return (privilege >= MethodPrivilege);
        }
    }
}
