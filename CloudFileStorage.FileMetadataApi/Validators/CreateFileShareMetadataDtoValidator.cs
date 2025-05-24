using FluentValidation;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.Common.Constants;

namespace CloudFileStorage.FileMetadataApi.Validators
{
    public class CreateFileShareMetadataDtoValidator : AbstractValidator<CreateFileShareMetadataDto>
    {
        public CreateFileShareMetadataDtoValidator()
        {
            RuleFor(x => x.FileMetadataId)
                .GreaterThan(0).WithMessage(ValidationMessages.FileIdRequired);

            RuleFor(x => x.UserIds)
                .NotNull().WithMessage(ValidationMessages.UserIdsRequired)
                .Must(list => list.Any()).WithMessage(ValidationMessages.UserIdsRequired);


            RuleFor(x => x.Permission)
                .NotEmpty().WithMessage(ValidationMessages.PermissionRequired)
                .MaximumLength(50).WithMessage(ValidationMessages.PermissionMaxLength);
        }
    }
}
