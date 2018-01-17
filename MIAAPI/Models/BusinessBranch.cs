using System;

namespace MIAAPI.Models
{
    public class BusinessBranch : Utilities._GlobalFields
    {
        public Guid businessBranchId { get; set; }
        public Guid businessId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Guid? cityId { get; set; }
        public City city { get; set; }
        public Guid? townshipId { get; set; }
        public Township township { get; set; }
        public string phone { get; set; }
        public float locLat { get; set; }
        public float locLong { get; set; }
    }
}
