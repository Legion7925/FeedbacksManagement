using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TblAnsower: Ansower
    {

        public virtual TblFeedback TblFeedbackVi { get; set; }
    }
}
