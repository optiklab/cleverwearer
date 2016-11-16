using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
//using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;
using Phi.MobileWebApp.Helpers;
using Phi.MobileWebApp.Models;
using Phi.Repository;
using Phi.Repository.Enums;
using Phi.Repository.Services;
using Phi.Repository.Stores;
using System.Web;
using System;
using System.Linq;
using System.Collections.Generic;
using Phi.Models.Models;
using System.Text;

namespace Phi.MobileWebApp.Controllers
{
    //
    // [BotAuthentication]
    [AllowAnonymous]
    public class TigerController : BaseApiController
    {
        private const string NUMERIC_FORMAT = "{0:0.#}";
        private const string ZERO = "?";

        private readonly IDataStore _dataStore;
        private readonly ISuggestionService _suggestionService;
        private readonly IUserProfileStore _userProfileStore;
        private readonly IDataProvider _provider;

        public TigerController(IDataStore dataStore, ISuggestionService suggestionService, IUserProfileStore userProfileStore)
        {
            this._dataStore = dataStore;
            this._suggestionService = suggestionService;
            this._provider = new Phi.OpenWeatherMapProvider.DataProvider();
            this._userProfileStore = userProfileStore;
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message == null)
            {
                _GatherStatistics("Null request");
            }

            _GatherStatistics(message.Text);

            return HandleSystemMessage(message);
        }

        private void _GatherStatistics(string message)
        {
            try
            {
                this._dataStore.Insert(new UserStat
                {
                    ActionPage = "Tiger API",
                    Action = message,
                    UserId = "None",
                    UserName = "None",
                    UserEmail = "None",
                    Browser = "None",
                    DateTime = DateTime.UtcNow,
                    IPAddress = null,
                    UrlReferrer = "None"
                });
            }
            catch
            {

            }
        }

        private Message HandleSystemMessage(Message message)
        {
            string cityName = HttpUtility.UrlDecode(message.Text);

            string replyText = string.Empty;
            try
            {
                string woeid = GetWoeid(cityName);

                var forecasts = this._provider.GetWeatherForecastByWoeid(woeid, _provider.GetUnitsName(Units.Imperial));

                var selectedConditions = forecasts.FirstOrDefault(x => !x.IsForecast);

                replyText = _Convert(forecasts);
            }
            catch (Exception ex)
            {
                replyText = ex.Message + " " + ex.StackTrace;
            }

            Message reply = message.CreateReplyMessage();
            reply.Type = "Ping";
            reply.Text = replyText;

            return reply;
        }

        private string GetWoeid(string cityName)
        {
            try
            {
                var locations = this._dataStore.GetLocationsByCity(cityName);

                if (locations != null && locations.Any())
                {
                    Phi.Models.Models.Location location = locations.First();

                    return location.WOEID;
                }
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        private string _Convert(IEnumerable<Phi.Repository.External.WeatherCondition> forecasts)
        {
            var result = new StringBuilder();

            Phi.Repository.External.WeatherCondition currentConditions = forecasts.FirstOrDefault(x => !x.IsForecast);
            var iconName = this._dataStore.GetConditionDescriptionByExtId(currentConditions.Condition.Value, 1);

            if (currentConditions.ForecastDate.HasValue)
            {
                result.AppendLine("Forecast for " + currentConditions.ForecastDate.Value.ToString() + " in " + currentConditions.City);
            }

            if (iconName != null)
            {
                result.AppendLine(iconName.ShortDescription);
            }

            var unit = _dataStore.GetUnitByName(UnitSystems.USA.ToString(), 1);

            var temperatureMax = currentConditions.TemperatureMax.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.TemperatureMax.Value) : ZERO;
            var temperatureMin = currentConditions.TemperatureMin.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.TemperatureMin.Value) : ZERO;
            var effectiveTemperature = currentConditions.EffectiveTemperature.HasValue ? String.Format(NUMERIC_FORMAT, currentConditions.EffectiveTemperature.Value) : ZERO;

            if (currentConditions.Temperature.HasValue)
            {
                result.AppendLine(string.Format(NUMERIC_FORMAT, currentConditions.Temperature.Value) + " " + unit.Temperature);
            }
            else
            {
                // Calculate middle temperature.
                if (currentConditions.TemperatureMax.HasValue && currentConditions.TemperatureMin.HasValue)
                {
                    result.AppendLine(string.Format(NUMERIC_FORMAT, Convert.ToInt32(((currentConditions.TemperatureMax.Value + currentConditions.TemperatureMin.Value) / 2))) + " " + unit.Temperature);
                }
                else
                {
                    result.AppendLine(string.Format(NUMERIC_FORMAT, "0 " + unit.Temperature));
                }
            }

            if (currentConditions.WindSpeed.HasValue)
            {
                result.AppendLine("Wind: " + string.Format(NUMERIC_FORMAT, currentConditions.WindSpeed.Value) + " " + unit.Speed);

                if (currentConditions.WindDirection.HasValue)
                {
                    result.AppendLine("Wind direction: " + Phi.MobileWebApp.Helpers.CommonHelpers.GetWindDirectionByAngle(currentConditions.WindDirection.Value));
                }
            }

            if (currentConditions.AthmosphereHumidity.HasValue)
            {
                result.AppendLine("Athmosphere humidity: " + String.Format(NUMERIC_FORMAT, currentConditions.AthmosphereHumidity.Value) + " " + unit.Humidity);
            }

            if (currentConditions.AthmospherePressure.HasValue)
            {
                result.AppendLine("Athmosphere pressure: " + String.Format(NUMERIC_FORMAT, currentConditions.AthmospherePressure.Value) + " " + unit.Pressure);
            }

            if (currentConditions.AthmosphereVisibility.HasValue)
            {
                result.AppendLine("Athmosphere visibility: " + String.Format(NUMERIC_FORMAT, currentConditions.AthmosphereVisibility.Value) + " " + unit.Distance);
            }

            if (!string.IsNullOrEmpty(currentConditions.ForecastDateString))
            {
                result.AppendLine(currentConditions.ForecastDateString);
            }

            if (!string.IsNullOrEmpty(currentConditions.FullDescription))
            {
                result.AppendLine(currentConditions.FullDescription);
            }

            result.AppendLine("Precipitation: " + currentConditions.Precipitation.ToString() + " " + unit.Distance);

            // Fill values for forecast dates.
            if (forecasts != null)
            {
                foreach (var forecast in forecasts)
                {
                    if (forecast.ForecastDate.HasValue)
                    {
                        result.AppendLine(forecast.ForecastDateString + " " + forecast.ForecastDate.Value);
                    }
                }
            }

            return result.ToString();
        }
    }
}
