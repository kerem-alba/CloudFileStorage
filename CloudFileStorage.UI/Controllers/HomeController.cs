using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IFileShareService _fileShareService;

        public HomeController(IFileService fileService, IFileShareService fileShareService)
        {
            _fileService = fileService;
            _fileShareService = fileShareService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.GetAllAsync(token);

            if (result == null || !result.Success || result.Data == null)
            {
                ViewBag.Error = result?.Message ?? "Dosyalar alýnamadý.";
                return View(new List<FileMetadataDto>());
            }

            return View(result.Data);
        }

        public async Task<IActionResult> SharedWithMe()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileShareService.GetSharedWithMeAsync(token);

            if (result == null || !result.Success || result.Data == null)
            {
                ViewBag.Error = result?.Message ?? "Paylaþýlan dosyalar alýnamadý.";
                return View(new List<FileMetadataDto>());
            }

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Share(CreateFileShareMetadataDto dto)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileShareService.ShareFileAsync(dto, token);

            if (result == null || !result.Success)
            {
                TempData["Error"] = result?.Message ?? "Dosya paylaþýmý baþarýsýz oldu.";
            }
            else
            {
                TempData["Success"] = "Dosya baþarýyla paylaþýldý.";
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Detail(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.GetByIdAsync(id, token);

            if (result == null || !result.Success || result.Data == null)
            {
                ViewBag.Error = result?.Message ?? "Dosya getirilemedi.";
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFileDto dto)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.CreateAsync(dto, token);

            if (result == null || !result.Success)
            {
                ViewBag.Error = result?.Message ?? "Dosya oluþturulamadý.";
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.GetByIdAsync(id, token);

            if (result == null || !result.Success || result.Data == null)
                return RedirectToAction("Index");

            var dto = new UpdateFileDto
            {
                Name = result.Data.Name,
                Description = result.Data.Description
            };

            ViewBag.FileId = id;
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateFileDto dto)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.UpdateAsync(id, dto, token);

            if (result == null || !result.Success)
            {
                ViewBag.Error = result?.Message ?? "Dosya güncellenemedi.";
                ViewBag.FileId = id;
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.DeleteAsync(id, token);

            if (result == null || !result.Success)
            {
                TempData["Error"] = result?.Message ?? "Dosya silinemedi.";
            }

            return RedirectToAction("Index");
        }
    }
}
