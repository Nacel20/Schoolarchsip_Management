using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolarshipManagement.DataAccess.IRepository
{
   public interface ICityRepository : IRepository<City>
    {
        void Update(City city);
    }
}
