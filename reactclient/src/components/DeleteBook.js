import React from "react";
import { Button } from "react-bootstrap";

const delete_url = 'https://localhost:7180/delete-book/';
const get_url = 'https://localhost:7180/get-book/';


export default class DeleteBook extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            name: '',
            loading: true,
            found: true,
        };
    }


    handleDelete = () => {
        const requestOptions = {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' },
        }
        fetch(delete_url + this.state.id, requestOptions).then((response) =>
            response.json()
        ).then(data => {
            alert(data);
            window.location.href = "books?id=1";
        }
        );
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
        const bookId = parseInt(params.get('bookId'));

        if (bookId === null || isNaN(bookId)) {
            this.setState({
                found: false,
                loading: false,
            })
            return;
        }

        fetch(get_url + bookId).then((response) => response.json())
            .then((data) => this.ifValid(data) ? (
                this.setState({
                    id: data.id,
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
        if (this.state.loading) {
            return (
                <h1 className='text-center my-5'>Loading...</h1>
            )
        }
        else if (!this.state.found) {
            return (
                <h1 className='text-center my-5'>Error 404: NOT FOUND.</h1>
            )
        }
        else return (
            <div className="mx-3 my-3">
                <h1 className="my-3">Do you want to delete <mark>{this.state.name}</mark>?</h1>
                <Button className="w-25 my-2" onClick={this.handleDelete}>Yes</Button>
                <Button className="w-25 my-2" href="books?id=1">No</Button>
            </div>
        );
    }
}
