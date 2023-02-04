import React, {useEffect, useState} from "react";
import Input from "../../../components/Input";
import {useDispatch, useSelector} from "react-redux";
import {createBook, getBook, updateBook} from "../store/thunks";
import {useParams} from "react-router-dom";

const LoadBook = ({isUpdate}) => {
    const [info, setInfo] = useState('');
    const [genre, setGenre] = useState('');
    const [author, setAuthor] = useState('');
    const [image, setImage] = useState('');
    const [name, setName] = useState('');

    const [id, setId] = useState(undefined);

    const dispatch = useDispatch();
    const params = useParams();

    const bookSelector = useSelector(state => state.book);

    useEffect(() => {
        if (!isUpdate)
            return;

        const bookId = parseInt(params.id);
        if (bookId === null || isNaN(bookId)) {
            // Throw error
            return;
        }
        setId(bookId);
        dispatch(getBook(bookId));
    }, [params.id, dispatch])

    useEffect(() => {
        const book = bookSelector.books[0];
        if (book && isUpdate) {
            setName(book.name);
            setImage(book.image);
            setGenre(book.genre);
            setAuthor(book.author);
            setInfo(book.info);
        }
    }, [bookSelector.books])

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
        else
            dispatch(createBook(bookData));
    }

    return (
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
    )
}

export default LoadBook;
