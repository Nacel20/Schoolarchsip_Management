using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolarshipManagement.DataAccess.IRepository
{
    public interface IUnitOfWork : IDisposable    {
        
        IStoreProcedureRepository StoreProcedureRepository { get; }
        IStudentRepository StudentRepository { get; }
        IGenderRepository GenderRepository { get; }
        ICountryRepository CountryRepository { get; }
        ICityRepository CityRepository { get; }
        ICourseRepository CourseRepository { get; }
        IFiliereRepository FiliereRepository { get; }
        ILevelRepository LevelRepository { get; }
        IAcademicYearRepository AcademicYearRepository { get; }
        IRegistrationRepository RegistrationRepository { get; }

        void Save();
    }
}
