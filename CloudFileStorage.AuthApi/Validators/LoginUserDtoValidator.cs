using CloudFileStorage.Common.Constants;
using CloudFileStorage.AuthApi.Models.DTOs;
using FluentValidation;

namespace CloudFileStorage.AuthApi.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
                .EmailAddress().WithMessage(ValidationMessages.EmailInvalid);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.PasswordRequired);
        }
    }
}
