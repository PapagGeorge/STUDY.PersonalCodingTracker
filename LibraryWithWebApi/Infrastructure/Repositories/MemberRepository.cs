using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public void AddMember(Member member)
        {
            throw new NotImplementedException();
        }

        public bool CanMemberRentBooks(int memberId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetAllMembers()
        {
            throw new NotImplementedException();
        }

        public bool MemberExists(int memberId)
        {
            throw new NotImplementedException();
        }

        public void RemoveMemberRentability(int memberId)
        {
            throw new NotImplementedException();
        }

        public void RestoreMemberRentability(int memberId)
        {
            throw new NotImplementedException();
        }

        public void SoftDeleteMember(int memberId)
        {
            throw new NotImplementedException();
        }
    }
}
