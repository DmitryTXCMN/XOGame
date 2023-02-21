import {useNavigate, useParams} from "react-router-dom";

export enum WhoWon {
    Me = 'meWon',
    Opponent = 'meLose',
    Tie = 'tie',
}

export const GameEnd = () => {
    const {winner} = useParams();
    const navigate = useNavigate();
    let won;
    switch (winner) {
        case WhoWon.Me:
            won = <span>You won!</span>;
            break;
        case WhoWon.Opponent:
            won = <span>You lost</span>;
            break;
        case WhoWon.Tie:
        default:
            won = <span>Tie</span>;
            break;
    }
    
    const onNext = () => {
        navigate('/');
    }
    
    return (
        <>
            {won}
            <p onClick={onNext}>Next</p>
        </>
    )
}