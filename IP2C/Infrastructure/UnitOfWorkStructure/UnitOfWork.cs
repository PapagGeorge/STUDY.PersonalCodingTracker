using Application.Interfaces;
using Infrastructure.DatabaseContext;

namespace Infrastructure.UnitOfWorkStructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAssignmentRepository AssignmentRepository { get; }
        private readonly ApplicationDbContext _context;
        

        public UnitOfWork(IAssignmentRepository assignmentRepository, ApplicationDbContext context)
        {
            AssignmentRepository = assignmentRepository ?? throw new ArgumentNullException(nameof(assignmentRepository));
            _context = context;
        }
    }
    
}
