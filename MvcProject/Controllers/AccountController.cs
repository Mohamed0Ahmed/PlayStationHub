using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;

namespace MvcProject.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAdminService _adminService;

        public AccountController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email = null, string password = null, string username = null, string storeName = null)
        {
            try
            {
                // Guest login via iPad
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(storeName))
                {
                    var sessionId = await _adminService.GuestLoginAsync(username, password, storeName);
                    HttpContext.Session.SetString("GuestSessionId", sessionId);
                    HttpContext.Session.SetString("GuestUsername", username);
                    HttpContext.Session.SetString("StoreName", storeName);
                    return Json(new { success = true, redirectUrl = Url.Action("CustomerLogin") });
                }

                // Admin/Owner/Staff login
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return Json(new { success = false, message = "Email and password are required." });
                }

                var (isSuccess, redirectUrl) = await _adminService.UserLoginAsync(email, password);
                if (isSuccess)
                {
                    return Json(new { success = true, redirectUrl });
                }

                return Json(new { success = false, message = "Invalid email or password." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult CustomerLogin()
        {
            var sessionId = HttpContext.Session.GetString("GuestSessionId");
            if (string.IsNullOrEmpty(sessionId))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomerLogin(string phoneNumber, int branchId)
        {
            try
            {
                var customer = await _adminService.CreateCustomerAsync(phoneNumber, branchId);
                return Json(new { success = true, message = "Customer registered successfully.", customerId = customer.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _adminService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}