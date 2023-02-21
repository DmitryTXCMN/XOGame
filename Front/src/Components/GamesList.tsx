import {useRef, useState} from "react";
import InfiniteScroll from "react-infinite-scroller";
import axios from "../axios";
import {Game} from "../Entities/Game";
import {useNavigate} from "react-router-dom";

export const GamesList = () => {
    const navigate = useNavigate();
    const [games, setGames] = useState<Game[]>([]);
    const [hasMore, setHasMore] = useState(true);
    const page = useRef(-1);

    const onLoadMore = async () => {
        page.current += 1;
        await axios.get(`/Game?page=${page.current}`)
            .then(res => {
                setGames(prev => [...prev, ...res.data.games]);
                setHasMore(res.data.hasMore)
            });
    }

    const onJoin = (game: Game) => {
        if (!game.userXId || !game.userOId)
            navigate(`/${game.id}`);
    }

    const userId = localStorage.getItem('userId');

    return (
        <InfiniteScroll className="gameInfoList" pageStart={0} loadMore={onLoadMore} hasMore={hasMore}>
            {games?.map(game => {
                return <div key={game.id} className="gameInfo">
                    <div className="gameInfoMain">
                        <span className="gameInfoCreatorName"><b>{game.isEnded ? "Winner" : "User"}: {!game.isEnded ? game.creatorName : game.winnerId ? game.winnerId : 'Draw'}</b></span>
                        <span className="gameInfoId">Game: {game.id}</span>
                    </div>
                    <span className="gameInfoEnded">{game.isEnded ? 'Ended' : 'Ongoing'}</span>
                    <button onClick={() => onJoin(game)}
                            style={{color: 'grey', display: game.isEnded || (game.userOId && game.userXId) ? 'none' : 'block'}}>
                        Join
                    </button>
                </div>
            })}
        </InfiniteScroll>
    )
}
