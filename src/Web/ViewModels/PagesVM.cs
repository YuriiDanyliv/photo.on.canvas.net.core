namespace POC.Web.ViewModel
{
  public class PagesVM<T>
  {
    public int ElementsCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public T[] data { get; set; }
  }
}