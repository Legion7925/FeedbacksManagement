using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class SubmitFeedbackToExpertGroupRequestModel
    {
        [Required]
        public int[] FeedbackIds { get; set; } = new int[] { };

        [Required]
        public int ExpertGroupId { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }
    }
}
