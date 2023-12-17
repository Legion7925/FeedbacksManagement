using Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ExpertReportFilterModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        /// <summary>
        /// نام 
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی
        /// </summary>  
        public string? LastName { get; set; }
        /// <summary>
        /// کد ملی
        /// </summary>
        public string? NationalCode { get; set; }
        /// <summary>
        /// شماره پاسپورت
        /// </summary>
        public string? PassportNumber { get; set; }
        /// <summary>
        /// ملیت
        /// </summary>
        public string? Nationality { get; set; }
        /// <summary>
        /// زبان
        /// </summary>
        public string? Language { get; set; }
        /// <summary>
        /// تحصیلات
        /// </summary>
        public Education Education { get; set; }
        /// <summary>
        /// رشته تحصیلی
        /// </summary>
        public string? FieldOfStudy { get; set; }
        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string? MobileNumber { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// کلید خارجی از جدول تخصص 
        /// </summary>
        public int FkIdSpecialty { get; set; }
    }
}
