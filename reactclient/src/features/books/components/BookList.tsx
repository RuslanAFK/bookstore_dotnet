import React, {useEffect, useState} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {getBooks} from "../store/effects";
import {isAdminOrCreator} from "../../auth/store/selectors";
import BookItem from "./BookItem";
import Pagination from "../../shared/components/pagination/Pagination";
import Search from "../../shared/components/search/Search";
import {AppDispatch, RootState} from "../../shared/store/store";
import QueryObject from "../../shared/interfaces/QueryObject";
import Spinner from "../../shared/components/spinners/Spinner";

const BookList = () => {

    const dispatch = useDispatch<AppDispatch>();
    const bookState = useSelector((state: RootState) => state.book);
    const authState = useSelector((state: RootState) => state.auth);

    const [currentPage, setCurrentPage] = useState(1);
    const [search, setSearch] = useState('');

    useEffect(() => {
        const input: QueryObject = {page: currentPage, search};
        dispatch(getBooks(input));
    }, [currentPage, search]);


    const isListEmpty = () => bookState.books.length === 0;

    return (
        <div>
            <h1 className="text-center">Books</h1>
            <Search search={search} setSearch={setSearch}/>

            {bookState.fetching ? <Spinner/>:
            <div>
                {isListEmpty() ?
                    <h2 className="text-center">You've seen all books.</h2> :
                    <ul className="list-group list-group-horizontal-md">
                        {
                            bookState.books.map((book) =>
                                <BookItem key={book.id} book={book} isAdmin={isAdminOrCreator(authState)}
                                    changing={bookState.changing} />)
                        }
                    </ul>}
                <Pagination total={bookState.count} setCurrentPage={setCurrentPage} currentPage={currentPage}/>
            </div>
            }
        </div>
    );
}

export default BookList;
