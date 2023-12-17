using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TblTag: Tag
    {

        public TblTag()
        {


            TblRelationshipTagFeedbakIc = new HashSet<RelationshipTagFeedbak>();
        }



        public ICollection<RelationshipTagFeedbak> TblRelationshipTagFeedbakIc { get; set; }

        public ICollection<RelationshipTreeTag> TblRelationshipTreeTagIc { get; set; }
    }
}
