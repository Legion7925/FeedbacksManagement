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
    public class TagsController : ControllerBase
    {
        private readonly ITagsRepository tagsRepository;

        public TagsController(ITagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetTags()
        {
            try
            {
                return Ok(tagsRepository.GetTags());
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت لیست تگ های موضوعی");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTag([FromBody] Tag tagBase)
        {
            try
            {
                await tagsRepository.AddTag(tagBase);
                return Ok("تگ جدید با موفقیت اضافه شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در اضافه کردن تگ جدید");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTagById(int id)
        {
            try
            {
                return Ok(await tagsRepository.GetTagById(id));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت اطلاعات تگ");
            }
        }

        [HttpPut]
        [Route("{tagId}")]
        public async Task<IActionResult> UpdateTag([FromBody] Tag tagBase, [FromRoute] int tagId)
        {
            try
            {
                await tagsRepository.UpdateTag(tagBase, tagId);
                return Ok("ویرایش تگ با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception)
            {
                return BadRequest("خطا در ویرایش کارشناس");
            }
        }

        [HttpDelete]
        [Route("{tagId}")]
        public async Task<IActionResult> DeleteTag([FromRoute] int tagId)
        {
            try
            {
                await tagsRepository.DeleteTag(tagId);
                return Ok("حذف تگ با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("خطا در حذف تگ");
            }
        }
    }
}
