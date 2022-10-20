import React, { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { throwHttpError } from "../helpers/ErrorThrowers";
import { LOGIN_URL } from "../helpers/Urls";
import { isUserDataValid } from "../helpers/Validators";

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const login = (data) => {
        window.location.href = `books?id=${data.id}`;
    }

    const handleSubmit = () => {
        const userData = { username, password };
        if (!isUserDataValid(userData)) {
            return;
        }
        let requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(userData)
        }
        fetch(LOGIN_URL, requestOptions)
            .then(data => throwHttpError(data))
            .then((response) => response.json())
            .then((data) => login(data))
            .catch(errorMessage => alert(errorMessage))
    }

    return (
        <Form className="w-50 p-3 mx-auto">
            <h1>Log In</h1>

            <div className="my-4">
                <Form.Label>
                    <mark><strong>Username</strong></mark>
                </Form.Label>
                <Form.Control
                    onChange={(e) => setUsername(e.target.value)}
                    placeholder="My Username"
                />
                <Form.Text className="text-warning">
                    Your username must contain 6-16 symbols.
                </Form.Text>
            </div>

            <div className="my-2">
                <Form.Label>
                    <mark><strong>Password</strong></mark>
                </Form.Label>
                <Form.Control
                    onChange={(e) => setPassword(e.target.value)}
                    placeholder="1111abc"
                    type="password"
                />
                <Form.Text className="text-warning">
                    Your password must be 6-16 characters long.
                </Form.Text>
            </div>
            <Link className="my-2" to="signup">Not Registered yet?</Link>

            <Button className="my-2 w-100" onClick={handleSubmit}>Log In</Button>
        </Form>
    )
}

export default Login;
