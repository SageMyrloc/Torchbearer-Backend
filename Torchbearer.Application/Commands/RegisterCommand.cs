using MediatR;
using Torchbearer.Application.DTOs;

namespace Torchbearer.Application.Commands;

public record RegisterCommand(string Username, string Password) : IRequest<AuthResponse>;
