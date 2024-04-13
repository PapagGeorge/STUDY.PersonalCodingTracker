using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MobilePhone { get; set; }
        [AllowNull]
        public string Email { get; set; }
        public int RentedBooks { get; set; } = 0;
        public bool CanRent { get; set; } = true;
        public bool isDeleted { get; set; } = false;

    }
}
