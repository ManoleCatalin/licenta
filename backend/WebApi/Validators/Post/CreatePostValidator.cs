using FluentValidation;
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
