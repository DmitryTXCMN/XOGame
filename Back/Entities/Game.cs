using System.ComponentModel.DataAnnotations.Schema;

namespace Back.Entities;

public enum Cell
{
    Empty = 0,
    X = 1,
    O = 2
}

public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid? UserXId { get; set; }

    public Guid? UserOId { get; set; }

    public Cell c00 { get; set; }
    public Cell c01 { get; set; }
    public Cell c02 { get; set; }
    public Cell c10 { get; set; }
    public Cell c11 { get; set; }
    public Cell c12 { get; set; }
    public Cell c20 { get; set; }
    public Cell c21 { get; set; }
    public Cell c22 { get; set; }

    public bool isEnded { get; set; }

    public Guid? winnerId { get; set; }

    [NotMapped]
    public Cell this[int x, int y]
    {
        get
        {
            return (x, y) switch
            {
                (0, 0) => c00,
                (0, 1) => c01,
                (0, 2) => c02,
                (1, 0) => c10,
                (1, 1) => c11,
                (1, 2) => c12,
                (2, 0) => c20,
                (2, 1) => c21,
                (2, 2) => c22,
                _ => throw new ArgumentException()
            };
        }
        set
        {
            switch (x, y)
            {
                case (0, 0):
                    c00 = value;
                    break;
                case (0, 1):
                    c01 = value;
                    break;
                case (0, 2):
                    c02 = value;
                    break;
                case (1, 0):
                    c10 = value;
                    break;
                case (1, 1):
                    c11 = value;
                    break;
                case (1, 2):
                    c12 = value;
                    break;
                case (2, 0):
                    c20 = value;
                    break;
                case (2, 1):
                    c21 = value;
                    break;
                case (2, 2):
                    c22 = value;
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
