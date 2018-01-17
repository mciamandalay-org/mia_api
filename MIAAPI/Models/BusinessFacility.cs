using System;

namespace MIAAPI.Models
{
    public class BusinessFacility : Utilities._GlobalFields
    {
        public Guid businessFacilityId { get; set; }
        public Guid? businessId { get; set; }
        public Guid? facilityId { get; set; }        
        public Facility facility { get; set; }
    }
}
