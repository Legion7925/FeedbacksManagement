using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;

namespace FeedbacksManagementApi.MapperConfiguration
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TblTag>();
        }
    }
}
