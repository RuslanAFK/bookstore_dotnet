import {useDispatch, useSelector} from "react-redux";
import {FormEvent, useEffect, useState} from "react";
import Input from "../../shared/components/Input";
import {deleteAccount, updateProfile} from "../store/effects";
import {notify} from "../../shared/services/toast-notifier";
import {ToastContainer} from "react-toastify";
import {AppDispatch, RootState} from "../../shared/store/store";
import React from "react";
import UpdateUser from "../interfaces/UpdateUser";
import {applyUpdate, clearError} from "../store/auth-slice";
import SpinnerButton from "../../shared/components/SpinnerButton";
import Spinner from "../../shared/components/Spinner";
import {isCreator} from "../store/selectors";
import MainLabel from "../../shared/components/MainLabel";

const Profile = () => {

    const authState = useSelector((state: RootState) => state.auth);

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');

    const dispatch = useDispatch<AppDispatch>();

    useEffect(() => {
        if (authState.user) {
            setUsername(authState.user.username);
        }
    }, [authState.user])

    useEffect(() => {
        if (authState.updated) {
            notify("Successfully updated profile.", "success");
            dispatch(applyUpdate());
        }
    }, [authState.updated])


    useEffect(() => {
        if (authState.error) {
            notify(authState.error, "error");
            dispatch(clearError());
        }
    }, [authState.error])

    const onSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const id = authState.user?.id;
        if (id !== undefined) {
            let userData: UpdateUser = {id, username, password};
            if (newPassword!=='') {
                userData = {id, username, password, newPassword};
            }
            dispatch(updateProfile(userData));
        }
    }

    const onDeleteAccount = () => {
        if (authState.user && window.confirm("Do you really want to delete your account?")) {
            dispatch(deleteAccount({password}));
        }
    }


    return (
        <div>
            {authState.fetching ? <Spinner/> :
                <div>
                    <MainLabel text="User Profile"/>
                    <form className="mx-auto w-25" onSubmit={onSubmit}>
                        <Input name="Username" setter={setUsername} value={username} text="Required."
                               textStyle="danger" maxLength={16} minLength={3} />
                        <Input name="Password" setter={setPassword} text="Required." textStyle="danger"
                               type="password" maxLength={16} minLength={3} />
                        <Input name="New Password" setter={setNewPassword} text="Optional."
                               type="password" maxLength={16} minLength={3} required={false} />
                        {authState.changing ?
                            <div className="my-2 w-100">
                                <SpinnerButton/>
                            </div> :
                            <button className="btn btn-primary w-100 my-2">Save</button>
                        }
                        {authState.changing ?
                            <div className="my-2 w-100">
                                <SpinnerButton/>
                            </div> :
                            <button disabled={isCreator(authState)} type="button"
                                    className="btn btn-danger w-100 my-2"
                                    onClick={onDeleteAccount}
                            >Delete Account</button>
                        }
                    </form>
                </div>}
            <ToastContainer/>
    </div>)
}

export default Profile;