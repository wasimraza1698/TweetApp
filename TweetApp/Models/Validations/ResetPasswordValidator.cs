using FluentValidation;
using System.Text.RegularExpressions;
using TweetApp.Models.Requests;

namespace TweetApp.Models.Validations
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
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

            RuleFor(x => x.ContactNumber)
                .NotNull()
                .NotEmpty()
                .Length(10);

            RuleFor(x => x.NewPassword)
                .NotNull()
                .NotEmpty()
                .Custom((x, context) =>
                {
                    if (!string.IsNullOrEmpty(x))
                    {
                        Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,14}$", RegexOptions.CultureInvariant | RegexOptions.Singleline);

                        if (!regex.IsMatch(x))
                        {
                            context.AddFailure($"Password should be greater than 8 characters and less than 14 characters and should have at least an uppercase letter, a lowercase letter, a special character and a number.");
                        }
                    }
                });
        }
    }
}
