import React, { useState } from "react";
import Input from "../../../components/Input";
import {useDispatch} from "react-redux";
import {login} from "../store/thunks";

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const dispatch = useDispatch();

    const handleSubmit = (e) => {
        e.preventDefault();
        const userData = { username, password };
        dispatch(login(userData));
    }

    return (
        <form>
            <h1>Log In</h1>

            <Input name="Username" setter={setUsername}/>
            <Input name="Password" setter={setPassword} type="password"/>

            <button className="btn btn-primary" onClick={handleSubmit}>Log In</button>
        </form>
    )
}

export default Login;
