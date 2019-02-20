using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAM.Model
{
    public class System
    {
        public string Name { get; set; }
        public List<DAM.Model.Param> Params { get; set; }
        public List<DAM.Model.Object> Objects { get; set; }
        public List<DAM.Model.User> Users { get; set; }
        public int Length { get; set; }
        //public string Json { get; set; }
        ////public List<AccessMatrixElement> AccesMatrix { get; set; }
        //public Dictionary<Tuple<string, int>,string> AccessMatrix { get; set; }
    }
}
