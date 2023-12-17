namespace Domain.Shared.Enums;

public enum FeedbackState : byte
{
    All = 0,
    /// <summary>
    /// آماده ارسال
    /// </summary>
    ReadyToSend = 1,
    /// <summary>
    /// ارسال شده به متخصص
    /// </summary>
    SentToExpert = 2,
    /// <summary>
    /// حذف شده
    /// </summary>
    Deleted = 3,
    /// <summary>
    /// بایگانی شده
    /// </summary>
    Archived = 4,
    /// <summary>
    /// پاسخ داده شده
    /// </summary>
    Answered = 5,
    /// <summary>
    /// پاسخ داده شده و پاسخ تایید شده
    /// </summary>
    Completed = 6,
}
