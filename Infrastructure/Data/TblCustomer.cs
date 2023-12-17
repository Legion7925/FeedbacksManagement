using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TblCustomer: Customer
    {
       
        public TblCustomer() {

            TblFeedbackIc = new HashSet<TblFeedback>();
            TblCaseIc = new HashSet<TblCase>();

        }
        public ICollection<TblFeedback> TblFeedbackIc { get; set; }
        public ICollection<TblCase> TblCaseIc { get; set; }
    }
}
