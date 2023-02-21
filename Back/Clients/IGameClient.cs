using Back.DTO;
using Back.Entities;

namespace Back.Clients;

public interface IGameClient
{
    Task ProgressGame(GameDto game);
    Task GameFinish(Cell winner);
}