using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SearchSchool.Models;
using SearchSchool.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SearchSchool
{
    public class DataPoaClient
    {
        private const string datastoreSearch = "datastore_search";
        private const string resourceId = "5579bc8e-1e47-47ef-a06e-9f08da28dec8";
        private readonly HttpClient _client;

        public DataPoaClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> CallAPI<T>(string api)
        {
            HttpResponseMessage response = await _client.GetAsync(api);
            return await TransformResult<T>(response);
        }
        public async Task<T> Search<T>(string api, Dictionary<string, object> param)
        {
            StringBuilder appendQuery = new($"?resource_id={resourceId}");
            if (param != null)
            {
                foreach (var val in param)
                { 
                    if (val.Value != null)
                        appendQuery.Append($"&{val.Key}={val.Value}");
                }
            }

            HttpResponseMessage response = await _client.GetAsync($"action/{datastoreSearch}{api ?? ""}{appendQuery}");
            return await TransformResult<T>(response);
        }
        public async Task<T> SearchByQuery<T>(string api, string query)
        {
            StringBuilder appendQuery = new($"?resource_id={resourceId}");
            appendQuery.Append($"&sql={query}");


            HttpResponseMessage response = await _client.GetAsync($"action/{datastoreSearch}{api ?? ""}{appendQuery}");
            return await TransformResult<T>(response);
        }

        public async Task<T> TransformResult<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var resultJson = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(resultJson);
            }

            return default;
        }

    }
}
