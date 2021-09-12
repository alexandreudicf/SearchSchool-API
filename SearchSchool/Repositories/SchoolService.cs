using GeoCoordinatePortable;
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
        private CacheSchools _schools;
        private readonly DataPoaClient _dataPoaClient;

        public SchoolService(DataPoaClient dataPoaClient, CacheSchools schools)
        {
            _dataPoaClient = dataPoaClient;
            _schools = schools;
        }

        public async Task<List<School>> Get(int? limit)
        {
            var result = await GetAll();

            return result.Take(limit ?? 5).ToList();

            //var param = new Dictionary<string, object> { { "limit", limit ?? 5 } };
            //Content<School> result =  await _dataPoaClient.Search<Content<School>>(null, param);
            //return result?.Result?.Records;
        }

        public async Task<List<School>> GetAll()
        {
            if (_schools != null && _schools.Count != 0)
                return _schools;

            Content<School> result = await _dataPoaClient.Search<Content<School>>(null, null);
            _schools.AddAll(result?.Result?.Records);

            return _schools;
        }


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

        public async Task<List<School>> GetSchoolFromRadius(double latitude, double longitude, double distanceLimit)
        {

            var user = new GeoCoordinate(latitude, longitude);

            var result = await GetAll();
            return result.Select(r =>
            {
                var current = new GeoCoordinate(r.Latitude, r.Longitude);
                double distanceBetween = current.GetDistanceTo(user);
                r.Distance = distanceBetween;

                if (distanceBetween <= distanceLimit)
                    return r;
                return null;
            })
            .Where(l => l != null)
            .OrderBy(location => location.Distance)
            .ToList();
        }
    }
}
