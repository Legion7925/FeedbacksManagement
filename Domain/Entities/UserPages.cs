using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class UserPages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// کلید خارجی کاربر
        /// </summary>
        public int FkIdUser { get; set; }
        /// <summary>
        /// کلید خارجی صفحه
        /// </summary>
        public int FkIdPage { get; set; }

    
      
    }
}
