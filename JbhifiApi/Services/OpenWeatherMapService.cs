using JbhifiApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JbhifiApi.Services
{
    public class OpenWeatherMapService : IOpenWeatherMapService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private string Url { get; set; }

        private string ApiKey { get; set; }

        public OpenWeatherMapService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;

            Url = configuration.GetValue<string>("OpenWeatherMap:Url");
            ApiKey = configuration.GetValue<string>("OpenWeatherMap:ApiKey");
        }

        public async Task<string> GetWeatherDetailsAsync(string city, string country)
        {
            var httpClient = _httpClientFactory.CreateClient("OpenWeatherMap");

            var response = await httpClient.GetAsync(string.Format(Url, city, country, ApiKey));

            if(!response.IsSuccessStatusCode)
            {
                return "We couldn't find weather forecast for the given city and country. Please double check the city and country name and try again.";
            }

            using var contentStream = await response.Content.ReadAsStreamAsync();

            var result = await JsonSerializer.DeserializeAsync<Root>(contentStream);

            var description = result?.weather?.Select(x => x.description).FirstOrDefault();

            if (!string.IsNullOrEmpty(description))
                return $"The weather forecast for {city}, {country} is {description}";

            return $"The weather forecast for {city}, {country} is not available at the moment. Please try again later.";
        }
    }
}
