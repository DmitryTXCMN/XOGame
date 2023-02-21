using Back.Entities;

namespace Back.Domain;

public static class GameDomain
{
    public static Cell GetUserToMove(Game game)
    {
        if (game.isEnded)
            return Cell.Empty;

        var xCount = 0;
        var oCount = 0;
        for (var f = 0; f < 3; ++f)
            for (var s = 0; s < 3; ++s)
                switch (game[f, s])
                {
                    case Cell.X:
                        xCount++;
                        break;
                    case Cell.O:
                        oCount++;
                        break;
                    case Cell.Empty:
                    default:
                        break;
                }

        return xCount > oCount ? Cell.O : Cell.X;
    }

    public static Cell IsWinning(Game game)
    {
        for (var i = 0; i < 3; ++i)
        {
            if (game[i, 0] != Cell.Empty && game[i, 0] == game[i, 1] &&
                game[i, 0] == game[i, 2])
                return game[i, 0];
            if (game[0, i] != Cell.Empty && game[0, i] == game[1, i] &&
                game[0, i] == game[2, i])
                return game[0, i];
        }

        if (game[0, 0] != Cell.Empty && game[0, 0] == game[1, 1] &&
            game[0, 0] == game[2, 2])
            return game[0, 0];

        if (game[0, 2] != Cell.Empty && game[0, 2] == game[1, 1] &&
            game[0, 2] == game[2, 0])
            return game[0, 2];

        return Cell.Empty;
    }

    public static bool IsGameEnded(Game game)
    {
        if (IsWinning(game) != Cell.Empty)
            return true;
        var emptyCellExists = false;
        for (var x = 0; x < 3; ++x)
            for (var y = 0; y < 3; ++y)
            {
                if (game[x, y] == Cell.Empty)
                    emptyCellExists = true;
            }

        return !emptyCellExists;
    }
}