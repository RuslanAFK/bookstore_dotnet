import React, {FormEvent, useEffect, useState} from "react";
import Input from "../../shared/components/Input";
import {useDispatch, useSelector} from "react-redux";
import {login, register} from "../store/effects";
import {Link, useNavigate} from "react-router-dom";
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
    const [email, setEmail] = useState('');
    const [age, setAge] = useState(1);
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
        if (isPageRegister() && age) {
            userData.age = age;
            userData.email = email;
            dispatch(register(userData));
            return;
        }
        dispatch(login(userData));
    }

    const isPageRegister = () => {
        return page === 'register';
    }

    const getBelowLinkText = () => {
        return isPageRegister() ? "Already have an account" : "Don't have an account";
    }

    const getPageName = () => {
        return isPageRegister() ? "Sign up" : "Login";
    }

    const toDefaultPage = () => {
        navigate("/");
    };

    return (
        <div>
            <form onSubmit={handleSubmit} className="p-3 w-50 mx-auto">
                <MainLabel text={getPageName()} />
                {isPageRegister() ?
                    (<div>
                            <Input name="Email" setter={setEmail} minLength={3} maxLength={16} placeholder={"example@gmail.com"} />
                            <Input name="Name" setter={setUsername} minLength={5} maxLength={16} placeholder={"user101"}
                                   text={"Minimum length is 5. Use only letters and numbers"} textStyle={"black"}/>
                            <Input name="Password" setter={setPassword} type="password" minLength={3} maxLength={16}
                                   placeholder={"KJRHIYF76876&^d658%$%"} text={"Minimum length is 8. Use uppercase and lowercase letters, at least one number and special symbol"} textStyle={"black"} />
                            <Input name="Age" setter={setAge} value={age} type="number" />
                    </div>
                    ) : (
                        <div>
                            <Input name="Name" setter={setUsername} minLength={5} maxLength={16} placeholder={"user101"} />
                            <Input name="Password" setter={setPassword} type="password" minLength={3} maxLength={16}
                                   placeholder={"KJRHIYF76876&^d658%$%"} />
                        </div>
                    )}

                <Link className="my-5" to={isPageRegister() ? '/login': '/register'}>
                    {getBelowLinkText()}
                </Link>

                { authState.fetching || authState.changing ?
                    <div className="my-3 w-100">
                        <SpinnerButton/>
                    </div> :
                    <div className="row">
                        <button className="btn btn-primary my-5 col-5" type={"button"} onClick={toDefaultPage}>Cancel</button>
                        <span className="col-2"></span>
                        <button className="btn btn-primary my-5 col-5" type={"submit"}>{getPageName()}</button>
                    </div>
                }
            </form>
            <ToastContainer/>
        </div>
    )
}

export default Auth;
