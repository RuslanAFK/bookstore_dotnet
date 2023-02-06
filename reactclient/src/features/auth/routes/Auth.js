import React, {useEffect, useState} from "react";
import Input from "../../../components/Input";
import {useDispatch, useSelector} from "react-redux";
import {login, register} from "../store/effects";
import {useNavigate} from "react-router-dom";
import {isAuthed, isLoading} from "../store/selectors";
import {ToastContainer} from "react-toastify";
import {notify} from "../../../helpers/notifier";
import {hasError} from "../../../store/selectors";

const Auth = ({isRegisterPage=false}) => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const dispatch = useDispatch();
    const authState = useSelector(state => state.auth);
    const navigate = useNavigate();

    useEffect(() => {
        if (isAuthed(authState)) {
            navigate("/books");
        }
    }, [authState.user])

    useEffect(() => {
        if (!isAuthed(authState) && hasError(authState))
            notify(authState.error, "error");
    }, [authState.error])

    const handleSubmit = (e) => {
        e.preventDefault();
        const userData = { username, password };
        if (isRegisterPage) {
            dispatch(register(userData));
            return;
        }
        dispatch(login(userData));
    }

    return (
        <div>
            <form onSubmit={handleSubmit} className="w-25 p-3 mx-auto">
                <h1>{isRegisterPage ? "Register": "Log In"}</h1>

                <Input name="Username" setter={setUsername}/>
                <Input name="Password" setter={setPassword} type="password"/>

                { isLoading(authState) ?
                    <button type="button" className="btn btn-primary my-3 w-100">Logging in...</button> :
                    <button className="btn btn-primary my-3 w-100">Submit</button>
                }
            </form>
            <ToastContainer/>
        </div>
    )
}

export default Auth;
