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
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyRepository specialtyRepository;

        public SpecialtiesController(ISpecialtyRepository specialtyRepository)
        {
            this.specialtyRepository = specialtyRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Specialty>> GetSpecialties()
        {
            try
            {
                return Ok(specialtyRepository.GetSpecialties());
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت لیست تخصص ها");
            }
        }
    }
}
