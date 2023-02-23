import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoadBook from "./features/books/components/LoadBook";
import Auth from "./features/auth/components/Auth";
import BookView from "./features/books/components/BookView";
import BookList from "./features/books/components/BookList";
import Navbar from "./features/shared/components/navbar/Navbar";

import 'react-toastify/dist/ReactToastify.css'
import Home from "./features/shared/components/home/Home";
import UserList from "./features/users/components/UserList";
import Profile from "./features/auth/components/Profile";
import FileUpload from "./features/books/components/FileUpload";
import React from "react";

const App = () => {
    return (
        <div className="center">
            <Router>
                <Navbar/>
                <Routes>
                    <Route path="/" element={<Home />} />

                    <Route path="/login" element={<Auth page="login" />} />
                    <Route path="/register" element={<Auth page="register" />} />
                    <Route path="/profile" element={<Profile />} />

                    <Route path="/books" element={<BookList />} />
                    <Route path="/book/:id" element={<BookView />} />
                    <Route path="/load" element={<LoadBook page="create" />} />
                    <Route path="/update/:id" element={<LoadBook page="update" />} />

                    <Route path="/upload-file/:id" element={<FileUpload />} />

                    <Route path="/users" element={<UserList />} />

                </Routes>
            </Router>
        </div>
    )
}

export default App;
