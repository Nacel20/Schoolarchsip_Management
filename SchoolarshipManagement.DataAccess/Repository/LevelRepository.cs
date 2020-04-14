using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Repository
{
   public class LevelRepository : Repository<Level>, ILevelRepository
    {
        private readonly ApplicationDbContext _db;

        public LevelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Level level)
        {
            var levelFromDb = _db.Levels.FirstOrDefault(s => s.Id == level.Id);

            if (levelFromDb != null)
            {
                levelFromDb.Name = level.Name;
                levelFromDb.Description = level.Description;
            }
        }
    }
}
