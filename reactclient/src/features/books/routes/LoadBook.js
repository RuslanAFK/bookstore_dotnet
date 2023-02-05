import React, {useEffect, useState} from "react";
import Input from "../../../components/Input";
import {useDispatch, useSelector} from "react-redux";
import {createBook, getBook, updateBook} from "../store/effects";
import {useNavigate, useParams} from "react-router-dom";
import {isChanged} from "../store/helpers";
import {applyChanges} from "../store/bookSlice";
import {ToastContainer} from "react-toastify";
import {hasError} from "../../auth/store/helpers";
import notify from "../../../notifier";

const LoadBook = ({isUpdate}) => {
    const [info, setInfo] = useState('');
    const [genre, setGenre] = useState('');
    const [author, setAuthor] = useState('');
    const [image, setImage] = useState('');
    const [name, setName] = useState('');

    const [id, setId] = useState(undefined);

    const dispatch = useDispatch();
    const params = useParams();
    const navigate = useNavigate();

    const bookState = useSelector(state => state.book);

    const renewForm = () => {
        setName('');
        setImage('');
        setGenre('');
        setAuthor('');
        setInfo('');
    }

    const populateForm = (book) => {
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

        if (!isUpdate)
            return;

        const bookId = parseInt(params.id);
        if (bookId === null || isNaN(bookId)) {
            navigate("/");
            return;
        }
        setId(bookId);
        dispatch(getBook(bookId));
    }, [params.id, dispatch])

    useEffect(() => {
        const book = bookState.books[0];
        if (isUpdate && book)
            populateForm(book);
    }, [bookState.books])

    useEffect(() => {
        if (isChanged(bookState) && isUpdate) {
            dispatch(applyChanges());
            navigate(`/book/${id}`);
        }
        else if (isChanged(bookState) && !isUpdate) {
            dispatch(applyChanges());
            navigate(`/books`);
        }
    }, [bookState.changed])

    useEffect(() => {
        if (hasError(bookState))
            notify(bookState.error, "error");
    }, [bookState.error])

    const onUploadClicked = (e) => {
        e.preventDefault();
        const bookData = {
            name,
            info,
            author,
            image,
            genre,
        }
        if (isUpdate) {
            bookData.id = id;
            dispatch(updateBook(bookData));
        }
        else {
            dispatch(createBook(bookData));
            // Change on backend
            //navigate(`/books`);
        }

    }

    return (
        <>
        <form className="w-50 p-3 mx-auto" onSubmit={onUploadClicked}>
            <h1>Upload Book</h1>
            <Input name="Name" value={name} setter={setName} text="The name must have minimum maximum 36 letters."/>
            <Input name="Author" value={author} setter={setAuthor} text="Here you put the author of a book."/>
            <Input name="Genres" value={genre} setter={setGenre} text="Here you put at least one genre name."/>
            <Input name="Description" value={info} textarea setter={setInfo}
                   text="Your textarea must be 10-400 characters long." rows={4}/>
            <Input name="Book image url" value={image} setter={setImage} text="Enter image url."/>

            <button className="my-3 w-100 btn btn-primary">Upload</button>
        </form>
            <ToastContainer/>
        </>
    )
}

export default LoadBook;
