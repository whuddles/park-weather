using Capstone.Web.DAO.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO
{
    public class SurveySqlDAO : ISurveyDAO
    {
        private string connectionString = "";

        private string sqlQuerySaveSurvey = "INSERT INTO survey_result VALUES (@parkCode, @emailAddress, @state, @activityLevel)";

        private string sqlQuerySelectSurveyParks = @"SELECT COUNT(survey_result.parkCode) as surveyCount, survey_Result.parkCode, park.parkName, park.parkDescription  
                                                    FROM survey_result
                                                    JOIN park ON park.parkCode = survey_result.parkCode  
                                                    GROUP BY survey_result.parkCode, park.parkName, park.parkDescription
                                                    HAVING COUNT(survey_result.parkCode) > 0 
                                                    ORDER BY COUNT(survey_result.parkCode) desc";

        public SurveySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool SaveSurvey(Survey survey)
        {
            bool result = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlQuerySaveSurvey, connection);

                    command.Parameters.AddWithValue("@parkCode", survey.FavoriteNationalPark);
                    command.Parameters.AddWithValue("@emailAddress", survey.Email);
                    command.Parameters.AddWithValue("@state", survey.State);
                    command.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public List<Park> GetSurveyParks()
        {
            List<Park> parks = new List<Park>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlQuerySelectSurveyParks, connection);

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


        private Park MapRowToPark(SqlDataReader reader)
        {
            Park park = new Park();

            park.Name = Convert.ToString(reader["parkName"]);  
            park.Description = Convert.ToString(reader["parkDescription"]);
            park.SurveyCount = Convert.ToInt32(reader["surveyCount"]);
            park.ParkCode = Convert.ToString(reader["parkCode"]);

            return park;
        }
    }
}
