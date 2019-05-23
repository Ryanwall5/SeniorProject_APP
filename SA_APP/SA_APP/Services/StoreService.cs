using Newtonsoft.Json;
using SA_APP.Models;
using SA_APP.Models.FromApi;
using SA_APP.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SA_APP.Services
{
    public class StoreService
    {

        private HttpClient _client;
        public StoreService()
        {
            _client = new HttpClient();
        }

        public List<Store> GetStores()
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/Stores/GetAllStores");
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";
                var response = (HttpWebResponse) request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    var stores = JsonConvert.DeserializeObject<List<Store>>(responseJson);
                    return stores;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ShoppingUserAPI ChangeStore(int storeId, string token)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/Stores/ChangeHomeStore?id={storeId}");
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "PUT";
                request.ContentType = "application/json";
                string bearer = "Bearer " + token;
                request.ContentLength = 0;
                request.Headers.Add(HttpRequestHeader.Authorization, bearer);
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    var user = JsonConvert.DeserializeObject<ShoppingUserAPI>(responseJson);

                    return (user);
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Section GetSection(int id)
        {
           return InMemoryDatabase._sectionsCanned.FirstOrDefault(s => s.Id == id);
        }

        public LowerDepartment GetLowerDepartment(int id)
        {
            return InMemoryDatabase._lowerDepartmentsGrocery.FirstOrDefault(ld => ld.Id == id);
        }

        public Shelf GetShelf(int id)
        {
            return InMemoryDatabase.shelvesCannned.FirstOrDefault(s => s.Id == id);
        }

        public ShelfSlot GetSlot(int id)
        {
            foreach(var list_of_slots in InMemoryDatabase.slots.Values)
            {
                foreach(var shelfslot in list_of_slots)
                {
                    if(shelfslot.Id == id)
                    {
                        return shelfslot;
                    }
                }
            }
            return null;
        }

        public Department GetDepartment(int id)
        {
            return InMemoryDatabase._departments.FirstOrDefault(d => d.Id == id);
        }

        public Aisle GetAisle(int id)
        {
            return InMemoryDatabase._aislesCanned.FirstOrDefault(a => a.Id == id);
        }

    }
}
