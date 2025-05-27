using SMediator.Core.Abstractions;
using AutoMapper;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Queries;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Handlers
{
    public class GetFileByIdQueryHandler : IRequestHandler<GetFileByIdQuery, ServiceResponse<FileMetadataDto?>>
    {
        private readonly IFileMetadataService _fileService;
        private readonly IMapper _mapper;

        public GetFileByIdQueryHandler(IFileMetadataService fileService, IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FileMetadataDto?>> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _fileService.GetFileByIdAsync(request.Id);
            var fileDto = response.Data == null ? null : _mapper.Map<FileMetadataDto>(response.Data);

            return new ServiceResponse<FileMetadataDto?>
            {
                Data = fileDto,
                Success = response.Success,
                Message = response.Message,
                StatusCode = response.StatusCode
            };
        }
    }
}
