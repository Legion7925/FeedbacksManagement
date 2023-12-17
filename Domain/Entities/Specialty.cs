using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Specialty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// نام گروه
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// توضیحات
        /// </summary>
        public string? Description { get; set; }

       
    }
}
