using Newtonsoft.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        string url = "https://api.wheretheiss.at/v1/satellites/25544";
        using (var client = new HttpClient())
        {
            string oldCountry = "a";
            do
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                var data = JsonConvert.DeserializeObject<IssData>(jsonResponse);

                double Latitud = data.Latitud;
                double Longitud = data.Longitud;

                Thread.Sleep(10000);

                string urlgeo =
                    $"http://api.geonames.org/countryCodeJSON?lat={Latitud}&lng={Longitud}&username=gumi";

                HttpResponseMessage responsegeo = client.GetAsync(urlgeo).Result;
                string jsonResponsegeo = responsegeo.Content.ReadAsStringAsync().Result;

                var datageo = JsonConvert.DeserializeObject<Geo>(jsonResponsegeo);
                string CountryName = datageo.CountryName;

                if (CountryName != oldCountry && CountryName != "La iis no está en ningún país")
                {
                    oldCountry = CountryName;
                    Console.WriteLine($"La iis ahora en {CountryName}");
                }
                else if (
                    CountryName != oldCountry
                    && CountryName == "La iis no está en ningún país"
                )
                {
                    oldCountry = CountryName;
                    Console.WriteLine(CountryName);
                }
            } while ("b" != "a");
        }
    }

    private class IssData
    {
        [JsonProperty("latitude")]
        public required double Latitud { get; set; }

        [JsonProperty("longitude")]
        public required double Longitud { get; set; }
    }

    private class Geo
    {
        [JsonProperty("CountryName", NullValueHandling = NullValueHandling.Ignore)] //esta linea se la pedí a chatgpt por si la iis estaba en unas cordenadas sin país :p
        public string CountryName { get; set; } = "La iis no está en ningún país";
    }
}
