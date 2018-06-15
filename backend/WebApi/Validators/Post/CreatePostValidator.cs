using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Validators.Post
{
    public class CreatePostValidator : AbstractValidator<CreatePostModel>
    {
        public CreatePostValidator()
        {
            RuleFor(x => x.Interests).NotEmpty();
        }
    }
}
