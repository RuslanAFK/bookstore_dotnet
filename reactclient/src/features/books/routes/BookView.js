import React, {useEffect, useState} from 'react';
import { Image } from "react-bootstrap";
import {getBook} from "../store/thunks";
import {useParams} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

const BookView = () => {
    const params = useParams();
    const dispatch = useDispatch();

    const [book, setBook] = useState(undefined);
    const {books} = useSelector(state => state.book);

    useEffect(() => {
        const bookId = parseInt(params.id);
        if (bookId === null || isNaN(bookId)) {
            // Throw error
            return;
        }
        dispatch(getBook(bookId));
    }, [params.id, dispatch]);

    useEffect(() => {
        setBook(books[0])
    }, [books]);

    if (!book) {
        return <h1>Loading...</h1>
    }
    const {name,image,author,info,genre} = book;

    return (
        <div>
            <h1 className='mx-3 my-3'>Book {name}</h1>
            <img className='mx-3 my-3' src={image} alt="Image" width={350} height={500} />
            <p className='mx-3 my-2'>Author: {author}</p>
            <p className='mx-3 my-2'>Tags: {genre}</p>
            <p className='mx-3 my-2'>Description: {info}</p>
        </div>
    );
}

export default BookView;
