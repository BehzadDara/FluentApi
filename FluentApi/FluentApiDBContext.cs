using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluentApi
{
    public class FluentApiDBContext : DbContext
    {
        public DbSet<X1Model> X1Models { get; set; }

        public FluentApiDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<X1Model>()
                .HasKey(x => x.IntProperty);

            modelBuilder.Entity<X1Model>()
                .Property(x => x.StringProperty)
                .HasMaxLength(10)
                .IsRequired();

            modelBuilder.Entity<X1Model>()
                .Property(x => x.CreateDate)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            modelBuilder.Entity<X1Model>()
                .Property(x => x.UpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();
        }

        public IEnumerable<X1Model> Get()
        {
            return X1Models.ToList();
        }

        public X1Model? Get(int intProperty)
        {
            return X1Models.SingleOrDefault(x => x.IntProperty == intProperty);
        }

        public void Add(string stringProperty)
        {
            X1Models.Add(new X1Model
            {
                StringProperty = stringProperty
            });
            SaveChanges();
        }

        public void Update(int intProperty, string stringProperty)
        {
            if (Get(intProperty) is X1Model x1ModelNotNull)
            {
                x1ModelNotNull.StringProperty = stringProperty;
                x1ModelNotNull.UpdateDate = DateTime.Now;
                X1Models.Update(x1ModelNotNull);
                SaveChanges();
            }
        }

        [HttpDelete("{intProperty}")]
        public void Delete(int intProperty)
        {
            if (Get(intProperty) is X1Model x1ModelNotNull)
            {
                X1Models.Remove(x1ModelNotNull);
                SaveChanges();
            }
        }

    }
}
