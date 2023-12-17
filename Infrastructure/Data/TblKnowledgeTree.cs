using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TblKnowledgeTree: KnowledgeTree
    {
        public TblKnowledgeTree Parent { get; set; }
        public ICollection<TblKnowledgeTree> TblKnowledgeTreeIc { get; } = new List<TblKnowledgeTree>();

        public ICollection<RelationshipTreeTag> TblRelationshipTreeTagIc { get; set; }

        public ICollection<RelationshipTreeExpert> TblRelationshipTreeExpertIc { get; set; }


    }
}
