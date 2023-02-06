import {useDispatch, useSelector} from "react-redux";
import {useNavigate} from "react-router-dom";
import {isAdminOrCreator, isAuthed, isCreator} from "../../auth/store/selectors";
import {notify} from "../../../helpers/notifier";
import UserItem from "../../../components/UserItem";
import Pagination from "../../../components/Pagination";
import {ToastContainer} from "react-toastify";
import {useEffect, useState} from "react";
import {getUsers} from "../store/effects";
import {applyChanges} from "../store/userSlice";
import {isFetching, isFetched, hasError} from "../../../store/selectors";

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
        dispatch(applyChanges());
    }, [userState.changed, authState.user, currentPage]);

    useEffect(() => {
        if (hasError(userState))
            notify(userState.error, "error");
    }, [userState.error])

    if (!isAuthed(authState)) return (<></>)

    if (isFetching(userState)) return (<h1>Loading...</h1>)

    if (!isFetched(userState)) {
        notify("Something went wrong.", "warning");
    }

    if (userState.users.length === 0) {
        notify("You've seen all users.", "warning");
    }

    return (
        <div>
            <h1 className="text-center">Users</h1>
            <table className="w-100 table table-striped">
                <thead>
                <tr>
                    <th className="w-25">Username</th>
                    <th className="w-25">Is Admin?</th>
                    <th className="w-25">Update?</th>
                    <th className="w-25">Remove?</th>
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
