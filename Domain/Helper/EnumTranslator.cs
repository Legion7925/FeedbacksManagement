using Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper
{
    /// <summary>
    /// کلاس ترجمه اینام های برنامه به فارسی
    /// </summary>
    public static class EnumTranslator
    {
        public static string TranslateSource(this Source source)
        {
            switch (source)
            {
                case Source.Email:
                    return "ایمیل";
                case Source.SMS:
                    return "پیامک";
                case Source.Site:
                    return "وب سایت";
                case Source.MobileApp:
                    return "نرم افزار موبایل";
                default:
                    return "ناشناس";
            }
        }
        public static string state(this FeedbackState state)
        {
            switch (state)
            {
                case FeedbackState.ReadyToSend:
                    return "آماده ارسال";
                case FeedbackState.SentToExpert:
                    return "ارسال شده به متخصص";
                case FeedbackState.Deleted:
                    return "حذف شده";
                case FeedbackState.Archived:
                    return "بایگانی شده";
                default:
                    return "نامشخص";
            }
        }

        public static string TranslatePoriorty(this Priority priority)
        {
            switch (priority)
            {
                case Priority.Low:
                    return "پایین";
                case Priority.Medium:
                    return "معمولی";
                case Priority.High:
                    return "بالا";
                default:
                    return "نامشخص";
            }
        }

        public static string TranslateGender(this Gender gender)
        {
            switch (gender)
            {
                case Gender.Unknown:
                    return "نامشخص";
                case Gender.Male:
                    return "مرد";
                case Gender.Female:
                    return "زن";
                default:
                    return "نامشخص";
            }
        }

        public static string TranslateEducation(this Education education)
        {
            switch (education)
            {
                case Education.All:
                    return "همه";
                case Education.None:
                    return "بدون مدرک";
                case Education.Diploma:
                    return "دیپلم";
                case Education.Associate:
                    return "فوق دیپلم";
                case Education.Bachelor:
                    return "کارشناسی";
                case Education.Graduate:
                    return "کارشناسی ارشد";
                case Education.Doctoral:
                    return "دکترا";
                case Education.Professional:
                    return "فوق دکترا";
                default:
                    return "نام مشخص";
            }
        }
    }
}
