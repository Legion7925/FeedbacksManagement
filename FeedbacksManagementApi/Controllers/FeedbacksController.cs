using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using Domain.Model;
using Domain.Shared.Enums;
using FeedbacksManagementApi.Model;
using FeedbacksManagementApi.Repository;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FeedbacksManagementApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackRepository feedbackRepository;

        public FeedbacksController(IFeedbackRepository feedbackRepository)
        {
            this.feedbackRepository = feedbackRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FeedbackReport>> GetFeedbacks(int take , int skip , FeedbackState state)
        {
            try
            {
                return Ok(feedbackRepository.GetFeedbacks(take,skip,state));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت موارد");
            }
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetFeedbacksCount(FeedbackState state)
        {
            try
            {
                return Ok(await feedbackRepository.GetFeedbacksCount(state));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت تعداد");
            }
        }

        [HttpGet]
        [Route("feedbackId")]
        public async Task<ActionResult<FeedbackReport>> GetFeedbackById(int feedbackId)
        {
            try
            {
                var feedback = await feedbackRepository.GetFeedbackDetailsById(feedbackId);

                if (feedback == null)
                    return NoContent();

                return Ok(feedback);
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت فیدبک");
            }
        }

        [HttpGet]
        public async Task<ActionResult<FeedbackReport>> GetFeedbackDetailsBySerialNumber(string serialNumber)
        {
            try
            {
                return Ok(await feedbackRepository.GetFeedbackDetailsBySerialNumber(serialNumber));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت مورد");
            }
        }

        [HttpGet]
        [Route("feedbackId")]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTagsForOneFeedback(int feedbackId)
        {
            try
            {
                return Ok(await feedbackRepository.GetTagsForOneFeedback(feedbackId));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت لیست تگ های مورد");
            }
        }

        [HttpPost]
        [Route("expertId")]
        public async Task<IActionResult> AddFeedbackByExpert(int expertId , [FromBody] AddFeedbackByExpertRequestModel feedbackByExpertRequestModel)
        {
            try
            {
                await feedbackRepository
                    .AddFeedbackByExpert(feedbackByExpertRequestModel ,feedbackByExpertRequestModel.IsAnswered , expertId);

                return Ok("مورد جدید با موفقیت اضافه شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در اضافه کردن مورد جدید");
            }
        }

        [HttpPut]
        [Route("feedbackId")]
        public async Task<IActionResult> UpdateFeedback([FromBody] FeedbackBase feedbackBase, int feedbackId)
        {
            try
            {
                await feedbackRepository.UpdateFeedback(feedbackBase, feedbackId);
                return Ok("ویرایش مورد با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در ویرایش مورد");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFeedbacks(int[] feedbackIds)
        {
            try
            {
                await feedbackRepository.DeleteFeedbacks(feedbackIds);
                return Ok("حذف مورد با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch(Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در حذف مورد");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendToReadyToSendCustomer(int[] feedbackIds)
        {
            try
            {
                await feedbackRepository.SendToReadyToSendCustomer(feedbackIds);
                return Ok("تایید موارد برای ارسال با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch(Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در تایید برای ارسال");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedbacksToExpert([FromBody] SubmitFeedbacksToExpertRequestModel submitFeedbackModel)
        {
            try
            {
                await feedbackRepository.SubmitFeedbacksToExpert(submitFeedbackModel);
                return Ok("مورد با موفقیت برای کارشناس ارسال شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در ارسال مورد برای کارشناس");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeedbacksToExpertGroup([FromBody] SubmitFeedbackToExpertGroupRequestModel submitFeedbackModel)
        {
            try
            {
                await feedbackRepository.SubmitFeedbacksToExpertGroup(submitFeedbackModel);
                return Ok("مورد با موفقیت برای گروه متخصصین ارسال شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در ارسال مورد برای گروه متخصصین");
            }
        }

        [HttpPost] 
        public async Task<IActionResult> SubmitFeedbacksToCustomer(int[] feedbackIds)
        {
            try
            {
                await feedbackRepository.SubmitFeedbacksToCustomer(feedbackIds);
                return Ok("موارد ارسالی با موفقیت برای مشتری");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در ارسال موارد");
            }
        }

        [HttpPost] 
        public async Task<IActionResult> ArchiveFeedbacks(int[] feedbackIds)
        {
            try
            {
                await feedbackRepository.ArchiveFeedbacks(feedbackIds);
                return Ok("موارد ارسالی با موفقیت بایگانی شدند");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در بایگانی موارد");
            }
        }

        [HttpPost] 
        public async Task<IActionResult> RecycleFeedbacks(int[] feedbackIds)
        {
            try
            {
                await feedbackRepository.RecycleFeedbacks(feedbackIds);
                return Ok("موارد ارسالی با موفقیت بازیابی شدند");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در بازیابی موارد");
            }
        }

        [HttpPost]
        public ActionResult<IEnumerable<FeedbackReport>> GetFeedbackReports([FromBody]FeedbackReportFilterModel filterModel)
        {
            try
            {
                var report = feedbackRepository.GetFeedbackReport(filterModel);
                return Ok(report);
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت گزارش");
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> GetFeedbackReportCount([FromBody] FeedbackReportFilterModel filterModel)
        {
            try
            {
                return Ok(await feedbackRepository.GetFeedbackReportCount(filterModel));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت تعداد");
            }
        }

        [HttpPost("feedbackId/answerId")]
        public async Task<IActionResult> ConfirmFeedbackAnswer(int feedbackId , int answerId)
        {
            try
            {
                await feedbackRepository.ConfirmFeedbackAnswer(feedbackId , answerId);
                return Ok("پاسخ با موفقیت تایید شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در تایید پاسخ");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTagToFeedback(AddTagToFeedbackRequestModel requestModel)
        {
            try
            {
                await feedbackRepository.AddTagToFeedback(requestModel.FeedbackId , requestModel.Tag);
                return Ok("تگ با موفقیت تایید شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در ثبت تگ برای مورد");
            }
        }

        [HttpDelete]
        [Route("feedbackId/tagId")]
        public async Task<IActionResult> DeleteTagFromFeedback(int feedbackId , int tagId)
        {
            try
            {
                await feedbackRepository.DeleteTagFromFeedback(feedbackId,tagId);
                return Ok("تگ با موفقیت حذف شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در حذف تگ برای مورد");
            }
        }
    }
}
