using CloudFileStorage.Common.Extensions;
using CloudFileStorage.FileMetadataApi.Models.DTOs;
using CloudFileStorage.FileMetadataApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudFileStorage.FileMetadataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            int ownerId = User.GetUserId();
            var response = await _fileService.GetAllFilesAsync(ownerId);
            return this.HandleResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            int ownerId = User.GetUserId();
            var response = await _fileService.GetFileByIdAsync(id, ownerId);
            return this.HandleResponse(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile([FromBody] CreateFileDto dto)
        {
            int ownerId = User.GetUserId();
            var response = await _fileService.CreateFileAsync(dto, ownerId);
            return this.HandleResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateFileDto dto)
        {
            int ownerId = User.GetUserId();
            var response = await _fileService.UpdateFileAsync(id, ownerId, dto);
            return this.HandleResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            int ownerId = User.GetUserId();
            var response = await _fileService.DeleteFileAsync(id, ownerId);
            return this.HandleResponse(response);
        }
    }
}
