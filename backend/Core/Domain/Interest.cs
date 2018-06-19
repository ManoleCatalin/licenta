using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class Interest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ThumbnailImgUrl { get; set; }

        virtual public ICollection<PostInterest> PostInterests { get; set; }
    }
}
