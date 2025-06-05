using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.UI.Models.DTOs
{
    public class FileShareSelectionDto
    {
        public List<UserShareSelection> SelectedUsers { get; set; } = new List<UserShareSelection>();
    }

    public class UserShareSelection
    {
        public int UserId { get; set; }
        public Permission Permission { get; set; }
    }
}