import React, {useEffect} from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoadBook from "./features/books/components/LoadBook";
import Auth from "./features/auth/components/Auth";
import BookView from "./features/books/components/BookView";
import BookList from "./features/books/components/BookList";
import Navbar from "./components/Navbar";

import 'react-toastify/dist/ReactToastify.css'
import Home from "./components/Home";
import UserList from "./features/users/components/UserList";
import Profile from "./features/auth/components/Profile";

const App = () => {
    return (
        <div className="center">
            <Router>
                <Navbar/>
                <Routes>
                    <Route exact path="/" element={<Home />} />

                    <Route path="/login" element={<Auth />} />
                    <Route path="/register" element={<Auth isRegisterPage />} />
                    <Route path="/profile" element={<Profile />} />

                    <Route path="/books" element={<BookList />} />
                    <Route path="/book/:id" element={<BookView />} />
                    <Route path="/load" element={<LoadBook />} />
                    <Route path="/update/:id" element={<LoadBook isUpdatePage />} />

                    <Route exact path="/users" element={<UserList />} />

                </Routes>
            </Router>
        </div>
    )
}

export default App;
