using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolarshipManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolarshipManagement.Models.ViewModels
{
    public class CityVM
    {
        public City City { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
