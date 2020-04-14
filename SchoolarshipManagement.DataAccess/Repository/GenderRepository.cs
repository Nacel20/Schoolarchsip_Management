using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Repository
{
   public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        private readonly ApplicationDbContext _db;

        public GenderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Gender gender)
        {
            var genderFromDb = _db.Genders.FirstOrDefault(s => s.Id == gender.Id);

            if (genderFromDb != null)
            {
                genderFromDb.Name = gender.Name;
                

            }
        }
    }
}
