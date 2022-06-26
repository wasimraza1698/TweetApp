using FluentValidation;
using TweetApp.Models.Requests;

namespace TweetApp.Models.Validations
{
    public class TweetValidator : AbstractValidator<TweetRequest>
    {
        public TweetValidator()
        {
            RuleFor(x => x.TweetText)
                .NotEmpty()
                .NotNull()
                .MaximumLength(144);

            RuleFor(x => x.TweetTag)
                .MaximumLength(50);
        }
    }
}
