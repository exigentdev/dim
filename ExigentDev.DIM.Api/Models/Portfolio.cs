using System.ComponentModel.DataAnnotations.Schema;

namespace ExigentDev.DIM.Api.Models
{
  [Table("Portfolio")]
  public class Portfolio
  {
    public string AppUserId { get; set; } = string.Empty;
    public int StockId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public Stock Stock { get; set; } = null!;
  }
}
