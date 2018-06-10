using Core.Interfaces;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Persistence
{
    public sealed class DbService : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        private const string _isDeletedProperty = "IsDeleted";
        private static readonly MethodInfo _propertyMethod = typeof(EF).GetMethod(nameof(EF.Property),
            BindingFlags.Static | BindingFlags.Public).MakeGenericMethod(typeof(bool));


        public DbService(DbContextOptions<DbService> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var parm = Expression.Parameter(type, "it");
            var prop = Expression.Call(_propertyMethod, parm, Expression.Constant(_isDeletedProperty));
            var condition = Expression.MakeBinary(ExpressionType.Equal, prop, Expression.Constant(false));
            var lambda = Expression.Lambda(condition, parm);
            return lambda;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType) == true)
                {
                    entity.AddProperty(_isDeletedProperty, typeof(bool));

                    modelBuilder
                        .Entity(entity.ClrType)
                        .HasQueryFilter(GetIsDeletedRestriction(entity.ClrType));
                }
            }

           // modelBuilder.ApplyConfiguration(new UserConfiguration());
           // modelBuilder.ApplyConfiguration(new PostConfiguration());
        }

        public override int SaveChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                if (entry.Entity is ISoftDeletable)
                {
                    entry.Property(_isDeletedProperty).CurrentValue = true;
                    entry.State = EntityState.Modified;
                }
            }

            return base.SaveChanges();
        }
    }
}
