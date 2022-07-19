import React, { Component } from 'react';
import Books from "./Books";
import { Button } from "react-bootstrap";

const get_books_url = 'https://localhost:7180/get-all-books';
const get_user_url = 'https://localhost:7180/get-user/';


export default class BookStore extends Component {

    constructor(props) {
        super(props);
        this.state = {
            books: [],
            loading: false,
            error: false,
            empty: false,
            isAdmin: false,
        }
    }
    ifErrorUser = (data) => {
        return data.username == null;
    }
    ifErrorBooks = (data) => {
        return data.length === null;
    }
    ifEmpty = (data) => {
        return data.length < 1;
    }
    componentDidMount = () => {
        const href = window.location.search;
        const params = new URLSearchParams(href)
        const userId = parseInt(params.get('id'));

        if (userId === null || isNaN(userId)) {
            this.setState({ error: true });
            return;
        }

        fetch(get_user_url + userId)
            .then(response => response.json())
            .then(
                data => this.ifErrorUser(data) ? this.setState({ error: true })
                    : this.setState({ isAdmin: data.isAdmin })
            );

        this.setState({ loading: true });

        fetch(get_books_url)
            .then(response => response.json())
            .then(
                data => this.ifErrorBooks(data) ? this.setState({ error: true, loading: false })
                    : this.ifEmpty(data) ? this.setState({ empty: true, loading: false })
                        : this.setState({ books: data, loading: false })
            );
    }


    render = () => {
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
        else if (this.state.empty) {
            return (
                <div>
                    <h1>No books found.</h1>
                    {this.state.isAdmin && <Button href='add'>Add Book</Button>}
                </div>
            )
        }
        return (
            <div>
                <div className="d-flex mx-3 my-3">
                    <h1 className='mx-2 my-2'>Books</h1>
                    {this.state.isAdmin && <Button className='mx-2 my-3' href='add'>Add Book</Button>}
                </div>
                <Books books={this.state.books} isAdmin={this.state.isAdmin} />
            </div>
        );
    }

}
