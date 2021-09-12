using SearchSchool.DTO;
using SearchSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchSchool.Repositories.Contract
{
    public interface ISchoolService
    {
        Task<List<School>> Get(int? limit);
        Task<List<School>> GetNearByLocation(decimal longitude, decimal latitude);
        Task<List<School>> GetSchoolFromRadius(SchoolFilterDTO schoolFilterDTO);
    }
}
