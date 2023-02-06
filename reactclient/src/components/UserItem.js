const UserItem = ({user}) => {

    const onUserDelete = ({id, username}) => {
        if (window.confirm(`Do you really want to delete ${username}?`)) {
        }
    }

    const onUserSave = ({id, username}) => {
        if (window.confirm(`Do you really want to update ${username}?`)) {
        }
    }

    const isUserAdmin = () => user.roleName === "Admin";



    return (
        <tr>
            <td>
                <div>{user.username}</div>
            </td>
            <td>
                <input type="checkbox" defaultChecked={isUserAdmin()}/>
            </td>
            <td>
                <button className='w-50 my-2 btn btn-secondary'
                        onClick={() => onUserSave(user)}>
                    +</button>
            </td>
            <td>
                <button className='w-50 my-2 btn btn-secondary'
                        onClick={() => onUserDelete(user)}>
                    +</button>
            </td>
        </tr>
    )
}

export default UserItem;