import {Cell, Game} from "../Entities/Game"

export const whoseMove = (game: Game) => {
    let xCount = 0;
    let oCount = 0;
    game.cells.forEach(line => line.forEach(f => {
        if (f === Cell.X)
            xCount++;
        if (f === Cell.O)
            oCount++;
    }));

    if (!game.userXId || !game.userOId)
    return null;

    return xCount > oCount ? 'o' : 'x';
}