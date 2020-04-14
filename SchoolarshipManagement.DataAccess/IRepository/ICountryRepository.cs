using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolarshipManagement.DataAccess.IRepository
{
   public interface ICountryRepository : IRepository<Country>
    {
        void Update(Country country);
    }
}
