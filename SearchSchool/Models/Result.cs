using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSchool.Models
{
    public class Result<T>
    {
        [JsonProperty("records")]
        public List<T> Records;
    }
}
