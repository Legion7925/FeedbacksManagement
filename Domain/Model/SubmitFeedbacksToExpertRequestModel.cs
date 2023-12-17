using System.ComponentModel.DataAnnotations;

namespace FeedbacksManagementApi.Model
{
    public class SubmitFeedbacksToExpertRequestModel
    {
        [Required]
        public int[] FeedbackIds { get; set; } = new int[] {};

        [Required]
        public int ExpertId { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}
