using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.UI.Models.DTOs
{
    public class UpdateFileDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsPublic { get; set; }

        public ShareType ShareType { get; set; } = ShareType.Private;
        public Permission Permission { get; set; } = Permission.ReadOnly;
        public List<SelectedUserDto>? SelectedUsers { get; set; } = null;
    }
}
