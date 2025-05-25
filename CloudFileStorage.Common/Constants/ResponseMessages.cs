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

        public const string UserNotFound = "Kullanıcı bulunamadı.";
        public const string UserFetched = "Kullanıcı bilgileri başarıyla getirildi.";


        public const string FileCreated = "Dosya bilgisi başarıyla oluşturuldu.";
        public const string FileUpdated = "Dosya bilgisi başarıyla güncellendi.";
        public const string FileDeleted = "Dosya bilgisi başarıyla silindi.";
        public const string FileNotFound = "Dosya bulunamadı.";

        public const string FileShareFetched = "Paylaşılan dosyalar başarıyla getirildi.";
        public const string NoFilesSharedWithYou = "Size paylaşılan dosya bulunmamaktadır.";
        public const string FilesFetched = "Dosyalar başarıyla getirildi.";
        public const string NoFilesFound = "Herhangi bir dosya bulunamadı.";
        public const string FileShareCreated = "Dosya paylaşımı başarıyla oluşturuldu.";
        public const string FileShareDeleted = "Dosya paylaşımı başarıyla kaldırıldı.";
        public const string FileShareUpdated = "Dosya paylaşımı başarıyla güncellendi.";
        public const string FileShareNotFound = "Dosya paylaşımı bulunamadı.";
        public const string FileShareFailed = "Dosya paylaşımı başarısız oldu: {0}";
        public const string InternalServerError = "Beklenmeyen bir hata oluştu.";

        public const string FileNotSelected = "Dosya seçilmedi.";
        public const string FileUploadSuccess = "Dosya başarıyla yüklendi.";
        public const string FileUploadFail = "Dosya yükleme işlemi sırasında bir hata oluştu.";

        public const string FileNameRequired = "Dosya adı zorunludur.";
        public const string FileDownloadSuccess = "Dosya başarıyla indirildi.";
        public const string FileDownloadFail = "Dosya indirilirken bir hata oluştu: {0}";

    }
}
