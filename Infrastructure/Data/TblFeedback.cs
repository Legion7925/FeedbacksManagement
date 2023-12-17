using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TblFeedback: Feedback
    {

        public TblFeedback() {


            TblRelationshipTagFeedbakIc = new HashSet<RelationshipTagFeedbak>();
            TblRelationshipExpertFeedbakIc = new HashSet<RelationshipExpertFeedbak>();
            TblAnsowerIc = new HashSet<TblAnsower>();
        }

       
        public virtual TblCustomer TblCustomerVi { get; set; }
        public virtual TblProduct TblProductVi { get; set; }
        public ICollection<RelationshipTagFeedbak> TblRelationshipTagFeedbakIc { get; set; }
        public ICollection<RelationshipExpertFeedbak> TblRelationshipExpertFeedbakIc { get; set; }
        public ICollection<TblAnsower> TblAnsowerIc { get; set; }
    }
}
