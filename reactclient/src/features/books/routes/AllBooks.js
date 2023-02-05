import React, {useEffect} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {deleteBook, getBooks} from "../store/effects";
import {Link} from "react-router-dom";
import {isAdmin} from "../../auth/store/helpers";
import {isBookListEmpty, isFetched, isFetching} from "../store/helpers";
import {ToastContainer} from "react-toastify";
import notify from "../../../notifier";

const AllBooks = () => {

    const dispatch = useDispatch();
    const bookState = useSelector(state => state.book);
    const authState = useSelector(state => state.auth);

    useEffect(() => {
        dispatch(getBooks())
    }, [bookState.changed]);


    const onBookDelete = ({id, name}) => {
        if (window.confirm(`Do you really want to delete ${name}?`)) {
            dispatch(deleteBook(id));
        }
    }

    if (isFetching(bookState)) return (<h1>Loading...</h1>)

    if (!isFetched(bookState)) {
        notify("Something went wrong.", "warning");
    }


    if (isBookListEmpty(bookState)) {
        notify("You've seen all books.", "warning");
    }

    return (
        <div>
            <h1>Books</h1>
            <ul className="list-group list-group-horizontal-md">
                {
                    bookState.books.map((book) =>
                        (
                            <li key={book.id} className="list-group-item">
                                <div>
                                    <Link to={`/book/${book.id}`}>
                                        <img src={book.image} alt={book.name} height={500} width={350} />
                                        <p className="text-center text-secondary">{book.name}</p>
                                    </Link>
                                    {isAdmin(authState) &&
                                        <>
                                            <Link to={`/update/${book.id}`}>
                                                <button className='w-50 my-2 btn btn-secondary'>Change</button>
                                            </Link>
                                            <button className='w-50 my-2 btn btn-secondary'
                                                    onClick={() => onBookDelete(book)}>
                                                Delete</button>
                                        </>}
                                </div>
                            </li>
                        ))
                }
            </ul>
            <ToastContainer/>
        </div>
    );
}

export default AllBooks;
