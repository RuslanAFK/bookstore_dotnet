import {useDispatch, useSelector} from "react-redux";
import {useNavigate} from "react-router-dom";
import {isAdminOrCreator, isAuthed, isCreator} from "../../auth/store/selectors";
import {notify} from "../../../helpers/notifier";
import UserItem from "../../../components/UserItem";
import Pagination from "../../../components/Pagination";
import {ToastContainer} from "react-toastify";
import {useEffect, useState} from "react";
import {isFetched, isFetching, isUserListEmpty} from "../store/selectors";
import {getUsers} from "../store/effects";
import auth from "../../auth/routes/Auth";

const UserList = () => {

    const dispatch = useDispatch();
    const userState = useSelector(state => state.user);
    const authState = useSelector(state => state.auth);
    const navigate = useNavigate();

    const [currentPage, setCurrentPage] = useState(1);


    useEffect(() => {
        if (!isCreator(authState))
            navigate('/login');
        if (isAuthed(authState))
            navigate(`/users?page=${currentPage}`);
        dispatch(getUsers(currentPage));
    }, [userState.changed, authState.user, currentPage]);

    if (!isAuthed(authState)) return (<></>)

    if (isFetching(userState)) return (<h1>Loading...</h1>)

    if (!isFetched(userState)) {
        notify("Something went wrong.", "warning");
    }

    if (isUserListEmpty(userState)) {
        notify("You've seen all users.", "warning");
    }

    return (
        <div>
            <h1 className="text-center">Users</h1>
            <table>
                <thead>
                <tr>
                    <th>Username</th>
                    <th>Role</th>
                    <th>Update</th>
                    <th>Remove</th>
                </tr>
                </thead>
                <tbody>
                {

                    userState.users
                        .filter(user => user.username !== authState.user.username)
                        .map((user) =>
                            <UserItem key={user.id} user={user} isAdmin={isAdminOrCreator(authState)}/>
                        )
                }
                </tbody>
            </table>
            <Pagination total={userState.count-1} setCurrentPage={setCurrentPage} currentPage={currentPage}
                pageSize={10}/>
            <ToastContainer/>
        </div>
    );
}

export default UserList;
