﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RelationshipTreeTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual TblKnowledgeTree TblKnowledgeTreeVi { get; set; }
        public virtual TblTag TblTagVi { get; set; }

        public int FkIdTree { get; set; }

        public int FkIdTag { get; set; }

    }
}
