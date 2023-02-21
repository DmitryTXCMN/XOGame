using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Back;
using Back.Domain;
using Back.DTO;
using Back.Entities;
using Back.Clients;

namespace Back.Hubs;

public class GameHub : Hub<IGameClient>
{
    private readonly AppDbContext _dbContext;

    public GameHub(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private async Task<Game> GetGame(Guid gameId) =>
         await _dbContext.Games.FirstOrDefaultAsync(game => game.Id == gameId)
            ?? throw new NullReferenceException($"Game {gameId} not found");

    public async Task Join(Guid gameId, Guid userId)
    {
        var game = await GetGame(gameId);

        if (game.UserXId == null && game.UserOId != userId)
        {
            game.UserXId = userId;
            await _dbContext.SaveChangesAsync();
        }
        if (game.UserOId == null && game.UserXId != userId)
        {
            game.UserOId = userId;
            await _dbContext.SaveChangesAsync();
        }
        if (!(game.UserOId == userId || game.UserXId == userId))
        {
            throw new Exception($"Can't join user: {userId} to the Game: {gameId}");
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        await Clients.Group(gameId.ToString()).ProgressGame(new GameDto(game));
    }

    public async Task PlaceFigure(int x, int y, Guid gameId, Guid userId)
    {
        var game = await GetGame(gameId);

        if (game.isEnded || game[x, y] != Cell.Empty)
        {
            Console.WriteLine($"{gameId}({x},{y}) - game ended or cell isn't free");
            return;
        }

        var whoseMove = GameDomain.GetUserToMove(game);
        switch (whoseMove)
        {
            case Cell.X:
                if (game.UserXId != userId)
                    return;
                break;
            case Cell.O:
                if (game.UserOId != userId)
                    return;
                break;
            case Cell.Empty:
            default:
                return;
        }

        Console.WriteLine($"{gameId}:{whoseMove}: moving ({x},{y})");

        game[x, y] = whoseMove;

        if (GameDomain.IsGameEnded(game))
        {
            game.isEnded = true;
            var winnerCell = GameDomain.IsWinning(game);
            await Clients.Group(gameId.ToString()).GameFinish(winnerCell);
            if (winnerCell != Cell.Empty)
            {
                var winner = await _dbContext.Users
                    .FirstAsync(u => u.Id == (winnerCell == Cell.X ? game.UserXId : game.UserOId));

                game.winnerId = winner.Id;
            }
        }

        await _dbContext.SaveChangesAsync();
        await Clients.Group(gameId.ToString()).ProgressGame(new GameDto(game));
    }
}