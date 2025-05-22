using CloudFileStorage.Common.Constants;
using CloudFileStorage.AuthApi.Models.DTOs;
using FluentValidation;

namespace CloudFileStorage.AuthApi.Validators
{
    public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenDtoValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage(ValidationMessages.RefreshTokenRequired);
        }
    }
}
