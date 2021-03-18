using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.DAL.Models
{
    public class PagesList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int ElementsCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagesList(List<T> items, int count, int pageNumber, int pageSize)
        {
            ElementsCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static async Task<PagesList<T>> GetPagesListAsync(IQueryable<T> source, QueryParameters param)
        {
            var count = source.Count();
            var items = await source.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync();

            return new PagesList<T>(items, count, param.PageNumber, param.PageSize);
        }
    }
}