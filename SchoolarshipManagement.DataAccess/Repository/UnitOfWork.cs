using SchoolarshipManagement.DataAccess.Data;
using SchoolarshipManagement.DataAccess.IRepository;

namespace SchoolarshipManagement.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            StoreProcedureRepository = new StoreProcedureRepository(_db);
            StudentRepository = new StudentRepository(_db);
            GenderRepository = new GenderRepository(_db);
            CountryRepository = new CountryRepository(_db);
            CityRepository = new CityRepository(_db);
            CourseRepository = new CourseRepository(_db);
            FiliereRepository = new FiliereRepository(_db);
            LevelRepository = new LevelRepository(_db);
            RegistrationRepository = new RegistrationRepository(_db);
            AcademicYearRepository = new AcademicYearRepository(_db);

        }

        public IStoreProcedureRepository StoreProcedureRepository { get; private set; }
        public IStudentRepository StudentRepository { get; private set; }
        public IGenderRepository GenderRepository { get; private set; }
        public ICountryRepository CountryRepository { get; private set; }
        public ICityRepository CityRepository { get; private set; }
        public ICourseRepository CourseRepository { get; private set; }
        public IFiliereRepository FiliereRepository { get; private set; }
        public ILevelRepository LevelRepository { get; private set; }
        public IAcademicYearRepository AcademicYearRepository { get; private set; }
        public IRegistrationRepository RegistrationRepository { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
