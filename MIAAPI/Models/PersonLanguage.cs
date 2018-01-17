using System;

namespace MIAAPI.Models
{
    public class PersonLanguage : Utilities._GlobalFields
    {
        public Guid personLanguageId { get; set; }
        public Guid personId { get; set; }

        public Guid? languageId { get; set; }
        public Language language { get; set; }
    }
}
