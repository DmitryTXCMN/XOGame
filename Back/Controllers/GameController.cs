using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back;
using Back.DTO;
using Back.Entities;

namespace Back.Controllers;

[ApiController]
[Route("[controller]"), OpenIdDictAuthorize]
public class GameController : ControllerBase
{
    private readonly AppDbContext _postgresDbContext;
    private readonly UserManager<IdentityUser<Guid>> _userManager;

    public GameController(AppDbContext postgresDbContext, UserManager<IdentityUser<Guid>> userManager)
    {
        _postgresDbContext = postgresDbContext;
        _userManager = userManager;
    }


    [HttpPost, OpenIdDictAuthorize]
    public async Task<IActionResult> CreateGame()
    {
        var user = await _userManager.GetUserAsync(User);
        var statringCell = (Cell)(Random.Shared.Next(1, 100) % 2 + 1);
        var game = new Game
        {
            UserXId = statringCell == Cell.X ? user.Id : null,
            UserOId = statringCell == Cell.O ? user.Id : null,
        };
        await _postgresDbContext.Games.AddAsync(game);
        await _postgresDbContext.SaveChangesAsync();
        return Ok(new CreateGameDto(game.Id,statringCell));
    }

    public record GetGamesDto(int Page);

    public record GamesListDto(List<GameDto> Games, bool HasMore);

    [HttpGet, OpenIdDictAuthorize]
    public async Task<IActionResult> GetActualGames([FromQuery] GetGamesDto getGamesDto)
    {
        UserManager<IdentityUser<Guid>> userManager = _userManager;
        const int pageLength = 5;
        var games = await _postgresDbContext.Games
            .Skip(getGamesDto.Page * pageLength)
            .Take(pageLength)
            .Select(game => new GameDto(game){
                CreatorName = (game.UserOId != null ? game.UserOId : game.UserXId).ToString()
            })
            .ToListAsync();
        var hasMore = await _postgresDbContext.Games.CountAsync() > pageLength * (getGamesDto.Page + 1);
        return Ok(new GamesListDto(games, hasMore));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAll()
    {
        _postgresDbContext.Games.RemoveRange(_postgresDbContext.Games);
        await _postgresDbContext.SaveChangesAsync();
        return Ok();
    }
}