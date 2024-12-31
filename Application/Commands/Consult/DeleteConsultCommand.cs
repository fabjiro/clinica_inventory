using Ardalis.Result;
using MediatR;

public record DeleteConsultCommand(string UserId,Guid Id) : IRequest<Result<Guid>>;