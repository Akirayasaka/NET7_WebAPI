using Main.Data;
using Main.Repository.IRepository;
using Microsoft.Extensions.Configuration;

namespace Main.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Villa = new VillaRepository(_db);
            VillaNumber= new VillaNumberRepository(_db);
            User = new UserRepository(_db, _configuration);
        }

        public IVillaRepository Villa { get; private set; }
        public IVillaNumberRepository VillaNumber { get; private set; }
        public IUserRepository User { get; private set; }

        public void Dispose() => _db.Dispose();

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
