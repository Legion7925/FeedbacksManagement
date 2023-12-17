using Domain.Entities;

namespace FeedbackManagementWeb.Interface
{
    public interface ICaseService
    {
        Task AddCase(CaseBase @case);
        Task DeleteCases(int[] caseIds);
        Task<IEnumerable<CaseReport>> GetCases(int take, int skip);
        Task SubmitCaseForAnswer(int caseId);
        Task UpdateCase(CaseBase @case, int caseId);
        Task<int> GetCasesCount();
    }
}
