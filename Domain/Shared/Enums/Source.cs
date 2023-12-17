namespace Domain.Shared.Enums;

public enum Source : byte
{
    /// <summary>
    /// همه منشاء ها
    /// </summary>
    All = 0,
    /// <summary>
    /// ایمیل
    /// </summary>
    Email = 1,
    /// <summary>
    /// پیامک
    /// </summary>
    SMS = 2,
    /// <summary>
    /// وبسایت
    /// </summary>
    Site = 3,
    /// <summary>
    /// برنامه موبایل
    /// </summary>
    MobileApp= 4,
}
