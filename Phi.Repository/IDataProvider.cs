using System.Collections.Generic;
using Phi.Models.Models;
using Phi.Repository.Enums;

namespace Phi.Repository
{
    public interface IDataProvider
    {
        string ProviderName { get; }

        string GetUnitsName(Units units);

        Location GetCityLocationByWoeid(string woeid);
        Location GetCityLocation(string cityName);
        List<Location> GetCityLocations(string cityName);
        IEnumerable<Phi.Repository.External.WeatherCondition> GetWeatherForecastByWoeid(string woeid, string system);
    }
}
