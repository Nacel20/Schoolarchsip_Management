using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolarshipManagement.Models.Models;
using System.Collections.Generic;

namespace SchoolarshipManagement.Models.ViewModels
{
    public class RegistrationVM
    {
        public Registration Registration { get; set; }
        public Student Student { get; set; }
        public IEnumerable<SelectListItem> LevelList { get; set; }
        public IEnumerable<SelectListItem> FiliereList { get; set; }
        public IEnumerable<SelectListItem> AcademicYearList { get; set; }
    }
}
