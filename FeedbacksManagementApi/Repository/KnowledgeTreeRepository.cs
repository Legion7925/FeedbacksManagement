using AutoMapper;
using Domain.Entities;
using Domain.Model;
using Infrastructure.Data;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace FeedbacksManagementApi.Repository
{
    public interface IKnowledgeTreeRepository
    {
        Task Edit(BaseKnowledgeTree argo, int id);
        Task Insert(BaseKnowledgeTree argo);
        Task<HashSet<KnowledgeTreeReport>> ListTree();
        Task Remove(int id);
        Task AddExpertToBranch(int expertId, int nodeId);
        Task AddTagToBranch(Tag tag, int nodeId);
        Task UpdateExpertBranch(int id, int nodeId);
        Task DeleteExpertFromBranch(int id);
        IEnumerable<TagsBranchModel> GetTagsForBranch(int branchId);
        IEnumerable<ExpertsBranchModel> GetExpertsForBranch(int branchId);
        Task DeleteTagFromBranch(int relationId);
    }

    public class KnowledgeTreeRepository : IKnowledgeTreeRepository
    {
        private readonly FeedbacksDbContext context;
        private readonly IMapper mapper;

        public KnowledgeTreeRepository(FeedbacksDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        /// <summary>
        /// ایجاد شاخه
        /// </summary>
        /// <param name="argo"></param>
        /// <returns></returns>
        public async Task Insert(BaseKnowledgeTree argo)
        {
            if (await context.TblKnowledgeTree.AnyAsync(i => i.Name == argo.Name)) new Exception("todoo");
            await context.TblKnowledgeTree.AddAsync(new TblKnowledgeTree
            {
                Name = argo.Name,
                ParentId = argo.ParentId,

            });

            await context.SaveChangesAsync();
        }
        /// <summary>
        /// ویرایش شاخه
        /// </summary>
        /// <param name="argo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Edit(BaseKnowledgeTree argo, int id)
        {
            var ch = await context.TblKnowledgeTree.FirstOrDefaultAsync(i => i.Id == id);
            if (ch == null) throw new Exception("کد شاخه یافت نشد");
            ch.Name = argo.Name;
            ch.ParentId = argo.ParentId;
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// حذف شاخه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Remove(int id)
        {
            var ch = await context.TblKnowledgeTree.FirstOrDefaultAsync(i => i.Id == id);
            if (ch == null) throw new Exception("todoo");
            context.TblKnowledgeTree.Remove(ch);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// اضافه کردن تگ به شاخه
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AddTagToBranch(Tag tag,int nodeId)
        {
            var nodeExist = await context.TblKnowledgeTree.AnyAsync(i => i.Id == nodeId);
            if (!nodeExist) throw new AppException("کد درخت یافت نشد");
            var insertTag = mapper.Map<TblTag>(tag);
            await context.TblTag.AddAsync(insertTag);
            await context.SaveChangesAsync();
            await context.TblRelationshipTreeTag.AddAsync(new RelationshipTreeTag
            {
                FkIdTag = insertTag.Id,
                FkIdTree = nodeId
            });
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// حذف تگ از شاخه
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteTagFromBranch(int relationId)
        {
            var branchTag = await context.TblRelationshipTreeTag.FirstOrDefaultAsync(i => i.Id == relationId);
            if (branchTag == null) throw new AppException("مورد یافت نشد");
            context.TblRelationshipTreeTag.Remove(branchTag);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// دریافت لیست تگ های یک شاخه
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public IEnumerable<TagsBranchModel> GetTagsForBranch(int branchId)
        {
            var exist = context.TblKnowledgeTree.Any(i => i.Id == branchId);
            if (exist is not true) throw new AppException("مورد یافت نشد");
            var tags = context.TblRelationshipTreeTag.Where(i => i.FkIdTree == branchId)
                .Include(i => i.TblTagVi).Select(e => new TagsBranchModel
                {
                    tagId = e.TblTagVi.Id,
                    TagName = e.TblTagVi.Name,
                    RelationId = e.Id
                }).ToList();
            return tags;
        }
        /// <summary>
        /// اضافه کردن کارشناس به شاخه
        /// </summary>
        /// <param name="expertId"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task AddExpertToBranch(int expertId , int nodeId)
        {
            var nodeExist = await context.TblKnowledgeTree.AnyAsync(i => i.Id == nodeId);
            if (!nodeExist) throw new AppException("کد درخت یافت نشد");
            var expertExist = await context.TblExpert.AnyAsync(i => i.Id == expertId);
            if (!expertExist) throw new AppException("کد متخصص یافت نشد");
            await context.TblRelationshipTreeExpert.AddAsync(new RelationshipTreeExpert
            {
                FkIdExpert = expertId,
                FkIdTree = nodeId
            });
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// ویرایش متخصص شاخه
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task UpdateExpertBranch(int id, int nodeId)
        {
            var branchExpert = await context.TblRelationshipTreeExpert.FirstOrDefaultAsync(i => i.Id == id);
            if (branchExpert == null) throw new AppException("مورد یافت نشد");
            branchExpert.FkIdTree = nodeId;
            await context.SaveChangesAsync();     
        }
        /// <summary>
        /// حذف متخصص از شاخه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task DeleteExpertFromBranch(int id)
        {
            var branchExpert = await context.TblRelationshipTreeExpert.FirstOrDefaultAsync(i => i.Id == id);
            if (branchExpert == null) throw new AppException("مورد یافت نشد");
            context.TblRelationshipTreeExpert.Remove(branchExpert);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// دریافت کارشناسان شاخه
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public IEnumerable<ExpertsBranchModel> GetExpertsForBranch(int branchId)
        {
            var exist = context.TblKnowledgeTree.Any(i=> i.Id == branchId);
            if (exist is not true) throw new AppException("مورد یافت نشد");
            var experts = context.TblRelationshipTreeExpert.Where(i => i.FkIdTree == branchId)
                .Include(i => i.TblExpertVi).Select(e => new ExpertsBranchModel
                {
                    RelationId = e.Id,
                    ExpertName = e.TblExpertVi.FirstName + " " + e.TblExpertVi.LastName,
                });
            return experts.AsEnumerable();
        }
        /// <summary>
        /// لیست شاخه ها
        /// </summary>
        /// <returns></returns>
        public async Task<HashSet<KnowledgeTreeReport>> ListTree()
        {
            var result = await context.TblKnowledgeTree.AsNoTracking().Where(i => i.ParentId == null).Include(i => i.TblKnowledgeTreeIc).ToListAsync();
            return await FillList(result);
        }
        /// <summary>
        /// پر کردن لیست شاخه ها
        /// </summary>
        /// <param name="listIn"></param>
        /// <returns></returns>
        private async Task<HashSet<KnowledgeTreeReport>> FillList(List<TblKnowledgeTree> listIn)
        {
            HashSet<KnowledgeTreeReport> list = new HashSet<KnowledgeTreeReport>();
            foreach (var item in listIn)
            {
                var init = await context.TblKnowledgeTree.AsNoTracking().Where(i => i.ParentId == item.Id).Include(i => i.TblKnowledgeTreeIc).ToListAsync();
                var re = new KnowledgeTreeReport
                {
                    Id = item.Id,
                    Name = item.Name,
                    ParentId = item.ParentId,
                    KnowledgeTreeChildren = await FillList(init)
                };
                list.Add(re);
            }
            return list;
        }
    }
}
