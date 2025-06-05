using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.UI.Models.DTOs
{
    public class CreateFileDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public IFormFile File { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public ShareType ShareType { get; set; } = ShareType.Private;
        public Permission? Permission { get; set; }
        public List<UserShareSelection> SelectedUsers { get; set; } = [];
        public List<UserDto> UserList { get; set; } = [];
    }
}
