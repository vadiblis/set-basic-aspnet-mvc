using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using set_basic_aspnet_mvc.Domain.Contracts;
using set_basic_aspnet_mvc.Domain.DataTransferObjects;
using set_basic_aspnet_mvc.Domain.Entities;
using set_basic_aspnet_mvc.Domain.Repositories;
using set_basic_aspnet_mvc.Helpers;

namespace set_basic_aspnet_mvc.Domain.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> _feedbackRepo;

        public FeedbackService(IRepository<Feedback> feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
        }
        
        public Task<bool> AddFeedback(long userId, string userEmail, string info)
        {
            if (string.IsNullOrEmpty(info) 
                || string.IsNullOrEmpty(userEmail) 
                || !userEmail.IsEmail()) return Task.FromResult(false);

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

        public Task<FeedbackDto> GetFeedback(int id)
        {
            if (id < 0) return null;

            var item = _feedbackRepo.FindOne(x => x.Id == id);

            var result = Mapper.Map<Feedback, FeedbackDto>(item);

            return Task.FromResult(result);
        }
        
        public async Task<bool> SetFeedbackToReviewed(int id)
        {
            var item = _feedbackRepo.FindOne(x => x.Id == id);
            if (item == null) return await Task.FromResult(false);

            item.IsReviewed = true;
            item.ReviewedAt = DateTime.Now;

            _feedbackRepo.Update(item);

            var result = _feedbackRepo.SaveChanges();

            return await Task.FromResult(result);
        }

        public Task<PagedList<FeedbackDto>> GetFeedbacks(int pageNumber, bool isReviewed = false)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            pageNumber--;

            var items = _feedbackRepo.FindAll();

            long totalCount = items.Count();

            items = items.OrderByDescending(x => x.Id).Skip(ConstHelper.PageSize * pageNumber).Take(ConstHelper.PageSize);

            var result = items.Select(Mapper.Map<Feedback, FeedbackDto>).ToList();

            return Task.FromResult(new PagedList<FeedbackDto>(pageNumber, ConstHelper.PageSize, totalCount, result));
        }
    }
}