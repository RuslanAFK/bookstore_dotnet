import React, {useEffect, useState} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {getBooks} from "../store/effects";
import {isAdmin, isAuthed} from "../../auth/store/helpers";
import {isBookListEmpty, isFetched, isFetching} from "../store/helpers";
import {ToastContainer} from "react-toastify";
import {notify} from "../../../notifier";
import BookItem from "../../../components/BookItem";
import Pagination from "../../../components/Pagination";
import {useNavigate} from "react-router-dom";

const AllBooks = () => {

    const dispatch = useDispatch();
    const bookState = useSelector(state => state.book);
    const authState = useSelector(state => state.auth);
    const navigate = useNavigate();

    const [currentPage, setCurrentPage] = useState(1);


    useEffect(() => {
        if (isAuthed(authState))
            navigate(`/books?page=${currentPage}`);
        dispatch(getBooks(currentPage));
    }, [bookState.changed, currentPage]);



    if (isFetching(bookState)) return (<h1>Loading...</h1>)

    if (!isFetched(bookState)) {
        notify("Something went wrong.", "warning");
    }


    if (isBookListEmpty(bookState)) {
        notify("You've seen all books.", "warning");
    }

    return (
        <div>
            <h1 className="text-center">Books</h1>
            <ul className="list-group list-group-horizontal-md">
                {
                    bookState.books.map((book) =>
                        <BookItem book={book} isAdmin={isAdmin(authState)}/>)
                }
            </ul>
            <Pagination total={bookState.count} setCurrentPage={setCurrentPage} currentPage={currentPage}/>
            <ToastContainer/>
        </div>
    );
}

export default AllBooks;
