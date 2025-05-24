namespace CloudFileStorage.UI.Models
{
    public class TokenResponseDto
    {
        public TokenDataDto Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class TokenDataDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
