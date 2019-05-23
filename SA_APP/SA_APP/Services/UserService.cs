using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SA_APP.Models.FromApi;
using SA_APP.Models.ToApi;
//using SA_APP.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SA_APP.Services
{
    public class UserService
    {

        private HttpClient _client;
        public UserService()
        {
            _client = new HttpClient();
        }

        public async Task<Tuple<BaseUser, string>> LoginUser(string email, string password)
        {
            _client.BaseAddress = new Uri("https://seniorproject-api.azurewebsites.net/api/Users/LoginUser");
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;

                JObject jObject = new JObject
                {
                    { "Email" , email },
                    { "Password", password }
                };

                string body = jObject.ToString();
                request.ContentLength = body.Length;
                request.Method = "POST";
                request.ContentType = "application/json";

                StreamWriter stream = new StreamWriter(await request.GetRequestStreamAsync(), Encoding.ASCII);
                stream.Write(body);
                stream.Close();

                var response = (HttpWebResponse) await request.GetResponseAsync();

                var cookie = response.Headers["authorization"];

                var tokenHandler = new JwtSecurityTokenHandler();
                Guid userId;
                string role = "";
                if (tokenHandler.CanReadToken(cookie))
                {
                    var token = tokenHandler.ReadJwtToken(cookie);
                    foreach (var claim in token.Claims)
                    {
                        if (claim.Type == "sub")
                        {
                            userId = Guid.Parse(claim.Value);
                        }
                        else if (claim.Type == "unique_name")
                        {
                            role = claim.Value;
                        }
                    }
                }


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseJson;
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseJson = streamReader.ReadToEnd();
                    };

                    if (role == "Shopping")
                    {
                        var user = JsonConvert.DeserializeObject<ShoppingUserAPI>(responseJson);

                        return Tuple.Create<BaseUser, string>(user, role);
                    }
                    else if (role == "Store")
                    {
                        var user = JsonConvert.DeserializeObject<StoreUser>(responseJson);

                        return Tuple.Create<BaseUser, string>(user, role);
                    }
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return null;
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;

            }

            return null;
        }

        public async Task<bool> RegisterUser(APIUser user)
        {
            try
            {
                _client.BaseAddress = new Uri("https://seniorproject-api.azurewebsites.net/api/Users/RegisterShoppingUser");
                _client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var request = WebRequest.Create(_client.BaseAddress) as WebRequest;

                JObject jObject = new JObject
                {
                    { "FirstName", user.FirstName },
                    { "LastName", user.LastName },
                    { "Email" , user.Email },
                    { "UserName", user.Email },
                    { "Password", user.Password },
                    { "HomeStoreId", user.HomeStoreId }
                };
                string body = jObject.ToString();
                request.ContentLength = body.Length;
                request.Method = "POST";
                request.ContentType = "application/json";

                StreamWriter stream = new StreamWriter(await request.GetRequestStreamAsync(), Encoding.ASCII);
                stream.Write(body);
                stream.Close();

                var response = (HttpWebResponse) await request.GetResponseAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine(response.StatusDescription);
                    return true;
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    Console.WriteLine(response.StatusDescription);
                }
                else if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    Console.WriteLine(response.StatusDescription);
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
