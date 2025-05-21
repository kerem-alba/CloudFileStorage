namespace CloudFileStorage.Common.Constants
{
    public static class ValidationMessages
    {
        public const string NameRequired = "İsim boş olamaz.";
        public const string NameTooShort = "İsim en az 2 karakter olmalıdır.";
        public const string EmailRequired = "E-posta adresi boş olamaz.";
        public const string EmailInvalid = "Geçerli bir e-posta adresi giriniz.";
        public const string PasswordRequired = "Şifre boş olamaz.";
        public const string PasswordTooShort = "Şifre en az 6 karakter olmalıdır.";

        public const string FileNameRequired = "Dosya adı boş olamaz.";
        public const string FileNameMaxLength = "Dosya adı en fazla 255 karakter olabilir.";
        public const string DescriptionMaxLength = "Açıklama en fazla 1000 karakter olabilir.";
    }
}
