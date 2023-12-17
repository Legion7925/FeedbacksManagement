using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FeedbacksDbContext context;

        public ProductRepository(FeedbacksDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// دریافت لیست محصولات
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetPorducts()
        {
            return context.TblProduct.AsNoTracking().AsEnumerable();
        }
    }
}
