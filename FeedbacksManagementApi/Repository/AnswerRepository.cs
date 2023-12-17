using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly FeedbacksDbContext context;
        private readonly IMapper mapper;

        public AnswerRepository(FeedbacksDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// دریافت پاسخ های یک فیدبک
        /// </summary>
        /// <param name="feedbackId"></param>
        /// <returns></returns>
        public IEnumerable<Ansower> GetAnswersForFeedback(int feedbackId)
        {
            var answers = context.TblAnsower.Where(i => i.FkIdFeedback == feedbackId).Select(i => new Ansower
            {
                FkIdFeedback = i.FkIdFeedback,
                FkIdExpert = i.FkIdExpert,
                Id = i.Id,
                Respond = i.Respond,
                RespondDate = i.RespondDate,
                IsConfirmed = i.IsConfirmed
            });
            return answers;
        }
        /// <summary>
        /// اضافه کردن پاسخ جدید
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public async Task AddAnswer(AnsowerBase answer)
        {
            var insertAnswer = mapper.Map<TblAnsower>(answer);
            await context.TblAnsower.AddAsync(insertAnswer);
            await context.SaveChangesAsync();
        }
    }
}
