using PersianDate.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ExpertOrGroupExpert
    {
        public int id { get; set; } 

        public string Name { get; set; } = string.Empty;

        public DateTime RefrralDate { get; set; }
        public string RefrralDateFa => RefrralDate.ToFa() ?? string.Empty;
    }
}
