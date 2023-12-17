using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.MapperConfiguration
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<FeedbackBase, TblFeedback>();
            CreateMap<Feedback, FeedbackReport>();
            CreateMap<Feedback, TblFeedback>();
        }
    }
}
