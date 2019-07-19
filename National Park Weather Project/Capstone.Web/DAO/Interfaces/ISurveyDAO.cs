using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO.Interfaces
{
    public interface ISurveyDAO
    {
        bool SaveSurvey(Survey survey);
        List<Park> GetSurveyParks();
    }
}
