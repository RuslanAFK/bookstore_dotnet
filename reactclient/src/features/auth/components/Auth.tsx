import React, {FormEvent, useEffect, useState} from "react";
import Input from "../../shared/components/Input";
import {useDispatch, useSelector} from "react-redux";
import {login, register} from "../store/effects";
import {useNavigate} from "react-router-dom";
import {isAuthed} from "../store/selectors";
import {ToastContainer} from "react-toastify";
import {notify} from "../../shared/services/toast-notifier";
import {AppDispatch, RootState} from "../../shared/store/store";
import AuthUser from "../interfaces/AuthUser";
import {clearError} from "../store/auth-slice";
import SpinnerButton from "../../shared/components/SpinnerButton";
import MainLabel from "../../shared/components/MainLabel";
import AuthProps from "../component-props/AuthProps";


const Auth = ({page}: AuthProps) => {

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
                <MainLabel text={page === "register" ? "Register": "Log In"} />
                <Input name="Username" setter={setUsername} minLength={3} maxLength={16} />
                <Input name="Password" setter={setPassword} type="password" minLength={3} maxLength={16} />

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
