using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PiServer
{
    class Weather
    {
        public string City { get; set; }
        public string Postcode { get; set; }
        public string WeatherDesc { get; set; }
        public string TempC {get; set; }
        public string Humidity {get; set;}
        public string WindDesc {get; set;}
        public string WindSpeed {get; set;}
    }
    static class Wunderground
    {
        public static Weather GetWeather(string postcode)
        {
            string API_Key = "078b7bd311a5dd63";
            Weather result = new Weather();
            string query = String.Format("http://api.wunderground.com/api/{0}/conditions/q/{1}.xml", API_Key, postcode);
            XDocument doc = XDocument.Load(query);
            if (doc.Descendants("current_observation").Count() > 0)
            {
                System.Collections.Generic.IEnumerable<XElement> display_location = (from i in doc.Descendants("current_observation") select i.Element("display_location"));
                result.City = display_location.Descendants("city").First().Value;
                result.Postcode = postcode;
                result.WeatherDesc = (from i in doc.Descendants("current_observation") select i.Element("weather").Value).First();
                result.TempC = (from i in doc.Descendants("current_observation") select i.Element("temp_c").Value).First();
                result.Humidity = (from i in doc.Descendants("current_observation") select i.Element("relative_humidity").Value).First();
                result.WindDesc = (from i in doc.Descendants("current_observation") select i.Element("wind_string").Value).First();
                result.WindSpeed = (from i in doc.Descendants("current_observation") select i.Element("wind_gust_mph").Value).First();
                return result;
            }
            throw new Exception("No weather data returned");
        }
    }
}
