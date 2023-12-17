using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class RelationshipExpertFeedbak: ExpertFeedbak
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        

        public virtual TblFeedback TblFeedbackVi { get; set; }
        public virtual TblExpert? TblExpertVi { get; set; }
        public virtual TblSpecialty? TblSpecialtyVi { get; set; }

        

    }
}
