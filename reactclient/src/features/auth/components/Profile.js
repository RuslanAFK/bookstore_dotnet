import {useDispatch, useSelector} from "react-redux";
import {isAuthed} from "../store/selectors";
import {useEffect, useState} from "react";
import Input from "../../../components/Input";
import {updateProfile} from "../store/effects";
import {hasError} from "../../../store/selectors";
import {notify} from "../../../helpers/notifier";
import {ToastContainer} from "react-toastify";

const Profile = () => {

    const authState = useSelector(state => state.auth);

    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');

    const dispatch = useDispatch();

    useEffect(() => {
        if (isAuthed(authState)) {
            setUsername(authState.user.username);
            notify("Successfully loaded profile.", "success");
        }
    }, [authState.user])


    useEffect(() => {
        if (hasError(authState))
            notify(authState.error, "error");
    }, [authState.error])

    const onSubmit = (e) => {
        e.preventDefault();
        const id = authState.user.id;
        const userData = {id, username, password};
        if (newPassword!=='')
            userData.newPassword = newPassword;
        dispatch(updateProfile(userData));
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