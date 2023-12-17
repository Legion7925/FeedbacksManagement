using PersianDate.Standard;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AnsowerBase
    {
        /// <summary>
        ///  کلید خارجی کارشناس
        /// </summary>
        public int FkIdExpert { get; set; }
        /// <summary>
        /// کلید خارجی فیدبک
        /// </summary>
        public int FkIdFeedback { get; set; }
        /// <summary>
        /// تاریخ ثبت
        /// </summary>
        public DateTime RespondDate { get; set; }

        /// <summary>
        /// پاسخ مشکل
        /// </summary>
        public string? Respond { get; set; }


    }

    public class Ansower : AnsowerBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// تاریخ فارسی پاسخ
        /// </summary>
        public string RespondDateFa => RespondDate.ToFa();
        /// <summary>
        /// آیا پاسخ تایید شده است یا خیر
        /// </summary>
        public bool IsConfirmed { get; set; }

    }
}
