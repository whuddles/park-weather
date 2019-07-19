using Capstone.Web.DAO.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public class WeatherSqlDAO : IWeatherDAO
    {
        private string connectionString = "";

        private string sqlQuerySelectWeatherForAPark = "SELECT * FROM weather WHERE parkCode = @parkCode";

        public WeatherSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Weather> GetAllWeathers(string id)
        {
            List<Weather> weathers = new List<Weather>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlQuerySelectWeatherForAPark, connection);
                    command.Parameters.AddWithValue("@parkCode", id);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        weathers.Add(MapRowToWeather(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return weathers;
        }
  
        

        private Weather MapRowToWeather(SqlDataReader reader)
        {
            Weather weather = new Weather();
            weather.ParkCode = Convert.ToString(reader["parkCode"]);
            weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
            weather.High = Convert.ToInt32(reader["high"]);
            weather.Low = Convert.ToInt32(reader["low"]);
            weather.Forecast = Convert.ToString(reader["forecast"]);
            

            return weather;
        }


    }
}
