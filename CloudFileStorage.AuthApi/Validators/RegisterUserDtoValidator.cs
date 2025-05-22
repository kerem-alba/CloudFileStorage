using CloudFileStorage.Common.Constants;
using CloudFileStorage.AuthApi.Models.DTOs;
using FluentValidation;

namespace CloudFileStorage.AuthApi.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NameRequired)
                .MinimumLength(2).WithMessage(ValidationMessages.NameTooShort);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationMessages.EmailRequired)
                .EmailAddress().WithMessage(ValidationMessages.EmailInvalid);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidationMessages.PasswordRequired)
                .MinimumLength(6).WithMessage(ValidationMessages.PasswordTooShort);
        }
    }
}
