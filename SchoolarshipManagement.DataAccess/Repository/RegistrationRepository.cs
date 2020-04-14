using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Repository
{
   public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
    {
        private readonly ApplicationDbContext _db;
        public RegistrationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Registration registration)
        {
            var registrationFromDb = _db.Registrations.FirstOrDefault(s => s.Id == registration.Id);

            if (registrationFromDb != null)
            {
                registrationFromDb.AcademicYearId = registration.AcademicYearId;
                registrationFromDb.LevelId = registration.LevelId;
                registrationFromDb.StudentId = registration.StudentId;
                registrationFromDb.FiliereId = registration.FiliereId;
                registrationFromDb.RegistrationAmount = registration.RegistrationAmount;
            }
        }
    }
}
