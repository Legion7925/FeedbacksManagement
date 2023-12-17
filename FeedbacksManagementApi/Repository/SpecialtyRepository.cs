using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public class SpecialtyRepository : ISpecialtyRepository
    {
        private readonly FeedbacksDbContext context;

        public SpecialtyRepository(FeedbacksDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// دریافت لیست گروه متخصصین
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Specialty> GetSpecialties()
        {
            return context.TblSpecialtie.AsNoTracking().AsEnumerable();
        }
    }
}
