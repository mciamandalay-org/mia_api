using System;

namespace MIAAPI.Models
{
    public class Business : Utilities._GlobalFields
    {
        public Guid businessId { get; set; }

        public Guid? businessTypeId { get; set; }
        public BusinessType businessType { get; set; }

        public string name { get; set; }
        public string address { get; set; }

        public Guid? cityId { get; set; }
        public City city { get; set; }

        public Guid? townshipId { get; set; }
        public Township township { get; set; }

        public DateTime foundedDate { get; set; }
        public string licenseNumber { get; set; }
        public string otherLicense { get; set; }
        public string typeOfOwnership { get; set; }
        public int numberOfEmployee { get; set; }
        public string website { get; set; }
        public string email { get; set; }
        public string fax { get; set; }

        public Guid? industryTypeId { get; set; }
        public IndustryType industryType { get; set; }

        public decimal capital { get; set; }
        public decimal annualIncome { get; set; }

        public BusinessBranch[] businessBranch { get; set; }
        public BusinessFacility[] businessFacility { get; set; }
    }
}