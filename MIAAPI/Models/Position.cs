using System;

namespace MIAAPI.Models
{
    public class Position : Utilities._GlobalFields
    {
        public Guid positionId { get; set; }
        public Guid? committeeId { get; set; }
        public Guid? parentPositionId { get; set; }
        public string name { get; set; }
        public string jobDescription { get; set; }
    }
}
