using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;
using SchoolarshipManagement.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolarshipManagement.DataAccess.Repository
{
   public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Student student)
        {
            var studentFromDb = _db.Students.FirstOrDefault(s => s.Id == student.Id);

            if (studentFromDb != null)
            {
                studentFromDb.FirstName = student.FirstName;
                studentFromDb.LastName = student.LastName;
                studentFromDb.Age = student.Age;
                studentFromDb.GenderId = student.GenderId;
                studentFromDb.CityId = student.CityId;
                studentFromDb.CountryId = student.CountryId;
                studentFromDb.PhoneNumber = student.PhoneNumber;
                studentFromDb.Email = student.Email;
                studentFromDb.Address = student.Address;

                if (studentFromDb.ImageUrl != null)
                {
                    studentFromDb.ImageUrl = student.ImageUrl;
                }

            }
        }
    }
}
