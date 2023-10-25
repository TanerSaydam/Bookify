using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;

namespace Bookify.Application.Bookings.ReserveBooking;

public record ReserveBookingCommand(
    ApartmentId ApartmentId,
    UserId UserId,
    DateOnly StartDate,
    DateOnly EndDate) : ICommand<BookingId>;