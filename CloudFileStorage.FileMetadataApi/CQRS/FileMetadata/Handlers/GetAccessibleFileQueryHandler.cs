using SMediator.Core.Abstractions;
using AutoMapper;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Queries;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Handlers
{
    public class GetAccessibleFileQueryHandler : IRequestHandler<GetAccessibleFileQuery, ServiceResponse<FileMetadataDto>>
    {
        private readonly IFileMetadataService _fileService;

        public GetAccessibleFileQueryHandler(IFileMetadataService fileMetadataService)
        {
            _fileService = fileMetadataService;
        }

        public async Task<ServiceResponse<FileMetadataDto>> Handle(GetAccessibleFileQuery request, CancellationToken cancellationToken)
        {
            return await _fileService.GetAccessibleByIdAsync(request.FileId, request.UserId);
        }
    }
}
