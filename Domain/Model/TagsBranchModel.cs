using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class TagsBranchModel
    {
        public int RelationId { get; set; }

        public int tagId { get; set; }

        public string TagName { get; set; } = string.Empty;

    }
}
