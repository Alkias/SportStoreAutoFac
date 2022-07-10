using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportStoreAutoFac.UI.Paging
{
    //seeL https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-5.0
    //see: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-5.0
    /// <summary>
    /// To add paging to the Students Index page, you'll create a PaginatedList class that uses
    /// Skip and Take statements to filter data on the server instead of always retrieving all rows of the table.
    /// Then you'll make additional changes in the Index method and add paging buttons to the Index view.
    /// The following illustration shows the paging buttons.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedList<T> : List<T>
    {
        #region Ctor

        public PaginatedList(
            List<T> items,
            int count,
            int pageIndex,
            int pageSize
        ) {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);

            AddRange(items);
        }

        #endregion

        public int PageIndex { get; }
        public int TotalPages { get; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        #region Methods

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source,
            int pageIndex,
            int pageSize) {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        #endregion
    }
}