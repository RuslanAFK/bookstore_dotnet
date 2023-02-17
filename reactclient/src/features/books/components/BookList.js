import React, {useEffect, useState} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {getBooks} from "../store/effects";
import {isAdminOrCreator} from "../../auth/store/selectors";
import BookItem from "./BookItem";
import Pagination from "../../../components/Pagination";
import Search from "../../../components/Search";
import HubConnector from "../../../hub-connector";

const BookList = () => {

    const dispatch = useDispatch();
    const bookState = useSelector(state => state.book);
    const authState = useSelector(state => state.auth);

    const {subscribe, unsubscribe} = HubConnector();

    const [currentPage, setCurrentPage] = useState(1);
    const [search, setSearch] = useState('');

    useEffect(() => {
        const input = {page: currentPage, search};
        dispatch(getBooks(input));
    }, [currentPage, search]);

    useEffect(() => {
            subscribe(() => {
                const input = {page: currentPage, search};
                dispatch(getBooks(input));
            })
            return () => unsubscribe();
    }, []);


    const isListEmpty = () => bookState.books.length === 0;


    return (
        <div>
            <h1 className="text-center">Books</h1>

            <Search search={search} setSearch={setSearch}/>

            {isListEmpty() ?
                <h2 className="text-center">You've seen all books.</h2> :
                <ul className="list-group list-group-horizontal-md">
                {
                    bookState.books.map((book) =>
                        <BookItem key={book.id} book={book} isAdmin={isAdminOrCreator(authState)}/>)
                }
            </ul>}
            <Pagination total={bookState.count} setCurrentPage={setCurrentPage} currentPage={currentPage}/>
        </div>
    );
}

export default BookList;
