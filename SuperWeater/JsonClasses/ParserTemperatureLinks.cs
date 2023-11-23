using System.Security.Principal;

namespace SuperWeather.JsonClasses
{
    public class ParserTemperatureLinks
    {
        public string name { get; set; }
        public string dt { get; set; }
        public string timeOnServer { get; set; }
        public string differenceTime { get; set; }
        public MainSaver Main { get; set; }
        public Speed Wind { get; set; }
        public All Clouds { get; set; }
    }
}
