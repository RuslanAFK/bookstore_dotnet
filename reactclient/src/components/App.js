import React, { Component } from "react";

import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoadBook from "./LoadBook";
import ChangeBook from "./ChangeBook";
import DeleteBook from "./DeleteBook";
import Login from "./Login";
import Signup from "./Signup";
import BookView from "./BookView";
import UserPage from "./UserPage";
import BookStore from "./BookStore";



export default class App extends Component {
    constructor(props) {
        super(props);
        this.state = {
            username: '',
            is_admin: false,
        };
    }

    render() {
        return (
            <div className="center">
                <Router>
                    <Routes>
                        <Route exact path="/" element={<Login />} />
                        <Route path="/signup" element={<Signup />} />

                        <Route path="/change" element={<ChangeBook />} />
                        <Route path="/delete" element={<DeleteBook />} />

                        <Route path="/books" element={<BookStore />} />
                        <Route path="/book" element={<BookView />} />
                    </Routes>
                </Router>
            </div>
        )
    }

}
