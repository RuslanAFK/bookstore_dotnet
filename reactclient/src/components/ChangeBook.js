import React from "react";
import { Button, Form } from "react-bootstrap";

const update_url = 'https://localhost:7180/update-book';
const get_url = 'https://localhost:7180/get-book/';

export default class ChangeBook extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            name: '',
            genre: '',
            info: '',
            image: '',
            author: '',
            loading: true,
            error: false,
        };
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


    ifError = (data) => {
        return data === "Not found.";
    }

    componentDidMount = () => {
        const href = window.location.search;
        const params = new URLSearchParams(href);
        const bookId = parseInt(params.get('bookId'));

        if (bookId === null || isNaN(bookId)) {
            this.setState({
                error: true,
                loading: false,
            })
            return;
        }

        fetch(get_url + bookId)
            .then(response => response.json())
            .then(data =>
                this.ifError(data) ? this.setState({ error: true, loading: false })
                    : this.setState({
                        id: data.id,
                        name: data.name,
                        genre: data.genre,
                        info: data.info,
                        image: data.image,
                        author: data.author,
                        loading: false
                    })
            )
    }

    validatedData = () => {
        const name = this.state.name;
        const info = this.state.info;
        const author = this.state.author;
        const image = this.state.image;
        const genre = this.state.genre;

        if (name.length < 6 || name.length > 36) {
            document.getElementById('u_name').setCustomValidity("Name must be from 6 to 36 symbols.");
            document.getElementById('u_name').reportValidity();
            return false;
        }
        else if (genre.length < 6 || genre.length > 36) {
            document.getElementById('u_genre').setCustomValidity("Genres must be from 6 to 36 symbols.");
            document.getElementById('u_genre').reportValidity();
            return false;
        }
        else if (author.length < 6 || author.length > 36) {
            document.getElementById('u_author').setCustomValidity("Author name must be from 6 to 36 symbols.");
            document.getElementById('u_author').reportValidity();
            return false;
        }
        else if (image.length < 10 || image.length > 1000) {
            document.getElementById('u_image').setCustomValidity("Image url must be from 10 to 1000 symbols.");
            document.getElementById('u_image').reportValidity();
            return false;
        }
        else if (info.length < 10 || info.length > 400) {
            document.getElementById('u_textarea').setCustomValidity("Description must be from 10 to 400 symbols.");
            document.getElementById('u_textarea').reportValidity();
            return false;
        }
        return true;
    }

    ifErrorUpdating = (data) => {
        return data !== "Update Successful";
    }
    catchUpdateError = (message) => {
        document.getElementById('u_name').setCustomValidity(message);
        document.getElementById('u_name').reportValidity();
    }
    onUpdateSuccess = (message) => {
        window.location.href = "books?id=1";
        alert(message);
    }

    onChangeClicked = () => {
        if (!this.validatedData()) {
            return;
        }
        const requestOptions = {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                id: this.state.id,
                name: this.state.name,
                info: this.state.info,
                author: this.state.author,
                image: this.state.image,
                genre: this.state.genre,
            }),
        }
        fetch(update_url, requestOptions)
            .then(response => response.json())
            .then(data =>
                this.ifErrorUpdating(data) ? this.catchUpdateError(data)
                    : this.onUpdateSuccess(data)
            );
    }

    render() {
        if (this.state.loading) {
            return (
                <h1 className='text-center my-5'>Loading...</h1>
            )
        }
        else if (this.state.error) {
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
                        value={this.state.name}
                        id="u_name"
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
                        value={this.state.author}
                        id="u_author"
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
                        id="u_genre"
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
                        id="u_textarea"
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
                        id="u_image"
                        value={this.state.image}
                    />
                    <Form.Text className="text-warning">
                        Enter image url.
                    </Form.Text>
                </div>

                <Button className="w-100" onClick={this.onChangeClicked}>Change</Button>
            </Form>
        )
    }
}
