using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {

        private readonly LibraryDbContext _context;

        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public void AddMember(Member member)
        {
            try
            {
                _context.Members.Add(member);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while adding new member. {ex.Message}");
            }
        }

        public bool CanMemberRentBooks(int memberId)
        {
            try
            {
                var member = _context.Members.FirstOrDefault(member => member.MemberId == memberId);
                if(member.CanRent == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to check if member with Id: {memberId}" +
                    $"can rent books. {ex.Message}");
            }
        }

        public IEnumerable<Member> GetAllMembers()
        {
            try
            {
                return _context.Members;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to find all members. {ex.Message}");
            }

        }

        public bool MemberExists(int memberId)
        {
            try
            {
                return _context.Members.Any(member => member.MemberId == memberId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to find id member with Id: {memberId} exists. {ex.Message}");
            }
        }

        public void RemoveMemberRentability(int memberId)
        {
            try
            {
                var member = _context.Members.FirstOrDefault(member => member.MemberId == memberId);
                member.CanRent = false;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while removing member with Id: {memberId} the ability to rent. {ex.Message}");
            }
        }

        public void RestoreMemberRentability(int memberId)
        {
            try
            {
                var member = _context.Members.FirstOrDefault(member => member.MemberId == memberId);
                member.CanRent = true;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while restoring member with Id: {memberId} the ability to rent. {ex.Message}");
            }
        }

        public void SoftDeleteMember(int memberId)
        {
            try
            {
                var member = _context.Members.FirstOrDefault(member => member.MemberId == memberId);
                if(member != null)
                {
                    member.isDeleted = true;
                    _context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while deleting member with Id: {memberId}. {ex.Message}");
            }
        }
    }
}
