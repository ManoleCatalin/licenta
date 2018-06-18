using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class PostInterest
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public Guid InterestId { get; set; }
        public Interest Interest { get; set; }
    }
}
