using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.FileMetadataApi.Models.DTOs
{
    public class UpdateFileDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPublic { get; set; }
        public Permission Permission { get; set; }
        public ShareType ShareType { get; set; }
        public List<FileShareDto>? SelectedUsers { get; set; }

    }
}
