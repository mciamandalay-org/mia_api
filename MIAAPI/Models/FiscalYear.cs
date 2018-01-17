using System;

namespace MIAAPI.Models
{
    public class FiscalYear : Utilities._GlobalFields
    {
        public Guid fiscalYearId { get; set; }
        public DateTime? fiscalYear { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
