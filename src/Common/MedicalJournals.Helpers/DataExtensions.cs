using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalJournals.Helpers
{
    public static class DataExtensions
    {

        private static readonly MethodInfo SetMethod = typeof(DbContext).GetTypeInfo().GetDeclaredMethod("Set");

        public static object Find(this DbContext context, Type entityType, params object[] keyValues)
        {
            dynamic set = SetMethod.MakeGenericMethod(entityType).Invoke(context, null);
            var entity = Find(set, keyValues);
            return entity;
        }

        public static TEntity Find<TEntity>(this DbSet<TEntity> set, params object[] keyValues) where TEntity : class
        {
            var context = set.GetInfrastructure<IServiceProvider>().GetService<IDbContextServices>().CurrentContext.Context;
            var entityType = context.Model.FindEntityType(typeof(TEntity));
            var keys = entityType.GetKeys();
            var entries = context.ChangeTracker.Entries<TEntity>();
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            IQueryable<TEntity> query = context.Set<TEntity>();

            //first, check if the entity exists in the cache
            var i = 0;

            //iterate through the key properties
            foreach (var property in keys.SelectMany(x => x.Properties))
            {
                var keyValue = keyValues[i];

                //try to get the entity from the local cache
                entries = entries.Where(e => keyValue.Equals(e.Property(property.Name).CurrentValue));

                //build a LINQ expression for loading the entity from the store
                var expression = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, property.Name),
                            Expression.Constant(keyValue)),
                        parameter) as Expression<Func<TEntity, bool>>;

                query = query.Where(expression);

                i++;
            }

            var entity = entries.Select(x => x.Entity).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }

            //second, try to load the entity from the data store
            entity = query.FirstOrDefault();

            return entity;
        }
    }
}
