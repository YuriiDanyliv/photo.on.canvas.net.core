using System;
using System.Collections.Generic;
using System.Linq;

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

    public static PagesList<T> GetPagesList(IQueryable<T> source, int pageNumber, int pageSize)
    {
      var count = source.Count();
      var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

      return new PagesList<T>(items, count, pageNumber, pageSize);
    }
  }
}