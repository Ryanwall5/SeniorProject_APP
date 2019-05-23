using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SA_APP.Models.FromApi;
using SA_APP.Models.ToApi;
//using SA_APP.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SA_APP.Services
{
    public class ShoppingListService
    {
        private HttpClient _client;
        public ShoppingListService()
        {
            _client = new HttpClient();
        }

        public ShoppingListAPP CreateShoppingList(string token, string name)
        {
            _client.BaseAddress = new Uri("https://seniorproject-api.azurewebsites.net/api/ShoppingLists/PostShoppingList");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;

                JObject jObject = new JObject
                {
                    { "Name", name }
                };

                string body = jObject.ToString();
                request.ContentLength = body.Length;
                request.Method = "POST";
                request.ContentType = "application/json";
                string bearer = "Bearer " + token;
                request.Headers.Add(HttpRequestHeader.Authorization, bearer);
                StreamWriter stream = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                stream.Write(body);
                stream.Close();

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    var shoppingList = JsonConvert.DeserializeObject<ShoppingList>(responseJson);

                    if (shoppingList.id != 0)
                    {
                        ShoppingListAPP shoppingListApp = new ShoppingListAPP
                        {
                            id = shoppingList.id,
                            items = new ObservableCollection<ShoppingItemAPP>(),
                            name = shoppingList.name,
                            //store = shoppingList.store,
                            timeOfCreation = shoppingList.timeOfCreation,
                            totalCost = shoppingList.totalCost,
                            totalItems = shoppingList.totalItems
                        };
                        return shoppingListApp;
                    }

                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //public bool CreateShoppingList(string token, string name)
        //{
        //    var listCreated = InMemoryDatabase.CreateShoppingList(token, name);
        //    if(listCreated)
        //    {
        //        return true;
        //    }

        //    return false;
        //}


        //{
        //"ListId": 1,
        //"ItemId": 1,
        //"ItemQuantity": 1
        //}
        public ShoppingItemAPP AddItemToShoppingList(string token, ItemListLink link)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/ItemShoppingListLinks/PostItemToList");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;

                JObject jObject = new JObject
                {
                    { "ItemId", link.ItemId },
                    { "ListId", link.ListId },
                    { "ItemQuantity", link.ItemQuantity }
                };

                string body = jObject.ToString();
                request.ContentLength = body.Length;
                request.Method = "POST";
                request.ContentType = "application/json";
                string bearer = "Bearer " + token;
                request.Headers.Add(HttpRequestHeader.Authorization, bearer);
                StreamWriter stream = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                stream.Write(body);
                stream.Close();

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };


                    var shoppingItem = JsonConvert.DeserializeObject<ShoppingItemAPP>(responseJson);
                    return shoppingItem;

                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteShoppingList(int id, string token)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/ShoppingLists/{id}");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "DELETE";
                request.ContentType = "application/json";
                string bearer = "Bearer " + token;
                request.Headers.Add(HttpRequestHeader.Authorization, bearer);

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteItemFromShoppingList(int linkId, string token)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/ItemShoppingListLinks/DeleteLink?linkId={linkId}");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "DELETE";
                request.ContentType = "application/json";
                string bearer = "Bearer " + token;
                request.Headers.Add(HttpRequestHeader.Authorization, bearer);

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeShoppingListName(string name, int listId, string token)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/ShoppingLists/UpdateList?listId={listId}");
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "PUT";
                request.ContentType = "application/json";
                string bearer = "Bearer " + token;
                request.Headers.Add(HttpRequestHeader.Authorization, bearer);
                JObject jObject = new JObject
                {
                    { "Name" , name }
                };

                string body = jObject.ToString();
                request.ContentLength = body.Length;

                StreamWriter stream = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                stream.Write(body);
                stream.Close();



                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    var listResponse = responseJson;

                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ItemListLink UpdateLink(ShoppingItemAPP item, string token)
        {
            _client.BaseAddress = new Uri($"https://seniorproject-api.azurewebsites.net/api/ItemShoppingListLinks/UpdateLink?linkId={item.linkId}&newQuantity={item.itemQuantity}");
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "PUT";
                request.ContentType = "application/json";
                request.ContentLength = 0;
                string bearer = "Bearer " + token;
                request.Headers.Add(HttpRequestHeader.Authorization, bearer);

                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    /*
                        {
                            "id": 1,
                            "shoppingListId": 2,
                            "shoppingList": null,
                            "itemId": 1,
                            "item": null,
                            "itemQuantity": 5
                        }
                    */

                    var updatedLink = JsonConvert.DeserializeObject<ItemListLink>(responseJson);

                    return updatedLink;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ShoppingList> GetUsersShoppingLists(string token)
        {
            _client.BaseAddress = new Uri("https://seniorproject-api.azurewebsites.net/api/ShoppingLists/GetHomeStoreShoppingLists");
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";
                string bearer = "Bearer " + token;
                request.Headers.Add(HttpRequestHeader.Authorization, bearer);
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    ListofShoppingLists listofShoppingLists = new ListofShoppingLists();
                    var shoppingLists = JsonConvert.DeserializeObject<List<ShoppingList>>(responseJson);
                    return shoppingLists;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ShoppingItem GetShoppingItem(string token, int linkId)
        {
            _client.BaseAddress = new Uri("https://seniorproject-api.azurewebsites.net/api/ItemShoppingListLinks/GetUsersShoppingItem?linkId={linkId}");
            _client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;
                request.Method = "GET";
                request.ContentType = "application/json";
                string bearer = "Bearer " + token;
                request.Headers.Add(HttpRequestHeader.Authorization, bearer);
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    var item = JsonConvert.DeserializeObject<ShoppingItem>(responseJson);
                    return item;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //public ShoppingList GetInMemoryShoppingList()
        //{
        //    return InMemoryDatabase.GetShoppingList();
        //}

        //public ListofShoppingLists GetInMemoryShoppingLists()
        //{

        //    return InMemoryDatabase.GetShoppingLists();
        //}
    }
}
