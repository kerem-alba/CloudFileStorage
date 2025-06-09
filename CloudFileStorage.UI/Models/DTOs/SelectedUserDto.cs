using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.UI.Models.DTOs
{
    public class SelectedUserDto
    {
        public int UserId { get; set; }
        public Permission Permission { get; set; } = Permission.ReadOnly;
    }
}
