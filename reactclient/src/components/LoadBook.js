import React, { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { throwHttpError } from "../helpers/ErrorThrowers";
import { CREATE_BOOK_URL } from "../helpers/Urls";
import { isBookDataValid } from "../helpers/Validators";

const LoadBook = () => {
    const [info, setInfo] = useState('');
    const [genre, setGenre] = useState('');
    const [author, setAuthor] = useState('');
    const [image, setImage] = useState('');
    const [name, setName] = useState('');

    const onUploadSuccess = (message) => {
        window.location.href = "books?id=1";
        alert("Upload successfull");
    }

    const onUploadClicked = () => {
        const bookData = {
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
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(bookData)
        }
        fetch(CREATE_BOOK_URL, requestOptions)
            .then(response => throwHttpError(response))
            .then(response => response.json())
            .then(data => onUploadSuccess(data))
            .catch(errorMessage => alert(errorMessage))
    }

    return (
        <Form className="w-50 p-3 mx-auto">
            <h1>Upload Book</h1>

            <div className="my-4">
                <Form.Label>
                    <mark><strong>Name</strong></mark>
                </Form.Label>
                <Form.Control
                    onChange={(e) => setName(e.target.value)}
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
            <Button className="w-100" onClick={onUploadClicked}>Upload</Button>
        </Form>
    )
}

export default LoadBook;
