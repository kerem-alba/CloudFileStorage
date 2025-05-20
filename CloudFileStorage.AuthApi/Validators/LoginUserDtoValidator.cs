using CloudFileStorage.AuthApi.Constants;
using CloudFileStorage.AuthApi.DTOs;
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
