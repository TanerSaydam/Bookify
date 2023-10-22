using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Dapper;

namespace Bookify.Application.Bookings.GetBooking;
internal sealed class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, BookingResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetBookingQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<BookingResponse>> Handle(GetBookingQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT
                                Id,
                                UserId,
                                Status,
                                Price_Amount as PriceAmount,
                                Price_Currency as PriceCurrency,
                                CleaningFee_Amount as CleaningFeeAmount,
                                CleaningFee_Currency as CleaningFeeCurrency,
                                AmenitiesUpCharge_Amount as AmenitiesUpChargeAmount,
                                AmenityUpCharge_Currency as AmenitiesUpChargeCurrency,
                                TotalPrice_Amount as TotalPriceAmount,
                                TotalPrice_Currency as TotalPriceCurrency,
                                DurationStart,
                                DurationEnd,
                                CreatedOnUtc
                           FROM Bookings
                           WHERE Id = @BookingId
                           """;

        var booking = await connection.QueryFirstOrDefaultAsync<BookingResponse>(
            sql,
            new
            {
                request.BookingId
            });

        return booking;
    }
}
