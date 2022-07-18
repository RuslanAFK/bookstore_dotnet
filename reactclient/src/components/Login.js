import React from "react";
import { Button, Form } from "react-bootstrap";
import { Link, Navigate } from "react-router-dom";

const server_url = 'https://localhost:7180/login';

export default class Login extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            is_admin: false,
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

    ifValid = (data) => {
        console.log(data);
        return data.username != null;
    }

    login(data) {
       window.location.href = `books?id=${data.id}`;
    }

    raiseErr(data) {
        document.getElementById('l_username').setCustomValidity(data);
        document.getElementById('l_username').reportValidity();
    }

    validatedData = () => {
        const username = this.state.username;
        const password = this.state.password;
        if(username.length < 6 || username.length > 16){
            document.getElementById('l_username').setCustomValidity("Username must be from 6 to 16 symbols.");
            document.getElementById('l_username').reportValidity();
            return false;
        }
        else if(password.length < 6 || password.length > 16){
            document.getElementById('l_password').setCustomValidity("Password must be from 6 to 16 symbols.");
            document.getElementById('l_password').reportValidity();
            return false;
        }
        return true;
    }

    handleSubmit = () => {
        if(!this.validatedData()){
            return;
        }
        let requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                username: this.state.username,
                password: this.state.password
            }),
        }
        fetch(server_url, requestOptions).then((response) =>
            response.json()
        ).then((data) => this.ifValid(data) ? this.login(data) : this.raiseErr(data));
    }


    render() {
        return (
            <div>
                <Form className="rl_form" id="login_form">
                    <h1>Log In</h1>

                    <Form.Label htmlFor="inputUsername">Username</Form.Label>
                    <Form.Control
                        onChange={this.handleUsernameChange}
                        name="username"
                        placeholder="My Username"
                        id="l_username"
                    />
                    <Form.Text className="text-warning">
                        Your username must be valid.
                    </Form.Text>
                    <br />

                    <Form.Label htmlFor="inputPassword">Password</Form.Label>
                    <Form.Control
                        onChange={this.handlePasswordChange}
                        name="password"
                        placeholder="1111abc"
                        type="password"
                        id="l_password"
                    />
                    <Form.Text className="text-warning">
                        Your password must be same as in database.
                    </Form.Text>
                    <br />

                    <Link to='/signup'>Not registed yet?</Link>
                    <br />
                    <br />

                    <Button onClick={this.handleSubmit}>Log In</Button>
                </Form>

            </div>

        )
    }
}
