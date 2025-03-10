// using ExigentDev.DIM.Api.Dtos.Comment;
// using ExigentDev.DIM.Api.Models;

// namespace ExigentDev.DIM.Api.Mappers
// {
//   public static class CommentMapper
//   {
//     public static CommentDto ToCommentDto(this Comment commentModel)
//     {
//       return new CommentDto
//       {
//         Id = commentModel.Id,
//         Title = commentModel.Title,
//         Content = commentModel.Content,
//         Created = commentModel.Created,
//         StockId = commentModel.StockId,
//       };
//     }

//     public static Comment ToCommentFromCreateDto(
//       this CreateCommentDto createCommentDto,
//       int stockId
//     )
//     {
//       return new Comment
//       {
//         Title = createCommentDto.Title,
//         Content = createCommentDto.Content,
//         StockId = stockId,
//       };
//     }
//   }
// }
