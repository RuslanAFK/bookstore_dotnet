import React from "react";
import { Button, Form } from "react-bootstrap";
import {Link} from "react-router-dom";

const server_url = 'https://localhost:7180/signup';

export default class Signup extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            isAdmin: false,
        };
    }

    handleUsernameChange = (e) => {
        this.setState({
            username: e.target.value,
        });
    }

    handlePasswordChange = (e) => {
        this.setState({
            password: e.target.value,
        });
    }

    handleIsadminChange = (e) => {
        this.setState({
            is_admin: e.target.value,
        });
    }


    ifValid(data) {
        return data === "Signup Successful";
    }

    login(data) {
        window.location.href = '';
        alert(this.state.username + ", you are successfully signed up!");
    }

    raiseErr(data) {
        document.getElementById('r_username').setCustomValidity(data);
        document.getElementById('r_username').reportValidity();
    }

    validatedData = () => {
        const username = this.state.username;
        const password = this.state.password;
        if (username.length < 6 || username.length > 16) {
            document.getElementById('r_username').setCustomValidity("Username must be from 6 to 16 symbols.");
            document.getElementById('r_username').reportValidity();
            return false;
        }
        else if (password.length < 6 || password.length > 16) {
            document.getElementById('r_password').setCustomValidity("Password must be from 6 to 16 symbols.");
            document.getElementById('r_password').reportValidity();
            return false;
        }
        return true;
    }
    handleSubmit = () => {
        if (!this.validatedData()) {
            return;
        }
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                username: this.state.username,
                password: this.state.password,
                isAdmin: this.state.isAdmin,
            }),
        }
        fetch(server_url, requestOptions).then((response) =>
            response.json()
        ).then((data) => this.ifValid(data) ? this.login(data) : this.raiseErr(data));
    }


    render() {
        return (
            <Form className="w-50 p-3 mx-auto">
                <h1>Sign Up</h1>

                <div className="my-4">
                    <Form.Label>
                        <mark><strong>Username</strong></mark>
                    </Form.Label>
                    <Form.Control
                        onChange={this.handleUsernameChange}
                        placeholder="My Username"
                        id="r_username"
                    />
                    <Form.Text className="text-warning">
                        Your username must contain 6-16 symbols.
                    </Form.Text>
                </div>

                <div className="my-4">
                <Form.Label>
                        <mark><strong>Password</strong></mark>
                    </Form.Label>
                    <Form.Control
                        onChange={this.handlePasswordChange}
                        placeholder="1111abc"
                        type="password"
                        id="r_password"
                    />
                    <Form.Text className="text-warning">
                        Your password must be 6-16 characters long.
                    </Form.Text>
                </div>
                <div className="my-2">
                    <Form.Check
                        onChange={this.handleIsadminChange}
                        label="Is admin?"
                        id="checkbox1"
                    />
                </div>

                <Link className="my-2" to="/">Log in</Link>

                <Button className="my-2 w-100" onClick={this.handleSubmit}>Sign Up</Button>
            </Form>
        )
    }
}
