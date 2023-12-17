using Domain.Entities;
using Domain.Shared.Enums;

namespace FeedbacksManagementApi.Model
{
    public class FeedbackReportFilterModel
    {
        public int Take { get; set; }

        public int Skip { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? ReferralDate { get; set; }

        public DateTime? RespondDate { get; set; }

        public string? SerialNumber { get; set; }

        public string? Title { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public int ExpertId { get; set; }
        public int SpecialityId { get; set; }

        public string? Description { get; set; }

        public string? Respond { get; set; }

        public Source Source { get; set; }

        public FeedbackState State { get; set; }

        public Priority Priorty { get; set; }

        public IEnumerable<int>? Tags { get; set; }

    }
}
