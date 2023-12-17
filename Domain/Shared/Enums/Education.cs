namespace Domain.Shared.Enums
{
    public enum Education : byte
    {
        /// <summary>
        /// همه
        /// </summary>
        All = 0,
        /// <summary>
        /// بدون مدرک
        /// </summary>
        None = 1,
        /// <summary>
        /// دیپلم
        /// </summary>
        Diploma = 2,
        /// <summary>
        /// فوق دیپلم
        /// </summary>
        Associate = 3,
        /// <summary>
        /// کارشناسی
        /// </summary>
        Bachelor = 4,
        /// <summary>
        /// کارشناسی ارشد
        /// </summary>
        Graduate = 5,
        /// <summary>
        /// دکترا
        /// </summary>
        Doctoral = 6,
        /// <summary>
        /// فوق دکترا
        /// </summary>
        Professional = 7,
    }
}
