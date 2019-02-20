using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAM.Dict
{
    static class Term
    {
        public static string Name { get { return "Имя"; } }
        public static string ID { get { return "ID"; } }
        public static string Value { get { return "Значение"; } }
        public static string Separator { get { return "Разделитель"; } }
        public static string EndLine { get; set; }
    }
}
