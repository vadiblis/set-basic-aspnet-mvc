using System;

namespace set_basic_aspnet_mvc.Domain.DataTransferObjects
{
    public class FeedbackDto : BaseDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string UserEmail { get; set; }

        public string Info { get; set; }

        public bool? Reviewed { get; set; }

        public DateTime? ReviewedAt { get; set; }
    }
}