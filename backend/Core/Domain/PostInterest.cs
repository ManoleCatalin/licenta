using System;

namespace Core.Domain
{
    public class PostInterest
    {
        public Guid PostId { get; set; }
        virtual public Post Post { get; set; }

        public Guid InterestId { get; set; }
        virtual public Interest Interest { get; set; }
    }
}
