using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model;
using Domain.Shared.Enums;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class ExpertRepository : IExpertRepository
    {
        private readonly FeedbacksDbContext context;
        private readonly IMapper mapper;

        public ExpertRepository(FeedbacksDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// دریافت لیست متخصصین
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Expert> GetExperts()
        {
            var experts =  context.TblExpert.AsEnumerable();
            foreach (var item in experts)
            {
                item.CountAnswer = context.TblAnsower.Where(i => i.FkIdExpert == item.Id).Count();
            }
            return experts;
        }
        /// <summary>
        /// دریافت یک مورد بر اساس آیدی
        /// </summary>
        /// <param name="expertId">آیدی مورد</param>
        /// <returns></returns>
        public async Task<Expert?> GetExpertById(int expertId)
        {

            return await context.TblExpert.FirstOrDefaultAsync(i => i.Id == expertId);
        }
        /// <summary>
        /// گزارش از کارشناسان بر اساس فیلتر
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<Expert> GetExpertReport(ExpertReportFilterModel filter)
        {
            var experts = context.TblExpert.AsNoTracking();
            if (!string.IsNullOrEmpty(filter.Email)) experts = experts.Where(e => e.Email!.ToLower().Contains(filter.Email.ToLower()));
            if (!string.IsNullOrEmpty(filter.FirstName)) experts = experts.Where(e => e.FirstName!.ToLower().Contains(filter.FirstName.ToLower()));
            if (!string.IsNullOrEmpty(filter.LastName)) experts = experts.Where(e => e.LastName!.ToLower().Contains(filter.LastName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Language)) experts = experts.Where(e => e.Language!.ToLower().Contains(filter.Language.ToLower()));
            if (!string.IsNullOrEmpty(filter.FieldOfStudy)) experts = experts.Where(e => e.FieldOfStudy!.ToLower().Contains(filter.FieldOfStudy.ToLower()));
            if (!string.IsNullOrEmpty(filter.MobileNumber)) experts = experts.Where(e => e.MobileNumber!.ToLower().Contains(filter.MobileNumber.ToLower()));
            if (!string.IsNullOrEmpty(filter.NationalCode)) experts = experts.Where(e => e.NationalCode!.ToLower().Contains(filter.NationalCode.ToLower()));
            if (!string.IsNullOrEmpty(filter.Nationality)) experts = experts.Where(e => e.Nationality!.ToLower().Contains(filter.Nationality.ToLower()));
            if (!string.IsNullOrEmpty(filter.PassportNumber)) experts = experts.Where(e => e.PassportNumber!.ToLower().Contains(filter.PassportNumber.ToLower()));
            if (filter.FkIdSpecialty is not 0) experts = experts.Where(e => e.FkIdSpecialty == filter.FkIdSpecialty);
            if (filter.Education is not Education.All) experts = experts.Where(e => e.Education == filter.Education);
            return experts.Skip(filter.Skip).Take(filter.Take).AsEnumerable();
        } 
        /// <summary>
        /// دریافت تعداد گزارش کارشناسان
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> GetExpertReportCount(ExpertReportFilterModel filter)
        {
            var experts = context.TblExpert.AsNoTracking();
            if (!string.IsNullOrEmpty(filter.Email)) experts.Where(e => e.Email!.ToLower().Contains(filter.Email.ToLower()));
            if (!string.IsNullOrEmpty(filter.FirstName)) experts.Where(e => e.FirstName!.ToLower().Contains(filter.FirstName.ToLower()));
            if (!string.IsNullOrEmpty(filter.LastName)) experts.Where(e => e.LastName!.ToLower().Contains(filter.LastName.ToLower()));
            if (!string.IsNullOrEmpty(filter.Language)) experts.Where(e => e.Language!.ToLower().Contains(filter.Language.ToLower()));
            if (!string.IsNullOrEmpty(filter.FieldOfStudy)) experts.Where(e => e.FieldOfStudy!.ToLower().Contains(filter.FieldOfStudy.ToLower()));
            if (!string.IsNullOrEmpty(filter.MobileNumber)) experts.Where(e => e.MobileNumber!.ToLower().Contains(filter.MobileNumber.ToLower()));
            if (!string.IsNullOrEmpty(filter.NationalCode)) experts.Where(e => e.NationalCode!.ToLower().Contains(filter.NationalCode.ToLower()));
            if (!string.IsNullOrEmpty(filter.Nationality)) experts.Where(e => e.Nationality!.ToLower().Contains(filter.Nationality.ToLower()));
            if (!string.IsNullOrEmpty(filter.PassportNumber)) experts.Where(e => e.PassportNumber!.ToLower().Contains(filter.PassportNumber.ToLower()));
            if (filter.FkIdSpecialty is not 0) experts.Where(e => e.FkIdSpecialty == filter.FkIdSpecialty);
            if (filter.Education is not Education.All) experts.Where(e => e.Education == filter.Education);
            return await experts.CountAsync();
        }
        /// <summary>
        /// اضافه کردن کارشناس جدید
        /// </summary>
        /// <param name="expertBase"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AddExpert(ExpertBase expertBase)
        {
            if (expertBase.BirthDate > DateTime.Now)
                throw new AppException("تاریخ تولد وارد شده معتبر نیست");
            await ValidateForeignKeys(expertBase.FkIdSpecialty);
            var expert = mapper.Map<TblExpert>(expertBase);
            expert.Created = DateTime.Now;
            await context.TblExpert.AddAsync(expert);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// ویرایش کارشناس
        /// </summary>
        /// <param name="expertBase"></param>
        /// <param name="caseId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task UpdateExpert(ExpertBase expertBase, int caseId)
        {
            await ValidateForeignKeys(expertBase.FkIdSpecialty);
            var expert = await GetExpertById(caseId);
            if (expert == null)
            {
                throw new AppException("کارشناس مورد نظر یافت نشد");
            }

            mapper.Map(expertBase, expert);

            await context.SaveChangesAsync();
        }
        /// <summary>
        /// حذف کارشناس
        /// </summary>
        /// <param name="expertId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteExpert(int expertId)
        {
            var expert = await context.TblExpert.FirstOrDefaultAsync(i => i.Id == expertId);
            if (expert == null)
            {
                throw new AppException("کارشناس مورد نظر یافت نشد");
            }
            context.TblExpert.Remove(expert);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// اعتبارسنجی کلید های خارجی موجودیت
        /// </summary>
        /// <param name="specialtyId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        private async Task ValidateForeignKeys(int specialtyId)
        {
            var customerExist = await context.TblSpecialtie.AnyAsync(i => i.Id == specialtyId);
            if (customerExist is not true) throw new AppException("کد تخصص یافت نشد");
        }
    }
}
