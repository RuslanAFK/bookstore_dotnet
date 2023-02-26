import React, {useEffect, useState} from 'react';
import {getBook} from "../store/effects";
import {useNavigate, useParams} from "react-router-dom";
import {useDispatch, useSelector} from "react-redux";
import {BookViewService} from "../services/book-view.service";
import {API_URL} from "../../shared/store/urls";
import {AppDispatch, RootState} from "../../shared/store/store";
import Spinner from "../../shared/components/Spinner";
import MainLabel from "../../shared/components/MainLabel";

import "../stylesheets/BookView.css";
import BookManagement from "../../shared/components/BookManagement";
import SpinnerButton from "../../shared/components/SpinnerButton";
import {isAdminOrCreator} from "../../auth/store/selectors";
import {applyChanges, clearError} from "../store/book-slice";

const BookView = () => {
    const params = useParams();
    const dispatch = useDispatch<AppDispatch>();

    const [book, setBook] = useState<any>();
    const bookState = useSelector((state: RootState) => state.book);
    const authState = useSelector((state: RootState) => state.auth);

    const navigate = useNavigate();


    useEffect(() => {
        const bookId = BookViewService.getBookIdFromParams(params, navigate);
        dispatch(getBook(bookId));
    }, [params.id]);


    useEffect(() => {
        setBook(bookState.books[0])
    }, [bookState.fetched]);

    useEffect(() => {
        if (bookState.error) {
            dispatch(clearError());
            navigate("/");
        }
        }, [bookState.error]);


    useEffect(() => {
        if (bookState.changed) {
            const bookId = BookViewService.getBookIdFromParams(params, navigate);
            dispatch(applyChanges());
            dispatch(getBook(bookId));
        }
    }, [bookState.changed]);


    if (!book) {
        return <h1>Loading...</h1>
    }
    const {name,image,author,info,genre,bookFile} = book;

    return (
        <div>
            {bookState.fetching ? <Spinner/>:
                <div>
                    <MainLabel text={name}/>
                    <div className="container">
                        <div className="row">
                            <div className="col-sm">
                                <img className='bookViewImage' src={image} alt={name} />
                            </div>
                            <div className="col-8">

                                <h6>Author: {author}</h6>
                                <h6>Tags: {genre}</h6>
                                <h6>Description: {info}</h6>
                                <div className="text-center my-4">
                                    {bookFile &&
                                        <a href={API_URL+'uploads/'+bookFile} target="_blank">
                                            <img src="/icons/pdf-file.png" alt="Go to File" className="clickableIconInView"/>
                                        </a>
                                    }
                                </div>

                                {isAdminOrCreator(authState) &&
                                    <div className="my-1">
                                        {bookState.changing ?
                                            <div className="w-100">
                                                <SpinnerButton/>
                                            </div> : <BookManagement book={book}/>
                                        }
                                    </div>
                                }
                            </div>
                        </div>
                    </div>
                </div>
            }
        </div>
    );
}

export default BookView;
