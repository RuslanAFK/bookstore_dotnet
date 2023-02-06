import React, {useEffect, useState} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {getBooks} from "../store/effects";
import {isAdminOrCreator, isAuthed} from "../../auth/store/selectors";
import {isBookListEmpty, isFetched, isFetching} from "../store/selectors";
import {ToastContainer} from "react-toastify";
import {notify} from "../../../helpers/notifier";
import BookItem from "../../../components/BookItem";
import Pagination from "../../../components/Pagination";
import {useNavigate} from "react-router-dom";

const BookList = () => {

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
                    bookState.books.map((book) => {
                            return <BookItem key={book.id} book={book} isAdmin={isAdminOrCreator(authState)}/>
                        }
                    )
                }
            </ul>
            <Pagination total={bookState.count} setCurrentPage={setCurrentPage} currentPage={currentPage}/>
            <ToastContainer/>
        </div>
    );
}

export default BookList;
