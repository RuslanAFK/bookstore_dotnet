import {useDispatch} from "react-redux";
import {deleteUser, updateUserRole} from "../store/effects";
import React, {FormEvent, useState} from "react";
import {AppDispatch} from "../../shared/store/store";
import UpdateUserRole from "../interfaces/UpdateUserRole";
import GetUser from "../interfaces/GetUser";

type Params = {
    user: GetUser
}

const UserItem = ({user}: Params) => {

    const dispatch = useDispatch<AppDispatch>();

    const onUserDelete = () => {
        const {id, username} = user;
        if (window.confirm(`Do you really want to delete ${username}?`)) {
            dispatch(deleteUser(id));
        }
    }

    const onUserSave = () => {
        const {id, username} = user;
        if (window.confirm(`Do you really want to update ${username}?`)) {
            let roleName = isAdmin ? "Admin": "User";
            const data: UpdateUserRole = {id, roleName}
            dispatch(updateUserRole(data));
        }
    }

    const isUserAdmin = () => user.roleName === "Admin";

    const [isAdmin, setIsAdmin] = useState(isUserAdmin());

    const onChange = (event: FormEvent<HTMLInputElement>) => {
        const checked  = (event.target as HTMLInputElement).checked;
        setIsAdmin(checked);
    }


    return (
        <tr>
            <td>
                <div>{user.username}</div>
            </td>
            <td>
                <input type="checkbox" defaultChecked={isUserAdmin()} onChange={onChange}  />
            </td>
            <td>
                <button className='w-50 my-2 btn btn-secondary'
                        onClick={onUserSave}>
                    Update</button>
            </td>
            <td>
                <button className='w-50 my-2 btn btn-secondary'
                        onClick={onUserDelete}>
                    Delete</button>
            </td>
        </tr>
    )
}

export default UserItem;