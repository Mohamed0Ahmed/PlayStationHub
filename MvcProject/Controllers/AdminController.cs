using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;


namespace System.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var stores = await _adminService.GetAllStoresAsync();
            return View(stores);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore(string storeName, string address)
        {
            try
            {
                var store = await _adminService.CreateStoreAsync(storeName, address);
                return Json(new { success = true, storeId = store.Id, storeName = store.Name });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditStore(int id, string storeName, string address)
        {
            try
            {
                var store = await _adminService.UpdateStoreAsync(id, storeName, address);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStore(int id)
        {
            try
            {
                var result = await _adminService.DeleteStoreAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedStores()
        {
            var deletedStores = await _adminService.GetDeletedStoresAsync();
            return Json(deletedStores.Select(s => new { id = s.Id, name = s.Name, address = s.Address }));
        }

        [HttpPost]
        public async Task<IActionResult> RestoreStore(int id)
        {
            try
            {
                var result = await _adminService.RestoreStoreAsync(id);
                if (result)
                {
                    var store = await _adminService.GetStoreByIdAsync(id);
                    return Json(new { success = true, storeName = store.Name, storeAddress = store.Address });
                }
                return Json(new { success = false, message = "Store not found or already restored." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBranches(int storeId)
        {
            var branches = await _adminService.GetBranchesByStoreIdAsync(storeId);
            return Json(branches.Select(b => new { id = b.Id, branchName = b.BranchName }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(int storeId, string branchName)
        {
            try
            {
                var branch = await _adminService.CreateBranchAsync(storeId, branchName);
                return Json(new { success = true, branchId = branch.Id, branchName = branch.BranchName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditBranch(int id, string branchName)
        {
            try
            {
                var branch = await _adminService.UpdateBranchAsync(id, branchName);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            try
            {
                var result = await _adminService.DeleteBranchAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms(int branchId)
        {
            var rooms = await _adminService.GetRoomsByBranchIdAsync(branchId);
            return Json(rooms.Select(r => new { id = r.Id, roomName = r.RoomName }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(int branchId, string roomName)
        {
            try
            {
                var room = await _adminService.CreateRoomAsync(branchId, roomName);
                return Json(new { success = true, roomId = room.Id, roomName = room.RoomName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditRoom(int id, string roomName)
        {
            try
            {
                var room = await _adminService.UpdateRoomAsync(id, roomName);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                var result = await _adminService.DeleteRoomAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetGuests(int roomId)
        {
            var guests = await _adminService.GetGuestsByRoomIdAsync(roomId);
            return Json(guests.Select(g => new { id = g.Id, sessionToken = g.SessionToken }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuest(int roomId)
        {
            try
            {
                var guest = await _adminService.CreateGuestAsync(roomId);
                return Json(new { success = true, guestId = guest.Id, sessionToken = guest.SessionToken });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGuest(string id)
        {
            try
            {
                var result = await _adminService.DeleteGuestAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOwners()
        {
            var owners = await _adminService.GetAllMainOwnersAsync();
            return Json(owners.Select(o => new { id = o.Id, email = o.Email }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner(string email, string password, int storeId)
        {
            try
            {
                var owner = await _adminService.CreateMainOwnerAsync(email, password, storeId);
                return Json(new { success = true, ownerId = owner.Id, email = owner.Email });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditOwner(string id, string email)
        {
            try
            {
                var owner = await _adminService.UpdateMainOwnerAsync(id, email);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOwner(string id)
        {
            try
            {
                var result = await _adminService.DeleteMainOwnerAsync(id);
                return Json(new { success = result });
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