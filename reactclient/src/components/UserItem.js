import {useDispatch} from "react-redux";
import {deleteUser, updateUserRole} from "../features/users/store/effects";
import {useState} from "react";

const UserItem = ({user}) => {

    const dispatch = useDispatch();

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
            const data = {id, roleName}
            dispatch(updateUserRole(data));
        }
    }

    const isUserAdmin = () => user.roleName === "Admin";

    const [isAdmin, setIsAdmin] = useState(isUserAdmin());

    const onChange = ($event) => {
        setIsAdmin($event.target.checked);
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