using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;

namespace FeedbacksManagementApi.MapperConfiguration
{
    public class ExpertsProfile : Profile
    {
        public ExpertsProfile()
        {
            CreateMap<ExpertBase, TblExpert>();
        }
    }
}
