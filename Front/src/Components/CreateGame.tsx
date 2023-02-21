import axios from "../axios";
import {useNavigate} from "react-router-dom";
import { BASE_URL } from "../config";

export const CreateGame = () => {
    const navigate = useNavigate();
    const onCreateNew = () => {
        axios.post(`${BASE_URL}/Game/`).then(function (res) {
            navigate(`/${res.data.id}`);
        })
    }

    return (<div className="infoForm">
        <span>
            Create new Game?
        </span>
        <button className="button" onClick={onCreateNew}>Create game</button>
    </div>)
}