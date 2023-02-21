using Back.Entities;

namespace Back.DTO;

public class CreateGameDto
{
    public Guid Id { get; set; }
    public Cell statringCell { get; set; }
    public CreateGameDto(Guid id, Cell statringCell)
    {
        Id = id;
        this.statringCell = statringCell;
    }
}
