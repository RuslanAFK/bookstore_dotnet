import React from "react";
import { Button, Form } from "react-bootstrap";

const update_url = 'https://localhost:7180/update-book/';
const get_url = 'https://localhost:7180/get-book/';

class ChangeBook extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            genre: '',
            info: '',
            image: '',
            author: '',
            loading: true,
            error: false,
        };
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleGenreChange = this.handleGenreChange.bind(this);
        this.handleInfoChange = this.handleInfoChange.bind(this);
        this.handleImageChange = this.handleImageChange.bind(this);
        this.handleAuthorChange = this.handleAuthorChange.bind(this);
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


    ifValid(data) {
        return !data.error;
    }

    login(data) {
        alert("The book " + data.name + " is successfully edited!");
    }

    raiseErr(data) {
        let errText;
        if (data.error.name) {
            errText = data.error.name[0];
            document.getElementById('c_name').setCustomValidity(errText);
            document.getElementById('c_name').reportValidity();
        } else if (data.error.image) {
            errText = data.error.image[0];
            document.getElementById('c_image').setCustomValidity(errText);
            document.getElementById('c_image').reportValidity();
        } else {
            console.log(data);
        }
    }

    ifError = (data) => {
        return data.name === null;
    }

    componentDidMount = () => {
        const href = window.location.search;
        const params = new URLSearchParams(href);
        const bookId = params.get('userId');
        fetch(get_url + bookId)
            .then(response => response.json())
            .then(data =>
                this.ifError(data) ? this.setState({ error: true })
                    : this.setState({
                        name: data.name,
                        genre: data.genre,
                        info: data.info,
                        image: data.image,
                        author: data.image,
                        loading: false
                    })
            )
    }
    handleSubmit = () => {

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                name: this.state.name,
                image: this.state.image,
            })
        }
        fetch(update_url, requestOptions).then((response) =>
            response.json()
        ).then((data) => this.ifValid(data) ? this.login(data) : this.raiseErr(data));
    }


    render() {
        return (
            <Form className="rl_form">

                <h1>Edit Book</h1>
                <Form.Label>Name</Form.Label>
                <Form.Control
                    onChange={this.handleNameChange}
                    value={this.state.name}
                    id="u_name"
                />
                <Form.Text className="text-warning">
                    The name must have minimum 3 letters and maximum 40.
                </Form.Text>
                <br />

                <Form.Label>Author</Form.Label>
                <Form.Control
                    onChange={this.handleAuthorChange}
                    value={this.state.author}
                    id="u_author"
                    required
                />
                <Form.Text className="text-warning">
                    Here you put at least one genre name, separator is a whitespace.
                </Form.Text>
                <br />


                <Form.Label>Genres</Form.Label>
                <Form.Control
                    onChange={this.handleGenreChange}
                    value={this.state.genre}
                    id="u_genre"
                />
                <Form.Text className="text-warning">
                    Here you put at least one genre name, separator is a whitespace.
                </Form.Text>
                <br />

                <Form.Label>Description</Form.Label>

                <Form.Control
                    onChange={this.handleInfoChange}
                    value={this.state.info}
                    as="textarea"
                    rows={4}
                    id="utextarea"
                    minLength={100} maxLength={200}
                    required
                />
                <Form.Text className="text-warning">
                    Your textarea must be 100-200 characters long.
                </Form.Text>
                <br />

                <Form.Label>Choose book image</Form.Label>

                <Form.Control
                    onChange={this.handleImageChange}
                    id="u_image"
                    value={this.state.image}
                />
                <Form.Text className="text-warning">
                    Enter image url.
                </Form.Text>
                <br />


            </Form>
        )
    }
}


export default ChangeBook;
