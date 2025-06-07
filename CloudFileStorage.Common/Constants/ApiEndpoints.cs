namespace CloudFileStorage.Common.Constants
{
    public static class ApiEndpoints
    {
        public const string GatewayBase = "https://localhost:5000/api";
        public static class Auth
        {
            public const string Base = $"{GatewayBase}/Auth";
            public const string Login = $"{Base}/login";
            public const string Register = $"{Base}/register";
            public const string Refresh = $"{Base}/refresh";

            public const string UsersBase = $"{GatewayBase}/User";
            public const string GetUserNameById = $"{UsersBase}/{{id}}";
            public const string GetAllUsers = $"{UsersBase}";
            public const string GetUserList = $"{UsersBase}/list";
            public const string GetUserNamesByIds = UsersBase + "/names";
        }

        public static class FileMetadata
        {
            public const string Base = $"{GatewayBase}/Files";
            public const string GetAll = $"{Base}";
            public const string GetById = $"{Base}/{{id}}";
            public const string GetAccessibleById = $"{Base}/{{id}}/accessible";
            public const string Create = $"{Base}";
            public const string Update = $"{Base}/{{id}}";
            public const string Delete = $"{Base}/{{id}}";
        }

        public static class FileShares
        {
            public const string Base = $"{GatewayBase}/FileShares";
            public const string SharedWithMe = $"{Base}/shared-with-me";
            public const string ShareFile = $"{Base}";
            public const string Update = $"{Base}/{{id}}";
            public const string Delete = $"{Base}/{{id}}";
            public const string CheckAccess = $"{Base}/check-access";
        }

        public static class FileStorage
        {
            public const string Base = $"{GatewayBase}/FileStorage";
            public const string Upload = $"{Base}/upload";
            public const string Download = $"{Base}/download";
        }
    }
}
