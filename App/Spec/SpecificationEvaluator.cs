using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Spec
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,
            ISpecification<T> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                //This checks if it is setup to order by descending
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.IsDistinct)
            {
                query = query.Distinct();
            }
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

        /// <summary>
        /// This is for the projection of the query
        /// </summary>
        /// <typeparam name="TSpec"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="inputQuery"></param>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> inputQuery,
            ISpecification<T, TResult> spec)
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending != null)
            {
                //This checks if it is setup to order by descending
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            var selectQuery = query as IQueryable<TResult>;

            if (spec.Select != null)
            {
                selectQuery = query.Select(spec.Select);
            }

            if (spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }

            if (spec.IsPagingEnabled)
            {
                selectQuery = selectQuery?.Skip(spec.Skip).Take(spec.Take);
            }

            return selectQuery ?? query.Cast<TResult>();
        }
    }
}
