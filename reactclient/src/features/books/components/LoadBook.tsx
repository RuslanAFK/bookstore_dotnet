import React, {FormEvent, useEffect, useState} from "react";
import Input from "../../shared/components/Input";
import {useDispatch, useSelector} from "react-redux";
import {createBook, getBook, updateBook} from "../store/effects";
import {useNavigate, useParams} from "react-router-dom";
import {applyChanges, clearError} from "../store/book-slice";
import {ToastContainer} from "react-toastify";
import {notify} from "../../shared/services/toast-notifier";
import {AppDispatch, RootState} from "../../shared/store/store";
import UpdateBook from "../interfaces/UpdateBook";
import CreateBook from "../interfaces/CreateBook";
import Spinner from "../../shared/components/Spinner";
import SpinnerButton from "../../shared/components/SpinnerButton";
import MainLabel from "../../shared/components/MainLabel";
import LoadBookProps from "../component-props/LoadBookProps";

const LoadBook = ({page}: LoadBookProps) => {
    const [info, setInfo] = useState('');
    const [genre, setGenre] = useState('');
    const [author, setAuthor] = useState('');
    const [image, setImage] = useState('');
    const [name, setName] = useState('');

    const [id, setId] = useState<undefined | number>();

    const dispatch = useDispatch<AppDispatch>();
    const params = useParams();
    const navigate = useNavigate();

    const bookState = useSelector((state: RootState) => state.book);

    const renewForm = () => {
        setName('');
        setImage('');
        setGenre('');
        setAuthor('');
        setInfo('');
    }

    const populateForm = (book: any) => {
        const {name, image, genre, author, info} = book;
        if (name && image && genre && author && info) {
            setName(name);
            setImage(image);
            setGenre(genre);
            setAuthor(author);
            setInfo(info);
        }
    }

    useEffect(() => {
        renewForm();

        if (page === "update" && params.id) {
            const bookId = parseInt(params.id);
            if (bookId === null || isNaN(bookId))
                navigate("/");
            setId(bookId);
            dispatch(getBook(bookId));
        }
    }, [params.id, dispatch])

    useEffect(() => {
        const book = bookState.books[0];
        if (page === "update" && book)
            populateForm(book);
    }, [bookState.books])

    useEffect(() => {
        if (bookState.changed) {
            dispatch(applyChanges());
            if (bookState.changed && page === "update") {
                navigate(`/book/${id}`);
            }
            else {
                notify("Successfully added book.", "success");
                renewForm();
            }
        }
    }, [bookState.changed])

    useEffect(() => {
        if (bookState.error) {
            dispatch(clearError());
            notify(bookState.error, "error");
        }
    }, [bookState.error])

    const onUploadClicked = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        let bookData: CreateBook | UpdateBook = {
            name,
            info,
            author,
            image,
            genre,
        }
        if (page === "update" && id) {
            bookData = {...bookData, id}
            dispatch(updateBook(bookData));
        }
        else {
            dispatch(createBook(bookData));
        }
    }

    return (
        <div>
            {bookState.fetching ? <Spinner/>:
                <div>
                    <form className="w-50 p-3 mx-auto" onSubmit={onUploadClicked}>
                        <MainLabel text="Upload Book"/>
                        <Input name="Name" value={name} setter={setName} minLength={3} maxLength={36}
                               text="The name must have minimum 3 and maximum 36 letters."/>
                        <Input name="Author" value={author} setter={setAuthor} minLength={3} maxLength={36}
                               text="The author name must have 3 to 36 letters."/>
                        <Input name="Genres" value={genre} setter={setGenre} minLength={3} maxLength={36}
                               text="Enter genre names divided by commas."/>
                        <Input name="Description" value={info} textarea rows={4}
                               setter={setInfo} minLength={10} maxLength={400}
                               text="Your textarea must be 10-400 characters long."/>
                        <Input name="Book image url" textarea rows={2} value={image} setter={setImage}
                               text="Enter valid image url." type="url"/>
                        {bookState.changing ? <div className="my-3 w-100"><SpinnerButton/></div> :
                            <button className="my-3 w-100 btn btn-primary">Upload</button>
                        }
                    </form>
                </div>}
            <ToastContainer/>
        </div>
    )
}

export default LoadBook;
