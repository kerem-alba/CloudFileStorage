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
            int userId = User.GetUserId();
            var response = await _mediator.Send(new GetAllFilesQuery(userId));
            return this.HandleResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            int userId = User.GetUserId();
            var response = await _mediator.Send(new GetFileByIdQuery(id, userId));
            return this.HandleResponse(response);
        }

        [HttpGet("{id}/accessible")]
        public async Task<IActionResult> GetAccessibleById(int id)
        {
            int userId = User.GetUserId();
            var response = await _mediator.Send(new GetAccessibleFileQuery(id, userId));
            return this.HandleResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile([FromBody] CreateFileDto dto)
        {
            int userId = User.GetUserId();
            var response = await _mediator.Send(new CreateFileCommand(dto, userId));
            if (response.Data == null)
            {
                return this.HandleResponse(response);
            }
            return this.HandleResponse(response, nameof(GetById), new { id = response.Data.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFileDto dto)
        {
            int userId = User.GetUserId();
            var response = await _mediator.Send(new UpdateFileCommand(id, userId, dto));
            return this.HandleResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int userId = User.GetUserId();
            var response = await _mediator.Send(new DeleteFileCommand(id, userId));
            return this.HandleResponse(response);
        }
    }
}
