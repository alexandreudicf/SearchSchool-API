using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSchool.DTO
{
    /// <summary>
    /// School Filter DTO to retrive data to load schools. 
    /// </summary>
    public class SchoolFilterDTO
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int? Distance { get; set; }
        public string SchoolName { get; set; }
        public string District { get; set; }
    }
}
