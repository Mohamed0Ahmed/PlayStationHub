using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Application.Interfaces;
using System.Domain.Entities;

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

        public IActionResult ManageStore(int storeId)
        {
            ViewBag.StoreId = storeId;
            return View();
        }

        public async Task<IActionResult> LinkOwnerToStore()
        {
            ViewBag.Stores = await _adminService.GetAllStoresAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LinkOwnerToStore(string email, int storeId)
        {
            try
            {
                await _adminService.LinkOwnerToStoreAsync(email, storeId);
                TempData["Success"] = "Owner linked to store successfully.";
                return RedirectToAction("LinkOwnerToStore");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                ViewBag.Stores = await _adminService.GetAllStoresAsync();
                return View();
            }
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
                await _adminService.UpdateStoreAsync(id, storeName, address);
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
                await _adminService.DeleteStoreAsync(id);
                return Json(new { success = true });
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
                await _adminService.RestoreStoreAsync(id);
                var store = await _adminService.GetStoreByIdAsync(id);
                return Json(new { success = true, storeName = store.Name, storeAddress = store.Address });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedStores()
        {
            var stores = await _adminService.GetDeletedStoresAsync();
            return Json(stores);
        }

        [HttpGet]
        public async Task<IActionResult> GetBranches(int storeId)
        {
            var branches = await _adminService.GetBranchesByStoreIdAsync(storeId);
            return Json(branches.Select(b => new { id = b.Id, branchName = b.BranchName }));
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedBranches(int storeId)
        {
            var branches = await _adminService.GetDeletedBranchesAsync(storeId);
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
                await _adminService.UpdateBranchAsync(id, branchName);
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
                await _adminService.DeleteBranchAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RestoreBranch(int id)
        {
            try
            {
                await _adminService.RestoreBranchAsync(id);
                return Json(new { success = true });
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
            var roomDtos = new List<object>();
            foreach (var room in rooms)
            {
                var hasGuest = await _adminService.HasGuestAsync(room.Id);
                roomDtos.Add(new
                {
                    id = room.Id,
                    roomName = room.RoomName,
                    storeId = room.Branch.StoreId,
                    hasGuest
                });
            }
            return Json(roomDtos);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedRooms(int branchId)
        {
            var rooms = await _adminService.GetDeletedRoomsAsync(branchId);
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
                await _adminService.UpdateRoomAsync(id, roomName);
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
                await _adminService.DeleteRoomAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RestoreRoom(int id)
        {
            try
            {
                await _adminService.RestoreRoomAsync(id);
                return Json(new { success = true });
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
            return Json(guests.Select(g => new { id = g.Id, username = g.Username }));
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedGuests(int branchId)
        {
            var guests = await _adminService.GetDeletedGuestsAsync(branchId);
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
                await _adminService.DeleteGuestAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RestoreGuest(string id)
        {
            try
            {
                await _adminService.RestoreGuestAsync(id);
                return Json(new { success = true });
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

        [HttpGet]
        public async Task<IActionResult> GetDeletedOwners()
        {
            var owners = await _adminService.GetDeletedOwnersAsync();
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
                await _adminService.UpdateMainOwnerAsync(id, email);
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
                await _adminService.DeleteMainOwnerAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RestoreOwner(string id)
        {
            try
            {
                await _adminService.RestoreOwnerAsync(id);
                var owner = await _adminService.GetAllMainOwnersAsync();
                var restoredOwner = owner.FirstOrDefault(o => o.Id == id);
                return Json(new { success = true, email = restoredOwner?.Email });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}