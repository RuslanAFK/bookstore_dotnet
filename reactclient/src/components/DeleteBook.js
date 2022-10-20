import React, { useEffect, useState } from "react";
import { Button } from "react-bootstrap";
import { throwHttpError } from "../helpers/ErrorThrowers";
import { DELETE_BOOK_URL, GET_BOOK_URL } from "../helpers/Urls";

const DeleteBook = () => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);
    const [found, setFound] = useState(true);

    const handleDelete = () => {
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
        }
        fetch(DELETE_BOOK_URL + user.id, requestOptions)
            .then(response => throwHttpError(response))
            .then((response) => response.json())
            .then((data) => {
                alert(`Book ${data.name} deleted`);
                window.location.href = "books?id=1";
            })
            .catch(errorMessage => alert(errorMessage))
    }

    useEffect(() => {
        getBookDetails();
    }, [])

    const getBookDetails = () => {
        const href = window.location.search;
        const params = new URLSearchParams(href);
        const bookId = parseInt(params.get('bookId'));

        if (bookId === null || isNaN(bookId)) {
            setFound(false);
            setLoading(false);
            return;
        }

        fetch(GET_BOOK_URL + bookId)
            .then(response => throwHttpError(response))
            .then((response) => response.json())
            .then((data) => {
                setUser({
                    id: data.id,
                    name: data.name,
                    info: data.info,
                    genre: data.genre,
                    image: data.image,
                    author: data.author,
                });
                setLoading(false);
            })
            .catch(error => {
                setFound(false);
                setLoading(false);
            })
    }

    if (loading) {
        return (
            <h1 className='text-center my-5'>Loading...</h1>
        )
    }
    else if (!found) {
        return (
            <h1 className='text-center my-5'>Error 404: NOT FOUND.</h1>
        )
    }
    else return (
        <div className="mx-3 my-3">
            <h1 className="my-3">Do you want to delete <mark>{user.name}</mark>?</h1>
            <Button className="w-25 my-2" onClick={handleDelete}>Yes</Button>
            <Button className="w-25 my-2" href="books?id=1">No</Button>
        </div>
    );
}

export default DeleteBook;