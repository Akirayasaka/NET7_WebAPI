using Main.Data;
using Main.Repository.IRepository;

namespace Main.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Villa = new VillaRepository(_db);
            VillaNumber= new VillaNumberRepository(_db);
        }

        public IVillaRepository Villa { get; private set; }
        public IVillaNumberRepository VillaNumber { get; private set; }

        public void Dispose() => _db.Dispose();

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
