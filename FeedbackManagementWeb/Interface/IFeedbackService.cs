using Domain.Entities;
using Domain.Model;
using Domain.Shared.Enums;
using FeedbacksManagementApi.Model;

namespace FeedbackManagementWeb.Interface
{
    public interface IFeedbackService
    {
        Task AddfeedbackByExpert(FeedbackBase feedback, int expertId);
        Task ArchiveFeedbacks(int[] feedbackIds);
        Task DeleteFeedbacks(int[] feedbackIds);
        Task<int> GetFeedbackCount(FeedbackState state);
        Task<IEnumerable<FeedbackReport>> GetFeedbackReport(FeedbackReportFilterModel filter);
        Task<int> GetFeedbackReportCount(FeedbackReportFilterModel filter);
        Task<IEnumerable<FeedbackReport>> GetFeedbacks(int take, int skip,FeedbackState state);
        Task SubmitFeedbacksToExpert(SubmitFeedbacksToExpertRequestModel feedbackRequest);
        Task SubmitFeedbacksToExpertGroup(SubmitFeedbackToExpertGroupRequestModel feedbackRequest);
        Task UpdateFeedback(FeedbackBase feedback, int feedbackId);
        Task RecycleFeedbacks(int[] feedbackIds);
        Task<FeedbackReport> GetFeedbackBySerialNumber(string serialNumber);
        Task<IEnumerable<Tag>> GetTagsForOneFeedback(int feedbackId);
        Task AddTagForFeedback(AddTagToFeedbackRequestModel model);
        Task DeleteTagFromFeedback(int feedbackId, int tagId);
        Task ConfirmFeedbackAnswer(int feedbackId, int answerId);
        Task SubmitFeedbackToCustomer(int[] feedbackIds);
        Task SendToReadyToSendCustomer(int[] feedbackIds);
        Task<FeedbackReport> GetFeedbackById(int feedbackId);
    }
}
