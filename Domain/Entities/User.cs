using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User
{
    /// <summary>
    /// آیدی
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    /// <summary>
    /// نام
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// نام خانوادگی
    /// </summary>
    public string? FamilyName { get; set; }
    /// <summary>
    /// آدرس ایمیل
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// شماره موبایل
    /// </summary>
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// نام کاربری
    /// </summary>
    public string? Username { get; set; }
    /// <summary>
    /// کلمه عبور
    /// </summary>
    public string? Password { get; set; }
    /// <summary>
    /// تاریخ ساخت
    /// </summary>
    public DateTime Created { get; set; }
    /// <summary>
    /// تاریخ ویرایش
    /// </summary>
    public DateTime Updated { get; set; }
    /// <summary>
    /// تاریخ ویرایش پسورد
    /// </summary>
    public DateTime PasswordReset { get; set; }

}
