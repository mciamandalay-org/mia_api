using System;

namespace MIAAPI.Models
{
    public class AccountHead : Utilities._GlobalFields
    {
        public Guid accountHeadId { get; set; }
        public string name { get; set; }
        public int accountType { get; set; }
        //public int status { get; set; }
    }
}
