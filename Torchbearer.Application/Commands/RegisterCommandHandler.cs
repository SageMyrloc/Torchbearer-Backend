using MediatR;
using Torchbearer.Application.DTOs;
using Torchbearer.Application.Interfaces;
using Torchbearer.Domain.Entities;

namespace Torchbearer.Application.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IPlayerRepository playerRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _playerRepository = playerRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingPlayer = await _playerRepository.GetByUsernameAsync(request.Username);
        if (existingPlayer != null)
        {
            throw new InvalidOperationException("Username already exists");
        }

        var hashedPassword = _passwordHasher.Hash(request.Password);

        var player = new Player(request.Username, hashedPassword);

        await _playerRepository.AddAsync(player);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var role = await _roleRepository.GetByNameAsync("Player");
        if (role == null)
        {
            throw new InvalidOperationException("Player role not found");
        }

        var playerRole = new PlayerRole(player.Id, role.Id);
        player.AddRole(playerRole);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(player);
        var roles = player.PlayerRoles.Select(pr => pr.Role.Name);

        return new AuthResponse(token, player.Username, player.Id, roles);
    }
}
