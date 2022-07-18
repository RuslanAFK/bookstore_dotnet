import React from "react";
import {Button, Form} from "react-bootstrap";

const server_url = 'https://localhost:7180/create-book';

class LoadBook extends React.Component
{
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            genre: '',
            info: '',
            image: '',
            author: '',
        };
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleGenreChange = this.handleGenreChange.bind(this);
        this.handleInfoChange = this.handleInfoChange.bind(this);
        this.handleImageChange = this.handleImageChange.bind(this);
        this.handleAuthorChange = this.handleAuthorChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleNameChange = (e) => {
        this.setState({
            name: e.target.value,
        });
    }

    handleGenreChange= (e) => {
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
        alert("The book "+data.name+" is successfully uploaded!");
    }

    raiseErr(data) {
        let errText;
        if(data.error.name){
            errText = data.error.name[0];
            document.getElementById('r_name').setCustomValidity(errText);
            document.getElementById('r_name').reportValidity();
        }else if(data.error.genre){
            errText = data.error.genre[0];
            document.getElementById('r_genre').setCustomValidity(errText);
            document.getElementById('r_genre').reportValidity();
        }else if(data.error.image){
            errText = data.error.image[0];
            document.getElementById('r_image').setCustomValidity(errText);
            document.getElementById('r_image').reportValidity();
        }else if(data.error.info){
            errText = data.error.info[0];
            document.getElementById('textarea').setCustomValidity(errText);
            document.getElementById('textarea').reportValidity();
        }else if(data.error.author){
            errText = data.error.author[0];
            document.getElementById('r_author').setCustomValidity(errText);
            document.getElementById('r_author').reportValidity();
        }else {
            console.log(data);
        }
    }

    handleSubmit = (e) => {
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                name: this.state.name,
                genre: this.state.genre,
                info: this.state.info,
                image: this.state.image,
                author: this.state.author
            }),
        }
        fetch(server_url, requestOptions).then((response)=>
            response.json()
        ).then((data)=>this.ifValid(data)?this.login(data):this.raiseErr(data));
    }


    render(){
        return(
            <Form className="rl_form">

                <h1>Upload Book</h1>
                <Form.Label>Name</Form.Label>
                <Form.Control
                    onChange={this.handleNameChange}
                    placeHolder="Cinderella in the woods"
                    id="r_name"
                    required
                />
                <Form.Text className="text-warning">
                    The name must have minimum 3 letters and maximum 40.
                </Form.Text>
                <br/>

                <Form.Label>Author</Form.Label>
                <Form.Control
                    onChange={this.handleAuthorChange}
                    placeHolder="Mark Twain"
                    id="r_author"
                    required
                />
                <Form.Text className="text-warning">
                    Here you put at least one genre name, separator is a whitespace.
                </Form.Text>
                <br/>


                <Form.Label>Genres</Form.Label>
                <Form.Control
                    onChange={this.handleGenreChange}
                    placeHolder="action horror fantazy"
                    id="r_genre"
                    required
                />
                <Form.Text className="text-warning">
                    Here you put at least one genre name, separator is a whitespace.
                </Form.Text>
                <br/>

                <Form.Label>Description</Form.Label>

                <Form.Control
                    onChange={this.handleInfoChange}
                    placeHolder="I am John Rockefeller and i'm playing chess for 5 years now..."
                    as="textarea"
                    rows={4}
                    id="textarea"
                    minLength={100} maxLength={200}
                    required
                />
                <Form.Text className="text-warning">
                    Your textarea must be 100-200 characters long.
                </Form.Text>
                <br/>

                <Form.Label>Choose book image</Form.Label>

                <Form.Control
                    onChange={this.handleImageChange}
                    id="r_image"
                    placeholder="https://media.gettyimages.com/photos/stack-of-books-picture-id157482029?s=612x612"
                    aria-required
                />
                <Form.Text className="text-warning">
                    Enter image url.
                </Form.Text>
                <br/>

                <Button className="one_but" onClick={this.handleSubmit}>Upload</Button>
            </Form>
        )
    }
}


export default LoadBook;
