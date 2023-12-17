using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public interface ITagsRepository
    {
        Task AddTag(Tag tagBase);
        Task DeleteTag(int tagId);
        IEnumerable<Tag> GetTags();
        Task<Tag> GetTagById(int tagId);
        Task UpdateTag(Tag tag, int tagId);
    }

    public class TagsRepository : ITagsRepository
    {
        private readonly FeedbacksDbContext context;
        private readonly IMapper mapper;

        public TagsRepository(FeedbacksDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// دریافت لیست تگ های ثبت شده
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tag> GetTags()
        {
            return context.TblTag.AsNoTracking().Select(t => new Tag
            {
                Id = t.Id,
                Name = t.Name,
            });
        }
        /// <summary>
        /// دریافت یک تگ بر اساس آیدی
        /// </summary>
        /// <param name="tagId">آیدی مورد</param>
        /// <returns></returns>
        public async Task<Tag> GetTagById(int tagId)
        {

            var tag = await context.TblTag.FirstOrDefaultAsync(i => i.Id == tagId);
            if (tag is null) throw new AppException("کد تگ یافت نشد");
            return new Tag
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }
        /// <summary>
        /// اضافه کردن تگ جدید
        /// </summary>
        /// <param name="tagBase"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AddTag(Tag tagBase)
        {
            var insert = mapper.Map<TblTag>(tagBase);
            await context.TblTag.AddAsync(insert);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// ویرایش تگ
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task UpdateTag(Tag tag, int tagId)
        {
            var foundTag = await context.TblTag.FirstOrDefaultAsync(i => i.Id == tagId);
            if (tag is null) throw new AppException("کد تگ یافت نشد");
            foundTag!.Name = tag.Name;
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// حذف تگ
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteTag(int tagId)
        {
            var foundTag = await context.TblTag.AsNoTracking().FirstOrDefaultAsync(i => i.Id == tagId);
            var tagEntity = mapper.Map<TblTag>(foundTag);
            context.TblTag.Remove(tagEntity);
            await context.SaveChangesAsync();
        }
    }
}
