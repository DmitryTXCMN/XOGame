export enum Cell {
    Empty = 0,
    X = 1,
    O = 2,
}

export interface Game {
    id: string;
    creatorName?: string;
    cells: Cell[][];
    isEnded: boolean;
    userXId?: string;
    userOId?: string;
    winnerId?: string;
    createdDateTime: string;
}