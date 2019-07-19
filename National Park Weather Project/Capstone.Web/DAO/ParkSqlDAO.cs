using Capstone.Web.DAO.Interfaces;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public class ParkSqlDAO : IParkDAO
    {
        private string connectionString = "";

        private string sqlQuerySelectAllParks = "SELECT * FROM park";

        private string sqlSelectByParkCode = @"SELECT * FROM park 
                                            JOIN weather ON park.parkCode = weather.parkCode
                                            WHERE park.parkcode = @parkCode";

        public ParkSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Park> ListAllParks()
        {

            List<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlQuerySelectAllParks, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        parks.Add(MapRowToPark(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return parks;
        }

        public Park Detail(string id)
        {
            Park park = new Park();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlSelectByParkCode, connection);

                    command.Parameters.AddWithValue("@parkCode", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        park.ParkCode = Convert.ToString(reader["parkCode"]);
                        park.Name = Convert.ToString(reader["parkName"]);
                        park.State = Convert.ToString(reader["state"]);
                        park.Acreage = Convert.ToInt32(reader["acreage"]);
                        park.Elevation = Convert.ToInt32(reader["elevationInFeet"]);
                        park.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        park.Climate = Convert.ToString(reader["climate"]);
                        park.YearFounded = Convert.ToString(reader["yearFounded"]);
                        park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        park.Description = Convert.ToString(reader["parkDescription"]);
                        park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                        park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);


                    }

                }
            }
            catch (Exception)
            {

                throw;
            }


            return park;
        }

        public List<SelectListItem> GetSelectList()
        {
            List<Park> parks = ListAllParks();

            List<SelectListItem> selectList = new List<SelectListItem>();

            SelectListItem empty = new SelectListItem();

            empty.Text = "none";
            empty.Value = "";
            selectList.Add(empty);
            
            for (int i = 0; i < parks.Count; i++)
            {
                selectList.Add(new SelectListItem() { Text = parks[i].Name, Value = parks[i].ParkCode });
            }

            return selectList;
        }

        private Park MapRowToPark(SqlDataReader reader)
        {
            Park park = new Park();
            park.ParkCode = Convert.ToString(reader["parkCode"]);
            park.Name = Convert.ToString(reader["parkName"]);
            park.State = Convert.ToString(reader["state"]);
            park.Acreage = Convert.ToInt32(reader["acreage"]);
            park.Elevation = Convert.ToInt32(reader["elevationInFeet"]);
            park.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
            park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
            park.Climate = Convert.ToString(reader["climate"]);
            park.YearFounded = Convert.ToString(reader["yearFounded"]);
            park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
            park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
            park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
            park.Description = Convert.ToString(reader["parkDescription"]);
            park.EntryFee = Convert.ToInt32(reader["entryFee"]);
            park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

            return park;
        }
    }
}
