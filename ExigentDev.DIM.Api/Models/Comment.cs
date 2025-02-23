namespace ExigentDev.DIM.Api.Models
{
  public class Comment
  {
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public int? StockId { get; set; }
    public Stock? Stock { get; set; }
  }
}
