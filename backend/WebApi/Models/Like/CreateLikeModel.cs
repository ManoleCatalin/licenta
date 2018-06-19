using System;

namespace WebApi.Models.Like
{
    public class CreateLikeModel
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
