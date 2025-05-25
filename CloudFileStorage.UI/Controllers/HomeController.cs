using CloudFileStorage.UI.Constants;
using CloudFileStorage.UI.Models.DTOs;
using CloudFileStorage.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CloudFileStorage.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IFileShareService _fileShareService;
        private readonly IUserService _userService;


        public HomeController(IFileService fileService, IFileShareService fileShareService, IUserService userService)
        {
            _fileService = fileService;
            _fileShareService = fileShareService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.GetAllAsync(token);

            if (result == null || !result.Success || result.Data == null)
            {
                ViewBag.Error = result?.Message ?? UiMessages.GetFilesFailed;
                return View(new List<FileMetadataDto>());
            }

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> SharedWithMe()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileShareService.GetSharedWithMeAsync(token);

            if (result == null || !result.Success || result.Data == null)
            {
                ViewBag.Error = result?.Message ?? UiMessages.GetSharedWithMeFilesFailed;
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
                TempData["Error"] = result?.Message ?? UiMessages.FileShareFailed;
            }
            else
            {
                TempData["Success"] = UiMessages.FileShareSuccess;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.GetByIdAsync(id, token);
            if (result == null || !result.Success || result.Data == null)
            {
                ViewBag.Error = result?.Message ?? UiMessages.GetFileDetailsFailed;
                return RedirectToAction("Index");
            }
            var file = result.Data;

            var userResult = await _userService.GetUserNameByIdAsync(file.OwnerId, token);
            file.OwnerName = userResult?.Data ?? UiMessages.NoName;

            if (result == null || !result.Success || result.Data == null)
            {
                ViewBag.Error = result?.Message ?? UiMessages.GetFileDetailsFailed;
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

            var uploadResult = await _fileService.UploadAsync(dto.File, token);

            if (uploadResult == null || !uploadResult.Success || string.IsNullOrEmpty(uploadResult.Data))
            {
                ViewBag.Error = uploadResult?.Message ?? UiMessages.FileUploadFailed;
                return View(dto);
            }

            dto.FileName = uploadResult.Data;

            var result = await _fileService.CreateAsync(dto, token);

            if (result == null || !result.Success)
            {
                ViewBag.Error = result?.Message ?? UiMessages.FileCreateFailed;
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
                ViewBag.Error = result?.Message ?? UiMessages.FileUpdateFailed;
                ViewBag.FileId = id;
                return View(dto);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.DeleteAsync(id, token);

            if (result == null || !result.Success)
            {
                TempData["Error"] = result?.Message ?? UiMessages.FileDeleteFailed;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string fileName)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.DownloadAsync(fileName, token);

            if (result == null || !result.Success || result.Data == null)
            {
                TempData["Error"] = result?.Message ?? "Dosya indirilemedi.";
                return RedirectToAction("Index");
            }

            return File(result.Data, "application/octet-stream", fileName);
        }

    }
}
