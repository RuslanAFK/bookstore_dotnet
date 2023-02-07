import React, {useEffect, useState} from 'react';
import {getBook} from "../store/effects";
import {useNavigate, useParams} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";

const BookView = () => {
    const params = useParams();
    const dispatch = useDispatch();

    const [book, setBook] = useState(undefined);
    const bookState = useSelector(state => state.book);
    const navigate = useNavigate();


    useEffect(() => {
        const bookId = parseInt(params.id);
        if (bookId === null || isNaN(bookId)) {
            navigate("/");
            return;
        }
        dispatch(getBook(bookId));
    }, [params.id, bookState.changed]);

    useEffect(() => {
        setBook(bookState.books[0])
    }, [bookState.fetched]);

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
