using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace HMI.API.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Extension that encapsulate Paging logic 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pagesSize">Page Size, use -1 for all data</param>
        /// <returns></returns>
        public static IQueryable<T> GetPage<T>(this IQueryable<T> source, int pageNumber, int pagesSize)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Invalid Page Number value");
            return pagesSize == -1 ? source : source.Skip((pageNumber - 1) * pagesSize).Take(pagesSize);
        }

        /// <summary>
        /// Extension that encapsulate Paging logic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pagesSize">Page Size, use -1 for all data</param>
        /// <returns></returns>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> source, int pageNumber, int pagesSize)
        {
            if (pageNumber < 1)
                throw new ArgumentException("Invalid Page Number value");
            return pagesSize == -1 ? source : source.Skip((pageNumber - 1) * pagesSize).Take(pagesSize);
        }


        public static IQueryable<T> OrderBy<T>(this IQueryable<T> entities, string propertyName, string direction)
        {
            if (!entities.Any() || string.IsNullOrEmpty(propertyName))
                return entities;

            var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
                return entities;
            //if (!string.IsNullOrWhiteSpace(direction) && direction.Equals("DESC", StringComparison.OrdinalIgnoreCase))
            //    return entities.OrderByDescending(e => propertyInfo.GetValue(e, null));
            //return entities.OrderBy(e => propertyInfo.GetValue(e, null));

            if (!string.IsNullOrWhiteSpace(direction) && direction.Equals("DESC", StringComparison.OrdinalIgnoreCase))
                return entities.OrderByDescending(e => EF.Property<object>(e, propertyInfo.Name));
            return entities.OrderBy(e => EF.Property<object>(e, propertyInfo.Name));

        }
    }
}
