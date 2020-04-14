using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Repository
{
   public class AcademicYearRepository : Repository<AcademicYear>, IAcademicYearRepository
    {
        private readonly ApplicationDbContext _db;
        public AcademicYearRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(AcademicYear academicYear)
        {
            var academicYearFromDb = _db.AcademicYears.FirstOrDefault(s => s.Id == academicYear.Id);
            if (academicYearFromDb != null)
            {
                academicYearFromDb.Code = academicYear.Code.Trim();
                academicYearFromDb.Description = academicYear.Description;
            }
        }
    }
}
