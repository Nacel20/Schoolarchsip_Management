using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolarshipManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolarshipManagement.Models.ViewModels
{
    public class StudentVM
    {
        public Student Student { get; set; }
        public IEnumerable<SelectListItem> GenderList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> CityList { get; set; }
    }
}
