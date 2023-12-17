using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;

namespace FeedbacksManagementApi.MapperConfiguration
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<CaseBase, TblCase>();

            CreateMap<CaseBase , Feedback>();

            CreateMap<TblCase , TblFeedback>();
        }
    }
}
