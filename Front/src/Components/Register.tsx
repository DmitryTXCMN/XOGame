import {useRef} from "react";
import axios from "../axios";

export const Register = () => {
    const username = useRef<HTMLInputElement>(null);
    const pass1 = useRef<HTMLInputElement>(null);
    const pass2 = useRef<HTMLInputElement>(null);

    const onRegister = async () => {
        const usernameData = username.current?.value;
        const pass1Data = pass1.current?.value;
        const pass2Data = pass2.current?.value;
        if (!usernameData || !pass1Data || !pass2Data) {
            alert('Wrong data. Check fields.');
            return;
        }
        if (pass1Data !== pass2Data) {
            alert('Passwords are not equal');
            return;
        }
        if (usernameData.length < 6) {
            alert('Validation error. Username should be at least 6 symbols length');
            return;
        }
        const res = await axios.post('Auth/Register', {
            username: usernameData,
            password: pass1Data,
            repeatPassword: pass2Data
        });
        
        alert('Account created');
        window.location.replace('/login');
    }

    const onGoToLogin = () => {
        window.location.replace('/login');
    }

    return (
        <div className="authForm">
            <input ref={username} placeholder={'username'}/>
            <input ref={pass1} placeholder={'password'}/>
            <input ref={pass2} placeholder={'repeat password'}/>
            <button className="button" onClick={onRegister}>register</button>
            <br/>
            <button className="spanLink" onClick={onGoToLogin}>Already have an account?</button>
        </div>
    )
}