namespace Main.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IVillaRepository Villa { get; }
        IVillaNumberRepository VillaNumber { get; }

        IUserRepository User { get; }
        void Save();
    }
}
