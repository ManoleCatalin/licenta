using System;
using Core.Domain;

namespace Data.Core.Auth
{
    public class TokenUserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public Guid RoleId { get; set; }

        public TokenUserModel(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
        }
    }
}
