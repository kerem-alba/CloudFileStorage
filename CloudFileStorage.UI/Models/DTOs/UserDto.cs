namespace CloudFileStorage.UI.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public bool IsSelected { get; set; } = false;
        public string SelectedPermission { get; set; } = "ReadOnly";

    }

}
