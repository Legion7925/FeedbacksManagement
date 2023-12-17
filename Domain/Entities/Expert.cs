using Domain.Helper;
using Domain.Shared.Enums;
using PersianDate.Standard;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ExpertBase
    {
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
        /// تاریخ تولد
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// شماره پاسپورت
        /// </summary>
        public string? PassportNumber { get; set; }
        /// <summary>
        /// جنسیت
        /// </summary>
        public Gender Gender { get; set; }
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
        /// شغل
        /// </summary>
        public string? Occupation { get; set; }
        /// <summary>
        /// شماره تلفن ثابت
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string? MobileNumber { get; set; }
        /// <summary>
        /// آدرس ایمیل
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// آدرس
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// کلید خارجی از جدول تخصص 
        /// </summary>

        public int FkIdSpecialty { get; set; }

    }
    public class Expert : ExpertBase
    {
        [Key]   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string EducationTranslate => Education.TranslateEducation();

        public string GenderTranslate => Gender.TranslateGender();

        public string CreatedFa => Created.ToFa();

        public int CountAnswer { get; set; }

        /// <summary>
        /// تاریخ عضویت
        /// </summary>
        public DateTime Created { get; set; }

    }
}
