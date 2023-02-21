import React, {useEffect, useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import {CellCont} from "./Cell";
import {Cell, Game} from "../Entities/Game"
import {whoseMove} from "../Domain/gameDomain";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {BASE_URL} from "../config";

export const XOGame = () => {
    const navigate = useNavigate();
    const {id} = useParams();
    const userId = localStorage.getItem('userId');

    const [game, setGame] = useState<Game>({
        cells: [[0, 0, 0], [0, 0, 0], [0, 0, 0]],
        id: id || '',
        isEnded: false,
        createdDateTime: '',
    });
    const [winner, setWinner] = useState<Cell>(Cell.Empty);
    const [connection, setConnection] = useState<null | HubConnection>(null);

    const figure = userId == game.userXId ? 'x' : game.userOId ? 'o' : undefined;

    useEffect(() => {
        const connect = new HubConnectionBuilder()
            .withUrl(BASE_URL + '/gamehub')
            .withAutomaticReconnect()
            .build();

        setConnection(connect);
    }, []);

    useEffect(() => {
        if (connection) {
            connection
                .start()
                .then(async () => {
                    connection.on('ProgressGame', (game: Game) => {
                        setGame(game);
                        console.log(game);
                    });
                    connection.on("GameFinish", (winner: Cell) => {
                        setWinner(winner);
                    })
                    if (id) {
                        connection.invoke("Join", id, userId);
                    }
                })
                .catch(error => console.log('Connection failed: ', error));
        }
    }, [connection]);

    const yourMove = whoseMove(game) === figure;

    const onPlaceFigure = (x: number, y: number, current: Cell) => {
        if (current === Cell.Empty && yourMove && game.userXId && game.userOId) {
            const userId = localStorage.getItem('userId');
            connection?.send("PlaceFigure", x, y, id, userId).catch(e => console.log(e));
        }
    }

    const onRestartGame = () => {
        navigate(`/`);
    }

    const userName = localStorage.getItem('userName');


    return (
        <div className="boardContainer">
            <span>{`You're mark is: ${figure}`}</span>
            {figure && !game.isEnded && (yourMove ? 'Your turn' : whoseMove(game) != null ? 'Waiting...' : 'Waiting 2nd player')}
            <div style={{
                display: "grid",
                gridTemplateColumns: "1fr 1fr 1fr"
            }
            }>
                {game.isEnded
                    ? <span>{winner === Cell.Empty
                        ? 'Tie'
                        : figure
                            ? winner === (figure === 'x' ? Cell.X : Cell.O) ? 'You won' : 'You lost'
                            : `${winner === Cell.X ? 'X' : 'O'} won`}</span>
                        : game.cells.map((arr, x) => arr.map((f, y) => (
                        <CellCont key={`${x}${y}`}
                               onPlaceFigure={() => onPlaceFigure(x, y, f)}
                               cell={f}/>)
                ))}
            </div>
            {figure && game.isEnded && <button onClick={onRestartGame}>Next</button>}   
        </div>
    );
}
