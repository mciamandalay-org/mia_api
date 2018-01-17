using System;

namespace MIAAPI.Models
{
    public class PersonBusinessProductType : Utilities._GlobalFields
    {
        public Guid personBusinessProductTypeId { get; set; }
        public Guid personBusinessId { get; set; }

        public Guid? productTypeId { get; set; }
        public ProductType productType { get; set; }
    }
}
