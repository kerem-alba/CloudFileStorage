using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.UI.Models.DTOs
{
    public class FileMetadataDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime UploadDate { get; set; }
        public int OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public string FileName { get; set; } = null!;
        public bool IsPublic { get; set; }
        public Permission Permission { get; set; } = Permission.ReadOnly;
        public ShareType ShareType { get; set; } = ShareType.Private;
        public bool IsOwner { get; set; }

    }
}
