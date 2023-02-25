import React, {useEffect, useState} from 'react';
import {getBook} from "../store/effects";
import {useNavigate, useParams} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";
import {BookViewService} from "../services/book-view.service";
import {API_URL} from "../../shared/store/urls";
import {AppDispatch, RootState} from "../../shared/store/store";
import Spinner from "../../shared/components/spinners/Spinner";

const BookView = () => {
    const params = useParams();
    const dispatch = useDispatch<AppDispatch>();

    const [book, setBook] = useState<any>();
    const bookState = useSelector((state: RootState) => state.book);

    const navigate = useNavigate();


    useEffect(() => {
        const bookId = BookViewService.getBookIdFromParams(params, navigate);
        dispatch(getBook(bookId));
    }, [params.id]);


    useEffect(() => {
        setBook(bookState.books[0])
    }, [bookState.fetched]);

    useEffect(() => {
        if (bookState.error)
            navigate("/");
        }, [bookState.error]);

    if (!book) {
        return <h1>Loading...</h1>
    }
    const {name,image,author,info,genre,bookFile} = book;

    return (
        <div>
            {bookState.fetching ? <Spinner/>:
                <div>
                    <h1 className='mx-3 my-3'>Book {name}</h1>
                    <img className='mx-3 my-3' src={image} alt="Image" width={350} height={500} />
                    {bookFile &&
                        <a href={API_URL+'uploads/'+bookFile} target="_blank">Go to Book PDF</a>}
                    <p className='mx-3 my-2'>Author: {author}</p>
                    <p className='mx-3 my-2'>Tags: {genre}</p>
                    <p className='mx-3 my-2'>Description: {info}</p>
                </div>
            }
        </div>
    );
}

export default BookView;
