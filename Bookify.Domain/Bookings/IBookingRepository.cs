using Bookify.Domain.Apartments;
using Bookify.Domain.Users;

namespace Bookify.Domain.Bookings;

public interface IBookingRepository
{
    Task<Booking> GetByIdAsync(BookingId id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(
        Apartment apartment,
        DateRange duration,
        CancellationToken cancellationToken = default);

    void Add(Booking booking);
}