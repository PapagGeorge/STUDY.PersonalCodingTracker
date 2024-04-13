using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMemberRepository
    {
        bool MemberExists(int memberId);
        void AddMember(Member member);
        void SoftDeleteMember(int memberId);
        IEnumerable<Member> GetAllMembers();
        void RemoveMemberRentability(int memberId);
        void RestoreMemberRentability(int memberId);
        bool CanMemberRentBooks(int memberId);




    }
}
