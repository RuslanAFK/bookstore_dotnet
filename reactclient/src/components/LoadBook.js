import React from "react";
import { Button, Form } from "react-bootstrap";

const server_url = 'https://localhost:7180/create-book';

export default class LoadBook extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            genre: '',
            info: '',
            image: '',
            author: '',
        };
    }

    validatedData = () => {
        const name = this.state.name;
        const info = this.state.info;
        const author = this.state.author;
        const image = this.state.image;
        const genre = this.state.genre;

        if (name.length < 6 || name.length > 36) {
            document.getElementById('l_name').setCustomValidity("Name must be from 6 to 36 symbols.");
            document.getElementById('l_name').reportValidity();
            return false;
        }
        else if (genre.length < 6 || genre.length > 36) {
            document.getElementById('l_genre').setCustomValidity("Genres must be from 6 to 36 symbols.");
            document.getElementById('l_genre').reportValidity();
            return false;
        }
        else if (author.length < 6 || author.length > 36) {
            document.getElementById('l_author').setCustomValidity("Author name must be from 6 to 36 symbols.");
            document.getElementById('l_author').reportValidity();
            return false;
        }
        else if (image.length < 10 || image.length > 1000) {
            document.getElementById('l_image').setCustomValidity("Image url must be from 10 to 1000 symbols.");
            document.getElementById('l_image').reportValidity();
            return false;
        }
        else if (info.length < 10 || info.length > 400) {
            document.getElementById('l_textarea').setCustomValidity("Description must be from 10 to 400 symbols.");
            document.getElementById('l_textarea').reportValidity();
            return false;
        }
        return true;
    }

    handleNameChange = (e) => {
        this.setState({
            name: e.target.value,
        });
    }

    handleGenreChange = (e) => {
        this.setState({
            genre: e.target.value,
        });
    }

    handleInfoChange = (e) => {
        this.setState({
            info: e.target.value,
        });
    }

    handleImageChange = (e) => {
        this.setState({
            image: e.target.value,
        });
    }

    handleAuthorChange = (e) => {
        this.setState({
            author: e.target.value,
        });
    }


    raiseErr(data) {
        let errText;
        if (data.error.name) {
            errText = data.error.name[0];
            document.getElementById('r_name').setCustomValidity(errText);
            document.getElementById('r_name').reportValidity();
        } else if (data.error.genre) {
            errText = data.error.genre[0];
            document.getElementById('r_genre').setCustomValidity(errText);
            document.getElementById('r_genre').reportValidity();
        } else if (data.error.image) {
            errText = data.error.image[0];
            document.getElementById('r_image').setCustomValidity(errText);
            document.getElementById('r_image').reportValidity();
        } else if (data.error.info) {
            errText = data.error.info[0];
            document.getElementById('textarea').setCustomValidity(errText);
            document.getElementById('textarea').reportValidity();
        } else if (data.error.author) {
            errText = data.error.author[0];
            document.getElementById('r_author').setCustomValidity(errText);
            document.getElementById('r_author').reportValidity();
        } else {
            console.log(data);
        }
    }

    ifErrorUploading = (data) => {
        return data !== "Create Successful";
    }
    catchUploadError = (message) => {
        document.getElementById('u_name').setCustomValidity(message);
        document.getElementById('u_name').reportValidity();
    }
    onUploadSuccess = (message) => {
        window.location.href = "books?id=1";
        alert(message);
    }

    onUploadClicked = () => {
        if (!this.validatedData()) {
            return;
        }
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                name: this.state.name,
                info: this.state.info,
                author: this.state.author,
                image: this.state.image,
                genre: this.state.genre,
            }),
        }
        fetch(server_url, requestOptions)
            .then(response => response.json())
            .then(data =>
                this.ifErrorUploading(data) ? this.catchUploadError(data)
                    : this.onUploadSuccess(data)
            );
    }

    render() {
        return (
            <Form className="w-50 p-3 mx-auto">
                <h1>Upload Book</h1>

                <div className="my-4">
                    <Form.Label>
                        <mark><strong>Name</strong></mark>
                    </Form.Label>
                    <Form.Control
                        value={this.state.name}
                        id="l_name"
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
                        onChange={this.handleAuthorChange}
                        id="l_author"
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
                        onChange={this.handleGenreChange}
                        value={this.state.genre}
                        id="l_genre"
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
                        onChange={this.handleInfoChange}
                        value={this.state.info}
                        as="textarea"
                        rows={4}
                        id="l_textarea"
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
                        onChange={this.handleImageChange}
                        id="l_image"
                        value={this.state.image}
                    />
                    <Form.Text className="text-warning">
                        Enter image url.
                    </Form.Text>
                </div>
                <Button className="w-100" onClick={this.onUploadClicked}>Upload</Button>
            </Form>
        )
    }
}
