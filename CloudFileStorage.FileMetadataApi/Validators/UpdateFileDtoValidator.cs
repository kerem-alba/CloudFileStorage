using FluentValidation;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Constants;

namespace CloudFileStorage.FileMetadataApi.Validators
{
    public class UpdateFileDtoValidator : AbstractValidator<UpdateFileDto>
    {
        public UpdateFileDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.NameRequired)
                .MaximumLength(255).WithMessage(ValidationMessages.FileNameMaxLength);

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage(ValidationMessages.DescriptionMaxLength);
        }
    }
}
