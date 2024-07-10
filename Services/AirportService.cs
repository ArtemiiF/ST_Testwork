using ST_Testwork.Interfaces;
using ST_Testwork.Models;
using System;
using System.Threading.Tasks;

namespace ST_Testwork.Services
{
    public class AirportService : IAirportService
    {
        private readonly IHttpClientService httpClientService;
        public AirportService(IHttpClientService httpClientService)
        {
            this.httpClientService = httpClientService;
        }

        public async Task<double> GetDistanceBetweenTwoAirportsInMilesAsync(string firstAirportIATACode, string secondAirportIATACode)
        {
            var firstAirportResponse = await httpClientService.GetAsync<AirportHttpResponse>($"https://places-dev.cteleport.com/airports/{firstAirportIATACode}");

            if(!firstAirportResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Airport with IATA {firstAirportIATACode} not found");
            }

            var secondAirportResponse = await httpClientService.GetAsync<AirportHttpResponse>($"https://places-dev.cteleport.com/airports/{secondAirportIATACode}");

            if (!firstAirportResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Airport with IATA {secondAirportIATACode} not found");
            }

            return CalculateDistanceBetweenTwoAirportsInMiles(firstAirportResponse.Value.Coordinates, secondAirportResponse.Value.Coordinates);
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
