using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{

    public class BaseKnowledgeTree
    {
        /// <summary>
        /// نام شاخه
        /// </summary>
        [Required]
        public string Name { get; set; }=string.Empty;
        /// <summary>
        /// آیدی والد
        /// </summary>
        public int? ParentId { get; set; }
    }

    public class KnowledgeTree: BaseKnowledgeTree
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

    }

    public class KnowledgeTreeReport : KnowledgeTree
    {
        /// <summary>
        /// لیست فرزندان شاخه
        /// </summary>
        public HashSet<KnowledgeTreeReport> KnowledgeTreeChildren { get; set; }=new HashSet<KnowledgeTreeReport>();

        /// <summary>
        /// تعداد فرزندان شاخه
        /// </summary>
        public int ChildrenCount => KnowledgeTreeChildren.Count();


        public bool IsExpanded { get; set; } = false;

        /// <summary>
        /// آیا شاخه فرزند دارد یا خیر
        /// </summary>
        public bool HasChild => KnowledgeTreeChildren != null && KnowledgeTreeChildren.Count > 0;
    }
}
