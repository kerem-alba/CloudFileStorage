﻿using FluentValidation;
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

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage(ValidationMessages.UserIdRequired);

            RuleFor(x => x.Permission)
                .NotEmpty().WithMessage(ValidationMessages.PermissionRequired);
        }
    }
}
