using System;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Models
{
    public class FeedbackModel : BaseModel
    {
        public long Id { get; set; }

        public string UserEmail { get; set; }

        public string Info { get; set; }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(Info)
                   && !string.IsNullOrEmpty(Info)
                   && UserEmail.IsEmail();
        }

        public static FeedbackModel Map(Feedback feedback)
        {
            return new FeedbackModel()
            {
                Id =  feedback.Id,
                UserEmail = feedback.UserEmail,
                Info = feedback.Info
            };
        }
    }
}