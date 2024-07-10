using System.Text.Json.Serialization;

namespace ST_Testwork.Models
{
    public class AirportHttpResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("iata")]
        public string Code { get; set; }

        [JsonPropertyName("location")]
        public AirportCoordinates Coordinates { get; set; }
    }

    public class AirportCoordinates
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }
    }
}
