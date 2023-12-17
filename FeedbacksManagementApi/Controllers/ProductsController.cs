using Domain.Entities;
using Domain.Helper;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeedbacksManagementApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return Ok(productRepository.GetPorducts());
            }
            catch (AppException ax)
            {
                return BadRequest(ax.Message);
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog(ex.Message, ex);
                return BadRequest("خطا در دریافت محصولات");
            }
        }
    }
}
