import {useDispatch, useSelector} from "react-redux";
import {FormEvent, useEffect, useState} from "react";
import Input from "../../../components/Input";
import {updateProfile} from "../store/effects";
import {notify} from "../../../services/toast-notifier";
import {ToastContainer} from "react-toastify";
import {AppDispatch, RootState} from "../../../store/store";
import React from "react";
import UpdateUser from "../interfaces/UpdateUser";

const Profile = () => {

    const authState = useSelector((state: RootState) => state.auth);

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');

    const dispatch = useDispatch<AppDispatch>();

    useEffect(() => {
        if (authState.user) {
            setUsername(authState.user.username);
            notify("Successfully loaded profile.", "success");
        }
    }, [authState.user])


    useEffect(() => {
        if (authState.error)
            notify(authState.error, "error");
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


    return (<div>
        <h2 className="text-center my-2">User Profile</h2>
        <form className="mx-auto w-25" onSubmit={onSubmit}>
            <Input name="Username" setter={setUsername} value={username} text="Required." textStyle="danger" />
            <Input name="Password" setter={setPassword} text="Required." textStyle="danger" type="password" />
            <Input name="New Password" setter={setNewPassword} text="Optional." type="password" />
            <button className="btn btn-primary w-100">Save</button>
        </form>
        <ToastContainer/>
    </div>)
}

export default Profile;