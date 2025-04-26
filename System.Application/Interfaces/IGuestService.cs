using System.Domain.Entities;

namespace System.Application.Interfaces
{
    public interface IGuestService
    {
        Task<string> GenerateSessionTokenAsync(string guestId);
        Task<Guest> GetGuestByIdAsync(string guestId);
        Task<Customer> CustomerLoginAsync(string guestId, string phoneNumber, int branchId, bool isFromQrCode = false);
        Task CloseQrCodeSessionAsync(string guestId, string customerId);
    }
}