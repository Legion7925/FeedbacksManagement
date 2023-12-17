using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using FeedbacksManagementApi.Repository;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedbacksManagementApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository answerRepository;

        public AnswerController(IAnswerRepository answerRepository)
        {
            this.answerRepository = answerRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Ansower>> GetAnswersFroFeedback(int feedbackId)
        {
            try
            {
                return Ok(answerRepository.GetAnswersForFeedback(feedbackId));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت پاسخ ها");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAnswerForFeedback([FromBody]AnsowerBase answer)
        {
            try
            {
                await answerRepository.AddAnswer(answer);
                return Ok("پاسخ با موفقیت ثبت شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در ثبت پاسخ برای فیدبک");
            }
        }

    }
}
