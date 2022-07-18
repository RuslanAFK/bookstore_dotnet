import React, { Component } from 'react';
import { Image } from "react-bootstrap";

const server_url = 'https://localhost:7180/get-book/';

class BookView extends Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            info: '',
            genre: '',
            image: '',
            author: '',
            loading: true,
            found: true,
        }
    }

    ifValid(data) {
        return data.name != null;
    }

    componentDidMount = () => {
        this.getBookDetails();
    }

    getBookDetails() {
        const href = window.location.search;
        const params = new URLSearchParams(href);
        const bookId = params.get('bookId');
        fetch(server_url + bookId).then((response) => response.json())
            .then((data) => this.ifValid(data) ? (
                this.setState({
                    name: data.name,
                    info: data.info,
                    genre: data.genre,
                    image: data.image,
                    author: data.author,
                    loading: false,
                })) : this.setState({
                    found: false,
                    loading: false,
                }));
    }



    render() {
        if(this.state.loading){
            return(
                <h1>Loading...</h1>
            )
        }
        else if(!this.state.found){
            return(
                <h1>Error 404: NOT FOUND.</h1>
            )
        }
        else return (
            <div>
                <Image src={this.state.image} alt="Image" width={350} height={500} />
                <h1>Book {this.state.name}</h1>
                <p>Author: {this.state.author}</p>
                <p>Tags: {this.state.genre}</p>
                <p>Description: {this.state.info}</p>
            </div>
        );
    }
}

export default BookView;
