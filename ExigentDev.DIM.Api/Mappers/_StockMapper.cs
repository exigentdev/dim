// using ExigentDev.DIM.Api.Dtos.Stock;
// using ExigentDev.DIM.Api.Models;

// namespace ExigentDev.DIM.Api.Mappers
// {
//   public static class StockMapper
//   {
//     public static StockDto ToStockDto(this Stock stockModel)
//     {
//       return new StockDto
//       {
//         Id = stockModel.Id,
//         Symbol = stockModel.Symbol,
//         CompanyName = stockModel.CompanyName,
//         Purchase = stockModel.Purchase,
//         LastDiv = stockModel.LastDiv,
//         Industry = stockModel.Industry,
//         MarketCap = stockModel.MarketCap,
//         Comments = [.. stockModel.Comments.Select(c => c.ToCommentDto())],
//       };
//     }

//     public static Stock ToStockFromCreateDto(this CreateStockDto stockDto)
//     {
//       return new Stock
//       {
//         Symbol = stockDto.Symbol,
//         CompanyName = stockDto.CompanyName,
//         Purchase = stockDto.Purchase,
//         LastDiv = stockDto.LastDiv,
//         Industry = stockDto.Industry,
//         MarketCap = stockDto.MarketCap,
//       };
//     }
//   }
// }
