import React, { useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { throwHttpError } from "../helpers/ErrorThrowers";
import { GET_BOOK_URL, UPDATE_BOOK_URL } from "../helpers/Urls";
import { isBookDataValid } from "../helpers/Validators";

const ChangeBook = () => {
    const [info, setInfo] = useState('');
    const [genre, setGenre] = useState('');
    const [author, setAuthor] = useState('');
    const [image, setImage] = useState('');
    const [name, setName] = useState('');
    const [id, setId] = useState(0);

    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(false);

    useEffect(() => {
        const href = window.location.search;
        const params = new URLSearchParams(href);
        const bookId = parseInt(params.get('bookId'));

        if (bookId === null || isNaN(bookId)) {
            setError(true);
            setLoading(false);
            return;
        }

        fetch(GET_BOOK_URL + bookId)
            .then(response => throwHttpError(response))
            .then(response => response.json())
            .then(data => {
                setId(data.id);
                setAuthor(data.author);
                setGenre(data.genre);
                setName(data.name);
                setInfo(data.info);
                setImage(data.image);
                setLoading(false);
            })
            .catch(errorMessage => {
                setError(true);
                setLoading(false);
                alert(errorMessage);
            })
    }, [])

    const onUpdateSuccess = () => {
        window.location.href = "books?id=1";
        alert("Change successfull");
    }

    const onChangeClicked = () => {
        const bookData = {
            id,
            name,
            info,
            author,
            image,
            genre,
        }
        if (!isBookDataValid(bookData)) {
            return;
        }
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(bookData),
        }
        fetch(UPDATE_BOOK_URL, requestOptions)
            .then(response => throwHttpError(response))
            .then(response => response.json())
            .then(() =>
                onUpdateSuccess()
            )
            .catch(error => {
                alert(error.message)
            });
    }

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
    return (
        <Form className="w-50 p-3 mx-auto">
            <h1>Edit Book</h1>

            <div className="my-4">
                <Form.Label>
                    <mark><strong>Name</strong></mark>
                </Form.Label>
                <Form.Control
                    disabled
                    value={name}
                />
                <Form.Text className="text-warning">
                    The name must have minimum 6 letters and maximum 36.
                </Form.Text>
            </div>

            <div className="my-4">
                <Form.Label>
                    <mark><strong>Author</strong></mark>
                </Form.Label>
                <Form.Control
                    onChange={(e) => setAuthor(e.target.value)}
                    value={author}
                />
                <Form.Text className="text-warning">
                    Here you put the author of a book.
                </Form.Text>
            </div>

            <div className="my-4">
                <Form.Label>
                    <mark><strong>Genres</strong></mark>
                </Form.Label>
                <Form.Control
                    onChange={(e) => setGenre(e.target.value)}
                    value={genre}
                />
                <Form.Text className="text-warning">
                    Here you put at least one genre name.
                </Form.Text>
            </div>

            <div className="my-4">
                <Form.Label>
                    <mark><strong>Description</strong></mark>
                </Form.Label>
                <Form.Control
                    onChange={(e) => setInfo(e.target.value)}
                    value={info}
                    as="textarea"
                    rows={4}
                />
                <Form.Text className="text-warning">
                    Your textarea must be 10-400 characters long.
                </Form.Text>
            </div>

            <div className="my-4">
                <Form.Label>
                    <mark><strong>Book image url</strong></mark>
                </Form.Label>
                <Form.Control
                    onChange={(e) => setImage(e.target.value)}
                    value={image}
                />
                <Form.Text className="text-warning">
                    Enter image url.
                </Form.Text>
            </div>

            <Button className="w-100" onClick={onChangeClicked}>Change</Button>
        </Form>
    )
}

export default ChangeBook;
