using System;

namespace MIAAPI.Models
{
    public class Person : Utilities._GlobalFields
    {
        public Guid personId { get; set; }
        public string name { get; set; }
        public string fatherName { get; set; }
        public string motherName { get; set; }
        public string nrc { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public string race { get; set; }
        public string nationality { get; set; }
        public string religion { get; set; }
        public Int16 gender { get; set; }
        public string homeAddress { get; set; }

        public Guid? homeCityId { get; set; }
        public City homeCity { get; set; }

        public Guid? homeTownshipId { get; set; }
        public Township homeTownship { get; set; }

        public string mobilePhone { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string socialProfileUrl { get; set; }
        public string chatPhone { get; set; }
        public string education { get; set; }
        public string professional { get; set; }
        public string biography { get; set; }

        public Guid? nativeCityId { get; set; }
        public City nativeCity { get; set; }

        public string photo { get; set; }

        public PersonBusiness[] personBusiness { get; set; }
        public PersonLanguage[] personLanguage { get; set; }
    }
}
