using API.Contracts;
using API.Data;
using API.Utilities.Handlers;

namespace API.Repositories
{
    // Defines a generic class GeneralRepository that implements the IGeneralRepository interface
    public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class
    {
        // Declares a protected field _context of type OvertimeDbContext
        protected readonly OvertimeDbContext _context;

        // Defines a protected constructor for the GeneralRepository class
        protected GeneralRepository(OvertimeDbContext context)
        {
            _context = context;
        }

        // Defines a public method GetAll() that returns an IEnumerable<TEntity> object
        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        // Defines a public method GetByGuid(Guid guid) that takes a Guid parameter
        // Returns an object of type TEntity
        public TEntity? GetByGuid(Guid guid)
        {
            var entity = _context.Set<TEntity>().Find(guid);
            _context.ChangeTracker.Clear(); 
            return entity;
        }

        // Defines a public method Create(TEntity entity) that takes an object of type TEntity as a parameter
        // Returns an object of type TEntity.
        public TEntity? Create(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity); 
                _context.SaveChanges(); 
                return entity;
            }
            catch (Exception ex)
            {
                // Exception for Nik, Email, and Phone Number employees if already exist.
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_nik"))
                {
                    throw new ExceptionHandler("NIK already exists");
                }
                if (ex.InnerException is not null && ex.InnerException.Message.Contains("IX_tb_m_employees_email"))
                {
                    throw new ExceptionHandler("Email already exists");
                }
                if (ex.InnerException != null && ex.InnerException.Message.Contains("IX_tb_m_employees_phone_number"))
                {
                    throw new ExceptionHandler("Phone number already exists");
                }
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }

        // Defines a public method Update(TEntity entity) that takes an object of type TEntity as a parameter
        // Returns a boolean value
        public bool Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity); 
                _context.SaveChanges(); 
                return true; 
            }
            catch (Exception ex)
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }

        // Defines a public method Delete(TEntity entity) that takes an object of type TEntity as a parameter
        // Returns a boolean value
        public bool Delete(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity); 
                _context.SaveChanges(); 
                return true; 
            }
            catch (Exception ex)
            {
                throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
