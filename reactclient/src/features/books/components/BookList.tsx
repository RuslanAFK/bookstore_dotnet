import React, {useEffect, useState} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {getBooks} from "../store/effects";
import {isAdminOrCreator} from "../../auth/store/selectors";
import BookItem from "./BookItem";
import Pagination from "../../shared/components/Pagination";
import Search from "../../shared/components/Search";
import {AppDispatch, RootState} from "../../shared/store/store";
import QueryObject from "../../shared/interfaces/QueryObject";
import Spinner from "../../shared/components/Spinner";
import MainLabel from "../../shared/components/MainLabel";

import "../stylesheets/BookList.css";
import {applyChanges} from "../store/book-slice";

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

    useEffect(() => {
        if (bookState.changed) {
            const input: QueryObject = {page: currentPage, search};
            dispatch(applyChanges());
            dispatch(getBooks(input));
        }
    }, [bookState.changed]);

    const isListEmpty = () => bookState.books.length === 0;

    return (
        <div>
            <MainLabel text="Books"/>
            <Search search={search} setSearch={setSearch}/>

            {bookState.fetching ? <Spinner/>:
            <div>
                {isListEmpty() ?
                    <h2 className="text-center">You've seen all books.</h2> :
                    <ul className="bookListBox">
                        {
                            bookState.books.map((book) =>
                                <div key={book.id}>
                                    <BookItem book={book} isAdmin={isAdminOrCreator(authState)}
                                              changing={bookState.changing} />
                                </div>
                                )
                        }
                    </ul>}
                <Pagination total={bookState.count} setCurrentPage={setCurrentPage} currentPage={currentPage}/>
            </div>
            }
        </div>
    );
}

export default BookList;
