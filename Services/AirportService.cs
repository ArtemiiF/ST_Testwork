using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ST_Testwork.Interfaces;
using ST_Testwork.Models;
using System;
using System.Threading.Tasks;

namespace ST_Testwork.Services
{
    public class AirportService : IAirportService
    {
        private readonly IHttpClientService httpClientService;
        private readonly IConfiguration configuration;
        private IMemoryCache cache;

        public AirportService(IHttpClientService httpClientService,
            IConfiguration configuration,
            IMemoryCache memoryCache)
        {
            this.httpClientService = httpClientService;
            this.configuration = configuration;
            this.cache = memoryCache;
        }

        public async Task<double> GetDistanceBetweenTwoAirportsInMilesAsync(string firstAirportIATACode, string secondAirportIATACode)
        {
            var url = configuration.GetValue("AirportSearcherUrl", string.Empty);

            AirportCoordinates firstAirportCoordinates;

            AirportCoordinates secondAirportCoordinates;

            if (url == string.Empty)
            {
                throw new Exception("Url for airport search doesnt set");
            }

            if (cache.TryGetValue(firstAirportIATACode, out AirportCoordinates first))
            {
                firstAirportCoordinates = first;
            }
            else
            {
                var firstAirportResponse = await httpClientService.GetAsync<AirportHttpResponse>($"{url}{firstAirportIATACode}");
                firstAirportCoordinates = firstAirportResponse.Value.Coordinates;

                if (!firstAirportResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"Airport with IATA {firstAirportIATACode} not found");
                }
                cache.Set(firstAirportIATACode, firstAirportResponse.Value.Coordinates);
            }


            if (cache.TryGetValue(secondAirportIATACode, out AirportCoordinates second))
            {
                secondAirportCoordinates = second;
            }
            else
            {
                var secondAirportResponse = await httpClientService.GetAsync<AirportHttpResponse>($"{url}{secondAirportIATACode}");
                secondAirportCoordinates = secondAirportResponse.Value.Coordinates;

                if (!secondAirportResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"Airport with IATA {secondAirportIATACode} not found");
                }
                cache.Set(secondAirportIATACode, secondAirportResponse.Value.Coordinates);
            }


            return CalculateDistanceBetweenTwoAirportsInMiles(firstAirportCoordinates, secondAirportCoordinates);
        }

        #region Private methods

        private double CalculateDistanceBetweenTwoAirportsInMiles(AirportCoordinates first, AirportCoordinates second)
        {
            var rFromLatitude = Math.PI * first.Latitude / 180;
            var rToLatitude = Math.PI * second.Latitude / 180;
            var theta = first.Longitude - second.Longitude;
            var rTheta = Math.PI * theta / 180;

            var distInMiles =
                Math.Sin(rFromLatitude) * Math.Sin(rToLatitude) + Math.Cos(rFromLatitude) *
                Math.Cos(rToLatitude) * Math.Cos(rTheta);

            distInMiles = Math.Acos(distInMiles);
            distInMiles = distInMiles * 180 / Math.PI;
            distInMiles = distInMiles * 60 * 1.1515;


            return distInMiles;
        }

        #endregion
    }
}
