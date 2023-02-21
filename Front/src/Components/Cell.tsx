import {Cell} from "../Entities/Game";

type Props = {
    onPlaceFigure: () => any;
    cell: Cell;
}
export const CellCont = (props: Props) => {
    let mappedCell = undefined;
    switch (props.cell){
        case Cell.X:
            mappedCell = 'X';
            break;
        case Cell.O:
            mappedCell = 'O';
            break;
    }
    
    return (
        <div className={mappedCell ? "cell" : "cell cellHoverable"}
        onClick={props.onPlaceFigure}>
            {mappedCell}
        </div>
    );
}