using System;

namespace MIAAPI.Models
{
    public class Committee : Utilities._GlobalFields
    {
        public Guid committeeId { get; set; }
        public Guid fiscalYearId { get; set; }
        public FiscalYear fiscalYear { get; set;}
        public string name { get; set; }
        public string objective { get; set; }
    }
}
