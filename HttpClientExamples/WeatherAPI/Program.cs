using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Weather
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetWeather().GetAwaiter().GetResult();
        }

        public static async Task GetWeather()
        {
            using var client = new HttpClient();
            var baseUrl = "https://api.openweathermap.org/data/2.5/weather";
            var latitude = 37.99M;
            var longtitute = 23.91M;
            var apiKey = "71f468c6b598871b2307ad2457a5952e";

            var url = $"{baseUrl}?lat={latitude}&lon={longtitute}&appid={apiKey}";

            try
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(responseBody, options);

                    Console.WriteLine("Weather Data:");
                    Console.WriteLine($"Location: {weatherResponse.Name}");
                    Console.WriteLine($"Coordinates: Latitude - {weatherResponse.Coord.Lat}, Longitude - {weatherResponse.Coord.Lon}");
                    Console.WriteLine($"Base: {weatherResponse.Base}");
                    Console.WriteLine($"Visibility: {weatherResponse.Visibility}");
                    Console.WriteLine($"Timezone: {weatherResponse.Timezone}");
                    Console.WriteLine($"ID: {weatherResponse.Id}");
                    Console.WriteLine($"Cod: {weatherResponse.Cod}");
                    Console.WriteLine("Weather:");
                    foreach (var weather in weatherResponse.Weather)
                    {
                        Console.WriteLine($"ID: {weather.Id}");
                        Console.WriteLine($"Main: {weather.Main}");
                        Console.WriteLine($"Description: {weather.Description}");
                        Console.WriteLine($"Icon: {weather.Icon}");
                    }
                    Console.WriteLine("Main:");
                    Console.WriteLine($"Temperature: {weatherResponse.Main.Temp}");
                    Console.WriteLine($"Feels Like: {weatherResponse.Main.FeelsLike}");
                    Console.WriteLine($"Minimum Temperature: {weatherResponse.Main.TempMin}");
                    Console.WriteLine($"Maximum Temperature: {weatherResponse.Main.TempMax}");
                    Console.WriteLine($"Pressure: {weatherResponse.Main.Pressure}");
                    Console.WriteLine($"Humidity: {weatherResponse.Main.Humidity}");
                    Console.WriteLine($"Sea Level: {weatherResponse.Main.SeaLevel}");
                    Console.WriteLine($"Ground Level: {weatherResponse.Main.GrndLevel}");
                    Console.WriteLine("Wind:");
                    Console.WriteLine($"Speed: {weatherResponse.Wind.Speed}");
                    Console.WriteLine($"Degree: {weatherResponse.Wind.Deg}");
                    Console.WriteLine($"Gust: {weatherResponse.Wind.Gust}");
                    Console.WriteLine($"Rain (1h): {weatherResponse.Rain?.OneHour}");
                    Console.WriteLine($"Cloudiness: {weatherResponse.Clouds.All}");
                    Console.WriteLine("Sys:");
                    Console.WriteLine($"Type: {weatherResponse.Sys.Type}");
                    Console.WriteLine($"ID: {weatherResponse.Sys.Id}");
                    Console.WriteLine($"Country: {weatherResponse.Sys.Country}");
                    Console.WriteLine($"Sunrise: {weatherResponse.Sys.Sunrise}");
                    Console.WriteLine($"Sunset: {weatherResponse.Sys.Sunset}");
                }
                else
                {
                    Console.WriteLine($"Failed to retrieve weather data. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }



    public class Coord
    {
        [JsonPropertyName("lon")]
        public double Lon { get; set; }
        [JsonPropertyName("lat")]
        public double Lat { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("main")]
        public string Main { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }
    }

    public class Main
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }
        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }
        [JsonPropertyName("temp_min")]
        public double TempMin { get; set; }
        [JsonPropertyName("temp_max")]
        public double TempMax { get; set; }
        [JsonPropertyName("pressure")]
        public int Pressure { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
        [JsonPropertyName("sea_level")]
        public int SeaLevel { get; set; }
        [JsonPropertyName("grnd_level")]
        public int GrndLevel { get; set; }
    }

    public class Wind
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }
        [JsonPropertyName("deg")]
        public int Deg { get; set; }
        [JsonPropertyName("gust")]
        public double Gust { get; set; }
    }

    public class Rain
    {
        [JsonPropertyName("1h")]
        public double OneHour { get; set; }
    }

    public class Clouds
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }

    public class Sys
    {
        [JsonPropertyName("type")]
        public int Type { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("sunrise")]
        public int Sunrise { get; set; }
        [JsonPropertyName("sunset")]

        public int Sunset { get; set; }
    }

    public class WeatherResponse
    {
        [JsonPropertyName("coord")]
        public Coord Coord { get; set; }
        [JsonPropertyName("weather")]
        public List<Weather> Weather { get; set; }
        [JsonPropertyName("base")]
        public string Base { get; set; }
        [JsonPropertyName("main")]
        public Main Main { get; set; }
        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }
        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }
        [JsonPropertyName("rain")]
        public Rain Rain { get; set; }
        [JsonPropertyName("clouds")]
        public Clouds Clouds { get; set; }
        [JsonPropertyName("dt")]
        public int Dt { get; set; }
        [JsonPropertyName("sys")]
        public Sys Sys { get; set; }
        [JsonPropertyName("timezone")]
        public int Timezone { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("cod")]
        public int Cod { get; set; }
    }

}
