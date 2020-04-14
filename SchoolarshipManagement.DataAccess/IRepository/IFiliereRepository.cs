using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolarshipManagement.DataAccess.IRepository
{
   public interface IFiliereRepository : IRepository<Filiere>
    {
        void Update(Filiere filiere);
    }
}
