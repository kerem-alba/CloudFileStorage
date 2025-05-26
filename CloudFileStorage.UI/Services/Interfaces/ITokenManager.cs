namespace CloudFileStorage.UI.Services.Interfaces
{
    public interface ITokenManager
    {
        Task<string> GetValidAccessTokenAsync();
        void SaveTokens(string accessToken, string refreshToken);
        void ClearTokens();
    }
}
