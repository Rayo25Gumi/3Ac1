using Newtonsoft.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        string url = "https://api.wheretheiss.at/v1/satellites/25544";
        using (var client = new HttpClient())
        {
            HttpResponseMessage response = client.GetAsync(url).Result;
            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            var pokeApiObject = JsonConvert.DeserializeObject<Iss>(jsonResponse);

            double Latitud = issApiObject.Latitud;
            double Longitud = pokeApiObject.growth_time;
            Console.WriteLine(name);
            Console.WriteLine(size);
        }
    }

    private class Iss
    {
        [JsonProperty("latitude")]
        public string Latitud { get; set; }

        [JsonProperty("longitude")]
        public string Longitud { get; set; }
    }
}
