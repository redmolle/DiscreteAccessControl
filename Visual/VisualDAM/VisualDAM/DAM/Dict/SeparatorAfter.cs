using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAM.Dict
{
    class SeparatorAfter
    {
        public static string SystemName { get { return ":"; } }
        public static string SystemParamName { get { return "/"; } }
        public static string SystemParamID { get { return "="; } }
        public static string SystemParam { get { return ","; } }
        public static string System { get { return "."; } }

        public static string ObjectName { get { return "/"; } }
        public static string ObjectID { get { return ":"; } }
        public static string ObjectParamName { get { return "="; } }
        public static string ObjectParam { get { return ","; } }
        public static string Object { get { return ";"; } }
        public static string LastObject { get { return "."; } }

        public static string UserName { get { return "/"; } }
        public static string UserID { get { return ":"; } }
        public static string UserParamID { get { return "="; } }
        public static string UserParam { get { return ","; } }
        public static string User { get { return ";"; } }
        public static string LastUser { get { return "."; } }
    }
}
