import React, {FormEvent, useEffect, useState} from "react";
import Input from "../../shared/components/input/Input";
import {useDispatch, useSelector} from "react-redux";
import {login, register} from "../store/effects";
import {useNavigate} from "react-router-dom";
import {isAuthed} from "../store/selectors";
import {ToastContainer} from "react-toastify";
import {notify} from "../../shared/services/toast-notifier";
import {AppDispatch, RootState} from "../../shared/store/store";
import AuthUser from "../interfaces/AuthUser";
import {clearError} from "../store/auth-slice";
import SpinnerButton from "../../shared/components/spinners/SpinnerButton";

type Params = {
    page: "register" | "login"
}

const Auth = ({page}: Params) => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const dispatch = useDispatch<AppDispatch>();
    const authState = useSelector((state: RootState) => state.auth);
    const navigate = useNavigate();

    useEffect(() => {
        if (isAuthed(authState)) {
            navigate("/books");
        }
    }, [authState.user])

    useEffect(() => {
        if (!isAuthed(authState) && authState.error) {
            notify(authState.error, "error");
            dispatch(clearError());
        }
    }, [authState.error])

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const userData: AuthUser = { username, password };
        if (page === "register") {
            dispatch(register(userData));
            return;
        }
        dispatch(login(userData));
    }

    return (
        <div>
            <form onSubmit={handleSubmit} className="w-25 p-3 mx-auto">
                <h1>{page === "register" ? "Register": "Log In"}</h1>

                <Input name="Username" setter={setUsername}/>
                <Input name="Password" setter={setPassword} type="password"/>

                { authState.fetching || authState.changing ?
                    <div className="my-3 w-100">
                        <SpinnerButton/>
                    </div> :
                    <button className="btn btn-primary my-3 w-100">Submit</button>
                }
            </form>
            <ToastContainer/>
        </div>
    )
}

export default Auth;
