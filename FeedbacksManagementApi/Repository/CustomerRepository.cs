using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FeedbacksDbContext context;

        public CustomerRepository(FeedbacksDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// دریافت لیست مشتریان
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return context.TblCustomer.AsNoTracking().AsEnumerable();
        }
    }
}
