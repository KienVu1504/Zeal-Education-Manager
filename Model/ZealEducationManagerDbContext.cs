using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ZealEducationManager.Model
{
    public partial class ZealEducationManagerDbContext : DbContext
    {
        public ZealEducationManagerDbContext()
            : base("name=ZealEducationManagerDbContext")
        {
        }

        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Result> Results { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Exam>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Exam>()
                .Property(e => e.QuestionList)
                .IsUnicode(false);

            modelBuilder.Entity<Exam>()
                .Property(e => e.AnswerList)
                .IsUnicode(false);

            modelBuilder.Entity<Exam>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Exam>()
                .Property(e => e.ScoreList)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.PromotionPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.LinkVideo)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ListType)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.ResultQuiz)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.StartDateQuiz)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.StartTimeQuiz)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.FinishTimeQuiz)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.StartTimeEssay)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.FinishTimeEssay)
                .IsUnicode(false);

            modelBuilder.Entity<Result>()
                .Property(e => e.Score)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ProductList)
                .IsUnicode(false);
        }
    }
}
