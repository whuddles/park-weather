using Capstone.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAO.Interfaces
{
    public interface IParkDAO
    {
        List<Park> ListAllParks();
        Park Detail(string id);
        List<SelectListItem> GetSelectList();
    }
}
