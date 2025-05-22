using FluentValidation;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Constants;

namespace CloudFileStorage.FileMetadataApi.Validators
{
    public class CreateFileDtoValidator : AbstractValidator<CreateFileDto>
    {
        public CreateFileDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NameRequired)
                .MaximumLength(255).WithMessage(ValidationMessages.FileNameMaxLength);

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage(ValidationMessages.DescriptionMaxLength);
        }
    }
}
