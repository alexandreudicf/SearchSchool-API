using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSchool.Models
{
    public class Content<T>
    {
        [JsonProperty("help")]
        public string Help { get; set; }
        [JsonProperty("success")]
        public string Success { get; set; }

        [JsonProperty("result")]
        public Result<T> Result { get; set; }
    }
}
