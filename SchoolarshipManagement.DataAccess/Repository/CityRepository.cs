using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Repository
{
   public class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _db;

        public CityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(City city)
        {
            var cityFromDb = _db.City.FirstOrDefault(s => s.Id == city.Id);

            if (cityFromDb != null)
            {
                cityFromDb.Name = city.Name;
                cityFromDb.Code = city.Code;
                cityFromDb.CountryId = city.CountryId;


            }
        }
    }
}
