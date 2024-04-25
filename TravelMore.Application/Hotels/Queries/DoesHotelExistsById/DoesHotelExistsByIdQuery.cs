using MediatR;
using TravelMore.Application.Common.Results;

namespace TravelMore.Application.Hotels.Queries.DoesHotelExistsById;

public record DoesHotelExistsByIdQuery(Guid HotelId) : IRequest<Result<bool>>;
