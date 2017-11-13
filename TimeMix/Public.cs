using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeMix
{
   public static class Public
    {
        public static DateTime ChangHetime() {

          return DateTime.Now.AddSeconds(Settings.Default.deltaTime);//长河时间
        }

    }
}
