using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolarshipManagement.DataAccess.IRepository
{
   public interface ICourseRepository : IRepository<Course>
    {
        void Update(Course course);
    }
}
