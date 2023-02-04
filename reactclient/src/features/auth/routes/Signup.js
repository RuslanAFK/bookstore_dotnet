import React, { useState } from "react";
import { Link } from "react-router-dom";
import Input from "../../../components/Input";
import {useDispatch} from "react-redux";
import {register} from "../store/thunks";

const Signup = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [isAdmin, setIsAdmin] = useState(false);
    const dispatch = useDispatch();

    const handleSubmit = (e) => {
        e.preventDefault();
        const userData = {username, password, isAdmin};
        dispatch(register(userData));
    }

    return (
        <form>
            <h1>Sign Up</h1>

            <Input name="Username" setter={setUsername}/>
            <Input name="Password" setter={setPassword} type="password"/>

            <Input
                type="checkbox"
                setter={setIsAdmin}
                className="form-check"
                name="Is Admin?"
            />

            <button className="btn btn-primary" onClick={handleSubmit}>Sign Up</button>
        </form>
    )
}

export default Signup;
