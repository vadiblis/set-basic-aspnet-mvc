using System;

namespace set_basic_aspnet_mvc.Domain.Entities
{
    public class Feedback : BaseEntity
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string Info { get; set; }
        public bool? Reviewed { get; set; }
        public DateTime? ReviewedAt { get; set; }
    }
}