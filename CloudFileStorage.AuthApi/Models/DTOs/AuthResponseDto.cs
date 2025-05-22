namespace CloudFileStorage.AuthApi.Models.DTOs
{
    public class AuthResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

    }
}
