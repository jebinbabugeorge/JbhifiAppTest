using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JbhifiApi.Services
{
    public interface IOpenWeatherMapService
    {
        Task<string> GetWeatherDetailsAsync(string city, string country);
    }
}
