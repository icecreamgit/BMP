using System.Security.Cryptography.Xml;



namespace SuperWeather.Contracts.ContractHelpClasses
{
    public class ConvertUrl
    {
        public string url { get; set; }
        public string geocodeUrl { get; set; }
        public ConvertUrl(string url, string geocodeurl)
        {
            this.url = url;
            this.geocodeUrl = geocodeurl;
        }

        public string TransformUrl(string key, List<Geolocation> coordinates)
        {
            string[] listDeletePositions = new string[] {
            "{lat}", "{lon}", "{part}", "{API key}" };
            url = url.Replace(listDeletePositions[0], coordinates[0].lat);
            url = url.Replace(listDeletePositions[1], coordinates[0].lon);
            //url = url.Replace(listDeletePositions[2], "hourly, daily");
            url = url.Replace(listDeletePositions[3], key);
            return url;
        }

        public string TransformGeocode(string key, string cityName)
        {
            string[] listDeletePositions = new string[] {
            "{city name},{state code},{country code}", "{limit}", "{API key}"};

            geocodeUrl = geocodeUrl.Replace(listDeletePositions[0], cityName);
            geocodeUrl = geocodeUrl.Replace(listDeletePositions[1], "1");
            geocodeUrl = geocodeUrl.Replace(listDeletePositions[2], key);
            return geocodeUrl;
        }
    }
}
