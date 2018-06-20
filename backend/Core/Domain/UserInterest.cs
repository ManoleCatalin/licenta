using System;

namespace Core.Domain
{
    public class UserInterest
    {
        public Guid UserId { get; set; }
        virtual public User User { get; set; }

        public Guid InterestId { get; set; }
        virtual public Interest Interest { get; set; }
    }
}
