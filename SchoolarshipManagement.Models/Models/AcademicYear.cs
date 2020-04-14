using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolarshipManagement.Models.Models
{
    public class AcademicYear
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        public string Description { get; set; }

    }
}
