import React, { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { Link } from "react-router-dom";
import { throwHttpError } from "../helpers/ErrorThrowers";
import { SIGNUP_URL } from "../helpers/Urls";
import { isUserDataValid } from "../helpers/Validators";

const Signup = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [isAdmin, setIsAdmin] = useState(false);

    const login = (data) => {
        window.location.href = '';
        alert(username + ", you are successfully signed up!");
    }

    const handleSubmit = () => {
        const userData = { username, password };
        if (!isUserDataValid(userData)) {
            return;
        }
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                username,
                password,
                isAdmin,
            }),
        }
        fetch(SIGNUP_URL, requestOptions)
            .then(data => throwHttpError(data))
            .then((response) => response.json())
            .then((data) => login(data))
            .catch(errorMessage => alert(errorMessage))
    }

    return (
        <Form className="w-50 p-3 mx-auto">
            <h1>Sign Up</h1>

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

            <div className="my-4">
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
            <div className="my-2">
                <Form.Check
                    onChange={(e) => setIsAdmin(e.target.value)}
                    label="Is admin?"
                />
            </div>

            <Link className="my-2" to="/">Log in</Link>
            <Button className="my-2 w-100" onClick={handleSubmit}>Sign Up</Button>
        </Form>
    )
}

export default Signup;
