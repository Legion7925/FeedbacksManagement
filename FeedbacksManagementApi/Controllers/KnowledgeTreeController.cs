using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using Domain.Model;
using FeedbacksManagementApi.Repository;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedbacksManagementApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KnowledgeTreeController : ControllerBase
    {
        private readonly IKnowledgeTreeRepository knowledgeTreeRepository;

        public KnowledgeTreeController(IKnowledgeTreeRepository knowledgeTreeRepository)
        {
            this.knowledgeTreeRepository = knowledgeTreeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<HashSet<KnowledgeTreeReport>>> GetKnowledgeTrees()
        {
            try
            {
                return Ok(await knowledgeTreeRepository.ListTree());
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت درخت");
            }
        }

        [HttpGet]
        [Route("branchId")]
        public ActionResult<IEnumerable<TagsBranchModel>> GetTagsForBranch(int branchId)
        {
            try
            {
                return Ok(knowledgeTreeRepository.GetTagsForBranch(branchId));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت لیست تگ");
            }
        }

        [HttpGet]
        [Route("branchId")]
        public ActionResult<IEnumerable<ExpertsBranchModel>> GetExpertsForBranch(int branchId)
        {
            try
            {
                return Ok(knowledgeTreeRepository.GetExpertsForBranch(branchId));
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت متخصصین");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBranch([FromBody] BaseKnowledgeTree branch)
        {
            try
            {
                await knowledgeTreeRepository.Insert(branch);
                return Ok("شاخه جدید با موفقیت اضافه شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در اضافه کردن شاخه");
            }
        }

        [HttpPost]
        [Route("nodeId")]
        public async Task<IActionResult> AddTagToBranch(int nodeId ,[FromBody] Tag tag)
        {
            try
            {
                await knowledgeTreeRepository.AddTagToBranch(tag , nodeId);
                return Ok("تگ جدید با موفقیت اضافه شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در اضافه کردن تگ");
            }
        }

        [HttpPost]
        [Route("nodeId/expertId")]
        public async Task<IActionResult> AddExpertToBranch(int nodeId ,int expertId)
        {
            try
            {
                await knowledgeTreeRepository.AddExpertToBranch(expertId,nodeId);
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

        [HttpPut]
        public async Task<IActionResult> UpdateExpertBranch(UpdateExpertBranchRequestModel updateExpertBranchRequestModel)
        {
            try
            {
                await knowledgeTreeRepository.UpdateExpertBranch(updateExpertBranchRequestModel.RelationId, updateExpertBranchRequestModel.TreeId);
                return Ok("ویرایش متخصص با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در ویرایش متخصص");
            }
        }

        [HttpDelete]
        [Route("{relationId}")]
        public async Task<IActionResult> DeleteExpertFromBranch([FromRoute] int relationId)
        {
            try
            {
                await knowledgeTreeRepository.DeleteExpertFromBranch(relationId);
                return Ok("حذف متخصص با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در حذف متخصص");
            }
        }

        [HttpDelete]
        [Route("{relationId}")]
        public async Task<IActionResult> DeleteTagFromBranch([FromRoute] int relationId)
        {
            try
            {
                await knowledgeTreeRepository.DeleteTagFromBranch(relationId);
                return Ok("حذف تگ با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در حذف تگ");
            }
        }

        [HttpPut]
        [Route("{branchId}")]
        public async Task<IActionResult> UpdateBranch([FromBody] BaseKnowledgeTree branch, [FromRoute] int branchId)
        {
            try
            {
                await knowledgeTreeRepository.Edit(branch, branchId);
                return Ok("ویرایش شاخه با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در ویرایش شاخه");
            }
        }

        [HttpDelete]
        [Route("{branchId}")]
        public async Task<IActionResult> DeleteBranch([FromRoute] int branchId)
        {
            try
            {
                await knowledgeTreeRepository.Remove(branchId);
                return Ok("حذف شاخه با موفقیت انجام شد");
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در حذف شاخه");
            }
        }
    }
}
