namespace ExigentDev.DIM.Api.Helpers
{
  public class QueryObject
  {
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
  }
}
