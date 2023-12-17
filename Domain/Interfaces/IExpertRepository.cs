using Domain.Entities;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IExpertRepository
    {
        Task AddExpert(ExpertBase expertBase);
        Task DeleteExpert(int expertId);
        Task<Expert?> GetExpertById(int expertId);
        IEnumerable<Expert> GetExperts();
        Task UpdateExpert(ExpertBase expertBase, int caseId);
        IEnumerable<Expert> GetExpertReport(ExpertReportFilterModel filter);
        Task<int> GetExpertReportCount(ExpertReportFilterModel filter);
    }
}
