using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TblExpert : Expert
    {
        public TblExpert()
        {

            TblRelationshipTagFeedbakIs = new HashSet<RelationshipExpertFeedbak>();
        }

        public ICollection<RelationshipExpertFeedbak> TblRelationshipTagFeedbakIs { get; set; }

        public virtual TblSpecialty TblSpecialtyVi { get; set; }

        public ICollection<RelationshipTreeExpert> TblRelationshipTreeExpertIc { get; set; }
    }
}
