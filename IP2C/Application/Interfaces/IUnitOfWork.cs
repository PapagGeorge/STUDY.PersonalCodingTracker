namespace Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IAssignmentRepository AssignmentRepository { get; }
    }
}
