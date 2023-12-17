using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Page
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// آدرس صفحه
        /// </summary>
        public string? PageUrl { get; set; }
        /// <summary>
        /// نام صفحه
        /// </summary>
        public string? PageName { get; set; }

     
    }
}
