using System;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Ordering
{
    public class Ordering<T>
    {
        private readonly Func<IQueryable<T>, IOrderedQueryable<T>> transform;

        private Ordering(Func<IQueryable<T>, IOrderedQueryable<T>> transform)
        {
            this.transform = transform;
        }

        public static Ordering<T> Create<TKey>
            (Expression<Func<T, TKey>> primary)
        {
            return new Ordering<T>(query => query.OrderBy(primary));
        }

        public static Ordering<T> CreateDesc<TKey>
            (Expression<Func<T, TKey>> primary)
        {
            return new Ordering<T>(query => query.OrderByDescending(primary));
        }

        public Ordering<T> ThenBy<TKey>(Expression<Func<T, TKey>> secondary)
        {
            return new Ordering<T>(query => transform(query).ThenBy(secondary));
        }

        public Ordering<T> ThenByDesc<TKey>(Expression<Func<T, TKey>> secondary)
        {
            return new Ordering<T>(query => transform(query).ThenByDescending(secondary));
        }

        public IOrderedQueryable<T> Apply(IQueryable<T> query)
        {
            return transform(query);
        }
    }
}
