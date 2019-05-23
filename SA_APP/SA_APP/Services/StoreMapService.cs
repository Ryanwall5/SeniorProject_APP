using Newtonsoft.Json;
using SA_APP.Models;
using SA_APP.Models.FromApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SA_APP.Services
{
    public class StoreMapService
    {
        private HttpClient _client;
        public StoreMapService()
        {
            _client = new HttpClient();
        }


        public List<LowerDepartment> GetLowerDepartments(int departmentId)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/StoreMap/GetLowerDepartments?={departmentId}");
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    var lowerDepartments = JsonConvert.DeserializeObject<List<LowerDepartment>>(responseJson);
                    return lowerDepartments;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Aisle>> GetAisles(int lowerDepartmentId)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/StoreMap/GetAisles?={lowerDepartmentId}");
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";
                var response = (HttpWebResponse) await request.GetResponseAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    var aisles = JsonConvert.DeserializeObject<List<Aisle>>(responseJson);
                    return aisles;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Section>> GetSections(int aisleId, int storeId)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/StoreMap/GetSections?id={aisleId}&storeId={storeId}");
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";
                var response = (HttpWebResponse) await request.GetResponseAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    var lowerDepartments = JsonConvert.DeserializeObject<List<Section>>(responseJson);
                    return lowerDepartments;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
