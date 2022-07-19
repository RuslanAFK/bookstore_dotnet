import React, { Component } from "react";

import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoadBook from "./components/LoadBook";
import ChangeBook from "./components/ChangeBook";
import DeleteBook from "./components/DeleteBook";
import Login from "./components/Login";
import Signup from "./components/Signup";
import BookView from "./components/BookView";
import BookStore from "./components/BookStore";

export default class App extends Component {

    render() {
        return (
            <div className="center">
                <Router>
                    <Routes>
                        <Route exact path="/" element={<Login />} />
                        <Route path="/signup" element={<Signup />} />

                        <Route path="/add" element={<LoadBook />} />
                        <Route path="/change" element={<ChangeBook />} />
                        <Route path="/delete" element={<DeleteBook />} />

                        <Route path="/books" element={<BookStore />} />
                        <Route path="/view" element={<BookView />} />
                    </Routes>
                </Router>
            </div>
        )
    }

}
