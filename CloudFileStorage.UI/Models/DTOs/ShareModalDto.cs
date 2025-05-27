using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.UI.Models.DTOs
{
    public class ShareModalDto
    {
        public int FileMetadataId { get; set; }
        public ShareType ShareType { get; set; } = ShareType.Private;
        public Permission Permission { get; set; } = Permission.ReadOnly;
    }
}
