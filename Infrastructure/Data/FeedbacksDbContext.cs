using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Data
{
    /// <summary>
    /// کلاس کانتکست برای کانفیگ های دیتابیس میباشد
    /// </summary>
    public class FeedbacksDbContext : DbContext
    {
        public FeedbacksDbContext()
        {

        }

        public FeedbacksDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        //جداولی که انتیتی فریمورک از روی آن جداول دیتابیس را تولید میکند

        /// <summary>
        /// جدول مشتری
        /// </summary>
        public DbSet<TblCustomer> TblCustomer { get; set; }
        /// <summary>
        /// جدول واسطه فیدبک و کارشناس
        /// </summary>
        public DbSet<RelationshipExpertFeedbak> TblRelationshipExpertFeedbak { get; set; }
        /// <summary>
        /// جدول واسطه تگ و فیدبک
        /// </summary>
        public DbSet<RelationshipTagFeedbak> TblRelationshipTagFeedbak { get; set; }
        /// <summary>
        /// جدول کارشناسان
        /// </summary>
        public DbSet<TblExpert> TblExpert { get; set; }
        /// <summary>
        /// جدول فیدبک
        /// </summary>
        public DbSet<TblFeedback> TblFeedback { get; set; }
        /// <summary>
        /// جدول تگ ها
        /// </summary>
        public DbSet<TblTag> TblTag { get; set; }
        /// <summary>
        /// جدول محصولات
        /// </summary>
        public DbSet<TblProduct> TblProduct { get; set; }

        /// <summary>
        /// جدول مورد های رسیده
        /// </summary>
        public DbSet<TblCase> TblCase { get; set; }

        //گروه متخصصین
        public DbSet<TblSpecialty> TblSpecialtie { get; set; }
        /// <summary>
        /// جدول پاسخ ها
        /// </summary>
        public DbSet<TblAnsower> TblAnsower { get; set; }

        /// <summary>
        /// جدول درخت دانش
        /// </summary>
        public DbSet<TblKnowledgeTree> TblKnowledgeTree { get; set; }
        /// <summary>
        /// جدول واسط درخت دانش و کارشناس
        /// </summary>
        public DbSet<RelationshipTreeExpert> TblRelationshipTreeExpert { get; set; }
        /// <summary>
        /// جدول واسط تگ و درخت دانش
        /// </summary>
        public DbSet<RelationshipTreeTag> TblRelationshipTreeTag { get; set; }

        /// <summary>
        /// جدول صفحات
        /// </summary>
        public DbSet<Page> TblPages { get; set; }
        /// <summary>
        /// جدول کاربران
        /// </summary>
        public DbSet<User> TblUsers { get; set; }
       /// <summary>
       /// جدول واسط کاربران و صفحات برنامه
       /// </summary>
        public DbSet<UserPages> TblUserPages { get; set; }

        /// <summary>
        /// در این تابع روابط بین جدول ها مشخص میشود
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblProduct>()
               .HasMany(e => e.TblFeedbackIc)
               .WithOne(e => e.TblProductVi)
               .HasForeignKey(e => e.FkIdProduct).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblProduct>()
              .HasMany(e => e.TblCaseIc)
              .WithOne(e => e.TblProductVi)
              .HasForeignKey(e => e.FkIdProduct).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblCustomer>()
              .HasMany(e => e.TblFeedbackIc)
              .WithOne(e => e.TblCustomerVi)
              .HasForeignKey(e => e.FkIdCustomer).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblCustomer>()
              .HasMany(e => e.TblCaseIc)
              .WithOne(e => e.TblCustomerVi)
              .HasForeignKey(e => e.FkIdCustomer).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblExpert>()
              .HasMany(e => e.TblRelationshipTagFeedbakIs)
              .WithOne(e => e.TblExpertVi)
              .HasForeignKey(e => e.FkIdExpert).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TblTag>()
              .HasMany(e => e.TblRelationshipTagFeedbakIc)
              .WithOne(e => e.TblTagVi)
              .HasForeignKey(e => e.FkIdTag).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<TblFeedback>()
              .HasMany(e => e.TblRelationshipTagFeedbakIc)
              .WithOne(e => e.TblFeedbackVi)
              .HasForeignKey(e => e.FkIdFeedback).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblFeedback>()
             .HasMany(e => e.TblRelationshipExpertFeedbakIc)
             .WithOne(e => e.TblFeedbackVi)
             .HasForeignKey(e => e.FkIdFeedback).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblFeedback>()
              .HasMany(e => e.TblAnsowerIc)
              .WithOne(e => e.TblFeedbackVi)
              .HasForeignKey(e => e.FkIdFeedback).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblSpecialty>()
               .HasMany(e => e.TblExpertIc)
               .WithOne(e => e.TblSpecialtyVi)
               .HasForeignKey(e => e.FkIdSpecialty).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblSpecialty>()
               .HasMany(e => e.TblRelationshipTagFeedbakIs)
               .WithOne(e => e.TblSpecialtyVi)
               .HasForeignKey(e => e.FkIdSpecialty).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblKnowledgeTree>()
                .HasMany(e => e.TblRelationshipTreeTagIc)
                .WithOne(e => e.TblKnowledgeTreeVi)
                .HasForeignKey(e => e.FkIdTree).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblKnowledgeTree>()
                .HasMany(e => e.TblRelationshipTreeExpertIc)
                .WithOne(e => e.TblKnowledgeTreeVi)
                .HasForeignKey(e => e.FkIdTree).OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<TblTag>()
                .HasMany(e => e.TblRelationshipTreeTagIc)
                .WithOne(e => e.TblTagVi)
                .HasForeignKey(e => e.FkIdTag).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TblExpert>()
                .HasMany(e => e.TblRelationshipTreeExpertIc)
                .WithOne(e => e.TblExpertVi)
                .HasForeignKey(e => e.FkIdExpert).OnDelete(DeleteBehavior.NoAction);
        }


    }
}
