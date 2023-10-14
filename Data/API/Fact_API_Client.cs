using Cats_Program.Domain.ProviderJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cats_Program.Data.API
{
    public class Fact_API_Client
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://meowfacts.herokuapp.com/";

        public Fact_API_Client()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<string> GetFact()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("?lang=ukr");

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    var factData = JsonConvert.DeserializeObject<JsonProvider>(responseString);

                    if (factData != null && factData.Data.Count > 0)
                    {
                        string fact = factData.Data[0];
                        return fact;
                    }
                    else
                    {
                        throw new Exception("Отсутствуют данные факта кота.");
                    }
                }
                else
                {
                    throw new HttpRequestException("Не удалось получить fact кота. Код статуса: " + response.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }
}
