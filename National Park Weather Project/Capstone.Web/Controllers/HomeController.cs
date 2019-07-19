using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAO.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IParkDAO parkDAO;
        private IWeatherDAO weatherDAO;

        public HomeController(IParkDAO parkDAO, IWeatherDAO weatherDAO)
        {
            this.parkDAO = parkDAO;
            this.weatherDAO = weatherDAO;
        }


        public IActionResult Index()
        {
            IList<Park> parks = parkDAO.ListAllParks();            

            return View(parks);
        }


        public IActionResult Detail(string id, string tempType)
        {
            string tempTypeSession = HttpContext.Session.GetString("tempTypeSession");

            if (string.IsNullOrEmpty(tempTypeSession))
            {
                HttpContext.Session.SetString("tempTypeSession", "Fahrenheit");
            }

            if (!string.IsNullOrEmpty(tempType))
            {
                HttpContext.Session.SetString("tempTypeSession", tempType);
            }

            tempTypeSession = HttpContext.Session.GetString("tempTypeSession");

            if (tempTypeSession == "Fahrenheit")
            {
                ViewData["tempType"] = "Fahrenheit";
            }

            if (tempTypeSession == "Celsius")
            {
                ViewData["tempType"] = "Celsius";
            }

            ParkPlusWeather parkPlusWeather = new ParkPlusWeather();
            parkPlusWeather.Park = parkDAO.Detail(id);
            parkPlusWeather.Weathers = weatherDAO.GetAllWeathers(id);

            return View(parkPlusWeather);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
