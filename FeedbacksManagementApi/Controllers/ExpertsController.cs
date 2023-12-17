using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using Domain.Model;
using FeedbacksManagementApi.Model;
using FeedbacksManagementApi.Repository;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedbacksManagementApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpertsController : ControllerBase
    {
        private readonly IExpertRepository expertRepository;

        public ExpertsController(IExpertRepository expertRepository)
        {
            this.expertRepository = expertRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Expert>> GetExperts()
        {
            try
            {
                return Ok(expertRepository.GetExperts());
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت لیست کارشناسان");
            }
        }

        [HttpPost]
        public ActionResult<IEnumerable<Expert>> GetExpertReports([FromBody] ExpertReportFilterModel filterModel)
        {
            try
            {
                var report = expertRepository.GetExpertReport(filterModel);
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
        public async Task<ActionResult<int>> GetExpertReportCount([FromBody] ExpertReportFilterModel filterModel)
        {
            try
            {
                return Ok(await expertRepository.GetExpertReportCount(filterModel));
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


        [HttpPost]
        public async Task<IActionResult> AddExpert([FromBody] ExpertBase expert)
        {
            try
            {
                await expertRepository.AddExpert(expert);
                return Ok("کارشناس جدید با موفقیت اضافه شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در اضافه کردن کارشناس");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetExpertById(int id)
        {
            try
            {
                return Ok(await expertRepository.GetExpertById(id));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت اطلاعات کارشناس");
            }
        }

        [HttpPut]
        [Route("{expertId}")]
        public async Task<IActionResult> UpdateExpert([FromBody] ExpertBase expert, [FromRoute] int expertId)
        {
            try
            {
                await expertRepository.UpdateExpert(expert, expertId);
                return Ok("ویرایش کارشناس با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در ویرایش کارشناس");
            }
        }

        [HttpDelete]
        [Route("{expertId}")]
        public async Task<IActionResult> DeleteExpert([FromRoute] int expertId)
        {
            try
            {
                await expertRepository.DeleteExpert(expertId);
                return Ok("حذف کارشناس با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در حذف کارشناس");
            }
        }

    }
}
