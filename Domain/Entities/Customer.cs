using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        [Required]
        public string NameAndFamily { get; set; }
        /// <summary>
        /// ایمیل
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// شماره همراه
        /// </summary>
        [Required]
        public string Phone { get; set; }

      
    }
}
