using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;

namespace MvcProject.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetDeletedStores()
        {
            var stores = await _adminService.GetDeletedStoresAsync();
            return Json(stores);
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
                var success = await _adminService.DeleteStoreAsync(id);
                return Json(new { success });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RestoreStore(int id)
        {
            try
            {
                var success = await _adminService.RestoreStoreAsync(id);
                var store = await _adminService.GetStoreByIdAsync(id);
                return Json(new { success, storeName = store.Name, storeAddress = store.Address });
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
            return Json(branches);
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
                var success = await _adminService.DeleteBranchAsync(id);
                return Json(new { success });
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
            return Json(rooms);
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
                var success = await _adminService.DeleteRoomAsync(id);
                return Json(new { success });
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
            return Json(guests);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuest(int roomId, int storeId, string username, string password)
        {
            try
            {
                var guest = await _adminService.CreateGuestAsync(roomId, storeId, username, password);
                return Json(new { success = true, guestId = guest.Id, username = guest.Username });
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
                var success = await _adminService.DeleteGuestAsync(id);
                return Json(new { success });
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
            return Json(owners);
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
                var success = await _adminService.DeleteMainOwnerAsync(id);
                return Json(new { success });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}