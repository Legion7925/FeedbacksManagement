using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TblSpecialty: Specialty
    {
        public TblSpecialty() {
            TblExpertIc = new HashSet<TblExpert>();
            TblRelationshipTagFeedbakIs = new HashSet<RelationshipExpertFeedbak>();
        }
        public ICollection<TblExpert> TblExpertIc { get; set; }
        public ICollection<RelationshipExpertFeedbak> TblRelationshipTagFeedbakIs { get; set; }
    }
}
