using Domain.Entities;
using Domain.Model;
using Domain.Shared.Enums;
using FeedbacksManagementApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFeedbackRepository
    {
        Task AddFeedbackByExpert(FeedbackBase feedbackBase, bool isAnswered, int expertId);
        Task ArchiveFeedbacks(int[] feedbackIds);
        Task DeleteFeedbacks(int[] feedbackIds);
        Task<Feedback?> GetFeedbackById(int feedbackId);
        IEnumerable<FeedbackReport> GetFeedbackReport(FeedbackReportFilterModel filterModel);
        Task<int> GetFeedbackReportCount(FeedbackReportFilterModel filterModel);
        IEnumerable<FeedbackReport> GetFeedbacks(int take, int skip, FeedbackState state);
        Task SubmitFeedbacksToExpert(SubmitFeedbacksToExpertRequestModel submitModel);
        Task SubmitFeedbacksToExpertGroup(SubmitFeedbackToExpertGroupRequestModel submitModel);
        Task UpdateFeedback(FeedbackBase feedbackBase, int feedbackId);
        Task<FeedbackReport> GetFeedbackDetailsById(int feedbackId);
        Task<int> GetFeedbacksCount(FeedbackState state);
        Task RecycleFeedbacks(int[] feedbackIds);
        Task ConfirmFeedbackAnswer(int feedbackId, int answerId);
        Task AddTagToFeedback(int feedbackId, Tag tag);
        Task<FeedbackReport> GetFeedbackDetailsBySerialNumber(string serialNumber);
        Task<IEnumerable<Tag>> GetTagsForOneFeedback(int feedbackId);
        Task DeleteTagFromFeedback(int feedbackId, int tagId);
        Task SubmitFeedbacksToCustomer(int[] feedbackIds);
        Task SendToReadyToSendCustomer(int[] feedbackIds);
    }
}
