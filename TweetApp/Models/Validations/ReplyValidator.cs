using FluentValidation;
using TweetApp.Models.Requests;

namespace TweetApp.Models.Validations
{
    public class ReplyValidator : AbstractValidator<ReplyRequest>
    {
        public ReplyValidator()
        {
            RuleFor(x => x.ReplyText)
                .NotEmpty()
                .NotNull()
                .MaximumLength(144);

            RuleFor(x => x.ReplyTag)
                .MaximumLength(50);
        }
    }
}
