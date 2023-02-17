import {useDispatch, useSelector} from "react-redux";
import {isAdminOrCreator} from "../../auth/store/selectors";
import {notify} from "../../../services/toast-notifier";
import UserItem from "./UserItem";
import Pagination from "../../../components/Pagination";
import {ToastContainer} from "react-toastify";
import {useEffect, useState} from "react";
import {getUsers} from "../store/effects";
import {hasError} from "../../../store/selectors";
import Search from "../../../components/Search";
import {clearError} from "../store/userSlice";

const UserList = () => {

    const dispatch = useDispatch();
    const userState = useSelector(state => state.user);
    const authState = useSelector(state => state.auth);

    const [currentPage, setCurrentPage] = useState(1);
    const [search, setSearch] = useState('');

    useEffect(() => {
        const input = {page: currentPage, search};
        dispatch(getUsers(input));
    }, [userState.changed, authState.user, currentPage, search]);

    useEffect(() => {
        if (hasError(userState)) {
            notify(userState.error, "error");
            dispatch(clearError());
        }
    }, [userState.error])

    const isListEmpty = () => userState.users.filter(u =>
        authState.user && u.username !== authState.user.username
    ).length === 0;

    return (
        <div>
            <h1 className="text-center">Users</h1>
            <Search search={search} setSearch={setSearch}/>
            {isListEmpty() ?
                <h2 className="text-center">You've seen all users.</h2> :
                <>
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
                                .filter(user => authState.user && user.username !== authState.user.username)
                                .map((user) =>
                                    <UserItem key={user.id} user={user} isAdmin={isAdminOrCreator(authState)}/>
                                )
                        }
                        </tbody>
                    </table>
                    <Pagination total={userState.count-1} setCurrentPage={setCurrentPage} currentPage={currentPage}
                                pageSize={10}/>
                </>
            }
            <ToastContainer/>
        </div>
    );
}

export default UserList;
