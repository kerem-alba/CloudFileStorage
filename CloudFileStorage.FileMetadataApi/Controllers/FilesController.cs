using CloudFileStorage.Common.Extensions;
using CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Commands;
using CloudFileStorage.FileMetadataApi.CQRS.FileMetadata.Queries;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMediator.Core.Abstractions;

namespace CloudFileStorage.FileMetadataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int ownerId = User.GetUserId();
            var response = await _mediator.Send(new GetAllFilesQuery(ownerId));
            return this.HandleResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            int ownerId = User.GetUserId();
            var response = await _mediator.Send(new GetFileByIdQuery(id, ownerId));
            return this.HandleResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile([FromBody] CreateFileDto dto)
        {
            int ownerId = User.GetUserId();
            var response = await _mediator.Send(new CreateFileCommand(dto, ownerId));
            return this.HandleResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFileDto dto)
        {
            int ownerId = User.GetUserId();
            var response = await _mediator.Send(new UpdateFileCommand(id, ownerId, dto));
            return this.HandleResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int ownerId = User.GetUserId();
            var response = await _mediator.Send(new DeleteFileCommand(id, ownerId));
            return this.HandleResponse(response);
        }
    }
}
