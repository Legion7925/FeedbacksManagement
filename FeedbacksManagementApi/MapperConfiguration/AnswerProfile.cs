using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;

namespace FeedbacksManagementApi.MapperConfiguration
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<AnsowerBase, TblAnsower>();
        }
    }
}
