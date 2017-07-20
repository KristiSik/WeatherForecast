using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.Models;

namespace WeatherForecast.Json
{
    public interface IForecastService
    {
        Task<Forecast> GetJsonFromUrl(SearchCity city);
    }
}
