using System;

namespace MIAAPI.Models
{
    public class PersonBusiness : Utilities._GlobalFields
    {
        public Guid personBusinessId { get; set; }
        public Guid personId { get; set; }

        public Guid? businessId { get; set; }
        public Business business { get; set; }

        public string positionName { get; set; }
        public string departmentName { get; set; }

        public PersonBusinessProductType[]  personBusinessProductType { get; set; }
    }
}
