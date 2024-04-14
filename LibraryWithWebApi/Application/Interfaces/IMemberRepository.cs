using Domain.Entities;
using System.Collections;

namespace Application.Interfaces
{
    public interface IMemberRepository
    {
        bool MemberExists(int memberId);
        void AddMember(Member member);
        void SoftDeleteMember(int memberId);
        IEnumerable<Member> GetAllActiveMembers();
        void RemoveMemberRentability(int memberId);
        void RestoreMemberRentability(int memberId);
        bool CanMemberRentBooks(int memberId);
        int BooksOwedByMemberCount(int memberId);
        IEnumerable<Book> BooksOwedByMember(int memberId);
        bool isMemberDeleted(int memberId);




    }
}
