using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWorkStructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public INewsDataRepository NewsDataRepository { get; }
        public INewsApiClientRepository NewsApiClientRepository { get; }
        private readonly NewsDbContext _dbContext;

        public UnitOfWork(INewsDataRepository newsDataRepository,
            INewsApiClientRepository newsApiClientRepository,
            NewsDbContext dbContext)
        {
            NewsDataRepository = newsDataRepository;
            NewsApiClientRepository = newsApiClientRepository;
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
