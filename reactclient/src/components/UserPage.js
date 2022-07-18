import React, { Component } from 'react';
import { Button, ButtonGroup, Form } from "react-bootstrap";

const get_user_url = 'https://localhost:7180/get-user/';
const get_books_url = 'https://localhost:7180/get-all-books/';

export default class UserPage extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            isAdmin: false,
            search: '',
        }
        this.handleSubmit = this.handleSubmit.bind(this);
        this.handleSearchChange = this.handleSearchChange.bind(this);
    }


    ifValid(data) {
        return data.username != null;
    }

    raiseErr(data) {
        window.location.href = '/';
    }


    componentWillMount() {
        const params = new URLSearchParams(window.location.search);
        const userId = params.get('id');
        fetch(get_user_url + userId, { method: 'GET' }).then((response) => response.json())
            .then((data) => this.ifValid(data) ? (
                this.setState({
                    username: data.username,
                    isAdmin: data.isAdmin,
                })) : this.raiseErr(data));
    }

    handleSearchChange = (e) => {
        this.setState({
            search: e.target.value,
        });
    }

    handleSubmit = () => {
        window.location.href = '/books?search=' + this.state.search;
    }

    render() {
        return (
            <div>
                
            </div>
        );
    }
}

