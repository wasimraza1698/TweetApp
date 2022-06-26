using FluentValidation;
using System.Text.RegularExpressions;
using TweetApp.Models.Requests;

namespace TweetApp.Models.Validations
{
    public class UserValidator : AbstractValidator<UserRequest>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ContactNumber)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.EmailId)
                .NotNull()
                .NotEmpty()
                .Custom((x, context) =>
                {
                    if (!string.IsNullOrEmpty(x))
                    {
                        Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

                        if (!regex.IsMatch(x))
                        {
                            context.AddFailure($"{x} is not a valid email");
                        }
                    }
                });
        }
    }
}
