using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FeedbacksManagementApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CasesController : ControllerBase
{
    private readonly ICasesRepository caseRepository;

    public CasesController(ICasesRepository caseRepository)
    {
        this.caseRepository = caseRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CaseReport>> GetCases(int skip , int take)
    {
        try
        {
            return Ok(caseRepository.GetCases(take,skip));
        }
        catch(AppException ax)
        {
            return BadRequest(ax.Message);
        }
        catch(Exception ex)
        {
            AppLogger.WriteLog(ex.Message, ex);
            return BadRequest("خطا در دریافت موارد");
        }
    }

    [HttpGet]
    public async Task<ActionResult<int>> GetCasesCount()
    {
        try
        {
            return Ok(await caseRepository.GetCasesCount());
        }
        catch(AppException ax)
        {
            return BadRequest(ax.Message);
        }
        catch(Exception ex)
        {
            AppLogger.WriteLog(ex.Message, ex);
            return BadRequest("خطا در دریافت تعداد");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCase([FromBody]CaseBase @case)
    {
        try
        {
            await caseRepository.AddCase(@case);
            return Ok("مورد جدید با موفقیت اضافه شد");
        }
        catch (AppException ax)
        {
            return BadRequest(ax.Message);
        }
        catch (Exception ex)
        {
            AppLogger.WriteLog(ex.Message, ex);
            return BadRequest("خطا در اضافه کردن مورد");
        }
    }

    [HttpPost]
    [Route("{caseId}")]
    public async Task<IActionResult> SubmitCaseForAnswer([FromRoute] int caseId)
    {
        try
        {
            await caseRepository.SubmitForRespond(caseId);
            return Ok("ارسال مورد برای پاسخ دهی با موفقیت انجام شد");
        }
        catch (AppException ax)
        {
            return BadRequest(ax.Message);
        }
        catch (Exception ex)
        {
            AppLogger.WriteLog(ex.Message, ex);
            return BadRequest("خطا در ارسال برای پاسخ دهی");
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<CaseReport>> GetCaseById(int id)
    {
        try
        {
            return Ok(await caseRepository.GetOneCase(id));
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

    [HttpPut]
    [Route("{caseId}")]
    public async Task<IActionResult> UpdateCase([FromBody]CaseBase @case , [FromRoute] int caseId)
    {
        try
        {
            await caseRepository.UpdateCase(@case, caseId);
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
    [Route("{caseId}")]
    public async Task<IActionResult> DeleteCase([FromRoute] int caseId)
    {
        try
        {
            await caseRepository.DeleteCase(caseId);
            return Ok("حذف مورد با موفقیت انجام شد");
        }
        catch (AppException ax)
        {
            return BadRequest(ax.Message);
        }
        catch (Exception ex)
        {
            AppLogger.WriteLog(ex.Message, ex);
            return BadRequest("خطا در حذف مورد");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMultipleCases([FromBody] int[] caseIds)
    {
        try
        {
            await caseRepository.DeleteMultipleCases(caseIds);
            return Ok("حذف موارد ارسالی با موفقیت انجام شد");
        }
        catch (AppException ax)
        {
            return BadRequest(ax.Message);
        }
        catch (Exception ex)
        {
            AppLogger.WriteLog(ex.Message, ex);
            return BadRequest("خطا در حذف موارد");
        }
    }
}
