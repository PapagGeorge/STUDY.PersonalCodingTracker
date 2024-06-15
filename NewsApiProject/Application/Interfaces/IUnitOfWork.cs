namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        INewsApiClientRepository NewsApiClientRepository { get; }
        INewsDataRepository NewsDataRepository { get; }
        Task SaveChangesAsync();
    }
}
