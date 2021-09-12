using GeoCoordinatePortable;
using SearchSchool.DTO;
using SearchSchool.Models;
using SearchSchool.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SearchSchool.Repositories
{
    public class SchoolService : ISchoolService
    {
        private readonly CacheSchools _schools;
        private readonly DataPoaClient _dataPoaClient;

        public SchoolService(DataPoaClient dataPoaClient, CacheSchools schools)
        {
            _dataPoaClient = dataPoaClient;
            _schools = schools;
        }

        /// <summary>
        /// Get school list based on the limit.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<School>> Get(int? limit)
        {
            var result = await GetAll();

            return result.Take(limit ?? 5).ToList();

            //var param = new Dictionary<string, object> { { "limit", limit ?? 5 } };
            //Content<School> result =  await _dataPoaClient.Search<Content<School>>(null, param);
            //return result?.Result?.Records;
        }
        
        /// <summary>
        /// Brings all school records.
        /// </summary>
        /// <returns></returns>
        public async Task<List<School>> GetAll()
        {
            if (_schools != null && _schools.Count != 0)
                return _schools;

            Content<School> result = await _dataPoaClient.Search<Content<School>>(null, null);
            _schools.AddAll(result?.Result?.Records);

            return _schools;
        }


        /// <summary>
        /// I tried to use this param query to instead of calling get all I could use this method to get in demand.
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public async Task<List<School>> GetNearByLocation(decimal longitude, decimal latitude)
        {
            string query = "SELECT * FROM \"5579bc8e-1e47-47ef-a06e-9f08da28dec8\" WHERE abr_nome LIKE 'CIRANDINHA'";
            //string query = @$"SELECT id, 3956 * 2 * ASIN(SQRT(POWER(SIN(({latitude} - abs(latitude)) * pi()/180 / 2), 2)
            //                       + COS(37 * pi() / 180) * COS(abs(latitude) * pi() / 180)
            //                       * POWER(SIN(({longitude} - longitude) * pi() / 180 / 2), 2) )) as distance
            //                FROM ""5579bc8e-1e47-47ef-a06e-9f08da28dec8""
            //                HAVING distance< 50
            //                ORDER BY distance";
            Content<School> result = await _dataPoaClient.SearchByQuery<Content<School>>(null, query);
            return result?.Result?.Records;
        }

        /// <summary>
        /// Get School list based on some filter.
        /// </summary>
        /// <param name="schoolFilterDTO"></param>
        /// <returns></returns>
        public async Task<List<School>> GetSchoolFromRadius(SchoolFilterDTO schoolFilterDTO)
        {
            if (schoolFilterDTO == null)
                throw new ArgumentNullException(nameof(schoolFilterDTO));

            schoolFilterDTO.Distance = schoolFilterDTO.Distance == null ? 3000 : schoolFilterDTO.Distance.GetValueOrDefault() * 1000;

            var user = new GeoCoordinate(schoolFilterDTO.Latitude, schoolFilterDTO.Longitude);

            var result = await GetAll();
            return result.Select(r =>
            {
                var current = new GeoCoordinate(r.Latitude, r.Longitude);
                double distanceBetween = current.GetDistanceTo(user);
                r.Distance = distanceBetween;

                if (distanceBetween <= schoolFilterDTO.Distance)
                    return r;
                return null;
            })
            .Where(l => l != null && 
                        (schoolFilterDTO.District == null || l.Bairro.Contains(schoolFilterDTO.District, StringComparison.OrdinalIgnoreCase)) &&
                        (schoolFilterDTO.SchoolName == null || l.Nome.Contains(schoolFilterDTO.SchoolName, StringComparison.OrdinalIgnoreCase)))
            .OrderBy(location => location.Distance)
            .ToList();
        }
    }
}
