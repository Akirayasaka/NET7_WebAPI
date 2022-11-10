namespace Main.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IVillaRepository Villa { get; }
        void Save();
    }
}
