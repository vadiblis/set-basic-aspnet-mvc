using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public interface IFeedbackService
    {
        Task<bool> AddFeedback(long userId, string userEmail, string info);
        
        Task<Feedback> GetFeedback(int id);
        
        Task<bool> SetFeedbackToReviewed(int id);
                
        // get feedbacks starting from feedback with specific date
        Task<List<Feedback>> GetFeedbacks(DateTime fromDate, int count, bool forward, bool includingReviewed);
        
        // get feedbacks starting from feedback with specific id
        Task<List<Feedback>> GetFeedbacks(int fromId, int count, bool forward, bool includingReviewed);
    }

    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> _feedbackRepo;

        public FeedbackService(IRepository<Feedback> feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }
        
        public Task<bool> AddFeedback(long userId, string userEmail, string info)
        {
            if (string.IsNullOrEmpty(info) || 
                string.IsNullOrEmpty(userEmail) || 
                !userEmail.IsEmail()) 
                return Task.FromResult(false);

            var newFeedback = new Feedback
            {
                UserId = userId,
                UserEmail = userEmail,
                Info = info
            };
            
            _feedbackRepo.Create(newFeedback);

            var result = _feedbackRepo.SaveChanges();
            return Task.FromResult(result);
        }

        public Task<Feedback> GetFeedback(int id)
        {
            if (id < 0) return null;

            var feedback = _feedbackRepo.FindOne(x => x.Id == id);
            return Task.FromResult(feedback);
        }
        
        public async Task<bool> SetFeedbackToReviewed(int id)
        {
            if (id < 0) return await Task.FromResult(false);

            var feedback = await GetFeedback(id);
            if (feedback == null) return false;

            if (feedback.Reviewed.HasValue && feedback.Reviewed.Value) return await Task.FromResult(true);

            feedback.Reviewed = true;
            feedback.ReviewedAt = DateTime.Now;
            _feedbackRepo.Update(feedback);

            var result = _feedbackRepo.SaveChanges();
            return await Task.FromResult(result);
        }

        public Task<List<Feedback>> GetFeedbacks(DateTime fromDate, int count, bool forward, bool includingReviewed)
        {
            if (count < 0) return null;
            
            var feedbacks = _feedbackRepo.FindAll();

            if (!includingReviewed) feedbacks = feedbacks.Where(x => !x.Reviewed.HasValue || !x.Reviewed.Value);
            if (!forward) feedbacks = feedbacks.Reverse();

            var selectedFeedbacks = feedbacks.SkipWhile(x => x.CreatedAt == fromDate).Take(count).ToList();
            return Task.FromResult(selectedFeedbacks);
        }

        public Task<List<Feedback>> GetFeedbacks(int fromId, int count, bool forward, bool includingReviewed)
        {
            if (count < 0) return null;

            var feedbacks = _feedbackRepo.FindAll();

            if (!includingReviewed) feedbacks = feedbacks.Where(x => !x.Reviewed.HasValue || !x.Reviewed.Value);
            if (!forward) feedbacks = feedbacks.Reverse();

            var selectedFeedbacks = feedbacks.SkipWhile(x => x.Id == fromId).Take(count).ToList();
            return Task.FromResult(selectedFeedbacks);
        }
    }
}