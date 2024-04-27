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
import {applyChanges, clearError} from "../store/book-slice";

const BookView = () => {
    const params = useParams();
    const dispatch = useDispatch<AppDispatch>();

    const [book, setBook] = useState<any>();
    const [isBookOpened, setBookOpened] = useState(false);
    const bookState = useSelector((state: RootState) => state.book);

    const navigate = useNavigate();


    useEffect(() => {
        const bookId = BookViewService.getBookIdFromParams(params, navigate);
        dispatch(getBook(bookId));
        setBookOpened(false);
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
                    <div className="container">
                        <div className="row">
                            <div className="col-6">
                                <MainLabel text={name}/>
                                <h6>Author: {author}</h6>
                                <h6>Tags: {genre}</h6>
                                <h6>Description: {info}</h6>

                                <div>
                                    {bookFile && !isBookOpened &&
                                        <div>
                                            <h6>File actions:</h6>
                                            <div className="link-primary" onClick={() => setBookOpened(true)}>
                                                Open
                                            </div>
                                        </div>
                                    }
                                </div>
                            </div>
                            <div className="col-6">
                                {isBookOpened ?
                                    <embed
                                        src={API_URL + 'uploads/' + bookFile}
                                        width="100%"
                                        height="600px"
                                        className="my-2"
                                    ></embed> :
                                    <img className='bookViewImage my-2' src={image} alt={name}/>
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
