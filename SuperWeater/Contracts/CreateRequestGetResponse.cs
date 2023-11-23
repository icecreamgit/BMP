using Newtonsoft.Json;
using System;
using System.Net;
using SuperWeather.JsonClasses;
using SuperWeather.Contracts.ContractHelpClasses;


namespace SuperWeather.Contracts
{
    public class CreateRequestGetResponse
    {
        private string url = "https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&appid={API key}";
        private string geocodeUrl = "http://api.openweathermap.org/geo/1.0/direct?q={city name},{state code},{country code}&limit={limit}&appid={API key}";
        string apiKey;
        
        public CreateRequestGetResponse(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public string API(string name)
        {
            ConvertUrl convertUrl = new ConvertUrl(url, geocodeUrl);

            geocodeUrl = convertUrl.TransformGeocode(apiKey, name);

            string response = ResultFromServer(geocodeUrl);
            List<Geolocation> info = JsonConvert.DeserializeObject<List<Geolocation>>(response);

            url = convertUrl.TransformUrl(apiKey, info);
            response = ResultFromServer(url);
            ParserTemperatureLinks parserTemperatureLinks = JsonConvert.DeserializeObject<ParserTemperatureLinks>(response);
            
            // Преобразование даты в строковый формат
            int timestamp = Int32.Parse(parserTemperatureLinks.dt);
            DateTime dateTime = new DateTime(1970, 1, 1).AddSeconds(timestamp);
            string date = dateTime.ToString();

            parserTemperatureLinks.dt = date;
            response = JsonConvert.SerializeObject(parserTemperatureLinks);



            return response;
        }
        private string ResultFromServer(string url)
        {
            HttpWebRequest httpwebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpwebRequest.GetResponse();

            string response;

            using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = sr.ReadToEnd();
            }
            return response;
        }
        private void ConsoleOutput(ParserTemperatureLinks parserTemperatureLinks)
        {
            

            Console.WriteLine($"City is\t{parserTemperatureLinks.name}");
            //Console.WriteLine($"Time is\t{date}");
            Console.WriteLine($"Temp:\t{parserTemperatureLinks.Main.temp}");
            Console.WriteLine($"Presure: {parserTemperatureLinks.Main.pressure}");
            Console.WriteLine($"Humidity is\t{parserTemperatureLinks.Main.humidity}");
            Console.WriteLine($"Wind speed is\t{parserTemperatureLinks.Wind.speed}");
            Console.WriteLine($"Clouds is\t{parserTemperatureLinks.Clouds.all}");
        }

    }
}
