using System;

namespace DAL.Models
{
  public class ErrorResponse
  {
    public string Type { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }

    public ErrorResponse(Exception ex)
    {
      Type = ex.GetType().Name;
      Message = ex.Message;
      StackTrace = ex.ToString();
    }
  }
}