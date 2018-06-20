using System;

namespace WebApi.Models.Favorite
{
    public class CreateFavoriteModel
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
