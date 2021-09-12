using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSchool.Models
{
    public class CacheSchools : List<School>
    {
        public void AddAll(List<School> list)
        {
            this.AddRange(list);
        }
    }
}
