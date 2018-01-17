using System;

namespace MIAAPI.Models
{
    public class Reason : Utilities._GlobalFields
    {
        public Guid reasonId { get; set; }
        public string description { get; set; }
    }
}
