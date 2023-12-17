using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model;
using Domain.Shared.Enums;
using FeedbacksManagementApi.Model;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Collections.Generic;

namespace FeedbacksManagementApi.Repository;

public class FeedbackRepository : IFeedbackRepository
{
    private readonly FeedbacksDbContext context;
    private readonly IMapper mapper;

    public FeedbackRepository(FeedbacksDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }


    private string TrTag(List<string> tags)
    {
        if (tags.Count == 0)
            return string.Empty;
        string result = string.Empty;
        foreach (var tag in tags)
        {
            result += tag + ",";
        }
        return result.Substring(0, result.Length - 2);
    }
    /// <summary>
    /// دریافت لیست موارد
    /// </summary>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public IEnumerable<FeedbackReport> GetFeedbacks(int take, int skip, FeedbackState state)
    {

        var lisTag = context.TblTag.Select(i => new { i.Name, i.Id }).ToList();
        var Expert = context.TblRelationshipExpertFeedbak.Include(i => i.TblExpertVi).Include(i => i.TblSpecialtyVi).Select(i => new { i.Id, Name = i.TblExpertVi != null ? "@" + i.TblExpertVi.FirstName : i.TblSpecialtyVi != null ? "#" + i.TblSpecialtyVi.Title : "عدم شناسایی" }).ToList();
        var ch = context.TblFeedback.AsNoTracking().Where(i => i.State == state)
             .Skip(skip).Take(take).Include(c => c.TblCustomerVi).Include(c => c.TblProductVi)
             .Include(c => c.TblRelationshipTagFeedbakIc)
             .Include(c => c.TblRelationshipExpertFeedbakIc)
             /*.Where(i=> i.TblRelationshipTagFeedbakIc.Any() && i.TblRelationshipExpertFeedbakIc.Any())*/;

        foreach (var c in ch)
        {

            var res = new FeedbackReport();
            res.CustomerName = c.TblCustomerVi.NameAndFamily;
            res.Description = c.Description;
            res.FkIdCustomer = c.FkIdCustomer;
            res.Id = c.Id;
            res.ProductName = c.TblProductVi!.Name;
            res.Resources = c.Resources;
            res.Source = c.Source;
            res.SourceAddress = c.SourceAddress;
            res.Title = c.Title;
            res.FkIdProduct = c.FkIdProduct;
            res.RespondDate = c.RespondDate;
            res.Created = c.Created;
            res.Priorty = c.Priorty;
            res.RefrralDate = c.RefrralDate;
            res.Similarity = c.Similarity;
            res.SerialNumber = c.SerialNumber;
            res.State = c.State;
            res.TagsUs = TrTag(lisTag.Where(i => c.TblRelationshipTagFeedbakIc.Select(p => p.FkIdTag).Contains(i.Id)).Select(o => o.Name).ToList());

            var exprId = c.TblRelationshipExpertFeedbakIc?.FirstOrDefault()?.Id ?? 0;
            res.ExpertUs = (Expert.FirstOrDefault(i => i.Id == exprId)?.Name ?? "عدم شناسایی");

            yield return res;
        }

        yield break;
    }
    /// <summary>
    /// تعداد مورد های رسیده
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetFeedbacksCount(FeedbackState state)
    {
        return await context.TblFeedback.Where(i => i.State == state).CountAsync();
    }
    /// <summary>
    /// دریافت یک مورد بر اساس آیدی
    /// </summary>
    /// <param name="feedbackId">آیدی مورد</param>
    /// <returns></returns>
    public async Task<FeedbackReport> GetFeedbackDetailsById(int feedbackId)
    {
        var tblFeedback = await context.TblFeedback
       .Include(i => i.TblCustomerVi)
       .Include(i => i.TblProductVi).FirstOrDefaultAsync(i => i.Id == feedbackId);
        if (tblFeedback == null) throw new AppException("مورد یافت نشد");


        var lisTag = context.TblTag.Select(i => new { i.Name, i.Id }).ToList();
        var expertIds = context.TblAnsower.Select(i => i.FkIdExpert);
        var refrredExpertsOrGroup = context.TblRelationshipExpertFeedbak
            .Where(i => i.FkIdFeedback == tblFeedback.Id)
            .Include(i => i.TblExpertVi)
            .Include(i => i.TblSpecialtyVi)
            .Select(i => new
            {
                i.Id,
                Name = i.TblExpertVi != null ? i.TblExpertVi.FirstName + i.TblExpertVi.LastName : i.TblSpecialtyVi != null ? i.TblSpecialtyVi.Title : "عدم شناسایی",
                RefrealDate = i.RefrralDate
            }).ToList();
        var feedback = mapper.Map<FeedbackReport>(tblFeedback);
        feedback.ProductName = tblFeedback.TblProductVi.Name;
        feedback.CustomerName = tblFeedback.TblCustomerVi.NameAndFamily;
        feedback.TagsUs = TrTag(lisTag.Where(i => tblFeedback.TblRelationshipTagFeedbakIc.Select(p => p.FkIdTag).Contains(i.Id)).Select(o => o.Name).ToList());
        feedback.ExpertsOrGroupExpertsList = refrredExpertsOrGroup.Select(i => new ExpertOrGroupExpert { id = i.Id, Name = i.Name ?? "ناشناس" , RefrralDate = i.RefrealDate }).ToList();

        return feedback;
    }
    /// <summary>
    /// دریافت اطلاعات مورد بر اساس شماره سریال
    /// </summary>
    /// <returns></returns>
    public async Task<FeedbackReport> GetFeedbackDetailsBySerialNumber(string serialNumber)
    {
        var tblFeedback = await context.TblFeedback
            .Include(i => i.TblCustomerVi)
            .Include(i => i.TblProductVi).FirstOrDefaultAsync(i => i.SerialNumber == serialNumber);
        if (tblFeedback == null) throw new AppException("مورد یافت نشد");


        var lisTag = context.TblTag.Select(i => new { i.Name, i.Id }).ToList();
        var expertIds = context.TblAnsower.Select(i => i.FkIdExpert);

        var refrredExpertsOrGroup = context.TblRelationshipExpertFeedbak
            .Where(i => i.FkIdFeedback == tblFeedback.Id)
            .Include(i => i.TblExpertVi)
            .Include(i => i.TblSpecialtyVi)
            .Select(i => new
            {
                i.Id,
                Name = i.TblExpertVi != null ? i.TblExpertVi.FirstName + " " + i.TblExpertVi.LastName : i.TblSpecialtyVi != null ?  i.TblSpecialtyVi.Title : "عدم شناسایی",
                RefrealDate = i.RefrralDate
            }).ToList();
        var feedback = mapper.Map<FeedbackReport>(tblFeedback);
        feedback.ProductName = tblFeedback.TblProductVi.Name;
        feedback.CustomerName = tblFeedback.TblCustomerVi.NameAndFamily;
        feedback.TagsUs = TrTag(lisTag.Where(i => tblFeedback.TblRelationshipTagFeedbakIc.Select(p => p.FkIdTag).Contains(i.Id)).Select(o => o.Name).ToList());
        feedback.ExpertsOrGroupExpertsList = refrredExpertsOrGroup
            .Select(i => new ExpertOrGroupExpert { id = i.Id, Name = i.Name ?? "ناشناس", RefrralDate = i.RefrealDate })
            .ToList();

        return feedback;
    }
    /// <summary>
    /// گرفتن اطلاعات یک مورد
    /// </summary>
    /// <param name="feedbackId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task<Feedback?> GetFeedbackById(int feedbackId)
    {
        return await context.TblFeedback.FirstOrDefaultAsync(i => i.Id == feedbackId);
    }
    /// <summary>
    /// اضافه کردن مورد جدید
    /// </summary>
    /// <param name="feedbackBase"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task AddFeedbackByExpert(FeedbackBase feedbackBase, bool isAnswered, int expertId)
    {
        var expertExist = await context.TblExpert.AnyAsync(i => i.Id == expertId);
        if (expertExist is not true) throw new AppException("کد کارشناس یافت نشد");
        await ValidateForeignKeys(feedbackBase.FkIdCustomer, feedbackBase.FkIdProduct);
        var feedback = mapper.Map<TblFeedback>(feedbackBase);
        feedback.State = FeedbackState.ReadyToSend;
        feedback.SerialNumber = $"{DateTime.Now.Ticks}";
        feedback.Created = DateTime.Now;
        await context.TblFeedback.AddAsync(feedback);
        await context.SaveChangesAsync();
        await context.TblRelationshipExpertFeedbak.AddAsync(new RelationshipExpertFeedbak
        {
            FkIdFeedback = feedback.Id,
            RefrralDate = DateTime.Now,
            FkIdSpecialty = null,
            FkIdExpert = expertId,
            Description = feedbackBase.Description,
        });

        if (isAnswered)
        {
            await context.TblAnsower.AddAsync(new TblAnsower
            {
                FkIdExpert = expertId,
                FkIdFeedback = feedback.Id,
                Respond = feedback.Respond,
                RespondDate = DateTime.Now,
            });
        }
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// ویرایش مورد
    /// </summary>
    /// <param name="feedbackBase"></param>
    /// <param name="feedbackId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task UpdateFeedback(FeedbackBase feedbackBase, int feedbackId)
    {
        await ValidateForeignKeys(feedbackBase.FkIdCustomer, feedbackBase.FkIdProduct);
        var feedback = await GetFeedbackById(feedbackId);
        if (feedback == null) throw new AppException("مورد مورد نظر یافت نشد");
        feedback.Title = feedbackBase.Title;
        feedback.Description = feedbackBase.Description;
        feedback.Resources = feedbackBase.Resources;
        feedback.FkIdCustomer = feedbackBase.FkIdCustomer;
        feedback.Source = feedbackBase.Source;
        feedback.SourceAddress = feedbackBase.SourceAddress;
        feedback.Priorty = feedbackBase.Priorty;
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// تغییر وضعیت موارد ارسالی به وضعیت حذف شده
    /// </summary>
    /// <param name="feedbackId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task DeleteFeedbacks(int[] feedbackIds)
    {
        if (feedbackIds.Any() is not true)
            throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

        await context.TblFeedback.Where(i => feedbackIds.Contains(i.Id))
            .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.Deleted));
    }
    /// <summary>
    /// ارسال یک یا چند مورد به متخصص
    /// </summary>
    /// <param name="feedbackIds"></param>
    /// <param name="expertId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task SubmitFeedbacksToExpert(SubmitFeedbacksToExpertRequestModel submitModel)
    {
        if (!submitModel.FeedbackIds.Any())
            throw new AppException("لیست موارد برای ارسال به متخصص نمیتواند خالی باشد");

        var expertExist = await context.TblExpert.AnyAsync(e => e.Id == submitModel.ExpertId);
        if (!expertExist)
            throw new AppException("کد متخصص یافت نشد");

        foreach (var item in submitModel.FeedbackIds)
        {
            await context.TblRelationshipExpertFeedbak.AddAsync(new RelationshipExpertFeedbak
            {
                FkIdExpert = submitModel.ExpertId,
                FkIdFeedback = item,
                Description = submitModel.Description,
                FkIdSpecialty = null,
                RefrralDate = DateTime.Now
            });
        }
        await context.SaveChangesAsync();

        await context.TblFeedback
            .Where(i => submitModel.FeedbackIds.Contains(i.Id))
            .ExecuteUpdateAsync(f => f
            .SetProperty(p => p.State, p => FeedbackState.SentToExpert)
            .SetProperty(p => p.RefrralDate, p => DateTime.Now));

    }
    /// <summary>
    /// ارسال فیدبک به گروه متخصص
    /// </summary>
    /// <returns></returns>
    public async Task SubmitFeedbacksToExpertGroup(SubmitFeedbackToExpertGroupRequestModel submitModel)
    {
        var exist = await context.TblSpecialtie.AnyAsync(i => i.Id == submitModel.ExpertGroupId);
        if (exist is not true) throw new AppException("کد گروه یافت نشد");
        var experts = context.TblExpert.Where(e => e.FkIdSpecialty == submitModel.ExpertGroupId).Select(i => i.Id);

        foreach (var item in submitModel.FeedbackIds)
        {
            await context.TblRelationshipExpertFeedbak.AddAsync(new RelationshipExpertFeedbak
            {
                FkIdExpert = null,
                FkIdFeedback = item,
                Description = submitModel.Description,
                FkIdSpecialty = submitModel.ExpertGroupId,
                RefrralDate = DateTime.Now
            });
        }
        await context.SaveChangesAsync();

        await context.TblFeedback
       .Where(i => submitModel.FeedbackIds.Contains(i.Id))
       .ExecuteUpdateAsync(f => f
       .SetProperty(p => p.State, p => FeedbackState.SentToExpert)
       .SetProperty(p => p.RefrralDate, p => DateTime.Now));

    }
    /// <summary>
    /// ارسال فیدبک برای مشتری
    /// </summary>
    /// <param name="feedbackIds"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task SubmitFeedbacksToCustomer(int[] feedbackIds)
    {
        if (feedbackIds.Any() is not true)
            throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

        await context.TblFeedback.Where(i => feedbackIds.Contains(i.Id))
            .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.Completed));
    }
    /// <summary>
    /// تغییر وضعیت موارد ارسالی به موارد بایگانی
    /// </summary>
    /// <param name="feedbackIds"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task ArchiveFeedbacks(int[] feedbackIds)
    {
        if (!feedbackIds.Any())
            throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

        await context.TblFeedback.Where(i => feedbackIds.Contains(i.Id))
            .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.Archived));
    }
    /// <summary>
    /// بازیابی موارد
    /// </summary>
    /// <param name="feedbackIds"></param>
    /// <returns></returns>
    public async Task RecycleFeedbacks(int[] feedbackIds)
    {
        if (!feedbackIds.Any())
            throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

        await context.TblFeedback.Where(i => feedbackIds.Contains(i.Id))
            .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.ReadyToSend));
    }
    /// <summary>
    /// گزارش از موارد با فیلتر
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    public IEnumerable<FeedbackReport> GetFeedbackReport(FeedbackReportFilterModel filterModel)
    {
        var feedbacks = context.TblFeedback.AsNoTracking();
        if (filterModel.ProductId is not 0) feedbacks = feedbacks.Where(i => i.FkIdProduct == filterModel.ProductId);
        if (filterModel.CustomerId is not 0) feedbacks = feedbacks.Where(i => i.FkIdCustomer == filterModel.CustomerId);
        if (filterModel.Source is not Source.All) feedbacks = feedbacks.Where(i => i.Source == filterModel.Source);
        if (filterModel.Created is not null) feedbacks = feedbacks.Where(i => i.Created.Date == filterModel.Created.Value.Date);
        if (filterModel.ReferralDate is not null) feedbacks = feedbacks
                .Include(i => i.TblRelationshipExpertFeedbakIc).Where(i => i.RefrralDate.Value.Date == filterModel.ReferralDate.Value.Date);
        if (filterModel.RespondDate is not null) feedbacks = feedbacks.Where(i => i.RespondDate.Value.Date == filterModel.RespondDate.Value.Date);
        if (filterModel.Priorty is not Priority.All) feedbacks = feedbacks.Where(i => i.Priorty == filterModel.Priorty);
        if (filterModel.State is not FeedbackState.All) feedbacks = feedbacks.Where(i => i.State == filterModel.State);
        if (!string.IsNullOrEmpty(filterModel.Description)) feedbacks = feedbacks.Where(i => i.Description == filterModel.Description);
        if (!string.IsNullOrEmpty(filterModel.Respond)) feedbacks = feedbacks.Where(i => i.Respond == filterModel.Respond);
        if (!string.IsNullOrEmpty(filterModel.Title)) feedbacks = feedbacks.Where(i => i.Title == filterModel.Title);
        if (!string.IsNullOrEmpty(filterModel.SerialNumber)) feedbacks = feedbacks.Where(i => i.SerialNumber == filterModel.SerialNumber);
        if (filterModel.ExpertId != 0)
        {
            feedbacks = feedbacks.Include(i => i.TblRelationshipExpertFeedbakIc).Where(i => i.TblRelationshipExpertFeedbakIc.Select(p => p.FkIdExpert).Contains(filterModel.ExpertId));
        }
        else if (filterModel.SpecialityId != 0)
        {
            feedbacks = feedbacks.Include(i => i.TblRelationshipExpertFeedbakIc).Where(i => i.TblRelationshipExpertFeedbakIc.Select(p => p.FkIdSpecialty).Contains(filterModel.SpecialityId));
        }

        if (filterModel.Tags != null && filterModel.Tags.Any())
        {
            foreach (var tag in filterModel.Tags)
            {
                   feedbacks = feedbacks.Include(i => i.TblRelationshipTagFeedbakIc).Where(i =>i.TblRelationshipTagFeedbakIc.Select(i=> i.FkIdTag).Contains(tag));
            }
           
        }
        if (filterModel.ExpertId != 0) feedbacks = feedbacks.Where(i => i.TblRelationshipExpertFeedbakIc.Select(p => p.FkIdExpert).Contains(filterModel.ExpertId));
        return feedbacks.Skip(filterModel.Skip).Take(filterModel.Take).Select(c => new FeedbackReport
        {
            Id = c.Id,
            Source = c.Source,
            SourceAddress = c.SourceAddress,
            Title = c.Title,
            SerialNumber = c.SerialNumber,
        });
    }
    /// <summary>
    /// تعداد گزارش
    /// </summary>
    /// <param name="filterModel"></param>
    /// <returns></returns>
    public async Task<int> GetFeedbackReportCount(FeedbackReportFilterModel filterModel)
    {
        var feedbacks = context.TblFeedback.AsNoTracking();
        if (filterModel.ProductId is not 0) feedbacks = feedbacks.Where(i => i.FkIdProduct == filterModel.ProductId);
        if (filterModel.CustomerId is not 0) feedbacks = feedbacks.Where(i => i.FkIdCustomer == filterModel.CustomerId);
        if (filterModel.Source is not Source.All) feedbacks = feedbacks.Where(i => i.Source == filterModel.Source);
        if (filterModel.Created is not null) feedbacks = feedbacks.Where(i => i.Created.Date == filterModel.Created.Value.Date);
        if (filterModel.ReferralDate is not null) feedbacks = feedbacks
                .Include(i => i.TblRelationshipExpertFeedbakIc).Where(i => i.RefrralDate.Value.Date == filterModel.ReferralDate.Value.Date);
        if (filterModel.RespondDate is not null) feedbacks = feedbacks.Where(i => i.RespondDate.Value.Date == filterModel.RespondDate.Value.Date);
        if (filterModel.Priorty is not Priority.All) feedbacks = feedbacks.Where(i => i.Priorty == filterModel.Priorty);
        if (filterModel.State is not FeedbackState.All) feedbacks = feedbacks.Where(i => i.State == filterModel.State);
        if (!string.IsNullOrEmpty(filterModel.Description)) feedbacks = feedbacks.Where(i => i.Description == filterModel.Description);
        if (!string.IsNullOrEmpty(filterModel.Respond)) feedbacks = feedbacks.Where(i => i.Respond == filterModel.Respond);
        if (!string.IsNullOrEmpty(filterModel.Title)) feedbacks = feedbacks.Where(i => i.Title == filterModel.Title);
        if (!string.IsNullOrEmpty(filterModel.SerialNumber)) feedbacks = feedbacks.Where(i => i.SerialNumber == filterModel.SerialNumber);
        if (filterModel.ExpertId != 0)
        {
            feedbacks = feedbacks.Include(i => i.TblRelationshipExpertFeedbakIc).Where(i => i.TblRelationshipExpertFeedbakIc.Select(p => p.FkIdExpert).Contains(filterModel.ExpertId));
        }
        else if (filterModel.SpecialityId != 0)
        {
            feedbacks = feedbacks.Include(i => i.TblRelationshipExpertFeedbakIc).Where(i => i.TblRelationshipExpertFeedbakIc.Select(p => p.FkIdSpecialty).Contains(filterModel.SpecialityId));
        }

        if (filterModel.Tags != null && filterModel.Tags.Any())
        {
            foreach (var tag in filterModel.Tags)
            {
                feedbacks = feedbacks.Include(i => i.TblRelationshipTagFeedbakIc).Where(i => i.TblRelationshipTagFeedbakIc.Select(i => i.FkIdTag).Contains(tag));
            }

        }
        if (filterModel.ExpertId != 0) feedbacks = feedbacks.Where(i => i.TblRelationshipExpertFeedbakIc.Select(p => p.FkIdExpert).Contains(filterModel.ExpertId));
        return await feedbacks.CountAsync();
    }
    /// <summary>
    /// اضافه کردن تگ جدید به فیدبک
    /// </summary>
    /// <param name="feedbackId"></param>
    /// <param name="tagId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task AddTagToFeedback(int feedbackId, Tag tag)
    {
        var tblTag = new TblTag
        {
            Name = tag.Name
        };
        await context.TblTag.AddAsync(tblTag);
        await context.SaveChangesAsync();
        var feedbackExist = await context.TblFeedback.AnyAsync(i => i.Id == feedbackId);
        if (!feedbackExist) throw new AppException("کد مورد یافت نشد");
        await context.TblRelationshipTagFeedbak.AddAsync(new RelationshipTagFeedbak
        {
            FkIdFeedback = feedbackId,
            FkIdTag = tblTag.Id,
        });
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// حذف یک تگ از فیدبک
    /// </summary>
    /// <returns></returns>
    public async Task DeleteTagFromFeedback(int feedbackId, int tagId)
    {
        var tagFeedback = await context.TblRelationshipTagFeedbak.FirstOrDefaultAsync(i => i.FkIdTag == tagId && i.FkIdFeedback == feedbackId);
        if (tagFeedback is null) throw new AppException("کد مورد یافت نشد");
        context.TblRelationshipTagFeedbak.Remove(tagFeedback);
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// تایید پاسخ فیدبک
    /// </summary>
    /// <param name="feedbackId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task ConfirmFeedbackAnswer(int feedbackId, int answerId)
    {
        var feedback = await GetFeedbackById(feedbackId);
        if (feedback is null) throw new AppException("کد مورد یافت نشد");
        var answer = await context.TblAnsower.FirstOrDefaultAsync(i => i.Id == answerId);
        if (answer is null) throw new AppException("کد پاسخ یافت نشد");
        feedback.Respond = answer.Respond;
        feedback.RespondDate = DateTime.Now;
        answer.IsConfirmed = true;
        feedback.State = FeedbackState.ReadyToSend;
        await context.SaveChangesAsync();
    }
    /// <summary>
    /// ارسال برای باکس پاسخدهی
    /// </summary>
    /// <returns></returns>
    public async Task SendToReadyToSendCustomer(int[] feedbackIds)
    {
        if (!feedbackIds.Any())
            throw new AppException("لیست موارد ارسالی نمیتواند خالی باشد");

        await context.TblFeedback.Where(i => feedbackIds.Contains(i.Id))
            .ExecuteUpdateAsync(f => f.SetProperty(p => p.State, p => FeedbackState.Answered));

    }
    /// <summary>
    /// دریافت لیست تگ های یک فیدبک
    /// </summary>
    /// <param name="feedbackId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    public async Task<IEnumerable<Tag>> GetTagsForOneFeedback(int feedbackId)
    {
        var feedback = await GetFeedbackById(feedbackId);
        if (feedback is null) throw new AppException("کد مورد یافت نشد");
        var tagIds = context.TblRelationshipTagFeedbak.Where(t => t.FkIdFeedback == feedbackId).Select(t => t.FkIdTag).ToList();
        if (tagIds.Any())
        {
            var tags = context.TblTag.Where(i => tagIds.Contains(i.Id)).Select(t => new Tag
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();
            return tags;
        }
        return new List<Tag>();
    }
    /// <summary>
    /// اعتبارسنجی وجود آیدی مشتری و محصول
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    private async Task ValidateForeignKeys(int customerId, int productId)
    {
        var customerExist = await context.TblCustomer.AnyAsync(i => i.Id == customerId);
        if (customerExist is not true) throw new AppException("کد مشتری یافت نشد");
        var productExist = await context.TblProduct.AnyAsync(i => i.Id == productId);
        if (productExist is not true) throw new AppException("کد محصول یافت نشد");
    }
}
