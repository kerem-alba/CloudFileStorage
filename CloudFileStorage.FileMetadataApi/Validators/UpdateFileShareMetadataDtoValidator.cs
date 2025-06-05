using FluentValidation;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Constants;

namespace CloudFileStorage.FileMetadataApi.Validators
{
    public class UpdateFileShareMetadataDtoValidator : AbstractValidator<UpdateFileShareMetadataDto>
    {
        public UpdateFileShareMetadataDtoValidator()
        {
            RuleFor(x => x.Permission)
                .NotEmpty().WithMessage(ValidationMessages.PermissionRequired)
                .NotNull().WithMessage(ValidationMessages.PermissionMaxLength);
        }
    }
}
