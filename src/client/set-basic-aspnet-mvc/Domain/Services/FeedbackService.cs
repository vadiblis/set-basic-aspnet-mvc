using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Domain.Services
{
    interface IFeedbackService
    {
        Task<bool> AddFeedback(string userEmail, string info);
        Task<Feedback> GetFeedback(int id);
        Task<DateTime?> GetFeedbackCreateDate(int id);
        Task<bool> HideFeedback(int id, bool isImprovement, bool isDone);
        
        /// <summary>
        /// get feedbacks from the start of the table
        /// </summary>
        /// <param name="count">amount of items to get</param>
        /// <returns></returns>
        Task<List<Feedback>> GetStartFeedbacks(int count);
        /// <summary>
        /// get feedbacks from the end of the table
        /// </summary>
        /// <param name="count">amount of items to get</param>
        /// <returns></returns>
        Task<List<Feedback>> GetEndFeedbacks(int count);
        /// <summary>
        /// get feedbacks starting from feedback with specific date
        /// </summary>
        /// <param name="fromDate">from date to get</param>
        /// <param name="count">amount of items to get</param>
        /// <param name="forward">direction of items to get, if false then take backward</param>
        /// <returns></returns>
        Task<List<Feedback>> GetFeedbacks(DateTime fromDate, int count, bool forward);
        /// <summary>
        /// get feedbacks starting from feedback with specific id
        /// </summary>
        /// <param name="fromId">from ffedback with id</param>
        /// <param name="count">amount of items to get</param>
        /// <param name="forward">direction of items to get, if false then take backward</param>
        /// <returns></returns>
        Task<List<Feedback>> GetFeedbacks(int fromId, int count, bool forward);
    }
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> _feedbackRepo;

        public FeedbackService(IRepository<Feedback> feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }

        public Task<bool> AddFeedback(string userEmail, string info)
        {
            if (string.IsNullOrEmpty(info) || 
                string.IsNullOrEmpty(userEmail) || 
                !userEmail.IsEmail()) 
                return Task.FromResult(false);

            var newFeedback = new Feedback
            {
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

            var feedback = _feedbackRepo.FindOne(x => x.Id.Equals(id));
            return Task.FromResult(feedback);
        }

        public async Task<DateTime?> GetFeedbackCreateDate(int id)
        {
            if (id < 0) return null;

            var feedback = await GetFeedback(id);
            if (feedback == null) return null;

            return await Task.FromResult(feedback.CreatedAt);
        }

        public Task<bool> HideFeedback(int id, bool isImprovement, bool isDone)
        {
            throw new NotImplementedException();
        }

        public Task<List<Feedback>> GetStartFeedbacks(int count)
        {
            if (count < 0) return null;
            var feedbacks = _feedbackRepo.FindAll().Take(count).ToList();
            return Task.FromResult(feedbacks);
        }

        public Task<List<Feedback>> GetEndFeedbacks(int count)
        {
            if (count < 0) return null;
            
            var feedbacks = _feedbackRepo.FindAll().ToList();
            var totalCount = feedbacks.Count();
            if (count >= totalCount) return Task.FromResult(feedbacks);

            var selectedFeedbacks = _feedbackRepo.FindAll().Reverse().Take(count).ToList();
            return Task.FromResult(selectedFeedbacks);
        }

        public Task<List<Feedback>> GetFeedbacks(DateTime fromDate, int count, bool forward)
        {
            if (count < 0) return null;
            
            var feedbacks = _feedbackRepo.FindAll().ToList();
            var totalCount = feedbacks.Count();
            if (count >= totalCount) return Task.FromResult(feedbacks);

            if (!forward) feedbacks.Reverse();

            var selectedFeedbacks = feedbacks.SkipWhile(x => x.CreatedAt.Equals(fromDate)).Take(count).ToList();
            return Task.FromResult(selectedFeedbacks);
        }

        public Task<List<Feedback>> GetFeedbacks(int fromId, int count, bool forward)
        {
            if (count < 0) return null;

            var feedbacks = _feedbackRepo.FindAll().ToList();
            var totalCount = feedbacks.Count();
            if (count >= totalCount) return Task.FromResult(feedbacks);

            if (!forward) feedbacks.Reverse();

            var selectedFeedbacks = feedbacks.SkipWhile(x => x.Id.Equals(fromId)).Take(count).ToList();
            return Task.FromResult(selectedFeedbacks);

        }
    }
}