import React, { useEffect, useState } from 'react';
import { Image } from "react-bootstrap";
import { throwHttpError } from '../helpers/ErrorThrowers';
import { GET_BOOK_URL } from '../helpers/Urls';

const BookView = () => {
    const [user, setUser] = useState(null);

    const [loading, setLoading] = useState(true);
    const [found, setFound] = useState(true);

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
                    name: data.name,
                    info: data.info,
                    genre: data.genre,
                    image: data.image,
                    author: data.author,
                })
                setLoading(false);
            })
            .catch(() => {
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
        <div>
            <h1 className='mx-3 my-3'>Book {user.name}</h1>
            <Image className='mx-3 my-3' src={user.image} alt="Image" width={350} height={500} />
            <p className='mx-3 my-2'>Author: {user.author}</p>
            <p className='mx-3 my-2'>Tags: {user.genre}</p>
            <p className='mx-3 my-2'>Description: {user.info}</p>
        </div>
    );
}

export default BookView;
