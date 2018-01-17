using System;

namespace MIAAPI.Models
{
    public class AppUser : Utilities._GlobalFields
    {
        public Guid userId { get; set; }
        public string loginName { get; set; }
        public string password { get; set; }
    }
}
