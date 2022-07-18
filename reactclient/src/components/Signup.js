import React from "react";
import {Button, Form} from "react-bootstrap";

const server_url = 'https://localhost:7180/signup';

export default class Signup extends React.Component
{

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

    handlePasswordChange= (e) => {
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
        alert(this.state.username+", you are successfully signed up!");
    }

    raiseErr(data) {
        document.getElementById('r_username').setCustomValidity(data);
        document.getElementById('r_username').reportValidity();
    }

    validatedData = () => {
        const username = this.state.username;
        const password = this.state.password;
        if(username.length < 6 || username.length > 16){
            document.getElementById('r_username').setCustomValidity("Username must be from 6 to 16 symbols.");
            document.getElementById('r_username').reportValidity();
            return false;
        }
        else if(password.length < 6 || password.length > 16){
            document.getElementById('r_password').setCustomValidity("Password must be from 6 to 16 symbols.");
            document.getElementById('r_password').reportValidity();
            return false;
        }
        return true;
    }
    handleSubmit = () => {
        if(!this.validatedData()){
            return;
        }
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({
                username: this.state.username,
                password: this.state.password,
                isAdmin: this.state.isAdmin,
            }),
        }
        fetch(server_url, requestOptions).then((response)=>
            response.json()
        ).then((data) => this.ifValid(data) ? this.login(data) : this.raiseErr(data));
    }


    render(){
        return(
            <Form className="rl_form">

                <h1>Sign Up</h1>
                <Form.Label htmlFor="inputUsername">Username</Form.Label>
                <Form.Control
                    onChange={this.handleUsernameChange}
                    placeholder="My Username"
                    id="r_username"
                />
                <Form.Text className="text-warning">
                    Your username must contain 4-10 letters and then 0-4 numbers.
                </Form.Text>
                <br/>

                <Form.Label htmlFor="inputPassword">Password</Form.Label>
                <Form.Control
                    onChange={this.handlePasswordChange}
                    placeholder="1111abc"
                    type="password"
                    id="r_password"
                />
                <Form.Text className="text-warning">
                    Your password must be 10-16 characters long.
                </Form.Text>
                <br/>


                <Form.Check
                    onChange={this.handleIsadminChange}
                    label="Is admin?"
                    id="checkbox1"
                />
                <Button className="one_but" onClick={this.handleSubmit}>Sign Up</Button>
            </Form>
        )
    }
}
