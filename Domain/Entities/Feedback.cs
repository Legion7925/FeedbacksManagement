using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Domain.Helper;
using Domain.Model;
using Domain.Shared.Enums;
using PersianDate.Standard;

namespace Domain.Entities
{
    public class FeedbackBase
    {
        /// <summary>
        /// عنوان
        /// </summary>
        [Required]
        public string? Title { get; set; }
        /// <summary>
        /// شرح مشکل
        /// </summary>
        [Required]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// منبع
        /// </summary>
        [Required]
        public Source Source { get; set; }
        /// <summary>
        /// آدرس منبع
        /// </summary>
        [Required]
        public string SourceAddress { get; set; } = string.Empty;
        /// <summary>
        /// کلید خارجی کاربر
        /// </summary>
        [Required]
        public int FkIdCustomer { get; set; }
        /// <summary>
        /// کلید خارجی محصول
        /// </summary>
        [Required]
        public int FkIdProduct { get; set; }
        /// <summary>
        /// الحاقیات
        /// </summary>
        public string? Resources { get; set; }
        /// <summary>
        /// اولویت
        /// </summary>
        public Priority Priorty { get; set; }
    }

    public class Feedback : FeedbackBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// تاریخ ایجاد
        /// </summary>
        public DateTime Created { get; set; } = new DateTime(2000, 01, 01);

        /// <summary>
        /// تاریخ برگشت
        /// </summary>
        public DateTime? RespondDate { get; set; }

        /// <summary>
        /// تاریخچه ارجاع
        /// </summary>
        public DateTime? RefrralDate { get; set; }

        /// <summary>
        /// وضعیت فیدبک
        /// </summary>
        public FeedbackState State { get; set; }


        /// <summary>
        /// میزان شباهت
        /// </summary>
        public byte? Similarity { get; set; }


        /// <summary>
        /// شماره سریال
        /// </summary>
        [MaxLength(250)]
        public string? SerialNumber { get; set; }


        /// <summary>
        /// پاسخ تایید شده
        /// </summary>
        public string? Respond { get; set; }
    }

    public class FeedbackReport : Feedback
    {
        public string? ProductName { get; set; }

        public string? CustomerName { get; set; }

        public string SourceTranslate => Source.TranslateSource();

        public string StatusTranslate => State.state();

        public string PriortyTranslate => Priorty.TranslatePoriorty();

        public string RespondDateFa => RespondDate?.ToFa() ?? string.Empty;

        public string RefrerralDateFa => RefrralDate?.ToFa() ?? string.Empty;

        public string CreatedDateFa => Created.ToFa();

        public string TagsUs { get; set; } = string.Empty;

        public string ExpertUs { get; set; } = string.Empty;

        public List<ExpertOrGroupExpert> ExpertsOrGroupExpertsList { get; set; } = new();
    }
}
