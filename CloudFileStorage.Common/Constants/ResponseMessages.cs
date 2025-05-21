namespace CloudFileStorage.Common.Constants
{
    public static class ResponseMessages
    {
        public const string UserAlreadyExists = "Bu e-posta adresiyle kayıtlı bir kullanıcı zaten var.";
        public const string UserCreated = "Kullanıcı başarıyla kaydedildi.";
        public const string LoginFailed = "E-posta veya şifre hatalı.";
        public const string LoginSuccess = "Giriş başarılı.";
        public const string Unauthorized = "Bu işlemi yapmak için yetkiniz yok.";
        public const string RefreshTokenInvalid = "Geçersiz ya da süresi bitmiş yenileme tokeni.";
        public const string RefreshTokenSuccess = "Yenileme tokeni başarıyla yenilendi.";
        public const string RegisterSuccess = "Kayıt işlemi başarılı.";

        public const string FileCreated = "Dosya bilgisi başarıyla oluşturuldu.";
        public const string FileUpdated = "Dosya bilgisi başarıyla güncellendi.";
        public const string FileDeleted = "Dosya bilgisi başarıyla silindi.";
        public const string FileNotFound = "Dosya bulunamadı.";
    }
}
