using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TblCase: Case
    {

        public virtual TblCustomer TblCustomerVi { get; set; }
        public virtual TblProduct TblProductVi { get; set; }
    }
}
