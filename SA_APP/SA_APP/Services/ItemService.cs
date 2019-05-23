using Newtonsoft.Json;
using SA_APP.Models.FromApi;
using SA_APP.Views;
//using SA_APP.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace SA_APP.Services
{
    public class ItemService
    {
        private HttpClient _client;
        public ItemService()
        {
            _client = new HttpClient();
        }

        //public List<Item> SearchItemsInMemory(string search, int storeId)
        //{
        //    var itemsFound = InMemoryDatabase.SearchItems(search, storeId);
        //    return itemsFound;
        //}


        public List<Item> SearchItems(string search, int storeId)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/Items/SearchItems?searchitem={search}&storeId={storeId}");
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

                    var items = JsonConvert.DeserializeObject<List<Item>>(responseJson);
                    return items;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        //public GetItemNutritionWidget()
        //{

        //}

        public List<Item> GetThreeStoreItemsForHomePage(int storeId)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/ItemStoreLinks/GetThreeItemsFromStore?storeId={storeId}");
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

                    var items = JsonConvert.DeserializeObject<List<Item>>(responseJson);
                    return items;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public Item GetItemById(int itemId)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/Items/GetItemByIdAsync?itemId={itemId}");
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

                    var item = JsonConvert.DeserializeObject<Item>(responseJson);
                    return item;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Item> GetInMemoryItems()
        {

            var assembly = typeof(SearchItemsPage).GetTypeInfo().Assembly;
            List<Item> items = new List<Item>();
            foreach (var res in assembly.GetManifestResourceNames())
            {
                if (res.Contains("SpoonItems.txt"))
                {
                    Stream stream = assembly.GetManifestResourceStream(res);

                    using (var reader = new StreamReader(stream))
                    {
                        string data = "";
                        int countId = 1;
                        while ((data = reader.ReadLine()) != null)
                        {
                            var splitLine = data.Split(',');
                            int spoonId = Convert.ToInt32(splitLine[0]);
                            string itemName = splitLine[1];
                            string section = splitLine[2];
                            int storeId = Convert.ToInt32(splitLine[3]);
                            decimal price = Convert.ToDecimal(splitLine[4]);
                            int stockAmount = Convert.ToInt32(splitLine[5]);
                            int slotId = Convert.ToInt32(splitLine[6]);
                            int other = Convert.ToInt32(splitLine[7]);


                            Item item = new Item
                            {
                                Id = countId,
                                Name = itemName,
                                Price = price,
                                storeId = storeId,
                                Image = "https://spoonacular.com/productImages/29260-312x231.jpg",
                                InStock = true,
                                StockAmount = stockAmount,
                                SlotId = slotId,
                            };

                            items.Add(item);
                            countId++;
                        }
                    }
                }
            }

            return items;
            //return null;
        }
    }
}
