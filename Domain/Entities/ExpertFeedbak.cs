using PersianDate.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// جدول واسط فیدبک و کارشناس
    /// </summary>
    public class ExpertFeedbak
    {
        /// <summary>
        /// کلید خارجی کارشناس
        /// </summary>
        public int? FkIdExpert { get; set; }
        /// <summary>
        /// کلید خارجی جدول گروه متخصصین
        /// </summary>
        public int? FkIdSpecialty { get; set; }
        /// <summary>
        /// کلید خارجی فیدبک
        /// </summary>
        public int FkIdFeedback { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// تاریخ ارجاع
        /// </summary>
        public DateTime RefrralDate { get; set; }

        /// <summary>
        /// تاریخ فارسی ارجاع
        /// </summary>
        public string RefrralDateFa => RefrralDate.ToFa();
    }
}
