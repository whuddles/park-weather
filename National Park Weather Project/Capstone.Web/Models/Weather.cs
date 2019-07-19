using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.Models
{
    public class Weather
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        public int Low { get; set; }
        public int High { get; set; }
        public string Forecast { get; set; }
        

        public List<String> Recommendations()
        {
            List<string> recommendations = new List<string>();

            if (Forecast == "snow")
            {
                recommendations.Add("Pack snow shoes.");
            }
            if (Forecast == "rain")
            {
                recommendations.Add("Pack rain gear and waterproof shoes.");
            }
            if (Forecast == "thunderstorms")
            {
                recommendations.Add("Seek shelter and avoid hiking on exposed ridges.");
            }
            if (Forecast == "sunny")
            {
                recommendations.Add("Pack sunblock.");
            }
            if (High > 75)
            {
                recommendations.Add("Bring an extra gallon of water.");
            }
            if (High - Low > 20)
            {
                recommendations.Add("Wear breathable layers.");
            }
            if (Low < 20)
            {
                recommendations.Add("Beware of exposure to frigid temperatures.");
            }

            return recommendations;
        }
    }
}
