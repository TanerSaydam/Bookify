using Bookify.Application.Abstractions.Data;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using Dapper;

namespace Bookify.Application.Apartments.SearchApartments;

internal sealed class SearchApartmentsQueryHandler :
    IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentResponse>>
{
    private static readonly int[] ActiveBookingStatuses = new[]
    {
        (int)BookingStatus.Reserved,
        (int)BookingStatus.Confirmed,
        (int)BookingStatus.Completed
    };

private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchApartmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<ApartmentResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
    {
        if (request.StartDate > request.EndDate)
        {
            return new List<ApartmentResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
                           SELECT
                                a.Id,
                                a.Name,
                                a.Description,
                                a.Price_Amount as PriceAmount,
                                a.Price_Currency as PriceCurrency,
                                a.Address_Country as Country,
                                a.Address_City as City,
                                a.Address_State as State,
                                a.Address_ZipCode as ZipCode,
                                a.Address_Street as Street,
                           FROM Apartments AS a
                           WHERE NOT EXISTS
                           (
                                SELECT 1
                                FROM Bookings as b
                                WHERE
                                    b.ApartmentId = a.Id AND
                                    b.DurationStart <= @EndDate AND
                                    b.DurationEnd >= @StartDate
                                    b.Status = ANY(@ActiveBookingStatuses)
                           )
                           """;
        var apartments = await connection
            .QueryAsync<ApartmentResponse, AddressResponse, ApartmentResponse>(
                sql,
                (apartment, address) =>
                {
                    apartment.Address = address;
                    return apartment;
                },
            new
                {
                    request.StartDate,
                    request.EndDate,
                    ActiveBookingStatuses
                },
                splitOn: "Country"); ;

        return apartments.ToList();
    }
}