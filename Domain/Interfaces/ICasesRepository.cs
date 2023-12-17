using Domain.Entities;

namespace Domain.Interfaces;

public interface ICasesRepository
{
    Task AddCase(CaseBase feedbackCase);
    Task DeleteCase(int caseId);
    Task DeleteMultipleCases(int[] caseIds);
    Task<CaseReport> GetOneCase(int caseId);
    IEnumerable<CaseReport> GetCases(int take, int skip);
    Task<int> GetCasesCount();
    Task UpdateCase(CaseBase feedbackCase, int caseId);
    Task SubmitForRespond(int caseId);
}
