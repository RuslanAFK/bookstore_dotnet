import {useDispatch, useSelector} from "react-redux";
import {notify} from "../../shared/services/toast-notifier";
import UserItem from "./UserItem";
import Pagination from "../../shared/components/pagination/Pagination";
import {ToastContainer} from "react-toastify";
import React, {useEffect, useState} from "react";
import {getUsers} from "../store/effects";
import Search from "../../shared/components/search/Search";
import {clearError} from "../store/user-slice";
import {AppDispatch, RootState} from "../../shared/store/store";
import Spinner from "../../shared/components/spinners/Spinner";

const UserList = () => {

    const dispatch = useDispatch<AppDispatch>();
    const userState = useSelector((state: RootState) => state.user);
    const authState = useSelector((state: RootState) => state.auth);

    const [currentPage, setCurrentPage] = useState(1);
    const [search, setSearch] = useState('');

    useEffect(() => {
        const input = {page: currentPage, search};
        dispatch(getUsers(input));
    }, [userState.changed, authState.user, currentPage, search]);

    useEffect(() => {
        if (userState.error) {
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
            {userState.fetching ? <Spinner/> :
            <div>
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
                                        <UserItem key={user.id} user={user} changing={userState.changing} />
                                    )
                            }
                            </tbody>
                        </table>
                        <Pagination total={userState.count-1} setCurrentPage={setCurrentPage} currentPage={currentPage}
                                    pageSize={10}/>
                    </>
                }
            </div>
            }
            <ToastContainer/>
        </div>
    );
}

export default UserList;
