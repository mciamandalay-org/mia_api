using System;

namespace MIAAPI.Models
{
    public class Member : Utilities._GlobalFields
    {
        public Guid memberId { get; set; }

        public Guid personId { get; set; }
        public Person person { get; set; }

        public string registrationNo { get; set; }
        public DateTime? registrationDate { get; set; }

        public Guid? memberTypeId { get; set; }
        public MemberType memberType { get; set; }

        public string loginName { get; set; }
        public string password { get; set; }
        public Int16 memberstatus { get; set; }
    }
}
