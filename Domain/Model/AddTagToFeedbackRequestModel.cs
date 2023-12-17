using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class AddTagToFeedbackRequestModel
    {
        public int FeedbackId { get; set; }

        public Tag Tag { get; set; }
    }
}
