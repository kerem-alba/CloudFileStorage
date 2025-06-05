using CloudFileStorage.Common.Enums;
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

            var result = await _fileService.GetAllAsync();

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

            var result = await _fileShareService.GetSharedWithMeAsync();

            if (result == null || !result.Success || result.Data == null)
            {
                ViewBag.Error = result?.Message ?? UiMessages.GetSharedWithMeFilesFailed;
                return View(new List<FileMetadataDto>());
            }

            return View(result.Data);
        }

        //[HttpPost]
        //public async Task<IActionResult> Share(CreateFileShareMetadataDto dto)
        //{
        //    var token = HttpContext.Session.GetString("token");
        //    if (string.IsNullOrEmpty(token))
        //        return RedirectToAction("Login", "Auth");

        //    var result = await _fileShareService.ShareFileAsync(dto);

        //    if (result == null || !result.Success)
        //    {
        //        TempData["Error"] = result?.Message ?? UiMessages.FileShareFailed;
        //    }
        //    else
        //    {
        //        TempData["Success"] = UiMessages.FileShareSuccess;
        //    }

        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.GetAccessibleByIdAsync(id);
            if (result == null || !result.Success || result.Data == null)
            {
                TempData["Message"] = result?.Message ?? UiMessages.GetFileDetailsFailed;
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }
            var file = result.Data;

            var userResult = await _userService.GetUserNameByIdAsync(file.OwnerId);
            file.OwnerName = userResult?.Data ?? UiMessages.NoName;

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var dto = new CreateFileDto
            {
                UserList = (await _userService.GetAllUsersAsync()).Data ?? new List<UserDto>()
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFileDto dto)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var uploadResult = await _fileService.UploadAsync(dto.File);

            if (uploadResult == null || !uploadResult.Success || string.IsNullOrEmpty(uploadResult.Data))
            {
                dto.UserList = (await _userService.GetAllUsersAsync()).Data ?? [];
                ViewBag.Error = uploadResult?.Message ?? UiMessages.FileUploadFailed;
                return View(dto);
            }

            dto.FileName = uploadResult.Data;
            if (dto.ShareType == ShareType.Private)
                dto.Permission = Permission.Edit;

            var result = await _fileService.CreateAsync(dto);

            if (result == null || !result.Success)
            {
                dto.UserList = (await _userService.GetAllUsersAsync()).Data ?? [];
                ViewBag.Error = result?.Message ?? UiMessages.FileCreateFailed;
                return View(dto);
            }

            if (result.Data != null && result.Success && dto.ShareType == ShareType.Specific && dto.SelectedUsers.Count > 0)
            {
                var fileMetadataId = result.Data.Id;
                var shareResult = await _fileShareService.ShareFileWithMultipleUsersAsync(fileMetadataId, dto.SelectedUsers);

                if (shareResult == null || !shareResult.Success)
                {
                    TempData["Warning"] = shareResult?.Message ?? UiMessages.FileShareFailed;
                }
            }

            TempData["Success"] = UiMessages.FileCreateSuccess;
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.GetByIdAsync(id);

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

            var result = await _fileService.UpdateAsync(id, dto);

            if (result == null || !result.Success)
            {
                ViewBag.Error = result?.Message ?? UiMessages.FileUpdateFailed;
                ViewBag.FileId = id;
                return View(dto);
            }

            return RedirectToAction("Detail", new { id = dto.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var token = HttpContext.Session.GetString("token");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var result = await _fileService.DeleteAsync(id);

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

            var result = await _fileService.DownloadAsync(fileName);

            if (result == null || !result.Success || result.Data == null)
            {
                TempData["Error"] = result?.Message ?? "Dosya indirilemedi.";
                return RedirectToAction("Index");
            }

            return File(result.Data, "application/octet-stream", fileName);
        }

    }
}
