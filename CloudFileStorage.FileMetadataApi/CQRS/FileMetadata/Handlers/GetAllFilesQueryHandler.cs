using SMediator.Core.Abstractions;
using AutoMapper;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Services.Interfaces;
using CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Queries;
using CloudFileStorage.Common.Models;

namespace CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Handlers
{
    public class GetAllFilesQueryHandler : IRequestHandler<GetAllFilesQuery, ServiceResponse<List<FileMetadataDto>>>
    {
        private readonly IFileMetadataService _fileService;
        private readonly IMapper _mapper;

        public GetAllFilesQueryHandler(IFileMetadataService fileService, IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<FileMetadataDto>>> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
        {
            var response = await _fileService.GetAllFilesAsync(request.OwnerId);
            var fileDtoList = _mapper.Map<List<FileMetadataDto>>(response.Data);

            return new ServiceResponse<List<FileMetadataDto>>
            {
                Data = fileDtoList,
                Success = response.Success,
                Message = response.Message,
                StatusCode = response.StatusCode
            };
        }
    }
}
