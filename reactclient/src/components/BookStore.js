import React, { useState, useEffect } from 'react';
import Books from "./Books";
import { Button } from "react-bootstrap";
import { GET_BOOKS_URL, GET_USER_URL } from '../helpers/Urls';
import { throwHttpError } from '../helpers/ErrorThrowers';

const BookStore = () => {
    const [books, setBooks] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(false);
    const [isEmpty, setIsEmpty] = useState(false);
    const [isAdmin, setIsAdmin] = useState(false);

    const ifEmpty = (data) => {
        return data.length < 1;
    }

    useEffect(() => {
        const href = window.location.search;
        const params = new URLSearchParams(href)
        const userId = parseInt(params.get('id'));

        if (userId === null || isNaN(userId)) {
            setError(true);
            return;
        }

        fetch(GET_USER_URL + userId)
            .then(response => throwHttpError(response))
            .then(response => response.json())
            .then(
                data => setIsAdmin(data.isAdmin)
            )
            .catch(() => setError(true))

        setLoading(true);

        fetch(GET_BOOKS_URL)
            .then(response => throwHttpError(response))
            .then(response => response.json())
            .then(
                data => {
                    if (ifEmpty(data)) {
                        setIsEmpty(true);
                    }
                    else {
                        setBooks(data);
                    }
                    setLoading(false);
                }
            )
            .catch(error => {
                setError(true);
                setLoading(false);
            })
    }, []);

    if (loading) {
        return (
            <h1 className='text-center my-5'>Loading...</h1>
        )
    }
    else if (error) {
        return (
            <h1 className='text-center my-5'>Error 404: NOT FOUND.</h1>
        )
    }
    else if (isEmpty) {
        return (
            <div>
                <h1>No books found.</h1>
                {isAdmin && <Button href='add'>Add Book</Button>}
            </div>
        )
    }
    return (
        <div>
            <div className="d-flex mx-3 my-3">
                <h1 className='mx-2 my-2'>Books</h1>
                {isAdmin && <Button className='mx-2 my-3' href='add'>Add Book</Button>}
            </div>
            <Books books={books} isAdmin={isAdmin} />
        </div>
    );
}

export default BookStore;
