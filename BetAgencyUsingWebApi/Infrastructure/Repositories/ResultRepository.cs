using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly BetDbContext _context;

        public ResultRepository(BetDbContext context)
        {
            _context = context;
        }
        public void AddMatchResult(Result result)
        {
            try
            {
                if (result == null)
                {
                    throw new Exception("Result object is null");
                }

                _context.Results.Add(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while adding new result. {ex.Message}");
            }


        }
    }
}
