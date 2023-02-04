import React, { useEffect } from 'react';
import {useDispatch, useSelector} from "react-redux";
import {deleteBook, getBooks} from "../store/thunks";
import {Link} from "react-router-dom";

const AllBooks = () => {
    const dispatch = useDispatch();
    const {books, loading, success} = useSelector(state => state.book);
    const auth = useSelector(state => state.auth);

    useEffect(() => {
        dispatch(getBooks())
    }, [dispatch]);

    const isUserAdmin = () => {
        return auth.success && auth.user.role === "Admin";
    }

    const onBookDelete = ({id, name}) => {
        if (window.confirm(`Do you really want to delete ${name}?`)) {
            dispatch(deleteBook(id));
        }
    }

    if (loading)
        return (<h1>
            Loading...
        </h1>)

    if (!success)
        return (<h1>
            Yet Nothing Found.
        </h1>)

    if (books.length === 0)
        return (<h1>
            No books at this page.
        </h1>)

    return (
        <div>
            <h1>Books</h1>
            <ul className="list-group list-group-horizontal-md">
                {
                    books.map((book) =>
                        (
                            <li key={book.id} className="list-group-item">
                                <div>
                                    <Link to={`/book/${book.id}`}>
                                        <img src={book.image} alt={book.name} height={500} width={350} />
                                        <p className="text-center text-secondary">{book.name}</p>
                                    </Link>
                                    {isUserAdmin() &&
                                        <>
                                            <Link to={`/load/${book.id}`}>
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
        </div>
    );
}

export default AllBooks;
