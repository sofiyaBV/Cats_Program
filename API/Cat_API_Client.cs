using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cats_Program.API
{
    public class Cat_API_Client
    {
        private readonly HttpClient _client;
        private const string BaseUrl = "https://cataas.com";

        public Cat_API_Client()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<byte[]> GetRandomCatImageAsync()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("/cat");

                if (response.IsSuccessStatusCode)
                {
                    byte[] catImageBytes = await response.Content.ReadAsByteArrayAsync();
                    return catImageBytes;
                }
                else
                {
                    throw new HttpRequestException("Не удалось получить изображение кота. Код статуса: " + response.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }
    }
}
