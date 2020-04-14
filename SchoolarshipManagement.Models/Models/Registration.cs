using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SchoolarshipManagement.Models.Models
{
    public class Registration
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public decimal RegistrationAmount { get; set; }

        [Required]
        public int LevelId { get; set; }

        [ForeignKey("LevelId")]
        public Level Level { get; set; }

        [Required]
        public int FiliereId { get; set; }

        [ForeignKey("FiliereId")]
        public Filiere Filiere { get; set; }

        [Required]
        public int AcademicYearId { get; set; }

        [ForeignKey("AcademicYearId")]
        public AcademicYear AcademicYear { get; set; }

        [Required]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }


    }
}
