using Bookify.Domain.Apartments;
using Bookify.Domain.Users;

namespace Bookify.Api.Controllers.Bookings;

public sealed record ReserveBookingRequest(
    ApartmentId ApartmentId,
    UserId UserId,
    DateOnly StartDate,
    DateOnly EndDate);