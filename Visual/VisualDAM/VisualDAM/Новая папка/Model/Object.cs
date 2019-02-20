using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAM.Model
{
    public class Object
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public List<DAM.Model.Param> Params { get; set; }
    }
}
