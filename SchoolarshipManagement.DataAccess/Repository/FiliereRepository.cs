using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Repository
{
   public class FiliereRepository : Repository<Filiere>, IFiliereRepository
    {
        private readonly ApplicationDbContext _db;

        public FiliereRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Filiere filiere)
        {
            var filiereFromDb = _db.Filieres.FirstOrDefault(s => s.Id == filiere.Id);

            if (filiereFromDb != null)
            {
                filiereFromDb.Name = filiere.Name;
                filiereFromDb.Code = filiere.Code;
                filiereFromDb.Description = filiere.Description;
            }
        }
    }
}
