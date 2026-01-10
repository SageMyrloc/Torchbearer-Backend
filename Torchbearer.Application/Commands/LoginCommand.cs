using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record LoginCommand(string Username, string Password) : IRequest<AuthResponse>;
