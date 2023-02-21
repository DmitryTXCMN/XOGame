using Back.Entities;

namespace Back.DTO;

public class GameDto
{
    public Guid Id { get; set; }
    public Cell[][] Cells { get; set; }
    public bool isEnded { get; set; }
    public string? CreatorName { get; set; }
    public Guid? winnerId { get; set; }
    public Guid? UserXId { get; set; }
    public Guid? UserOId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public GameDto(Game game)
    {
        Id = game.Id;
        isEnded = game.isEnded;
        UserXId = game.UserXId;
        UserOId = game.UserOId;
        winnerId = game.winnerId;

        Cells = new Cell[3][];
        for (var x = 0; x < 3; ++x)
        {
            Cells[x] = new Cell[3];
        }
        for (var x = 0; x < 3; ++x)
        {
            for (var y = 0; y < 3; ++y)
            {
                Cells[x][y] = game[x, y];
            }
        }
    }
}
