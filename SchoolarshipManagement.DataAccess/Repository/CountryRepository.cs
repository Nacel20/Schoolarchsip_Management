using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Repository
{
   public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _db;

        public CountryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Country country)
        {
            var countryFromDb = _db.Countries.FirstOrDefault(s => s.Id == country.Id);

            if (countryFromDb != null)
            {
                countryFromDb.Name = country.Name;
                countryFromDb.Code = country.Code;


            }
        }
    }
}
