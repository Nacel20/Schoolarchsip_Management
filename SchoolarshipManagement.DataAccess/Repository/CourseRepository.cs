using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Repository
{
   public class CourseRepository : Repository<Course>, ICourseRepository
    {
        private readonly ApplicationDbContext _db;

        public CourseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Course course)
        {
            var courseFromDb = _db.Courses.FirstOrDefault(s => s.Id == course.Id);

            if (courseFromDb != null)
            {
                courseFromDb.Name = course.Name;
                courseFromDb.Description = course.Description;
            }
        }
    }
}
