import React, { Component, useEffect, useState } from 'react';
import axios from "axios";
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
        const userId = params.get('id');

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
                <h1>Loading...</h1>
            )
        }
        else if (this.state.error) {
            return (
                <h1>Error 404: NOT FOUND.</h1>
            )
        }
        else if (this.state.empty) {
            return (
                <div>
                    <h1>No books found.</h1>
                    {this.state.isAdmin && <Button>Add Book</Button>}
                </div>
            )
        }
        return (
            <div>
                <h1>Books</h1>
                {this.state.isAdmin && <Button>Add Book</Button>}
                <Books books={this.state.books} isAdmin={this.state.isAdmin} />
            </div>
        );
    }

}
