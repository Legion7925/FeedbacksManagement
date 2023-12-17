using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RelationshipTagFeedbak
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public virtual TblFeedback TblFeedbackVi { get; set; }
        public virtual TblTag TblTagVi { get; set; } 
        
        public int FkIdFeedback { get; set; }
        public int  FkIdTag { get; set; }

    }
}
