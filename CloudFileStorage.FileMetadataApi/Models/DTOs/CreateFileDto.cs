using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.FileMetadataApi.Models.DTOs
{
    public class CreateFileDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string FileName { get; set; } = null!;
        public ShareType ShareType { get; set; }
        public Permission? Permission { get; set; }
    }
}
