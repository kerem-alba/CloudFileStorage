using CloudFileStorage.Common.Enums;

namespace CloudFileStorage.UI.Models.DTOs
{
    public class UserShareSelection
    {
        public int UserId { get; set; }
        public Permission Permission { get; set; }
    }
}