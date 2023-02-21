import React from 'react';
import './App.css';
import {BrowserRouter, Link, Navigate, Route, Routes} from 'react-router-dom';
import {GameEnd, XOGame} from './Components';
import {Register} from "./Components/Register";
import {Login} from "./Components/Login";
import {GamesList} from "./Components/GamesList";
import {CreateGame} from './Components/CreateGame';

function App() {
    
    const onSignOut = () => {
        localStorage.removeItem('jwt');
        window.location.replace('/login');
    }

    const onGoToLogin = () => {
        window.location.replace('/login');
    }

    
    const onGoToRegister = () => {
        window.location.replace('/register');
    }


    const authorized = !!localStorage.getItem('jwt');

    if (!authorized && !window.location.href.endsWith('login'))
        window.location.replace('/login')

    return (
        <div className="body">
            <BrowserRouter>
                    {authorized ? (
                    <div className='header'>
                        <Link to={'/list'}>Games</Link>
                        <Link to={'/create'}>Start new game</Link>
                        <button onClick={onSignOut}>Logout</button>
                    </div>
                    )
                        : (
                            <div className='header'>
                            </div>
                        )
                    }
                <div className="content">
                    <Routes>
                        <Route path={'/'} element={<Navigate replace to={'/list'}/>}/>
                        <Route path={'/register'} element={<Register/>}/>
                        <Route path={'/login'} element={<Login/>}/>
                        <Route path={'/list'} element={<GamesList/>}/>
                        <Route path={'/create'} element={<CreateGame/>}/>
                        <Route path={'/gameEnd/:winner'} element={<GameEnd/>}/>
                        <Route path={"/:figure/:id"} element={<XOGame/>}/>
                        <Route path={"/:id"} element={<XOGame/>}/>
                    </Routes>
                </div>
            </BrowserRouter>
        </div>
    );
}

export default App;
