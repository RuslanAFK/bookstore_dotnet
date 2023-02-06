import React, {useEffect, useState} from "react";
import Input from "../../../components/Input";
import {useDispatch, useSelector} from "react-redux";
import {login, register} from "../store/effects";
import {useNavigate} from "react-router-dom";
import {hasError, isAuthed, isLoading} from "../store/selectors";
import {ToastContainer} from "react-toastify";
import {notify} from "../../../helpers/notifier";

const Auth = ({isRegisterPage=false}) => {

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [isAdmin, setIsAdmin] = useState(false);

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
            userData.isAdmin = isAdmin;
            dispatch(register(userData));
            return;
        }
        dispatch(login(userData));
    }

    return (
        <>
        <form>
            <h1>{isRegisterPage ? "Register": "Log In"}</h1>

            <Input name="Username" setter={setUsername}/>
            <Input name="Password" setter={setPassword} type="password"/>

            {isRegisterPage && (
                <Input
                    type="checkbox"
                    setter={setIsAdmin}
                    className="form-check"
                    name="Is Admin?"
                />
            )}

            { isLoading(authState) ?
                <button type="button" className="btn btn-primary">Logging in...</button> :
                <button className="btn btn-primary" onClick={handleSubmit}>Submit</button>
            }
        </form>
            <ToastContainer/>
        </>
    )
}

export default Auth;
